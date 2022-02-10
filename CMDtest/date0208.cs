using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDtest
{
    [TransactionAttribute(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class date0208 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string messages, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;

            Selection sel = uidoc.Selection;
            IList<Reference> eleSelecArea = sel.PickObjects(ObjectType.Element, "框選範圍");

            List<ElementId> data = (from x in eleSelecArea select x.ElementId).ToList();
            data.Reverse();

            //using (Transaction tx = new Transaction(doc))
            //{
            //tx.Start("Start");

            ReferenceArray refArray = new ReferenceArray();
            XYZ Point1 = null;
            XYZ Point2 = null;

            foreach (ElementId eid in data)
            {
                if (isPipe(doc, eid))
                {
                    if (eid.ToString() == "1580391")
                    {
                        Element elem = doc.GetElement(eid);
                        refArray.Append(new Reference(elem));
                        Pipe pp = elem as Pipe;
                        LocationCurve lc = pp.Location as LocationCurve;

                        Point1 = lc.Curve.GetEndPoint(0);
                        Point1 = new XYZ(Point1.X, Point1.Y, 0);
                    }
                    //var info = pipeDirection(doc, eid);
                    //Log.Logger($"{eid} / {info.direction}", false);
                }
                else
                {
                    if (eid.ToString() == "1580344")
                    {
                        LocationPoint eleLocation = doc.GetElement(eid).Location as LocationPoint;
                        Point2 = new XYZ(eleLocation.Point.X, Point1.Y, 0);
                        refArray.Append(refType(doc.GetElement(eid), "X"));
                    }
                }
            }
            Line Line = Line.CreateBound(Point1, Point2);
            using (Transaction trans = new Transaction(doc))
            {
                trans.Start("Create Dimension");
                doc.Create.NewDimension(doc.ActiveView, Line, refArray);
                trans.Commit();
            }
            //tx.Commit();
            // }
            //Log.Logger("", true);
            return Result.Succeeded;
        }

        private bool isPipe(Document doc, ElementId elementId)
        {
            return doc.GetElement(elementId).Category.Name == "管";
        }

        private PipeInfo pipeDirection(Document doc, ElementId elementId)
        {
            PipeInfo pipeInfo = new PipeInfo();
            Pipe pp = doc.GetElement(elementId) as Pipe;
            LocationCurve lc = pp.Location as LocationCurve;

            var Head = lc.Curve.GetEndPoint(0);
            var Tail = lc.Curve.GetEndPoint(1);
            if (almost(Head.X, Tail.X))
            {
                pipeInfo.direction = "垂直";
                pipeInfo.head = Head.Y;
                pipeInfo.tail = Tail.Y;
            }
            else if (almost(Head.Y, Tail.Y))
            {
                pipeInfo.direction = "水平";
                pipeInfo.head = Head.X;
                pipeInfo.tail = Tail.X;
            }
            else
            {
                pipeInfo.direction = "傾斜";
                pipeInfo.head = 0;
                pipeInfo.tail = 0;
            }
            return pipeInfo;
        }

        private bool almost(double A, double B)
        {
            if (Math.Abs(A - B) < 0.000005)
            {
                return true;
            }
            else
                return false;
        }
        private Reference refType(Element element)
        {
            if (element.Category.Name == "撒水頭")
            {
                return (element as FamilyInstance).GetReferences(FamilyInstanceReferenceType.CenterLeftRight).FirstOrDefault();
            }
            else
            {
                return (element as FamilyInstance).GetReferences(FamilyInstanceReferenceType.CenterFrontBack).FirstOrDefault();
            }
        }
        private Reference refType(Element element, string axis)
        {
            if (axis == "X")
                return (element as FamilyInstance).GetReferences(FamilyInstanceReferenceType.CenterLeftRight).FirstOrDefault();
            else
                return (element as FamilyInstance).GetReferences(FamilyInstanceReferenceType.CenterFrontBack).FirstOrDefault();
        }
        public class PipeInfo
        {
            public string direction { get; set; }
            public double head { get; set; }
            public double tail { get; set; }
        }
    }
}
