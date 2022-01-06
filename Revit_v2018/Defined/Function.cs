using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB.Events;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.ExtensibleStorage;
using Autodesk.Revit.UI;
using System.Drawing;
using System.IO;
using System.Diagnostics;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Windows.Media;

namespace Revit_v2018.Defined
{
    public class Function
    {
        /// <summary>
        /// 新增 panel
        /// </summary>
        /// <param name="application">UIControlledApplication</param>
        /// <param name="tabName">Tab名稱</param>
        /// <param name="PanelName">Panel名稱</param>
        /// <returns></returns>
        public RibbonPanel CreatePanel(UIControlledApplication application, string tabName, string PanelName)
        {
            RibbonPanel ribbonPanel = application.CreateRibbonPanel(tabName, PanelName);
            return ribbonPanel;
        }

        /// <summary>
        /// 新增 button
        /// </summary>
        /// <param name="ribbonPanel">Panel name</param>
        /// <param name="buttonInfo">button info</param>
        /// <returns></returns>
        public PushButton CreateButton(RibbonPanel ribbonPanel, Args.ButtonInfo buttonInfo)
        {
            PushButton pushButton = ribbonPanel.AddItem(new PushButtonData(buttonInfo.Name, buttonInfo.Text, buttonInfo.AssemblyName, buttonInfo.ClassName)) as PushButton;

            Uri uriImange = new Uri(buttonInfo.ImgURi);
            BitmapImage largeImage = new BitmapImage(uriImange);
            pushButton.LargeImage = largeImage;

            return pushButton;
        }

        /// <summary>
        /// 更換圖檔
        /// </summary>
        /// <param name="ImgName">File name (png only)</param>
        /// <returns></returns>
        public string ImgPath(string ImgName)
        {
            string Path = $@"C:\Users\user\Desktop\Task\RevitByEric\Revit_v2018\Icon\{ImgName}.png";
            return Path;
        }
        
        public BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
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
