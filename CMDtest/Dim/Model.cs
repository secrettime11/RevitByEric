using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDtest.Dim
{
    public class Model
    {
        public static List<string> extra { get; set; }
        public static string choose { get; set; }
        public class ini
        {
            public static List<string> DimInfo = new List<string>();
            /// <summary>
            /// up / down
            /// </summary>
            public static string BaseX { get; set; }
            /// <summary>
            /// left / right
            /// </summary>
            public static string BaseY { get; set; }
            public static decimal MainPipeDia { get; set; }
            public static string DimType { get; set; }
        }
        public class ElementInfo
        {
            public ElementId id { get; set; }
            public string name { get; set; }
            public string Type { get; set; }
            public double pipeAvg { get; set; }
            public XYZ point { get; set; }
            public BoundingBoxXYZ pipe_BoxXYZ { get; set; }
        }

        public class DimReference
        {
            public ElementId elementId { get; set; }
            public XYZ point { get; set; }
            public Reference refElement { get; set; }
        }
    }
}
