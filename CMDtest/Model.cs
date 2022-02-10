using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDtest
{
    public class Model
    {
        public ElementId elementId { get; set; }
        public XYZ coordinate { get; set; }
    }

    public class DistanceEle
    {
        public Reference reference { get; set; }
        public double distance { get; set; }
        public XYZ coordinate { get; set; }
    }
}
