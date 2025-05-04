using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsServiceDemo.Helper
{
    public static class LogHelper
    {
        public static void LogError(Exception ex)
        {
            string logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ServiceLog.txt");
            File.AppendAllText(logFilePath, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ": ERROR - " + ex.ToString() + Environment.NewLine);
        }

        public static void LogMessage(string message)
        {
            string logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ServiceLog.txt");
            File.AppendAllText(logFilePath, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ": " + message + Environment.NewLine);
        }
    }
}
