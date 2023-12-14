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
        private static CoreLogic core;
        private static FormManager formManager;

        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            core = new CoreLogic();
            formManager = new FormManager(core);

            Application.Run(formManager);
        }
    }
}