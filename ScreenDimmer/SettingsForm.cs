﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Win32;

namespace ScreenDimmer
{
    public partial class SettingsForm : Form
    {
        //reference to other forms
        FormManager parentForm;
        CoreLogic core;
        Action<object, EventArgs> overlayUpdateTick;

        private bool dimSettingsForm;

        //UI elements
        private TrackBar OpacityDaySlider;
        private Label NightStartTimeLabel;
        private TrackBar OpacityNightSlider;
        private Label TransitionTimeLabel;
        private Label DimmingDayLabel;
        private Label DimmingNightLabel;
        private GroupBox DayNightCycleGroupBox;
        private GroupBox DimmingGroupBox;
        private CheckBox DimmingEnableCheckBox;
        private NotifyIcon NotifyIcon;
        private System.ComponentModel.IContainer components;
        private TextBox OpacityNightValueBox;
        private TextBox OpacityDayValueBox;
        private Label NightEndTimeLabel;
        private GroupBox PreviewGroupBox;
        private RadioButton PreviewDayRadioButton;
        private CheckBox PreviewEnableCheckBox;
        private RadioButton PreviewNightRadioButton;
        private DateTimePicker NightStartHourDateTimePicker;
        private DateTimePicker DayStartMinuteDateTimePicker;
        private DateTimePicker NightStartMinuteDateTimePicker;
        private DateTimePicker DayStartHourDateTimePicker;
        private DateTimePicker TransitionTimeMinuteDateTimePicker;
        private DateTimePicker TransitionTimeHourDateTimePicker;
        private GroupBox OptionsGroupBox;
        private CheckBox DimWindowCheckBox;
        private CheckBox RunOnStartUpCheckBox;
        private CheckBox NightTransitionEnabledCheckBox;

        public SettingsForm(CoreLogic core, FormManager parentForm, Action<object, EventArgs> overlayUpdateTick)
        {
            this.parentForm = parentForm;
            this.core = core;
            this.overlayUpdateTick = overlayUpdateTick;
            InitializeComponent();
            SetDefaultValues();
            this.TopMost = !dimSettingsForm;
        }

        ~SettingsForm()
        {
            NotifyIcon.Visible = false;
            NotifyIcon = null;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.DoubleBuffered = true;
            this.ShowInTaskbar = true;
        }


        #region General
        //________________ General ________________
        private void SettingsForm_Closing(object sender, EventArgs e)
        {
            NotifyIcon.Visible = false;
            NotifyIcon = null;
            parentForm.Close();
        }

        private void SettignsFormResize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)  // only hide if minimizing the form
            {
                this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                this.ShowInTaskbar = false;
                this.Visible = false;
            }
        }

        private void NotifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.ShowInTaskbar = true;
            this.Visible = true;
            WindowState = FormWindowState.Normal;

            this.TopMost = true;
            this.TopMost = !dimSettingsForm;

            if (CanFocus)
            {
                Focus();
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if(keyData == Keys.Enter)
            {
                DayNightCycleGroupBox.Focus();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        public static int BoxIndexToHour(int boxIndex)
        {
            return boxIndex;
        }

        public static int BoxIndexToMinute(int boxIndex)
        {
            return boxIndex * 5;
        }
        #endregion

        #region Dimming
        //________________ Dimming ________________
        private void DimmingEnableCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (DimmingEnableCheckBox.Checked)
            {
                core.EnableDimming();
            }
            else
            {
                core.DisableDimming();
            }

            core.Update();
            overlayUpdateTick(sender, e);
        }

        private void OpacityDaySlider_Scroll(object sender, EventArgs e)
        {
            core.opacityDay = OpacityDaySlider.Value;
            OpacityDayValueBox.Text = core.opacityDay.ToString();
            core.Update();
            overlayUpdateTick(sender, e);
        }

        private void OpacityNightSlider_Scroll(object sender, EventArgs e)
        {
            core.opacityNight = OpacityNightSlider.Value;
            OpacityNightValueBox.Text = core.opacityNight.ToString();
            core.Update();
            overlayUpdateTick(sender, e);
        }

        private void OpacityDayValueBox_TextChanged(object sender, EventArgs e)
        {
            int value;
            if (Int32.TryParse(OpacityDayValueBox.Text, out value))
            {
                if(value < 0 || value > 100)
                {
                    OpacityDayValueBox.Text = core.opacityDay.ToString();
                    return;
                }

                core.opacityDay = value;
                OpacityDaySlider.Value = core.opacityDay;
                core.Update();
                overlayUpdateTick(sender, e);
            }
            else
            {
                if (OpacityDayValueBox.Text != "")
                    OpacityDayValueBox.Text = core.opacityDay.ToString();
            }
        }

        private void OpacityNightValueBox_TextChanged(object sender, EventArgs e)
        {
            int value;
            if (Int32.TryParse(OpacityNightValueBox.Text, out value))
            {
                if (value < 0 || value > 100)
                {
                    OpacityDayValueBox.Text = core.opacityDay.ToString();
                    return;
                }

                core.opacityNight = value;
                OpacityNightSlider.Value = core.opacityNight;
                core.Update();
                overlayUpdateTick(sender, e);
            }
            else
            {
                if (OpacityDayValueBox.Text != "")
                    OpacityNightValueBox.Text = core.opacityNight.ToString();
            }
        }
        #endregion

        #region Night Transition
        //________________ Night Transition ________________
        private void NightTransitionEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            core.nightTransitionEnabled = NightTransitionEnabledCheckBox.Checked;
            core.Update();
            overlayUpdateTick(sender, e);
        }

        private void NightStartHourDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            core.nightStartHour = NightStartHourDateTimePicker.Value.Hour;
            core.Update();
            overlayUpdateTick(sender, e);
        }

        private void NightStartMinuteDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            core.nightStartMinute = NightStartMinuteDateTimePicker.Value.Minute;
            core.Update();
            overlayUpdateTick(sender, e);
        }

        private void DayStartHourDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            core.dayStartHour = DayStartHourDateTimePicker.Value.Hour;
            core.Update();
            overlayUpdateTick(sender, e);
        }

        private void DayStartMinuteDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            core.dayStartMinute = DayStartMinuteDateTimePicker.Value.Minute;
            core.Update();
            overlayUpdateTick(sender, e);
        }

        private void TransitionTimeHourDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            TimeSpan newTransitionTimeSpan = new TimeSpan(TransitionTimeHourDateTimePicker.Value.Hour, 
                                                          TransitionTimeMinuteDateTimePicker.Value.Minute,
                                                          0);

            if (newTransitionTimeSpan > core.maxTransitionTimeSpan)
            {
                TransitionTimeHourDateTimePicker.Value = new DateTime(DateTime.Now.Year, 1, 1, core.maxTransitionTimeSpan.Hours, 0, 0);
                TransitionTimeMinuteDateTimePicker.Value = new DateTime(DateTime.Now.Year, 1, 1, 0, core.maxTransitionTimeSpan.Minutes, 0);
            }

            core.transitionTimeHour = TransitionTimeHourDateTimePicker.Value.Hour;
            core.Update();
            overlayUpdateTick(sender, e);
        }

        private void TransitionTimeMinuteDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            TimeSpan newTransitionTimeSpan = new TimeSpan(TransitionTimeHourDateTimePicker.Value.Hour,
                                                          TransitionTimeMinuteDateTimePicker.Value.Minute,
                                                          0);

            if (newTransitionTimeSpan > core.maxTransitionTimeSpan)
            {
                TransitionTimeHourDateTimePicker.Value = new DateTime(DateTime.Now.Year, 1, 1, core.maxTransitionTimeSpan.Hours, 0, 0);
                TransitionTimeMinuteDateTimePicker.Value = new DateTime(DateTime.Now.Year, 1, 1, 0, core.maxTransitionTimeSpan.Minutes, 0);
            }

            core.transitionTimeMinute = TransitionTimeMinuteDateTimePicker.Value.Minute;
            core.Update();
            overlayUpdateTick(sender, e);
        }
        #endregion

        #region Dimming Preview
        //________________ Dimming Preview ________________
        private void PreviewEnableCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            core.previewEnabled = PreviewEnableCheckBox.Checked;
            core.Update();
            overlayUpdateTick(sender, e);
        }

        private void PreviewRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            core.previewSelection = PreviewDayRadioButton.Checked ? PreviewSelection.Day : PreviewSelection.Night;
            core.Update();
            overlayUpdateTick(sender, e);
        }
        #endregion

        #region Options
        //________________ Options ________________
        private void DimWindowCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            dimSettingsForm = DimWindowCheckBox.Checked;
            this.TopMost = !dimSettingsForm;
        }

        private void RunOnStartupCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (RunOnStartUpCheckBox.Checked)
            {
                registryKey.SetValue("ScreenDimmer", Application.ExecutablePath);
            }
            else
            {
                registryKey.DeleteValue("ScreenDimmer", false);
            }
        }
        #endregion

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.OpacityDaySlider = new System.Windows.Forms.TrackBar();
            this.NightStartTimeLabel = new System.Windows.Forms.Label();
            this.OpacityNightSlider = new System.Windows.Forms.TrackBar();
            this.TransitionTimeLabel = new System.Windows.Forms.Label();
            this.DimmingDayLabel = new System.Windows.Forms.Label();
            this.DimmingNightLabel = new System.Windows.Forms.Label();
            this.DayNightCycleGroupBox = new System.Windows.Forms.GroupBox();
            this.TransitionTimeMinuteDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.TransitionTimeHourDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.DayStartMinuteDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.NightStartMinuteDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.NightStartHourDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.NightTransitionEnabledCheckBox = new System.Windows.Forms.CheckBox();
            this.DayStartHourDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.NightEndTimeLabel = new System.Windows.Forms.Label();
            this.DimmingGroupBox = new System.Windows.Forms.GroupBox();
            this.OpacityNightValueBox = new System.Windows.Forms.TextBox();
            this.OpacityDayValueBox = new System.Windows.Forms.TextBox();
            this.DimmingEnableCheckBox = new System.Windows.Forms.CheckBox();
            this.NotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.PreviewGroupBox = new System.Windows.Forms.GroupBox();
            this.PreviewNightRadioButton = new System.Windows.Forms.RadioButton();
            this.PreviewDayRadioButton = new System.Windows.Forms.RadioButton();
            this.PreviewEnableCheckBox = new System.Windows.Forms.CheckBox();
            this.OptionsGroupBox = new System.Windows.Forms.GroupBox();
            this.DimWindowCheckBox = new System.Windows.Forms.CheckBox();
            this.RunOnStartUpCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.OpacityDaySlider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OpacityNightSlider)).BeginInit();
            this.DayNightCycleGroupBox.SuspendLayout();
            this.DimmingGroupBox.SuspendLayout();
            this.PreviewGroupBox.SuspendLayout();
            this.OptionsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // OpacityDaySlider
            // 
            resources.ApplyResources(this.OpacityDaySlider, "OpacityDaySlider");
            this.OpacityDaySlider.Maximum = 100;
            this.OpacityDaySlider.Name = "OpacityDaySlider";
            this.OpacityDaySlider.Scroll += new System.EventHandler(this.OpacityDaySlider_Scroll);
            // 
            // NightStartTimeLabel
            // 
            resources.ApplyResources(this.NightStartTimeLabel, "NightStartTimeLabel");
            this.NightStartTimeLabel.Name = "NightStartTimeLabel";
            // 
            // OpacityNightSlider
            // 
            resources.ApplyResources(this.OpacityNightSlider, "OpacityNightSlider");
            this.OpacityNightSlider.Maximum = 100;
            this.OpacityNightSlider.Name = "OpacityNightSlider";
            this.OpacityNightSlider.Scroll += new System.EventHandler(this.OpacityNightSlider_Scroll);
            // 
            // TransitionTimeLabel
            // 
            resources.ApplyResources(this.TransitionTimeLabel, "TransitionTimeLabel");
            this.TransitionTimeLabel.Name = "TransitionTimeLabel";
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
            // DayNightCycleGroupBox
            // 
            this.DayNightCycleGroupBox.Controls.Add(this.TransitionTimeMinuteDateTimePicker);
            this.DayNightCycleGroupBox.Controls.Add(this.TransitionTimeHourDateTimePicker);
            this.DayNightCycleGroupBox.Controls.Add(this.DayStartMinuteDateTimePicker);
            this.DayNightCycleGroupBox.Controls.Add(this.NightStartMinuteDateTimePicker);
            this.DayNightCycleGroupBox.Controls.Add(this.NightStartHourDateTimePicker);
            this.DayNightCycleGroupBox.Controls.Add(this.NightTransitionEnabledCheckBox);
            this.DayNightCycleGroupBox.Controls.Add(this.DayStartHourDateTimePicker);
            this.DayNightCycleGroupBox.Controls.Add(this.NightStartTimeLabel);
            this.DayNightCycleGroupBox.Controls.Add(this.TransitionTimeLabel);
            this.DayNightCycleGroupBox.Controls.Add(this.NightEndTimeLabel);
            this.DayNightCycleGroupBox.ForeColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.DayNightCycleGroupBox, "DayNightCycleGroupBox");
            this.DayNightCycleGroupBox.Name = "DayNightCycleGroupBox";
            this.DayNightCycleGroupBox.TabStop = false;
            // 
            // TransitionTimeMinuteDateTimePicker
            // 
            resources.ApplyResources(this.TransitionTimeMinuteDateTimePicker, "TransitionTimeMinuteDateTimePicker");
            this.TransitionTimeMinuteDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.TransitionTimeMinuteDateTimePicker.Name = "TransitionTimeMinuteDateTimePicker";
            this.TransitionTimeMinuteDateTimePicker.ShowUpDown = true;
            this.TransitionTimeMinuteDateTimePicker.ValueChanged += new System.EventHandler(this.TransitionTimeMinuteDateTimePicker_ValueChanged);
            // 
            // TransitionTimeHourDateTimePicker
            // 
            resources.ApplyResources(this.TransitionTimeHourDateTimePicker, "TransitionTimeHourDateTimePicker");
            this.TransitionTimeHourDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.TransitionTimeHourDateTimePicker.Name = "TransitionTimeHourDateTimePicker";
            this.TransitionTimeHourDateTimePicker.ShowUpDown = true;
            this.TransitionTimeHourDateTimePicker.ValueChanged += new System.EventHandler(this.TransitionTimeHourDateTimePicker_ValueChanged);
            // 
            // DayStartMinuteDateTimePicker
            // 
            resources.ApplyResources(this.DayStartMinuteDateTimePicker, "DayStartMinuteDateTimePicker");
            this.DayStartMinuteDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DayStartMinuteDateTimePicker.Name = "DayStartMinuteDateTimePicker";
            this.DayStartMinuteDateTimePicker.ShowUpDown = true;
            this.DayStartMinuteDateTimePicker.ValueChanged += new System.EventHandler(this.DayStartMinuteDateTimePicker_ValueChanged);
            // 
            // NightStartMinuteDateTimePicker
            // 
            resources.ApplyResources(this.NightStartMinuteDateTimePicker, "NightStartMinuteDateTimePicker");
            this.NightStartMinuteDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.NightStartMinuteDateTimePicker.Name = "NightStartMinuteDateTimePicker";
            this.NightStartMinuteDateTimePicker.ShowUpDown = true;
            this.NightStartMinuteDateTimePicker.ValueChanged += new System.EventHandler(this.NightStartMinuteDateTimePicker_ValueChanged);
            // 
            // NightStartHourDateTimePicker
            // 
            resources.ApplyResources(this.NightStartHourDateTimePicker, "NightStartHourDateTimePicker");
            this.NightStartHourDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.NightStartHourDateTimePicker.Name = "NightStartHourDateTimePicker";
            this.NightStartHourDateTimePicker.ShowUpDown = true;
            this.NightStartHourDateTimePicker.ValueChanged += new System.EventHandler(this.NightStartHourDateTimePicker_ValueChanged);
            // 
            // NightTransitionEnabledCheckBox
            // 
            resources.ApplyResources(this.NightTransitionEnabledCheckBox, "NightTransitionEnabledCheckBox");
            this.NightTransitionEnabledCheckBox.Name = "NightTransitionEnabledCheckBox";
            this.NightTransitionEnabledCheckBox.UseVisualStyleBackColor = true;
            this.NightTransitionEnabledCheckBox.CheckedChanged += new System.EventHandler(this.NightTransitionEnabledCheckBox_CheckedChanged);
            // 
            // DayStartHourDateTimePicker
            // 
            resources.ApplyResources(this.DayStartHourDateTimePicker, "DayStartHourDateTimePicker");
            this.DayStartHourDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DayStartHourDateTimePicker.Name = "DayStartHourDateTimePicker";
            this.DayStartHourDateTimePicker.ShowUpDown = true;
            this.DayStartHourDateTimePicker.ValueChanged += new System.EventHandler(this.DayStartHourDateTimePicker_ValueChanged);
            // 
            // NightEndTimeLabel
            // 
            resources.ApplyResources(this.NightEndTimeLabel, "NightEndTimeLabel");
            this.NightEndTimeLabel.Name = "NightEndTimeLabel";
            // 
            // DimmingGroupBox
            // 
            this.DimmingGroupBox.Controls.Add(this.OpacityNightValueBox);
            this.DimmingGroupBox.Controls.Add(this.OpacityDayValueBox);
            this.DimmingGroupBox.Controls.Add(this.OpacityNightSlider);
            this.DimmingGroupBox.Controls.Add(this.DimmingNightLabel);
            this.DimmingGroupBox.Controls.Add(this.DimmingEnableCheckBox);
            this.DimmingGroupBox.Controls.Add(this.DimmingDayLabel);
            this.DimmingGroupBox.Controls.Add(this.OpacityDaySlider);
            this.DimmingGroupBox.ForeColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.DimmingGroupBox, "DimmingGroupBox");
            this.DimmingGroupBox.Name = "DimmingGroupBox";
            this.DimmingGroupBox.TabStop = false;
            // 
            // OpacityNightValueBox
            // 
            resources.ApplyResources(this.OpacityNightValueBox, "OpacityNightValueBox");
            this.OpacityNightValueBox.Name = "OpacityNightValueBox";
            this.OpacityNightValueBox.TextChanged += new System.EventHandler(this.OpacityNightValueBox_TextChanged);
            // 
            // OpacityDayValueBox
            // 
            resources.ApplyResources(this.OpacityDayValueBox, "OpacityDayValueBox");
            this.OpacityDayValueBox.Name = "OpacityDayValueBox";
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
            // PreviewGroupBox
            // 
            this.PreviewGroupBox.Controls.Add(this.PreviewNightRadioButton);
            this.PreviewGroupBox.Controls.Add(this.PreviewDayRadioButton);
            this.PreviewGroupBox.Controls.Add(this.PreviewEnableCheckBox);
            this.PreviewGroupBox.ForeColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.PreviewGroupBox, "PreviewGroupBox");
            this.PreviewGroupBox.Name = "PreviewGroupBox";
            this.PreviewGroupBox.TabStop = false;
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
            // PreviewEnableCheckBox
            // 
            resources.ApplyResources(this.PreviewEnableCheckBox, "PreviewEnableCheckBox");
            this.PreviewEnableCheckBox.Name = "PreviewEnableCheckBox";
            this.PreviewEnableCheckBox.UseVisualStyleBackColor = true;
            this.PreviewEnableCheckBox.CheckedChanged += new System.EventHandler(this.PreviewEnableCheckBox_CheckedChanged);
            // 
            // OptionsGroupBox
            // 
            this.OptionsGroupBox.Controls.Add(this.DimWindowCheckBox);
            this.OptionsGroupBox.Controls.Add(this.RunOnStartUpCheckBox);
            this.OptionsGroupBox.ForeColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.OptionsGroupBox, "OptionsGroupBox");
            this.OptionsGroupBox.Name = "OptionsGroupBox";
            this.OptionsGroupBox.TabStop = false;
            // 
            // DimWindowCheckBox
            // 
            resources.ApplyResources(this.DimWindowCheckBox, "DimWindowCheckBox");
            this.DimWindowCheckBox.Name = "DimWindowCheckBox";
            this.DimWindowCheckBox.UseVisualStyleBackColor = true;
            this.DimWindowCheckBox.CheckedChanged += new System.EventHandler(this.DimWindowCheckBox_CheckedChanged);
            // 
            // RunOnStartUpCheckBox
            // 
            resources.ApplyResources(this.RunOnStartUpCheckBox, "RunOnStartUpCheckBox");
            this.RunOnStartUpCheckBox.Name = "RunOnStartUpCheckBox";
            this.RunOnStartUpCheckBox.UseVisualStyleBackColor = true;
            this.RunOnStartUpCheckBox.CheckedChanged += new System.EventHandler(this.RunOnStartupCheckBox_CheckedChanged);
            // 
            // SettingsForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.Controls.Add(this.OptionsGroupBox);
            this.Controls.Add(this.PreviewGroupBox);
            this.Controls.Add(this.DimmingGroupBox);
            this.Controls.Add(this.DayNightCycleGroupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "SettingsForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingsForm_Closing);
            this.Resize += new System.EventHandler(this.SettignsFormResize);
            ((System.ComponentModel.ISupportInitialize)(this.OpacityDaySlider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OpacityNightSlider)).EndInit();
            this.DayNightCycleGroupBox.ResumeLayout(false);
            this.DayNightCycleGroupBox.PerformLayout();
            this.DimmingGroupBox.ResumeLayout(false);
            this.DimmingGroupBox.PerformLayout();
            this.PreviewGroupBox.ResumeLayout(false);
            this.PreviewGroupBox.PerformLayout();
            this.OptionsGroupBox.ResumeLayout(false);
            this.OptionsGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        private void SettingsForm_Closing()
        {
            System.Environment.Exit(0);
        }
    }



}
