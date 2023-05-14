using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C969___Task_1
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
            bool isExiting = false;
            while (!Session.IsAuthenticated() && !isExiting)
            {
                //Application.Run(new Form1());
                Form logon = new LogonForm();
                Language.LanguageFill(ref logon);
                logon.ShowDialog();
                if (Session.IsAuthenticated())
                {
                    Form mainScreen = new MainScreen();
                    Language.LanguageFill(ref mainScreen);
                    Application.Run(mainScreen);
                }
                else
                {
                    isExiting = true;
                }
            }
        }
    }
}
