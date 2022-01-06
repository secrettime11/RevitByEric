using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WinForm = System.Windows.Forms;

namespace Revit_v2018.Defined
{
    public static class ListBoxExtention
    {
        public static bool MoveSelectedItems(this WinForm.ListBox listBox, bool isUp, Action noSelectAction)
        {
            if (listBox.SelectedItems.Count > 0)
            {
                return listBox.MoveSelectedItems(isUp);
            }
            else
            {
                noSelectAction();
                return false;
            }
        }

        public static bool MoveSelectedItems(this WinForm.ListBox listBox, bool isUp)
        {
            bool result = true;
            WinForm.ListBox.SelectedIndexCollection indices = listBox.SelectedIndices;
            if (isUp)
            {
                if (listBox.SelectedItems.Count > 0 && indices[0] != 0)
                {
                    foreach (int i in indices)
                    {
                        result &= MoveSelectedItem(listBox, i, true);
                    }
                }
            }
            else
            {
                if (listBox.SelectedItems.Count > 0 && indices[indices.Count - 1] != listBox.Items.Count - 1)
                {
                    for (int i = indices.Count - 1; i >= 0; i--)
                    {
                        result &= MoveSelectedItem(listBox, indices[i], false);
                    }
                }
            }
            return result;
        }

        public static bool MoveSelectedItem(this WinForm.ListBox listBox, bool isUp, Action noSelectAction)
        {
            if (listBox.SelectedItems.Count > 0)
            {
                return MoveSelectedItem(listBox, listBox.SelectedIndex, isUp);
            }
            else
            {
                noSelectAction();
                return false;
            }
        }

        public static bool MoveSelectedItem(this WinForm.ListBox listBox, bool isUp)
        {
            return MoveSelectedItem(listBox, listBox.SelectedIndex, isUp);
        }

        private static bool MoveSelectedItem(this WinForm.ListBox listBox, int selectedIndex, bool isUp)
        {
            if (selectedIndex != (isUp ? 0 : listBox.Items.Count - 1))
            {
                object current = listBox.Items[selectedIndex];
                int insertAt = selectedIndex + (isUp ? -1 : 1);

                listBox.Items.RemoveAt(selectedIndex);
                listBox.Items.Insert(insertAt, current);
                listBox.SelectedIndex = insertAt;

                var temp = Args.Category_Sort[selectedIndex];
                Args.Category_Sort[selectedIndex] = Args.Category_Sort[insertAt];
                Args.Category_Sort[insertAt] = temp;

                return true;
            }
            return false;
        }
    }
}
