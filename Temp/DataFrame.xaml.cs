using System;
using System.Collections.Generic;
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
    /// DataFrame.xaml 的互動邏輯
    /// </summary>
    public partial class DataFrame : UserControl
    {
        public VM_DataFrame VM_DataFrame = new VM_DataFrame();
        public DataFrame()
        {
            InitializeComponent();
            DataContext = VM_DataFrame;
        }

        private void ImgPanel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Data.TypeOne = VM_DataFrame.ObjImg.TypeOne;
            Data.TypeTwo = VM_DataFrame.ObjImg.TypeTwo;
        }
    }
}
