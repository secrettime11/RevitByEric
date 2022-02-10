using Autodesk.Revit.UI;
using CMDtest.Parameters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CMDtest
{
    public partial class AutoDimensionForm : Form
    {
        #region Equal scaling
        private System.Drawing.Size m_szInit;   // 初始窗體大小
        private Dictionary<Control, Rectangle> m_dicSize = new Dictionary<Control, Rectangle>();
        protected override void OnLoad(EventArgs e)
        {
            m_szInit = this.Size;//get initial size
            this.GetInitSize(this);
            base.OnLoad(e);
        }

        private void GetInitSize(Control ctrl)
        {
            foreach (Control c in ctrl.Controls)
            {
                m_dicSize.Add(c, new Rectangle(c.Location, c.Size));
                this.GetInitSize(c);
            }
        }
        protected override void OnResize(EventArgs e)
        {
            //Calculate size propotion between present and initial
            float fx = (float)this.Width / m_szInit.Width;
            float fy = (float)this.Height / m_szInit.Height;
            foreach (var v in m_dicSize)
            {
                v.Key.Left = (int)(v.Value.Left * fx);
                v.Key.Top = (int)(v.Value.Top * fy);
                v.Key.Width = (int)(v.Value.Width * fx);
                v.Key.Height = (int)(v.Value.Height * fy);
            }
            base.OnResize(e);
        }
        #endregion
        public Handler.Dimension_Handler Event_Dimension { get; set; }
        public ExternalEvent Event_Dimension_ { get; set; }

        public Handler.PipeDim_handler Event_PipeDimension { get; set; }
        public ExternalEvent Event_PipeDimension_ { get; set; }

        public AutoDimensionForm()
        {
            InitializeComponent();

            Event_Dimension = new Handler.Dimension_Handler();
            Event_Dimension_ = ExternalEvent.Create(Event_Dimension);

            Event_PipeDimension = new Handler.PipeDim_handler();
            Event_PipeDimension_ = ExternalEvent.Create(Event_PipeDimension);
        }

        private void btn_execute_Click(object sender, EventArgs e)
        {
            init();
            this.Close();
            Event_Dimension_.Raise();
        }

        private void AutoDimensionForm_Load(object sender, EventArgs e)
        {

        }

        private void init()
        {
            Dimension_Parameters.DimType = new List<string>();

            if (!string.IsNullOrEmpty(txt_range.Text))
                Dimension_Parameters.Range = Convert.ToDecimal(txt_range.Text);

            if (ckcb_water.Checked)
                Dimension_Parameters.DimType.Add("撒水頭");

            if (ckcb_pipeAccessory.Checked)
                Dimension_Parameters.DimType.Add("管附件");

            //if (ckcb_pipeKits.Checked)
            //    Dimension_Parameters.DimType.Add("管配件");

            if (rdb_xdimTop.Checked)
                Dimension_Parameters.AxisX = "上";
            else
                Dimension_Parameters.AxisX = "下";

            if (rdb_ydimLeft.Checked)
                Dimension_Parameters.AxisY = "左";
            else
                Dimension_Parameters.AxisY = "右";
        }

        private void btn_pipeDim_Click(object sender, EventArgs e)
        {
            this.Close();
            Event_PipeDimension_.Raise();
        }
    }
}
