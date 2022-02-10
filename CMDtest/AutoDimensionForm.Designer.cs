namespace CMDtest
{
    partial class AutoDimensionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AutoDimensionForm));
            this.btn_execute = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ckcb_pipeKits = new System.Windows.Forms.CheckBox();
            this.ckcb_pipeAccessory = new System.Windows.Forms.CheckBox();
            this.ckcb_water = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rdb_ydimRight = new System.Windows.Forms.RadioButton();
            this.rdb_ydimLeft = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rdb_xdimDown = new System.Windows.Forms.RadioButton();
            this.rdb_xdimTop = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.txt_range = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_pipeDim = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_execute
            // 
            this.btn_execute.Font = new System.Drawing.Font("新細明體", 11F);
            this.btn_execute.Location = new System.Drawing.Point(193, 183);
            this.btn_execute.Name = "btn_execute";
            this.btn_execute.Size = new System.Drawing.Size(75, 27);
            this.btn_execute.TabIndex = 0;
            this.btn_execute.Text = "標註";
            this.btn_execute.UseVisualStyleBackColor = true;
            this.btn_execute.Click += new System.EventHandler(this.btn_execute_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ckcb_pipeKits);
            this.groupBox1.Controls.Add(this.ckcb_pipeAccessory);
            this.groupBox1.Controls.Add(this.ckcb_water);
            this.groupBox1.Font = new System.Drawing.Font("新細明體", 11F);
            this.groupBox1.Location = new System.Drawing.Point(7, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(92, 114);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "標註類型";
            // 
            // ckcb_pipeKits
            // 
            this.ckcb_pipeKits.AutoSize = true;
            this.ckcb_pipeKits.Checked = true;
            this.ckcb_pipeKits.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckcb_pipeKits.Location = new System.Drawing.Point(12, 70);
            this.ckcb_pipeKits.Name = "ckcb_pipeKits";
            this.ckcb_pipeKits.Size = new System.Drawing.Size(71, 19);
            this.ckcb_pipeKits.TabIndex = 2;
            this.ckcb_pipeKits.Text = "管配件";
            this.ckcb_pipeKits.UseVisualStyleBackColor = true;
            this.ckcb_pipeKits.Visible = false;
            // 
            // ckcb_pipeAccessory
            // 
            this.ckcb_pipeAccessory.AutoSize = true;
            this.ckcb_pipeAccessory.Checked = true;
            this.ckcb_pipeAccessory.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckcb_pipeAccessory.Location = new System.Drawing.Point(12, 45);
            this.ckcb_pipeAccessory.Name = "ckcb_pipeAccessory";
            this.ckcb_pipeAccessory.Size = new System.Drawing.Size(71, 19);
            this.ckcb_pipeAccessory.TabIndex = 1;
            this.ckcb_pipeAccessory.Text = "管附件";
            this.ckcb_pipeAccessory.UseVisualStyleBackColor = true;
            // 
            // ckcb_water
            // 
            this.ckcb_water.AutoSize = true;
            this.ckcb_water.Checked = true;
            this.ckcb_water.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckcb_water.Location = new System.Drawing.Point(12, 21);
            this.ckcb_water.Name = "ckcb_water";
            this.ckcb_water.Size = new System.Drawing.Size(71, 19);
            this.ckcb_water.TabIndex = 0;
            this.ckcb_water.Text = "撒水頭";
            this.ckcb_water.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Font = new System.Drawing.Font("新細明體", 11F);
            this.groupBox2.Location = new System.Drawing.Point(105, 7);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(171, 114);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "標註方向";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rdb_ydimRight);
            this.groupBox4.Controls.Add(this.rdb_ydimLeft);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Location = new System.Drawing.Point(7, 63);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(159, 43);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            // 
            // rdb_ydimRight
            // 
            this.rdb_ydimRight.AutoSize = true;
            this.rdb_ydimRight.Location = new System.Drawing.Point(107, 17);
            this.rdb_ydimRight.Name = "rdb_ydimRight";
            this.rdb_ydimRight.Size = new System.Drawing.Size(40, 19);
            this.rdb_ydimRight.TabIndex = 4;
            this.rdb_ydimRight.Text = "右";
            this.rdb_ydimRight.UseVisualStyleBackColor = true;
            // 
            // rdb_ydimLeft
            // 
            this.rdb_ydimLeft.AutoSize = true;
            this.rdb_ydimLeft.Checked = true;
            this.rdb_ydimLeft.Location = new System.Drawing.Point(61, 17);
            this.rdb_ydimLeft.Name = "rdb_ydimLeft";
            this.rdb_ydimLeft.Size = new System.Drawing.Size(40, 19);
            this.rdb_ydimLeft.TabIndex = 3;
            this.rdb_ydimLeft.TabStop = true;
            this.rdb_ydimLeft.Text = "左";
            this.rdb_ydimLeft.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "Y軸";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rdb_xdimDown);
            this.groupBox3.Controls.Add(this.rdb_xdimTop);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(7, 18);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(159, 43);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            // 
            // rdb_xdimDown
            // 
            this.rdb_xdimDown.AutoSize = true;
            this.rdb_xdimDown.Location = new System.Drawing.Point(107, 17);
            this.rdb_xdimDown.Name = "rdb_xdimDown";
            this.rdb_xdimDown.Size = new System.Drawing.Size(40, 19);
            this.rdb_xdimDown.TabIndex = 4;
            this.rdb_xdimDown.Text = "下";
            this.rdb_xdimDown.UseVisualStyleBackColor = true;
            // 
            // rdb_xdimTop
            // 
            this.rdb_xdimTop.AutoSize = true;
            this.rdb_xdimTop.Checked = true;
            this.rdb_xdimTop.Location = new System.Drawing.Point(61, 17);
            this.rdb_xdimTop.Name = "rdb_xdimTop";
            this.rdb_xdimTop.Size = new System.Drawing.Size(40, 19);
            this.rdb_xdimTop.TabIndex = 3;
            this.rdb_xdimTop.TabStop = true;
            this.rdb_xdimTop.Text = "上";
            this.rdb_xdimTop.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "X軸";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.txt_range);
            this.groupBox6.Controls.Add(this.label3);
            this.groupBox6.Font = new System.Drawing.Font("新細明體", 11F);
            this.groupBox6.Location = new System.Drawing.Point(7, 127);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(92, 83);
            this.groupBox6.TabIndex = 10;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "範圍設定";
            // 
            // txt_range
            // 
            this.txt_range.Location = new System.Drawing.Point(13, 47);
            this.txt_range.Name = "txt_range";
            this.txt_range.Size = new System.Drawing.Size(63, 25);
            this.txt_range.TabIndex = 1;
            this.txt_range.Text = "200";
            this.txt_range.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "半徑";
            // 
            // btn_pipeDim
            // 
            this.btn_pipeDim.Font = new System.Drawing.Font("新細明體", 11F);
            this.btn_pipeDim.Location = new System.Drawing.Point(112, 183);
            this.btn_pipeDim.Name = "btn_pipeDim";
            this.btn_pipeDim.Size = new System.Drawing.Size(75, 27);
            this.btn_pipeDim.TabIndex = 11;
            this.btn_pipeDim.Text = "管標註";
            this.btn_pipeDim.UseVisualStyleBackColor = true;
            this.btn_pipeDim.Click += new System.EventHandler(this.btn_pipeDim_Click);
            // 
            // AutoDimensionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(288, 218);
            this.Controls.Add(this.btn_pipeDim);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_execute);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AutoDimensionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "框選標註";
            this.Load += new System.EventHandler(this.AutoDimensionForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_execute;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox ckcb_pipeAccessory;
        private System.Windows.Forms.CheckBox ckcb_water;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rdb_xdimDown;
        private System.Windows.Forms.RadioButton rdb_xdimTop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton rdb_ydimRight;
        private System.Windows.Forms.RadioButton rdb_ydimLeft;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TextBox txt_range;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox ckcb_pipeKits;
        private System.Windows.Forms.Button btn_pipeDim;
    }
}