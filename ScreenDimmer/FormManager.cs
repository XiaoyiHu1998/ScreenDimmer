using System;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace ScreenDimmer
{
    public class FormManager : Form
    {
        CoreLogic core;
        SettingsForm settingsForm;
        List<OverlayForm> overlayForms;

        System.Windows.Forms.Timer overlayUpdateTimer;

        public FormManager(CoreLogic core)
        {
            InitializeComponent();
            SetOverlayUpdateTimer();
            SystemEvents.PowerModeChanged += OverlayUpdateTimer_PowerModeChanged;

            this.core = core;
            overlayForms = new List<OverlayForm>();
            for (int i = 0; i < Screen.AllScreens.Length; i++)
            {
                OverlayForm currentOverlayForm = new OverlayForm(core);
                Screen currentScreen = Screen.AllScreens[i];

                currentOverlayForm.StartPosition = FormStartPosition.Manual;
                currentOverlayForm.Location = currentScreen.Bounds.Location;
                currentOverlayForm.Bounds = currentScreen.Bounds;

                overlayForms.Add(currentOverlayForm);
                overlayForms[i].Show();
            }

            settingsForm = new SettingsForm(core, this, OverlayUpdateTick);
            settingsForm.Show();
        }

        private void SetOverlayUpdateTimer()
        {
            overlayUpdateTimer = new Timer();
            overlayUpdateTimer.Interval = (int)Math.Floor(DefaultSettings.MaxTransitionUpdateIntervalSeconds * 1000);
            overlayUpdateTimer.Tick += new EventHandler(OverlayUpdateTick);
            overlayUpdateTimer.Start();
        }

        private void OverlayUpdateTick(object sender, EventArgs e)
        {
            OverlayUpdate();
        }

        private void OverlayUpdate()
        {
            core.Update();
            overlayUpdateTimer.Stop();
            overlayUpdateTimer.Interval = core.GetUpdateTimerInterval();
            overlayUpdateTimer.Start();

            foreach (OverlayForm overlayForm in overlayForms)
            {
                overlayForm.UpdateOpacity();
            }
        }

        private void OverlayUpdateTimer_PowerModeChanged(object sender, PowerModeChangedEventArgs e)
        {
            if (e.Mode == PowerModes.Resume)
            {
                OverlayUpdate();
            }
        }

        private void FormManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            SystemEvents.PowerModeChanged -= OverlayUpdateTimer_PowerModeChanged;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // FormManager
            // 
            this.ClientSize = new System.Drawing.Size(10, 10);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "FormManager";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "FormManager";
            this.ResumeLayout(false);
            this.Visible = false;
            this.Opacity = 0;
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.FormClosing += new FormClosingEventHandler(this.FormManager_FormClosing);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x20;  // WS_EX_TRANSPARENT
                return cp;
            }
        }
    }
}
