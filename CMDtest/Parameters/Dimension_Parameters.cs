using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDtest.Parameters
{
    public class Dimension_Parameters
    {
        public static List<string> DimType { get; set; }
        public static string AxisX { get; set; }
        public static string AxisY { get; set; }
        public static string Dibs { get; set; }
        public static decimal Range { get; set; }
    }

    public class ElementInfo 
    {
        public ElementId id { get; set; }
        public string Type { get; set; }
        public XYZ point { get; set; }
        public XYZ Head_pipe { get; set; }
        public XYZ End_pipe { get; set; }
        public string direction { get; set; }
    }
    public class PipeElementInfo 
    {
        public ElementId id { get; set; }
        public XYZ Head_pipe { get; set; }
        public XYZ End_pipe { get; set; }
        public string direction { get; set; }
    }
}
