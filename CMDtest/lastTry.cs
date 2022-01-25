using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CMDtest
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.NoCommandData)]
    public class lastTry : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string msg, ElementSet elemSet)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            List<Reference> familyInstance = new List<Reference>();

            Selection selection = commandData.Application.ActiveUIDocument.Selection;

            Reference eRef = selection.PickObject(ObjectType.Element, "item1");
            Reference eRef2 = selection.PickObject(ObjectType.Element, "item2");

            Element element = doc.GetElement(eRef);
            Element element2 = doc.GetElement(eRef2);

            // 標中線 撒水頭 CenterLeftRight 管束 CenterFrontBack
            familyInstance.Add(refType(element));
            familyInstance.Add(refType(element2));

            LocationPoint locationPoint1 = element.Location as LocationPoint;
            LocationPoint locationPoint2 = element2.Location as LocationPoint;

            XYZ coordinate1 = locationPoint1.Point;
            XYZ coordinate2 = locationPoint2.Point;

            DimByTwoXYZ(commandData, coordinate1, coordinate2, familyInstance);

            return Result.Succeeded;
        }


        public void DimByTwoXYZ(ExternalCommandData commandData, XYZ pt1, XYZ pt2, List<Reference> familyInstance)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;
            View activeView = uidoc.ActiveView;


            pt1 = new XYZ(pt1.X, pt1.Y, 0);
            pt2 = new XYZ(pt2.X, pt2.Y, 0);

            using (Transaction tx = new Transaction(doc))
            {
                tx.Start("tx");

                ReferenceArray refArray = new ReferenceArray();
                refArray.Append(familyInstance[0]);
                refArray.Append(familyInstance[1]);

                // Default Horizontal [top]
                XYZ dimPoint1 = new XYZ(pt1.X, pt1.Y + 1, pt1.Z);
                XYZ dimPoint2 = new XYZ(pt2.X, pt2.Y + 1, pt2.Z);

                // Set Vertical [left]
                if (pt1.X == pt2.X)
                {
                    dimPoint1 = new XYZ(pt1.X - 1, pt1.Y, pt1.Z);
                    dimPoint2 = new XYZ(pt2.X - 1, pt2.Y, pt2.Z);
                }

                Line dimLine = Line.CreateBound(dimPoint1, dimPoint2);

                Dimension dim = doc.Create.NewDimension(activeView, dimLine, refArray);

                //dim.DimensionType = SetDimStyle(doc);

                tx.Commit();
            }
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
        /// <summary>
        /// ModelCurve dim with CreateLinearDimension function
        /// </summary>
        /// <param name="uidoc"></param>
        /// <param name="app"></param>
        /// <param name="P1"></param>
        /// <param name="P2"></param>
        public void PickPoint(UIDocument uidoc, Application app, XYZ P1, XYZ P2)
        {
            View activeView = uidoc.ActiveView;
            SketchPlane sketch = activeView.SketchPlane;
            ObjectSnapTypes snapTypes = ObjectSnapTypes.Endpoints | ObjectSnapTypes.Intersections | ObjectSnapTypes.Points | ObjectSnapTypes.Perpendicular;
            XYZ startPoint;
            XYZ endPoint;

            Plane geometryPlane = Plane.CreateByNormalAndOrigin(XYZ.BasisZ, XYZ.Zero);

            sketch = SketchPlane.Create(uidoc.Document, geometryPlane);

            uidoc.Document.ActiveView.SketchPlane = sketch;

            // 顯示工作區域
            //uidoc.Document.ActiveView.ShowActiveWorkPlane();
            try
            {
                //startPoint = uidoc.Selection.PickPoint(snapTypes, "Select start point");
                //endPoint = uidoc.Selection.PickPoint(snapTypes, "Select end point");
                startPoint = P1;
                endPoint = P2;

            }
            catch (Autodesk.Revit.Exceptions.OperationCanceledException oc)
            {
                Console.WriteLine(oc.Message);
                return;
            }
            catch (Autodesk.Revit.Exceptions.InvalidOperationException oe)
            {
                Console.WriteLine(oe.Message);
                TaskDialog.Show("Revit", "No work plane set in current view.");
                return;

            }
            catch (Autodesk.Revit.Exceptions.ArgumentNullException n)
            {
                Console.WriteLine(n.Message);
                return;
            }

            double dist = startPoint.DistanceTo(endPoint);

            string distance = "Distance is " + dist.ToString();
            string strCoords = "Selected start point is " + startPoint.ToString() + "\nSelected end point is " + endPoint.ToString() + distance;
            Line line = Line.CreateBound(startPoint, endPoint);
            //TaskDialog.Show("Revit", strCoords);
            CreateLinearDimension(uidoc.Document, startPoint, endPoint, sketch, app, activeView);

        }
        public void CreateLinearDimension(Document doc, XYZ pt1, XYZ pt2, SketchPlane sketch, Application app, View view)
        {
            // first create line

            pt1 = new XYZ(pt1.X, pt1.Y + 1, 0);
            pt2 = new XYZ(pt2.X, pt2.Y + 1, 0);

            Line line = Line.CreateBound(pt1, pt2);
            ModelCurve modelcurve = doc.Create
            .NewModelCurve(line, sketch);

            //GraphicsStyle gs2 = modelcurve.LineStyle as GraphicsStyle;
            //gs2.GraphicsStyleCategory.LineColor = new Color(255, 255, 255);

            ReferenceArray ra = new ReferenceArray();
            ra.Append(modelcurve.GeometryCurve.GetEndPointReference(0));
            ra.Append(modelcurve.GeometryCurve.GetEndPointReference(1));

            doc.Create.NewDimension(doc.ActiveView, line, ra);
            doc.Delete(modelcurve.Id);
        }
        public List<Solid> GetAllSolid(Application rapp, Element e)
        {
            List<Solid> lstSolid = new List<Solid>();

            Options options = rapp.Create.NewGeometryOptions();
            options.ComputeReferences = true;
            options.DetailLevel = ViewDetailLevel.Fine;

            GeometryElement geoElem = e.get_Geometry(options);
            IEnumerator it = geoElem.GetEnumerator();
            while (it.MoveNext())
            {
                GeometryObject geoObject = (GeometryObject)it.Current;
                Solid solid = geoObject as Solid;
                if (solid == null)
                    continue;
                lstSolid.Add(solid);
            }
            return lstSolid;
        }
        private bool getSolidReference(Application revitApp, Element e, XYZ dirLine, ref ReferenceArray refs)
        {
            if (refs == null)
            {
                return false;
            }
            refs.Clear();
            foreach (Solid solid in GetAllSolid(revitApp, e))
            {
                FaceArrayIterator fIt = solid.Faces.ForwardIterator();
                while (fIt.MoveNext())
                {
                    PlanarFace p = fIt.Current as PlanarFace;
                    if (p == null)
                        continue;
                    if (p.FaceNormal.CrossProduct(dirLine).IsZeroLength() == false)
                    {
                        continue;
                    }

                    refs.Append(p.Reference);
                    if (2 == refs.Size)
                    {
                        break;
                    }
                }
                if (2 == refs.Size)
                {
                    break;
                }
            }
            return (refs.Size == 2);
        }
        private Reference GetFamilyInstancePointReference(
            FamilyInstance fi)
        {
            var _opt = new Options();
            _opt.ComputeReferences = true;
            _opt.IncludeNonVisibleObjects = true;

            return fi.get_Geometry(_opt)
                .OfType<Point>()
                .Select(x => x.Reference)
                .FirstOrDefault();
        }

        private DimensionType SetDimStyle(Document doc)
        {
            FilteredElementCollector DimesionTypeCollector = new FilteredElementCollector(doc);
            DimesionTypeCollector.OfClass(typeof(DimensionType));

            DimensionType dimesionType = DimesionTypeCollector.Cast<DimensionType>().ToList().FirstOrDefault();
            DimensionType newdimesionType = dimesionType.Duplicate("new dimesionType") as DimensionType;
            newdimesionType.LookupParameter("Color").Set(125);

            return newdimesionType;
        }
    }
}
