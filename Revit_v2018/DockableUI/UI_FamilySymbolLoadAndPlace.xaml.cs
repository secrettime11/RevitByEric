using Autodesk.Revit.DB;
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

namespace Revit_v2018.DockableUI
{
    /// <summary>
    /// UI_FamilySymbolLoadAndPlace.xaml 的互動邏輯
    /// </summary>
    public partial class UI_FamilySymbolLoadAndPlace : UserControl, IDockablePaneProvider
    {

        public static List<Defined.Args.ListView_Class> myListClass { get; set; } = new List<Defined.Args.ListView_Class>();


        public HandlerEvent.Event_LoadFamilySymbol Event_LoadFamilySymbol { get; set; }
        public ExternalEvent Event_LoadFamilySymbol_ { get; set; }

        public HandlerEvent.Event_PlaceFamilySymbol Event_PlaceFamilySymbol { get; set; }
        public ExternalEvent Event_PlaceFamilySymbol_ { get; set; }


        public UI_FamilySymbolLoadAndPlace()
        {
            foreach (string myStrrr in Defined.Args.Families_ThatMustBeLoaded.ListStringMustHaveFamilies)
            {
                myListClass.Add(new Defined.Args.ListView_Class() { String_Name = myStrrr, String_FileName = @"\Families\" + myStrrr + ".rfa" });
            }

            InitializeComponent();

            Event_LoadFamilySymbol = new HandlerEvent.Event_LoadFamilySymbol();
            Event_LoadFamilySymbol_ = ExternalEvent.Create(Event_LoadFamilySymbol);

            Event_PlaceFamilySymbol = new HandlerEvent.Event_PlaceFamilySymbol();
            Event_PlaceFamilySymbol_ = ExternalEvent.Create(Event_PlaceFamilySymbol);

        }

        public void SetupDockablePane(DockablePaneProviderData data)
        {
            data.FrameworkElement = this;
            data.InitialState = new DockablePaneState();
            data.InitialState.DockPosition = DockPosition.Tabbed;
            data.InitialState.TabBehind = DockablePanes.BuiltInDockablePanes.ProjectBrowser;
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

                Event_LoadFamilySymbol_.Raise();
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
        private void ListViewItem_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int eL = -1;

            try
            {
                UIDocument uidoc = Command.Command_FamilySymbolLoadAndPlace.commandData.Application.ActiveUIDocument;
                Document doc = uidoc.Document;

                Defined.Args.ListView_Class myListView_Class = myListView.SelectedItem as Defined.Args.ListView_Class;

                IEnumerable<Element> myIEnumerableElement = new FilteredElementCollector(doc).WherePasses(new ElementClassFilter(typeof(Family))).Where(x => x.Name == myListView_Class.String_Name);

                if (myIEnumerableElement.Count() == 0)
                {
                    TaskDialog.Show("Info", myListView_Class.String_Name + Environment.NewLine + Environment.NewLine + "Is not present in model" + Environment.NewLine + "...please click the 'Load all families' button below");
                    return;
                }
                FamilySymbol myFamilySymbol_Carrier = doc.GetElement(((Family)myIEnumerableElement.First()).GetFamilySymbolIds().First()) as FamilySymbol;

                Event_PlaceFamilySymbol.myFamilySymbol = myFamilySymbol_Carrier;
                Event_PlaceFamilySymbol_.Raise();

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

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            int eL = -1;

            try
            {
                myListView.ItemsSource = myListClass;
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
    }
}
