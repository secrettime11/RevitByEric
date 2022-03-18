namespace CMDtest.ColorPipe
{
    partial class ColorPipeForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ColorPipeForm));
            this.btn_delete = new System.Windows.Forms.Button();
            this.dgv_allFile = new System.Windows.Forms.DataGridView();
            this.filename = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_colour = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_create = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.stretchBtn = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btn_coverColor = new System.Windows.Forms.Button();
            this.btn_oriColor = new System.Windows.Forms.Button();
            this.dgv_detail = new System.Windows.Forms.DataGridView();
            this.CId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NColor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btn_rollbackAll = new System.Windows.Forms.Button();
            this.btn_3D = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_allFile)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_detail)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_delete
            // 
            this.btn_delete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_delete.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_delete.Location = new System.Drawing.Point(180, 307);
            this.btn_delete.Name = "btn_delete";
            this.btn_delete.Size = new System.Drawing.Size(72, 23);
            this.btn_delete.TabIndex = 41;
            this.btn_delete.Text = "刪除";
            this.btn_delete.UseVisualStyleBackColor = true;
            this.btn_delete.Click += new System.EventHandler(this.btn_delete_Click);
            // 
            // dgv_allFile
            // 
            this.dgv_allFile.AllowUserToAddRows = false;
            this.dgv_allFile.AllowUserToResizeRows = false;
            this.dgv_allFile.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_allFile.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_allFile.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.filename});
            this.dgv_allFile.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dgv_allFile.Location = new System.Drawing.Point(12, 122);
            this.dgv_allFile.Name = "dgv_allFile";
            this.dgv_allFile.ReadOnly = true;
            this.dgv_allFile.RowHeadersVisible = false;
            this.dgv_allFile.RowTemplate.Height = 24;
            this.dgv_allFile.Size = new System.Drawing.Size(240, 179);
            this.dgv_allFile.TabIndex = 36;
            this.dgv_allFile.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_allFile_CellClick);
            this.dgv_allFile.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_allFile_CellDoubleClick);
            // 
            // filename
            // 
            this.filename.HeaderText = "檔案名稱";
            this.filename.Name = "filename";
            this.filename.ReadOnly = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(12, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "顏色選擇";
            // 
            // btn_colour
            // 
            this.btn_colour.BackColor = System.Drawing.Color.Gold;
            this.btn_colour.Location = new System.Drawing.Point(87, 26);
            this.btn_colour.Name = "btn_colour";
            this.btn_colour.Size = new System.Drawing.Size(75, 23);
            this.btn_colour.TabIndex = 1;
            this.btn_colour.UseVisualStyleBackColor = false;
            this.btn_colour.Click += new System.EventHandler(this.btn_colour_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(12, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "檔名";
            // 
            // btn_create
            // 
            this.btn_create.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_create.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_create.Location = new System.Drawing.Point(168, 25);
            this.btn_create.Name = "btn_create";
            this.btn_create.Size = new System.Drawing.Size(60, 59);
            this.btn_create.TabIndex = 36;
            this.btn_create.Text = "建立";
            this.btn_create.UseVisualStyleBackColor = true;
            this.btn_create.Click += new System.EventHandler(this.btn_create_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_create);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btn_colour);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("新細明體", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(240, 100);
            this.groupBox1.TabIndex = 40;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "建立新紀錄";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // stretchBtn
            // 
            this.stretchBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("stretchBtn.BackgroundImage")));
            this.stretchBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.stretchBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.stretchBtn.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stretchBtn.Location = new System.Drawing.Point(258, -1);
            this.stretchBtn.Name = "stretchBtn";
            this.stretchBtn.Size = new System.Drawing.Size(16, 341);
            this.stretchBtn.TabIndex = 43;
            this.stretchBtn.UseVisualStyleBackColor = true;
            this.stretchBtn.Click += new System.EventHandler(this.stretchBtn_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "icons8_double_right.ico");
            this.imageList1.Images.SetKeyName(1, "icons8_double_left.ico");
            // 
            // btn_coverColor
            // 
            this.btn_coverColor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_coverColor.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_coverColor.Location = new System.Drawing.Point(277, 307);
            this.btn_coverColor.Name = "btn_coverColor";
            this.btn_coverColor.Size = new System.Drawing.Size(72, 23);
            this.btn_coverColor.TabIndex = 45;
            this.btn_coverColor.Text = "覆蓋";
            this.btn_coverColor.UseVisualStyleBackColor = true;
            this.btn_coverColor.Click += new System.EventHandler(this.btn_coverColor_Click);
            // 
            // btn_oriColor
            // 
            this.btn_oriColor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_oriColor.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_oriColor.Location = new System.Drawing.Point(504, 307);
            this.btn_oriColor.Name = "btn_oriColor";
            this.btn_oriColor.Size = new System.Drawing.Size(72, 23);
            this.btn_oriColor.TabIndex = 47;
            this.btn_oriColor.Text = "復原";
            this.btn_oriColor.UseVisualStyleBackColor = true;
            this.btn_oriColor.Click += new System.EventHandler(this.btn_oriColor_Click);
            // 
            // dgv_detail
            // 
            this.dgv_detail.AllowUserToAddRows = false;
            this.dgv_detail.AllowUserToResizeRows = false;
            this.dgv_detail.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_detail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_detail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CId,
            this.CName,
            this.CType,
            this.NColor});
            this.dgv_detail.Location = new System.Drawing.Point(3, 3);
            this.dgv_detail.Name = "dgv_detail";
            this.dgv_detail.ReadOnly = true;
            this.dgv_detail.RowTemplate.Height = 24;
            this.dgv_detail.Size = new System.Drawing.Size(377, 299);
            this.dgv_detail.TabIndex = 48;
            // 
            // CId
            // 
            this.CId.HeaderText = "物件ID";
            this.CId.Name = "CId";
            this.CId.ReadOnly = true;
            // 
            // CName
            // 
            this.CName.HeaderText = "名稱";
            this.CName.Name = "CName";
            this.CName.ReadOnly = true;
            // 
            // CType
            // 
            this.CType.HeaderText = "系統類型";
            this.CType.Name = "CType";
            this.CType.ReadOnly = true;
            // 
            // NColor
            // 
            this.NColor.HeaderText = "覆蓋色";
            this.NColor.Name = "NColor";
            this.NColor.ReadOnly = true;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.dgv_detail);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(274, -1);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(386, 302);
            this.flowLayoutPanel1.TabIndex = 49;
            // 
            // btn_rollbackAll
            // 
            this.btn_rollbackAll.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_rollbackAll.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_rollbackAll.Location = new System.Drawing.Point(582, 307);
            this.btn_rollbackAll.Name = "btn_rollbackAll";
            this.btn_rollbackAll.Size = new System.Drawing.Size(72, 23);
            this.btn_rollbackAll.TabIndex = 50;
            this.btn_rollbackAll.Text = "全部復原";
            this.btn_rollbackAll.UseVisualStyleBackColor = true;
            this.btn_rollbackAll.Click += new System.EventHandler(this.btn_rollbackAll_Click);
            // 
            // btn_3D
            // 
            this.btn_3D.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_3D.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_3D.Location = new System.Drawing.Point(355, 307);
            this.btn_3D.Name = "btn_3D";
            this.btn_3D.Size = new System.Drawing.Size(72, 23);
            this.btn_3D.TabIndex = 51;
            this.btn_3D.Text = "3D視圖";
            this.btn_3D.UseVisualStyleBackColor = true;
            this.btn_3D.Click += new System.EventHandler(this.btn_3D_Click);
            // 
            // ColorPipeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(658, 337);
            this.Controls.Add(this.btn_3D);
            this.Controls.Add(this.btn_rollbackAll);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.btn_oriColor);
            this.Controls.Add(this.btn_coverColor);
            this.Controls.Add(this.stretchBtn);
            this.Controls.Add(this.btn_delete);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgv_allFile);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "ColorPipeForm";
            this.Text = "ColorPipeForm";
            this.Load += new System.EventHandler(this.ColorPipeForm_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_allFile)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_detail)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_delete;
        public System.Windows.Forms.DataGridView dgv_allFile;
        private System.Windows.Forms.DataGridViewTextBoxColumn filename;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_colour;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_create;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Button stretchBtn;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button btn_coverColor;
        private System.Windows.Forms.Button btn_oriColor;
        private System.Windows.Forms.DataGridView dgv_detail;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.DataGridViewTextBoxColumn CId;
        private System.Windows.Forms.DataGridViewTextBoxColumn CName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CType;
        private System.Windows.Forms.DataGridViewTextBoxColumn NColor;
        private System.Windows.Forms.Button btn_rollbackAll;
        private System.Windows.Forms.Button btn_3D;
    }
}