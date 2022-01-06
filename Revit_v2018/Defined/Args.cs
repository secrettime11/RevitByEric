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

        public class ButtonInfo
        {
            public string Name { get; set; }
            public string Text { get; set; }
            public string AssemblyName { get; set; }
            public string ClassName { get; set; }
            public string ImgURi { get; set; }
        }

        public class ListView_Class
        {
            public string String_Name { get; set; }
            public string String_FileName { get; set; }
        }

        public static class Families_ThatMustBeLoaded
        {
            public static string myString00 = "Furniture Chair Executive";
            public static string myString01 = "Furniture Chair Viper";
            public static string myString02 = "Furniture Couch Viper";

            public static List<string> ListStringMustHaveFamilies = new List<string>() { myString00, myString01, myString02 };
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
