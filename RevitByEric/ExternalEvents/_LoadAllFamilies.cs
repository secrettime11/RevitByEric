using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RevitByEric.ExternalEvents
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class _LoadAllFamilies : IExternalEventHandler
    {
        public MainWindow myWindow1 { get; set; }
        public void Execute(UIApplication uiapp)
        {
            string adsdad = "";

            try
            {
                UIDocument uidoc = uiapp.ActiveUIDocument;
                Document doc = uidoc.Document;

                string myStringMessageBox = "";

                int myInt = 0;
                foreach (ULoadAndPlace.ListView_Class myListView_Class in myWindow1.myListClass)
                {
                    List<Element> myListFamily = new FilteredElementCollector(doc).WherePasses(new ElementClassFilter(typeof(Family))).Where(x => x.Name == myListView_Class.String_Name).ToList();
                    if (myListFamily.Count == 0)
                    {
                        string myString_TempPath = "";

                        //if (myWindow1.myThisApplication.messageConst.Split('|')[0] == "Button_01_Invoke01")  //constructs a path for release directory (in program files)
                        //{
                        //    myString_TempPath = myWindow1.myThisApplication.messageConst.Split('|')[1] + myListView_Class.String_FileName;
                        //}
                        //if (myWindow1.myThisApplication.messageConst.Split('|')[0] == "Button_01_Invoke01Development") //constructs a path for development directory
                        //{
                        //    myString_TempPath = myWindow1.myThisApplication.messageConst.Split('|')[1] + @"\RevitByEric" + myListView_Class.String_FileName;
                        //}
                        myString_TempPath = @"C:\Users\user\Desktop\Task\RevitByEric\RevitByEric" + myListView_Class.String_FileName;
                        adsdad = myString_TempPath;
                        using (Transaction tx = new Transaction(doc))
                        {
                            tx.Start("Load a " + myListView_Class.String_Name);
                            doc.LoadFamily(myString_TempPath, new FamilyOptionOverWrite(), out Family myFamily);
                            tx.Commit();
                        }

                        myStringMessageBox = myStringMessageBox + Environment.NewLine + myListView_Class.String_Name;
                        myInt++;
                    }
                }

                string myStringStart = myInt.ToString() + " families have been loaded: " + Environment.NewLine + Environment.NewLine;

                MessageBox.Show(myStringStart + myStringMessageBox + Environment.NewLine + Environment.NewLine + "This only happens once per project.");

            }
            #region catch and finally
            catch (Exception ex)
            {
                Log.writeDebug("Load Error: " + Environment.NewLine + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine + myWindow1.myListClass.Count.ToString() + "/" + adsdad, true);
            }
            finally
            {
            }
            #endregion
        }

        public string GetName()
        {
            return "External Event Example";
        }

        public class FamilyOptionOverWrite : IFamilyLoadOptions
        {
            public bool OnFamilyFound(bool familyInUse, out bool overwriteParameterValues)
            {
                overwriteParameterValues = true;
                return true;
            }
            public bool OnSharedFamilyFound(Family sharedFamily, bool familyInUse, out FamilySource source, out bool overwriteParameterValues)
            {
                source = FamilySource.Family;
                overwriteParameterValues = true;
                return true;
            }
        }


    }
}
