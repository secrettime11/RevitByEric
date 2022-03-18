using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDtest.ColorPipe
{
    public class _3Devent: IExternalEventHandler
    {
        public void Execute(UIApplication uiapp)
        {
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;
            //View view = doc.ActiveView;
            

            ViewFamilyType viewFamilyType3D = new FilteredElementCollector(doc)
                                                  .OfClass(typeof(ViewFamilyType))
                                                  .Cast<ViewFamilyType>()
                                                  .FirstOrDefault(x => ViewFamily.ThreeDimensional == x.ViewFamily);
            View3D view3D = View3D.CreateIsometric(doc, viewFamilyType3D.Id);
            //view3D.Name = viewName;
            view3D.DisplayStyle = DisplayStyle.ShadingWithEdges;
            view3D.DetailLevel = ViewDetailLevel.Fine;
            view3D.SaveOrientationAndLock();
            view3D.Scale = 20;
            //view3D.ApplyViewTemplateParameters(ViewTemplate);
        }
        public string GetName()
        {
            return "_3Devent";
        }
    }
}
