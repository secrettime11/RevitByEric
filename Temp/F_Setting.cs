using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Temp
{
    public partial class F_Setting : Form
    {
        List<string> data = new List<string>();

        List<string> data_user = new List<string>();
        public F_Setting()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();

            this.lb_before.DrawMode = DrawMode.OwnerDrawVariable;
            this.lb_before.DrawItem += new DrawItemEventHandler(ListBoxGroupRange_DrawItem);
            this.lb_before.MeasureItem += new MeasureItemEventHandler(ListBoxGroupRange_MeasureItem);

            this.lb_after.DrawMode = DrawMode.OwnerDrawVariable;
            this.lb_after.DrawItem += new DrawItemEventHandler(ListBoxGroupRange_DrawItem);
            this.lb_after.MeasureItem += new MeasureItemEventHandler(ListBoxGroupRange_MeasureItem);
        }

        private void F_Setting_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                data.Add($"Sample {i + 1}");
                lb_before.Items.Add($"Sample {i + 1}");
            }
        }

        private void btn_trans_Click(object sender, EventArgs e)
        {
            List<Object> listObj = new List<object>();
            foreach (Object obj in lb_before.SelectedItems)
            {
                lb_after.Items.Add(obj);
                listObj.Add(obj);
                data_user.Add(obj.ToString());
            }
            foreach (Object obj in listObj)
            {
                lb_before.Items.Remove(obj);
                data.Remove(obj.ToString());
            }
        }

        private void btn_transBack_Click(object sender, EventArgs e)
        {
            List<Object> listObj = new List<object>();
            foreach (Object obj in lb_after.SelectedItems)
            {
                lb_before.Items.Add(obj);
                listObj.Add(obj);
                data.Add(obj.ToString());
            }
            foreach (Object obj in listObj)
            {
                lb_after.Items.Remove(obj);
                data_user.Remove(obj.ToString());
            }
        }

        private void btn_up_Click(object sender, EventArgs e)
        {
            this.lb_after.MoveSelectedItems(true, () => { });
        }

        private void btn_down_Click(object sender, EventArgs e)
        {
            this.lb_after.MoveSelectedItems(false, () => { });
        }

        private void ListBoxGroupRange_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(e.BackColor), e.Bounds);
            if (e.Index >= 0)
            {
                StringFormat sStringFormat = new StringFormat();
                sStringFormat.LineAlignment = StringAlignment.Center;
                e.Graphics.DrawString(((ListBox)sender).Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), e.Bounds, sStringFormat);
            }
            e.DrawFocusRectangle();
        }

        private void ListBoxGroupRange_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            e.ItemHeight = e.ItemHeight + 6;
        }

        private void lb_before_DoubleClick(object sender, EventArgs e)
        {
            List<Object> listObj = new List<object>();
            foreach (Object obj in lb_before.SelectedItems)
            {
                lb_after.Items.Add(obj);
                listObj.Add(obj);
                data_user.Add(obj.ToString());
            }
            foreach (Object obj in listObj)
            {
                lb_before.Items.Remove(obj);
                data.Remove(obj.ToString());
            }
        }

        private void lb_after_DoubleClick(object sender, EventArgs e)
        {
            List<Object> listObj = new List<object>();
            foreach (Object obj in lb_after.SelectedItems)
            {
                lb_before.Items.Add(obj);
                listObj.Add(obj);
                data.Add(obj.ToString());
            }
            foreach (Object obj in listObj)
            {
                lb_after.Items.Remove(obj);
                data_user.Remove(obj.ToString());
            }
        }

        private void Display(string filter)
        {
            this.lb_before.Items.Clear();

            var result = data.Where(x => x.Contains(filter)).ToList();

            foreach (var item in result)
                lb_before.Items.Add(item);
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            string filter = this.txt_search.Text.Trim();
            this.Display(filter);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult myResult = MessageBox.Show("確定儲存嗎?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (myResult == DialogResult.Yes)
            {
                if (File.Exists(@"C:\Users\user\Desktop\Task\RevitByEric\Temp\bin\Debug\ini\ini_SymbolPlacement.txt"))
                    File.Delete(@"C:\Users\user\Desktop\Task\RevitByEric\Temp\bin\Debug\ini\ini_SymbolPlacement.txt");

                foreach (var item in data)
                {
                    ini.write(item, false);
                }
                ini.write("end", true);
            }
            else if (myResult == DialogResult.No)
            {
                return;
            }
        }
        static List<string> data2 = new List<string>();  
        private void button2_Click(object sender, EventArgs e)
        {
            var data = ini.read();
            foreach (var item in data)
            {
                Console.WriteLine(item);
            }
            data2 = ini.read();
        }
    }
}

