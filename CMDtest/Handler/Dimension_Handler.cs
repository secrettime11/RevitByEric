using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using CMDtest.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDtest.Handler
{
    public class Dimension_Handler : IExternalEventHandler
    {
        public void Execute(UIApplication uiapp)
        {
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            Selection selection = uidoc.Selection;
            IList<Reference> eleSelecArea = new List<Reference>();
            try
            {
                // 選取標註範圍
                eleSelecArea = selection.PickObjects(ObjectType.Element, "框選範圍");
            }
            catch (Exception ee)
            {
                TaskDialog.Show("title", ee.Message);
                throw;
            }


            // 取出所有選中element id
            List<ElementId> data = (from x in eleSelecArea select x.ElementId).ToList();

            List<Model> coorList = new List<Model>();

            List<string> dimed = new List<string>();

            if (data != null)
            {
                foreach (ElementId eleId in data)
                {
                    LocationPoint eleLocation = doc.GetElement(eleId).Location as LocationPoint;
                    if (eleLocation != null)
                    {
                        XYZ coordinate = new XYZ(eleLocation.Point.X, eleLocation.Point.Y, 0);

                        // 加入符合條件的類型
                        if (Dimension_Parameters.DimType.Contains(doc.GetElement(eleId).Category.Name))
                        {
                            Model temp = new Model();
                            temp.elementId = eleId;
                            temp.coordinate = coordinate;
                            coorList.Add(temp);
                        }
                    }
                }

                if (Dimension_Parameters.Dibs == "X")
                {
                    Dim(uiapp, coorList, "X");
                    Dim(uiapp, coorList, "Y");
                }
                else
                {
                    Dim(uiapp, coorList, "Y");
                    Dim(uiapp, coorList, "X");
                }
                Log.Logger($"end", true);
            }
        }
        public bool DimByTwoXYZ(UIApplication uiapp, XYZ pt1, XYZ pt2, List<Reference> refElement)
        {
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;
            View activeView = uidoc.ActiveView;

            using (Transaction tx = new Transaction(doc))
            {
                tx.Start("Dimension mission");

                ReferenceArray refArray = new ReferenceArray();
                refArray.Append(refElement[0]);
                refArray.Append(refElement[1]);

                XYZ dimPoint1;
                XYZ dimPoint2;

                // X軸標註位置
                if (Dimension_Parameters.AxisX == "上")
                {
                    dimPoint1 = new XYZ(pt1.X, pt1.Y + 1, pt1.Z);
                    dimPoint2 = new XYZ(pt2.X, pt2.Y + 1, pt2.Z);
                }
                else
                {
                    dimPoint1 = new XYZ(pt1.X, pt1.Y - 1, pt1.Z);
                    dimPoint2 = new XYZ(pt2.X, pt2.Y - 1, pt2.Z);
                }

                // Y軸標註位置
                if (pt1.X.ToString("F3") == pt2.X.ToString("F3"))
                {
                    if (Dimension_Parameters.AxisY == "左")
                    {
                        dimPoint1 = new XYZ(pt1.X - 1, pt1.Y, pt1.Z);
                        dimPoint2 = new XYZ(pt2.X - 1, pt2.Y, pt2.Z);
                    }
                    else
                    {
                        dimPoint1 = new XYZ(pt1.X + 1, pt1.Y, pt1.Z);
                        dimPoint2 = new XYZ(pt2.X + 1, pt2.Y, pt2.Z);
                    }
                }

                Line dimLine = Line.CreateBound(dimPoint1, dimPoint2);
                try
                {
                    // Dimension
                    doc.Create.NewDimension(activeView, dimLine, refArray);
                    tx.Commit();
                    return true;
                }
                catch (Exception)
                {

                }
                return false;
            }
        }
        private Reference refType(Element element)
        {
            //if (element.Category.Name == "撒水頭")
            //{
            //    return (element as FamilyInstance).GetReferences(FamilyInstanceReferenceType.CenterLeftRight).FirstOrDefault();
            //}
            //else if (element.Category.Name == "管附件")
            //{
            //    return (element as FamilyInstance).GetReferences(FamilyInstanceReferenceType.CenterFrontBack).FirstOrDefault();
            //}
            //else if (element.Category.Name == "管配件")
            //{
            //    return (element as FamilyInstance).GetReferences(FamilyInstanceReferenceType.CenterLeftRight).FirstOrDefault();
            //}
            //else return null;
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
        private void Dim(UIApplication uiapp, List<Model> coorList, string axis)
        {
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            var info = coorList.Distinct(p => p.coordinate.Y.ToString("F3"));

            if (axis == "Y")
                info = coorList.Distinct(p => p.coordinate.X.ToString("F3"));

            Dictionary<string, double> tempMap = new Dictionary<string, double>();
            List<string> dimed = new List<string>();
            // 水平垂直
            foreach (var distinctItem in info)
            {
                // 座標軸小排到大
                var axisList = coorList.Where(c => c.coordinate.Y.ToString("F3") == distinctItem.coordinate.Y.ToString("F3")).ToList().OrderBy(x => x.coordinate.X).ToList();
                if (axis == "Y")
                    axisList = coorList.Where(c => c.coordinate.X.ToString("F3") == distinctItem.coordinate.X.ToString("F3")).ToList().OrderBy(x => x.coordinate.Y).ToList();

                for (int i = 0; i < axisList.Count(); i++)
                {
                    for (int j = i + 1; j < axisList.Count(); j++)
                    {
                        double distance = Math.Round(axisList[i].coordinate.DistanceTo(axisList[j].coordinate) * 30.48, 2);

                        if (Convert.ToDecimal(distance) > 0)
                            tempMap.Add($"{axisList[i].elementId}_{axisList[j].elementId}", distance);
                    }
                }
            }

            var map = tempMap.OrderBy(o => o.Value).ToList();

            foreach (var item in map)
            {
                //TaskDialog.Show("title", $"{item.Key} / {item.Value}");
                Log.Logger($"{item.Key} / {item.Value}", false);
                string[] id = item.Key.Split('_');
                if (!dimed.Contains(id[0]) && !dimed.Contains(id[1]))
                {
                    if (Convert.ToDecimal(item.Value) <= Dimension_Parameters.Range)
                    {
                        ElementId ele1 = (from c in coorList where c.elementId.ToString() == id[0] select c.elementId).FirstOrDefault();
                        ElementId ele2 = (from c in coorList where c.elementId.ToString() == id[1] select c.elementId).FirstOrDefault();
                        XYZ pt1 = coorList.Where(x => x.elementId == ele1).Select(x => x.coordinate).FirstOrDefault();
                        XYZ pt2 = coorList.Where(x => x.elementId == ele2).Select(x => x.coordinate).FirstOrDefault();
                        List<Reference> refElement = new List<Reference>();
                        //refElement.Add(refType(doc.GetElement(ele1), axis));
                        //refElement.Add(refType(doc.GetElement(ele2), axis));
                        refElement.Add(refType(doc.GetElement(ele1)));
                        refElement.Add(refType(doc.GetElement(ele2)));
                        if (DimByTwoXYZ(uiapp, pt1, pt2, refElement))
                        {
                            dimed.Add(id[0]);
                            dimed.Add(id[1]);
                        }
                    }
                }
            }

        }
        public string GetName()
        {
            return "Dimension_Handler";
        }
    }

}
