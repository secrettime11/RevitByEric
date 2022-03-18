using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDtest.FindDwg
{
    public class importH: IExternalEventHandler
    {
        public void Execute(UIApplication uiapp)
        {
            import_dwg(uiapp, @"C:\Users\user\Desktop\範例圖檔\給水均等表.dwg");
        }
        private void import_dwg(UIApplication uiapp, string dwg_filepath)
        {
            UIDocument uiDoc = uiapp.ActiveUIDocument;
            Document document = uiDoc.Document;

            Transaction transaction = new Transaction(document, "Create Schedules");
            transaction.Start();

            ElementId elementid = null;
            DWGImportOptions options = new DWGImportOptions();

            //options.SetRefPoint(new XYZ(0, 0, 0));
            options.Placement = Autodesk.Revit.DB.ImportPlacement.Centered;
            options.OrientToView = true;
            options.ThisViewOnly = true;
            options.Unit = ImportUnit.Default;

            document.Import(dwg_filepath, options, document.ActiveView, out elementid);

            transaction.Commit();
        }
        public string GetName()
        {
            return "importH";
        }
    }
}
