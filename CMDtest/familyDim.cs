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
    [Journaling(JournalingMode.NoCommandData)]
    public class familyDim : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string msg, ElementSet elemSet)
        {
            Dimension(commandData);
            return Result.Succeeded;
        }
        public void Dimension(ExternalCommandData commandData)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            List<Reference> familyInstance = new List<Reference>();

            Selection selection = commandData.Application.ActiveUIDocument.Selection;

            Reference eRef = selection.PickObject(ObjectType.Element, "objOne");
            Reference eRef2 = selection.PickObject(ObjectType.Element, "objTwo");

            Element element = doc.GetElement(eRef);
            Element element2 = doc.GetElement(eRef2);

            // 標中線 撒水頭 CenterLeftRight 管束 CenterFrontBack
            familyInstance.Add(refType(element));
            familyInstance.Add(refType(element2));

            LocationPoint locationPoint1 = element.Location as LocationPoint;
            LocationPoint locationPoint2 = element2.Location as LocationPoint;

            XYZ coordinate1 = locationPoint1.Point;
            XYZ coordinate2 = locationPoint2.Point;

            //IsSameDirection(coordinate1, coordinate2);
            //TaskDialog.Show("IsSameDirection", IsSameDirection(coordinate1, coordinate2).ToString());
            //TaskDialog.Show("Distance",coordinate1.DistanceTo(coordinate2).ToString());

            DimByTwoXYZ(commandData, coordinate1, coordinate2, familyInstance);
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

                if (!pt1.X.ToString("F3").Equals(pt2.X.ToString("F3"))  && !pt1.Y.ToString("F3").Equals(pt2.Y.ToString("F3")))
                {
                    dimPoint1 = new XYZ(pt1.X - 1, pt1.Y, pt1.Z);
                    dimPoint2 = new XYZ(pt2.X - 1, pt1.Y, pt2.Z);
                }

                Line dimLine = Line.CreateBound(dimPoint1, dimPoint2);

                Dimension dim = doc.Create.NewDimension(activeView, dimLine, refArray);
                //TaskDialog.Show("dim.Value", dim.ValueString);
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
        public static bool IsParallel(XYZ vt1, XYZ vt2, double dDist = 0.001)
        {
            return vt1.Normalize().DistanceTo(vt2.Normalize()) < dDist || vt1.Normalize().DistanceTo(-vt2.Normalize()) < dDist;
        }
        public static bool IsSameDirection(XYZ vt1, XYZ vt2, double dDist = 0.001)
        {
            return vt1.Normalize().DistanceTo(vt2.Normalize()) < dDist;
        }

    }
}
