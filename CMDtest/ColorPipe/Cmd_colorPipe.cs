using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDtest.ColorPipe
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.NoCommandData)]
    public class Cmd_colorPipe: IExternalCommand
    {
        public static ExternalCommandData commandData { get; set; }
        Colorback colorback { get; set; }
        public static ExternalEvent colorback_e { get; set; }
        event_ event_ { get; set; }
        public static ExternalEvent colorevent { get; set; }
        eventb eventb { get; set; }
        public static  ExternalEvent coloreventb { get; set; }

        _3Devent _3D { get; set; }
        public static ExternalEvent event_3d { get; set; }
        public Result Execute(ExternalCommandData CMD, ref string msg, ElementSet elemSet)
        {
            commandData = CMD;

            #region Event

            colorback = new Colorback();
            colorback_e = ExternalEvent.Create(colorback);

            event_ = new event_();
            colorevent = ExternalEvent.Create(event_);
            
            _3D = new _3Devent();
            event_3d = ExternalEvent.Create(_3D);

            #endregion
            UIApplication uiapp = CMD.Application;
            Model.SystemList = new List<SystemType>();
            GetAllFilterInfo(uiapp, Model.SystemList);

            ColorPipeForm colorPipeForm = new ColorPipeForm();
            colorPipeForm.Show();

            return Result.Succeeded;
        }

        public void GetAllFilterInfo(UIApplication uiapp, List<SystemType> SystemList)
        {
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;
            View view = doc.ActiveView;
            FilteredElementCollector filters = new FilteredElementCollector(doc);
            filters.OfClass(typeof(ParameterFilterElement));
            ICollection<ElementId> filterIds = view.GetFilters();
            Color c = new Color(255, 255, 255);

            foreach (ElementId id in filterIds)
            {
                Element filter = doc.GetElement(id);
                OverrideGraphicSettings ogs2 = view.GetFilterOverrides(id);
                c = ogs2.ProjectionFillColor;
                if (c.IsValid)
                {
                    SystemType type = new SystemType();
                    type.Name = filter.Name;
                    type.SysColor = System.Drawing.Color.FromArgb(c.Red, c.Green, c.Blue);
                    SystemList.Add(type);
                }
            }
        }
    }
}
