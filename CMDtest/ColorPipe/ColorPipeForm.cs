using Autodesk.Revit.UI;
using JohnsonRevitAPI2.Public_Folder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CMDtest.ColorPipe
{
    public partial class ColorPipeForm : System.Windows.Forms.Form
    {
        // 摺疊視窗 取得原視窗大小
        private int originalWidth;
        public ColorPipeForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            stretchBtn.BackgroundImage = imageList1.Images[1];
            this.Text = "ProjectName";
            btn_colour.BackColor = Color.FromArgb(255,149,255);
            Model.colorSet = btn_colour.BackColor;
            //dgv_allFile.Rows.Add("demo");
        }

        private void ColorPipeForm_Load(object sender, EventArgs e)
        {
            //txt_filename.Placeholder = DateTime.Now.ToString("yyyy/MM/dd");
        }

        public class ZhmTextBox : System.Windows.Forms.TextBox
        {
            private const int EM_SETCUEBANNER = 0x1501;

            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            private static extern Int32 SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

            private string placeholder = string.Empty;
            public string Placeholder
            {
                get { return placeholder; }
                set
                {
                    placeholder = value;
                    SendMessage(Handle, EM_SETCUEBANNER, 0, Placeholder);
                }
            }
        }
        public class LocationCheck
        {
            public static bool Exist { get; set; } = false;
        }

        private void dgv_allFile_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           

        }

        private void ColorPipeForm_Load_1(object sender, EventArgs e)
        {
            loadFile();
        }

        private void btn_create_Click(object sender, EventArgs e)
        {
            this.Close();
            Cmd_colorPipe.colorevent.Raise();
        }

        private void btn_colour_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            // Keeps the user from selecting a custom color.
            MyDialog.AllowFullOpen = true;
            // Allows the user to get help. (The default is false.)
            MyDialog.ShowHelp = true;
            // Sets the initial color select to the current text color.
            MyDialog.Color = btn_colour.BackColor;

            // Update the text box color if the user clicks OK 
            if (MyDialog.ShowDialog() == DialogResult.OK)
            {
                btn_colour.BackColor = MyDialog.Color;
                Model.colorSet = MyDialog.Color;
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            string path = $@"C:\ProgramData\Autodesk\Revit\Addins\Data\{dgv_allFile.CurrentCell.Value}.txt";
            bool result = File.Exists(path);
            if (result == true)
            {
                File.Delete(path);
                dgv_allFile.Rows.Clear();
                loadFile();
            }
            else
            {
                Console.WriteLine("File Not Found");
            }
        }

        private void loadFile()
        {
            foreach (string fname in System.IO.Directory.GetFileSystemEntries(@"C:\ProgramData\Autodesk\Revit\Addins\Data\", "*.txt"))
            {
                string[] txtName = fname.Split(new[] { "\\" }, StringSplitOptions.None);
                string Nametxt = txtName[txtName.Length - 1];
                Nametxt = Nametxt.Replace(".txt", "");
                dgv_allFile.Rows.Add(Nametxt);
            }
            if (dgv_allFile.Rows.Count > 0)
            {
                dgv_allFile.Rows[0].Selected = false;
            }
        }

        private void stretchBtn_Click(object sender, EventArgs e)
        {
            if (flowLayoutPanel1.Visible)
            {
                stretchBtn.BackgroundImage = imageList1.Images[0];
                flowLayoutPanel1.Visible = false;
                this.Width = originalWidth - flowLayoutPanel1.Width;
            }
            else
            {
                stretchBtn.BackgroundImage = imageList1.Images[1];
                flowLayoutPanel1.Visible = true;
                this.Width = originalWidth;
            }
        }
        protected override void OnLoad(EventArgs e)
        {
            originalWidth = this.Width;
            base.OnLoad(e);
        }

        private void dgv_allFile_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgv_detail.Rows.Clear();
            var data = ini.read(ini.iniPath, dgv_allFile.SelectedCells[0].Value.ToString() + ".txt");
            foreach (var item in data)
            {
                int index = dgv_detail.Rows.Add();
                string[] tmp = Regex.Split(item, "<!>", RegexOptions.IgnoreCase);
                dgv_detail.Rows[index].Cells[0].Value = tmp[1];
                dgv_detail.Rows[index].Cells[1].Value = tmp[2];
                dgv_detail.Rows[index].Cells[2].Value = tmp[3];
                //dgv_detail.Rows[index].Cells[3].Value = tmp[4];

                dgv_detail.Rows[index].Cells[3].Style.BackColor = Color.FromArgb(Convert.ToInt32(tmp[4].Split(',')[0]), Convert.ToInt32(tmp[4].Split(',')[1]), Convert.ToInt32(tmp[4].Split(',')[2]));
            }
        }

        private void btn_oriColor_Click(object sender, EventArgs e)
        {
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

        private void btn_coverColor_Click(object sender, EventArgs e)
        {
            Model.Status = "修改色";
            if (Model.SystemList.Count > 0)
            {
                Model.SelectEleList = new List<SelectEle>();
                foreach (DataGridViewRow item in dgv_detail.Rows)
                {
                    SelectEle selectEle = new SelectEle();
                    //string[] rgb = item.Cells[3].Value.ToString().Split(',');
                    //var color = Color.FromArgb(Convert.ToInt32(rgb[0]), Convert.ToInt32(rgb[1]), Convert.ToInt32(rgb[2]));
                    selectEle.Id = item.Cells[0].Value.ToString();
                    selectEle.SysColor = item.Cells[3].Style.BackColor;
                    Model.SelectEleList.Add(selectEle);
                }
            }
            Cmd_colorPipe.colorback_e.Raise();
        }

        private void btn_rollbackAll_Click(object sender, EventArgs e)
        {
            Model.Status = "全部復原";
            Cmd_colorPipe.colorback_e.Raise();
        }

        private void btn_3D_Click(object sender, EventArgs e)
        {
            Cmd_colorPipe.event_3d.Raise();
        }
    }
}
