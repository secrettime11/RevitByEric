using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revit_v2018.Command
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    internal class Command_FamilySymbolLoadAndPlace : IExternalCommand
    {
        public static ExternalCommandData commandData { get; set; }
        public Result Execute(ExternalCommandData cD, ref string message, ElementSet elements)
        {
            commandData = cD;
            UIApplication uiapp = cD.Application;

            DockablePane pane = uiapp.GetDockablePane(new DockablePaneId(new Guid("{9E105067-C67F-4FC9-96C2-B78422D6AF1A}")));

            pane.Show();

            return Result.Succeeded;
        }
    }
}
