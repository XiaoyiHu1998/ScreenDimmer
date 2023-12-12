using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

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

    public partial class SettingsForm : Form
    {
        //Internal logic members;
        private List<OverlayForm> overlayForms;
        private readonly object updateLock = new object();
        private PreviewSelection previewSelection;
        private DimmingPhase dimmingPhase = DimmingPhase.Day;

        private DateTime now;
        private DateTime dayTransitionStart;
        private DateTime dayTransitionEnd;
        private DateTime nightTransitionStart;
        private DateTime nightTransitionEnd;
        private TimeSpan transitionTimeSpan;

        private float maxTrueOpacity;

        //UI members
        public bool dimmingEnabled;
        public int opacityDay;
        public int opacityNight;
        public int overlayOpacityCurrent;

        public bool nightTransitionEnabled;
        public int dayStartHour;
        public int dayStartMinute;
        public int nightStartHour;
        public int nightStartMinute;
        public int transitionTimeHour;
        public int transitionTimeMinute;

        public bool previewEnabled;

        public SettingsForm()
        {
            overlayForms = new List<OverlayForm>();
            UpdateOverlayOpacityCurrent();
            for (int i = 0; i < Screen.AllScreens.Length; i++)
            {
                OverlayForm currentOverlayForm = new OverlayForm(overlayOpacityCurrent);
                Screen currentScreen = Screen.AllScreens[i];

                currentOverlayForm.StartPosition = FormStartPosition.Manual;
                currentOverlayForm.Location = currentScreen.Bounds.Location;
                currentOverlayForm.Bounds = currentScreen.Bounds;

                overlayForms.Add(currentOverlayForm);
                overlayForms[i].Show();
            }

            InitializeComponent();
            setDefaultLogicValues();
            SetDefaultUIValues();
            UpdateOverlayForms();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.DoubleBuffered = true;
            this.ShowInTaskbar = true;
        }
         
        public void UpdateOverlayForms()
        {
            lock (updateLock)
            {
                UpdateDateTimes();
                UpdateTotalTransitionTimeMinutes();
                UpdateDimmingPhase();
                UpdateOverlayOpacityCurrent();
                SetOpacity(overlayOpacityCurrent);
            }
        }

        private void UpdateDateTimes()
        {
            Func<DateTime, DateTime> TransitionEndDateTime = startDateTime => startDateTime.AddHours(transitionTimeHour).AddMinutes(transitionTimeMinute);

            now = DateTime.Now;
            nightTransitionStart = new DateTime(now.Year, now.Month, now.Day, nightStartHour, nightStartMinute, 0);
            dayTransitionStart = new DateTime(now.Year, now.Month, now.Day, dayStartHour, dayStartMinute, 0);

            //if (dayTransitionStart <= nightTransitionStart)
            //    dayTransitionStart = dayTransitionStart.AddDays(1);

            nightTransitionEnd = TransitionEndDateTime(nightTransitionStart);
            dayTransitionEnd = TransitionEndDateTime(dayTransitionStart);
        }

        private void UpdateTotalTransitionTimeMinutes()
        {
            transitionTimeSpan = new TimeSpan(transitionTimeHour, transitionTimeMinute, 0);
        }

        private void UpdateDimmingPhase()
        {
            if(dayTransitionStart <= nightTransitionStart)
            {
                if (dayTransitionStart <= now && now < nightTransitionStart)
                {
                    dimmingPhase = (now <= dayTransitionEnd) ? DimmingPhase.DayStart : DimmingPhase.Day;
                    return;
                }
            }
            else
            {
                if(dayTransitionStart <= now || now < nightTransitionStart)
                {
                    dimmingPhase = (now <= dayTransitionEnd) ? DimmingPhase.DayStart : DimmingPhase.Day;
                    return;
                }
            }

            dimmingPhase = (nightTransitionStart <= now && now <= nightTransitionEnd) ? DimmingPhase.NightStart : DimmingPhase.Night;
        }

        private int InterpolateOpacity(int transitionTimePassed)
        {
            Func<float, float> EasingFunction = x => (float)(-(Math.Cos(Math.PI * x) - 1.0f) / 2.0f);

            float interpolationPoint = (float)transitionTimePassed / (float)transitionTimeSpan.TotalMinutes;
            if (dimmingPhase == DimmingPhase.DayStart)
                interpolationPoint = 1.0f - interpolationPoint;

            float interpolationFactor =  EasingFunction(interpolationPoint);

            return (int)((opacityNight - opacityDay) * interpolationFactor + opacityDay);
        }

        private void UpdateOverlayOpacityCurrent()
        {
            if(previewEnabled)
            {
                overlayOpacityCurrent = previewSelection == PreviewSelection.Day ? opacityDay : opacityNight;
                return;
            }

            if (!dimmingEnabled)
            {
                overlayOpacityCurrent = 0;
                return;
            }

            if (!nightTransitionEnabled)
            {
                overlayOpacityCurrent = opacityDay;
                return;
            }

            int dayStartDelta = (int)Math.Abs(dayTransitionStart.Subtract(now).TotalMinutes);
            int nightStartDelta = (int)Math.Abs(nightTransitionStart.Subtract(now).TotalMinutes);

            switch (dimmingPhase)
            {
                case DimmingPhase.Day:
                    overlayOpacityCurrent = opacityDay;
                    break;
                case DimmingPhase.DayStart:
                    overlayOpacityCurrent = InterpolateOpacity(dayStartDelta);
                    break;
                case DimmingPhase.Night:
                    overlayOpacityCurrent = opacityNight;
                    break;
                case DimmingPhase.NightStart:
                    overlayOpacityCurrent = InterpolateOpacity(nightStartDelta);
                    break;
            }
        }

        private void SetOpacity(int opacityPercentage)
        {
            Func<int, float> TrueOverlayOpacity = x => Math.Max(Math.Min((float)x / 100.0f, maxTrueOpacity), 0.0f);
            float trueOverlayOpacity = TrueOverlayOpacity(opacityPercentage);

            foreach (OverlayForm overlayForm in overlayForms)
            {
                overlayForm.SetOpacity(trueOverlayOpacity);
                overlayForm.Refresh();
            }
        }

        private void EnableDimming()
        {
            dimmingEnabled = true;
            UpdateOverlayForms();
        }

        private void DisableDimming()
        {
            dimmingEnabled = false;
            UpdateOverlayForms();
        }

        private int BoxIndexToHour(int boxIndex)
        {
            return boxIndex;
        }

        private int BoxIndexToMinute(int boxIndex)
        {
            return boxIndex * 15;
        }
    }
}
