using Autodesk.Revit.UI;
using JohnsonRevitAPI2.Public_Folder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static CMDtest.ColorPipe.ColorPipeForm;

namespace CMDtest.ColorPipe
{
    public partial class detailForm : Form
    {
        //private Form frm;
        public detailForm(/*Form form*/)
        {
            //frm = form;
            InitializeComponent();


            this.StartPosition = FormStartPosition.CenterParent;
            LocationCheck.Exist = true;

            this.Text = Model.SelectFile;
            var data = ini.read(DirPath.Data, Model.SelectFile + ".txt");
            foreach (var item in data)
            {
                int index = dgv_detail.Rows.Add();
                string[] tmp = Regex.Split(item, "<!>", RegexOptions.IgnoreCase);
                dgv_detail.Rows[index].Cells[0].Value = tmp[1];
                dgv_detail.Rows[index].Cells[1].Value = tmp[2];
                dgv_detail.Rows[index].Cells[2].Value = tmp[3];
                dgv_detail.Rows[index].Cells[3].Value = tmp[4];

                dgv_detail.Rows[index].Cells[3].Style.BackColor = Color.FromArgb(Convert.ToInt32(tmp[4].Split(',')[0]), Convert.ToInt32(tmp[4].Split(',')[1]), Convert.ToInt32(tmp[4].Split(',')[2]));
            }
            //dgv_detail.Rows.Add("A");
            //dgv_detail.Rows[0].Cells[0].Style.BackColor = Color.Red;
        }

        private void detailForm_Load(object sender, EventArgs e)
        {
            
        }

        private void detailForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            LocationCheck.Exist = false;
        }

        private void btn_rollback_Click(object sender, EventArgs e)
        {
            //this.Close();
            Model.Status = "原色";
            if (Model.SystemList.Count > 0)
            {
                Model.SelectEleList = new List<SelectEle>();
                foreach (DataGridViewRow item in dgv_detail.Rows)
                {
                    SelectEle selectEle = new SelectEle();
                    var color = (from o in Model.SystemList where o.Name == item.Cells[2].Value.ToString() select o.SysColor).FirstOrDefault();
                    selectEle.Id = item.Cells[0].Value.ToString();
                    selectEle.SysColor = color;
                    Model.SelectEleList.Add(selectEle);
                }
            }
            Cmd_colorPipe.colorback_e.Raise();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Model.Status = "修改色";
            if (Model.SystemList.Count > 0)
            {
                Model.SelectEleList = new List<SelectEle>();
                foreach (DataGridViewRow item in dgv_detail.Rows)
                {
                    SelectEle selectEle = new SelectEle();
                    string[] rgb = item.Cells[3].Value.ToString().Split(',');
                    var color = Color.FromArgb(Convert.ToInt32(rgb[0]), Convert.ToInt32(rgb[1]), Convert.ToInt32(rgb[2]));
                    selectEle.Id = item.Cells[0].Value.ToString();
                    selectEle.SysColor = color;
                    Model.SelectEleList.Add(selectEle);
                }
            }
            Cmd_colorPipe.colorback_e.Raise();
        }
    }
}
