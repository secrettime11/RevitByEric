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
using System.Runtime.InteropServices;
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
            AutoDimensionForm autoDimension = new AutoDimensionForm();
            autoDimension.Show();

            return Result.Succeeded;
        }

        public void DimByTwoXYZ(ExternalCommandData commandData, XYZ pt1, XYZ pt2, List<Reference> familyInstance)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;
            View activeView = uidoc.ActiveView;

            using (Transaction tx = new Transaction(doc))
            {
                tx.Start("Dimension mission");

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

                // Dimension
                doc.Create.NewDimension(activeView, dimLine, refArray);

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

        /// <summary>
        /// 判斷兩個向量是否平行，包含方向相反
        /// 通過Normalize()將兩個向量單位正交化，這樣就可以通過比較這兩個單位向量的大小
        /// 或者說比較這兩個點是否重合來比較。
        /// </summary>
        /// <param name="vt1"></param>
        /// <param name="vt2"></param>
        /// <param name="dDist"></param>
        /// <returns></returns>
        public static bool IsParallel(XYZ vt1, XYZ vt2, double dDist = 0.001)
        {
            return vt1.Normalize().DistanceTo(vt2.Normalize()) < dDist || vt1.Normalize().DistanceTo(-vt2.Normalize()) < dDist;
        }

        /// <summary>
        /// 判斷兩個向量是否同向
        /// </summary>
        /// <param name="vt1"></param>
        /// <param name="vt2"></param>
        /// <param name="dDist"></param>
        /// <returns></returns>
        public static bool IsSameDirection(XYZ vt1, XYZ vt2, double dDist = 0.001)
        {
            return vt1.Normalize().DistanceTo(vt2.Normalize()) < dDist;
        }

        /// <summary>
        /// 判斷兩個向量是否垂直，通過判斷兩個向量的點乘（若等於0說明垂直）
        /// </summary>
        /// <param name="vt1"></param>
        /// <param name="vt2"></param>
        /// <param name="dDist"></param>
        /// <returns></returns>
        public static bool IsPerpendicular(XYZ vt1, XYZ vt2, double dDist = 0.001)
        {
            return Math.Abs(vt1.Normalize().DotProduct(vt2.Normalize())) < dDist;
        }

    }
}
