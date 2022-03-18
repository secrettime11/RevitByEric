using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ColourPipe
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Text = "ProjectName";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txt_filename.Placeholder = DateTime.Now.ToString("yyyy/MM/dd");
            dgv_allFile.Rows.Add("Demo001");
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
                btn_colour.BackColor = MyDialog.Color;
        }

        private void dgv_allFile_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (LocationCheck.Exist)
                return;
            
            detailForm detailForm = new detailForm(this);
            detailForm.Show();
        }

        private void btn_create_Click(object sender, EventArgs e)
        {
            
        }
    }
    public class ZhmTextBox : TextBox
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
}
