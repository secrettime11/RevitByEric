using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Temp
{
    /// <summary>
    /// UserControl1.xaml 的互動邏輯
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }
        List<string> data = new List<string>();
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                data.Add($"你好 {i + 1}");
                lstFileManager.Items.Add($"你好 {i + 1}");
            }
        }

        private void lv_category_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Console.WriteLine(lv_category.SelectedItem.ToString());
        }

        private void btn_search_Click(object sender, RoutedEventArgs e)
        {
            //BitmapImage bi = new BitmapImage();
            //bi.BeginInit();
            //bi.UriSource = new Uri(@"C:\Users\user\Desktop\Task\RevitByEric\Revit_v2018\Icon\B.png", UriKind.RelativeOrAbsolute);
            //bi.EndInit();
            //bi.Freeze();

            //Image image = new Image();
            //image.Name = "Y1";
            //image.Source = bi;
            //image.MouseDown += dynamic_Img_MouseDown;
            //panel_img.Children.Add(image);
        }

        private void dynamic_Img_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image ImageProperty = sender as Image;

            MessageBox.Show(ImageProperty.Name);
        }

        private void btn_load_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<LVData> LVDatas = new ObservableCollection<LVData>();
            lstFileManager.ItemsSource = LVDatas;

            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(@"C:\Users\user\Desktop\Task\RevitByEric\Revit_v2018\Icon\B.png", UriKind.RelativeOrAbsolute);
            bi.EndInit();
            bi.Freeze();

            LVData vData = new LVData();
            //Dictionary<string, string> dic = new Dictionary<string, string>();
            //dic.Add("N95", "Ydkaopwkdqpkdpqkwp");
            //dic.Add("N96", "ajdjoijwodjqjpjqop");
            vData.Family_ = "Taiwlpq,,,,l;a[ddd4443";
            vData.Symbol_ = "jpjqop";
            vData.Img_ = bi;

            LVDatas.Add(new LVData { Family_ = vData.Family_, Symbol_ = vData.Symbol_, Img_ = vData.Img_,Lname= "45"});

            //dic.Add("N97", "Ydkaopwkdqpkdpqkwp");
            //dic.Add("N98", "ajdjoijwodjqjpjqop");
            vData.Family_ = ">OIIOM";
            vData.Symbol_ = "pqkwp";
            vData.Img_ = bi;
            LVDatas.Add(new LVData { Family_ = vData.Family_, Symbol_ = vData.Symbol_, Img_ = vData.Img_ ,Lname="46"});
            LVDatas.Add(new LVData { Family_ = vData.Family_, Symbol_ = vData.Symbol_, Img_ = vData.Img_ ,Lname="46"});
            LVDatas.Add(new LVData { Family_ = vData.Family_, Symbol_ = vData.Symbol_, Img_ = vData.Img_ ,Lname="46"});
            LVDatas.Add(new LVData { Family_ = vData.Family_, Symbol_ = vData.Symbol_, Img_ = vData.Img_ ,Lname="46"});
            LVDatas.Add(new LVData { Family_ = vData.Family_, Symbol_ = vData.Symbol_, Img_ = vData.Img_ ,Lname="46"});
            LVDatas.Add(new LVData { Family_ = vData.Family_, Symbol_ = vData.Symbol_, Img_ = vData.Img_ ,Lname="46"});
        }

        public class LVData
        {
            public string Id { get; set; }
            public string Lname { get; set; }
            public string Family_ { get; set; }
            public string Symbol_ { get; set; }
            public BitmapImage Img_ { get; set; }

        }

        private void lstFileManager_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView listView = FindFirstVisualChild<ListView>(lstFileManager, "F1");
            if (listView != null)
            {
                var a = listView.SelectedIndex;
                var ab = listView.SelectedItem as LVData;
                Console.WriteLine(ab.Lname);
            }
            //DataTemplate dt = lstFileManager.ItemTemplate;
            //DependencyObject dio = dt.LoadContent();
            //foreach (var timeLine in FindVisualChildren<ListView>(dio))
            //{
            //    if (timeLine.Name == "xking")
            //    {
            //        var qq = timeLine.SelectedItem;
            //        Console.WriteLine(qq.ToString());
            //    }
            //}
            //var data = lstFileManager.SelectedItem as LVData;

            //if (data != null)
            //{

            //};
        }
        public T FindFirstVisualChild<T>(DependencyObject obj, string childName) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is T && child.GetValue(NameProperty).ToString() == childName)
                {
                    return (T)child;
                }
                else
                {
                    T childOfChild = FindFirstVisualChild<T>(child, childName);
                    if (childOfChild != null)
                    {
                        return childOfChild;
                    }
                }
            }
            return null;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void btn_test_Click(object sender, RoutedEventArgs e)
        {
            F_Setting f_Setting = new F_Setting();
            f_Setting.Show();
        }


        private void txt_search_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filter = this.txt_search.Text.Trim();
            this.Display(filter);
        }
        private void Display(string filter)
        {
            this.lstFileManager.Items.Clear();

            var result = data.Where(x => x.Contains(filter)).ToList();

            foreach (var item in result)
                lstFileManager.Items.Add(item);
        }
        private void btn_setting_Click(object sender, RoutedEventArgs e)
        {
            //F_Setting f_Setting = new F_Setting();
            //f_Setting.Show();

            Form1 f1 = new Form1();
            f1.Show();
        }

        
    }
}
