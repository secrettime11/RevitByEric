using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDtest
{
    [Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]
    [Journaling(Autodesk.Revit.Attributes.JournalingMode.NoCommandData)]

    public class DimensionTry : IExternalCommand
    {

        public Result Execute(ExternalCommandData commandData, ref string msg, ElementSet elemSet)
        {
            //Document doc = commandData.Application.ActiveUIDocument.Document;
            //Selection selection = commandData.Application.ActiveUIDocument.Selection;
            //Application app = commandData.Application.Application;
            //View activeView = commandData.Application.ActiveUIDocument.ActiveView;

            //XYZ coordinateA = null;
            //XYZ coordinateB = null;

            //Reference eRef = selection.PickObject(ObjectType.Element, "yu1");
            //Element element = doc.GetElement(eRef);

            //LocationPoint LLa = element.Location as LocationPoint;
            //coordinateA = LLa.Point;

            //Reference eRef2 = selection.PickObject(ObjectType.Element, "yu2");
            //Element element2 = doc.GetElement(eRef2);

            //ModelCurve mc2 = doc.GetElement(eRef) as ModelCurve;

            //LocationPoint LLa2 = element2.Location as LocationPoint;
            //coordinateB = LLa2.Point;


            //XYZ point3 = coordinateA;
            //XYZ point4 = coordinateB;

            //Line geomLine3 = Line.CreateBound(point3, point4);
            //Line dummyLine = Line.CreateBound(XYZ.Zero, XYZ.BasisY);

            //using (Transaction tx = new Transaction(doc))
            //{
            //    tx.Start("tx");
            //    XYZ dimPoint1 = new XYZ(point3.X, point3.Y + 1, 0);
            //    XYZ dimPoint2 = new XYZ(point4.X, point4.Y + 1, 0);
            //    Line dimLine3 = Line.CreateBound(dimPoint1, dimPoint2);

            //    DetailLine line3 = doc.Create.NewDetailCurve(
            //      activeView, dimLine3) as DetailLine;

            //    DetailLine dummy = doc.Create.NewDetailCurve(
            //      activeView, dummyLine) as DetailLine;

            //    ReferenceArray refArray = new ReferenceArray();
            //    refArray.Append(dummy.GeometryCurve.Reference);
            //    refArray.Append(line3.GeometryCurve.GetEndPointReference(0));
            //    refArray.Append(line3.GeometryCurve.GetEndPointReference(1));

            //    Dimension dim = doc.Create.NewDimension(
            //      activeView, dimLine3, refArray);

            //    doc.Delete(dummy.Id);
            //    tx.Commit();
            //}

            AutoColumnGridDim(commandData);

            return Result.Succeeded;
        }

        /// <summary>
        /// 判断设备位置大致情况 是否为水平 还是垂直
        /// </summary>
        /// <param name="familyInstances"></param>
        /// <returns></returns>
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

        public void AutoColumnGridDim(ExternalCommandData commandData)
        {
            Document document = commandData.Application.ActiveUIDocument.Document;
            Selection selection = commandData.Application.ActiveUIDocument.Selection;
            Autodesk.Revit.ApplicationServices.Application app = commandData.Application.Application;

            Autodesk.Revit.DB.View view = document.ActiveView;
            int hashcode = 0;
            ReferenceArray referenceArray = new ReferenceArray();
            //Pick one object from Revit.
            List<Wall> mWalls = new List<Wall>();
            List<FamilyInstance> familyInstances = new List<FamilyInstance>();

            IList<Reference> instanceReferences = new List<Reference>();
            IList<Reference> hasPickOne = selection.PickObjects(ObjectType.Element);
            foreach (Reference reference in hasPickOne)
            {
                familyInstances.Add((document.GetElement(reference)) as FamilyInstance);//转换选中的族们
            }
            bool isHorizen = isDimensionHorizen(familyInstances);//判断设备位置横竖大致情况
            foreach (FamilyInstance familyInstance in familyInstances)
            {
                double rotation = (familyInstance.Location as LocationPoint).Rotation;
                bool phare = Math.Round(rotation / (0.5 * Math.PI) % 2, 4) == 0;//是否180旋转  即 与原有族各参照线平行  平行则true
                //MessageBox.Show(isHorizen.ToString() + "\t" + phare.ToString());
                IList<Reference> refs = isHorizen ^ phare == false ? familyInstance.GetReferences(FamilyInstanceReferenceType.CenterLeftRight)
                    : familyInstance.GetReferences(FamilyInstanceReferenceType.CenterFrontBack);//将实例中特殊的参照平台拿出来

                IList<Reference> refLRs = familyInstance.GetReferences(FamilyInstanceReferenceType.CenterLeftRight);
                IList<Reference> reFBRs = familyInstance.GetReferences(FamilyInstanceReferenceType.CenterFrontBack);



                // IList<Reference> refs = familyInstance.GetReferences(FamilyInstanceReferenceType.CenterLeftRight);//将实例中特殊的参照平台拿出来
                instanceReferences.Add(refs.Count == 0 ? null : refs[0]); //将获得的中线放入参照平台面内

                referenceArray.Append(refs.Count == 0 ? null : refs[0]);
            }

            //  AutoCreatDimension(document, selection, instanceReferences);  //该函数可用 但条件苛刻

            Element element = document.GetElement(hasPickOne.ElementAt(0));
            XYZ elementXyz = (element.Location as LocationPoint).Point;
            // Line line = (element.Location as LocationCurve).Curve as Line;

            //尺寸线定位
            double distanceNewLine = 2;
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

            Transaction transaction = new Transaction(document, "添加标注");
            transaction.Start();
            //调用创建尺寸的方法创建

            Dimension autoDimension = document.Create.NewDimension(view, newLine, referenceArray);
            transaction.Commit();


            // AutoCreatDimension(document,selection, hasPickOne);  //该函数可用 但条件苛刻

            //throw new NotImplementedException();
        }

        public void DimTwoPoints(ExternalCommandData commandData)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            UIApplication uiapp = commandData.Application;
            Autodesk.Revit.ApplicationServices.Application app = uiapp.Application;
            Document doc = uidoc.Document;

            Autodesk.Revit.DB.View view = doc.ActiveView;
            ViewType vt = view.ViewType;
            if (vt == ViewType.FloorPlan || vt == ViewType.Elevation)
            {
                Reference eRef = uidoc.Selection.PickObject(ObjectType.Element, "Please pick a curve based element like wall.");
                Element element = doc.GetElement(eRef);
                if (eRef != null && element != null)
                {
                    XYZ dirVec = new XYZ();
                    XYZ viewNormal = view.ViewDirection;

                    LocationCurve locCurve = element.Location as LocationCurve;
                    if (locCurve == null || locCurve.Curve == null)
                    {
                        TaskDialog.Show("Prompt", "Selected element isn’t curve based!");
                        //  return Result.Cancelled;
                    }
                    XYZ dirCur = locCurve.Curve.GetEndPoint(0).Subtract(locCurve.Curve.GetEndPoint(1)).Normalize();
                    double d = dirCur.DotProduct(viewNormal);
                    if (d > -0.000000001 && d < 0.000000001)
                    {
                        dirVec = dirCur.CrossProduct(viewNormal);
                        XYZ p1 = locCurve.Curve.GetEndPoint(0);
                        XYZ p2 = locCurve.Curve.GetEndPoint(1);
                        XYZ dirLine = XYZ.Zero.Add(p1);
                        XYZ newVec = XYZ.Zero.Add(dirVec);
                        newVec = newVec.Normalize().Multiply(3);
                        dirLine = dirLine.Subtract(p2);
                        p1 = p1.Add(newVec);
                        p2 = p2.Add(newVec);
                        Line newLine = Line.CreateBound(p1, p2);
                        ReferenceArray arrRefs = new ReferenceArray();
                        Options options = app.Create.NewGeometryOptions();
                        options.ComputeReferences = true;
                        options.DetailLevel = ViewDetailLevel.Fine;
                        GeometryElement gelement = element.get_Geometry(options);
                        foreach (var geoObject in gelement)
                        {
                            Solid solid = geoObject as Solid;
                            if (solid == null)
                                continue;

                            FaceArrayIterator fIt = solid.Faces.ForwardIterator();
                            while (fIt.MoveNext())
                            {
                                PlanarFace p = fIt.Current as PlanarFace;
                                if (p == null)
                                    continue;

                                p2 = p.FaceNormal.CrossProduct(dirLine);
                                if (p2.IsZeroLength())
                                {
                                    arrRefs.Append(p.Reference);
                                }
                                if (2 == arrRefs.Size)
                                {
                                    break;
                                }
                            }
                            if (2 == arrRefs.Size)
                            {
                                break;
                            }
                        }
                        if (arrRefs.Size != 2)
                        {
                            TaskDialog.Show("Prompt", "Couldn’t find enough reference for creating dimension");
                            //return Result.Cancelled;
                        }

                        Transaction trans = new Transaction(doc, "create dimension");
                        trans.Start();
                        doc.Create.NewDimension(doc.ActiveView, newLine, arrRefs);
                        trans.Commit();
                    }
                    else
                    {
                        TaskDialog.Show("Prompt", "Selected element isn’t curve based!");
                        // return Result.Cancelled;
                    }
                }
            }
            else
            {
                TaskDialog.Show("Prompt", "Only support Plan View or Elevation View");
            }
        }
    }
}
