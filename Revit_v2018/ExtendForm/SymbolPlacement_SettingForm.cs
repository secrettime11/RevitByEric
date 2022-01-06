using Revit_v2018.Defined;
using Revit_v2018.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Revit_v2018.ExtendForm
{
    public partial class SymbolPlacement_SettingForm : Form
    {
        private DockableUI.UI_SymbolDisplayAndPlacement syncForm = null;

        public SymbolPlacement_SettingForm(DockableUI.UI_SymbolDisplayAndPlacement uI_Symbol)
        {
            InitializeComponent();

            this.syncForm = uI_Symbol;

            this.StartPosition = FormStartPosition.CenterScreen;

            this.lb_before.DrawMode = DrawMode.OwnerDrawVariable;
            this.lb_before.DrawItem += new DrawItemEventHandler(ListBoxGroupRange_DrawItem);
            this.lb_before.MeasureItem += new MeasureItemEventHandler(ListBoxGroupRange_MeasureItem);

            this.lb_after.DrawMode = DrawMode.OwnerDrawVariable;
            this.lb_after.DrawItem += new DrawItemEventHandler(ListBoxGroupRange_DrawItem);
            this.lb_after.MeasureItem += new MeasureItemEventHandler(ListBoxGroupRange_MeasureItem);
        }

        private void SymbolPlacement_SettingForm_Load(object sender, EventArgs e)
        {
            foreach (var item in Args.Category)
            {
                lb_before.Items.Add(item);
            }

            foreach (var item in Args.Category_Sort)
            {
                lb_after.Items.Add(item);
            }
        }

        private void btn_trans_Click(object sender, EventArgs e)
        {
            ListBoxTransItems(lb_before, lb_after,true);
        }

        private void btn_transBack_Click(object sender, EventArgs e)
        {
            ListBoxTransItems(lb_after, lb_before,false);
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

        private void btn_save_Click(object sender, EventArgs e)
        {
            DialogResult myResult = MessageBox.Show("確定儲存嗎?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (myResult == DialogResult.Yes)
            {
                if (File.Exists($@"{ini.iniPath}\{ini.ini_CategoryName}"))
                    File.Delete($@"{ini.iniPath}\{ini.ini_CategoryName}");

                if (File.Exists($@"{ini.iniPath}\{ini.ini_CategorySortName}"))
                    File.Delete($@"{ini.iniPath}\{ini.ini_CategorySortName}");

                foreach (var item in Args.Category)
                {
                    ini.write(item, ini.iniPath, ini.ini_CategoryName);
                }

                foreach (var item in Args.Category_Sort)
                {
                    ini.write(item, ini.iniPath, ini.ini_CategorySortName);
                }
            }
            else if (myResult == DialogResult.No)
            {
                return;
            }

            var displayData = new List<VM_Category>();

            foreach (var item in Args.Category_Sort)
            {
                displayData.Add(new VM_Category { Name = item });
            }
            DockableUI.UI_SymbolDisplayAndPlacement.VM_CategoryList.CList = new ObservableCollection<VM_Category>(displayData);

            //syncForm.lv_category.ItemsSource = null;
            //syncForm.lv_category.ItemsSource = Args.Category_Sort;

            this.Close();
        }

        private void ListBoxTransItems(ListBox From, ListBox To,bool trans)
        {
            List<Object> listObj = new List<object>();
            foreach (Object obj in From.SelectedItems)
            {
                To.Items.Add(obj);
                listObj.Add(obj);
                if (trans)
                {
                    Args.Category.Remove(obj.ToString());
                    Args.Category_Sort.Add(obj.ToString());
                }
                else
                {
                    Args.Category_Sort.Remove(obj.ToString());
                    Args.Category.Add(obj.ToString());
                }
            }
            foreach (Object obj in listObj)
            {
                From.Items.Remove(obj);
            }
        }

        private void lb_before_DoubleClick(object sender, EventArgs e)
        {
            ListBoxTransItems(lb_before, lb_after,true);
        }

        private void lb_after_DoubleClick(object sender, EventArgs e)
        {
            ListBoxTransItems(lb_after, lb_before,false);
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            string filter = this.txt_search.Text.Trim();
            this.Display(filter);
        }
        private void Display(string filter)
        {
            this.lb_before.Items.Clear();
            var result = Args.Category.Where(x => x.Contains(filter)).ToList();

            foreach (var item in result)
            {
                lb_before.Items.Add(item);
            }
        }
    }
}
