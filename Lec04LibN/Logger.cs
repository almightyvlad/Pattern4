using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lec04LibN
{
    public class Logger : ILogger
    {
        private static ILogger instance;
        private Stack<string> _namespace = new Stack<string>();
        private int id = 0;
        private string LogFileName = string.Format(@"{0}/LOG{1}.txt", Directory.GetCurrentDirectory(), DateTime.Now.ToString("yyyMMdd-HH-mm-ss"));
        private Logger()
        {
            logWrite("INIT");
        }
        public static ILogger create()
        {
            if (instance == null)
            {
                instance = new Logger();
            }
            return instance;
        }
        public void start(string title)
        {
            _namespace.Push(title);
            logWrite("STRT");
        }
        public void log(string message = "")
        {
            logWrite("INFO", message);
        }
        public void logWrite(string logType, string message = "")
        {
            id++;
            string namespaces = _namespace.Any() ? string.Join(":", _namespace.Reverse()) + ":" : "";
            File.AppendAllText(LogFileName, $"{id:d6}-{DateTime.Now:dd.MM.yyyy HH:mm:ss}-{logType} {namespaces} {message}\n");
        }
        public void stop()
        {
            _namespace.Pop();
            logWrite("STOP");
        }
    }
}
