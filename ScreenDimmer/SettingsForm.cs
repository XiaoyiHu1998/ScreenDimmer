﻿using System;
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
        //reference to other forms
        FormManager parentForm;
        CoreLogic core;
        Action<object, EventArgs> overlayUpdateTick;

        //UI elements
        private TrackBar OpacityDaySlider;
        private Label NightStartTimeLabel;
        private TrackBar OpacityNightSlider;
        private Label TransitionTimeLabel;
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
        private GroupBox PreviewGroup;
        private RadioButton PreviewDayRadioButton;
        private CheckBox PreviewEnableCheckedBox;
        private RadioButton PreviewNightRadioButton;
        private DateTimePicker NightStartHourDateTimePicker;
        private ComboBox TransitionTimeHourBox;
        private ComboBox TransitionTimeMinuteBox;
        private DateTimePicker DayStartMinuteDateTimePicker;
        private DateTimePicker NightStartMinuteDateTimePicker;
        private DateTimePicker DayStartHourDateTimePicker;
        private CheckBox NightTransitionEnabledCheckedBox;

        public SettingsForm(CoreLogic core, FormManager parentForm, Action<object, EventArgs> overlayUpdateTick)
        {
            this.parentForm = parentForm;
            this.core = core;
            this.overlayUpdateTick = overlayUpdateTick;
            InitializeComponent();
            SetDefaultValues();
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
            if (CanFocus)
            {
                Focus();
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if(keyData == Keys.Enter)
            {
                NightTransitionControlGroup.Focus();
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
        private void NightTransitionEnabledCheckedBox_CheckedChanged(object sender, EventArgs e)
        {
            core.nightTransitionEnabled = NightTransitionEnabledCheckedBox.Checked;
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

        private void TransitionTimeHourBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            core.transitionTimeHour = BoxIndexToHour(TransitionTimeHourBox.SelectedIndex);
            core.Update();
            overlayUpdateTick(sender, e);
        }

        private void TransitionTimeMinuteBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            core.transitionTimeMinute = BoxIndexToMinute(TransitionTimeMinuteBox.SelectedIndex);
            core.Update();
            overlayUpdateTick(sender, e);
        }
        #endregion

        #region Dimming Preview
        //________________ Dimming Preview ________________
        private void PreviewEnableCheckedBox_CheckedChanged(object sender, EventArgs e)
        {
            core.previewEnabled = PreviewEnableCheckedBox.Checked;
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
            this.NightTransitionControlGroup = new System.Windows.Forms.GroupBox();
            this.NightEndTimeLabel = new System.Windows.Forms.Label();
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
            this.NightStartHourDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.TransitionTimeHourBox = new System.Windows.Forms.ComboBox();
            this.TransitionTimeMinuteBox = new System.Windows.Forms.ComboBox();
            this.DayStartHourDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.NightStartMinuteDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.DayStartMinuteDateTimePicker = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.OpacityDaySlider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OpacityNightSlider)).BeginInit();
            this.NightTransitionControlGroup.SuspendLayout();
            this.DimmingControlGroup.SuspendLayout();
            this.PreviewGroup.SuspendLayout();
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
            // NightTransitionControlGroup
            // 
            this.NightTransitionControlGroup.Controls.Add(this.DayStartMinuteDateTimePicker);
            this.NightTransitionControlGroup.Controls.Add(this.NightStartMinuteDateTimePicker);
            this.NightTransitionControlGroup.Controls.Add(this.DayStartHourDateTimePicker);
            this.NightTransitionControlGroup.Controls.Add(this.NightStartHourDateTimePicker);
            this.NightTransitionControlGroup.Controls.Add(this.NightEndTimeLabel);
            this.NightTransitionControlGroup.Controls.Add(this.NightTransitionEnabledCheckedBox);
            this.NightTransitionControlGroup.Controls.Add(this.TransitionTimeHourBox);
            this.NightTransitionControlGroup.Controls.Add(this.NightStartTimeLabel);
            this.NightTransitionControlGroup.Controls.Add(this.TransitionTimeLabel);
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
            // NightTransitionEnabledCheckedBox
            // 
            resources.ApplyResources(this.NightTransitionEnabledCheckedBox, "NightTransitionEnabledCheckedBox");
            this.NightTransitionEnabledCheckedBox.Name = "NightTransitionEnabledCheckedBox";
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
            // TransitionTimeHourBox
            // 
            this.TransitionTimeHourBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TransitionTimeHourBox.FormattingEnabled = true;
            this.TransitionTimeHourBox.Items.AddRange(new object[] {
            resources.GetString("TransitionTimeHourBox.Items"),
            resources.GetString("TransitionTimeHourBox.Items1"),
            resources.GetString("TransitionTimeHourBox.Items2"),
            resources.GetString("TransitionTimeHourBox.Items3"),
            resources.GetString("TransitionTimeHourBox.Items4"),
            resources.GetString("TransitionTimeHourBox.Items5"),
            resources.GetString("TransitionTimeHourBox.Items6")});
            resources.ApplyResources(this.TransitionTimeHourBox, "TransitionTimeHourBox");
            this.TransitionTimeHourBox.Name = "TransitionTimeHourBox";
            this.TransitionTimeHourBox.SelectedIndexChanged += new System.EventHandler(this.TransitionTimeHourBox_SelectedIndexChanged);
            // 
            // TransitionTimeMinuteBox
            // 
            this.TransitionTimeMinuteBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TransitionTimeMinuteBox.FormattingEnabled = true;
            this.TransitionTimeMinuteBox.Items.AddRange(new object[] {
            resources.GetString("TransitionTimeMinuteBox.Items"),
            resources.GetString("TransitionTimeMinuteBox.Items1"),
            resources.GetString("TransitionTimeMinuteBox.Items2"),
            resources.GetString("TransitionTimeMinuteBox.Items3"),
            resources.GetString("TransitionTimeMinuteBox.Items4"),
            resources.GetString("TransitionTimeMinuteBox.Items5"),
            resources.GetString("TransitionTimeMinuteBox.Items6"),
            resources.GetString("TransitionTimeMinuteBox.Items7"),
            resources.GetString("TransitionTimeMinuteBox.Items8"),
            resources.GetString("TransitionTimeMinuteBox.Items9"),
            resources.GetString("TransitionTimeMinuteBox.Items10"),
            resources.GetString("TransitionTimeMinuteBox.Items11")});
            resources.ApplyResources(this.TransitionTimeMinuteBox, "TransitionTimeMinuteBox");
            this.TransitionTimeMinuteBox.Name = "TransitionTimeMinuteBox";
            this.TransitionTimeMinuteBox.SelectedIndexChanged += new System.EventHandler(this.TransitionTimeMinuteBox_SelectedIndexChanged);
            // 
            // DayStartHourDateTimePicker
            // 
            resources.ApplyResources(this.DayStartHourDateTimePicker, "DayStartHourDateTimePicker");
            this.DayStartHourDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DayStartHourDateTimePicker.Name = "DayStartHourDateTimePicker";
            this.DayStartHourDateTimePicker.ShowUpDown = true;
            this.DayStartHourDateTimePicker.ValueChanged += new System.EventHandler(this.DayStartHourDateTimePicker_ValueChanged);
            // 
            // DayStartMinuteDateTimePicker
            // 
            resources.ApplyResources(this.DayStartMinuteDateTimePicker, "DayStartMinuteDateTimePicker");
            this.DayStartMinuteDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DayStartMinuteDateTimePicker.Name = "DayStartMinuteDateTimePicker";
            this.DayStartMinuteDateTimePicker.ShowUpDown = true;
            this.DayStartMinuteDateTimePicker.ValueChanged += new System.EventHandler(this.DayStartMinuteDateTimePicker_ValueChanged);
            // 
            // NightStartHourDateTimePicker
            // 
            resources.ApplyResources(this.NightStartHourDateTimePicker, "NightStartHourDateTimePicker");
            this.NightStartHourDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.NightStartHourDateTimePicker.Name = "NightStartHourDateTimePicker";
            this.NightStartHourDateTimePicker.ShowUpDown = true;
            this.NightStartHourDateTimePicker.ValueChanged += new System.EventHandler(this.NightStartHourDateTimePicker_ValueChanged);
            // 
            // NightStartMinuteDateTimePicker
            // 
            resources.ApplyResources(this.NightStartMinuteDateTimePicker, "NightStartMinuteDateTimePicker");
            this.NightStartMinuteDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.NightStartMinuteDateTimePicker.Name = "NightStartMinuteDateTimePicker";
            this.NightStartMinuteDateTimePicker.ShowUpDown = true;
            this.NightStartMinuteDateTimePicker.ValueChanged += new System.EventHandler(this.NightStartMinuteDateTimePicker_ValueChanged);
            // 
            // SettingsForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.Controls.Add(this.PreviewGroup);
            this.Controls.Add(this.DimmingControlGroup);
            this.Controls.Add(this.NightTransitionControlGroup);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "SettingsForm";
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

        private void SettingsForm_Closing()
        {
            System.Environment.Exit(0);
        }
    }



}
