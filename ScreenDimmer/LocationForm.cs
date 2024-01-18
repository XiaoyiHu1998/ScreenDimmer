using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenDimmer
{
    public class LocationForm : Form
    {
        private CoreLogic core;
        private SettingsForm settingsForm;

        float northDegrees;
        float eastDegrees;

        private Label LocationLabel;
        private TextBox NorthDegreeBox;
        private Label NorthLabel;
        private Label DegreeNorthLabel;
        private Button SaveButton;
        private Label DegreeEastLabel;
        private Label EastLabel;
        private TextBox EastDegreeBox;
        private Button CancelLocationButton;

        public LocationForm(CoreLogic core, SettingsForm settingsForm)
        {
            InitializeComponent();
            this.core = core;
            this.settingsForm = settingsForm;
            this.TopMost = settingsForm.TopMost;

            northDegrees = settingsForm.latitude;
            eastDegrees = settingsForm.longitude;

            NorthDegreeBox.Text = northDegrees.ToString();
            EastDegreeBox.Text = eastDegrees.ToString();
        }

        private void TextBoxCheck_LostFocus(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;

            float newDegrees;
            if(!float.TryParse(textBox.Text, out newDegrees))
            {
                float resetValue = (textBox.Name == "NorthDegreeBox") ? northDegrees : eastDegrees;
                textBox.Text = resetValue.ToString();
            }

            if(textBox.Name == "NorthDegreeBox")
            {
                northDegrees = newDegrees;
            }
            else
            {
                eastDegrees = newDegrees;
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            core.UpdateGeoLocation(northDegrees, eastDegrees);

            settingsForm.latitude = northDegrees;
            settingsForm.longitude = eastDegrees;

            this.Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void NextControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                e.Handled = true;
            }

        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LocationForm));
            this.LocationLabel = new System.Windows.Forms.Label();
            this.NorthDegreeBox = new System.Windows.Forms.TextBox();
            this.NorthLabel = new System.Windows.Forms.Label();
            this.DegreeNorthLabel = new System.Windows.Forms.Label();
            this.SaveButton = new System.Windows.Forms.Button();
            this.DegreeEastLabel = new System.Windows.Forms.Label();
            this.EastLabel = new System.Windows.Forms.Label();
            this.EastDegreeBox = new System.Windows.Forms.TextBox();
            this.CancelLocationButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LocationLabel
            // 
            this.LocationLabel.AutoSize = true;
            this.LocationLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.LocationLabel.Location = new System.Drawing.Point(12, 9);
            this.LocationLabel.Name = "LocationLabel";
            this.LocationLabel.Size = new System.Drawing.Size(51, 13);
            this.LocationLabel.TabIndex = 0;
            this.LocationLabel.Text = "Location:";
            // 
            // NorthDegreeBox
            // 
            this.NorthDegreeBox.Location = new System.Drawing.Point(52, 29);
            this.NorthDegreeBox.Name = "NorthDegreeBox";
            this.NorthDegreeBox.Size = new System.Drawing.Size(35, 20);
            this.NorthDegreeBox.TabIndex = 1;
            this.NorthDegreeBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NextControl_KeyPress);
            this.NorthDegreeBox.LostFocus += new System.EventHandler(this.TextBoxCheck_LostFocus);
            // 
            // NorthLabel
            // 
            this.NorthLabel.AutoSize = true;
            this.NorthLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.NorthLabel.Location = new System.Drawing.Point(14, 32);
            this.NorthLabel.Name = "NorthLabel";
            this.NorthLabel.Size = new System.Drawing.Size(36, 13);
            this.NorthLabel.TabIndex = 5;
            this.NorthLabel.Text = "North:";
            // 
            // DegreeNorthLabel
            // 
            this.DegreeNorthLabel.AutoSize = true;
            this.DegreeNorthLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.DegreeNorthLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.DegreeNorthLabel.Location = new System.Drawing.Point(86, 28);
            this.DegreeNorthLabel.Name = "DegreeNorthLabel";
            this.DegreeNorthLabel.Size = new System.Drawing.Size(14, 17);
            this.DegreeNorthLabel.TabIndex = 6;
            this.DegreeNorthLabel.Text = "°";
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(45, 66);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(52, 23);
            this.SaveButton.TabIndex = 3;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            this.SaveButton.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NextControl_KeyPress);
            // 
            // DegreeEastLabel
            // 
            this.DegreeEastLabel.AutoSize = true;
            this.DegreeEastLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.DegreeEastLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.DegreeEastLabel.Location = new System.Drawing.Point(168, 28);
            this.DegreeEastLabel.Name = "DegreeEastLabel";
            this.DegreeEastLabel.Size = new System.Drawing.Size(14, 17);
            this.DegreeEastLabel.TabIndex = 8;
            this.DegreeEastLabel.Text = "°";
            // 
            // EastLabel
            // 
            this.EastLabel.AutoSize = true;
            this.EastLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.EastLabel.Location = new System.Drawing.Point(101, 32);
            this.EastLabel.Name = "EastLabel";
            this.EastLabel.Size = new System.Drawing.Size(31, 13);
            this.EastLabel.TabIndex = 7;
            this.EastLabel.Text = "East:";
            // 
            // EastDegreeBox
            // 
            this.EastDegreeBox.Location = new System.Drawing.Point(133, 29);
            this.EastDegreeBox.Name = "EastDegreeBox";
            this.EastDegreeBox.Size = new System.Drawing.Size(35, 20);
            this.EastDegreeBox.TabIndex = 2;
            this.EastDegreeBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NextControl_KeyPress);
            this.EastDegreeBox.LostFocus += new System.EventHandler(this.TextBoxCheck_LostFocus);
            // 
            // CancelLocationButton
            // 
            this.CancelLocationButton.Location = new System.Drawing.Point(105, 66);
            this.CancelLocationButton.Name = "CancelLocationButton";
            this.CancelLocationButton.Size = new System.Drawing.Size(54, 23);
            this.CancelLocationButton.TabIndex = 4;
            this.CancelLocationButton.Text = "Cancel";
            this.CancelLocationButton.UseVisualStyleBackColor = true;
            this.CancelLocationButton.Click += new System.EventHandler(this.CancelButton_Click);
            this.CancelLocationButton.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NextControl_KeyPress);
            // 
            // LocationForm
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(194, 101);
            this.Controls.Add(this.CancelLocationButton);
            this.Controls.Add(this.DegreeEastLabel);
            this.Controls.Add(this.EastLabel);
            this.Controls.Add(this.EastDegreeBox);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.DegreeNorthLabel);
            this.Controls.Add(this.NorthLabel);
            this.Controls.Add(this.NorthDegreeBox);
            this.Controls.Add(this.LocationLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LocationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Location Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
