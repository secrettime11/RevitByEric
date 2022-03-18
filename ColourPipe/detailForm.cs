using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ColourPipe
{
    public partial class detailForm : Form
    {
        public detailForm(Form1 form1)
        {
            InitializeComponent();
            LocationCheck.Exist = true;
            for (int i = 0; i < 10; i++)
            {
                dgv_detail.Rows.Add(i.ToString());
            }
        }

        private void detailForm_Load(object sender, EventArgs e)
        {
            
        }

        private void detailForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            LocationCheck.Exist = false;
        }
    }
}
