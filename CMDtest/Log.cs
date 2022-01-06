using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDtest
{
    internal class Log
    {
        static string logPath = @"C:\Users\user\Desktop\Task\RevitByEric\CMDtest\bin\Debug\Log";
        static string logFileName = DateTime.Now.Year.ToString() + int.Parse(DateTime.Now.Month.ToString()).ToString("00") + int.Parse(DateTime.Now.Day.ToString()).ToString("00") + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00") + ".txt";
        static void createDir()
        {
            if (!Directory.Exists(logPath))
                Directory.CreateDirectory(logPath);
        }

        static void createFile()
        {
            if (!File.Exists($@"{logPath}\{logFileName}"))
            {
                File.Create($@"{logPath}\{logFileName}");
            }
        }

        public static void Logger(string content,bool openFile)
        {

            createDir();
            //createFile();
            using (StreamWriter sw = File.AppendText($@"{logPath}\{logFileName}"))
            {
                sw.WriteLine(content);
                sw.WriteLine();
            }
            if (openFile)
            {
                Process.Start($@"{logPath}\{logFileName}");
            }
        }
    }
}
