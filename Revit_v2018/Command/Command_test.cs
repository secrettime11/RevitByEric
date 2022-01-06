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
    public class Command_test: IExternalCommand
    {
        public static ExternalCommandData commandData { get; set; }
        public Result Execute(ExternalCommandData CMD, ref string message, ElementSet elements)
        {
            commandData = CMD;

            UIApplication uiapp = CMD.Application;

            DockablePane dockable = uiapp.GetDockablePane(new DockablePaneId(new Guid("{18C5001E-63CC-4172-8ADA-07DF9A63256B}")));

            dockable.Show();

            return Result.Succeeded;
        }
    }
}
