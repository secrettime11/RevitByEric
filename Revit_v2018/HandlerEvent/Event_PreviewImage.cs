using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revit_v2018.HandlerEvent
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class Event_PreviewImage: IExternalEventHandler
    {
        public void Execute(UIApplication app)
        {
            Document doc = app.ActiveUIDocument.Document;

            FilteredElementCollector collector = new FilteredElementCollector(doc);

            collector.OfClass(typeof(FamilyInstance));

            foreach (FamilyInstance fi in collector)
            {
                ElementId typeId = fi.GetTypeId();

                ElementType type = doc.GetElement(typeId) as ElementType;

                System.Drawing.Size imgSize = new System.Drawing.Size(50, 50);

                System.Drawing.Bitmap image = type.GetPreviewImage(imgSize);

                //Model.Data.images = image;
                //Model.Data.TypeOne = fi.Symbol.FamilyName;
                //Model.Data.TypeTwo = fi.Name;
                //Log.Logger($"{Model.Data.TypeOne} / {Model.Data.TypeTwo}");
                break;
            }
        }

        public string GetName()
        {
            return "ImageEvent";
        }
    }
}
