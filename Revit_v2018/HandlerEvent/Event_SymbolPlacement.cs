using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revit_v2018.HandlerEvent
{
    public class Event_SymbolPlacement : IExternalEventHandler
    {
        public void Execute(UIApplication uiapp)
        {
            UIDocument uidoc = uiapp.ActiveUIDocument;
            FamilySymbol symbol = Defined.Args.AllData.Where(x => x.Id == Defined.Args.NowObj).FirstOrDefault().FamilySymbol_;
            try
            {
                uidoc.PromptForFamilyInstancePlacement(symbol);
            }
            catch (Exception)
            { }
        }

        public string GetName()
        {
            return "Event_SymbolPlacement";
        }

    }
}
