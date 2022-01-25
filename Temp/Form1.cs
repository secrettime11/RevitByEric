using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Temp
{
    public partial class Form1 : Form
    {
        List<string> data = new List<string>();
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string filter = this.textBox1.Text.Trim();
            this.Display(filter);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                data.Add($"Sample {i + 1}");
                listBox1.Items.Add($"Sample {i + 1}");
            }
        }

        private void Display(string filter)
        {
            this.listBox1.Items.Clear();

            var result = data.Where(x => x.Contains(filter)).ToList();

            foreach (var item in result)
                listBox1.Items.Add(item);
        }
    }
}
