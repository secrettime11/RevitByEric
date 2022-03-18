using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CMDtest.Dim.Model;

namespace CMDtest.Dim
{
    public class Dim_handler : IExternalEventHandler
    {
        Document doc;
        public void Execute(UIApplication uiapp)
        {
            UIDocument uidoc = uiapp.ActiveUIDocument;
            doc = uidoc.Document;
            Selection sel = uidoc.Selection;

            using (Transaction tx = new Transaction(doc))
            {
                tx.Start("Dimension mission");
                IList<Reference> eleSelecArea = sel.PickObjects(ObjectType.Element, "框選標註範圍");
                List<ElementId> data = (from x in eleSelecArea select x.ElementId).ToList();
                if (ini.DimInfo.Contains("X"))
                    Dim(data, uidoc, "X");
                if (ini.DimInfo.Contains("Y"))
                    Dim(data, uidoc, "Y");
                if (ini.DimInfo.Contains("Hanger"))
                    DimHanger(data, uidoc);
                tx.Commit();
            }
            //Log.Logger("", true);
        }
        public void Dim(List<ElementId> data, UIDocument uidoc, string axis)
        {
            List<ElementInfo> sprinkler = new List<ElementInfo>();
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
                    elementInfo.name = elem.Name;
                    elementInfo.Type = elem.Category.Name;
                    elementInfo.point = new XYZ(eleLocation.Point.X, eleLocation.Point.Y, eleLocation.Point.Z);
                    sprinkler.Add(elementInfo);
                }
                else if (elem.Category.Name == "管")
                {
                    LocationCurve eleLocation = (elem as Pipe).Location as LocationCurve;
                    elementInfo.id = eid;
                    elementInfo.Type = elem.Category.Name;
                    elementInfo.pipe_BoxXYZ = doc.GetElement(eid).get_BoundingBox(doc.ActiveView);
                    pipes.Add(elementInfo);
                }
            }

            // 偵測到兩種以上的灑水頭
            var multipleSprinkler = sprinkler.Distinct(p => p.name).ToList();
            if (multipleSprinkler.Count > 1)
            {
                Dictionary<string, double> tmp = new Dictionary<string, double>();
                foreach (var item in multipleSprinkler)
                {
                    tmp.Add(item.name, item.point.Z);
                }
                var name = tmp.Aggregate((l, r) => l.Value >= r.Value ? l : r).Key;

                foreach (var item in sprinkler.Where(o => o.name != name).ToList())
                {
                    sprinkler.Remove(item);
                }
            }

            double MaxAxis;
            // 標註軸最大(最外層)
            if (axis == "X")
            {
                if (ini.BaseX == "up")
                    MaxAxis = sprinkler.Distinct(p => p.point.Y.ToString("F3")).Max(o => o.point.Y);
                else
                    MaxAxis = sprinkler.Distinct(p => p.point.Y.ToString("F3")).Min(o => o.point.Y);
            }
            else
            {
                if (ini.BaseY == "left")
                    MaxAxis = sprinkler.Distinct(p => p.point.X.ToString("F3")).Min(o => o.point.X);
                else
                    MaxAxis = sprinkler.Distinct(p => p.point.X.ToString("F3")).Max(o => o.point.X);
            }

            // 最外層灑水頭
            List<ElementInfo> BorderObj = new List<ElementInfo>();
            if (axis == "X")
                BorderObj = sprinkler.Where(c => almost(c.point.Y, MaxAxis, 0.1)).OrderBy(x => x.point.X).ToList();
            else
                BorderObj = sprinkler.Where(c => almost(c.point.X, MaxAxis, 0.1)).OrderBy(x => x.point.Y).ToList();

            //bool isHor = isDimensionHorizen(BorderObj);

            List<ElementInfo> passPipes = new List<ElementInfo>();
            // 管是否穿過兩灑水頭之間
            foreach (var pipe in pipes)
            {
                if (axis == "X")
                {
                    // 管為垂直
                    if (PipeDirection(pipe.id) == "V")
                    {
                        pipe.pipeAvg = (pipe.pipe_BoxXYZ.Max.X + pipe.pipe_BoxXYZ.Min.X) / 2;
                        if (IsBetween(pipe.pipe_BoxXYZ, MaxAxis, BorderObj, "X"))
                        {
                            passPipes.Add(pipe);
                        }
                    }
                }
                else
                {
                    // 管為平行
                    if (PipeDirection(pipe.id) == "H")
                    {
                        pipe.pipeAvg = (pipe.pipe_BoxXYZ.Max.Y + pipe.pipe_BoxXYZ.Min.Y) / 2;

                        if (IsBetween(pipe.pipe_BoxXYZ, MaxAxis, BorderObj, "Y"))
                        {
                            passPipes.Add(pipe);
                        }
                    }
                }
            }
            if (passPipes.Count() > 1)
            {
                for (int i = 0; i < passPipes.Count(); i++)
                {
                    int maxZ = i;
                    for (int j = i + 1; j < passPipes.Count(); j++)
                    {
                        // 上下管
                        if (almost(passPipes[i].pipeAvg, passPipes[j].pipeAvg, 0.5))
                        {
                            // 取高程較高的管
                            if (passPipes[j].pipe_BoxXYZ.Max.Z > passPipes[i].pipe_BoxXYZ.Max.Z)
                            {
                                maxZ = j;
                            }
                            // 移除另一隻高程較低相對管
                            else
                            {
                                passPipes.Remove(passPipes[j]);
                            }
                        }
                    }
                    passPipes[maxZ].point = new XYZ(passPipes[maxZ].pipe_BoxXYZ.Max.X, passPipes[maxZ].pipe_BoxXYZ.Max.Y, 0);
                    if (!BorderObj.Contains(passPipes[maxZ]))
                    {
                        BorderObj.Add(passPipes[maxZ]);
                    }
                }
            }
            else
            {
                if (passPipes.Count() != 0)
                {
                    foreach (var item in passPipes)
                    {
                        item.point = new XYZ(item.pipe_BoxXYZ.Max.X, item.pipe_BoxXYZ.Max.Y, 0);
                        if (!BorderObj.Contains(item))
                        {
                            BorderObj.Add(item);
                        }
                    }
                }
            }

            Dictionary<string, double> tmpResult = new Dictionary<string, double>();
            var orderList = BorderObj.OrderBy(x => x.point.X).ToList();
            if (axis == "Y")
                orderList = BorderObj.OrderByDescending(x => x.point.Y).ToList();


            for (int i = 0; i < orderList.Count() - 1; i++)
            {
                double distance = Math.Round(orderList[i].point.DistanceTo(orderList[i + 1].point) * 30.48, 2);

                // 取得標註id及距離
                if (Convert.ToDecimal(distance) > 0)
                    tmpResult.Add($"{orderList[i].id}_{orderList[i].Type}_{orderList[i + 1].id}_{orderList[i + 1].Type}_Normal", distance);
                // 特殊標註
                if (orderList[i].Type == "管")
                {
                    if (orderList[i - 1].Type == "撒水頭")
                    {
                        int index = i + 1;
                        bool find = false;
                        while (!find)
                        {
                            if (index == orderList.Count())
                            {
                                break;
                            }
                            if (orderList[index].Type == "撒水頭")
                            {
                                tmpResult.Add($"{orderList[i - 1].id}_{orderList[i - 1].Type}_{orderList[index].id}_{orderList[index].Type}_Special", distance);
                                find = true;
                            }
                            index++;
                        }
                    }
                }
            }
            var Result = tmpResult.OrderBy(o => o.Value).ToList();

            foreach (var item in Result)
            {
                //Log.Logger($"{item.Key} / {item.Value}", false);
                string[] obj = item.Key.Split('_');
                DimReference R1 = new DimReference();
                DimReference R2 = new DimReference();
                List<Reference> refElement = new List<Reference>();

                if (obj[1] == "管")
                    R1 = getDimInfo(obj[0], obj[1], pipes, axis);
                else
                    R1 = getDimInfo(obj[0], obj[1], sprinkler, axis);

                if (obj[3] == "管")
                    R2 = getDimInfo(obj[2], obj[3], pipes, axis);
                else
                    R2 = getDimInfo(obj[2], obj[3], sprinkler, axis);

                refElement.Add(R1.refElement);
                refElement.Add(R2.refElement);

                bool special = false;
                if (obj[4] == "Special")
                    special = true;

                DimensionXY(uidoc, R1.point, R2.point, refElement, special, axis, MaxAxis);
            }
        }
        public void DimHanger(List<ElementId> data, UIDocument uidoc)
        {
            List<ElementInfo> accessories = new List<ElementInfo>();
            List<ElementInfo> pipes = new List<ElementInfo>();

            // 選取物件分類
            foreach (ElementId eid in data)
            {
                Element elem = doc.GetElement(eid);
                ElementInfo elementInfo = new ElementInfo();

                if (elem.Name.Contains("管束") || elem.Name.Contains("吊架"))
                {
                    LocationPoint eleLocation = doc.GetElement(eid).Location as LocationPoint;
                    elementInfo.id = eid;
                    elementInfo.name = elem.Name;
                    elementInfo.Type = elem.Category.Name;
                    elementInfo.point = new XYZ(eleLocation.Point.X, eleLocation.Point.Y, 0);
                    accessories.Add(elementInfo);
                }
                else if (elem.Category.Name == "管")
                {
                    LocationCurve eleLocation = (elem as Pipe).Location as LocationCurve;
                    elementInfo.id = eid;
                    elementInfo.name = elem.Name;
                    elementInfo.Type = elem.Category.Name;
                    elementInfo.pipe_BoxXYZ = doc.GetElement(eid).get_BoundingBox(doc.ActiveView);
                    pipes.Add(elementInfo);
                }
            }

            List<ElementInfo> DimAccV = new List<ElementInfo>();
            List<ElementInfo> DimAccH = new List<ElementInfo>();

            // 找主幹上的管附件
            foreach (var p in pipes)
            {
                decimal diameter = decimal.Parse(doc.GetElement(p.id).LookupParameter("整體大小").AsString().TrimEnd('ø').TrimEnd('m').Trim());

                if (PipeDirection(p.id) == "V" && diameter >= 50)
                {
                    foreach (var accessory in accessories)
                    {
                        if (almost(accessory.point.X, (p.pipe_BoxXYZ.Max.X + p.pipe_BoxXYZ.Min.X) / 2, 0.1))
                        {
                            DimAccV.Add(accessory);
                        }
                    }
                }
                else if (PipeDirection(p.id) == "H" && diameter >= 50)
                {
                    foreach (var accessory in accessories)
                    {
                        if (almost(accessory.point.Y, (p.pipe_BoxXYZ.Max.Y + p.pipe_BoxXYZ.Min.Y) / 2, 0.1))
                        {
                            DimAccH.Add(accessory);
                        }
                    }
                }
            }

            if (DimAccH.Count > 0)
            {
                AccDimVH(uidoc, DimAccH, "H");
            }
            if (DimAccV.Count > 0)
            {
                AccDimVH(uidoc, DimAccV, "V");
            }
        }
        private void AccDimVH(UIDocument uidoc, List<ElementInfo> DimAcc, string type)
        {
            List<ElementInfo> orderList = new List<ElementInfo>();
            if (type == "V")
            {
                orderList = DimAcc.OrderBy(x => x.point.Y).ToList();
            }
            else
            {
                orderList = DimAcc.OrderBy(x => x.point.X).ToList();
            }

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
                ele1 = (from c in DimAcc where c.id.ToString() == obj[0] select c.id).FirstOrDefault();
                pt1 = DimAcc.Where(x => x.id == ele1).Select(x => x.point).FirstOrDefault();
                ele2 = (from c in DimAcc where c.id.ToString() == obj[2] select c.id).FirstOrDefault();
                pt2 = DimAcc.Where(x => x.id == ele2).Select(x => x.point).FirstOrDefault();

                if (type == "V")
                {
                    refElement.Add(refType(doc.GetElement(ele1)));
                    refElement.Add(refType(doc.GetElement(ele2)));
                    DimensionXY(uidoc, pt1, pt2, refElement, false, "Y", pt1.X);
                }
                else
                {
                    refElement.Add(refType(doc.GetElement(ele1)));
                    refElement.Add(refType(doc.GetElement(ele2)));
                    DimensionXY(uidoc, pt1, pt2, refElement, false, "X", pt1.Y);
                }
            }
        }
        private DimReference getDimInfo(string id, string type, List<ElementInfo> db, string axis)
        {
            DimReference dimReference = new DimReference();
            ElementId eid = (from c in db where c.id.ToString() == id select c.id).FirstOrDefault();
            dimReference.elementId = eid;
            dimReference.point = db.Where(x => x.id == eid).Select(x => x.point).FirstOrDefault();

            if (type == "管")
            {
                dimReference.refElement = new Reference(doc.GetElement(eid));
            }
            else if (type == "撒水頭")
            {
                dimReference.point = new XYZ(dimReference.point.X, dimReference.point.Y, 0);
                dimReference.refElement = refBaba(doc.GetElement(eid), axis);
            }
            return dimReference;
        }
        public void DimensionXY(UIDocument uidoc, XYZ pt1, XYZ pt2, List<Reference> refElement, bool special, string axis, double MaxAxis)
        {
            View activeView = uidoc.ActiveView;


            ReferenceArray refArray = new ReferenceArray();
            refArray.Append(refElement[0]);
            refArray.Append(refElement[1]);

            XYZ dimPoint1 = null;
            XYZ dimPoint2 = null;

            double dimGap = 1;
            if (special)
                dimGap = 2.5;

            if (axis == "X")
            {
                if (ini.BaseX == "up")
                {
                    dimPoint1 = new XYZ(pt1.X, MaxAxis + dimGap, pt1.Z);
                    dimPoint2 = new XYZ(pt2.X, MaxAxis + dimGap, pt2.Z);
                }
                else
                {
                    dimGap = 1.5;
                    if (special)
                        dimGap = 3;

                    dimPoint1 = new XYZ(pt1.X, MaxAxis - dimGap, pt1.Z);
                    dimPoint2 = new XYZ(pt2.X, MaxAxis - dimGap, pt2.Z);
                }
            }
            else if (axis == "Y")
            {
                if (ini.BaseY == "right")
                {
                    dimPoint1 = new XYZ(MaxAxis + dimGap, pt1.Y, pt1.Z);
                    dimPoint2 = new XYZ(MaxAxis + dimGap, pt2.Y, pt2.Z);
                }
                else
                {
                    dimGap = 1.5;
                    if (special)
                        dimGap = 3;

                    dimPoint1 = new XYZ(MaxAxis - dimGap, pt1.Y, pt1.Z);
                    dimPoint2 = new XYZ(MaxAxis - dimGap, pt2.Y, pt2.Z);
                }
            }

            Line dimLine = Line.CreateBound(dimPoint1, dimPoint2);
            try
            {

                DimensionType defaultType = (from v in new FilteredElementCollector(doc)
                                                    .OfClass(typeof(DimensionType))
                                                    .Cast<DimensionType>()
                                             where v.Name == ini.DimType
                                             select v).First();
                //Log.Logger(ini.DimType, false);
                Dimension dimension = doc.Create.NewDimension(activeView, dimLine, refArray);
                dimension.DimensionType = defaultType;

            }
            catch (Exception) { }
        }

        private bool IsBetween(BoundingBoxXYZ pipe, double maxAxis, List<ElementInfo> BorderObj, string axis)
        {
            if (axis == "X")
            {
                XYZ xYZA = new XYZ(0, pipe.Max.Y, 0);
                XYZ xYZB = new XYZ(0, pipe.Min.Y, 0);
                double distance = Math.Round(xYZA.DistanceTo(xYZB) * 30.48, 2);
                bool sameDir = false;
                foreach (var item in BorderObj)
                {
                    // 灑水頭和管是否在同一軸上
                    if (almost(item.point.X, (pipe.Max.X + pipe.Min.X) / 2, 0.1))
                    {
                        sameDir = true;
                    }
                }

                if ((Math.Abs(pipe.Max.Y - maxAxis) < 1 || Math.Abs(pipe.Min.Y - maxAxis) < 1) && distance >= 20 && !sameDir)
                {
                    return true;
                }
                else if (pipe.Max.Y >= maxAxis && ini.BaseX == "up" && !sameDir)
                {
                    return true;
                }
                else if (pipe.Min.Y <= maxAxis && ini.BaseX == "down" && !sameDir)
                {
                    return true;
                }
                else
                    return false;
            }
            else
            {
                XYZ xYZA = new XYZ(pipe.Max.X, 0, 0);
                XYZ xYZB = new XYZ(pipe.Min.X, 0, 0);
                double distance = Math.Round(xYZA.DistanceTo(xYZB) * 30.48, 2);
                bool sameDir = false;
                foreach (var item in BorderObj)
                {
                    if (almost(item.point.Y, (pipe.Max.Y + pipe.Min.Y) / 2, 0.1))
                    {
                        sameDir = true;
                    }
                }

                if ((Math.Abs(pipe.Max.X - maxAxis) < 1 || Math.Abs(pipe.Min.X - maxAxis) < 1) && distance >= 20 && !sameDir)
                {
                    return true;
                }
                else if (pipe.Max.X >= maxAxis && ini.BaseY == "right" && !sameDir)
                {
                    return true;
                }
                else if (pipe.Min.X <= maxAxis && ini.BaseY == "left" && !sameDir)
                {
                    return true;
                }
                else
                    return false;
            }
        }
        private bool almost(double A, double B, double val)
        {
            if (Math.Abs(A - B) < val)
            {
                return true;
            }
            else
                return false;
        }
        /// <summary>
        /// 判斷管方向 V / H / N
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="doc"></param>
        /// <returns></returns>
        private string PipeDirection(ElementId pid)
        {
            Pipe pipe = doc.GetElement(pid) as Pipe;
            LocationCurve locate = pipe.Location as LocationCurve;
            var Head = locate.Curve.GetEndPoint(0);
            var Tail = locate.Curve.GetEndPoint(1);
            if (almost(Head.X, Tail.X, 0.1))
                return "V";
            else if (almost(Head.Y, Tail.Y, 0.1))
                return "H";
            else
                return "N";
        }
        private Reference refType(Element element)
        {
            return (element as FamilyInstance).GetReferences(FamilyInstanceReferenceType.CenterFrontBack).FirstOrDefault();
        }
        private Reference refType(Element element, string axis)
        {
            var refobj = element as FamilyInstance;
            var faceori = refobj.FacingOrientation;
            var handori = refobj.HandOrientation;
            if ((faceori.X < 0 && equalzero(faceori.Y) && equalzero(faceori.Z) && equalzero(handori.X) && handori.Y >= 1 && equalzero(handori.Z)) || (faceori.X >= 1 && equalzero(faceori.Y) && equalzero(faceori.Z) && equalzero(handori.X) && handori.Y < 0 && equalzero(handori.Z)))
            {

                if (axis == "X")
                    return refobj.GetReferences(FamilyInstanceReferenceType.CenterFrontBack).FirstOrDefault();
                else
                    return refobj.GetReferences(FamilyInstanceReferenceType.CenterLeftRight).FirstOrDefault();

                //if (axis == "X")
                //    return refobj.GetReferences(FamilyInstanceReferenceType.CenterLeftRight).FirstOrDefault();
                //else
                //    return refobj.GetReferences(FamilyInstanceReferenceType.CenterFrontBack).FirstOrDefault();
            }
            else
            {
                if (axis == "X")
                    return refobj.GetReferences(FamilyInstanceReferenceType.CenterLeftRight).FirstOrDefault();
                else
                    return refobj.GetReferences(FamilyInstanceReferenceType.CenterFrontBack).FirstOrDefault();

                //if (axis == "X")
                //    return refobj.GetReferences(FamilyInstanceReferenceType.CenterFrontBack).FirstOrDefault();
                //else
                //    return refobj.GetReferences(FamilyInstanceReferenceType.CenterLeftRight).FirstOrDefault();
            }
        }
        private Reference refBaba(Element element, string axis)
        {
            bool isHor = true;
            if (axis == "Y")
            {
                isHor = false;
            }
            var familyInstance = element as FamilyInstance;
            
            double rotation = (familyInstance.Location as LocationPoint).Rotation;
            // 是否旋轉180
            bool phare = Math.Round(rotation / (0.5 * Math.PI) % 2, 4) == 2 || Math.Round(rotation / (0.5 * Math.PI) % 2, 4) == 0;
            IList<Reference> refs = isHor ^ phare == false ? familyInstance.GetReferences(FamilyInstanceReferenceType.CenterLeftRight)
                    : familyInstance.GetReferences(FamilyInstanceReferenceType.CenterFrontBack);

            //Log.Logger($"{element.Id} / {axis} / {rotation} / {Math.Round(rotation / (0.5 * Math.PI) % 2, 4)} / {isHor ^ phare}/{refs.Count}",false);

            return refs.Count == 0 ? null : refs[0];
        }
        public bool isDimensionHorizen(List<ElementInfo> BorderObj)
        {
            if (
                Math.Abs
                (
                   BorderObj.OrderBy(f => f.point.X).Select(f => f.point.X).First()
                 - BorderObj.OrderByDescending(f => f.point.X).Select(f => f.point.X).First()
                )
                >
                Math.Abs
                (
                      BorderObj.OrderBy(f => f.point.Y).Select(f => f.point.Y).First()
                 - BorderObj.OrderByDescending(f => f.point.Y).Select(f => f.point.Y).First()
                 )
                )
            {
                return true;
            }
            return false;

        }
        private bool equalzero(double val)
        {
            if (val >= 0 && val <= 0.005)
            {
                return true;
            }
            return false;
        }
        public string GetName()
        {
            return "Dim_handler";
        }

    }

}
