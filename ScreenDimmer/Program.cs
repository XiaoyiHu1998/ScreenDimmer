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
        private static CoreLogic core;
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            core = new CoreLogic();
            settingsForm = new SettingsForm(core);
            Application.Run(settingsForm);

            //Thread thread = new Thread(() => );
        }

        public void UpdateThread()
        {
            while (true)
            {
                core.Update();
                Thread.Sleep(60000);
            }
        }
    }
}