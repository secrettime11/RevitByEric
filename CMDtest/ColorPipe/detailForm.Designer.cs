namespace CMDtest.ColorPipe
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
            this.NColor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_show = new System.Windows.Forms.Button();
            this.btn_setcolor = new System.Windows.Forms.Button();
            this.btn_rollback = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
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
            this.CType,
            this.NColor});
            this.dgv_detail.Location = new System.Drawing.Point(12, 41);
            this.dgv_detail.Name = "dgv_detail";
            this.dgv_detail.RowTemplate.Height = 24;
            this.dgv_detail.Size = new System.Drawing.Size(569, 410);
            this.dgv_detail.TabIndex = 1;
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
            // NColor
            // 
            this.NColor.HeaderText = "NowColor";
            this.NColor.Name = "NColor";
            // 
            // btn_show
            // 
            this.btn_show.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_show.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_show.Location = new System.Drawing.Point(509, 12);
            this.btn_show.Name = "btn_show";
            this.btn_show.Size = new System.Drawing.Size(72, 23);
            this.btn_show.TabIndex = 38;
            this.btn_show.Text = "刪除";
            this.btn_show.UseVisualStyleBackColor = true;
            // 
            // btn_setcolor
            // 
            this.btn_setcolor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_setcolor.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_setcolor.Location = new System.Drawing.Point(103, 12);
            this.btn_setcolor.Name = "btn_setcolor";
            this.btn_setcolor.Size = new System.Drawing.Size(72, 23);
            this.btn_setcolor.TabIndex = 39;
            this.btn_setcolor.Text = "修改色";
            this.btn_setcolor.UseVisualStyleBackColor = true;
            this.btn_setcolor.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_rollback
            // 
            this.btn_rollback.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_rollback.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_rollback.Location = new System.Drawing.Point(13, 12);
            this.btn_rollback.Name = "btn_rollback";
            this.btn_rollback.Size = new System.Drawing.Size(72, 23);
            this.btn_rollback.TabIndex = 40;
            this.btn_rollback.Text = "原色";
            this.btn_rollback.UseVisualStyleBackColor = true;
            this.btn_rollback.Click += new System.EventHandler(this.btn_rollback_Click);
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(417, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(72, 23);
            this.button1.TabIndex = 41;
            this.button1.Text = "新增";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // detailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(595, 463);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn_rollback);
            this.Controls.Add(this.btn_setcolor);
            this.Controls.Add(this.btn_show);
            this.Controls.Add(this.dgv_detail);
            this.Name = "detailForm";
            this.Text = "detailForm";
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
        private System.Windows.Forms.DataGridViewTextBoxColumn NColor;
        private System.Windows.Forms.Button btn_show;
        private System.Windows.Forms.Button btn_setcolor;
        private System.Windows.Forms.Button btn_rollback;
        private System.Windows.Forms.Button button1;
    }
}