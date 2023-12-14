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
        //reference to other forms
        FormManager parentForm;
        CoreLogic core;
        Action<object, EventArgs> overlayUpdateTick;

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

        private void NightStartHourBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            core.nightStartHour = BoxIndexToHour(NightStartHourBox.SelectedIndex);
            core.Update();
            overlayUpdateTick(sender, e);
        }

        private void NightStartMinuteBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            core.nightStartMinute = BoxIndexToMinute(NightStartMinuteBox.SelectedIndex);
            core.Update();
            overlayUpdateTick(sender, e);
        }

        private void DayStartHourBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            core.dayStartHour = BoxIndexToHour(DayStartHourBox.SelectedIndex);
            core.Update();
            overlayUpdateTick(sender, e);
        }

        private void DayStartMinuteBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            core.dayStartMinute = BoxIndexToMinute(DayStartMinuteBox.SelectedIndex);
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
            // NightStartHourBox
            // 
            this.NightStartHourBox.FormattingEnabled = true;
            this.NightStartHourBox.Items.AddRange(new object[] {
            resources.GetString("NightStartHourBox.Items"),
            resources.GetString("NightStartHourBox.Items1"),
            resources.GetString("NightStartHourBox.Items2"),
            resources.GetString("NightStartHourBox.Items3"),
            resources.GetString("NightStartHourBox.Items4"),
            resources.GetString("NightStartHourBox.Items5"),
            resources.GetString("NightStartHourBox.Items6"),
            resources.GetString("NightStartHourBox.Items7"),
            resources.GetString("NightStartHourBox.Items8"),
            resources.GetString("NightStartHourBox.Items9"),
            resources.GetString("NightStartHourBox.Items10"),
            resources.GetString("NightStartHourBox.Items11"),
            resources.GetString("NightStartHourBox.Items12"),
            resources.GetString("NightStartHourBox.Items13"),
            resources.GetString("NightStartHourBox.Items14"),
            resources.GetString("NightStartHourBox.Items15"),
            resources.GetString("NightStartHourBox.Items16"),
            resources.GetString("NightStartHourBox.Items17"),
            resources.GetString("NightStartHourBox.Items18"),
            resources.GetString("NightStartHourBox.Items19"),
            resources.GetString("NightStartHourBox.Items20"),
            resources.GetString("NightStartHourBox.Items21"),
            resources.GetString("NightStartHourBox.Items22"),
            resources.GetString("NightStartHourBox.Items23")});
            resources.ApplyResources(this.NightStartHourBox, "NightStartHourBox");
            this.NightStartHourBox.Name = "NightStartHourBox";
            this.NightStartHourBox.SelectedIndexChanged += new System.EventHandler(this.NightStartHourBox_SelectedIndexChanged);
            // 
            // NightStartMinuteBox
            // 
            this.NightStartMinuteBox.FormattingEnabled = true;
            this.NightStartMinuteBox.Items.AddRange(new object[] {
            resources.GetString("NightStartMinuteBox.Items"),
            resources.GetString("NightStartMinuteBox.Items1"),
            resources.GetString("NightStartMinuteBox.Items2"),
            resources.GetString("NightStartMinuteBox.Items3"),
            resources.GetString("NightStartMinuteBox.Items4"),
            resources.GetString("NightStartMinuteBox.Items5"),
            resources.GetString("NightStartMinuteBox.Items6"),
            resources.GetString("NightStartMinuteBox.Items7"),
            resources.GetString("NightStartMinuteBox.Items8"),
            resources.GetString("NightStartMinuteBox.Items9"),
            resources.GetString("NightStartMinuteBox.Items10"),
            resources.GetString("NightStartMinuteBox.Items11")});
            resources.ApplyResources(this.NightStartMinuteBox, "NightStartMinuteBox");
            this.NightStartMinuteBox.Name = "NightStartMinuteBox";
            this.NightStartMinuteBox.SelectedIndexChanged += new System.EventHandler(this.NightStartMinuteBox_SelectedIndexChanged);
            // 
            // TransitionTimeMinuteBox
            // 
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
            // TransitionTimeHourBox
            // 
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
            // DayStartHourBox
            // 
            this.DayStartHourBox.FormattingEnabled = true;
            this.DayStartHourBox.Items.AddRange(new object[] {
            resources.GetString("DayStartHourBox.Items"),
            resources.GetString("DayStartHourBox.Items1"),
            resources.GetString("DayStartHourBox.Items2"),
            resources.GetString("DayStartHourBox.Items3"),
            resources.GetString("DayStartHourBox.Items4"),
            resources.GetString("DayStartHourBox.Items5"),
            resources.GetString("DayStartHourBox.Items6"),
            resources.GetString("DayStartHourBox.Items7"),
            resources.GetString("DayStartHourBox.Items8"),
            resources.GetString("DayStartHourBox.Items9"),
            resources.GetString("DayStartHourBox.Items10"),
            resources.GetString("DayStartHourBox.Items11"),
            resources.GetString("DayStartHourBox.Items12"),
            resources.GetString("DayStartHourBox.Items13"),
            resources.GetString("DayStartHourBox.Items14"),
            resources.GetString("DayStartHourBox.Items15"),
            resources.GetString("DayStartHourBox.Items16"),
            resources.GetString("DayStartHourBox.Items17"),
            resources.GetString("DayStartHourBox.Items18"),
            resources.GetString("DayStartHourBox.Items19"),
            resources.GetString("DayStartHourBox.Items20"),
            resources.GetString("DayStartHourBox.Items21"),
            resources.GetString("DayStartHourBox.Items22"),
            resources.GetString("DayStartHourBox.Items23")});
            resources.ApplyResources(this.DayStartHourBox, "DayStartHourBox");
            this.DayStartHourBox.Name = "DayStartHourBox";
            this.DayStartHourBox.SelectedIndexChanged += new System.EventHandler(this.DayStartHourBox_SelectedIndexChanged);
            // 
            // DayStartMinuteBox
            // 
            this.DayStartMinuteBox.FormattingEnabled = true;
            this.DayStartMinuteBox.Items.AddRange(new object[] {
            resources.GetString("DayStartMinuteBox.Items"),
            resources.GetString("DayStartMinuteBox.Items1"),
            resources.GetString("DayStartMinuteBox.Items2"),
            resources.GetString("DayStartMinuteBox.Items3"),
            resources.GetString("DayStartMinuteBox.Items4"),
            resources.GetString("DayStartMinuteBox.Items5"),
            resources.GetString("DayStartMinuteBox.Items6"),
            resources.GetString("DayStartMinuteBox.Items7"),
            resources.GetString("DayStartMinuteBox.Items8"),
            resources.GetString("DayStartMinuteBox.Items9"),
            resources.GetString("DayStartMinuteBox.Items10"),
            resources.GetString("DayStartMinuteBox.Items11")});
            resources.ApplyResources(this.DayStartMinuteBox, "DayStartMinuteBox");
            this.DayStartMinuteBox.Name = "DayStartMinuteBox";
            this.DayStartMinuteBox.SelectedIndexChanged += new System.EventHandler(this.DayStartMinuteBox_SelectedIndexChanged);
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
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingsForm_Closing);
            this.Load += new System.EventHandler(this.SettingsForm_Load);
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

        private void SettingsForm_Load(object sender, EventArgs e)
        {

        }
    }



}
