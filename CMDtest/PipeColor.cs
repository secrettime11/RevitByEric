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
    public class PipeColor : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string msg, ElementSet elemSet)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;
            View view = doc.ActiveView;
            ElementId id;

            try
            {
                Selection sel = uidoc.Selection;
                Reference r = sel.PickObject(ObjectType.Element, "Pick element to change its colour");
                id = r.ElementId;
            }
            catch (Autodesk.Revit.Exceptions.OperationCanceledException)
            {
                return Result.Cancelled;
            }

            ChangeElementColor(doc, id);

            return Result.Succeeded;
        }
        void ChangeElementColor(Document doc, ElementId id)
        {
            Color color = new Color(255, 100, 100);

            OverrideGraphicSettings ogs = new OverrideGraphicSettings();
            ogs.SetProjectionFillColor(color);
           
            using (Transaction tx = new Transaction(doc))
            {
                tx.Start("Change Element Color");
                doc.ActiveView.SetElementOverrides(id, ogs);
                tx.Commit();
            }
        }
        void GetAllFilterInfo(Document doc, View view) 
        {
            string names = null;
            FilteredElementCollector filters = new FilteredElementCollector(doc);
            filters.OfClass(typeof(ParameterFilterElement));

            ICollection<ElementId> filterIds = view.GetFilters();
            Color c = new Color(255, 255, 255);
            foreach (ElementId id in filterIds)
            {
                Element filter = doc.GetElement(id);
                OverrideGraphicSettings ogs2 = view.GetFilterOverrides(id);
                c = ogs2.ProjectionFillColor;
                if (c.IsValid)
                {
                    names += filter.Name + "  " + id.ToString() + "(" + c.Red + "," + c.Green + "," + c.Blue + ")" + "\n";
                }
            }
            Log.Logger(names, true);
        }
    }
}
