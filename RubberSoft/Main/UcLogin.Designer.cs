namespace RubberSoft.Main
{
    partial class UcLogin
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UcLogin));
            this.lblTime = new DevExpress.XtraEditors.LabelControl();
            this.lblDay = new DevExpress.XtraEditors.LabelControl();
            this.TxtPassword = new DevExpress.XtraEditors.TextEdit();
            this.TimeLogin = new System.Windows.Forms.Timer(this.components);
            this.lblชื่อผู้ใช้งาน = new DevExpress.XtraEditors.LabelControl();
            this.TxtUserName = new DevExpress.XtraEditors.TextEdit();
            this.lblรหัสผ่าน = new DevExpress.XtraEditors.LabelControl();
            this.BtnExit = new DevExpress.XtraEditors.SimpleButton();
            this.BtnLogin = new DevExpress.XtraEditors.SimpleButton();
            this.Grเข้าสู่ระบบ = new System.Windows.Forms.GroupBox();
            this.lblยินดีต้อนรับเข้าสู่ระบบ = new DevExpress.XtraEditors.LabelControl();
            this.lblFullDate = new DevExpress.XtraEditors.LabelControl();
            this.XtraBottom = new DevExpress.XtraEditors.XtraScrollableControl();
            this.XtraTop = new DevExpress.XtraEditors.XtraScrollableControl();
            this.PicProduct = new DevExpress.XtraEditors.PictureEdit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtUserName.Properties)).BeginInit();
            this.Grเข้าสู่ระบบ.SuspendLayout();
            this.XtraBottom.SuspendLayout();
            this.XtraTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicProduct.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTime
            // 
            this.lblTime.Appearance.Font = new System.Drawing.Font("Trebuchet MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.lblTime.Appearance.Options.UseFont = true;
            this.lblTime.Appearance.Options.UseForeColor = true;
            this.lblTime.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.TopCenter;
            this.lblTime.Location = new System.Drawing.Point(110, 493);
            this.lblTime.Margin = new System.Windows.Forms.Padding(2, 6, 2, 6);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(39, 18);
            this.lblTime.TabIndex = 561;
            this.lblTime.Text = "lblTime";
            this.lblTime.Visible = false;
            // 
            // lblDay
            // 
            this.lblDay.Appearance.Font = new System.Drawing.Font("Trebuchet MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDay.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.lblDay.Appearance.Options.UseFont = true;
            this.lblDay.Appearance.Options.UseForeColor = true;
            this.lblDay.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.TopCenter;
            this.lblDay.Location = new System.Drawing.Point(35, 493);
            this.lblDay.Margin = new System.Windows.Forms.Padding(2, 6, 2, 6);
            this.lblDay.Name = "lblDay";
            this.lblDay.Size = new System.Drawing.Size(32, 18);
            this.lblDay.TabIndex = 560;
            this.lblDay.Text = "lblDay";
            this.lblDay.Visible = false;
            // 
            // TxtPassword
            // 
            this.TxtPassword.EditValue = "";
            this.TxtPassword.Location = new System.Drawing.Point(120, 80);
            this.TxtPassword.Margin = new System.Windows.Forms.Padding(2, 6, 2, 6);
            this.TxtPassword.Name = "TxtPassword";
            this.TxtPassword.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.TxtPassword.Properties.Appearance.BorderColor = System.Drawing.Color.White;
            this.TxtPassword.Properties.Appearance.Font = new System.Drawing.Font("Kanit", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.TxtPassword.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.TxtPassword.Properties.Appearance.Options.UseBackColor = true;
            this.TxtPassword.Properties.Appearance.Options.UseBorderColor = true;
            this.TxtPassword.Properties.Appearance.Options.UseFont = true;
            this.TxtPassword.Properties.Appearance.Options.UseForeColor = true;
            this.TxtPassword.Properties.Appearance.Options.UseTextOptions = true;
            this.TxtPassword.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.TxtPassword.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.TxtPassword.Properties.LookAndFeel.SkinName = "Metropolis";
            this.TxtPassword.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.TxtPassword.Properties.UseSystemPasswordChar = true;
            this.TxtPassword.Properties.ValidateOnEnterKey = true;
            this.TxtPassword.Size = new System.Drawing.Size(173, 26);
            this.TxtPassword.TabIndex = 545;
            this.TxtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtPassword_KeyDown);
            // 
            // TimeLogin
            // 
            this.TimeLogin.Tick += new System.EventHandler(this.TimeLogin_Tick);
            // 
            // lblชื่อผู้ใช้งาน
            // 
            this.lblชื่อผู้ใช้งาน.Appearance.Font = new System.Drawing.Font("Kanit", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblชื่อผู้ใช้งาน.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lblชื่อผู้ใช้งาน.Appearance.Options.UseFont = true;
            this.lblชื่อผู้ใช้งาน.Appearance.Options.UseForeColor = true;
            this.lblชื่อผู้ใช้งาน.Location = new System.Drawing.Point(33, 45);
            this.lblชื่อผู้ใช้งาน.Margin = new System.Windows.Forms.Padding(2, 6, 2, 6);
            this.lblชื่อผู้ใช้งาน.Name = "lblชื่อผู้ใช้งาน";
            this.lblชื่อผู้ใช้งาน.Size = new System.Drawing.Size(64, 20);
            this.lblชื่อผู้ใช้งาน.TabIndex = 552;
            this.lblชื่อผู้ใช้งาน.Text = "ชื่อผู้ใช้งาน : ";
            // 
            // TxtUserName
            // 
            this.TxtUserName.EditValue = "";
            this.TxtUserName.Location = new System.Drawing.Point(120, 42);
            this.TxtUserName.Margin = new System.Windows.Forms.Padding(2, 6, 2, 6);
            this.TxtUserName.Name = "TxtUserName";
            this.TxtUserName.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.TxtUserName.Properties.Appearance.BorderColor = System.Drawing.Color.White;
            this.TxtUserName.Properties.Appearance.Font = new System.Drawing.Font("Kanit", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.TxtUserName.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.TxtUserName.Properties.Appearance.Options.UseBackColor = true;
            this.TxtUserName.Properties.Appearance.Options.UseBorderColor = true;
            this.TxtUserName.Properties.Appearance.Options.UseFont = true;
            this.TxtUserName.Properties.Appearance.Options.UseForeColor = true;
            this.TxtUserName.Properties.Appearance.Options.UseTextOptions = true;
            this.TxtUserName.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.TxtUserName.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.TxtUserName.Properties.LookAndFeel.SkinName = "Metropolis";
            this.TxtUserName.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.TxtUserName.Properties.ValidateOnEnterKey = true;
            this.TxtUserName.Size = new System.Drawing.Size(173, 26);
            this.TxtUserName.TabIndex = 544;
            this.TxtUserName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtUserName_KeyDown);
            // 
            // lblรหัสผ่าน
            // 
            this.lblรหัสผ่าน.Appearance.Font = new System.Drawing.Font("Kanit", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblรหัสผ่าน.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lblรหัสผ่าน.Appearance.Options.UseFont = true;
            this.lblรหัสผ่าน.Appearance.Options.UseForeColor = true;
            this.lblรหัสผ่าน.Location = new System.Drawing.Point(33, 83);
            this.lblรหัสผ่าน.Margin = new System.Windows.Forms.Padding(2, 6, 2, 6);
            this.lblรหัสผ่าน.Name = "lblรหัสผ่าน";
            this.lblรหัสผ่าน.Size = new System.Drawing.Size(53, 20);
            this.lblรหัสผ่าน.TabIndex = 553;
            this.lblรหัสผ่าน.Text = "รหัสผ่าน : ";
            // 
            // BtnExit
            // 
            this.BtnExit.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.True;
            this.BtnExit.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(141)))), ((int)(((byte)(59)))));
            this.BtnExit.Appearance.Font = new System.Drawing.Font("Kanit", 12F);
            this.BtnExit.Appearance.ForeColor = System.Drawing.Color.White;
            this.BtnExit.Appearance.Options.UseBackColor = true;
            this.BtnExit.Appearance.Options.UseFont = true;
            this.BtnExit.Appearance.Options.UseForeColor = true;
            this.BtnExit.AppearanceHovered.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Underline);
            this.BtnExit.AppearanceHovered.Options.UseFont = true;
            this.BtnExit.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.BtnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnExit.Dock = System.Windows.Forms.DockStyle.Right;
            this.BtnExit.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.TopCenter;
            this.BtnExit.Location = new System.Drawing.Point(580, 0);
            this.BtnExit.Margin = new System.Windows.Forms.Padding(2, 6, 2, 6);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(155, 45);
            this.BtnExit.TabIndex = 549;
            this.BtnExit.Text = "ออกจากระบบ";
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // BtnLogin
            // 
            this.BtnLogin.Appearance.BorderColor = System.Drawing.Color.Transparent;
            this.BtnLogin.Appearance.Font = new System.Drawing.Font("Kanit", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.BtnLogin.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.BtnLogin.Appearance.Options.UseBorderColor = true;
            this.BtnLogin.Appearance.Options.UseFont = true;
            this.BtnLogin.Appearance.Options.UseForeColor = true;
            this.BtnLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnLogin.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
            this.BtnLogin.Location = new System.Drawing.Point(120, 118);
            this.BtnLogin.LookAndFeel.SkinName = "VS2010";
            this.BtnLogin.LookAndFeel.UseDefaultLookAndFeel = false;
            this.BtnLogin.Margin = new System.Windows.Forms.Padding(2, 6, 2, 6);
            this.BtnLogin.Name = "BtnLogin";
            this.BtnLogin.Size = new System.Drawing.Size(173, 50);
            this.BtnLogin.TabIndex = 546;
            this.BtnLogin.Text = "เข้าระบบ";
            this.BtnLogin.Click += new System.EventHandler(this.BtnLogin_Click);
            // 
            // Grเข้าสู่ระบบ
            // 
            this.Grเข้าสู่ระบบ.Controls.Add(this.lblชื่อผู้ใช้งาน);
            this.Grเข้าสู่ระบบ.Controls.Add(this.BtnLogin);
            this.Grเข้าสู่ระบบ.Controls.Add(this.TxtUserName);
            this.Grเข้าสู่ระบบ.Controls.Add(this.TxtPassword);
            this.Grเข้าสู่ระบบ.Controls.Add(this.lblรหัสผ่าน);
            this.Grเข้าสู่ระบบ.Font = new System.Drawing.Font("Kanit", 12F);
            this.Grเข้าสู่ระบบ.Location = new System.Drawing.Point(305, 86);
            this.Grเข้าสู่ระบบ.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.Grเข้าสู่ระบบ.Name = "Grเข้าสู่ระบบ";
            this.Grเข้าสู่ระบบ.Padding = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.Grเข้าสู่ระบบ.Size = new System.Drawing.Size(349, 196);
            this.Grเข้าสู่ระบบ.TabIndex = 562;
            this.Grเข้าสู่ระบบ.TabStop = false;
            this.Grเข้าสู่ระบบ.Text = "เข้าสู่ระบบ";
            // 
            // lblยินดีต้อนรับเข้าสู่ระบบ
            // 
            this.lblยินดีต้อนรับเข้าสู่ระบบ.Appearance.Font = new System.Drawing.Font("Kanit", 12F);
            this.lblยินดีต้อนรับเข้าสู่ระบบ.Appearance.ForeColor = System.Drawing.Color.White;
            this.lblยินดีต้อนรับเข้าสู่ระบบ.Appearance.Options.UseFont = true;
            this.lblยินดีต้อนรับเข้าสู่ระบบ.Appearance.Options.UseForeColor = true;
            this.lblยินดีต้อนรับเข้าสู่ระบบ.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.TopCenter;
            this.lblยินดีต้อนรับเข้าสู่ระบบ.Location = new System.Drawing.Point(13, 6);
            this.lblยินดีต้อนรับเข้าสู่ระบบ.Margin = new System.Windows.Forms.Padding(2, 6, 2, 6);
            this.lblยินดีต้อนรับเข้าสู่ระบบ.Name = "lblยินดีต้อนรับเข้าสู่ระบบ";
            this.lblยินดีต้อนรับเข้าสู่ระบบ.Size = new System.Drawing.Size(171, 25);
            this.lblยินดีต้อนรับเข้าสู่ระบบ.TabIndex = 556;
            this.lblยินดีต้อนรับเข้าสู่ระบบ.Text = "โปรแกรมซื้อ-ขายยางพารา";
            // 
            // lblFullDate
            // 
            this.lblFullDate.Appearance.Font = new System.Drawing.Font("Kanit", 12F);
            this.lblFullDate.Appearance.ForeColor = System.Drawing.Color.White;
            this.lblFullDate.Appearance.Options.UseFont = true;
            this.lblFullDate.Appearance.Options.UseForeColor = true;
            this.lblFullDate.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.TopCenter;
            this.lblFullDate.Location = new System.Drawing.Point(435, 6);
            this.lblFullDate.Margin = new System.Windows.Forms.Padding(2, 6, 2, 6);
            this.lblFullDate.Name = "lblFullDate";
            this.lblFullDate.Size = new System.Drawing.Size(76, 25);
            this.lblFullDate.TabIndex = 555;
            this.lblFullDate.Text = "lblFullDate";
            this.lblFullDate.Visible = false;
            // 
            // XtraBottom
            // 
            this.XtraBottom.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(141)))), ((int)(((byte)(59)))));
            this.XtraBottom.Appearance.Options.UseBackColor = true;
            this.XtraBottom.Controls.Add(this.lblยินดีต้อนรับเข้าสู่ระบบ);
            this.XtraBottom.Controls.Add(this.lblFullDate);
            this.XtraBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.XtraBottom.Location = new System.Drawing.Point(0, 0);
            this.XtraBottom.Margin = new System.Windows.Forms.Padding(2, 6, 2, 6);
            this.XtraBottom.Name = "XtraBottom";
            this.XtraBottom.Size = new System.Drawing.Size(735, 45);
            this.XtraBottom.TabIndex = 557;
            // 
            // XtraTop
            // 
            this.XtraTop.Controls.Add(this.BtnExit);
            this.XtraTop.Controls.Add(this.XtraBottom);
            this.XtraTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.XtraTop.Location = new System.Drawing.Point(0, 0);
            this.XtraTop.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.XtraTop.Name = "XtraTop";
            this.XtraTop.Size = new System.Drawing.Size(735, 45);
            this.XtraTop.TabIndex = 563;
            // 
            // PicProduct
            // 
            this.PicProduct.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.PicProduct.EditValue = ((object)(resources.GetObject("PicProduct.EditValue")));
            this.PicProduct.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.PicProduct.Location = new System.Drawing.Point(39, 86);
            this.PicProduct.Margin = new System.Windows.Forms.Padding(2, 6, 2, 6);
            this.PicProduct.Name = "PicProduct";
            this.PicProduct.Properties.AllowFocused = false;
            this.PicProduct.Properties.AllowZoomOnMouseWheel = DevExpress.Utils.DefaultBoolean.True;
            this.PicProduct.Properties.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
            this.PicProduct.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.PicProduct.Properties.Appearance.Options.UseBackColor = true;
            this.PicProduct.Properties.Appearance.Options.UseForeColor = true;
            this.PicProduct.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.PicProduct.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.PicProduct.Properties.ShowMenu = false;
            this.PicProduct.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.PicProduct.Size = new System.Drawing.Size(227, 196);
            this.PicProduct.TabIndex = 559;
            // 
            // UcLogin
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.lblDay);
            this.Controls.Add(this.PicProduct);
            this.Controls.Add(this.Grเข้าสู่ระบบ);
            this.Controls.Add(this.XtraTop);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "UcLogin";
            this.Size = new System.Drawing.Size(735, 348);
            this.Load += new System.EventHandler(this.UcLogin_Load);
            this.Leave += new System.EventHandler(this.UcLogin_Leave);
            ((System.ComponentModel.ISupportInitialize)(this.TxtPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtUserName.Properties)).EndInit();
            this.Grเข้าสู่ระบบ.ResumeLayout(false);
            this.Grเข้าสู่ระบบ.PerformLayout();
            this.XtraBottom.ResumeLayout(false);
            this.XtraBottom.PerformLayout();
            this.XtraTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PicProduct.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal DevExpress.XtraEditors.LabelControl lblTime;
        internal DevExpress.XtraEditors.LabelControl lblDay;
        internal DevExpress.XtraEditors.TextEdit TxtPassword;
        private System.Windows.Forms.Timer TimeLogin;
        internal DevExpress.XtraEditors.LabelControl lblชื่อผู้ใช้งาน;
        internal DevExpress.XtraEditors.TextEdit TxtUserName;
        internal DevExpress.XtraEditors.LabelControl lblรหัสผ่าน;
        internal DevExpress.XtraEditors.SimpleButton BtnExit;
        internal DevExpress.XtraEditors.SimpleButton BtnLogin;
        private System.Windows.Forms.GroupBox Grเข้าสู่ระบบ;
        internal DevExpress.XtraEditors.LabelControl lblยินดีต้อนรับเข้าสู่ระบบ;
        internal DevExpress.XtraEditors.LabelControl lblFullDate;
        internal DevExpress.XtraEditors.XtraScrollableControl XtraBottom;
        private DevExpress.XtraEditors.XtraScrollableControl XtraTop;
        private DevExpress.XtraEditors.PictureEdit PicProduct;
    }
}
