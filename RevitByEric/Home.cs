using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace RevitByEric
{
    public class Home : IExternalApplication
    {

        public Result OnStartup(UIControlledApplication application)
        {
            string tabName = "Hello Revit";
            string panelName = "Revit Pannel";

            ButtonInfo buttonInfo = new ButtonInfo();
            buttonInfo.Name = "Name";
            buttonInfo.Text = "PlaceFamilyInstance";
            buttonInfo.AssemblyName = @"C:\Users\user\Desktop\Task\RevitByEric\RevitByEric\bin\Debug\RevitByEric.dll";
            buttonInfo.ClassName = "RevitByEric.ThisApplication";
            buttonInfo.ImgURi = ImgPath("BenzFactory");

            application.CreateRibbonTab(tabName);
            RibbonPanel ribbonPanel = CreatePanel(application, tabName, panelName);
            CreateButton(ribbonPanel, buttonInfo);

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        public class ButtonInfo
        {
            public string Name { get; set; }
            public string Text { get; set; }
            public string AssemblyName { get; set; }
            public string ClassName { get; set; }
            public string ImgURi { get; set; }
        }
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
        public PushButton CreateButton(RibbonPanel ribbonPanel, ButtonInfo buttonInfo)
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
            string Path = $@"C:\Users\user\Desktop\Task\RevitByEric\RevitByEric\Icon\{ImgName}.png";
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
    }
}
