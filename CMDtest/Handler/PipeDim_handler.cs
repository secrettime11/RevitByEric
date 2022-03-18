using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDtest.Handler
{
    public class PipeDim_handler: IExternalEventHandler
    {
        public void Execute(UIApplication uiapp)
        {
            QuickMark(uiapp);
        }

        private void QuickMark(UIApplication uiapp)
        {
            UIDocument uiDoc = uiapp.ActiveUIDocument;
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

        public string GetName()
        {
            return "PipeDim_handler";
        }

    }
}
