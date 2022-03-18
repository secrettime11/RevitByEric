using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDtest.ColorPipe
{
    public class eventb : IExternalEventHandler
    {
        public void Execute(UIApplication uiapp)
        {

            Set(uiapp);
        }
        void Set(UIApplication uiapp)
        {
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;
            View view = doc.ActiveView;
            try
            {
                Selection sel = uidoc.Selection;
                IList<Reference> eleSelecArea = sel.PickObjects(ObjectType.Element, "框選範圍");
                List<ElementId> data = (from x in eleSelecArea select x.ElementId).ToList();
                using (Transaction tx = new Transaction(doc))
                {
                    tx.Start("Change Element Color");
                    foreach (var id in data)
                    {
                        ChangeElementColor(doc, id);
                    }
                    tx.Commit();
                }
            }
            catch (Exception) { }

        }

        void ChangeElementColor(Document doc, ElementId id)
        {
            OverrideGraphicSettings overrideGraphicSettings = new OverrideGraphicSettings();
            doc.ActiveView.SetElementOverrides(id, overrideGraphicSettings);
        }

        public string GetName()
        {
            return "eventb";
        }
    }
}
