namespace RubberSoft.Tools
{
    partial class FrmTools
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
            this.BtnBack = new DevExpress.XtraEditors.SimpleButton();
            this.LinkGetBuyData = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.LinkAdjustOutstandingDebt = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.LinkClearData = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.LinkTerminal = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.SuspendLayout();
            // 
            // BtnBack
            // 
            this.BtnBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnBack.Appearance.Font = new System.Drawing.Font("Kanit", 12F);
            this.BtnBack.Appearance.Options.UseFont = true;
            this.BtnBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnBack.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.BtnBack.Location = new System.Drawing.Point(427, 203);
            this.BtnBack.LookAndFeel.SkinName = "VS2010";
            this.BtnBack.LookAndFeel.UseDefaultLookAndFeel = false;
            this.BtnBack.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BtnBack.Name = "BtnBack";
            this.BtnBack.Size = new System.Drawing.Size(149, 56);
            this.BtnBack.TabIndex = 442;
            this.BtnBack.Text = "ปิดโปรแกรม";
            this.BtnBack.Click += new System.EventHandler(this.BtnBack_Click);
            // 
            // LinkGetBuyData
            // 
            this.LinkGetBuyData.Appearance.Font = new System.Drawing.Font("Kanit", 12F);
            this.LinkGetBuyData.Appearance.Options.UseFont = true;
            this.LinkGetBuyData.Location = new System.Drawing.Point(33, 31);
            this.LinkGetBuyData.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.LinkGetBuyData.Name = "LinkGetBuyData";
            this.LinkGetBuyData.Size = new System.Drawing.Size(81, 25);
            this.LinkGetBuyData.TabIndex = 26;
            this.LinkGetBuyData.Text = "ข้อมูลการซื้อ";
            this.LinkGetBuyData.Click += new System.EventHandler(this.LinkGetBuyData_Click);
            // 
            // LinkAdjustOutstandingDebt
            // 
            this.LinkAdjustOutstandingDebt.Appearance.Font = new System.Drawing.Font("Kanit", 12F);
            this.LinkAdjustOutstandingDebt.Appearance.Options.UseFont = true;
            this.LinkAdjustOutstandingDebt.Location = new System.Drawing.Point(33, 82);
            this.LinkAdjustOutstandingDebt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.LinkAdjustOutstandingDebt.Name = "LinkAdjustOutstandingDebt";
            this.LinkAdjustOutstandingDebt.Size = new System.Drawing.Size(163, 25);
            this.LinkAdjustOutstandingDebt.TabIndex = 27;
            this.LinkAdjustOutstandingDebt.Text = "Log ราคาล่วงหน้า การซื้อ";
            this.LinkAdjustOutstandingDebt.Click += new System.EventHandler(this.LinkAdjustOutstandingDebt_Click);
            // 
            // LinkClearData
            // 
            this.LinkClearData.Appearance.Font = new System.Drawing.Font("Kanit", 12F);
            this.LinkClearData.Appearance.Options.UseFont = true;
            this.LinkClearData.Location = new System.Drawing.Point(33, 203);
            this.LinkClearData.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.LinkClearData.Name = "LinkClearData";
            this.LinkClearData.Size = new System.Drawing.Size(73, 25);
            this.LinkClearData.TabIndex = 28;
            this.LinkClearData.Text = "เคลียข้อมูล";
            this.LinkClearData.Click += new System.EventHandler(this.LinkClearData_Click);
            // 
            // LinkTerminal
            // 
            this.LinkTerminal.Appearance.Font = new System.Drawing.Font("Kanit", 12F);
            this.LinkTerminal.Appearance.Options.UseFont = true;
            this.LinkTerminal.Location = new System.Drawing.Point(33, 143);
            this.LinkTerminal.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.LinkTerminal.Name = "LinkTerminal";
            this.LinkTerminal.Size = new System.Drawing.Size(81, 25);
            this.LinkTerminal.TabIndex = 29;
            this.LinkTerminal.Text = "เครื่องใช้งาน";
            this.LinkTerminal.Click += new System.EventHandler(this.LinkTerminal_Click);
            // 
            // FrmTools
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(597, 283);
            this.ControlBox = false;
            this.Controls.Add(this.BtnBack);
            this.Controls.Add(this.LinkTerminal);
            this.Controls.Add(this.LinkClearData);
            this.Controls.Add(this.LinkAdjustOutstandingDebt);
            this.Controls.Add(this.LinkGetBuyData);
            this.LookAndFeel.SkinName = "The Bezier";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FrmTools";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tools";
            this.Load += new System.EventHandler(this.FrmTools_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        internal DevExpress.XtraEditors.SimpleButton BtnBack;
        private DevExpress.XtraEditors.HyperlinkLabelControl LinkGetBuyData;
        private DevExpress.XtraEditors.HyperlinkLabelControl LinkAdjustOutstandingDebt;
        private DevExpress.XtraEditors.HyperlinkLabelControl LinkClearData;
        private DevExpress.XtraEditors.HyperlinkLabelControl LinkTerminal;
    }
}