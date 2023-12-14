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
        //Default UI values
        public static bool EnableDimming = true;
        public static int OpacityDay = 10;
        public static int OpacityNight = 40;
        public static float MaxTrueOpacity = 0.8f;

        public static bool EnableTransition = false;
        public static int StartHourIndex = 19;
        public static int StartMinutesIndex = 0;
        public static int EndHourIndex = 7;
        public static int EndMinutesIndex = 0;
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

            this.nightStartHour = SettingsForm.BoxIndexToHour(Default.StartHourIndex);
            this.nightStartMinute = SettingsForm.BoxIndexToMinute(Default.StartMinutesIndex);
            this.dayStartHour = SettingsForm.BoxIndexToHour(Default.EndHourIndex);
            this.dayStartMinute = SettingsForm.BoxIndexToMinute(Default.EndMinutesIndex);
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

            this.NightTransitionEnabledCheckedBox.Checked = Default.EnableTransition;
            this.NightStartMinuteBox.SelectedIndex = Default.StartMinutesIndex;
            this.NightStartHourBox.SelectedIndex = Default.StartHourIndex;
            this.DayStartMinuteBox.SelectedIndex = Default.EndMinutesIndex;
            this.DayStartHourBox.SelectedIndex = Default.EndHourIndex;
            this.TransitionTimeMinuteBox.SelectedIndex = Default.TransitionTimeMinutesIndex;
            this.TransitionTimeHourBox.SelectedIndex = Default.TransitionTimeHourIndex;

            this.PreviewEnableCheckedBox.Checked = Default.PreviewEnabled;
            this.PreviewDayRadioButton.Checked = true;
            this.PreviewNightRadioButton.Checked = false;
        }
    }
}
