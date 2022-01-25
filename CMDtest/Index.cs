using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using View = Autodesk.Revit.DB.View;

namespace CMDtest
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class Index : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string msg, ElementSet elemSet)
        {
            //SpecialDim(commandData);
            return Result.Succeeded;
        }

        /// <summary>
        /// 單標
        /// </summary>
        public void Dim0121(ExternalCommandData commandData)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            View activeView = uidoc.ActiveView;


            Selection selection = commandData.Application.ActiveUIDocument.Selection;
            //IList<Reference> eRef = selection.PickObjects(ObjectType.Face, "框選範圍");
            ReferenceArray refArray = new ReferenceArray();
            Reference reference1 = selection.PickObject(ObjectType.PointOnElement);
            Reference reference2 = selection.PickObject(ObjectType.PointOnElement);

            XYZ p1 = reference1.GlobalPoint;
            XYZ p2 = reference2.GlobalPoint;

            Line line = Line.CreateBound(p1, p2);

            Element element1 = doc.GetElement(reference1);
            Element element2 = doc.GetElement(reference2);

            LocationPoint locationPoint1 = element1.Location as LocationPoint;
            LocationPoint locationPoint2 = element2.Location as LocationPoint;

            XYZ pt1 = locationPoint1.Point;
            XYZ pt2 = locationPoint2.Point;

            pt1 = new XYZ(pt1.X, pt1.Y, 0);
            pt2 = new XYZ(pt2.X, pt2.Y, 0);

            refArray.Append(line.GetEndPointReference(0));
            refArray.Append(line.GetEndPointReference(1));

            using (Transaction tx = new Transaction(doc))
            {
                tx.Start("tx");

                XYZ dimPoint1 = new XYZ(pt1.X, pt1.Y + 1, pt1.Z);
                XYZ dimPoint2 = new XYZ(pt2.X, pt2.Y + 1, pt2.Z);
                Line dimLine = Line.CreateBound(dimPoint1, dimPoint2);

                Dimension dim = doc.Create.NewDimension(activeView, dimLine, refArray);

                tx.Commit();
            }
        }
        /// <summary>
        /// 特別標
        /// </summary>
        /// <param name="commandData"></param>
        public void SpecialDim(ExternalCommandData commandData) 
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;

            Document doc = uidoc.Document;

            using (Transaction tx = new Transaction(doc))
            {
                tx.Start("BIM");

                Reference refer = uidoc.Selection.PickObject(ObjectType.Element, "Pick first obj");

                FamilyInstance instance = doc.GetElement(refer) as FamilyInstance;

                LocationPoint location = instance.Location as LocationPoint;

                XYZ xyz = location.Point;

                Reference reference = GetSpecialFamilyReference(doc, instance, SpecialReferenceType.CenterFB);

                Reference refer2 = uidoc.Selection.PickObject(ObjectType.Element, "Pick second obj");
                FamilyInstance instance2 = doc.GetElement(refer2) as FamilyInstance;

                LocationPoint location2 = instance2.Location as LocationPoint;

                XYZ xyz2 = location2.Point;

                Reference reference2 = GetSpecialFamilyReference(doc, instance2, SpecialReferenceType.CenterFB);

                XYZ dimPoint1 = new XYZ(xyz.X, xyz.Y + 1, 0);
                XYZ dimPoint2 = new XYZ(xyz2.X, xyz2.Y + 1, 0);
                Line dimLine = Line.CreateBound(dimPoint1, dimPoint2);

                ReferenceArray refarray = new ReferenceArray();

                refarray.Append(reference);

                refarray.Append(reference2);

                doc.Create.NewDimension(doc.ActiveView, dimLine, refarray);

                tx.Commit();
            }
        }
        public void DimByTwoXYZ(ExternalCommandData commandData, XYZ pt1, XYZ pt2)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;
            View activeView = uidoc.ActiveView;

            pt1 = new XYZ(pt1.X, pt1.Y, 0);
            pt2 = new XYZ(pt2.X, pt2.Y, 0);

            Line geomLine = Line.CreateBound(pt1, pt2);
            using (Transaction tx = new Transaction(doc))
            {
                tx.Start("tx");

                DetailLine line = doc.Create.NewDetailCurve(activeView, geomLine) as DetailLine;

                ReferenceArray refArray = new ReferenceArray();
                refArray.Append(line.GeometryCurve.GetEndPointReference(0));
                refArray.Append(line.GeometryCurve.GetEndPointReference(1));

                XYZ dimPoint1 = new XYZ(pt1.X, pt1.Y + 1, pt1.Z);
                XYZ dimPoint2 = new XYZ(pt2.X, pt2.Y + 1, pt2.Z);
                Line dimLine = Line.CreateBound(dimPoint1, dimPoint2);

                Dimension dim = doc.Create.NewDimension(activeView, dimLine, refArray);
                //doc.Delete(dim.Id);
                tx.Commit();
            }
        }
        Dimension CreateNewDimensionAlongLine(Autodesk.Revit.DB.Document document, Line line)
        {
            ReferenceArray references = new ReferenceArray();
            references.Append(line.GetEndPointReference(0));
            references.Append(line.GetEndPointReference(1));

            Dimension dimension = document.Create.NewDimension(document.ActiveView,
                                                                line, references);
            return dimension;
        }

        public enum SpecialReferenceType
        {
            Left = 0,
            CenterLR = 1,
            Right = 2,
            Front = 3,
            CenterFB = 4,
            Back = 5,
            Bottom = 6,
            CenterElevation = 7,
            Top = 8
        }
        public static Reference GetSpecialFamilyReference(Document doc, FamilyInstance instance, SpecialReferenceType ReferenceType)
        {
            Reference indexReference = null;

            int index = (int)ReferenceType;

            Options geomOptions = new Options();

            geomOptions.ComputeReferences = true;

            geomOptions.DetailLevel = ViewDetailLevel.Fine;

            geomOptions.IncludeNonVisibleObjects = true;

            GeometryElement geoElement = instance.get_Geometry(geomOptions);

            foreach (GeometryObject obj in geoElement)
            {
                if (obj is GeometryInstance)
                {
                    GeometryInstance geoInstance = obj as GeometryInstance;
                    String sampleStableRef = null;
                    if (geoInstance != null)
                    {
                        GeometryElement geoSymbol = geoInstance.GetSymbolGeometry();
                        if (geoSymbol != null)
                        {
                            foreach (GeometryObject geomObj in geoSymbol)
                            {
                                if (geomObj is Solid)
                                {
                                    Solid solid = geomObj as Solid;

                                    if (solid.Faces.Size > 0)
                                    {
                                        Face face = solid.Faces.get_Item(0);
                                        sampleStableRef = face.Reference.ConvertToStableRepresentation(doc);
                                        break;
                                    }
                                }
                            }
                        }

                        if (sampleStableRef != null)
                        {
                            String[] refTokens = sampleStableRef.Split(new char[] { ':' });
                            
                            String customStableRef = refTokens[0] + ":" + refTokens[1] + ":" + refTokens[2] + ":" + refTokens[3] + ":" + index.ToString();

                            indexReference = Reference.ParseFromStableRepresentation(doc, customStableRef);
                        }
                        else
                        {
                           
                        }
                        break;
                    }
                    else
                    {

                    }
                }
            }
            return indexReference;
        }
        private ReferenceArray GetEndPlanRefs(GeometryElement geoele)
        {
            var result = new ReferenceArray();

            var geometrys = geoele.Cast<GeometryObject>().ToList();

            foreach (GeometryObject geo in geometrys)
            {
                if (geo is Solid so)
                {
                    var faces = so.Faces;
                    foreach (var face in faces)
                    {
                        if (face is PlanarFace pface)
                        {
                            result.Append(pface.Reference);
                        }
                    }
                }
                else
                    continue;
            }

            return result;
        }

        /// <summary>
        /// 取得家族實體ID
        /// </summary>
        /// <param name="doc"></param>
        private void GetFamilyInstanceId(Document doc)
        {
            List<Element> myListFamily = new FilteredElementCollector(doc).WherePasses(new ElementClassFilter(typeof(FamilyInstance))).ToList();
            foreach (var item in myListFamily)
            {
                Log.Logger($"{item.Category.Name} / {item.Name} / {item.Id}", false);
            }
            Log.Logger($"end", true);
        }
        /// <summary>
        /// 取得管ID
        /// </summary>
        /// <param name="doc"></param>
        private void GetPipeInstanceId(Document doc)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            collector.OfCategory(BuiltInCategory.OST_PipeCurves);
            foreach (var item in collector.ToElements())
            {
                Log.Logger($"{item.Category.Name} / {item.Name} / {item.Id}", false);
            }
            Log.Logger($"end", true);
        }
        /// <summary>
        /// 標註管 / 網
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="uiDoc"></param>
        private void MarkPipe(Document doc, UIDocument uiDoc)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc, doc.ActiveView.Id);
            ElementCategoryFilter filter1 = new ElementCategoryFilter(BuiltInCategory.OST_Grids);
            ElementCategoryFilter filter2 = new ElementCategoryFilter(BuiltInCategory.OST_PipeCurves);
            LogicalOrFilter orFilter = new LogicalOrFilter(filter1, filter2);
            collector.WherePasses(orFilter);

            ReferenceArray refArrayX = new ReferenceArray();
            ReferenceArray refArrayY = new ReferenceArray();
            XYZ xDir = new XYZ(1, 0, 0);
            XYZ yDir = new XYZ(0, 1, 0);

            double minY = double.MaxValue;
            double maxY = double.MinValue;
            XYZ minPoint = null;
            XYZ maxPoint = null;

            double minX = double.MaxValue;
            double maxX = double.MinValue;
            XYZ minPointX = null;
            XYZ maxPointX = null;
            foreach (Element elem in collector.ToElements())
            {
                //if (elem is Grid)
                //{
                //    Grid grid = elem as Grid;
                //    Line gLine = grid.Curve as Line;
                //    XYZ gDir = gLine.Direction;

                //    if (gDir.IsAlmostEqualTo(xDir) || gDir.IsAlmostEqualTo(-xDir))
                //    {
                //        XYZ startPoint = gLine.GetEndPoint(0);
                //        double y = startPoint.Y;
                //        if (y < minY)
                //        {
                //            minY = y;
                //            minPoint = startPoint;
                //        }
                //        else if (y > maxY)
                //        {
                //            maxY = y;
                //            maxPoint = startPoint;
                //        }
                //        refArrayX.Append(new Reference(elem));
                //    }
                //    else if (gDir.IsAlmostEqualTo(yDir) || gDir.IsAlmostEqualTo(-yDir))
                //    {
                //        XYZ startPoint = gLine.GetEndPoint(0);
                //        double x = startPoint.X;
                //        if (x < minX)
                //        {
                //            minX = x;
                //            minPointX = startPoint;
                //        }
                //        else if (x > maxX)
                //        {
                //            maxX = x;
                //            maxPointX = startPoint;
                //        }
                //        refArrayY.Append(new Reference(elem));
                //    }
                //}
                //else
                if (elem is Pipe)
                {
                    Pipe pipe = elem as Pipe;
                    Line pLine = (pipe.Location as LocationCurve).Curve as Line;
                    XYZ pDir = pLine.Direction;
                    if (pDir.IsAlmostEqualTo(xDir) || pDir.IsAlmostEqualTo(-xDir))
                    {
                        XYZ startPoint = pLine.GetEndPoint(0);
                        double y = startPoint.Y;
                        if (y < minY)
                        {
                            minY = y;
                            minPoint = startPoint;
                        }
                        else if (y > maxY)
                        {
                            maxY = y;
                            maxPoint = startPoint;
                        }
                        refArrayX.Append(new Reference(elem));
                    }
                    else if (pDir.IsAlmostEqualTo(yDir) || pDir.IsAlmostEqualTo(-yDir))
                    {

                        XYZ startPoint = pLine.GetEndPoint(0);
                        double x = startPoint.X;
                        if (x < minX)
                        {
                            minX = x;
                            minPointX = startPoint;
                        }
                        else if (x > maxX)
                        {
                            maxX = x;
                            maxPointX = startPoint;
                        }
                        refArrayY.Append(new Reference(elem));
                    }
                }
            }

            // 拾取一个点，基于该点的X、Y放置标注
            XYZ selectPoint = uiDoc.Selection.PickPoint(ObjectSnapTypes.None);

            XYZ yPoint1 = new XYZ(selectPoint.X, minY, 0);
            XYZ yPoint2 = new XYZ(selectPoint.X, maxY, 0);
            Line yLine = Line.CreateBound(yPoint1, yPoint2);

            XYZ xPoint1 = new XYZ(minX, selectPoint.Y, 0);
            XYZ xPoint2 = new XYZ(maxX, selectPoint.Y, 0);
            Line xLine = Line.CreateBound(xPoint1, xPoint2);
            using (Transaction trans = new Transaction(doc))
            {
                trans.Start("Create Dimension");
                doc.Create.NewDimension(doc.ActiveView, yLine, refArrayX);

                doc.Create.NewDimension(doc.ActiveView, xLine, refArrayY);
                trans.Commit();
            }
        }
        /// <summary>
        /// 快速標註 (單一obj)
        /// </summary>
        /// <param name="commandData"></param>
        private void QuickMark(ExternalCommandData commandData)
        {
            UIDocument uiDoc = commandData.Application.ActiveUIDocument;
            Document doc = uiDoc.Document;

            Face face1 = null;
            Face face2 = null;
            Reference reference = uiDoc.Selection.PickObject(ObjectType.Element);
            Element elem = doc.GetElement(reference.ElementId);
            Options options = new Options();
            options.View = uiDoc.ActiveView;
            options.ComputeReferences = true;
            GeometryElement geometryElement = elem.get_Geometry(options);
            foreach (GeometryObject gObj in geometryElement)
            {
                Solid solid = gObj as Solid;
                foreach (Face face in solid.Faces)
                {

                    XYZ normal = face.ComputeNormal(new UV(0, 0));
                    if (Math.Abs(normal.X) > 0.1)
                    {
                        if (normal.X > 0.1)
                        {
                            face1 = face;
                        }
                        else
                        {
                            face2 = face;
                        }
                    }
                }
            }

            if (face1 != null && face2 != null)
            {
                Transaction tran = new Transaction(doc, "Create Dimension");
                tran.Start();
                XYZ p1 = face1.Evaluate(new UV(0, 0));
                XYZ p2 = face2.Project(p1).XYZPoint;
                int bias = 1;
                p1 = new XYZ(p1.X, p1.Y + bias, p1.Z);
                p2 = new XYZ(p2.X, p2.Y + bias, p2.Z);
                Line line = Line.CreateBound(p1, p2);

                ReferenceArray referenceArray = new ReferenceArray();
                referenceArray.Append(face1.Reference);
                referenceArray.Append(face2.Reference);
                doc.Create.NewDimension(uiDoc.ActiveView, line, referenceArray);
                tran.Commit();
            }
        }
        private void MarkObj(ExternalCommandData commandData)
        {
            UIApplication uiApp = commandData.Application;
            Document doc = uiApp.ActiveUIDocument.Document;
            Selection sel = uiApp.ActiveUIDocument.Selection;
            Reference obj = sel.PickObject(ObjectType.Element);

            Element e = doc.GetElement(obj.ElementId);
            Options opt = new Options();
            opt.ComputeReferences = true; //別忘了此步驟
            opt.DetailLevel = ViewDetailLevel.Medium;
            GeometryElement geoEle = e.get_Geometry(opt);
            foreach (GeometryObject geoObj in geoEle)
            {
                GeometryInstance instance = geoObj as GeometryInstance;
                if (instance != null)
                {
                    var instanceTransform = instance.Transform;

                    foreach (GeometryObject insObj in instance.SymbolGeometry)
                    {
                        Solid solid = insObj as Solid;

                        foreach (Face f in solid.Faces)
                        {
                            if (f.ComputeNormal(new UV(0.5, 0.5)).IsAlmostEqualTo(XYZ.BasisZ.Negate())) //get bottom face
                            {
                                foreach (EdgeArray edgeArr in f.EdgeLoops)
                                {
                                    foreach (Edge edge in edgeArr)
                                    {
                                        ReferenceArray refArray = new ReferenceArray();
                                        refArray.Append(edge.GetEndPointReference(0));
                                        refArray.Append(edge.GetEndPointReference(1));
                                        // 取得邊的法向量(normal vector)
                                        XYZ offsetVec = instanceTransform.OfVector(edge.Evaluate(0.5)).Normalize();
                                        // 將點座標依照轉換為全域坐標系(Local transform to global transform)
                                        XYZ p1 = instanceTransform.OfPoint(edge.Evaluate(0)) + offsetVec;
                                        // 將點座標依照轉換為全域坐標系(Local transform to global transform)
                                        XYZ p2 = instanceTransform.OfPoint(edge.Evaluate(1)) + offsetVec;
                                        Line line = Line.CreateBound(p1, p2);
                                        doc.Create.NewDimension(doc.ActiveView, line, refArray);
                                    }
                                }
                            }
                        }
                    }
                }
            }

        }
        private View3D Get3DView(Document doc)
        {
            FilteredElementCollector collector
              = new FilteredElementCollector(doc);

            collector.OfClass(typeof(View3D));

            foreach (View3D v in collector)
            {
                // skip view templates here because they
                // are invisible in project browsers:

                if (v != null && !v.IsTemplate && v.Name == "{3D}")
                {
                    return v;
                }
            }
            return null;
        }
        public static int toColor(Autodesk.Revit.DB.Color color)
        {
            int rgb = 0;
            rgb += color.Red * (int)Math.Pow(256, 0);
            rgb += color.Green * (int)Math.Pow(256, 0);
            rgb += color.Blue * (int)Math.Pow(256, 0);
            return rgb;
        }

        public Line getLine(Reference reference, Document doc)
        {
            Line line = null;
            Element element = doc.GetElement(reference);
            Wall wall = element as Wall;
            if (wall != null)
            {
                Location location = wall.Location;
                LocationCurve locationCurve = location as LocationCurve;
                if (locationCurve != null)
                {
                    XYZ p1 = locationCurve.Curve.GetEndPoint(0);
                    XYZ p2 = locationCurve.Curve.GetEndPoint(1);
                    XYZ wallXL = p1.Subtract(p2);//获得墙体的向量
                    XYZ wallunitXL = wallXL.Normalize();//获得墙体向量的单位向量
                    XYZ shituXL = doc.ActiveView.ViewDirection;//获得当前视图的法向量，也就是（0，0，1）因为这个试图现阶段是平面的
                                                               //根据三维图形的几何属性，两个向量的乘积是得到的向量是垂直于这个面的法向量
                                                               //根据这个思路，墙体的向量 和视图的法向量，得到的是垂直于墙体的法向量，因此
                    XYZ wallnorXL = shituXL.CrossProduct(wallXL);//得到垂直于墙体的法向量
                    XYZ wallnorUnitXL = wallnorXL.Normalize();//得到垂直于墙体的法向量的单位向量
                    XYZ wallnorUnitxl3 = wallnorUnitXL.Multiply(3);//获得三倍距离稍微远点，距离墙体的距离，这个数据越大，标注距离墙体越远。
                    p1 = p1.Add(wallnorUnitxl3);
                    p2 = p2.Add(wallnorUnitxl3);
                    line = Line.CreateBound(p1, p2);
                }
            }
            return line;
        }

        public void QuickDimen(ExternalCommandData commandData)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Autodesk.Revit.ApplicationServices.Application app = commandData.Application.Application;
            Document doc = uidoc.Document;

            IList<Reference> list_grid = uidoc.Selection.PickObjects(Autodesk.Revit.UI.Selection.ObjectType.Element);

            IList<Reference> list_ref = uidoc.Selection.PickObjects(Autodesk.Revit.UI.Selection.ObjectType.Element);

            foreach (Reference r_e in list_ref)
            {
                foreach (Reference r_g in list_grid)
                {
                    ElementId e_Id = r_e.ElementId;
                    Element e = doc.GetElement(e_Id);
                    FamilyInstance fi = e as FamilyInstance;

                    IList<Reference> list_ref_str = fi.GetReferences(FamilyInstanceReferenceType.StrongReference);
                    Reference r1 = list_ref_str.ElementAt(0);

                    LocationPoint lc = e.Location as LocationPoint;
                    XYZ pp1 = lc.Point;

                    ElementId grid_Id = r_g.ElementId;
                    Grid grd = doc.GetElement(grid_Id) as Grid;
                    Curve grd_c = grd.Curve;

                    Transform rot = Transform.CreateRotation(XYZ.BasisZ, Math.PI / 2);
                    Curve grd_c_2 = grd_c.CreateTransformed(rot);

                    Transform tran = Transform.CreateTranslation(pp1 - grd_c_2.GetEndPoint(0));
                    Line dimline = grd_c_2.CreateTransformed(tran) as Line;

                    ReferenceArray refArray = new ReferenceArray();
                    refArray.Append(r_g);
                    refArray.Append(r1);

                    using (Transaction trans = new Transaction(doc, "AUTOMATIC DIMENSION ON PLAN"))
                    {
                        trans.Start();

                        doc.Create.NewDimension(doc.ActiveView, dimline, refArray);

                        trans.Commit();
                    }
                }
            }
        }

        public ReferenceArray getReference(Reference reference, Document doc)
        {
            ReferenceArray arrRefs = new ReferenceArray();

            Element element = doc.GetElement(reference);
            Wall wall = element as Wall;
            LocationCurve locationCurve = wall.Location as LocationCurve;
            XYZ p1 = locationCurve.Curve.GetEndPoint(0);
            XYZ p2 = locationCurve.Curve.GetEndPoint(1);
            XYZ wallXL = p1.Subtract(p2);//获得墙体的向量
            XYZ shituXL = doc.ActiveView.ViewDirection;
            XYZ wallnorXL = shituXL.CrossProduct(wallXL);
            Options option = new Options();
            option.ComputeReferences = true;
            GeometryElement geometryelements = doc.GetElement(reference).get_Geometry(option);
            foreach (GeometryObject geoObject in geometryelements)
            {
                Solid solid = geoObject as Solid;
                if (solid != null)
                {
                    FaceArray faces = solid.Faces;
                    foreach (PlanarFace item in faces)
                    {
                        XYZ xYZ = item.FaceNormal;
                        XYZ c = xYZ.CrossProduct(wallXL);
                        if (c.IsZeroLength())
                        {
                            arrRefs.Append(item.Reference);
                        }
                    }

                }
            }
            if (arrRefs.Size != 2)
            {
                string CC = arrRefs.Size.ToString();
                Autodesk.Revit.UI.TaskDialog.Show(CC, "Couldn’t find enough reference for creating dimension");

            }

            return arrRefs;
        }

        public void GetInstanceGeometry_Solid(Document doc, UIDocument uidoc)
        {
            TaskDialog.Show("1", "选择一个轮廓");
            Reference profilReference = uidoc.Selection.PickObject(ObjectType.Element, "请选择一个族实例轮廓");
            FamilyInstance familyInstance = doc.GetElement(profilReference) as FamilyInstance;
            Options option = new Options();//新建一个解析几何的选项
            option.ComputeReferences = true; //打开计算几何引用 
            option.DetailLevel = ViewDetailLevel.Fine; //视图详细程度为最好 
            GeometryElement geomElement = familyInstance.get_Geometry(option); //从族实例中获取到它的几何元素GeometryElement
            foreach (GeometryObject geomObj in geomElement)
            {
                if (geomObj is GeometryInstance)//从几何元素GeometryElement中找到其几何实例GeometryInstance
                {
                    GeometryInstance geomInstance = geomObj as GeometryInstance;
                    //几何实例GeometryInstance的SymbolGeometry属性中获取族类型的几何对象（GeometryObject）
                    foreach (GeometryObject instObj in geomInstance.SymbolGeometry)
                    {
                        Solid solid = instObj as Solid;
                        //因为可能存在没有边和面的实体，所以不能用is来过滤
                        if (null == solid || 0 == solid.Faces.Size || 0 == solid.Edges.Size)
                        {
                            continue;
                        }
                        Transform instTransform = geomInstance.Transform;
                        // 从实体Solid获取面
                        foreach (Face face in solid.Faces)
                        {
                            Mesh mesh = face.Triangulate();//获得这个面的三角网
                            foreach (XYZ ii in mesh.Vertices)//遍历这个三角网的顶点
                            {
                                XYZ point = ii;
                                TaskDialog.Show("局部坐标系", "局部坐标系是：" + point);
                                XYZ transformedPoint = instTransform.OfPoint(point);
                                TaskDialog.Show("世界坐标系", "世界坐标系是：" + transformedPoint);
                            }
                        }
                        // 从实体Solid获取边
                        foreach (Edge edge in solid.Edges)
                        {
                            foreach (XYZ ii in edge.Tessellate())//遍历这个边的两个端点
                            {
                                XYZ point = ii;
                                TaskDialog.Show("局部坐标系", "局部坐标系是：" + point);
                                XYZ transformedPoint = instTransform.OfPoint(point);
                                TaskDialog.Show("世界坐标系", "世界坐标系是：" + transformedPoint);
                            }
                        }
                    }
                }
            }
        }
    }

}


