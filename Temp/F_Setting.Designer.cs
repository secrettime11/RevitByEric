namespace Temp
{
    partial class F_Setting
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
            this.lb_before = new System.Windows.Forms.ListBox();
            this.lb_after = new System.Windows.Forms.ListBox();
            this.btn_trans = new System.Windows.Forms.Button();
            this.btn_transBack = new System.Windows.Forms.Button();
            this.btn_down = new System.Windows.Forms.Button();
            this.btn_up = new System.Windows.Forms.Button();
            this.txt_search = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lb_before
            // 
            this.lb_before.Font = new System.Drawing.Font("新細明體", 12F);
            this.lb_before.FormattingEnabled = true;
            this.lb_before.ItemHeight = 16;
            this.lb_before.Location = new System.Drawing.Point(12, 36);
            this.lb_before.Margin = new System.Windows.Forms.Padding(5);
            this.lb_before.Name = "lb_before";
            this.lb_before.Size = new System.Drawing.Size(188, 276);
            this.lb_before.TabIndex = 0;
            this.lb_before.DoubleClick += new System.EventHandler(this.lb_before_DoubleClick);
            // 
            // lb_after
            // 
            this.lb_after.Font = new System.Drawing.Font("新細明體", 12F);
            this.lb_after.FormattingEnabled = true;
            this.lb_after.ItemHeight = 16;
            this.lb_after.Location = new System.Drawing.Point(252, 36);
            this.lb_after.Margin = new System.Windows.Forms.Padding(5);
            this.lb_after.Name = "lb_after";
            this.lb_after.Size = new System.Drawing.Size(188, 276);
            this.lb_after.TabIndex = 1;
            this.lb_after.DoubleClick += new System.EventHandler(this.lb_after_DoubleClick);
            // 
            // btn_trans
            // 
            this.btn_trans.Location = new System.Drawing.Point(209, 131);
            this.btn_trans.Name = "btn_trans";
            this.btn_trans.Size = new System.Drawing.Size(35, 23);
            this.btn_trans.TabIndex = 2;
            this.btn_trans.Text = "▶";
            this.btn_trans.UseVisualStyleBackColor = true;
            this.btn_trans.Click += new System.EventHandler(this.btn_trans_Click);
            // 
            // btn_transBack
            // 
            this.btn_transBack.Location = new System.Drawing.Point(209, 160);
            this.btn_transBack.Name = "btn_transBack";
            this.btn_transBack.Size = new System.Drawing.Size(35, 23);
            this.btn_transBack.TabIndex = 3;
            this.btn_transBack.Text = "◀";
            this.btn_transBack.UseVisualStyleBackColor = true;
            this.btn_transBack.Click += new System.EventHandler(this.btn_transBack_Click);
            // 
            // btn_down
            // 
            this.btn_down.Location = new System.Drawing.Point(448, 160);
            this.btn_down.Name = "btn_down";
            this.btn_down.Size = new System.Drawing.Size(35, 23);
            this.btn_down.TabIndex = 5;
            this.btn_down.Text = "▼";
            this.btn_down.UseVisualStyleBackColor = true;
            this.btn_down.Click += new System.EventHandler(this.btn_down_Click);
            // 
            // btn_up
            // 
            this.btn_up.Location = new System.Drawing.Point(448, 131);
            this.btn_up.Name = "btn_up";
            this.btn_up.Size = new System.Drawing.Size(35, 23);
            this.btn_up.TabIndex = 4;
            this.btn_up.Text = "▲";
            this.btn_up.UseVisualStyleBackColor = true;
            this.btn_up.Click += new System.EventHandler(this.btn_up_Click);
            // 
            // txt_search
            // 
            this.txt_search.Location = new System.Drawing.Point(12, 12);
            this.txt_search.Name = "txt_search";
            this.txt_search.Size = new System.Drawing.Size(188, 22);
            this.txt_search.TabIndex = 6;
            this.txt_search.TextChanged += new System.EventHandler(this.txt_search_TextChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(209, 289);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(35, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "W";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(209, 260);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(35, 23);
            this.button2.TabIndex = 8;
            this.button2.Text = "R";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // F_Setting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 336);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txt_search);
            this.Controls.Add(this.btn_down);
            this.Controls.Add(this.btn_up);
            this.Controls.Add(this.btn_transBack);
            this.Controls.Add(this.btn_trans);
            this.Controls.Add(this.lb_after);
            this.Controls.Add(this.lb_before);
            this.Name = "F_Setting";
            this.Text = "F_Setting";
            this.Load += new System.EventHandler(this.F_Setting_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lb_before;
        private System.Windows.Forms.ListBox lb_after;
        private System.Windows.Forms.Button btn_trans;
        private System.Windows.Forms.Button btn_transBack;
        private System.Windows.Forms.Button btn_down;
        private System.Windows.Forms.Button btn_up;
        private System.Windows.Forms.TextBox txt_search;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}