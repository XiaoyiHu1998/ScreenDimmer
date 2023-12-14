using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace ScreenDimmer
{
    public class OverlayForm : Form
    {
        private CoreLogic core;

        public OverlayForm(CoreLogic core)
        {
            this.core = core;

            this.WindowState = FormWindowState.Normal;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.Black;
            this.Opacity = core.overlayFormOpacity;
            this.TopMost = true;
        }

        public void UpdateOpacity()
        {
            this.Opacity = core.overlayFormOpacity;
            Refresh();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.DoubleBuffered = true;
            this.ShowInTaskbar = false;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.Escape)
                this.Close();
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

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // OverlayForm
            // 
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(227, 433);
            this.Name = "OverlayForm";
            this.Load += new System.EventHandler(this.OverlayForm_Load);
            this.ResumeLayout(false);

        }

        private void OverlayForm_Load(object sender, EventArgs e)
        {

        }
    }

    
}
