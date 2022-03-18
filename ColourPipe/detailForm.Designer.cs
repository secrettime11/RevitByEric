namespace ColourPipe
{
    partial class detailForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgv_detail = new System.Windows.Forms.DataGridView();
            this.CId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_detail)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_detail
            // 
            this.dgv_detail.AllowUserToAddRows = false;
            this.dgv_detail.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_detail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_detail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CId,
            this.CName,
            this.CType});
            this.dgv_detail.Location = new System.Drawing.Point(6, 6);
            this.dgv_detail.Name = "dgv_detail";
            this.dgv_detail.RowTemplate.Height = 24;
            this.dgv_detail.Size = new System.Drawing.Size(309, 439);
            this.dgv_detail.TabIndex = 0;
            // 
            // CId
            // 
            this.CId.HeaderText = "ID";
            this.CId.Name = "CId";
            // 
            // CName
            // 
            this.CName.HeaderText = "Name";
            this.CName.Name = "CName";
            // 
            // CType
            // 
            this.CType.HeaderText = "Type";
            this.CType.Name = "CType";
            // 
            // detailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(321, 450);
            this.Controls.Add(this.dgv_detail);
            this.Name = "detailForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Detail";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.detailForm_FormClosing);
            this.Load += new System.EventHandler(this.detailForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_detail)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_detail;
        private System.Windows.Forms.DataGridViewTextBoxColumn CId;
        private System.Windows.Forms.DataGridViewTextBoxColumn CName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CType;
    }
}