using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using CMDtest.Parameters;
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
            //try
            //{
            //    DimX(data, uidoc, doc);
            //}
            //catch (Exception) { }
            //try
            //{
            //    DimY(data, uidoc, doc);
            //}
            //catch (Exception) { }
            //try
            //{
            //    DimStuff(data, uidoc, doc);
            //}
            //catch (Exception) { }
            DimX(data, uidoc, doc);
            DimY(data, uidoc, doc);
            DimStuff(data, uidoc, doc);
            return Result.Succeeded;
        }

        public void DimX(List<ElementId> data, UIDocument uidoc, Document doc)
        {

            List<ElementInfo> sprinkler = new List<ElementInfo>();
            List<ElementInfo> accessories = new List<ElementInfo>();
            List<ElementInfo> pipes = new List<ElementInfo>();

            // 選取物件分類
            foreach (ElementId eid in data)
            {
                Element elem = doc.GetElement(eid);
                ElementInfo elementInfo = new ElementInfo();
                if (elem.Category.Name == "撒水頭")
                {
                    LocationPoint eleLocation = doc.GetElement(eid).Location as LocationPoint;
                    elementInfo.id = eid;
                    elementInfo.Type = elem.Category.Name;
                    elementInfo.point = new XYZ(eleLocation.Point.X, eleLocation.Point.Y, 0);
                    sprinkler.Add(elementInfo);
                }
                else if (elem.Category.Name == "管附件")
                {
                    LocationPoint eleLocation = doc.GetElement(eid).Location as LocationPoint;
                    elementInfo.id = eid;
                    elementInfo.Type = elem.Category.Name;
                    elementInfo.point = new XYZ(eleLocation.Point.X, eleLocation.Point.Y, 0);
                    accessories.Add(elementInfo);
                }
                else if (elem.Category.Name == "管")
                {
                    LocationCurve eleLocation = (elem as Pipe).Location as LocationCurve;
                    elementInfo.id = eid;
                    elementInfo.Type = elem.Category.Name;
                    elementInfo.Head_pipe = eleLocation.Curve.GetEndPoint(0);
                    elementInfo.End_pipe = eleLocation.Curve.GetEndPoint(1);
                    elementInfo.direction = pipeDirection(doc, eid).direction;
                    pipes.Add(elementInfo);
                }
            }

            // 標註軸最大(最外層)
            double MaxAxis = sprinkler.Distinct(p => p.point.Y.ToString("F3")).Max(o => o.point.Y);
            Dictionary<string, double> tempMap = new Dictionary<string, double>();

            // 最外層灑水頭
            List<ElementInfo> axisList = sprinkler.Where(c => c.point.Y.ToString("F3") == MaxAxis.ToString("F3")).ToList().OrderBy(x => x.point.X).ToList();

            // 管是否穿過兩灑水頭之間
            //foreach (var item in axisList)
            //{
            foreach (var p in pipes)
            {
                if (p.direction == "垂直")
                {
                    if (IsBetween(p.Head_pipe.Y, MaxAxis) || IsBetween(p.End_pipe.Y, MaxAxis))
                    {
                        var newP = new XYZ(p.Head_pipe.X, p.Head_pipe.Y, 0);
                        p.Head_pipe = newP;
                        p.point = newP;

                        axisList.Add(p);
                    }
                }
            }
            //}

            //axisList.OrderBy(x => x.point.X);
            var orderList = axisList.OrderBy(x => x.point.X).ToList();

            for (int i = 0; i < orderList.Count() - 1; i++)
            {
                double distance = Math.Round(orderList[i].point.DistanceTo(orderList[i + 1].point) * 30.48, 2);

                // 取得標註id及距離
                if (Convert.ToDecimal(distance) > 0)
                    tempMap.Add($"{orderList[i].id}_{orderList[i].Type}_{orderList[i + 1].id}_{orderList[i + 1].Type}_N", distance);
                if (orderList.Count() > 2 && i != 0 && i != orderList.Count() - 2 && orderList[i].Type == "管")
                {
                    tempMap.Add($"{orderList[i - 1].id}_{orderList[i - 1].Type}_{orderList[i + 1].id}_{orderList[i + 1].Type}_S", distance);
                }
            }

            var map = tempMap.OrderBy(o => o.Value).ToList();
           
            //List<string> dimed = new List<string>();
            foreach (var item in map)
            {
                //Log.Logger($"{item.Key} / {item.Value}", false);

                string[] obj = item.Key.Split('_');
                ElementId ele1 = null;
                ElementId ele2 = null;
                XYZ pt1 = null;
                XYZ pt2 = null;
                List<Reference> refElement = new List<Reference>();
                if (obj[1] == "管")
                {
                    ele1 = (from c in pipes where c.id.ToString() == obj[0] select c.id).FirstOrDefault();
                    refElement.Add(new Reference(doc.GetElement(ele1)));
                    pt1 = (from c in pipes where c.id.ToString() == obj[0] select c.Head_pipe).FirstOrDefault();
                }
                else if (obj[1] == "撒水頭")
                {
                    ele1 = (from c in sprinkler where c.id.ToString() == obj[0] select c.id).FirstOrDefault();
                    pt1 = sprinkler.Where(x => x.id == ele1).Select(x => x.point).FirstOrDefault();
                    refElement.Add(refType(doc.GetElement(ele1), "X"));
                }

                if (obj[3] == "管")
                {
                    ele2 = (from c in pipes where c.id.ToString() == obj[2] select c.id).FirstOrDefault();
                    refElement.Add(new Reference(doc.GetElement(ele2)));
                    pt2 = (from c in pipes where c.id.ToString() == obj[2] select c.Head_pipe).FirstOrDefault();
                }
                else if (obj[3] == "撒水頭")
                {
                    ele2 = (from c in sprinkler where c.id.ToString() == obj[2] select c.id).FirstOrDefault();
                    pt2 = sprinkler.Where(x => x.id == ele2).Select(x => x.point).FirstOrDefault();
                    refElement.Add(refType(doc.GetElement(ele2), "X"));
                }

                if (obj[1] == "管" && obj[3] == "撒水頭")
                {
                    pt1 = new XYZ(pt1.X, pt2.Y, 0);
                }
                else if (obj[3] == "管" && obj[1] == "撒水頭")
                {
                    pt2 = new XYZ(pt2.X, pt1.Y, 0);
                }

                if (pt1.X.ToString("F3") != pt2.X.ToString("F3"))
                {
                    bool special = false;
                    if (obj[4] == "S")
                    {
                        special = true;
                    }
                    DimByTwoXYZ(uidoc, pt1, pt2, refElement, special);
                }

            }
            //Log.Logger("", true);
        }
        public void DimY(List<ElementId> data, UIDocument uidoc, Document doc)
        {
            List<ElementInfo> sprinkler = new List<ElementInfo>();
            List<ElementInfo> accessories = new List<ElementInfo>();
            List<ElementInfo> pipes = new List<ElementInfo>();

            // 選取物件分類
            foreach (ElementId eid in data)
            {
                Element elem = doc.GetElement(eid);
                ElementInfo elementInfo = new ElementInfo();
                if (elem.Category.Name == "撒水頭")
                {
                    LocationPoint eleLocation = doc.GetElement(eid).Location as LocationPoint;
                    elementInfo.id = eid;
                    elementInfo.Type = elem.Category.Name;
                    elementInfo.point = new XYZ(eleLocation.Point.X, eleLocation.Point.Y, 0);
                    sprinkler.Add(elementInfo);
                }
                else if (elem.Category.Name == "管附件")
                {
                    LocationPoint eleLocation = doc.GetElement(eid).Location as LocationPoint;
                    elementInfo.id = eid;
                    elementInfo.Type = elem.Category.Name;
                    elementInfo.point = new XYZ(eleLocation.Point.X, eleLocation.Point.Y, 0);
                    accessories.Add(elementInfo);
                }
                else if (elem.Category.Name == "管")
                {
                    LocationCurve eleLocation = (elem as Pipe).Location as LocationCurve;
                    elementInfo.id = eid;
                    elementInfo.Type = elem.Category.Name;
                    elementInfo.Head_pipe = eleLocation.Curve.GetEndPoint(0);
                    elementInfo.End_pipe = eleLocation.Curve.GetEndPoint(1);
                    elementInfo.direction = pipeDirection(doc, eid).direction;
                    pipes.Add(elementInfo);
                }
            }

            // 標註軸最大(最外層)
            double MaxAxis = sprinkler.Distinct(p => p.point.X.ToString("F3")).Max(o => o.point.X);
            Dictionary<string, double> tempMap = new Dictionary<string, double>();

            // 最外層灑水頭
            List<ElementInfo> axisList = sprinkler.Where(c => c.point.X.ToString("F3") == MaxAxis.ToString("F3")).ToList().OrderBy(x => x.point.Y).ToList();
            int count = 0;
            // 主幹
            foreach (var p in pipes)
            {
                decimal diameter = decimal.Parse(doc.GetElement(p.id).LookupParameter("整體大小").AsString().TrimEnd('ø').TrimEnd('m').Trim());
                BoundingBoxXYZ boxXYZ = doc.GetElement(p.id).get_BoundingBox(doc.ActiveView);
                if (p.direction == "水平")
                {
                    Log.Logger($"{p.id} / {diameter} / {p.Head_pipe.X} / {MaxAxis} / {boxXYZ.Max} / {boxXYZ.Min}", false);
                    // 主幹尺寸
                    if (diameter >= 50 && boxXYZ.Max.X >= MaxAxis)
                    {
                        var newP = new XYZ(MaxAxis, p.Head_pipe.Y, 0);
                        p.Head_pipe = newP;
                        p.point = newP;
                        if (count < 1)
                        {
                            axisList.Add(p);
                            count++;
                        }
                    }
                }
            }

            //axisList.OrderBy(x => x.point.X);

            var orderList = axisList.OrderBy(x => x.point.Y).ToList();

            for (int i = 0; i < orderList.Count() - 1; i++)
            {
                double distance = Math.Round(orderList[i].point.DistanceTo(orderList[i + 1].point) * 30.48, 2);

                // 取得標註id及距離
                if (Convert.ToDecimal(distance) > 0)
                    tempMap.Add($"{orderList[i].id}_{orderList[i].Type}_{orderList[i + 1].id}_{orderList[i + 1].Type}_N", distance);

                Log.Logger($"{orderList.Count()} / {i} / {orderList[i].Type}", false);
                if (orderList.Count() > 2 && i != 0 && orderList[i].Type == "管")
                {
                    tempMap.Add($"{orderList[i - 1].id}_{orderList[i - 1].Type}_{orderList[i + 1].id}_{orderList[i + 1].Type}_S", distance);
                }
            }

            var map = tempMap.OrderBy(o => o.Value).ToList();

            foreach (var item in map)
            {
                Log.Logger($"{item.Key} / {item.Value}", false);
            }
            foreach (var item in map)
            {
                string[] obj = item.Key.Split('_');
                ElementId ele1 = null;
                ElementId ele2 = null;
                XYZ pt1 = null;
                XYZ pt2 = null;
                List<Reference> refElement = new List<Reference>();
                if (obj[1] == "管")
                {
                    ele1 = (from c in pipes where c.id.ToString() == obj[0] select c.id).FirstOrDefault();
                    refElement.Add(new Reference(doc.GetElement(ele1)));
                    pt1 = (from c in pipes where c.id.ToString() == obj[0] select c.Head_pipe).FirstOrDefault();
                }
                else if (obj[1] == "撒水頭")
                {
                    ele1 = (from c in sprinkler where c.id.ToString() == obj[0] select c.id).FirstOrDefault();
                    pt1 = sprinkler.Where(x => x.id == ele1).Select(x => x.point).FirstOrDefault();
                    refElement.Add(refType(doc.GetElement(ele1), "Y"));
                }

                if (obj[3] == "管")
                {
                    ele2 = (from c in pipes where c.id.ToString() == obj[2] select c.id).FirstOrDefault();
                    refElement.Add(new Reference(doc.GetElement(ele2)));
                    pt2 = (from c in pipes where c.id.ToString() == obj[2] select c.Head_pipe).FirstOrDefault();
                }
                else if (obj[3] == "撒水頭")
                {
                    ele2 = (from c in sprinkler where c.id.ToString() == obj[2] select c.id).FirstOrDefault();
                    pt2 = sprinkler.Where(x => x.id == ele2).Select(x => x.point).FirstOrDefault();
                    refElement.Add(refType(doc.GetElement(ele2), "Y"));
                }

                if (obj[1] == "管" && obj[3] == "撒水頭")
                {
                    pt1 = new XYZ(pt2.X, pt1.Y, 0);
                }
                else if (obj[3] == "管" && obj[1] == "撒水頭")
                {
                    pt2 = new XYZ(pt1.X, pt2.Y, 0);
                }

                bool special = false;
                if (obj[4] == "S")
                {
                    special = true;
                }
                Log.Logger($"{pt1} / {pt2}", false);
                DimByTwoXYZY(uidoc, pt1, pt2, refElement, special);
            }
            Log.Logger("", true);
        }
        public void DimStuff(List<ElementId> data, UIDocument uidoc, Document doc)
        {
            List<ElementInfo> accessories = new List<ElementInfo>();
            List<ElementInfo> pipes = new List<ElementInfo>();

            // 選取物件分類
            foreach (ElementId eid in data)
            {
                Element elem = doc.GetElement(eid);
                ElementInfo elementInfo = new ElementInfo();

                if (elem.Category.Name == "管附件")
                {
                    LocationPoint eleLocation = doc.GetElement(eid).Location as LocationPoint;
                    elementInfo.id = eid;
                    elementInfo.Type = elem.Category.Name;
                    elementInfo.point = new XYZ(eleLocation.Point.X, eleLocation.Point.Y, 0);
                    accessories.Add(elementInfo);
                }
                else if (elem.Category.Name == "管")
                {
                    LocationCurve eleLocation = (elem as Pipe).Location as LocationCurve;
                    elementInfo.id = eid;
                    elementInfo.Type = elem.Category.Name;
                    elementInfo.Head_pipe = eleLocation.Curve.GetEndPoint(0);
                    elementInfo.End_pipe = eleLocation.Curve.GetEndPoint(1);
                    elementInfo.direction = pipeDirection(doc, eid).direction;
                    pipes.Add(elementInfo);
                }
            }
            //List<double> MainPipeY = new List<double>();
            double MainPipeY = 0;
            // 主幹
            foreach (var p in pipes)
            {
                decimal diameter = decimal.Parse(doc.GetElement(p.id).LookupParameter("整體大小").AsString().TrimEnd('ø').TrimEnd('m').Trim());
                if (p.direction == "水平")
                {
                    // 主幹尺寸
                    if (diameter >= 50)
                    {
                        MainPipeY = p.Head_pipe.Y;
                    }
                }
            }
            List<ElementInfo> DimAccessories = new List<ElementInfo>();
            foreach (var item in accessories)
            {
                if (almost(item.point.Y, MainPipeY))
                {
                    DimAccessories.Add(item);
                }
            }

            var orderList = DimAccessories.OrderBy(x => x.point.X).ToList();
            Dictionary<string, double> tempMap = new Dictionary<string, double>();
            for (int i = 0; i < orderList.Count() - 1; i++)
            {
                double distance = Math.Round(orderList[i].point.DistanceTo(orderList[i + 1].point) * 30.48, 2);

                // 取得標註id及距離
                if (Convert.ToDecimal(distance) > 0)
                    tempMap.Add($"{orderList[i].id}_{orderList[i].Type}_{orderList[i + 1].id}_{orderList[i + 1].Type}", distance);
            }

            var map = tempMap.OrderBy(o => o.Value).ToList();

            foreach (var item in map)
            {
                string[] obj = item.Key.Split('_');
                ElementId ele1 = null;
                ElementId ele2 = null;
                XYZ pt1 = null;
                XYZ pt2 = null;
                List<Reference> refElement = new List<Reference>();
                ele1 = (from c in DimAccessories where c.id.ToString() == obj[0] select c.id).FirstOrDefault();
                pt1 = DimAccessories.Where(x => x.id == ele1).Select(x => x.point).FirstOrDefault();
                refElement.Add(refType(doc.GetElement(ele1), "Y"));

                ele2 = (from c in DimAccessories where c.id.ToString() == obj[2] select c.id).FirstOrDefault();
                pt2 = DimAccessories.Where(x => x.id == ele2).Select(x => x.point).FirstOrDefault();
                refElement.Add(refType(doc.GetElement(ele2), "Y"));

                DimByTwoXYZ(uidoc, pt1, pt2, refElement, false);
            }
            //Log.Logger("", true);

        }
        public bool DimByTwoXYZ(UIDocument uidoc, XYZ pt1, XYZ pt2, List<Reference> refElement, bool special)
        {
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

                double dimGap = 1;
                if (special)
                {
                    dimGap = 2.5;
                }

                // X軸標註位置
                if (Dimension_Parameters.AxisX == "上")
                {
                    dimPoint1 = new XYZ(pt1.X, pt1.Y + dimGap, pt1.Z);
                    dimPoint2 = new XYZ(pt2.X, pt2.Y + dimGap, pt2.Z);
                }
                else
                {
                    dimPoint1 = new XYZ(pt1.X, pt1.Y - dimGap, pt1.Z);
                    dimPoint2 = new XYZ(pt2.X, pt2.Y - dimGap, pt2.Z);
                }
                dimPoint1 = new XYZ(pt1.X, pt1.Y + dimGap, pt1.Z);
                dimPoint2 = new XYZ(pt2.X, pt2.Y + dimGap, pt2.Z);
                // Y軸標註位置
                if (pt1.X.ToString("F3") == pt2.X.ToString("F3"))
                {
                    if (Dimension_Parameters.AxisY == "左")
                    {
                        dimPoint1 = new XYZ(pt1.X - dimGap, pt1.Y, pt1.Z);
                        dimPoint2 = new XYZ(pt2.X - dimGap, pt2.Y, pt2.Z);
                    }
                    else
                    {
                        dimPoint1 = new XYZ(pt1.X + dimGap, pt1.Y, pt1.Z);
                        dimPoint2 = new XYZ(pt2.X + dimGap, pt2.Y, pt2.Z);
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
        public bool DimByTwoXYZY(UIDocument uidoc, XYZ pt1, XYZ pt2, List<Reference> refElement, bool special)
        {
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

                double dimGap = 1;
                if (special)
                {
                    dimGap = 2.5;
                }
                dimPoint1 = new XYZ(pt1.X + dimGap, pt1.Y, pt1.Z);
                dimPoint2 = new XYZ(pt2.X + dimGap, pt2.Y, pt2.Z);
                // Y軸標註位置
                //if (pt1.X.ToString("F3") == pt2.X.ToString("F3"))
                //{
                //    if (Dimension_Parameters.AxisY == "左")
                //    {
                //        dimPoint1 = new XYZ(pt1.X - dimGap, pt1.Y, pt1.Z);
                //        dimPoint2 = new XYZ(pt2.X - dimGap, pt2.Y, pt2.Z);
                //    }
                //    else
                //    {
                //    }
                //}

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
        private bool IsBetween(double A, double B)
        {
            if (Math.Abs(A - B) < 1 && !almost(A, B))
            {
                return true;
            }
            else
                return false;
        }
        private bool IsBetween(XYZ p1, XYZ p2, XYZ p3)
        {
            if (p1.X > p2.X)
            {
                if (p3.X > p2.X && p3.X < p1.X)
                {
                    return true;
                }
            }
            else
            {
                if (p3.X > p1.X && p3.X < p2.X)
                {
                    return true;
                }
            }
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
