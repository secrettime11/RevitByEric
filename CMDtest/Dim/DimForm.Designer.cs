namespace CMDtest.Dim
{
    partial class DimForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ckcb_dimHanger = new System.Windows.Forms.CheckBox();
            this.ckcb_dimY = new System.Windows.Forms.CheckBox();
            this.ckcb_dimX = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.ckcb_baseY_right = new System.Windows.Forms.CheckBox();
            this.ckcb_baseY_left = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ckcb_baseX_down = new System.Windows.Forms.CheckBox();
            this.ckcb_baseX_up = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_dim = new System.Windows.Forms.Button();
            this.btn_ini = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_pipeDiameter = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ckcb_dimHanger);
            this.groupBox1.Controls.Add(this.ckcb_dimY);
            this.groupBox1.Controls.Add(this.ckcb_dimX);
            this.groupBox1.Location = new System.Drawing.Point(8, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(178, 50);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "標註";
            // 
            // ckcb_dimHanger
            // 
            this.ckcb_dimHanger.AutoSize = true;
            this.ckcb_dimHanger.Checked = true;
            this.ckcb_dimHanger.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckcb_dimHanger.Location = new System.Drawing.Point(119, 21);
            this.ckcb_dimHanger.Name = "ckcb_dimHanger";
            this.ckcb_dimHanger.Size = new System.Drawing.Size(48, 16);
            this.ckcb_dimHanger.TabIndex = 3;
            this.ckcb_dimHanger.Text = "吊架";
            this.ckcb_dimHanger.UseVisualStyleBackColor = true;
            // 
            // ckcb_dimY
            // 
            this.ckcb_dimY.AutoSize = true;
            this.ckcb_dimY.Checked = true;
            this.ckcb_dimY.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckcb_dimY.Location = new System.Drawing.Point(69, 21);
            this.ckcb_dimY.Name = "ckcb_dimY";
            this.ckcb_dimY.Size = new System.Drawing.Size(44, 16);
            this.ckcb_dimY.TabIndex = 2;
            this.ckcb_dimY.Text = "Y軸";
            this.ckcb_dimY.UseVisualStyleBackColor = true;
            // 
            // ckcb_dimX
            // 
            this.ckcb_dimX.AutoSize = true;
            this.ckcb_dimX.Checked = true;
            this.ckcb_dimX.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckcb_dimX.Location = new System.Drawing.Point(19, 21);
            this.ckcb_dimX.Name = "ckcb_dimX";
            this.ckcb_dimX.Size = new System.Drawing.Size(44, 16);
            this.ckcb_dimX.TabIndex = 1;
            this.ckcb_dimX.Text = "X軸";
            this.ckcb_dimX.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.ckcb_baseY_right);
            this.groupBox3.Controls.Add(this.ckcb_baseY_left);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.ckcb_baseX_down);
            this.groupBox3.Controls.Add(this.ckcb_baseX_up);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Location = new System.Drawing.Point(8, 117);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(178, 83);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "標註基準";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(133, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "--------------------------------";
            // 
            // ckcb_baseY_right
            // 
            this.ckcb_baseY_right.AutoSize = true;
            this.ckcb_baseY_right.Checked = true;
            this.ckcb_baseY_right.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckcb_baseY_right.Location = new System.Drawing.Point(116, 58);
            this.ckcb_baseY_right.Name = "ckcb_baseY_right";
            this.ckcb_baseY_right.Size = new System.Drawing.Size(36, 16);
            this.ckcb_baseY_right.TabIndex = 8;
            this.ckcb_baseY_right.Text = "右";
            this.ckcb_baseY_right.UseVisualStyleBackColor = true;
            this.ckcb_baseY_right.CheckedChanged += new System.EventHandler(this.ckcb_baseY_right_CheckedChanged);
            // 
            // ckcb_baseY_left
            // 
            this.ckcb_baseY_left.AutoSize = true;
            this.ckcb_baseY_left.Location = new System.Drawing.Point(66, 58);
            this.ckcb_baseY_left.Name = "ckcb_baseY_left";
            this.ckcb_baseY_left.Size = new System.Drawing.Size(36, 16);
            this.ckcb_baseY_left.TabIndex = 7;
            this.ckcb_baseY_left.Text = "左";
            this.ckcb_baseY_left.UseVisualStyleBackColor = true;
            this.ckcb_baseY_left.CheckedChanged += new System.EventHandler(this.ckcb_baseY_left_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "Y軸";
            // 
            // ckcb_baseX_down
            // 
            this.ckcb_baseX_down.AutoSize = true;
            this.ckcb_baseX_down.Location = new System.Drawing.Point(116, 22);
            this.ckcb_baseX_down.Name = "ckcb_baseX_down";
            this.ckcb_baseX_down.Size = new System.Drawing.Size(36, 16);
            this.ckcb_baseX_down.TabIndex = 5;
            this.ckcb_baseX_down.Text = "下";
            this.ckcb_baseX_down.UseVisualStyleBackColor = true;
            this.ckcb_baseX_down.CheckedChanged += new System.EventHandler(this.ckcb_baseX_down_CheckedChanged);
            // 
            // ckcb_baseX_up
            // 
            this.ckcb_baseX_up.AutoSize = true;
            this.ckcb_baseX_up.Checked = true;
            this.ckcb_baseX_up.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckcb_baseX_up.Location = new System.Drawing.Point(66, 22);
            this.ckcb_baseX_up.Name = "ckcb_baseX_up";
            this.ckcb_baseX_up.Size = new System.Drawing.Size(36, 16);
            this.ckcb_baseX_up.TabIndex = 4;
            this.ckcb_baseX_up.Text = "上";
            this.ckcb_baseX_up.UseVisualStyleBackColor = true;
            this.ckcb_baseX_up.CheckedChanged += new System.EventHandler(this.ckcb_baseX_up_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(25, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "X軸";
            // 
            // btn_dim
            // 
            this.btn_dim.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btn_dim.Location = new System.Drawing.Point(101, 206);
            this.btn_dim.Name = "btn_dim";
            this.btn_dim.Size = new System.Drawing.Size(85, 29);
            this.btn_dim.TabIndex = 6;
            this.btn_dim.Text = "標註";
            this.btn_dim.UseVisualStyleBackColor = true;
            this.btn_dim.Click += new System.EventHandler(this.btn_dim_Click);
            // 
            // btn_ini
            // 
            this.btn_ini.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btn_ini.Location = new System.Drawing.Point(8, 206);
            this.btn_ini.Name = "btn_ini";
            this.btn_ini.Size = new System.Drawing.Size(87, 29);
            this.btn_ini.TabIndex = 7;
            this.btn_ini.Text = "設為默認";
            this.btn_ini.UseVisualStyleBackColor = true;
            this.btn_ini.Click += new System.EventHandler(this.btn_ini_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "大於等於";
            // 
            // txt_pipeDiameter
            // 
            this.txt_pipeDiameter.Location = new System.Drawing.Point(71, 17);
            this.txt_pipeDiameter.Name = "txt_pipeDiameter";
            this.txt_pipeDiameter.Size = new System.Drawing.Size(65, 22);
            this.txt_pipeDiameter.TabIndex = 1;
            this.txt_pipeDiameter.Text = "50";
            this.txt_pipeDiameter.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(144, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "mm";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txt_pipeDiameter);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(8, 61);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(178, 50);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "幹管直徑";
            // 
            // DimForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(196, 239);
            this.Controls.Add(this.btn_ini);
            this.Controls.Add(this.btn_dim);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "DimForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DimForm";
            this.Load += new System.EventHandler(this.DimForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox ckcb_dimHanger;
        private System.Windows.Forms.CheckBox ckcb_dimY;
        private System.Windows.Forms.CheckBox ckcb_dimX;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox ckcb_baseY_right;
        private System.Windows.Forms.CheckBox ckcb_baseY_left;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox ckcb_baseX_down;
        private System.Windows.Forms.CheckBox ckcb_baseX_up;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_dim;
        private System.Windows.Forms.Button btn_ini;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_pipeDiameter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}