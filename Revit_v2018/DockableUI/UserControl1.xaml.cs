using Autodesk.Revit.UI;
using Revit_v2018.Defined;
using Revit_v2018.tets;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// UserControl1.xaml 的互動邏輯
    /// </summary>
    public partial class UserControl1 : UserControl, IDockablePaneProvider
    {
        public static tets.ViewModel viewModel = new tets.ViewModel();
        public UserControl1()
        {
            InitializeComponent();
            this.lv_sss.DataContext = viewModel;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            List<string> dd = new List<string>();
            dd = ini.read(ini.iniPath, ini.ini_CategorySortName);
            var gogo = new List<Students>();
            foreach (var item in dd)
            {
                gogo.Add(new Students { Name = item });
            }
            viewModel.StudentList = new ObservableCollection<Students>(gogo);
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
           
        }
        public void SetupDockablePane(DockablePaneProviderData data)
        {
            data.FrameworkElement = this;
            data.InitialState = new DockablePaneState();
            data.InitialState.DockPosition = DockPosition.Right;
            data.InitialState.TabBehind = DockablePanes.BuiltInDockablePanes.ProjectBrowser;
        }

        private void lv_sss_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Console.WriteLine(lv_sss.SelectedItem.ToString());
        }
    }
}
