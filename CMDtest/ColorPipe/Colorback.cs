using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDtest.ColorPipe
{
    public class Colorback : IExternalEventHandler
    {
        public void Execute(UIApplication uiapp)
        {
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;
            using (Transaction tx = new Transaction(doc))
            {
                tx.Start("Change Element Color");
                ChangeElementColor(doc, Model.SelectEleList);
                tx.Commit();
            }
        }

        void ChangeElementColor(Document doc, List<SelectEle> SelectEleList)
        {
            if (Model.Status == "修改色")
            {
                foreach (var item in SelectEleList)
                {
                    Color color = new Color(item.SysColor.R, item.SysColor.G, item.SysColor.B);
                    OverrideGraphicSettings ogs = new OverrideGraphicSettings();
                    ogs.SetProjectionFillColor(color);
                    ElementId elementId = new ElementId(Convert.ToInt32(item.Id));
                    doc.ActiveView.SetElementOverrides(elementId, ogs);
                }
            }
            else if (Model.Status == "全部復原") 
            {
                List<Element> OtherInstance = new FilteredElementCollector(doc).WherePasses(new ElementClassFilter(typeof(FamilyInstance))).ToList();
                FilteredElementCollector PipeCollector = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_PipeCurves);

                List<ElementId> allCollector = new List<ElementId>();


                foreach (var item in OtherInstance)
                {
                    allCollector.Add(item.Id);
                }
                foreach (var item in PipeCollector.ToElements())
                {
                    allCollector.Add(item.Id);
                }

                foreach (var item in allCollector)
                {
                    try
                    {
                        OverrideGraphicSettings ogs = new OverrideGraphicSettings();
                        doc.ActiveView.SetElementOverrides(item, ogs);
                    }
                    catch (Exception)
                    {
                    }
                }
               
            }
            else
            {
                foreach (var item in SelectEleList)
                {
                    OverrideGraphicSettings ogs = new OverrideGraphicSettings();
                    ElementId elementId = new ElementId(Convert.ToInt32(item.Id));
                    doc.ActiveView.SetElementOverrides(elementId, ogs);
                }
            }
            //Log.Logger("", true);
        }
        public string GetName()
        {
            return "Colorback";
        }
    }
}