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
            this.txt_fileName = new System.Windows.Forms.TextBox();
            this.txt_projectName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
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
            this.btn_rollbackAll = new System.Windows.Forms.Button();
            this.btn_3D = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cb_project = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cb_template = new System.Windows.Forms.ComboBox();
            this.txt_3dname = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_allFile)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_detail)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_delete
            // 
            this.btn_delete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_delete.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_delete.Location = new System.Drawing.Point(180, 375);
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
            this.dgv_allFile.Location = new System.Drawing.Point(12, 190);
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
            this.btn_colour.Size = new System.Drawing.Size(141, 23);
            this.btn_colour.TabIndex = 1;
            this.btn_colour.UseVisualStyleBackColor = false;
            this.btn_colour.Click += new System.EventHandler(this.btn_colour_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(12, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "檔名";
            // 
            // btn_create
            // 
            this.btn_create.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_create.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_create.Location = new System.Drawing.Point(180, 100);
            this.btn_create.Name = "btn_create";
            this.btn_create.Size = new System.Drawing.Size(48, 25);
            this.btn_create.TabIndex = 36;
            this.btn_create.Text = "建立";
            this.btn_create.UseVisualStyleBackColor = true;
            this.btn_create.Click += new System.EventHandler(this.btn_create_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txt_fileName);
            this.groupBox1.Controls.Add(this.txt_projectName);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.btn_create);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btn_colour);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("新細明體", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(240, 140);
            this.groupBox1.TabIndex = 40;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "建立";
            // 
            // txt_fileName
            // 
            this.txt_fileName.Location = new System.Drawing.Point(57, 100);
            this.txt_fileName.Name = "txt_fileName";
            this.txt_fileName.Size = new System.Drawing.Size(117, 25);
            this.txt_fileName.TabIndex = 39;
            this.txt_fileName.Click += new System.EventHandler(this.txt_fileName_Click);
            // 
            // txt_projectName
            // 
            this.txt_projectName.Location = new System.Drawing.Point(57, 61);
            this.txt_projectName.Name = "txt_projectName";
            this.txt_projectName.Size = new System.Drawing.Size(171, 25);
            this.txt_projectName.TabIndex = 38;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label4.Location = new System.Drawing.Point(12, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 19);
            this.label4.TabIndex = 37;
            this.label4.Text = "專案";
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
            this.stretchBtn.Location = new System.Drawing.Point(258, -11);
            this.stretchBtn.Name = "stretchBtn";
            this.stretchBtn.Size = new System.Drawing.Size(16, 421);
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
            this.btn_coverColor.Location = new System.Drawing.Point(5, 13);
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
            this.btn_oriColor.Location = new System.Drawing.Point(5, 54);
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
            this.dgv_detail.Location = new System.Drawing.Point(83, 13);
            this.dgv_detail.Name = "dgv_detail";
            this.dgv_detail.ReadOnly = true;
            this.dgv_detail.RowTemplate.Height = 24;
            this.dgv_detail.Size = new System.Drawing.Size(288, 331);
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
            // btn_rollbackAll
            // 
            this.btn_rollbackAll.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_rollbackAll.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_rollbackAll.Location = new System.Drawing.Point(5, 95);
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
            this.btn_3D.Location = new System.Drawing.Point(299, 350);
            this.btn_3D.Name = "btn_3D";
            this.btn_3D.Size = new System.Drawing.Size(72, 49);
            this.btn_3D.TabIndex = 51;
            this.btn_3D.Text = "3D視圖";
            this.btn_3D.UseVisualStyleBackColor = true;
            this.btn_3D.Click += new System.EventHandler(this.btn_3D_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(8, 162);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 19);
            this.label3.TabIndex = 37;
            this.label3.Text = "專案名稱";
            // 
            // cb_project
            // 
            this.cb_project.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_project.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cb_project.Font = new System.Drawing.Font("新細明體", 11F);
            this.cb_project.FormattingEnabled = true;
            this.cb_project.Location = new System.Drawing.Point(83, 158);
            this.cb_project.Name = "cb_project";
            this.cb_project.Size = new System.Drawing.Size(169, 23);
            this.cb_project.TabIndex = 52;
            this.cb_project.SelectedIndexChanged += new System.EventHandler(this.cb_project_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.cb_template);
            this.panel1.Controls.Add(this.txt_3dname);
            this.panel1.Controls.Add(this.btn_coverColor);
            this.panel1.Controls.Add(this.dgv_detail);
            this.panel1.Controls.Add(this.btn_3D);
            this.panel1.Controls.Add(this.btn_oriColor);
            this.panel1.Controls.Add(this.btn_rollbackAll);
            this.panel1.Location = new System.Drawing.Point(275, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(386, 411);
            this.panel1.TabIndex = 53;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label5.Location = new System.Drawing.Point(81, 351);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 19);
            this.label5.TabIndex = 56;
            this.label5.Text = "視圖樣板";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label6.Location = new System.Drawing.Point(81, 378);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 19);
            this.label6.TabIndex = 54;
            this.label6.Text = "視圖名稱";
            // 
            // cb_template
            // 
            this.cb_template.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_template.FormattingEnabled = true;
            this.cb_template.Location = new System.Drawing.Point(156, 350);
            this.cb_template.Name = "cb_template";
            this.cb_template.Size = new System.Drawing.Size(128, 20);
            this.cb_template.TabIndex = 55;
            this.cb_template.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // txt_3dname
            // 
            this.txt_3dname.Location = new System.Drawing.Point(156, 376);
            this.txt_3dname.Name = "txt_3dname";
            this.txt_3dname.Size = new System.Drawing.Size(128, 22);
            this.txt_3dname.TabIndex = 52;
            // 
            // ColorPipeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(658, 402);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cb_project);
            this.Controls.Add(this.label3);
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
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.DataGridViewTextBoxColumn CId;
        private System.Windows.Forms.DataGridViewTextBoxColumn CName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CType;
        private System.Windows.Forms.DataGridViewTextBoxColumn NColor;
        private System.Windows.Forms.Button btn_rollbackAll;
        private System.Windows.Forms.Button btn_3D;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cb_project;
        private System.Windows.Forms.TextBox txt_fileName;
        private System.Windows.Forms.TextBox txt_projectName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txt_3dname;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cb_template;
        private System.Windows.Forms.Label label5;
    }
}