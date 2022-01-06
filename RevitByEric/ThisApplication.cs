using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitByEric
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public partial class ThisApplication : IExternalCommand
    {
        public string messageConst { get; set; }
        public MainWindow myWindow1 { get; set; }
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            messageConst = message;

            try
            {
                myWindow1 = new MainWindow(commandData, this);

                myWindow1.Show();
            }

            #region catch and finally
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
            finally
            {
            }
            #endregion

            return Result.Succeeded;
        }
    }
}
