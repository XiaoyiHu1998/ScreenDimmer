using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using Innovative.SolarCalculator;

namespace ScreenDimmer
{
    public enum PreviewSelection
    {
        Day,
        Night
    }

    public enum DimmingPhase
    {
        Day,
        DayStart,
        Night,
        NightStart
    }

    public partial class CoreLogic
    {
        //Internal logic members;
        private DimmingPhase dimmingPhase = DimmingPhase.Day;
        private float maxTrueOpacity;
        private float opacityCurrent;

        private DateTime now;
        private DateTime dayTransitionStart;
        private DateTime dayTransitionEnd;
        private DateTime nightTransitionStart;
        private DateTime nightTransitionEnd;
        private TimeSpan transitionTimeSpan;
        private int transitionSteps;

        private SolarTimes solarTimes;
        private System.Timers.Timer sunUpdateTimer;
        private DateTime sunUpdateTime;

        //Public members
        public PreviewSelection previewSelection;
        public TimeSpan maxTransitionTimeSpan { get; private set; }
        public float overlayFormOpacity { get; private set; }

        public bool dimmingEnabled;
        public int opacityDay;
        public int opacityNight;

        public bool nightTransitionEnabled;
        public int dayStartHour;
        public int dayStartMinute;
        public int nightStartHour;
        public int nightStartMinute;
        public int transitionTimeHour;
        public int transitionTimeMinute;

        public bool previewEnabled;
        public bool sunBasedDimming;
        public float latitude;
        public float longitude;
        public DateTime sunRise { get; private set; }
        public DateTime sunSet { get; private set; }

        public CoreLogic()
        {
            UpdateOverlayOpacityCurrent();
            SetDefaultSettings();
            SetupSunUpdateTimer();
            Update();
        }

        public void Update()
        {
            UpdateDateTimes();
            UpdateDimmingPhase();
            UpdateOverlayOpacityCurrent();
            UpdateOverlayFormOpacity(opacityCurrent);
        }

        private DateTime GetNextSunUpdateDateTime()
        {
            DateTime nextUpdate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, sunUpdateTime.Hour, sunUpdateTime.Minute, sunUpdateTime.Second);
            while (nextUpdate < DateTime.Now)
            {
                nextUpdate = nextUpdate.AddDays(1);
            }

            return nextUpdate;
        }

        private void UpdateSun()
        {
            solarTimes = new SolarTimes(DateTime.Now, latitude, longitude);
            sunRise = TimeZoneInfo.ConvertTimeFromUtc(solarTimes.Sunrise.ToUniversalTime(), TimeZoneInfo.Local);
            sunSet = TimeZoneInfo.ConvertTimeFromUtc(solarTimes.Sunset.ToUniversalTime(), TimeZoneInfo.Local);
        }

        private void SunUpdateTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            UpdateSun();
            sunUpdateTimer.Stop();
            sunUpdateTimer.Interval = (int)GetNextSunUpdateDateTime().Subtract(now).TotalMilliseconds;
            sunUpdateTimer.Start();
        }
            
        private void SetupSunUpdateTimer()
        {
            solarTimes = new SolarTimes(DateTime.Now, latitude, longitude);
            sunRise = solarTimes.Sunrise.ToLocalTime();
            sunSet = solarTimes.Sunset.ToLocalTime();

            sunUpdateTimer = new System.Timers.Timer();
            sunUpdateTimer.Elapsed += new System.Timers.ElapsedEventHandler(this.SunUpdateTimer_Elapsed);
            sunUpdateTimer.Interval = (int)GetNextSunUpdateDateTime().Subtract(DateTime.Now).TotalMilliseconds;
            sunUpdateTimer.Start();
        }

        private void UpdateDateTimes()
        {
            Func<DateTime, DateTime> TransitionEndDateTime = startDateTime => startDateTime.AddHours(transitionTimeSpan.TotalMinutes / 60).AddMinutes(transitionTimeSpan.TotalMinutes % 60);

            now = DateTime.Now;
            nightTransitionStart = new DateTime(now.Year, now.Month, now.Day, nightStartHour, nightStartMinute, 0);
            dayTransitionStart = new DateTime(now.Year, now.Month, now.Day, dayStartHour, dayStartMinute, 0);
            transitionTimeSpan = new TimeSpan(transitionTimeHour, transitionTimeMinute, 0);

            if (sunBasedDimming)
            {
                nightTransitionStart = sunSet;
                dayTransitionStart = sunRise;
            }

            DateTime firstTransitionTime = (dayTransitionStart < nightTransitionStart) ? dayTransitionStart : nightTransitionStart;
            DateTime secondTransitionTime = (dayTransitionStart > nightTransitionStart) ? dayTransitionStart : nightTransitionStart;
            DateTime firstTransitionTimeTommorow = firstTransitionTime.AddDays(1);

            TimeSpan firstMaxTransitionTimeSpan = (secondTransitionTime - firstTransitionTime).Duration();
            TimeSpan secondMaxTransitionTimeSpan = (firstTransitionTimeTommorow - secondTransitionTime).Duration();
            maxTransitionTimeSpan = (firstMaxTransitionTimeSpan < secondMaxTransitionTimeSpan) ? firstMaxTransitionTimeSpan : secondMaxTransitionTimeSpan;

            int maxTotalMinutes = (int)maxTransitionTimeSpan.TotalMinutes;
            if (transitionTimeSpan > maxTransitionTimeSpan){
                transitionTimeSpan = new TimeSpan(maxTotalMinutes / 60, maxTotalMinutes % 60, 0);
            }

            nightTransitionEnd = TransitionEndDateTime(nightTransitionStart);
            dayTransitionEnd = TransitionEndDateTime(dayTransitionStart);
        }

        private void UpdateDimmingPhase()
        {
            if(dayTransitionStart < nightTransitionStart)
            {
                if (dayTransitionStart <= now && now < nightTransitionStart)
                {
                    dimmingPhase = (dayTransitionStart <= now && now <= dayTransitionEnd) ? DimmingPhase.DayStart : DimmingPhase.Day;
                    return;
                }
            }
            else
            {
                if(dayTransitionStart <= now || now < nightTransitionStart)
                {
                    dimmingPhase = (dayTransitionStart <= now && now <= dayTransitionEnd) ? DimmingPhase.DayStart : DimmingPhase.Day;
                    return;
                }
            }

            dimmingPhase = (nightTransitionStart <= now && now <= nightTransitionEnd) ? DimmingPhase.NightStart : DimmingPhase.Night;
        }

        private float InterpolateOpacity(int transitionTimePassed)
        {
            Func<float, float> EasingFunction = x => (float)(-(Math.Cos(Math.PI * x) - 1.0f) / 2.0f);

            float interpolationPoint = Math.Min(Math.Max(transitionTimePassed / (float)transitionTimeSpan.TotalMilliseconds, 0.0f), 1.0f);
            if (dimmingPhase == DimmingPhase.DayStart)
                interpolationPoint = 1.0f - interpolationPoint;

            return (float)(opacityNight - opacityDay) * EasingFunction(interpolationPoint) + opacityDay;
        }

        private void UpdateOverlayOpacityCurrent()
        {
            if(previewEnabled)
            {
                opacityCurrent = previewSelection == PreviewSelection.Day ? opacityDay : opacityNight;
                return;
            }

            if (!dimmingEnabled)
            {
                opacityCurrent = 0;
                return;
            }

            if (!nightTransitionEnabled)
            {
                opacityCurrent = opacityDay;
                return;
            }

            int dayStartDelta = (int)Math.Abs(dayTransitionStart.Subtract(now).Duration().TotalMilliseconds);
            int nightStartDelta = (int)Math.Abs(nightTransitionStart.Subtract(now).Duration().TotalMilliseconds);

            switch (dimmingPhase)
            {
                case DimmingPhase.Day:
                    opacityCurrent = opacityDay;
                    break;
                case DimmingPhase.DayStart:
                    opacityCurrent = InterpolateOpacity(dayStartDelta);
                    break;
                case DimmingPhase.Night:
                    opacityCurrent = opacityNight;
                    break;
                case DimmingPhase.NightStart:
                    opacityCurrent = InterpolateOpacity(nightStartDelta);
                    break;
            }
        }

        private void UpdateOverlayFormOpacity(float opacityPercentage)
        {
            Func<float, float> TrueOverlayOpacity = x => Math.Max(Math.Min(x / 100.0f, maxTrueOpacity), 0.0f);
            overlayFormOpacity = TrueOverlayOpacity(opacityPercentage);
        }

        public int GetUpdateTimerInterval()
        {
            if(dimmingPhase == DimmingPhase.DayStart || dimmingPhase == DimmingPhase.NightStart)
            {
                return (int)Math.Min(Math.Floor(transitionTimeSpan.TotalMilliseconds / transitionSteps), DefaultSettings.MaxTransitionUpdateIntervalSeconds * 1000f);
            }

            DateTime nextTransitionDateTime = (dimmingPhase == DimmingPhase.Day) ? nightTransitionStart : dayTransitionStart;
            if (now > nextTransitionDateTime)
            {
                nextTransitionDateTime = nextTransitionDateTime.AddDays(1);
            }

            return (int)Math.Floor(Math.Abs(nextTransitionDateTime.Subtract(now).Duration().TotalMilliseconds));
        }

        public void UpdateGeoLocation(float latitude, float longitude)
        {
            this.latitude = latitude;
            this.longitude = longitude;
            UpdateSun();
            Update();
        }

        public void SetSunUpdateTime(int hour = 0, int minute = 0, int second = 0)
        {
            int updateHour = Math.Max(Math.Min(hour, 60), 0);
            int updateMinute = Math.Max(Math.Min(hour, 60), 0);
            int updateSecond = Math.Max(Math.Min(hour, 60), 0);
            sunUpdateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, updateHour, updateMinute, updateSecond);
        }

        public Tuple<int,int> GetSunUpdateTime()
        {
            return new Tuple<int, int>(sunUpdateTime.Hour, sunUpdateTime.Minute);
        }
    }
}
