using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDtest
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class Index : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string msg, ElementSet elemSet)
        {

            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;
            IList<Element> FamilySybol = new FilteredElementCollector(doc).OfClass(typeof(FamilySymbol)).ToElements();

            //var data = (from x in FamilySybol select x.Category.Name).Distinct().ToList();
            var dataX = from x in FamilySybol
                        where x.Category.Name == "管附件"
                        select x.Name;
            foreach (var item in dataX)
            {
                Log.Logger(item, false);
            }

            Log.Logger("end", true);

            return Result.Succeeded;
        }

    }
}
