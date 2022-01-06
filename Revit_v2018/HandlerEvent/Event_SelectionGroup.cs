using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revit_v2018.HandlerEvent
{
    public class Event_SelectionGroup : IExternalEventHandler
    {
        public event EventHandler<SelectionGroupCompletedEventArgs> Completed;
        public void Execute(UIApplication uiapp)
        {
            var uidoc = uiapp.ActiveUIDocument;

            var doc = uidoc?.Document;

            if (doc == null)
            {
                return;
            }

            var revitVersion = doc.Application.VersionNumber;


            var collector = new FilteredElementCollector(doc);

            var theLayers = collector
                .OfClass(typeof(SelectionFilterElement))
                .Select(sfe => sfe.Name)
                .ToList();

            OnCompleted(new SelectionGroupCompletedEventArgs(revitVersion, theLayers));
        }

        private void OnCompleted(SelectionGroupCompletedEventArgs e)
        {
            Completed?.Invoke(this, e);
        }

        public string GetName()
        {
            return "MyEvent";
        }
    }

    public class SelectionGroupCompletedEventArgs : EventArgs
    {
        public SelectionGroupCompletedEventArgs(string revitVer, IList<string> theLayers)
        {
            TheLayers = theLayers;
        }
        public IList<string> TheLayers { get; }
    }
}
