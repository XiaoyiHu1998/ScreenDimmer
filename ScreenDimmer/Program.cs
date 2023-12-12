using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace ScreenDimmer
{
    public class Program
    {
        private static SettingsForm settingsForm;
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            settingsForm = new SettingsForm();
            Application.Run(settingsForm);

            //Thread thread = new Thread(() => );
        }

        public void UpdateThread()
        {
            while (true)
            {
                settingsForm.UpdateOverlayForms();
                Thread.Sleep(60000);
            }
        }
    }
}