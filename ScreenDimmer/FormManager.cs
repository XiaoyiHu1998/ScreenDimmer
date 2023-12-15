using System;
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

        Timer overlayUpdateTimer;

        public FormManager(CoreLogic core)
        {
            SetFormManagerInvisible();

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

            SetTimer();
        }

        private void SetFormManagerInvisible()
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.WindowState = FormWindowState.Minimized;
            this.Visible = false;
            this.ShowInTaskbar = false;
            this.Refresh();
        }

        private void SetTimer()
        {
            overlayUpdateTimer = new Timer();
            overlayUpdateTimer.Interval = (int)Math.Floor(Default.UpdateIntervalSeconds * 1000);
            overlayUpdateTimer.Tick += new EventHandler(OverlayUpdateTick);
            overlayUpdateTimer.Start();
        }

        private void OverlayUpdateTick(object sender, EventArgs e)
        {
            core.Update();
            foreach (OverlayForm overlayForm in overlayForms)
            {
                overlayForm.UpdateOpacity();
            }
        }
    }
}
