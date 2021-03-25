namespace RubberSoft.Main
{
    partial class FrmPayment
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPayment));
            this.XtraBack = new DevExpress.XtraEditors.XtraScrollableControl();
            this.CkIsPrinter = new DevExpress.XtraEditors.CheckEdit();
            this.CkShowPrinter = new DevExpress.XtraEditors.CheckEdit();
            this.CboPrinterList = new DevExpress.XtraEditors.ComboBoxEdit();
            this.BtnCloseBill = new DevExpress.XtraEditors.SimpleButton();
            this.BtnBack = new DevExpress.XtraEditors.SimpleButton();
            this.DocShowBill = new DevExpress.XtraPrinting.Preview.DocumentViewer();
            this.XtraBack.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CkIsPrinter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CkShowPrinter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CboPrinterList.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // XtraBack
            // 
            this.XtraBack.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
            this.XtraBack.Appearance.Options.UseBackColor = true;
            this.XtraBack.Controls.Add(this.CkIsPrinter);
            this.XtraBack.Controls.Add(this.CkShowPrinter);
            this.XtraBack.Controls.Add(this.CboPrinterList);
            this.XtraBack.Controls.Add(this.BtnCloseBill);
            this.XtraBack.Controls.Add(this.BtnBack);
            this.XtraBack.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.XtraBack.Location = new System.Drawing.Point(0, 673);
            this.XtraBack.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.XtraBack.Name = "XtraBack";
            this.XtraBack.Size = new System.Drawing.Size(1070, 67);
            this.XtraBack.TabIndex = 15;
            // 
            // CkIsPrinter
            // 
            this.CkIsPrinter.Location = new System.Drawing.Point(838, 34);
            this.CkIsPrinter.Name = "CkIsPrinter";
            this.CkIsPrinter.Properties.Appearance.Font = new System.Drawing.Font("Kanit", 9F);
            this.CkIsPrinter.Properties.Appearance.Options.UseFont = true;
            this.CkIsPrinter.Properties.AutoWidth = true;
            this.CkIsPrinter.Properties.Caption = "พิมพ์ใบเสร็จ";
            this.CkIsPrinter.Size = new System.Drawing.Size(83, 23);
            this.CkIsPrinter.TabIndex = 448;
            // 
            // CkShowPrinter
            // 
            this.CkShowPrinter.Location = new System.Drawing.Point(455, 34);
            this.CkShowPrinter.Name = "CkShowPrinter";
            this.CkShowPrinter.Properties.Appearance.Font = new System.Drawing.Font("Kanit", 9F);
            this.CkShowPrinter.Properties.Appearance.Options.UseFont = true;
            this.CkShowPrinter.Properties.AutoWidth = true;
            this.CkShowPrinter.Properties.Caption = "แสดงปริ้นเตอร์";
            this.CkShowPrinter.Size = new System.Drawing.Size(97, 23);
            this.CkShowPrinter.TabIndex = 447;
            this.CkShowPrinter.CheckedChanged += new System.EventHandler(this.CkIsPrinter_CheckedChanged);
            // 
            // CboPrinterList
            // 
            this.CboPrinterList.Location = new System.Drawing.Point(558, 33);
            this.CboPrinterList.Name = "CboPrinterList";
            this.CboPrinterList.Properties.Appearance.Font = new System.Drawing.Font("Kanit", 9F);
            this.CboPrinterList.Properties.Appearance.Options.UseFont = true;
            this.CboPrinterList.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.CboPrinterList.Size = new System.Drawing.Size(232, 26);
            this.CboPrinterList.TabIndex = 445;
            // 
            // BtnCloseBill
            // 
            this.BtnCloseBill.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.BtnCloseBill.Appearance.BackColor = System.Drawing.Color.Green;
            this.BtnCloseBill.Appearance.Font = new System.Drawing.Font("Kanit", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCloseBill.Appearance.Options.UseBackColor = true;
            this.BtnCloseBill.Appearance.Options.UseFont = true;
            this.BtnCloseBill.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnCloseBill.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.BtnCloseBill.Location = new System.Drawing.Point(926, 7);
            this.BtnCloseBill.LookAndFeel.SkinName = "VS2010";
            this.BtnCloseBill.LookAndFeel.UseDefaultLookAndFeel = false;
            this.BtnCloseBill.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.BtnCloseBill.Name = "BtnCloseBill";
            this.BtnCloseBill.Size = new System.Drawing.Size(133, 50);
            this.BtnCloseBill.TabIndex = 444;
            this.BtnCloseBill.Text = "ปิดการซื้อ";
            this.BtnCloseBill.Click += new System.EventHandler(this.BtnCloseBill_Click);
            // 
            // BtnBack
            // 
            this.BtnBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnBack.Appearance.Font = new System.Drawing.Font("Kanit", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnBack.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnBack.Appearance.Options.UseFont = true;
            this.BtnBack.Appearance.Options.UseForeColor = true;
            this.BtnBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnBack.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.BtnBack.Location = new System.Drawing.Point(2, 13);
            this.BtnBack.LookAndFeel.SkinName = "VS2010";
            this.BtnBack.LookAndFeel.UseDefaultLookAndFeel = false;
            this.BtnBack.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.BtnBack.Name = "BtnBack";
            this.BtnBack.Size = new System.Drawing.Size(110, 50);
            this.BtnBack.TabIndex = 443;
            this.BtnBack.Text = "กลับ";
            this.BtnBack.Click += new System.EventHandler(this.BtnBack_Click);
            // 
            // DocShowBill
            // 
            this.DocShowBill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DocShowBill.IsMetric = true;
            this.DocShowBill.Location = new System.Drawing.Point(0, 0);
            this.DocShowBill.LookAndFeel.SkinName = "Office 2019 Colorful";
            this.DocShowBill.LookAndFeel.UseDefaultLookAndFeel = false;
            this.DocShowBill.Name = "DocShowBill";
            this.DocShowBill.Size = new System.Drawing.Size(1070, 673);
            this.DocShowBill.TabIndex = 16;
            // 
            // FrmPayment
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1070, 740);
            this.ControlBox = false;
            this.Controls.Add(this.DocShowBill);
            this.Controls.Add(this.XtraBack);
            this.IconOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("FrmPayment.IconOptions.LargeImage")));
            this.LookAndFeel.SkinName = "The Bezier";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1070, 770);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1070, 770);
            this.Name = "FrmPayment";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ตัวอย่างใบเสร็จ";
            this.Load += new System.EventHandler(this.FrmPayment_Load);
            this.XtraBack.ResumeLayout(false);
            this.XtraBack.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CkIsPrinter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CkShowPrinter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CboPrinterList.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.XtraScrollableControl XtraBack;
        public DevExpress.XtraEditors.SimpleButton BtnCloseBill;
        public DevExpress.XtraEditors.SimpleButton BtnBack;
        public DevExpress.XtraPrinting.Preview.DocumentViewer DocShowBill;
        private DevExpress.XtraEditors.ComboBoxEdit CboPrinterList;
        private DevExpress.XtraEditors.CheckEdit CkShowPrinter;
        private DevExpress.XtraEditors.CheckEdit CkIsPrinter;
    }
}