using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Text.Json;

namespace ScreenDimmer
{
    public static class DefaultSettings
    {
        public static string settingsFileName = "settings.json";
        public static JsonSerializerOptions serializerOptions = new JsonSerializerOptions { WriteIndented = true };
        public static float MaxTransitionUpdateIntervalSeconds = 1.0f;
        public static float JsonExportTimerIntervalSeconds = 5.0f;

        public static bool EnableDimming = true;
        public static int OpacityDay = 10;
        public static int OpacityNight = 60;
        public static float MaxTrueOpacity = 0.8f;

        public static int MinTransitionSteps = 60 * 30;
        public static bool EnableTransition = false;
        public static DateTime NightStart = new DateTime(DateTime.Now.Year, 1, 1, 19, 30, 0);
        public static DateTime DayStart = new DateTime(DateTime.Now.Year, 1, 1, 6, 00, 0);
        public static DateTime TransitionTime = new DateTime(DateTime.Now.Year, 1, 1, 2, 30, 0);

        public static bool PreviewEnabled = false;
        public static PreviewSelection PreviewState = PreviewSelection.Day;
        public static bool PreviewDay = true;
        public static bool PreviewNight = false;

        public static bool dimSettingsForm = false;
        public static bool RunOnStartup = true;
        public static bool SunBasedDimming = false;
        public static DateTime SunUpdateTime = new DateTime(DateTime.Now.Year, 1, 1, 12, 0, 0);
        public static float Latitude = 0.0f;
        public static float Longitude = 0.0f;
    }

    public struct SettingsFormValues
    {
        public int FormLocationX { get; set; }
        public int FormLocationY { get; set; }

        public bool EnableDimming { get; set; }
        public int OpacityDay { get; set; }
        public int OpacityNight { get; set; }
        public float MaxTrueOpacity { get; set; }

        public bool EnableTransition { get; set; }
        public int NightStartMinute { get; set; }
        public int NightStartHour { get; set; }
        public int DayStartMinute { get; set; }
        public int DayStartHour { get; set; }
        public int TransitionTimeMinute { get; set; }
        public int TransitionTimeHour { get; set; }

        public bool PreviewEnabled { get; set; }
        public bool PreviewDay { get; set; }
        public bool PreviewNight { get; set; }

        public bool dimSettingsForm { get; set; }
        public bool RunOnStartup { get; set; }
        public bool SunBasedDimming { get; set; }
        public int SunUpdateHour { get; set; }
        public int SunUpdateMinute { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
    }

    public partial class CoreLogic
    {
        private void SetDefaultSettings()
        {
            this.dimmingEnabled = DefaultSettings.EnableDimming;
            this.opacityDay = DefaultSettings.OpacityDay;
            this.opacityNight = DefaultSettings.OpacityNight;
            this.maxTrueOpacity = DefaultSettings.MaxTrueOpacity;

            this.minTransitionSteps = DefaultSettings.MinTransitionSteps;
            this.nightStartHour = DefaultSettings.NightStart.Hour;
            this.nightStartMinute = DefaultSettings.NightStart.Minute;
            this.dayStartHour = DefaultSettings.DayStart.Hour;
            this.dayStartMinute = DefaultSettings.DayStart.Minute;
            this.transitionTimeHour = DefaultSettings.TransitionTime.Hour;
            this.transitionTimeMinute = DefaultSettings.TransitionTime.Minute;
            this.transitionTimeSpan = new TimeSpan(transitionTimeHour, transitionTimeMinute, 0);
            this.latitude = DefaultSettings.Latitude;
            this.longitude = DefaultSettings.Longitude;
            this.sunUpdateTime = DefaultSettings.SunUpdateTime;

            this.previewEnabled = DefaultSettings.PreviewEnabled;
            this.previewSelection = DefaultSettings.PreviewState;
        }
    }

    public partial class SettingsForm
    {
        private void SetDefaultSettings()
        {
            this.CenterToScreen();

            this.DimmingEnableCheckBox.Checked = DefaultSettings.EnableDimming;
            this.OpacityDayValueBox.Text = DefaultSettings.OpacityDay.ToString();
            this.OpacityNightValueBox.Text = DefaultSettings.OpacityNight.ToString();
            this.OpacityDaySlider.Value = DefaultSettings.OpacityDay;
            this.OpacityNightSlider.Value = DefaultSettings.OpacityNight;
            this.dimSettingsForm = DefaultSettings.dimSettingsForm;

            this.NightTransitionEnabledCheckBox.Checked = DefaultSettings.EnableTransition;
            this.NightStartMinuteDateTimePicker.Value = DefaultSettings.NightStart;
            this.NightStartHourDateTimePicker.Value = DefaultSettings.NightStart;
            this.DayStartMinuteDateTimePicker.Value = DefaultSettings.DayStart;
            this.DayStartHourDateTimePicker.Value = DefaultSettings.DayStart;
            this.TransitionTimeMinuteDateTimePicker.Value = DefaultSettings.TransitionTime;
            this.TransitionTimeHourDateTimePicker.Value = DefaultSettings.TransitionTime;

            this.PreviewEnableCheckBox.Checked = DefaultSettings.PreviewEnabled;
            this.PreviewDayRadioButton.Checked = DefaultSettings.PreviewDay;
            this.PreviewNightRadioButton.Checked = DefaultSettings.PreviewNight;

            this.DimWindowCheckBox.Checked = DefaultSettings.dimSettingsForm;
            this.RunOnStartUpCheckBox.Checked = DefaultSettings.RunOnStartup;
            this.SunBasedDimmingCheckBox.Checked = DefaultSettings.SunBasedDimming;
            this.latitude = DefaultSettings.Latitude;
            this.longitude = DefaultSettings.Longitude;
        }

        private void ExportSettingsJson()
        {
            SettingsFormValues settingValues = new SettingsFormValues
            {
                FormLocationX = (WindowState == FormWindowState.Minimized) ? this.RestoreBounds.X : this.Location.X,
                FormLocationY = (WindowState == FormWindowState.Minimized) ? this.RestoreBounds.Y : this.Location.Y,

                EnableDimming = this.DimmingEnableCheckBox.Checked,
                OpacityDay = Int32.Parse(this.OpacityDayValueBox.Text),
                OpacityNight = Int32.Parse(this.OpacityNightValueBox.Text),
                MaxTrueOpacity = DefaultSettings.MaxTrueOpacity,

                EnableTransition = this.NightTransitionEnabledCheckBox.Checked,
                NightStartMinute = this.NightStartMinuteDateTimePicker.Value.Minute,
                NightStartHour = this.NightStartHourDateTimePicker.Value.Hour,
                DayStartMinute = this.DayStartMinuteDateTimePicker.Value.Minute,
                DayStartHour = this.DayStartMinuteDateTimePicker.Value.Hour,
                TransitionTimeMinute = this.TransitionTimeMinuteDateTimePicker.Value.Minute,
                TransitionTimeHour = this.TransitionTimeHourDateTimePicker.Value.Hour,

                PreviewEnabled = this.PreviewEnableCheckBox.Checked,
                PreviewDay = this.PreviewDayRadioButton.Checked,
                PreviewNight = this.PreviewNightRadioButton.Checked,

                dimSettingsForm = this.DimWindowCheckBox.Checked,
                RunOnStartup = this.RunOnStartUpCheckBox.Checked,
                SunBasedDimming = this.SunBasedDimmingCheckBox.Checked,
                SunUpdateHour = this.core.GetSunUpdateTime().Item1,
                SunUpdateMinute = this.core.GetSunUpdateTime().Item2,
                Latitude = this.latitude,
                Longitude = this.longitude,
            };

            string jsonString = JsonSerializer.Serialize(settingValues, DefaultSettings.serializerOptions);
            System.IO.File.WriteAllText(DefaultSettings.settingsFileName, jsonString);
        }

        private bool ImportSettingsJson()
        {
            if (System.IO.File.Exists(DefaultSettings.settingsFileName))
            {
                string jsonContent = System.IO.File.ReadAllText(DefaultSettings.settingsFileName);
                SettingsFormValues settingsValues = JsonSerializer.Deserialize<SettingsFormValues>(jsonContent, DefaultSettings.serializerOptions);

                int FormLocationX = Math.Max(settingsValues.FormLocationX, 0);
                int FormLocationY = Math.Max(settingsValues.FormLocationY, 0);
                this.Location = new Point(FormLocationX, FormLocationY);

                this.DimmingEnableCheckBox.Checked = settingsValues.EnableDimming;
                this.OpacityDayValueBox.Text = settingsValues.OpacityDay.ToString();
                this.OpacityNightValueBox.Text = settingsValues.OpacityNight.ToString();
                this.OpacityDaySlider.Value = settingsValues.OpacityDay;
                this.OpacityNightSlider.Value = settingsValues.OpacityNight;
                this.dimSettingsForm = settingsValues.dimSettingsForm;

                DateTime NightStart = new DateTime(DateTime.Now.Year, 1, 1, settingsValues.NightStartHour, settingsValues.NightStartMinute, 0);
                DateTime DayStart = new DateTime(DateTime.Now.Year, 1, 1, settingsValues.DayStartHour, settingsValues.DayStartMinute, 0);
                DateTime TransitionTime = new DateTime(DateTime.Now.Year, 1, 1, settingsValues.TransitionTimeHour, settingsValues.TransitionTimeMinute, 0);
                this.NightTransitionEnabledCheckBox.Checked = settingsValues.EnableTransition;
                this.NightStartMinuteDateTimePicker.Value = NightStart;
                this.NightStartHourDateTimePicker.Value = NightStart;
                this.DayStartMinuteDateTimePicker.Value = DayStart;
                this.DayStartHourDateTimePicker.Value = DayStart;
                this.TransitionTimeMinuteDateTimePicker.Value = TransitionTime;
                this.TransitionTimeHourDateTimePicker.Value = TransitionTime;

                this.PreviewEnableCheckBox.Checked = settingsValues.PreviewEnabled;
                this.PreviewDayRadioButton.Checked = settingsValues.PreviewDay;
                this.PreviewNightRadioButton.Checked = settingsValues.PreviewNight;

                this.DimWindowCheckBox.Checked = settingsValues.dimSettingsForm;
                this.RunOnStartUpCheckBox.Checked = settingsValues.RunOnStartup;
                this.core.SetSunUpdateTime(settingsValues.SunUpdateHour, settingsValues.SunUpdateMinute, 0);
                this.latitude = settingsValues.Latitude;
                this.longitude = settingsValues.Longitude;
                this.core.UpdateGeoLocation(this.latitude, this.longitude); //MUST be called after setting latitude and longitude
                this.SunBasedDimmingCheckBox.Checked = settingsValues.SunBasedDimming; //MUST be called after UpdateGeoLocation() to display correct data in settingsForm.

                return true;
            }

            return false;
        }
    }
}
