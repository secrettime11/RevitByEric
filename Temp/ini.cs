using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Temp
{
    internal class ini
    {
        static string Path = @"C:\Users\user\Desktop\Task\RevitByEric\Temp\bin\Debug\ini";
        static string FileName = "ini_SymbolPlacement.txt";
        static void createDir()
        {
            if (!Directory.Exists(Path))
                Directory.CreateDirectory(Path);
        }

        static void createFile()
        {
            if (!File.Exists($@"{Path}\{FileName}"))
            {
                File.Create($@"{Path}\{FileName}");
            }
        }

        public static void write(string content, bool openFile)
        {
            createDir();
            using (StreamWriter sw = new StreamWriter($@"{Path}\{FileName}", true))
            {
                sw.WriteLine(content);
            }
            if (openFile)
            {
                Process.Start($@"{Path}\{FileName}");
            }
        }

        public static List<string> read()
        {
            List<string> lines = new List<string>();
            if (File.Exists($@"C:\ProgramData\Autodesk\Revit\Addins\2018\JohnsonAPI\ini\ini_SymbolPlacement_sort.txt"))
            {
                using (StreamReader sr = new StreamReader($@"C:\ProgramData\Autodesk\Revit\Addins\2018\JohnsonAPI\ini\ini_SymbolPlacement_sort.txt"))
                {
                    while (!sr.EndOfStream)
                        lines.Add(sr.ReadLine());
                }
            }
            return lines;
        }
    }
}
