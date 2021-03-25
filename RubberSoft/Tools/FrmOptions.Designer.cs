namespace RubberSoft.Tools
{
    partial class FrmOptions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmOptions));
            this.XtraBack = new DevExpress.XtraEditors.XtraScrollableControl();
            this.BtnNew = new DevExpress.XtraEditors.SimpleButton();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.CboPrinterList = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblปริ้นเตอร์ = new DevExpress.XtraEditors.LabelControl();
            this.XtraBack.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CboPrinterList.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // XtraBack
            // 
            this.XtraBack.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
            this.XtraBack.Appearance.Options.UseBackColor = true;
            this.XtraBack.Controls.Add(this.BtnNew);
            this.XtraBack.Controls.Add(this.BtnSave);
            this.XtraBack.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.XtraBack.Location = new System.Drawing.Point(0, 396);
            this.XtraBack.Margin = new System.Windows.Forms.Padding(3, 9, 3, 9);
            this.XtraBack.Name = "XtraBack";
            this.XtraBack.Size = new System.Drawing.Size(800, 74);
            this.XtraBack.TabIndex = 19;
            // 
            // BtnNew
            // 
            this.BtnNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnNew.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnNew.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.BtnNew.Location = new System.Drawing.Point(668, 13);
            this.BtnNew.LookAndFeel.SkinName = "VS2010";
            this.BtnNew.LookAndFeel.UseDefaultLookAndFeel = false;
            this.BtnNew.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.BtnNew.Name = "BtnNew";
            this.BtnNew.Size = new System.Drawing.Size(120, 46);
            this.BtnNew.TabIndex = 581;
            this.BtnNew.Text = "กลับ";
            this.BtnNew.Click += new System.EventHandler(this.BtnNew_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.BtnSave.Location = new System.Drawing.Point(12, 13);
            this.BtnSave.LookAndFeel.SkinName = "VS2010";
            this.BtnSave.LookAndFeel.UseDefaultLookAndFeel = false;
            this.BtnSave.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(120, 46);
            this.BtnSave.TabIndex = 3;
            this.BtnSave.Text = "บันทึก";
            // 
            // CboPrinterList
            // 
            this.CboPrinterList.Location = new System.Drawing.Point(135, 32);
            this.CboPrinterList.Name = "CboPrinterList";
            this.CboPrinterList.Properties.Appearance.Font = new System.Drawing.Font("Kanit", 12F);
            this.CboPrinterList.Properties.Appearance.Options.UseFont = true;
            this.CboPrinterList.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.CboPrinterList.Size = new System.Drawing.Size(400, 32);
            this.CboPrinterList.TabIndex = 20;
            // 
            // lblปริ้นเตอร์
            // 
            this.lblปริ้นเตอร์.Appearance.Font = new System.Drawing.Font("Kanit", 12F);
            this.lblปริ้นเตอร์.Appearance.Options.UseFont = true;
            this.lblปริ้นเตอร์.Location = new System.Drawing.Point(51, 35);
            this.lblปริ้นเตอร์.Name = "lblปริ้นเตอร์";
            this.lblปริ้นเตอร์.Size = new System.Drawing.Size(60, 25);
            this.lblปริ้นเตอร์.TabIndex = 21;
            this.lblปริ้นเตอร์.Text = "ปริ้นเตอร์";
            // 
            // FrmOptions
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 470);
            this.ControlBox = false;
            this.Controls.Add(this.lblปริ้นเตอร์);
            this.Controls.Add(this.CboPrinterList);
            this.Controls.Add(this.XtraBack);
            this.IconOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("FrmOptions.IconOptions.LargeImage")));
            this.LookAndFeel.SkinName = "The Bezier";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmOptions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ตั้งค่า";
            this.Load += new System.EventHandler(this.FrmOptions_Load);
            this.XtraBack.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CboPrinterList.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.XtraScrollableControl XtraBack;
        internal DevExpress.XtraEditors.SimpleButton BtnNew;
        internal DevExpress.XtraEditors.SimpleButton BtnSave;
        private DevExpress.XtraEditors.ComboBoxEdit CboPrinterList;
        private DevExpress.XtraEditors.LabelControl lblปริ้นเตอร์;
    }
}