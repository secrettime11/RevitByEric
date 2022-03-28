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
    public class AA : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string msg, ElementSet elemSet)
        {

            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            // Find a suitable 3D view:

            View3D view = Get3dView(doc);

            if (null == view)
            {
                //message = "Sorry, no suitable 3D view found";

                return Result.Failed;
            }
            else
            {
                // Must set view before starting the transaction,
                // otherwise an exception is thrown saying 
                // "Cannot change the active view of a modifiable 
                // document (with a transaction currently open)."

                uidoc.ActiveView = view;

                // Must start a transaction in order to set the 
                // parameters on the view:

                Transaction t = new Transaction(doc);
                t.Start("Change to 3D view");

                view.get_Parameter(BuiltInParameter
                  .VIEW_DETAIL_LEVEL).Set(3);

                view.get_Parameter(BuiltInParameter
                  .MODEL_GRAPHICS_STYLE).Set(6);

                t.Commit();

                return Result.Succeeded;
            }
        }



        View3D Get3dView(Document doc)
        {
            FilteredElementCollector collector
              = new FilteredElementCollector(doc)
                .OfClass(typeof(View3D));

            foreach (View3D v in collector)
            {
                //Debug.Assert(null != v,
                //  "never expected a null view to be returned"
                //  + " from filtered element collector");

                // Skip view template here because view 
                // templates are invisible in project 
                // browser

                if (!v.IsTemplate)
                {
                    return v;
                }
            }
            return null;
        }
    }
}


