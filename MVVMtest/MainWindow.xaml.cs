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

namespace MVVMtest
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Person p1 = new Person();
        public static ViewModel viewModel = new ViewModel();
        ObservableCollection<Person> Peo { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            //DataContext = p1;//綁定數據
            
            this.lv_sss.DataContext = viewModel;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            List<string> dd = new List<string>();
            dd.Add("Tom");
            dd.Add("TomA");
            dd.Add("TomB");
            dd.Add("TomC");
            p1.Name = "李四";
            p1.Hobby = "足球";

            var gogo = new List<Students>();
            foreach (var item in dd)
            {
                gogo.Add(new Students { Name = item });
            }
            viewModel.StudentList = new ObservableCollection<Students>(gogo);
            //{
                //new Students(){ Name="Darren"},
                //new Students(){ Name="Jacky"},
                //new Students(){ Name="Andy"}
            //};

           

            //p1.Good.Add("QT");
            //Form1 form1 = new Form1();
            //form1.ShowDialog();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            p1.Age = p1.Age + 1;
            p1.Hobby = "足球";
        }

        private void lv_sss_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var a = lv_sss.SelectedItem as Students;
            Console.WriteLine(a.Name);
            //try
            //{
            //    var GetControl = GetAncestorOfType<TextBlock>(sender as ListView);
            //    var info = GetControl.DataContext as Students;
            //    if (info != null)
            //    {
            //        Console.WriteLine(info.Name);
            //    }
            //}
            //catch (Exception) { }
        }
        public T GetAncestorOfType<T>(FrameworkElement child) where T : FrameworkElement
        {
            var parent = VisualTreeHelper.GetParent(child);
            if (parent != null && !(parent is T))
                return (T)GetAncestorOfType<T>((FrameworkElement)parent);
            return (T)parent;
        }
    }
}
