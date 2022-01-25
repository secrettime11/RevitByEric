using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Revit_v2018.Defined;
using System.Collections.ObjectModel;
using Revit_v2018.ViewModel;

namespace Revit_v2018.HandlerEvent
{
    public class Event_GetFamilySymbol : IExternalEventHandler
    {
        public void Execute(UIApplication uiapp)
        {
            Args.Category.Clear();
            Args.Category_Sort.Clear();
            Args.AllData.Clear();

            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;
            IList<Element> FamilySybol = new FilteredElementCollector(doc).OfClass(typeof(FamilySymbol)).ToElements();

            foreach (FamilySymbol item in FamilySybol)
            {
                Args.Data data = new Args.Data();
                data.Id = item.UniqueId;
                data.Category = item.Category.Name;
                data.Family = item.FamilyName;
                data.Symbol = item.Name;
                data.FamilySymbol_ = item;
                data.ElementType_ = doc.GetElement(item.GetTypeId()) as ElementType;
                Args.AllData.Add(data);
            }

            var Category = (from o in Args.AllData select o.Category).Distinct();

            // 現有族群
            foreach (var item in Category)
                Args.Category.Add(item);

            Args.Category_Sort = ini.read(ini.iniPath, ini.ini_CategorySortName);

            var displayData = new List<VM_Category>();
            try
            {
                if (Args.Category_Sort.Count == 0)
                {
                    // 沒有自定義ini 加入全部
                    foreach (var item in Args.Category)
                    {
                        displayData.Add(new VM_Category { Name = item });
                    }
                }
                else
                {

                    foreach (var item in Args.Category_Sort)
                    {
                        var familyData = from x in Args.AllData
                                         where x.Category == item
                                         select x.Family;
                        // 現有族群 未包含 自訂義族群 => 移除
                        if (!familyData.Any())
                        {
                            Args.Category_Sort.Remove(item);
                        }
                        else
                        {
                            displayData.Add(new VM_Category { Name = item });
                            Args.Category.Remove(item);
                        }
                    }
                }
            }
            catch (Exception) { }

            DockableUI.UI_SymbolDisplayAndPlacement.VM_CategoryList.CList = new ObservableCollection<VM_Category>(displayData);
        }

        public string GetName()
        {
            return "Event_GetFamilySymbol";
        }
    }
}
