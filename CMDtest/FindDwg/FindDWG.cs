using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CMDtest.FindDwg
{
    [TransactionAttribute(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class FindDWG : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string messages, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;
            
            listImports(doc);

            return Result.Succeeded;
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
                // Collect all view-specific names.

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

                    if (null != e.Category)
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

            IReportImportData logOutput
              = new SimpleTextFileBasedReporter();

            if (!logOutput.init(doc.PathName))
            {
                TaskDialog.Show("FindImports",
                  "Unable to create report file");
            }
            else
            {
                if (listOfViewSpecificImports.HasKeys())
                {
                    logOutput.startReportSection(
                      "View Specific Imports");

                    listResults(listOfViewSpecificImports,
                      logOutput);
                }

                if (listOfModelImports.HasKeys())
                {
                    logOutput.startReportSection("Model Imports");
                    listResults(listOfModelImports, logOutput);
                }

                if (listOfUnidentifiedImports.HasKeys())
                {
                    logOutput.startReportSection(
                      "Unknown import instances");
                    listResults(listOfUnidentifiedImports,
                      logOutput);
                }

                if (!sanityCheckViewSpecific(
                  listOfViewSpecificImports, logOutput))
                {
                    logOutput.setWarning();
                    //TaskDialog.Show("FindImportedData", 
                    //"Possible issues found. Please review the log file");
                }

                logOutput.done();
            }
        }
        /// <summary>
        /// This is an import category. It is created from 
        /// a CAD file name, with appropriate (number) added. 
        /// We want to use the file name as a key for our 
        /// list of import instances, so strip off the 
        /// brackets. 
        /// </summary>
        private string importCategoryNameToFileName(
          string catName)
        {
            string fileName = catName;
            fileName = fileName.Trim();

            if (fileName.EndsWith(")"))
            {
                int lastLeftBracket = fileName.LastIndexOf("(");

                if (-1 != lastLeftBracket)
                    fileName = fileName.Remove(lastLeftBracket); // remove left bracket
            }

            return fileName.Trim();
        }

        private void listResults(
          NameValueCollection listOfImports,
          IReportImportData logFile)
        {

            foreach (String key in listOfImports.AllKeys)
            {
                logFile.logItem(key + ": "
                  + listOfImports.Get(key));
            }
        }

        /// <summary>
        /// Run a few basic sanity checks on the list of 
        /// view-specific imports. 
        /// View-specific sanity is not the same as model 
        /// sanity. Neither is necessarily sane.
        /// True means possibly sane, false means probably 
        /// not.
        /// </summary>
        private bool sanityCheckViewSpecific(
          NameValueCollection listOfImports,
          IReportImportData logFile)
        {
            logFile.startReportSection(
              "Sanity check report for view-specific imports");

            bool status = true;

            // Count number of entities per key.

            foreach (String key in listOfImports.AllKeys)
            {
                string[] levels = listOfImports.GetValues(key);
                if (levels != null && levels.GetLength(0) > 1)
                {
                    logFile.logItem("CAD data " + key
                      + " appears to have been imported in "
                      + "Current View Only mode multiple times. "
                      + "It is present in views "
                      + listOfImports.Get(key));
                    status = false;
                }
            }
            return status;
        }
    }

}
