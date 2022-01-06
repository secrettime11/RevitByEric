using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revit_v2018.Defined
{
    internal class ini
    {
        public static string iniPath = $@"C:\ProgramData\Autodesk\Revit\Addins\2018\JohnsonAPI\ini";
        public static string ini_CategoryName = "ini_SymbolPlacement.txt";
        public static string ini_CategorySortName = "ini_SymbolPlacement_sort.txt";
        static void createDir(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        static void createFile(string path, string fileName)
        {
            if (!File.Exists($@"{path}\{fileName}"))
            {
                File.Create($@"{path}\{fileName}");
            }
        }

        public static void write(string content, string path , string fileName)
        {
            createDir(path);

            using (StreamWriter sw = new StreamWriter($@"{path}\{fileName}", true))
            {
                sw.WriteLine(content);
            }
        }

        public static List<string> read(string path, string fileName)
        {
            List<string> lines = new List<string>();
            if (File.Exists($@"{path}\{fileName}"))
            {
                using (StreamReader sr = new StreamReader($@"{path}\{fileName}"))
                {
                    while (!sr.EndOfStream)
                        lines.Add(sr.ReadLine());
                }
            }
            return lines;
        }
    }
}
