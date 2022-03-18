using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDtest.ColorPipe
{
    public class Model
    {
        public static System.Drawing.Color colorSet { get; set; }
        public static OverrideGraphicSettings settings { get; set; }
        public static string SelectFile { get; set; }
        public static List<SystemType> SystemList { get; set; }
        public static List<SelectEle> SelectEleList { get; set; }
        public static string Status { get; set; }
    }
    public class Info 
    {
        public DateTime Date { get; set; }
        public ElementId Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public System.Drawing.Color NowColor { get; set; }
    }

    public class SystemType 
    {
        public string Name { get; set; }
        public System.Drawing.Color SysColor { get; set; }
    }

    public class SelectEle 
    {
        public string Id { get; set; }
        public System.Drawing.Color SysColor { get; set; }
    }
}
