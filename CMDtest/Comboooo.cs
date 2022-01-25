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
    public class Comboooo : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string msg, ElementSet elemSet)
        {
            Document document = commandData.Application.ActiveUIDocument.Document;
            Selection selection = commandData.Application.ActiveUIDocument.Selection;
            Application app = commandData.Application.Application;

            View view = document.ActiveView;

            ReferenceArray referenceArray = new ReferenceArray();
            List<FamilyInstance> familyInstances = new List<FamilyInstance>();

            IList<Reference> instanceReferences = new List<Reference>();
            IList<Reference> hasPickOne = selection.PickObjects(ObjectType.Element);

            foreach (Reference reference in hasPickOne)
            {
                // 轉換選中的obj
                familyInstances.Add((document.GetElement(reference)) as FamilyInstance);
            }

            // 判斷水平垂直
            bool isHorizen = isDimensionHorizen(familyInstances);


            foreach (FamilyInstance familyInstance in familyInstances)
            {
                double rotation = (familyInstance.Location as LocationPoint).Rotation;
                // 是否旋轉180  即 與原有族各参照線平行  平行則true
                bool phare = Math.Round(rotation / (0.5 * Math.PI) % 2, 4) == 0;

                IList<Reference> refs = isHorizen ^ phare == false ? familyInstance.GetReferences(FamilyInstanceReferenceType.CenterLeftRight)
                    : familyInstance.GetReferences(FamilyInstanceReferenceType.CenterLeftRight);// 將實例中特殊的参照平台拿出来

                // 將取得的中線放入參照平台面内
                instanceReferences.Add(refs.Count == 0 ? null : refs[0]);

                referenceArray.Append(refs.Count == 0 ? null : refs[0]);
            }

            Element element = document.GetElement(hasPickOne.ElementAt(0));
            XYZ elementXyz = (element.Location as LocationPoint).Point;

            // 尺寸線定位
            double distanceNewLine = 1;

            Line line = Line.CreateBound(elementXyz, new XYZ(elementXyz.X + distanceNewLine, elementXyz.Y, elementXyz.Z));

            line = isHorizen == true ?
                  Line.CreateBound(elementXyz, new XYZ(elementXyz.X, elementXyz.Y + distanceNewLine, elementXyz.Z))
                : Line.CreateBound(elementXyz, new XYZ(elementXyz.X + distanceNewLine, elementXyz.Y, elementXyz.Z));
            XYZ selectionPoint = selection.PickPoint();
            selectionPoint = new XYZ(elementXyz.X + distanceNewLine, elementXyz.Y + 50, elementXyz.Z);
            selectionPoint = isHorizen == true ?
                    new XYZ(elementXyz.X + 50, elementXyz.Y + distanceNewLine, elementXyz.Z)
                : new XYZ(elementXyz.X + distanceNewLine, elementXyz.Y + 50, elementXyz.Z);
            
            XYZ projectPoint = line.Project(selectionPoint).XYZPoint;
            Line newLine = Line.CreateBound(selectionPoint, projectPoint);

            Transaction transaction = new Transaction(document, "添加標註");
            transaction.Start();

            Dimension autoDimension = document.Create.NewDimension(view, newLine, referenceArray);
            transaction.Commit();

            return Result.Succeeded;
        }

        public bool isDimensionHorizen(List<FamilyInstance> familyInstances)
        {
            if (
                Math.Abs
                (
                   familyInstances.OrderBy(f => (f.Location as LocationPoint).Point.X).Select(f => (f.Location as LocationPoint).Point.X).First()
                 - familyInstances.OrderByDescending(f => (f.Location as LocationPoint).Point.X).Select(f => (f.Location as LocationPoint).Point.X).First()
                )
                >
                Math.Abs
                (
                      familyInstances.OrderBy(f => (f.Location as LocationPoint).Point.Y).Select(f => (f.Location as LocationPoint).Point.Y).First()
                    - familyInstances.OrderByDescending(f => (f.Location as LocationPoint).Point.Y).Select(f => (f.Location as LocationPoint).Point.Y).First()
                 )
                )
            {
                return true;
            }
            return false;
        }
    }
}
