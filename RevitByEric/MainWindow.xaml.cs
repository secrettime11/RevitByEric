using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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

namespace RevitByEric
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        public ThisApplication myThisApplication { get; set; }
        public ExternalCommandData commandData { get; set; }
        public List<ULoadAndPlace.ListView_Class> myListClass { get; set; } = new List<ULoadAndPlace.ListView_Class>();

        public ExternalEvents._LoadAllFamilies LoadAllFamilies_ { get; set; }
        public ExternalEvent Event_LoadAllFamilies { get; set; }

        public ExternalEvents._PlaceAFamily PlaceAFamily_ { get; set; }
        public ExternalEvent Event_PlaceAFamily { get; set; }

        public ULoadAndPlace UloadAndPlace { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Properties.Settings1.Default.Top = this.Top;
            Properties.Settings1.Default.Left = this.Left;
            Properties.Settings1.Default.Height = this.Height;
            Properties.Settings1.Default.Width = this.Width;
            Properties.Settings1.Default.Save();
        }

        private void myButton_EE05_LoadAllFamilies_Click(object sender, RoutedEventArgs e)
        {
            myMethod_ShowCodeButtons("EE05_LoadAllFamilies.txt");
        }

        private void myButton_EE06_PlaceAFamily_OnDoubleClick_Click(object sender, RoutedEventArgs e)
        {
            myMethod_ShowCodeButtons("EE06_PlaceAFamily_OnDoubleClick.txt");
        }

        private void myMethod_ShowCodeButtons(string myString_Filename)
        {
            try
            {
                if (myThisApplication.messageConst.Split('|')[0] == "Button_01_Invoke01")
                {
                    string myString_TempPath = myThisApplication.messageConst.Split('|')[1] + @"\Code Snippets\" + myString_Filename;
                    FileInfo myFileInfo_Start = new FileInfo(myString_TempPath);
                    string destDir = System.IO.Path.GetTempPath();

                    FileInfo myFileInfo_End = new FileInfo(System.IO.Path.Combine(destDir, myString_Filename));

                    myFileInfo_Start.CopyTo(myFileInfo_End.FullName, true);
                    System.Diagnostics.Process.Start(myFileInfo_End.FullName);

                }
                if (myThisApplication.messageConst.Split('|')[0] == "Button_01_Invoke01Development")
                {
                    string myString_TempPath = myThisApplication.messageConst.Split('|')[1] + @"\_929_Bilt2020_PlaypenChild\Code Snippets\" + myString_Filename;
                    System.Diagnostics.Process.Start(myString_TempPath);
                }
            }

            #region catch and finally
            catch (Exception ex)
            {
                Log.writeDebug("myMethod_ShowCodeButtons" + Environment.NewLine + ex.Message + Environment.NewLine + ex.InnerException, true);
            }
            finally
            {
            }
            #endregion   
        }

        public MainWindow(ExternalCommandData cD, ThisApplication tA)
        {
            myThisApplication = tA;
            commandData = cD;
            foreach (string myStrrr in Families_ThatMustBeLoaded.ListStringMustHaveFamilies) myListClass.Add(new ULoadAndPlace.ListView_Class() { String_Name = myStrrr, String_FileName = @"\Families\" + myStrrr + ".rfa" });

            InitializeComponent();
            // add 'UIDocument uid' as a parameter above, because this is the way it is called form the external event, please see youve 5 Secrets of Revit API Coding for an explaination on this

            if (true)
            {
                this.Top = Properties.Settings1.Default.Top;
                this.Left = Properties.Settings1.Default.Left;
            }

            LoadAllFamilies_ = new ExternalEvents._LoadAllFamilies();
            LoadAllFamilies_.myWindow1 = this;
            Event_LoadAllFamilies = ExternalEvent.Create(LoadAllFamilies_);

            PlaceAFamily_ = new ExternalEvents._PlaceAFamily();
            PlaceAFamily_.myWindow1 = this;
            Event_PlaceAFamily = ExternalEvent.Create(PlaceAFamily_);

        }

        private void my0506LoadingFamilies_Click(object sender, RoutedEventArgs e)
        {
            int eL = -1;
            UloadAndPlace = new ULoadAndPlace(commandData);

            try
            {
                UloadAndPlace.myWindow1 = this;
                UloadAndPlace.Topmost = true;
                UloadAndPlace.Owner = this;
                UloadAndPlace.Show();
            }

            #region catch and finally
            catch (Exception ex)
            {
                Log.writeDebug("my0506LoadingFamilies_Click, error line:" + eL + Environment.NewLine + ex.Message + Environment.NewLine + ex.InnerException, true);
            }
            finally
            {
            }
            #endregion
        }
    }
}
