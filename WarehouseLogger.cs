using System;
using System.IO;

namespace ConsoleApp1
{
    // Writes important system actions to a log file.
    public static class WarehouseLogger
    {
        private static readonly string LogFile = "waresmart_log.txt";

        public static void Log(string message)
        {
            try
            {
                string logMessage = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " | " + message;
                File.AppendAllText(LogFile, logMessage + Environment.NewLine);
            }
            catch (IOException)
            {
                // Logging must not stop the main application.
            }
        }
    }
}
