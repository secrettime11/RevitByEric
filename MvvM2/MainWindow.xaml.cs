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

namespace MvvM2
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        ViewModel viewModel = new ViewModel();
        public MainWindow()
        {
            InitializeComponent();
            viewModel.StudentList = new ObservableCollection<Students>() {
            new Students(){ Id=1, Age=11, Name="Tom"},
            new Students(){ Id=2, Age=12, Name="Darren"},
            new Students(){ Id=3, Age=13, Name="Jacky"},
            new Students(){ Id=4, Age=14, Name="Andy"}
        };
            this.lbStudent.DataContext = viewModel;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            viewModel.StudentList[1] = new Students() { Id = 4, Age = 14, Name = "这是一个集合改变" };

            viewModel.StudentList = new ObservableCollection<Students>() 
            {
                new Students(){ Id=19, Age=111, Name="这是变化后的几何"},
                new Students(){ Id=29, Age=121, Name="这是变化后的几何"},
                new Students(){ Id=39, Age=131, Name="这是变化后的几何"},
                new Students(){ Id=49, Age=141, Name="这是变化后的几何"}
            };
            viewModel.StudentList[2].Name = "这是一个属性改变";
        }
    }
}
