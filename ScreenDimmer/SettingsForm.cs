using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace ScreenDimmer
{
    public partial class SettingsForm : Form
    {
        //UI elements
        private TrackBar OpacityDaySlider;
        private Label NightStartTimeLabel;
        private TrackBar OpacityNightSlider;
        private Label TransitionTimeLabel;
        private ComboBox NightStartHourBox;
        private ComboBox NightStartMinuteBox;
        private ComboBox TransitionTimeMinuteBox;
        private ComboBox TransitionTimeHourBox;
        private Label DimmingDayLabel;
        private Label DimmingNightLabel;
        private GroupBox NightTransitionControlGroup;
        private GroupBox DimmingControlGroup;
        private CheckBox DimmingEnableCheckBox;
        private NotifyIcon NotifyIcon;
        private System.ComponentModel.IContainer components;
        private TextBox OpacityNightValueBox;
        private TextBox OpacityDayValueBox;
        private Label NightEndTimeLabel;
        private ComboBox DayStartHourBox;
        private ComboBox DayStartMinuteBox;
        private GroupBox PreviewGroup;
        private RadioButton PreviewDayRadioButton;
        private CheckBox PreviewEnableCheckedBox;
        private RadioButton PreviewNightRadioButton;
        private CheckBox NightTransitionEnabledCheckedBox;


        ~SettingsForm()
        {
            NotifyIcon = null;
        }
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.OpacityDaySlider = new System.Windows.Forms.TrackBar();
            this.NightStartTimeLabel = new System.Windows.Forms.Label();
            this.OpacityNightSlider = new System.Windows.Forms.TrackBar();
            this.TransitionTimeLabel = new System.Windows.Forms.Label();
            this.NightStartHourBox = new System.Windows.Forms.ComboBox();
            this.NightStartMinuteBox = new System.Windows.Forms.ComboBox();
            this.TransitionTimeMinuteBox = new System.Windows.Forms.ComboBox();
            this.TransitionTimeHourBox = new System.Windows.Forms.ComboBox();
            this.DimmingDayLabel = new System.Windows.Forms.Label();
            this.DimmingNightLabel = new System.Windows.Forms.Label();
            this.NightTransitionControlGroup = new System.Windows.Forms.GroupBox();
            this.NightEndTimeLabel = new System.Windows.Forms.Label();
            this.DayStartHourBox = new System.Windows.Forms.ComboBox();
            this.DayStartMinuteBox = new System.Windows.Forms.ComboBox();
            this.NightTransitionEnabledCheckedBox = new System.Windows.Forms.CheckBox();
            this.DimmingControlGroup = new System.Windows.Forms.GroupBox();
            this.OpacityNightValueBox = new System.Windows.Forms.TextBox();
            this.OpacityDayValueBox = new System.Windows.Forms.TextBox();
            this.DimmingEnableCheckBox = new System.Windows.Forms.CheckBox();
            this.NotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.PreviewGroup = new System.Windows.Forms.GroupBox();
            this.PreviewNightRadioButton = new System.Windows.Forms.RadioButton();
            this.PreviewDayRadioButton = new System.Windows.Forms.RadioButton();
            this.PreviewEnableCheckedBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.OpacityDaySlider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OpacityNightSlider)).BeginInit();
            this.NightTransitionControlGroup.SuspendLayout();
            this.DimmingControlGroup.SuspendLayout();
            this.PreviewGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // DayOpacitySlider
            // 
            resources.ApplyResources(this.OpacityDaySlider, "DayOpacitySlider");
            this.OpacityDaySlider.Maximum = 100;
            this.OpacityDaySlider.Name = "DayOpacitySlider";
            this.OpacityDaySlider.Scroll += new System.EventHandler(this.OpacityDaySlider_Scroll);
            // 
            // NightStartTimeLabel
            // 
            resources.ApplyResources(this.NightStartTimeLabel, "NightStartTimeLabel");
            this.NightStartTimeLabel.Name = "NightStartTimeLabel";
            // 
            // NightOpacitySlider
            // 
            resources.ApplyResources(this.OpacityNightSlider, "NightOpacitySlider");
            this.OpacityNightSlider.Maximum = 100;
            this.OpacityNightSlider.Name = "NightOpacitySlider";
            this.OpacityNightSlider.Scroll += new System.EventHandler(this.OpacityNightSlider_Scroll);
            // 
            // TransitionTimeLabel
            // 
            resources.ApplyResources(this.TransitionTimeLabel, "TransitionTimeLabel");
            this.TransitionTimeLabel.Name = "TransitionTimeLabel";
            // 
            // StartTimeHoursBox
            // 
            this.NightStartHourBox.FormattingEnabled = true;
            this.NightStartHourBox.Items.AddRange(new object[] {
            resources.GetString("StartTimeHoursBox.Items"),
            resources.GetString("StartTimeHoursBox.Items1"),
            resources.GetString("StartTimeHoursBox.Items2"),
            resources.GetString("StartTimeHoursBox.Items3"),
            resources.GetString("StartTimeHoursBox.Items4"),
            resources.GetString("StartTimeHoursBox.Items5"),
            resources.GetString("StartTimeHoursBox.Items6"),
            resources.GetString("StartTimeHoursBox.Items7"),
            resources.GetString("StartTimeHoursBox.Items8"),
            resources.GetString("StartTimeHoursBox.Items9"),
            resources.GetString("StartTimeHoursBox.Items10"),
            resources.GetString("StartTimeHoursBox.Items11"),
            resources.GetString("StartTimeHoursBox.Items12"),
            resources.GetString("StartTimeHoursBox.Items13"),
            resources.GetString("StartTimeHoursBox.Items14"),
            resources.GetString("StartTimeHoursBox.Items15"),
            resources.GetString("StartTimeHoursBox.Items16"),
            resources.GetString("StartTimeHoursBox.Items17"),
            resources.GetString("StartTimeHoursBox.Items18"),
            resources.GetString("StartTimeHoursBox.Items19"),
            resources.GetString("StartTimeHoursBox.Items20"),
            resources.GetString("StartTimeHoursBox.Items21"),
            resources.GetString("StartTimeHoursBox.Items22"),
            resources.GetString("StartTimeHoursBox.Items23")});
            resources.ApplyResources(this.NightStartHourBox, "StartTimeHoursBox");
            this.NightStartHourBox.Name = "StartTimeHoursBox";
            this.NightStartHourBox.SelectedIndexChanged += new System.EventHandler(this.NightStartHourBox_SelectedIndexChanged);
            // 
            // StartTimeMinutesBox
            // 
            this.NightStartMinuteBox.FormattingEnabled = true;
            this.NightStartMinuteBox.Items.AddRange(new object[] {
            resources.GetString("StartTimeMinutesBox.Items"),
            resources.GetString("StartTimeMinutesBox.Items1"),
            resources.GetString("StartTimeMinutesBox.Items2"),
            resources.GetString("StartTimeMinutesBox.Items3")});
            resources.ApplyResources(this.NightStartMinuteBox, "StartTimeMinutesBox");
            this.NightStartMinuteBox.Name = "StartTimeMinutesBox";
            this.NightStartMinuteBox.SelectedIndexChanged += new System.EventHandler(this.NightStartMinuteBox_SelectedIndexChanged);
            // 
            // TransitionTimeMinutesBox
            // 
            this.TransitionTimeMinuteBox.FormattingEnabled = true;
            this.TransitionTimeMinuteBox.Items.AddRange(new object[] {
            resources.GetString("TransitionTimeMinutesBox.Items"),
            resources.GetString("TransitionTimeMinutesBox.Items1"),
            resources.GetString("TransitionTimeMinutesBox.Items2"),
            resources.GetString("TransitionTimeMinutesBox.Items3")});
            resources.ApplyResources(this.TransitionTimeMinuteBox, "TransitionTimeMinutesBox");
            this.TransitionTimeMinuteBox.Name = "TransitionTimeMinutesBox";
            this.TransitionTimeMinuteBox.SelectedIndexChanged += new System.EventHandler(this.TransitionTimeMinuteBox_SelectedIndexChanged);
            // 
            // TransitionTimeHoursBox
            // 
            this.TransitionTimeHourBox.FormattingEnabled = true;
            this.TransitionTimeHourBox.Items.AddRange(new object[] {
            resources.GetString("TransitionTimeHoursBox.Items"),
            resources.GetString("TransitionTimeHoursBox.Items1"),
            resources.GetString("TransitionTimeHoursBox.Items2"),
            resources.GetString("TransitionTimeHoursBox.Items3"),
            resources.GetString("TransitionTimeHoursBox.Items4"),
            resources.GetString("TransitionTimeHoursBox.Items5"),
            resources.GetString("TransitionTimeHoursBox.Items6")});
            resources.ApplyResources(this.TransitionTimeHourBox, "TransitionTimeHoursBox");
            this.TransitionTimeHourBox.Name = "TransitionTimeHoursBox";
            this.TransitionTimeHourBox.SelectedIndexChanged += new System.EventHandler(this.TransitionTimeHourBox_SelectedIndexChanged);
            // 
            // DimmingDayLabel
            // 
            resources.ApplyResources(this.DimmingDayLabel, "DimmingDayLabel");
            this.DimmingDayLabel.Name = "DimmingDayLabel";
            // 
            // DimmingNightLabel
            // 
            resources.ApplyResources(this.DimmingNightLabel, "DimmingNightLabel");
            this.DimmingNightLabel.Name = "DimmingNightLabel";
            // 
            // NightTransitionControlGroup
            // 
            this.NightTransitionControlGroup.Controls.Add(this.NightEndTimeLabel);
            this.NightTransitionControlGroup.Controls.Add(this.DayStartHourBox);
            this.NightTransitionControlGroup.Controls.Add(this.DayStartMinuteBox);
            this.NightTransitionControlGroup.Controls.Add(this.NightTransitionEnabledCheckedBox);
            this.NightTransitionControlGroup.Controls.Add(this.TransitionTimeHourBox);
            this.NightTransitionControlGroup.Controls.Add(this.NightStartTimeLabel);
            this.NightTransitionControlGroup.Controls.Add(this.TransitionTimeLabel);
            this.NightTransitionControlGroup.Controls.Add(this.NightStartHourBox);
            this.NightTransitionControlGroup.Controls.Add(this.NightStartMinuteBox);
            this.NightTransitionControlGroup.Controls.Add(this.TransitionTimeMinuteBox);
            this.NightTransitionControlGroup.ForeColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.NightTransitionControlGroup, "NightTransitionControlGroup");
            this.NightTransitionControlGroup.Name = "NightTransitionControlGroup";
            this.NightTransitionControlGroup.TabStop = false;
            // 
            // NightEndTimeLabel
            // 
            resources.ApplyResources(this.NightEndTimeLabel, "NightEndTimeLabel");
            this.NightEndTimeLabel.Name = "NightEndTimeLabel";
            // 
            // EndTimeHoursBox
            // 
            this.DayStartHourBox.FormattingEnabled = true;
            this.DayStartHourBox.Items.AddRange(new object[] {
            resources.GetString("EndTimeHoursBox.Items"),
            resources.GetString("EndTimeHoursBox.Items1"),
            resources.GetString("EndTimeHoursBox.Items2"),
            resources.GetString("EndTimeHoursBox.Items3"),
            resources.GetString("EndTimeHoursBox.Items4"),
            resources.GetString("EndTimeHoursBox.Items5"),
            resources.GetString("EndTimeHoursBox.Items6"),
            resources.GetString("EndTimeHoursBox.Items7"),
            resources.GetString("EndTimeHoursBox.Items8"),
            resources.GetString("EndTimeHoursBox.Items9"),
            resources.GetString("EndTimeHoursBox.Items10"),
            resources.GetString("EndTimeHoursBox.Items11"),
            resources.GetString("EndTimeHoursBox.Items12"),
            resources.GetString("EndTimeHoursBox.Items13"),
            resources.GetString("EndTimeHoursBox.Items14"),
            resources.GetString("EndTimeHoursBox.Items15"),
            resources.GetString("EndTimeHoursBox.Items16"),
            resources.GetString("EndTimeHoursBox.Items17"),
            resources.GetString("EndTimeHoursBox.Items18"),
            resources.GetString("EndTimeHoursBox.Items19"),
            resources.GetString("EndTimeHoursBox.Items20"),
            resources.GetString("EndTimeHoursBox.Items21"),
            resources.GetString("EndTimeHoursBox.Items22"),
            resources.GetString("EndTimeHoursBox.Items23")});
            resources.ApplyResources(this.DayStartHourBox, "EndTimeHoursBox");
            this.DayStartHourBox.Name = "EndTimeHoursBox";
            this.DayStartHourBox.SelectedIndexChanged += new System.EventHandler(this.DayStartHourBox_SelectedIndexChanged);
            // 
            // EndTimeMinutesBox
            // 
            this.DayStartMinuteBox.FormattingEnabled = true;
            this.DayStartMinuteBox.Items.AddRange(new object[] {
            resources.GetString("EndTimeMinutesBox.Items"),
            resources.GetString("EndTimeMinutesBox.Items1"),
            resources.GetString("EndTimeMinutesBox.Items2"),
            resources.GetString("EndTimeMinutesBox.Items3")});
            resources.ApplyResources(this.DayStartMinuteBox, "EndTimeMinutesBox");
            this.DayStartMinuteBox.Name = "EndTimeMinutesBox";
            this.DayStartMinuteBox.SelectedIndexChanged += new System.EventHandler(this.DayStartMinuteBox_SelectedIndexChanged);
            // 
            // NightTransitionEnableCheckBox
            // 
            resources.ApplyResources(this.NightTransitionEnabledCheckedBox, "NightTransitionEnableCheckBox");
            this.NightTransitionEnabledCheckedBox.Name = "NightTransitionEnableCheckBox";
            this.NightTransitionEnabledCheckedBox.UseVisualStyleBackColor = true;
            this.NightTransitionEnabledCheckedBox.CheckedChanged += new System.EventHandler(this.NightTransitionEnabledCheckedBox_CheckedChanged);
            // 
            // DimmingControlGroup
            // 
            this.DimmingControlGroup.Controls.Add(this.OpacityNightValueBox);
            this.DimmingControlGroup.Controls.Add(this.OpacityDayValueBox);
            this.DimmingControlGroup.Controls.Add(this.OpacityNightSlider);
            this.DimmingControlGroup.Controls.Add(this.DimmingNightLabel);
            this.DimmingControlGroup.Controls.Add(this.DimmingEnableCheckBox);
            this.DimmingControlGroup.Controls.Add(this.DimmingDayLabel);
            this.DimmingControlGroup.Controls.Add(this.OpacityDaySlider);
            this.DimmingControlGroup.ForeColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.DimmingControlGroup, "DimmingControlGroup");
            this.DimmingControlGroup.Name = "DimmingControlGroup";
            this.DimmingControlGroup.TabStop = false;
            // 
            // NightOpacityValueBox
            // 
            resources.ApplyResources(this.OpacityNightValueBox, "NightOpacityValueBox");
            this.OpacityNightValueBox.Name = "NightOpacityValueBox";
            this.OpacityNightValueBox.TextChanged += new System.EventHandler(this.OpacityNightValueBox_TextChanged);
            // 
            // DayOpacityValueBox
            // 
            resources.ApplyResources(this.OpacityDayValueBox, "DayOpacityValueBox");
            this.OpacityDayValueBox.Name = "DayOpacityValueBox";
            this.OpacityDayValueBox.TextChanged += new System.EventHandler(this.OpacityDayValueBox_TextChanged);
            // 
            // DimmingEnableCheckBox
            // 
            resources.ApplyResources(this.DimmingEnableCheckBox, "DimmingEnableCheckBox");
            this.DimmingEnableCheckBox.Checked = true;
            this.DimmingEnableCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DimmingEnableCheckBox.Name = "DimmingEnableCheckBox";
            this.DimmingEnableCheckBox.UseVisualStyleBackColor = true;
            this.DimmingEnableCheckBox.CheckedChanged += new System.EventHandler(this.DimmingEnableCheckBox_CheckedChanged);
            // 
            // NotifyIcon
            // 
            resources.ApplyResources(this.NotifyIcon, "NotifyIcon");
            this.NotifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIcon_MouseClick);
            // 
            // PreviewGroup
            // 
            this.PreviewGroup.Controls.Add(this.PreviewNightRadioButton);
            this.PreviewGroup.Controls.Add(this.PreviewDayRadioButton);
            this.PreviewGroup.Controls.Add(this.PreviewEnableCheckedBox);
            this.PreviewGroup.ForeColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.PreviewGroup, "PreviewGroup");
            this.PreviewGroup.Name = "PreviewGroup";
            this.PreviewGroup.TabStop = false;
            // 
            // PreviewNightRadioButton
            // 
            resources.ApplyResources(this.PreviewNightRadioButton, "PreviewNightRadioButton");
            this.PreviewNightRadioButton.Name = "PreviewNightRadioButton";
            this.PreviewNightRadioButton.TabStop = true;
            this.PreviewNightRadioButton.UseVisualStyleBackColor = true;
            this.PreviewNightRadioButton.CheckedChanged += new System.EventHandler(this.PreviewRadioButton_CheckedChanged);
            // 
            // PreviewDayRadioButton
            // 
            resources.ApplyResources(this.PreviewDayRadioButton, "PreviewDayRadioButton");
            this.PreviewDayRadioButton.Name = "PreviewDayRadioButton";
            this.PreviewDayRadioButton.TabStop = true;
            this.PreviewDayRadioButton.UseVisualStyleBackColor = true;
            this.PreviewDayRadioButton.CheckedChanged += new System.EventHandler(this.PreviewRadioButton_CheckedChanged);
            // 
            // PreviewEnableCheckedBox
            // 
            resources.ApplyResources(this.PreviewEnableCheckedBox, "PreviewEnableCheckedBox");
            this.PreviewEnableCheckedBox.Name = "PreviewEnableCheckedBox";
            this.PreviewEnableCheckedBox.UseVisualStyleBackColor = true;
            this.PreviewEnableCheckedBox.CheckedChanged += new System.EventHandler(this.PreviewEnableCheckedBox_CheckedChanged);
            // 
            // SettingsFormUI
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.Controls.Add(this.PreviewGroup);
            this.Controls.Add(this.DimmingControlGroup);
            this.Controls.Add(this.NightTransitionControlGroup);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "SettingsFormUI";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingsForm_Closing);
            this.Resize += new System.EventHandler(this.SettignsFormResize);
            ((System.ComponentModel.ISupportInitialize)(this.OpacityDaySlider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OpacityNightSlider)).EndInit();
            this.NightTransitionControlGroup.ResumeLayout(false);
            this.NightTransitionControlGroup.PerformLayout();
            this.DimmingControlGroup.ResumeLayout(false);
            this.DimmingControlGroup.PerformLayout();
            this.PreviewGroup.ResumeLayout(false);
            this.PreviewGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #region General
        //________________ General ________________
        private void SettingsForm_Closing(object sender, EventArgs e)
        {
            NotifyIcon.Visible = false;
            NotifyIcon = null;
        }

        private void SettignsFormResize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)  // only hide if minimizing the form
            {
                this.ShowInTaskbar = false;
                this.Visible = false;
            }
        }

        private void NotifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            this.ShowInTaskbar = true;
            this.Visible = true;
            WindowState = FormWindowState.Normal;
            if (CanFocus)
            {
                Focus();
            }
        }
        #endregion

        #region Dimming
        //________________ Dimming ________________
        private void DimmingEnableCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (DimmingEnableCheckBox.Checked)
            {
                EnableDimming();
            }
            else
            {
                DisableDimming();
            }

            UpdateOverlayForms();
        }

        private void OpacityDaySlider_Scroll(object sender, EventArgs e)
        {
            opacityDay = OpacityDaySlider.Value;
            OpacityDayValueBox.Text = opacityDay.ToString();
            UpdateOverlayForms();
        }

        private void OpacityNightSlider_Scroll(object sender, EventArgs e)
        {
            opacityNight = OpacityNightSlider.Value;
            OpacityNightValueBox.Text = opacityNight.ToString();
            UpdateOverlayOpacityCurrent();
            UpdateOverlayForms();
        }

        private void OpacityDayValueBox_TextChanged(object sender, EventArgs e)
        {
            int value;
            if (Int32.TryParse(OpacityDayValueBox.Text, out value))
            {
                if(value < 0 || value > 100)
                {
                    OpacityDayValueBox.Text = opacityDay.ToString();
                    return;
                }

                opacityDay = value;
                OpacityDaySlider.Value = opacityDay;
                UpdateOverlayForms();
            }
            else
            {
                if (OpacityDayValueBox.Text != "")
                    OpacityDayValueBox.Text = opacityDay.ToString();
            }
        }

        private void OpacityNightValueBox_TextChanged(object sender, EventArgs e)
        {
            int value;
            if (Int32.TryParse(OpacityNightValueBox.Text, out value))
            {
                if (value < 0 || value > 100)
                {
                    OpacityDayValueBox.Text = opacityDay.ToString();
                    return;
                }

                opacityNight = value;
                OpacityNightSlider.Value = opacityNight;
                UpdateOverlayForms();
            }
            else
            {
                if (OpacityDayValueBox.Text != "")
                    OpacityNightValueBox.Text = opacityNight.ToString();
            }
        }
        #endregion

        #region Night Transition
        //________________ Night Transition ________________
        private void NightTransitionEnabledCheckedBox_CheckedChanged(object sender, EventArgs e)
        {
            nightTransitionEnabled = NightTransitionEnabledCheckedBox.Checked;
            UpdateOverlayForms();
        }

        private void NightStartHourBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            nightStartHour = BoxIndexToHour(NightStartHourBox.SelectedIndex);
            UpdateOverlayForms();
        }

        private void NightStartMinuteBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            nightStartMinute = BoxIndexToMinute(NightStartMinuteBox.SelectedIndex);
            UpdateOverlayForms();
        }

        private void DayStartHourBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            dayStartHour = BoxIndexToHour(DayStartHourBox.SelectedIndex);
            UpdateOverlayForms();
        }

        private void DayStartMinuteBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            dayStartMinute = BoxIndexToMinute(DayStartMinuteBox.SelectedIndex);
            UpdateOverlayForms();
        }

        private void TransitionTimeHourBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            transitionTimeHour = BoxIndexToHour(TransitionTimeHourBox.SelectedIndex);
            UpdateOverlayForms();
        }

        private void TransitionTimeMinuteBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            transitionTimeMinute = BoxIndexToMinute(TransitionTimeMinuteBox.SelectedIndex);
            UpdateOverlayForms();
        }
        #endregion

        #region Dimming Preview
        //________________ Dimming Preview ________________
        private void PreviewEnableCheckedBox_CheckedChanged(object sender, EventArgs e)
        {
            previewEnabled = PreviewEnableCheckedBox.Checked;
            UpdateOverlayForms();
        }

        private void PreviewRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            this.previewSelection = PreviewDayRadioButton.Checked ? PreviewSelection.Day : PreviewSelection.Night;
            UpdateOverlayForms();
        }
        #endregion
    }


}
