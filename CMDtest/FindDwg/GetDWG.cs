using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDtest.FindDwg
{
    [Transaction(TransactionMode.Manual)] 
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.NoCommandData)]
    public class GetDWG : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string messages, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;
            Application app = uiapp.Application;
            //getAllCAD(doc);
            //Log.Logger("------------", false);
            //OnlyLink(doc);

            listImports(doc);
            DWGInfo dWGInfo = new DWGInfo();
            dWGInfo.Show();
            //Log.Logger("", true);
            return Result.Succeeded;
        }
        private List<string> OnlyLink(Document doc)
        {
            List<string> result = new List<string>();
            var collector = new FilteredElementCollector(doc);

            var cadLinkTypes = collector
                .OfClass(typeof(CADLinkType))
                .OfType<CADLinkType>()
                .Where(x => x.IsExternalFileReference())
                .ToList();

            foreach (var cadLinkType in cadLinkTypes)
                result.Add(cadLinkType.Name);

            return result;
        }
        private void listImports(Document doc)
        {
            FilteredElementCollector col
                  = new FilteredElementCollector(doc)
                    .OfClass(typeof(ImportInstance));

            NameValueCollection listOfViewSpecificImports
              = new NameValueCollection();

            NameValueCollection listOfModelImports
              = new NameValueCollection();

            NameValueCollection listOfUnidentifiedImports
              = new NameValueCollection();

            foreach (Element e in col)
            {
                if (e.ViewSpecific)
                {
                    string viewName = null;

                    try
                    {
                        Element viewElement = doc.GetElement(
                          e.OwnerViewId);
                        viewName = viewElement.Name;
                    }
                    catch (Autodesk.Revit.Exceptions
                      .ArgumentNullException) // just in case
                    {
                        viewName = String.Concat(
                          "Invalid View ID: ",
                          e.OwnerViewId.ToString());
                    }

                    if (e.Category != null)
                    {
                        listOfViewSpecificImports.Add(
                          importCategoryNameToFileName(
                            e.Category.Name), viewName);
                    }
                    else
                    {
                        listOfUnidentifiedImports.Add(
                          e.Id.ToString(), viewName);
                    }
                }
                else
                {
                    listOfModelImports.Add(
                      importCategoryNameToFileName(
                        e.Category.Name), e.Name);
                }
            }


            var LinkCAD = OnlyLink(doc);
            List<Model> importL = new List<Model>();
            List<Model> linkL = new List<Model>();

            if (listOfViewSpecificImports.HasKeys())
                listResults(listOfViewSpecificImports, importL, linkL, LinkCAD);

            if (listOfModelImports.HasKeys())
                listResults(listOfModelImports, importL, linkL, LinkCAD);

            if (listOfUnidentifiedImports.HasKeys())
                listResults(listOfUnidentifiedImports, importL, linkL, LinkCAD);

            Model.result_import = GetTable(importL, doc);
            Model.result_link = GetTable(linkL, doc);
        }

        private string importCategoryNameToFileName(
         string catName)
        {
            string fileName = catName;
            fileName = fileName.Trim();

            //if (fileName.EndsWith(")"))
            //{
            //    int lastLeftBracket = fileName.LastIndexOf("(");

            //    // remove left bracket
            //    if (lastLeftBracket != -1)
            //        fileName = fileName.Remove(lastLeftBracket);
            //}

            return fileName.Trim();
        }

        private void listResults(NameValueCollection listOfImports, List<Model> importL, List<Model> linkL, List<string> LinkData)
        {
            foreach (string key in listOfImports.AllKeys)
            {
                Model model = new Model();
                model.name = key;
                model.position = listOfImports.Get(key);

                if (!LinkData.Contains(key))
                    importL.Add(model);
                else
                    linkL.Add(model);
            }
        }

        public DataTable GetTable(List<Model> tmp, Document doc)
        {
            DataTable dataTable = new DataTable();
            if (tmp.Count > 0)
            {
                foreach (var item in Model.head)
                {
                    dataTable.Columns.Add(item);
                }
                int row = 0;
                foreach (var item in tmp)
                {
                    dataTable.Rows.Add();
                    dataTable.Rows[row][0] = item.name;
                    dataTable.Rows[row][1] = item.position;
                    dataTable.Rows[row][2] = doc.PathName.Split('\\').Last();
                    row++;
                }
            }
            return dataTable;
        }
    }
}