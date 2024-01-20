using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenDimmer
{
    public class AboutForm : Form
    {
        private Label TitleLabel;
        private Label VersionLabel;
        private Label VersionNumberLabel;
        private Label label1;
        private Label CopyrightLabel;

        public AboutForm(SettingsForm settingsForm)
        {
            InitializeComponent();
            this.TopMost = settingsForm.TopMost;
        }


        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
            this.TitleLabel = new System.Windows.Forms.Label();
            this.CopyrightLabel = new System.Windows.Forms.Label();
            this.VersionLabel = new System.Windows.Forms.Label();
            this.VersionNumberLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TitleLabel
            // 
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TitleLabel.Location = new System.Drawing.Point(12, 9);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(229, 37);
            this.TitleLabel.TabIndex = 0;
            this.TitleLabel.Text = "ScreenDimmer";
            // 
            // CopyrightLabel
            // 
            this.CopyrightLabel.AutoSize = true;
            this.CopyrightLabel.Location = new System.Drawing.Point(16, 46);
            this.CopyrightLabel.Name = "CopyrightLabel";
            this.CopyrightLabel.Size = new System.Drawing.Size(138, 13);
            this.CopyrightLabel.TabIndex = 1;
            this.CopyrightLabel.Text = "Copyright © 2023 Xiaoyi Hu";
            // 
            // VersionLabel
            // 
            this.VersionLabel.AutoSize = true;
            this.VersionLabel.Location = new System.Drawing.Point(16, 59);
            this.VersionLabel.Name = "VersionLabel";
            this.VersionLabel.Size = new System.Drawing.Size(42, 13);
            this.VersionLabel.TabIndex = 2;
            this.VersionLabel.Text = "Version";
            // 
            // VersionNumberLabel
            // 
            this.VersionNumberLabel.AutoSize = true;
            this.VersionNumberLabel.Location = new System.Drawing.Point(55, 59);
            this.VersionNumberLabel.Name = "VersionNumberLabel";
            this.VersionNumberLabel.Size = new System.Drawing.Size(40, 13);
            this.VersionNumberLabel.TabIndex = 3;
            this.VersionNumberLabel.Text = "0.0.0.0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 81);
            this.label1.MaximumSize = new System.Drawing.Size(220, 300);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(218, 156);
            this.label1.TabIndex = 5;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // AboutForm
            // 
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(255, 261);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.VersionNumberLabel);
            this.Controls.Add(this.VersionLabel);
            this.Controls.Add(this.CopyrightLabel);
            this.Controls.Add(this.TitleLabel);
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "About";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
