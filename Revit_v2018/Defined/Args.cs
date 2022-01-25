using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RvtApplication = Autodesk.Revit.ApplicationServices.Application;
using RvtDocument = Autodesk.Revit.DB.Document;

namespace Revit_v2018.Defined
{
    public class Args
    {
        public static List<Data> AllData = new List<Data>();
        public static string NowObj = String.Empty;
        public static List<string> Category = new List<string>();
        public static List<string> Category_Sort = new List<string>();
        public static string version { get; set; }

        public class ButtonInfo
        {
            public string Name { get; set; }
            public string Text { get; set; }
            public string AssemblyName { get; set; }
            public string ClassName { get; set; }
            public string ImgURi { get; set; }
            public string ToolTip { get; set; }
            public string LongDescription { get; set; }
        }
        public class Data
        {
            /// <summary>
            /// 識別物件唯一Id
            /// </summary>
            public string Id { get; set; }
            public string Category { get; set; }
            public string Family { get; set; }
            public string Symbol { get; set; }
            public FamilySymbol FamilySymbol_ { get; set; }
            public ElementType ElementType_ { get; set; }
            public Bitmap Img { get; set; }
        }
    }
}
