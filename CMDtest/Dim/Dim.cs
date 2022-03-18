using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDtest.Dim
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.NoCommandData)]
    public class Dim: IExternalCommand
    {
        public static ExternalCommandData commandData { get; set; }
        public Result Execute(ExternalCommandData CMD, ref string msg, ElementSet elemSet)
        {
            commandData = CMD;
            DimForm dimForm = new DimForm();
            dimForm.Show();

            return Result.Succeeded;
        }
    }
}
