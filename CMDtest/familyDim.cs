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
    [TransactionAttribute(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class familyDim : IExternalCommand
    {
        static Options _opt = null;
        public Result Execute(ExternalCommandData commandData, ref string msg, ElementSet elemSet)
        {
            UIApplication app = commandData.Application;
            UIDocument uidoc = app.ActiveUIDocument;
            Document doc = uidoc.Document;

            JtPairPicker<FamilyInstance> picker
              = new JtPairPicker<FamilyInstance>(uidoc);

            Result rc = picker.Pick();

            if (Result.Failed == rc)
            {
                //message = "We need at least two "
                //  + "FamilyInstance elements in the model.";
            }
            else if (Result.Succeeded == rc)
            {
                IList<FamilyInstance> a = picker.Selected;

                _opt = new Options();
                _opt.ComputeReferences = true;
                _opt.IncludeNonVisibleObjects = true;
                _opt.View = doc.ActiveView;

                XYZ[] pts = new XYZ[2];
                Reference[] refs = new Reference[2];

                pts[0] = (a[0].Location as LocationPoint).Point;
                pts[1] = (a[1].Location as LocationPoint).Point;

                refs[0] = a[0].GetReferences(FamilyInstanceReferenceType.CenterLeftRight).FirstOrDefault();
                refs[1] = a[1].GetReferences(FamilyInstanceReferenceType.CenterLeftRight).FirstOrDefault();

                //refs[0] = GetFamilyInstancePointReference(a[0]);
                //refs[1] = GetFamilyInstancePointReference(a[1]);

                if (refs[0] != null && refs[1] != null)
                {
                    CmdDimensionWallsIterateFaces.CreateDimensionElement(doc.ActiveView, pts[0], refs[0], pts[1], refs[1]);
                }
                else
                {
                    TaskDialog.Show("title", "null");
                }
            }
            return rc;
        }

        public void dimensionConsolidation(ExternalCommandData commandData)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            Autodesk.Revit.ApplicationServices.Application app = uiapp.Application;

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

        /// <summary>
        /// Retrieve the given family instance's
        /// non-visible geometry point reference.
        /// </summary>
        Reference GetFamilyInstancePointReference(FamilyInstance fi)
        {
            return fi.get_Geometry(_opt)
              .OfType<Point>()
              .Select(x => x.Reference)
              .FirstOrDefault();
        }
        class JtPairPicker<T> where T : Element
        {
            UIDocument _uidoc;
            Document _doc;
            List<T> _a;

            /// <summary>
            /// Allow selection of elements of type T only.
            /// </summary>
            class ElementsOfClassSelectionFilter<T2> : ISelectionFilter
            {
                public bool AllowElement(Element e)
                {
                    return e is T2;
                }

                public bool AllowReference(Reference r, XYZ p)
                {
                    return true;
                }
            }

            public JtPairPicker(UIDocument uidoc)
            {
                _uidoc = uidoc;
                _doc = _uidoc.Document;
            }

            /// <summary>
            /// Return selection result.
            /// </summary>
            public IList<T> Selected
            {
                get
                {
                    return _a;
                }
            }

            /// <summary>
            /// Run the automatic or interactive 
            /// selection process.
            /// </summary>
            public Result Pick()
            {
                // Retrieve all T elements in the entire model.

                _a = new List<T>(
                  new FilteredElementCollector(_doc)
                    .OfClass(typeof(T))
                    .ToElements()
                    .Cast<T>());

                int n = _a.Count;

                // If there are less than two, 
                // there is nothing we can do.

                if (2 > n)
                {
                    return Result.Failed;
                }

                // If there are exactly two, pick those.

                if (2 == n)
                {
                    return Result.Succeeded;
                }

                // There are more than two to choose from.
                // Check for a pre-selection.

                _a.Clear();

                Selection sel = _uidoc.Selection;

                ICollection<ElementId> ids
                  = sel.GetElementIds();

                n = ids.Count;

                //Debug.Print("{0} pre-selected elements.", n);

                // If two or more T elements were pre-
                // selected, use the first two encountered.

                if (1 < n)
                {
                    foreach (ElementId id in ids)
                    {
                        T e = _doc.GetElement(id) as T;

                        //Debug.Assert(null != e,
                        //"only elements of type T can be picked");

                        _a.Add(e);

                        if (2 == _a.Count)
                        {
                            //Debug.Print("Found two pre-selected "
                            //+ "elements of desired type and "
                            //+ "ignoring everything else.");

                            break;
                        }
                    }
                }

                // None or less than two elements were pre-
                // selected, so prompt for an interactive 
                // post-selection instead.

                if (2 != _a.Count)
                {
                    _a.Clear();

                    // Select first element.

                    try
                    {
                        Reference r = sel.PickObject(
                          ObjectType.Element,
                          new ElementsOfClassSelectionFilter<T>(),
                          "Please pick first element.");

                        _a.Add(_doc.GetElement(r.ElementId)
                          as T);
                    }
                    catch (Autodesk.Revit.Exceptions
                      .OperationCanceledException)
                    {
                        return Result.Cancelled;
                    }

                    // Select second element.

                    try
                    {
                        Reference r = sel.PickObject(
                          ObjectType.Element,
                          new ElementsOfClassSelectionFilter<T>(),
                          "Please pick second element.");

                        _a.Add(_doc.GetElement(r.ElementId)
                          as T);
                    }
                    catch (Autodesk.Revit.Exceptions
                      .OperationCanceledException)
                    {
                        return Result.Cancelled;
                    }
                }
                return Result.Succeeded;
            }
        }
        class CmdDimensionWallsIterateFaces
        {

            /// <summary>
            ///     Create a new dimension element using the given
            ///     references and dimension line end points.
            ///     This method opens and commits its own transaction,
            ///     assuming that no transaction is open yet and manual
            ///     transaction mode is being used.
            ///     Note that this has only been tested so far using
            ///     references to surfaces on planar walls in a plan
            ///     view.
            /// </summary>
            public static void CreateDimensionElement(View view, XYZ pt1, Reference r1, XYZ pt2, Reference r2)
            {
                var doc = view.Document;

                var ra = new ReferenceArray();

                ra.Append(r1);
                ra.Append(r2);

                pt1 = new XYZ(pt1.X, pt1.Y, 0);
                pt2 = new XYZ(pt2.X, pt2.Y, 0);

                var line = Line.CreateBound(pt1, pt2);
                var t = new Transaction(doc);
                t.Start("Create New Dimension");

                var dim = doc.Create.NewDimension(view, line, ra);
                t.Commit();
            }
        }
    }
}
