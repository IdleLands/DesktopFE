using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IdleLandsGUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            IdleLandsComms comms = new IdleLandsComms();
            Timer timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += comms.DoTick;
            timer.Start();
            LoginForm form = new LoginForm(comms);
            form.Show();
            Application.Run();
        }
    }
}
