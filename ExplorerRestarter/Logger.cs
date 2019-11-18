using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ExplorerRestarter
{
    public enum Severity
    {
        Critical = 9,
        Exception = 3,
        Log = 0
    }
    public class Logger 
    {
        public readonly string Filename = "log.log";

        public void Log(Severity severity, string message)
        {
            if (severity == Severity.Critical)
            {
                Write(message);
                MessageBox.Show(message, "Critical error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
                Write(message);
        }

        private void Write(string message)
        {
            using (FileStream fs = new FileStream(Filename, FileMode.Append))
            using (StreamWriter sw = new StreamWriter(fs))
            {
                sw.WriteLine($"[{DateTime.Now.ToLongTimeString()}]: {message}");
                sw.Flush();
            }
        }

    }
}
