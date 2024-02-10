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
        public float latitude { get; private set; }
        public float longitude { get; private set; }
        public DateTime sunRise { get; private set; }
        public DateTime sunSet { get; private set; }

        public CoreLogic()
        {
            UpdateOverlayOpacityCurrent();
            SetDefaultSettings();
            Update();
        }

        public void Update()
        {
            UpdateDateTimes();
            UpdateDimmingPhase();
            UpdateOverlayOpacityCurrent();
            UpdateOverlayFormOpacity(opacityCurrent);
        }

        private void UpdateSun()
        {
            solarTimes = new SolarTimes(DateTime.Now, latitude, longitude);
            sunRise = TimeZoneInfo.ConvertTimeFromUtc(solarTimes.Sunrise.ToUniversalTime(), TimeZoneInfo.Local);
            sunSet = TimeZoneInfo.ConvertTimeFromUtc(solarTimes.Sunset.ToUniversalTime(), TimeZoneInfo.Local);
        }

        public void UpdateGeoLocation(float latitude, float longitude)
        {
            this.latitude = latitude;
            this.longitude = longitude;
            UpdateSun();
            Update();
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
                if(sunRise.Date < now.Date  && sunSet.Date < now.Date)
                    UpdateSun();

                nightTransitionStart = sunSet;
                dayTransitionStart = sunRise;
            }

            DateTime firstTransitionTime = (dayTransitionStart < nightTransitionStart) ? dayTransitionStart : nightTransitionStart;
            DateTime secondTransitionTime = (dayTransitionStart > nightTransitionStart) ? dayTransitionStart : nightTransitionStart;
            DateTime firstTransitionTimeTommorow = firstTransitionTime.AddDays(1);

            TimeSpan firstMaxTransitionTimeSpan = (secondTransitionTime - firstTransitionTime).Duration();
            TimeSpan secondMaxTransitionTimeSpan = (firstTransitionTimeTommorow - secondTransitionTime).Duration();
            maxTransitionTimeSpan = (firstMaxTransitionTimeSpan < secondMaxTransitionTimeSpan) ? firstMaxTransitionTimeSpan : secondMaxTransitionTimeSpan;

            int maxTransitionMinutes = (int)maxTransitionTimeSpan.TotalMinutes;
            if (transitionTimeSpan > maxTransitionTimeSpan){
                transitionTimeSpan = new TimeSpan(maxTransitionMinutes / 60, maxTransitionMinutes % 60, 0);
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
            // translation of opacity range from [0,100] to [0,Min(1,MaxTrueOpacity)] for overlayForm
            Func<float, float> TrueOverlayOpacity = x => Math.Max(Math.Min(x / 100.0f, 1.0f), 0.0f) * maxTrueOpacity;
            overlayFormOpacity = TrueOverlayOpacity(opacityPercentage);
        }

        public int GetUpdateTimerInterval()
        {
            if(dimmingPhase == DimmingPhase.DayStart || dimmingPhase == DimmingPhase.NightStart)
            {
                return (int)Math.Min(Math.Floor(transitionTimeSpan.TotalMilliseconds / transitionSteps), DefaultSettings.MaxTransitionUpdateIntervalSeconds * 1000f);
            }

            DateTime nextUpdateDateTime = (dimmingPhase == DimmingPhase.Day) ? nightTransitionStart : dayTransitionStart;
            if (now > nextUpdateDateTime)
            {
                nextUpdateDateTime = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0).AddDays(1);
            }

            return (int)Math.Floor(Math.Abs(nextUpdateDateTime.Subtract(now).Duration().TotalMilliseconds));
        }
    }
}
