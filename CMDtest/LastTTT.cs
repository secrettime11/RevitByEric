using Autodesk.Revit.ApplicationServices;
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
    public class LastTTT : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string msg, ElementSet elemSet)
        {
            var doc = commandData.Application.ActiveUIDocument.Document;
            var categories = new List<BuiltInCategory>() { BuiltInCategory.OST_GenericModel };
            var uiapp = new UIApplication(doc.Application);
            var uidoc = uiapp.ActiveUIDocument;
            var selector = new TypeSelectionFilter(categories);//自己可封装一个Category过滤器
            var elems = uidoc.Selection.PickElementsByRectangle(selector, "请框选构件!");
            if (elems != null && elems.Count > 1)
            {
                var refArray = new ReferenceArray();
                foreach (FamilyInstance elem in elems)
                {
                    refArray.Append(elem.GetReferences(FamilyInstanceReferenceType.CenterLeftRight).FirstOrDefault());
                }
                XYZ first = (elems[0].Location as LocationPoint).Point;
                XYZ second = (elems[1].Location as LocationPoint).Point;
                XYZ direction = first - second;

                using (var tran = new Transaction(doc, "Test"))
                {
                    tran.Start();
                    Line line = Line.CreateUnbound(first, direction);
                    doc.Create.NewDimension(doc.ActiveView, line, refArray);
                    tran.Commit();
                }
            }

            return Result.Succeeded;
        }



        private void GGG(ExternalCommandData commandData)
        {
            UIDocument uiDoc = commandData.Application.ActiveUIDocument;
            Document doc = uiDoc.Document;
            Selection selection = uiDoc.Selection;

            IList<Reference> eRef = selection.PickObjects(ObjectType.Element, "框選範圍");

            List<ElementId> data = (from x in eRef select x.ElementId).ToList();
            StringBuilder sb = new StringBuilder();
            foreach (ElementId eid in data)
            {
                Element e = doc.GetElement(eid);
                sb.Append($"{e.Name}" + Environment.NewLine);
            }
            TaskDialog.Show("title : ", sb.ToString());
        }
    }
    public class TypeSelectionFilter : ISelectionFilter
    {
        private List<BuiltInCategory> _cates;
        public TypeSelectionFilter(List<BuiltInCategory> cates)
        {
            _cates = cates;
        }
        public bool AllowElement(Element elem)
        {
            foreach (var c in _cates)
            {
                if (elem.Category.Id.IntegerValue.Equals((int)c))
                {
                    return true;
                }
            }
            return false;
        }

        public bool AllowReference(Reference reference, XYZ position)
        {
            return true;
        }
    }
}
