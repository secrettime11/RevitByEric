using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDtest.ColorPipe
{
    public class _3Devent : IExternalEventHandler
    {
        public void Execute(UIApplication uiapp)
        {
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;
            ICollection<ElementId> View3DIDList = queryAllView3D(doc);
            bool error  = false;
            var templateName = Model.templateName;
            View ViewTemplate = null;
            foreach (ElementId view3DId in View3DIDList)
            {
                Element viewEle = doc.GetElement(view3DId);
                var view = viewEle as View;
                if (viewEle.Name == Model.NameOf3D)
                {
                    error = true;
                    break;
                }
                else if (viewEle.Name == templateName && view.IsTemplate == true)
                {
                    ViewTemplate = view;
                }
            }

            if (!error) 
            {
                List<BoundingBoxXYZ> boundingBoxes = new List<BoundingBoxXYZ>();

                foreach (var item in Model.NowEleId)
                {
                    boundingBoxes.Add(doc.GetElement(new ElementId(Convert.ToInt32(item))).get_BoundingBox(doc.ActiveView));
                }

                double Xmax = (from x in boundingBoxes select x.Max.X).Max();
                double Ymax = (from x in boundingBoxes select x.Max.Y).Max();
                double Zmax = (from x in boundingBoxes select x.Max.Z).Max();

                double Xmin = (from x in boundingBoxes select x.Min.X).Min();
                double Ymin = (from x in boundingBoxes select x.Min.Y).Min();
                double Zmin = (from x in boundingBoxes select x.Min.Z).Min();
                ViewFamilyType viewFamilyType3D = new FilteredElementCollector(doc)
                                                       .OfClass(typeof(ViewFamilyType))
                                                       .Cast<ViewFamilyType>()
                                                       .FirstOrDefault(x => ViewFamily.ThreeDimensional == x.ViewFamily);
                View3D view3D = null;

                using (Transaction tr = new Transaction(doc, "Run"))
                {
                    tr.Start();
                    view3D = View3D.CreateIsometric(doc, viewFamilyType3D.Id);
                    view3D.Name = Model.NameOf3D;
                    view3D.DisplayStyle = DisplayStyle.ShadingWithEdges;
                    view3D.DetailLevel = ViewDetailLevel.Fine;
                    //view3D.SaveOrientationAndLock();
                    view3D.Scale = 20;
                    //Log.Logger(,true);
                    view3D.ApplyViewTemplateParameters(ViewTemplate);
                    tr.Commit();
                }
                uidoc.ActiveView = view3D; // 開啟新生成的3D視圖
                BoundingBoxXYZ boundingBox = view3D.GetSectionBox();
                boundingBox.Min = new XYZ(Xmin, Ymin, Zmin);
                boundingBox.Max = new XYZ(Xmax, Ymax, Zmax);
                boundingBox.Min += new XYZ(0, 0, 0);
                boundingBox.Max += new XYZ(0, 0, 16.404);

                using (Transaction tr = new Transaction(doc, "show"))
                {
                    tr.Start();
                    try
                    {
                        view3D.SetSectionBox(boundingBox); // 將視圖得頗面框大小設為與boundingBox一樣大
                        view3D.IsSectionBoxActive = true; // 是否開啟剖面框
                    }
                    catch (Exception ee)
                    { TaskDialog.Show("title", ee.Message); }

                    tr.Commit();
                }
            }
            else
            {
                TaskDialog.Show("錯誤", "3D視圖名稱重複。");
            }
        }
        /// <summary>
        /// 所有3D視圖
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public ICollection<ElementId> queryAllView3D(Document doc)
        {
            FilteredElementCollector viewFamilyCollector = new FilteredElementCollector(doc);
            viewFamilyCollector.OfCategory(BuiltInCategory.OST_Views);
            viewFamilyCollector.OfClass(typeof(View3D));
            ICollection<ElementId> View3DIDList = viewFamilyCollector.ToElementIds();
            return View3DIDList;
        }
        
        public string GetName()
        {
            return "_3Devent";
        }
    }
}
