using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JohnsonRevitAPI2.Public_Folder
{
    public class ini
    {
        public static string iniPath = $@"C:\ProgramData\Autodesk\Revit\Addins\Data\";
        //public static string ini_CategoryName = "ini_SymbolPlacement.txt";
        public static string ini_CategorySortName = "tmp.txt";
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

        public static void write(string content, string path, string fileName)
        {
            createDir(path);

            using (StreamWriter sw = new StreamWriter($@"{path}\{fileName}.txt", true))
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
