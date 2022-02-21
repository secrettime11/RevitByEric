using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CMDtest.Dim
{
    public partial class ExtraForm : Form
    {
        public ExtraForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void ExtraForm_Load(object sender, EventArgs e)
        {
            listbox_data.Items.Clear();
            foreach (var item in Model.extra)
            {
                listbox_data.Items.Add(item);
            }
        }

        private void listbox_data_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (listbox_data.SelectedItems != null)
                {
                    Model.choose = listbox_data.SelectedItem.ToString();
                    this.Close();
                }
            }
            catch (Exception)
            {

            }
            
        }
    }
}
