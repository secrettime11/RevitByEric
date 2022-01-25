using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Revit_v2018.Defined;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revit_v2018
{
    public class Controller : IExternalApplication
    {
        Function function = new Function();
        public HandlerEvent.Event_GetFamilySymbol Event_GetFamilySymbol { get; set; }
        public ExternalEvent Event_GetFamilySymbol_ { get; set; }

        public Result OnStartup(UIControlledApplication application) 
        {
            // 註冊 Dockable panel
            RegisterDockablePanel(application);

            string TabName = "Adjustice";
            string PanelName = "Families Template";

            // 族群實體放置(Botton)
            Args.ButtonInfo btn_DisAndFamilyPlace = new Args.ButtonInfo();
            btn_DisAndFamilyPlace.Name = "TemplateClass";
            btn_DisAndFamilyPlace.Text = "Symbol placement";
            btn_DisAndFamilyPlace.AssemblyName = @"C:\Users\user\Desktop\Task\RevitByEric\Revit_v2018\bin\Debug\Revit_v2018.dll";
            btn_DisAndFamilyPlace.ClassName = "Revit_v2018.Command.Command_SymbolDisplayAndPlacement";
            btn_DisAndFamilyPlace.ImgURi = function.ImgPath("setting");

            // test
            Args.ButtonInfo btn_test = new Args.ButtonInfo();
            btn_test.Name = "TestClass";
            btn_test.Text = "Test";
            btn_test.AssemblyName = @"C:\Users\user\Desktop\Task\RevitByEric\Revit_v2018\bin\Debug\Revit_v2018.dll";
            btn_test.ClassName = "Revit_v2018.Command.Command_test";
            btn_test.ImgURi = function.ImgPath("Home");


            application.CreateRibbonTab(TabName);
            RibbonPanel ribbonPanel = function.CreatePanel(application, TabName, PanelName);
            function.CreateButton(ribbonPanel, btn_DisAndFamilyPlace);
            function.CreateButton(ribbonPanel, btn_test);

            application.ViewActivated += Application_ViewActivated;

            return Result.Succeeded;
        }
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        private void RegisterDockablePanel(UIControlledApplication application) 
        {
            DockableUI.UI_SymbolDisplayAndPlacement Page_SymbolDisplayAndPlacement = new DockableUI.UI_SymbolDisplayAndPlacement();
            DockablePaneId paneId_SymbolDisplayAndPlacement = new DockablePaneId(new Guid("{5A1052C5-99BB-493A-BF7B-A039F050439A}"));
            application.RegisterDockablePane(paneId_SymbolDisplayAndPlacement, "SymbolDisplayAndPlacement", (IDockablePaneProvider)Page_SymbolDisplayAndPlacement);

            DockableUI.UserControl1 Page_test = new DockableUI.UserControl1();
            DockablePaneId paneId_test = new DockablePaneId(new Guid("{18C5001E-63CC-4172-8ADA-07DF9A63256B}"));
            application.RegisterDockablePane(paneId_test, "Test", (IDockablePaneProvider)Page_test);
        }


        private void Application_ViewActivated(object sender, Autodesk.Revit.UI.Events.ViewActivatedEventArgs e)
        {
            Event_GetFamilySymbol = new HandlerEvent.Event_GetFamilySymbol();
            Event_GetFamilySymbol_ = ExternalEvent.Create(Event_GetFamilySymbol);
            Event_GetFamilySymbol_.Raise();

            if (DockableUI.UI_SymbolDisplayAndPlacement.VM_SymbolList.SList != null)
                DockableUI.UI_SymbolDisplayAndPlacement.VM_SymbolList.SList.Clear();
        }
        
    }
}
