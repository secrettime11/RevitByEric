using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CMDtest.FindDwg
{
    public partial class DWGInfo : System.Windows.Forms.Form
    {
        importH importH { get; set; }
        public ExternalEvent importEvent { get; set; }

        public DWGInfo()
        {
            InitializeComponent();
            importH = new importH();
            importEvent = ExternalEvent.Create(importH);
        }
        private void DWGInfo_Load(object sender, EventArgs e)
        {
            dgv_import.DefaultCellStyle.Font = new Font("標楷體", 11);
            dgv_link.DefaultCellStyle.Font = new Font("標楷體", 11);
            if (Model.result_import.Rows.Count > 0)
            {
                dgv_import.DataSource = Model.result_import;
                dgv_import.Rows[0].Selected = false;
            }

            if (Model.result_link.Rows.Count > 0)
            {
                dgv_link.DataSource = Model.result_link;
                dgv_link.Rows[0].Selected = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            importEvent.Raise();
        }
    }
}
