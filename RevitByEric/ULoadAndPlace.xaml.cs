﻿using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RevitByEric
{
    public static class Families_ThatMustBeLoaded
    {
        public static string myString00 = "Furniture Chair Executive";
        public static string myString01 = "Furniture Chair Viper";
        public static string myString02 = "Furniture Couch Viper";
        //public static string myString03 = "Furniture Desk";
        //public static string myString04 = "Furniture Table Dining Round w Chairs";
        //public static string myString05 = "Furniture Table Night Stand";
        //public static string myString06 = "Generic Adaptive Nerf Gun";
        //public static string myString07 = "Generic Model Man Sitting Eating";
        //public static string myString08 = "Generic Model Man Women Construction Worker";
        //public static string myString09 = "Generic Model Tipping Hat Man";
        //public static string myString10 = "MiniDigger";

        public static List<string> ListStringMustHaveFamilies = new List<string>() { myString00, myString01, myString02/*, myString03, myString04, myString05, myString06, myString07, myString08, myString09, myString10*/ };
    }

    /// <summary>
    /// ULoadAndPlace.xaml 的互動邏輯
    /// </summary>
    public partial class ULoadAndPlace : Window
    {
        public ExternalCommandData commandData { get; set; }
        public MainWindow myWindow1 { get; set; }

        public class ListView_Class
        {
            public string String_Name { get; set; }
            public string String_FileName { get; set; }
        }

        public ULoadAndPlace(ExternalCommandData cD)
        {
            commandData = cD;

            int eL = -1;

            try
            {
                InitializeComponent();
                this.Top = Properties.Settings1.Default.Win3Top;
                this.Left = Properties.Settings1.Default.Win3Top;
            }

            #region catch and finally
            catch (Exception ex)
            {
                Log.writeDebug("Window0506_LoadAndPlaceFamilies, error line:" + eL + Environment.NewLine + ex.Message + Environment.NewLine + ex.InnerException, true);
            }
            finally
            {
            }
            #endregion
        }

        private void ListViewItem_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int eL = -1;

            try
            {
                UIDocument uidoc = commandData.Application.ActiveUIDocument;
                Document doc = uidoc.Document;

                ListView_Class myListView_Class = myWindow1.UloadAndPlace.myListView.SelectedItem as ListView_Class;

                IEnumerable<Element> myIEnumerableElement = new FilteredElementCollector(doc).WherePasses(new ElementClassFilter(typeof(Family))).Where(x => x.Name == myListView_Class.String_Name);

                if (myIEnumerableElement.Count() == 0)
                {
                    MessageBox.Show(myListView_Class.String_Name + Environment.NewLine + Environment.NewLine + "Is not present in model" + Environment.NewLine + "...please click the 'Load all families' button below");
                    return;
                }
                FamilySymbol myFamilySymbol_Carrier = doc.GetElement(((Family)myIEnumerableElement.First()).GetFamilySymbolIds().First()) as FamilySymbol;

                myWindow1.PlaceAFamily_.myFamilySymbol = myFamilySymbol_Carrier;
                myWindow1.Event_PlaceAFamily.Raise();

            }

            #region catch and finally
            catch (Exception ex)
            {
                Log.writeDebug("ListViewItem_PreviewMouseDoubleClick, error line:" + eL + Environment.NewLine + ex.Message + Environment.NewLine + ex.InnerException, true);
            }
            finally
            {
            }
            #endregion

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            int eL = -1;

            try
            {

                myListView.ItemsSource = myWindow1.myListClass;
            }

            #region catch and finally
            catch (Exception ex)
            {
               Log.writeDebug("Window_Loaded, error line:" + eL + Environment.NewLine + ex.Message + Environment.NewLine + ex.InnerException, true);
            }
            finally
            {
            }
            #endregion
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            int eL = -1;

            try
            {
                Properties.Settings1.Default.Win3Top = this.Top;
                Properties.Settings1.Default.Win3Left = this.Left;
                Properties.Settings1.Default.Save();

            }

            #region catch and finally
            catch (Exception ex)
            {
                Log.writeDebug("Window_Closing, error line:" + eL + Environment.NewLine + ex.Message + Environment.NewLine + ex.InnerException, true);
            }
            finally
            {
            }
            #endregion

        }
        private void myButton_LoadAllFamilies_Click(object sender, RoutedEventArgs e)
        {
            int eL = -1;

            try
            {
                MessageBoxResult result = System.Windows.MessageBox.Show("Loading all families will take approx 5 min?", "Continue?", System.Windows.MessageBoxButton.YesNoCancel);

                if (result != MessageBoxResult.Yes)
                {
                    return;
                }


                myWindow1.Event_LoadAllFamilies.Raise();
            }

            #region catch and finally
            catch (Exception ex)
            {
                Log.writeDebug("myButton_LoadAllFamilies_Click, error line:" + eL + Environment.NewLine + ex.Message + Environment.NewLine + ex.InnerException, true);
            }
            finally
            {
            }
            #endregion
        }

    }
}
