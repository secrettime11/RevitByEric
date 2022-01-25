using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDtest
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class LastTTT : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string msg, ElementSet elemSet)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            View activeView = uidoc.ActiveView;


            Selection selection = commandData.Application.ActiveUIDocument.Selection;
            IList<Reference> eRef = selection.PickObjects(ObjectType.PointOnElement, "框選範圍");
            
            List<ElementId> data = (from x in eRef select x.ElementId).ToList();
            List<XYZ> coorList = new List<XYZ>();
            if (eRef != null)
            {
                foreach (ElementId eid in data)
                {
                    Element e = doc.GetElement(eid);

                    LocationPoint La = e.Location as LocationPoint;
                    if (La != null)
                    {
                        XYZ coor = La.Point;

                        if (e.Name.Contains("管束") || e.Name.Contains("撒水"))
                        {
                            coorList.Add(coor);
                        }
                    }
                }

                for (int i = 0; i < coorList.Count() - 1; i++)
                {
                    coorList[i] = new XYZ(coorList[i].X, coorList[i].Y, 0);
                    coorList[i + 1] = new XYZ(coorList[i + 1].X, coorList[i + 1].Y, 0);

                    Line geomLine = Line.CreateBound(coorList[i], coorList[i + 1]);
                    using (Transaction tx = new Transaction(doc))
                    {
                        tx.Start("tx");

                        XYZ dimPoint1 = new XYZ(coorList[i].X, coorList[i].Y + 1, coorList[i].Z);
                        XYZ dimPoint2 = new XYZ(coorList[i + 1].X, coorList[i + 1].Y + 1, coorList[i + 1].Z);
                        Line dimLine = Line.CreateBound(dimPoint1, dimPoint2);
                        ReferenceArray ra = new ReferenceArray();
                        ra.Append(eRef[i]);
                        ra.Append(eRef[i+1]);
                        Dimension dim = doc.Create.NewDimension(activeView, dimLine, ra);

                        tx.Commit();
                    }
                }
            }

            //ReferenceArray refArray = new ReferenceArray();
            //Reference reference1 = selection.PickObject(ObjectType.Element);
            //Reference reference2 = selection.PickObject(ObjectType.Element);
            //refArray.Append(reference1);
            //refArray.Append(reference2);

            //Element element1 = doc.GetElement(reference1);
            //Element element2 = doc.GetElement(reference2);

            //LocationPoint locationPoint1 = element1.Location as LocationPoint;
            //LocationPoint locationPoint2 = element2.Location as LocationPoint;
            
            //XYZ pt1 = locationPoint1.Point;
            //XYZ pt2 = locationPoint2.Point;

            //pt1 = new XYZ(pt1.X, pt1.Y, 0);
            //pt2 = new XYZ(pt2.X, pt2.Y, 0);

            //using (Transaction tx = new Transaction(doc))
            //{
            //    tx.Start("tx");

            //    XYZ dimPoint1 = new XYZ(pt1.X, pt1.Y + 1, pt1.Z);
            //    XYZ dimPoint2 = new XYZ(pt2.X, pt2.Y + 1, pt2.Z);
            //    Line dimLine = Line.CreateBound(dimPoint1, dimPoint2);

            //    Dimension dim = doc.Create.NewDimension(activeView, dimLine, refArray);

            //    tx.Commit();
            //}

            return Result.Succeeded;
        }



        private void GGG(ExternalCommandData commandData)
        {
            UIDocument uiDoc = commandData.Application.ActiveUIDocument;
            Document doc = uiDoc.Document;
            Selection selection = uiDoc.Selection;

            IList<Reference> eRef = selection.PickObjects(ObjectType.Element, "框選範圍");

            List<ElementId> data = (from x in eRef select x.ElementId).ToList();
            StringBuilder sb = new StringBuilder();
            foreach (ElementId eid in data)
            {
                Element e = doc.GetElement(eid);
                sb.Append($"{e.Name}" + Environment.NewLine);
            }
            TaskDialog.Show("title : ", sb.ToString());
        }
    }
}
