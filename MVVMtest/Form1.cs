using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MVVMtest
{
    public partial class Form1 : Form
    {
        //ViewModel viewModel = new ViewModel();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //MainWindow.p1.Name = "XQh";
            //MainWindow.viewModel.StudentList = new ObservableCollection<Students>()
            //{
            //    new Students(){ Name="Tom1"},
            //    new Students(){ Name="Darren2"},
            //    new Students(){ Name="Jacky3"},
            //    new Students(){ Name="Andy4"}
            //};


            List<string> dd = new List<string>();
            dd.Add("Kald");
            dd.Add("Kald");
            dd.Add("Kald");
            dd.Add("Kald");

            var gogo = new List<Students>();
            foreach (var item in dd)
            {
                gogo.Add(new Students { Name = item });
            }
            MainWindow.viewModel.StudentList = new ObservableCollection<Students>(gogo);

        }
    }
}
