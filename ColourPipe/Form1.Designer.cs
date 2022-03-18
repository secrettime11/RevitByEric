namespace ColourPipe
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.dgv_allFile = new System.Windows.Forms.DataGridView();
            this.filename = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_show = new System.Windows.Forms.Button();
            this.btn_recovery = new System.Windows.Forms.Button();
            this.btn_recoveryDelete = new System.Windows.Forms.Button();
            this.btn_delete = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_colour = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_create = new System.Windows.Forms.Button();
            this.txt_filename = new ColourPipe.ZhmTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_allFile)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv_allFile
            // 
            this.dgv_allFile.AllowUserToAddRows = false;
            this.dgv_allFile.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_allFile.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_allFile.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.filename});
            this.dgv_allFile.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dgv_allFile.Location = new System.Drawing.Point(12, 122);
            this.dgv_allFile.Name = "dgv_allFile";
            this.dgv_allFile.ReadOnly = true;
            this.dgv_allFile.RowTemplate.Height = 24;
            this.dgv_allFile.Size = new System.Drawing.Size(240, 158);
            this.dgv_allFile.TabIndex = 20;
            this.dgv_allFile.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_allFile_CellDoubleClick);
            // 
            // filename
            // 
            this.filename.HeaderText = "檔案名稱";
            this.filename.Name = "filename";
            this.filename.ReadOnly = true;
            // 
            // btn_show
            // 
            this.btn_show.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_show.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_show.Location = new System.Drawing.Point(52, 297);
            this.btn_show.Name = "btn_show";
            this.btn_show.Size = new System.Drawing.Size(72, 23);
            this.btn_show.TabIndex = 31;
            this.btn_show.Text = "顯示";
            this.btn_show.UseVisualStyleBackColor = true;
            // 
            // btn_recovery
            // 
            this.btn_recovery.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_recovery.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_recovery.Location = new System.Drawing.Point(130, 297);
            this.btn_recovery.Name = "btn_recovery";
            this.btn_recovery.Size = new System.Drawing.Size(72, 23);
            this.btn_recovery.TabIndex = 32;
            this.btn_recovery.Text = "復原";
            this.btn_recovery.UseVisualStyleBackColor = true;
            // 
            // btn_recoveryDelete
            // 
            this.btn_recoveryDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_recoveryDelete.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_recoveryDelete.Location = new System.Drawing.Point(130, 328);
            this.btn_recoveryDelete.Name = "btn_recoveryDelete";
            this.btn_recoveryDelete.Size = new System.Drawing.Size(72, 23);
            this.btn_recoveryDelete.TabIndex = 33;
            this.btn_recoveryDelete.Text = "復原刪除";
            this.btn_recoveryDelete.UseVisualStyleBackColor = true;
            // 
            // btn_delete
            // 
            this.btn_delete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_delete.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_delete.Location = new System.Drawing.Point(52, 328);
            this.btn_delete.Name = "btn_delete";
            this.btn_delete.Size = new System.Drawing.Size(72, 23);
            this.btn_delete.TabIndex = 35;
            this.btn_delete.Text = "刪除";
            this.btn_delete.UseVisualStyleBackColor = true;
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
            // txt_filename
            // 
            this.txt_filename.Location = new System.Drawing.Point(62, 58);
            this.txt_filename.Name = "txt_filename";
            this.txt_filename.Placeholder = "";
            this.txt_filename.Size = new System.Drawing.Size(100, 25);
            this.txt_filename.TabIndex = 36;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txt_filename);
            this.groupBox1.Controls.Add(this.btn_create);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btn_colour);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("新細明體", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(240, 100);
            this.groupBox1.TabIndex = 34;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "建立新紀錄";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(263, 367);
            this.Controls.Add(this.btn_delete);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_recoveryDelete);
            this.Controls.Add(this.btn_recovery);
            this.Controls.Add(this.btn_show);
            this.Controls.Add(this.dgv_allFile);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Colour";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_allFile)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataGridView dgv_allFile;
        private System.Windows.Forms.Button btn_show;
        private System.Windows.Forms.Button btn_recovery;
        private System.Windows.Forms.Button btn_recoveryDelete;
        private System.Windows.Forms.DataGridViewTextBoxColumn filename;
        private System.Windows.Forms.Button btn_delete;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_colour;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_create;
        private ZhmTextBox txt_filename;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}

