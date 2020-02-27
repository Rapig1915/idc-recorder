using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IDCRecorder
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Helper.CreateDirectory();
            if(!Helper.LoadD3DGear())
            {
                Helper.ShowMsg("Cannot load Game recording engine. You can't do things with Game.", "Warn", MessageBoxIcon.Warning);
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
