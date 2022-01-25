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
    public class NewVersion0121 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string msg, ElementSet elemSet)
        {
            UIDocument uiDoc = commandData.Application.ActiveUIDocument;
            Document doc = uiDoc.Document;
            Selection selection = uiDoc.Selection;
            
            Reference reference1 = selection.PickObject(ObjectType.PointOnElement);
            Reference reference2 = selection.PickObject(ObjectType.PointOnElement);

            ReferenceArray refArray = new ReferenceArray();
            refArray.Append(reference1);
            refArray.Append(reference2);

            Element element1 = doc.GetElement(reference1);

            FamilyInstance familyInstance = element1 as FamilyInstance;

            LocationPoint locationPoint1 = element1.Location as LocationPoint;
            
            XYZ PipeRackPoint1 = locationPoint1.Point;

            Line dirLine = Line.CreateUnbound(PipeRackPoint1, familyInstance.FacingOrientation);

            //在事务中创建尺寸标注线
            using (Transaction tr = new Transaction(doc))
            {
                tr.Start("Create detail curve");
                doc.Create.NewDimension(doc.ActiveView, dirLine, refArray);
                tr.Commit();
            }

            return Result.Succeeded;
        }

        public Dimension CreateLinearDimension(Document document, ExternalCommandData commandData)
        {
            XYZ coordinateA = null;
            XYZ coordinateB = null;
            using (Transaction tx = new Transaction(document))
            {
                tx.Start("tx");
                Selection selection = commandData.Application.ActiveUIDocument.Selection;

                Reference eRef = selection.PickObject(ObjectType.Element, "yu1");
                Element element = document.GetElement(eRef);

                LocationPoint LLa = element.Location as LocationPoint;
                coordinateA = LLa.Point;


                Reference eRef2 = selection.PickObject(ObjectType.Element, "yu2");
                Element element2 = document.GetElement(eRef2);

                LocationPoint LLa2 = element2.Location as LocationPoint;
                coordinateB = LLa2.Point;

                // first create two lines
                XYZ pt1 = new XYZ(5, 5, 0);
                XYZ pt2 = new XYZ(5, 10, 0);
                Line line = Line.CreateBound(coordinateA, coordinateB);
                Plane plane = Plane.CreateByNormalAndOrigin(coordinateA.CrossProduct(coordinateB), coordinateB);
                SketchPlane skplane = SketchPlane.Create(document, plane);
                ModelCurve modelcurve1 = document.FamilyCreate.NewModelCurve(line, skplane);

                pt1 = new XYZ(10, 5, 0);
                pt2 = new XYZ(10, 10, 0);

                coordinateA = new XYZ(coordinateA.X, coordinateA.Y + 1, coordinateA.Z);
                coordinateB = new XYZ(coordinateB.X, coordinateB.Y + 1, coordinateB.Z);

                line = Line.CreateBound(coordinateA, coordinateB);
                plane = Plane.CreateByNormalAndOrigin(coordinateA.CrossProduct(coordinateB), coordinateB);
                skplane = SketchPlane.Create(document, plane);
                ModelCurve modelcurve2 = document.FamilyCreate.NewModelCurve(line, skplane);

                // now create a linear dimension between them
                ReferenceArray ra = new ReferenceArray();
                ra.Append(modelcurve1.GeometryCurve.Reference);
                ra.Append(modelcurve2.GeometryCurve.Reference);

                pt1 = new XYZ(5, 10, 0);
                pt2 = new XYZ(10, 10, 0);

                coordinateA = new XYZ(coordinateA.X + 1, coordinateA.Y, coordinateA.Z);
                coordinateB = new XYZ(coordinateB.X + 1, coordinateB.Y, coordinateB.Z);

                line = Line.CreateBound(pt1, pt2);

                Dimension dim = document.FamilyCreate.NewLinearDimension(document.ActiveView, line, ra);

                // create a label for the dimension called "width"
                FamilyParameter param = document.FamilyManager.AddParameter("width",
                    BuiltInParameterGroup.PG_CONSTRAINTS,
                    ParameterType.Length, false);

                dim.FamilyLabel = param;
                tx.Commit();
                return dim;
            }
        }
        
    }
}
