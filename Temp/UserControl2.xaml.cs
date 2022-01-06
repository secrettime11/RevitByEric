using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
namespace Temp
{
    /// <summary>
    /// UserControl2.xaml 的互動邏輯
    /// </summary>
    public partial class UserControl2 : UserControl
    {
        Function Function = new Function();
        public UserControl2()
        {
            InitializeComponent();
            
            List<AAC> aACs = new List<AAC>();
            for (int i = 0; i < 5; i++)
            {
                AAC aC = new AAC();
                aC.Name = "3";
                aC.ID = (i-1).ToString();
                aC.Num = (i+1).ToString();
                aACs.Add(aC);
            }
            var data = (from o in aACs
                       where Convert.ToInt16(o.Name) > 0
                       select o.Name).Distinct();
            foreach (var item in data) 
            {
                Console.WriteLine("A" +item);
            }
        }

        public class AAC 
        {
            public string Name { get; set; }
            public string ID { get; set; }
            public string Num { get; set; }
        }


        ObservableCollection<LVData> LVDatas = new ObservableCollection<LVData>();
        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            List<string> data = new List<string>();
            for (int i = 0; i < 20; i++)
            {
                data.Add(i.ToString());
            }
            lstFileManager.ItemsSource = LVDatas;
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(@"C:\Users\user\Desktop\Task\RevitByEric\Revit_v2018\Icon\B.png", UriKind.RelativeOrAbsolute);
            bi.EndInit();
            
            LVDatas.Add(new LVData { Name = $"h1", Name2 = data, Pic = bi, NumA = 0 });
            LVDatas.Add(new LVData { Name = $"h1", Name2 = data, Pic = bi, NumA = 1 });
            LVDatas.Add(new LVData { Name = $"h1", Name2 = data, Pic = bi, NumA = 2 });
            LVDatas.Add(new LVData { Name = $"h1", Name2 = data, Pic = bi, NumA = 3 });
            //LVDatas.Add(new LVData { Name = $"圖片", Name2 = "Google", Pic = "http://www.google.com/intl/en_ALL/images/logo.gif" });

        }
        public class LVData
        {
            public string Name { get; set; }
            public List<string> Name2 { get; set; }
            public BitmapImage Pic { get; set; }
            public int NumA { get; set; }
        }

        private void lstFileManager_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var data = lstFileManager.SelectedItem as LVData;
                if (data != null)
                {
                    Console.WriteLine(data.Name);
                    Console.WriteLine(data.NumA);
                }
            }
            catch (Exception)
            {
            }
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
    }
}
