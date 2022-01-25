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
        public Event_SymbolPlacement Event_SymbolPlacement { get; set; }
        public ExternalEvent Event_SymbolPlacement_ { get; set; }

        public ListView ListView_Category { get { return this.lv_category; } }

        public static VM_CList VM_CategoryList = new VM_CList();
        public static VM_SymbolList VM_SymbolList = new VM_SymbolList();

        public UI_SymbolDisplayAndPlacement()
        {
            InitializeComponent();

            Event_SymbolPlacement = new Event_SymbolPlacement();
            Event_SymbolPlacement_ = ExternalEvent.Create(Event_SymbolPlacement);

            this.lv_category.DataContext = VM_CategoryList;
            this.lstFileManager.DataContext = VM_SymbolList;
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
            try
            {
                var SelecttionInfo = lv_category.SelectedItem as VM_Category;
                if (SelecttionInfo != null)
                {
                    // 取得族群下各自的Family
                    var data = (from o in Args.AllData
                                where o.Category == SelecttionInfo.Name
                                select o.Family).Distinct();

                    var displayData = new List<VM_SymbolData>();
                    foreach (var item in data)
                    {
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
                        displayData.Add(new VM_SymbolData { Family_ = item, Img_ = Bitmap_image, SymbolName_ = Symbol.Values.ToList(), Symbol_ = Symbol, Name_ = "" });
                    }
                    VM_SymbolList.SList = new ObservableCollection<VM_SymbolData>(displayData);
                }
            }
            catch (Exception) { }
        }
        private void ListView_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var GetControl = myFunction.GetAncestorOfType<ListBoxItem>(sender as ListView);
                var info = GetControl.DataContext as VM_SymbolData;
                if (info != null)
                {
                    Args.NowObj = info.Symbol_.FirstOrDefault(x => x.Value == info.Name_).Key;
                    Event_SymbolPlacement_.Raise();
                }
            }
            catch (Exception)
            {
            }
        }
        private void btn_setting_Click(object sender, RoutedEventArgs e)
        {
            ExtendForm.SymbolPlacement_SettingForm extendForm = new ExtendForm.SymbolPlacement_SettingForm(this);

            extendForm.ShowDialog();
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
