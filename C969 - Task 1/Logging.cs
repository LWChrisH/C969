using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace C969___Task_1
{
    static class Logging
    {
        private static string _fileName = "c969log.txt";
        private static string _directoryPath = Environment.CurrentDirectory;
        public static void LogEntry(string message)
        {
            string fullLogPath = Path.Combine(_directoryPath, _fileName);
            try
            {
                File.AppendAllText(fullLogPath, DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss] ") + message + "\n");
            }
            catch (Exception ex)
            {
                MessageBox.Show(Language.LanguageFill("#logfileerror \n\n" + ex.Message));
                Application.Exit();
            }
        }
        public static void LaunchLog()
        {
            Process.Start("notepad.exe", Path.Combine(_directoryPath, _fileName));
        }
    }
}
