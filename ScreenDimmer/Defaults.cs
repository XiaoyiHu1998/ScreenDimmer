using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace ScreenDimmer
{
    public static class Default
    {
        public static float UpdateIntervalSeconds = 1.0f;

        public static bool EnableDimming = true;
        public static int OpacityDay = 10;
        public static int OpacityNight = 60;
        public static float MaxTrueOpacity = 0.8f;
        public static bool dimSettingsForm = false;
        public static bool RunOnStartup = true;

        public static bool EnableTransition = false;
        public static DateTime TransitionStart = new DateTime(DateTime.Now.Year, 1, 1, 19, 30, 0);
        public static DateTime TransitionEnd = new DateTime(DateTime.Now.Year, 1, 1, 6, 00, 0);
        public static DateTime TransitionTime = new DateTime(DateTime.Now.Year, 1, 1, 2, 30, 0);
        public static int TransitionTimeHourIndex = 1;
        public static int TransitionTimeMinutesIndex = 2;

        public static bool PreviewEnabled = false;
        public static PreviewSelection PreviewState = PreviewSelection.Day;
    }

    public partial class CoreLogic
    {
        private void SetDefaultValues()
        {
            this.dimmingEnabled = Default.EnableDimming;
            this.opacityDay = Default.OpacityDay;
            this.opacityNight = Default.OpacityNight;
            this.maxTrueOpacity = Default.MaxTrueOpacity;

            this.nightStartHour = Default.TransitionStart.Hour;
            this.nightStartMinute = Default.TransitionStart.Minute;
            this.dayStartHour = Default.TransitionEnd.Hour;
            this.dayStartMinute = Default.TransitionEnd.Minute;
            this.transitionTimeHour = SettingsForm.BoxIndexToHour(Default.TransitionTimeHourIndex);
            this.transitionTimeMinute = SettingsForm.BoxIndexToMinute(Default.TransitionTimeMinutesIndex);
            this.transitionTimeSpan = new TimeSpan(transitionTimeHour, transitionTimeMinute, 0);
            this.previewEnabled = Default.PreviewEnabled;
            this.previewSelection = Default.PreviewState;
        }
    }

    public partial class SettingsForm
    {
        private void SetDefaultValues()
        {
            this.DimmingEnableCheckBox.Checked = Default.EnableDimming;
            this.OpacityDayValueBox.Text = Default.OpacityDay.ToString();
            this.OpacityNightValueBox.Text = Default.OpacityNight.ToString();
            this.OpacityDaySlider.Value = Default.OpacityDay;
            this.OpacityNightSlider.Value = Default.OpacityNight;
            this.dimSettingsForm = Default.dimSettingsForm;
            this.DimWindowCheckBox.Checked = Default.dimSettingsForm;
            this.RunOnStartUpCheckBox.Checked = Default.RunOnStartup;


            this.NightTransitionEnabledCheckBox.Checked = Default.EnableTransition;
            this.NightStartMinuteDateTimePicker.Value = Default.TransitionStart;
            this.NightStartHourDateTimePicker.Value = Default.TransitionStart;
            this.DayStartMinuteDateTimePicker.Value = Default.TransitionEnd;
            this.DayStartHourDateTimePicker.Value = Default.TransitionEnd;
            this.TransitionTimeMinuteDateTimePicker.Value = Default.TransitionTime;
            this.TransitionTimeHourDateTimePicker.Value = Default.TransitionTime;

            this.PreviewEnableCheckBox.Checked = Default.PreviewEnabled;
            this.PreviewDayRadioButton.Checked = true;
            this.PreviewNightRadioButton.Checked = false;
        }
    }
}
