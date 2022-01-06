using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Revit_v2018.HandlerEvent
{
    public class Event_LoadFamilySymbol : IExternalEventHandler
    {
        public void Execute(UIApplication uiapp)
        {
            try
            {
                UIDocument uidoc = uiapp.ActiveUIDocument;
                Document doc = uidoc.Document;

                string myStringMessageBox = "";

                int myInt = 0;
                foreach (Defined.Args.ListView_Class myListView_Class in DockableUI.UI_FamilySymbolLoadAndPlace.myListClass)
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
                        myString_TempPath = @"C:\Users\user\Desktop\Task\RevitByEric\Revit_v2018" + myListView_Class.String_FileName;
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

                TaskDialog.Show("Info",myStringStart + myStringMessageBox + Environment.NewLine + Environment.NewLine + "This only happens once per project.");

            }
            #region catch and finally
            catch (Exception ex)
            {
                Log.writeDebug("Load Error: " + Environment.NewLine + ex.Message + Environment.NewLine + ex.InnerException + Environment.NewLine, true);
            }
            finally
            {
            }
            #endregion
        }
        public string GetName()
        {
            return "Event_LoadFamilySymbol";
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
