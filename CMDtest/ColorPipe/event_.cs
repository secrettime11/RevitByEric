using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using JohnsonRevitAPI2.Public_Folder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDtest.ColorPipe
{
    public class event_ : IExternalEventHandler
    {
        public void Execute(UIApplication uiapp)
        {
            Set(uiapp);
            //GetAllFilterInfo(uiapp);
        }


        void Set(UIApplication uiapp)
        {
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;
            View view = doc.ActiveView;
            try
            {
                Selection sel = uidoc.Selection;
                IList<Reference> eleSelecArea = sel.PickObjects(ObjectType.Element, "框選範圍");
                List<ElementId> data = (from x in eleSelecArea select x.ElementId).ToList();
                List<Info> result = new List<Info>();
                using (Transaction tx = new Transaction(doc))
                {
                    tx.Start("Change Element Color");
                    foreach (var id in data)
                    {
                        try
                        {
                            ChangeElementColor(doc, id, result);
                        }
                        catch (Exception)
                        {

                        }
                    }
                    tx.Commit();
                }

                string symbol = "<!>";
                foreach (var item in result)
                {
                    ini.write($"{item.Date.ToString("yyyy/MM/dd")}{symbol}{item.Id}{symbol}{item.Name}{symbol}{item.Type}{symbol}{item.NowColor.R},{item.NowColor.G},{item.NowColor.B}", ini.iniPath, DateTime.Now.ToString("yyyyMMdd-mm-ss"));
                }
            }
            catch (Exception) { }



            ColorPipeForm pipeForm = new ColorPipeForm();
            pipeForm.Show();
        }

        void ChangeElementColor(Document doc, ElementId id, List<Info> result)
        {
            if (doc.GetElement(id).LookupParameter("系統類型").AsValueString() != "未定義")
            {
                Color color = new Color(Model.colorSet.R, Model.colorSet.G, Model.colorSet.B);
                OverrideGraphicSettings ogs = new OverrideGraphicSettings();
                Model.settings = doc.ActiveView.GetElementOverrides(id);
                ogs.SetProjectionFillColor(color);
                doc.ActiveView.SetElementOverrides(id, ogs);

                Info info = new Info();
                info.Date = DateTime.Now;
                info.Id = id;
                info.Name = doc.GetElement(id).Name;
                info.Type = doc.GetElement(id).LookupParameter("系統類型").AsValueString();
                info.NowColor = System.Drawing.Color.FromArgb(color.Red, color.Green, color.Blue);

                result.Add(info);
            }
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


        private Color GetAppearenceColor(Document doc, Face face)
        {
            Color appearenceColor = Color.InvalidColorValue;

            Material material = doc.GetElement(face.MaterialElementId) as Material;

            AppearanceAssetElement appearanceElem = material.Document.GetElement(material.AppearanceAssetId) as AppearanceAssetElement;
            Autodesk.Revit.DB.Visual.Asset theAsset = appearanceElem.GetRenderingAsset();

            Autodesk.Revit.DB.Visual.AssetProperty colorProperty = theAsset["generic_diffuse"];
            Type type = colorProperty.GetType();
            object apVal = null;
            try
            {
                // using .net reflection to get the value  
                var prop = type.GetProperty("Value");
                if (prop != null &&
                    prop.GetIndexParameters().Length == 0)
                {
                    apVal = prop.GetValue(colorProperty);
                }
                else
                {
                    apVal = "<No Value Property>";
                }
            }
            catch (Exception ex)
            {
                apVal = ex.GetType().Name + "-" + ex.Message;
            }

            if (apVal is DoubleArray)
            {
                var doubles = apVal as DoubleArray;

                byte r = (byte)(doubles.get_Item(0) * 255);
                byte g = (byte)(doubles.get_Item(1) * 255);
                byte b = (byte)(doubles.get_Item(2) * 255);

                appearenceColor = new Color(r, g, b);
            }

            return appearenceColor;
        }
        public Face GetFace_fromelem(Element element, Connector connector)
        {
            Face face_answer = null;
            Options options = new Options();
            options.ComputeReferences = true;
            GeometryElement geometryElement = element.get_Geometry(options);
            foreach (GeometryObject geometry in geometryElement)
            {
                int i = 0;
                GeometryInstance instance = geometry as GeometryInstance;
                if (null != instance)
                {
                    foreach (GeometryObject instObj in instance.SymbolGeometry)
                    {
                        i++;
                        Solid solid = instObj as Solid;

                        if (null == solid || 0 == solid.Faces.Size)
                        {
                            continue;
                        }

                        foreach (Face face in solid.Faces)
                        {

                            if (face.Project(connector.Origin).Equals(connector.Origin))
                            {
                                face_answer = face;
                                break;
                            }
                        }
                    }
                }
            }
            return face_answer;
        }
        public string GetName()
        {
            return "event_";
        }
    }
}
