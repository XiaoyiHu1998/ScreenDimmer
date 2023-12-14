﻿using System;
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

        //Public members
        public PreviewSelection previewSelection;
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

        public CoreLogic()
        {
            UpdateOverlayOpacityCurrent();
            SetDefaultValues();
            Update();
        }

        public void Update()
        {
            UpdateDateTimes();
            UpdateTotalTransitionTimeMinutes();
            UpdateDimmingPhase();
            UpdateOverlayOpacityCurrent();
            UpdateOverlayFormOpacity(opacityCurrent);
        }

        private void UpdateDateTimes()
        {
            Func<DateTime, DateTime> TransitionEndDateTime = startDateTime => startDateTime.AddHours(transitionTimeHour).AddMinutes(transitionTimeMinute);

            now = DateTime.Now;
            nightTransitionStart = new DateTime(now.Year, now.Month, now.Day, nightStartHour, nightStartMinute, 0);
            dayTransitionStart = new DateTime(now.Year, now.Month, now.Day, dayStartHour, dayStartMinute, 0);

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

        private float InterpolateOpacity(int transitionTimePassed)
        {
            Func<float, float> EasingFunction = x => (float)(-(Math.Cos(Math.PI * x) - 1.0f) / 2.0f);

            float interpolationPoint = (float)transitionTimePassed / (float)transitionTimeSpan.TotalSeconds;
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

            int dayStartDelta = (int)Math.Abs(dayTransitionStart.Subtract(now).TotalSeconds);
            int nightStartDelta = (int)Math.Abs(nightTransitionStart.Subtract(now).TotalSeconds);

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

        public void EnableDimming()
        {
            dimmingEnabled = true;
            Update();
        }

        public void DisableDimming()
        {
            dimmingEnabled = false;
            Update();
        }
    }
}
