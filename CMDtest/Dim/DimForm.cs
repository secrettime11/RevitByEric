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
using static CMDtest.Dim.Model;

namespace CMDtest.Dim
{
    public partial class DimForm : Form
    {
        public Dim_handler dim_Handler { get; set; }
        public ExternalEvent dimEvent { get; set; }
        public DimForm()
        {
            InitializeComponent();
            dim_Handler = new Dim_handler();
            dimEvent = ExternalEvent.Create(dim_Handler);
        }

        private void DimForm_Load(object sender, EventArgs e)
        {

        }

        private void btn_dim_Click(object sender, EventArgs e)
        {
            init();
            this.Close();
            dimEvent.Raise();
        }

        private void btn_ini_Click(object sender, EventArgs e)
        {

        }

        public void init()
        {
            //extra = new List<string>();
            //choose = String.Empty;

            if (ckcb_dimX.Checked)
                ini.DimInfo.Add("X");
            if (ckcb_dimY.Checked)
                ini.DimInfo.Add("Y");
            if (ckcb_dimHanger.Checked)
                ini.DimInfo.Add("Hanger");

            if (ckcb_baseX_up.Checked)
                ini.BaseX = "up";
            else
                ini.BaseX = "down";

            if (ckcb_baseY_left.Checked)
                ini.BaseY = "left";
            else
                ini.BaseY = "right";

            ini.MainPipeDia = Convert.ToDecimal(txt_pipeDiameter.Text.Trim());
        }

        private void ckcb_baseX_up_CheckedChanged(object sender, EventArgs e)
        {
            CheckBoxSwitch(ckcb_baseX_up, ckcb_baseX_down);
        }

        private void ckcb_baseX_down_CheckedChanged(object sender, EventArgs e)
        {
            CheckBoxSwitch(ckcb_baseX_down, ckcb_baseX_up);
        }

        private void ckcb_baseY_left_CheckedChanged(object sender, EventArgs e)
        {
            CheckBoxSwitch(ckcb_baseY_left, ckcb_baseY_right);
        }

        private void ckcb_baseY_right_CheckedChanged(object sender, EventArgs e)
        {
            CheckBoxSwitch(ckcb_baseY_right, ckcb_baseY_left);
        }

        private void CheckBoxSwitch(CheckBox self, CheckBox another)
        {
            if (self.Checked)
                another.Checked = false;
            else
            {
                if (!another.Checked)
                    self.Checked = true;
            }
        }
    }
}