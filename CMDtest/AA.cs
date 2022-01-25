using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDtest
{
	[Transaction(TransactionMode.Manual)]
	[Regeneration(RegenerationOption.Manual)]
	public class AA: IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string msg, ElementSet elemSet) 
        {

			UIApplication uiapp = commandData.Application;
			UIDocument uidoc = uiapp.ActiveUIDocument;
			Document doc = uidoc.Document;
			Selection selection = commandData.Application.ActiveUIDocument.Selection;


			Element elem = doc.GetElement(selection.GetElementIds().FirstOrDefault());
			Options opt = new Options();
			opt.ComputeReferences = true;
			opt.IncludeNonVisibleObjects = true;
			opt.View = doc.ActiveView;
			Reference ptRef = null;
			foreach (var geoObj in elem.get_Geometry(opt))
			{
				Curve cv = geoObj as Curve;
				if (cv == null) continue;
				ptRef = cv.GetEndPointReference(0);
			}
			if (ptRef != null)
			{
				TaskDialog.Show("debug", ptRef.ConvertToStableRepresentation(doc));
			}

			return Result.Succeeded;
        }
    }
}
