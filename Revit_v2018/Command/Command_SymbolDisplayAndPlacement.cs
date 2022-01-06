using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Revit_v2018.Defined;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revit_v2018.Command
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class Command_SymbolDisplayAndPlacement: IExternalCommand
    {
        public static ExternalCommandData commandData { get; set; }
        public Result Execute(ExternalCommandData CMD, ref string message, ElementSet elements)
        {
            commandData = CMD;

            UIApplication uiapp = CMD.Application;

            DockablePane dockable = uiapp.GetDockablePane(new DockablePaneId(new Guid("{5A1052C5-99BB-493A-BF7B-A039F050439A}")));

            dockable.Show();

            return Result.Succeeded;
        }

        
    }

    
}
