using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace ScreenDimmer
{
    partial class SettingsForm
    {
        //Default UI values
        private bool defaultEnableDimming = true;
        private int defaultOpacityDay = 0;
        private int defaultOpacityNight = 40;
        private float defaultMaxTrueOpacity = 0.8f;

        private bool defaultEnableTransition = false;
        private int defaultStartHourIndex = 19;
        private int defaultStartMinutesIndex = 0;
        private int defaultEndHourIndex = 7;
        private int defaultEndMinutesIndex = 0;
        private int defaultTransitionTimeHourIndex = 1;
        private int defaultTransitionTimeMinutesIndex = 2;

        private bool defaultPreviewEnabled = false;
        private PreviewSelection defaultPreviewState = PreviewSelection.Day;

        private void setDefaultLogicValues()
        {
            this.dimmingEnabled = defaultEnableDimming;
            this.opacityDay = defaultOpacityDay;
            this.opacityNight = defaultOpacityNight;
            this.maxTrueOpacity = defaultMaxTrueOpacity;

            this.nightStartHour = BoxIndexToHour(defaultStartHourIndex);
            this.nightStartMinute = BoxIndexToMinute(defaultStartMinutesIndex);
            this.dayStartHour = BoxIndexToHour(defaultEndHourIndex);
            this.dayStartMinute = BoxIndexToMinute(defaultEndMinutesIndex);
            this.transitionTimeHour = BoxIndexToHour(defaultTransitionTimeHourIndex);
            this.transitionTimeMinute = BoxIndexToMinute(defaultTransitionTimeMinutesIndex);
            this.transitionTimeSpan = new TimeSpan(transitionTimeHour, transitionTimeMinute, 0);
            this.previewEnabled = defaultPreviewEnabled;
        }

        private void SetDefaultUIValues()
        {
            this.DimmingEnableCheckBox.Checked = defaultEnableDimming;
            this.OpacityDayValueBox.Text = defaultOpacityDay.ToString();
            this.OpacityNightValueBox.Text = defaultOpacityNight.ToString();
            this.OpacityDaySlider.Value = defaultOpacityDay;
            this.OpacityNightSlider.Value = defaultOpacityNight;

            this.NightTransitionEnabledCheckedBox.Checked = defaultEnableTransition;
            this.NightStartMinuteBox.SelectedIndex = defaultStartMinutesIndex;
            this.NightStartHourBox.SelectedIndex = defaultStartHourIndex;
            this.DayStartMinuteBox.SelectedIndex = defaultEndMinutesIndex;
            this.DayStartHourBox.SelectedIndex = defaultEndHourIndex;
            this.TransitionTimeMinuteBox.SelectedIndex = defaultTransitionTimeMinutesIndex;
            this.TransitionTimeHourBox.SelectedIndex = defaultTransitionTimeHourIndex;

            this.PreviewEnableCheckedBox.Checked = defaultPreviewEnabled;
            this.previewSelection = defaultPreviewState;
            this.PreviewDayRadioButton.Checked = true;
            this.PreviewNightRadioButton.Checked = false;
        }
    }
}
