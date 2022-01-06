using Autodesk.Revit.UI;
using Revit_v2018.Defined;
using Revit_v2018.HandlerEvent;
using Revit_v2018.tets;
using Revit_v2018.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Revit_v2018.DockableUI
{
    /// <summary>
    /// UI_SymbolDisplayAndPlacement.xaml 的互動邏輯
    /// </summary>
    public partial class UI_SymbolDisplayAndPlacement : UserControl, IDockablePaneProvider
    {
        Function myFunction = new Function();
        public Event_GetFamilySymbol Event_GetFamilySymbol { get; set; }
        public ExternalEvent Event_GetFamilySymbol_ { get; set; }

        public Event_SymbolPlacement Event_SymbolPlacement { get; set; }
        public ExternalEvent Event_SymbolPlacement_ { get; set; }

        public static VM_CList VM_CategoryList = new VM_CList();
        public ListView ListView_Category { get { return this.lv_category; } }

        public UI_SymbolDisplayAndPlacement()
        {
            InitializeComponent();

            Event_SymbolPlacement = new Event_SymbolPlacement();
            Event_SymbolPlacement_ = ExternalEvent.Create(Event_SymbolPlacement);

            this.lv_category.DataContext = VM_CategoryList;

        }
        public void SetupDockablePane(DockablePaneProviderData data)
        {
            data.FrameworkElement = this;
            data.InitialState = new DockablePaneState();
            data.InitialState.DockPosition = DockPosition.Right;
            data.InitialState.TabBehind = DockablePanes.BuiltInDockablePanes.ProjectBrowser;
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }
        private void lv_category_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ObservableCollection<LVData> LVDatas = new ObservableCollection<LVData>();
            lstFileManager.ItemsSource = LVDatas;

            try
            {
                var SelecttionInfo = lv_category.SelectedItem as VM_Category;
                
                // 取得族群下各自的Family
                var data = (from o in Args.AllData
                            where o.Category == SelecttionInfo.Name
                            select o.Family).Distinct();

                // Each Family
                foreach (var item in data)
                {
                    // item under family
                    var temp = Args.AllData.Where(x => x.Family == item).ToList();
                    Dictionary<string, string> Symbol = new Dictionary<string, string>();
                    BitmapImage Bitmap_image = null;
                    bool GetImage = false;
                    foreach (var stuff in temp)
                    {
                        // 只取一次Image
                        if (!GetImage)
                        {
                            System.Drawing.Size imgSize = new System.Drawing.Size(50, 50);
                            var img = stuff.FamilySymbol_.GetPreviewImage(imgSize);
                            if (img != null)
                            {
                                Bitmap_image = myFunction.BitmapToImageSource(img);
                                GetImage = true;
                            }
                        }
                        Symbol.Add(stuff.Id, stuff.Symbol);
                    }

                    LVDatas.Add(new LVData { Family_ = item, Symbol_ = Symbol, Img_ = Bitmap_image, Symbol_Name = Symbol.Values.ToList(), Name_ = "" });
                }
            }
            catch (Exception) { }
        }
        private void ListView_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var GetControl = myFunction.GetAncestorOfType<ListBoxItem>(sender as ListView);
                var info = GetControl.Content as LVData;
                if (info != null)
                {
                    Args.NowObj = info.Symbol_.FirstOrDefault(x => x.Value == info.Name_).Key;
                    Event_SymbolPlacement_.Raise();
                }
            }
            catch (Exception) { }
        }
        private void btn_setting_Click(object sender, RoutedEventArgs e)
        {
            ExtendForm.SymbolPlacement_SettingForm extendForm = new ExtendForm.SymbolPlacement_SettingForm(this);

            extendForm.ShowDialog();
        }

        private void btn_userSetting_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_loadAll_Click(object sender, RoutedEventArgs e)
        {
            var displayData = new List<VM_Category>();

            var Category = (from o in Args.AllData select o.Category).Distinct();

            foreach (var item in Category)
            {
                displayData.Add(new VM_Category { Name = item });
            }
            VM_CategoryList.CList = new ObservableCollection<VM_Category>(displayData);
        }

        public class LVData
        {
            public string Family_ { get; set; }
            public List<string> Symbol_Name { get; set; }
            public string Name_ { get; set; }
            public Dictionary<string, string> Symbol_ { get; set; }
            public BitmapImage Img_ { get; set; }
        }

    }
}
