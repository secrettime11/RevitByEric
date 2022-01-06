namespace Revit_v2018.ExtendForm
{
    partial class SymbolPlacement_SettingForm
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
            this.btn_down = new System.Windows.Forms.Button();
            this.btn_up = new System.Windows.Forms.Button();
            this.btn_transBack = new System.Windows.Forms.Button();
            this.btn_trans = new System.Windows.Forms.Button();
            this.lb_after = new System.Windows.Forms.ListBox();
            this.lb_before = new System.Windows.Forms.ListBox();
            this.btn_save = new System.Windows.Forms.Button();
            this.txt_search = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_down
            // 
            this.btn_down.Location = new System.Drawing.Point(470, 166);
            this.btn_down.Name = "btn_down";
            this.btn_down.Size = new System.Drawing.Size(35, 23);
            this.btn_down.TabIndex = 11;
            this.btn_down.Text = "▼";
            this.btn_down.UseVisualStyleBackColor = true;
            this.btn_down.Click += new System.EventHandler(this.btn_down_Click);
            // 
            // btn_up
            // 
            this.btn_up.Location = new System.Drawing.Point(470, 137);
            this.btn_up.Name = "btn_up";
            this.btn_up.Size = new System.Drawing.Size(35, 23);
            this.btn_up.TabIndex = 10;
            this.btn_up.Text = "▲";
            this.btn_up.UseVisualStyleBackColor = true;
            this.btn_up.Click += new System.EventHandler(this.btn_up_Click);
            // 
            // btn_transBack
            // 
            this.btn_transBack.Location = new System.Drawing.Point(209, 166);
            this.btn_transBack.Name = "btn_transBack";
            this.btn_transBack.Size = new System.Drawing.Size(49, 32);
            this.btn_transBack.TabIndex = 9;
            this.btn_transBack.Text = "移除";
            this.btn_transBack.UseVisualStyleBackColor = true;
            this.btn_transBack.Click += new System.EventHandler(this.btn_transBack_Click);
            // 
            // btn_trans
            // 
            this.btn_trans.Location = new System.Drawing.Point(209, 128);
            this.btn_trans.Name = "btn_trans";
            this.btn_trans.Size = new System.Drawing.Size(49, 32);
            this.btn_trans.TabIndex = 8;
            this.btn_trans.Text = "添加";
            this.btn_trans.UseVisualStyleBackColor = true;
            this.btn_trans.Click += new System.EventHandler(this.btn_trans_Click);
            // 
            // lb_after
            // 
            this.lb_after.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.lb_after.Font = new System.Drawing.Font("新細明體", 9F);
            this.lb_after.FormattingEnabled = true;
            this.lb_after.ItemHeight = 16;
            this.lb_after.Location = new System.Drawing.Point(275, 42);
            this.lb_after.Margin = new System.Windows.Forms.Padding(5);
            this.lb_after.Name = "lb_after";
            this.lb_after.Size = new System.Drawing.Size(188, 276);
            this.lb_after.TabIndex = 7;
            this.lb_after.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.ListBoxGroupRange_DrawItem);
            this.lb_after.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.ListBoxGroupRange_MeasureItem);
            this.lb_after.DoubleClick += new System.EventHandler(this.lb_after_DoubleClick);
            // 
            // lb_before
            // 
            this.lb_before.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.lb_before.Font = new System.Drawing.Font("新細明體", 9F);
            this.lb_before.FormattingEnabled = true;
            this.lb_before.ItemHeight = 16;
            this.lb_before.Location = new System.Drawing.Point(12, 42);
            this.lb_before.Margin = new System.Windows.Forms.Padding(5);
            this.lb_before.Name = "lb_before";
            this.lb_before.Size = new System.Drawing.Size(188, 276);
            this.lb_before.TabIndex = 6;
            this.lb_before.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.ListBoxGroupRange_DrawItem);
            this.lb_before.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.ListBoxGroupRange_MeasureItem);
            this.lb_before.DoubleClick += new System.EventHandler(this.lb_before_DoubleClick);
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(471, 265);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(35, 53);
            this.btn_save.TabIndex = 12;
            this.btn_save.Text = "儲存";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // txt_search
            // 
            this.txt_search.Location = new System.Drawing.Point(12, 12);
            this.txt_search.Name = "txt_search";
            this.txt_search.Size = new System.Drawing.Size(188, 22);
            this.txt_search.TabIndex = 13;
            this.txt_search.TextChanged += new System.EventHandler(this.txt_search_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(275, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 14;
            this.label1.Text = "使用者頁籤";
            // 
            // SymbolPlacement_SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 328);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_search);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.btn_down);
            this.Controls.Add(this.btn_up);
            this.Controls.Add(this.btn_transBack);
            this.Controls.Add(this.btn_trans);
            this.Controls.Add(this.lb_after);
            this.Controls.Add(this.lb_before);
            this.Name = "SymbolPlacement_SettingForm";
            this.Text = "SymbolPlacement_SettingForm";
            this.Load += new System.EventHandler(this.SymbolPlacement_SettingForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_down;
        private System.Windows.Forms.Button btn_up;
        private System.Windows.Forms.Button btn_transBack;
        private System.Windows.Forms.Button btn_trans;
        private System.Windows.Forms.ListBox lb_after;
        private System.Windows.Forms.ListBox lb_before;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.TextBox txt_search;
        private System.Windows.Forms.Label label1;
    }
}