namespace RubberSoft
{
    partial class FrmMain
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
            DevExpress.XtraEditors.TileItemElement tileItemElement1 = new DevExpress.XtraEditors.TileItemElement();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            DevExpress.XtraEditors.TileItemElement tileItemElement2 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement3 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement4 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement5 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement6 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement7 = new DevExpress.XtraEditors.TileItemElement();
            this.TileMain = new DevExpress.XtraEditors.TileControl();
            this.TileGroupBuy = new DevExpress.XtraEditors.TileGroup();
            this.TBuyProducts = new DevExpress.XtraEditors.TileItem();
            this.TileGroupSale = new DevExpress.XtraEditors.TileGroup();
            this.TSaleProducts = new DevExpress.XtraEditors.TileItem();
            this.TileGroupCustomer = new DevExpress.XtraEditors.TileGroup();
            this.TProducts = new DevExpress.XtraEditors.TileItem();
            this.TileGroupReport = new DevExpress.XtraEditors.TileGroup();
            this.TCustomers = new DevExpress.XtraEditors.TileItem();
            this.TileGroupBuyData = new DevExpress.XtraEditors.TileGroup();
            this.TileBuyData = new DevExpress.XtraEditors.TileItem();
            this.TileGroupSaleData = new DevExpress.XtraEditors.TileGroup();
            this.TileSaleData = new DevExpress.XtraEditors.TileItem();
            this.TileGroupAutorize = new DevExpress.XtraEditors.TileGroup();
            this.TileAutorize = new DevExpress.XtraEditors.TileItem();
            this.XtraDetails = new DevExpress.XtraEditors.XtraScrollableControl();
            this.lblUserName = new DevExpress.XtraEditors.LabelControl();
            this.BtnSetUsers = new DevExpress.XtraEditors.SimpleButton();
            this.BtnLogOut = new DevExpress.XtraEditors.SimpleButton();
            this.TimerMain = new System.Windows.Forms.Timer(this.components);
            this.xtraBottom = new DevExpress.XtraEditors.XtraScrollableControl();
            this.lblDay = new DevExpress.XtraEditors.LabelControl();
            this.xtraScrollableControl3 = new DevExpress.XtraEditors.XtraScrollableControl();
            this.BtnTools = new DevExpress.XtraEditors.SimpleButton();
            this.xtraScrollableControl1 = new DevExpress.XtraEditors.XtraScrollableControl();
            this.xtraScrollableControl2 = new DevExpress.XtraEditors.XtraScrollableControl();
            this.behaviorManager1 = new DevExpress.Utils.Behaviors.BehaviorManager(this.components);
            this.XtraDetails.SuspendLayout();
            this.xtraBottom.SuspendLayout();
            this.xtraScrollableControl1.SuspendLayout();
            this.xtraScrollableControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // TileMain
            // 
            this.TileMain.AllowDrag = false;
            this.TileMain.AllowDragTilesBetweenGroups = false;
            this.TileMain.AllowItemHover = true;
            this.TileMain.AppearanceGroupHighlighting.HoveredMaskColor = System.Drawing.Color.Empty;
            this.TileMain.AppearanceGroupHighlighting.MaskColor = System.Drawing.Color.Empty;
            this.TileMain.AppearanceGroupText.Font = new System.Drawing.Font("Kanit", 12F);
            this.TileMain.AppearanceGroupText.Options.UseFont = true;
            this.TileMain.AppearanceGroupText.Options.UseTextOptions = true;
            this.TileMain.AppearanceGroupText.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.TileMain.AppearanceItem.Normal.BackColor = System.Drawing.Color.Transparent;
            this.TileMain.AppearanceItem.Normal.Font = new System.Drawing.Font("Kanit", 12F, System.Drawing.FontStyle.Bold);
            this.TileMain.AppearanceItem.Normal.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.TileMain.AppearanceItem.Normal.Options.UseBackColor = true;
            this.TileMain.AppearanceItem.Normal.Options.UseFont = true;
            this.TileMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.TileMain.ColumnCount = 5;
            this.TileMain.Cursor = System.Windows.Forms.Cursors.Hand;
            this.TileMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.TileMain.Groups.Add(this.TileGroupBuy);
            this.TileMain.Groups.Add(this.TileGroupSale);
            this.TileMain.Groups.Add(this.TileGroupCustomer);
            this.TileMain.Groups.Add(this.TileGroupReport);
            this.TileMain.Groups.Add(this.TileGroupBuyData);
            this.TileMain.Groups.Add(this.TileGroupSaleData);
            this.TileMain.Groups.Add(this.TileGroupAutorize);
            this.TileMain.HorizontalContentAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.TileMain.ImeMode = System.Windows.Forms.ImeMode.On;
            this.TileMain.IndentBetweenGroups = 50;
            this.TileMain.ItemBackgroundImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.TopLeft;
            this.TileMain.ItemImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.TopLeft;
            this.TileMain.ItemPadding = new System.Windows.Forms.Padding(0);
            this.TileMain.ItemSize = 130;
            this.TileMain.Location = new System.Drawing.Point(0, 60);
            this.TileMain.LookAndFeel.SkinName = "Office 2019 Colorful";
            this.TileMain.LookAndFeel.UseDefaultLookAndFeel = false;
            this.TileMain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TileMain.MaxId = 7;
            this.TileMain.Name = "TileMain";
            this.TileMain.Padding = new System.Windows.Forms.Padding(57, 28, 24, 28);
            this.TileMain.SelectionColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.TileMain.ShowGroupText = true;
            this.TileMain.Size = new System.Drawing.Size(1122, 696);
            this.TileMain.TabIndex = 0;
            this.TileMain.VerticalContentAlignment = DevExpress.Utils.VertAlignment.Top;
            this.TileMain.Click += new System.EventHandler(this.TileMain_Click);
            // 
            // TileGroupBuy
            // 
            this.TileGroupBuy.Items.Add(this.TBuyProducts);
            this.TileGroupBuy.Name = "TileGroupBuy";
            this.TileGroupBuy.Text = "การซื้อ";
            // 
            // TBuyProducts
            // 
            this.TBuyProducts.AppearanceItem.Normal.BackColor = System.Drawing.Color.Transparent;
            this.TBuyProducts.AppearanceItem.Normal.Font = new System.Drawing.Font("Kanit", 12F);
            this.TBuyProducts.AppearanceItem.Normal.Options.UseBackColor = true;
            this.TBuyProducts.AppearanceItem.Normal.Options.UseFont = true;
            this.TBuyProducts.BorderVisibility = DevExpress.XtraEditors.TileItemBorderVisibility.Never;
            tileItemElement1.Appearance.Hovered.Font = new System.Drawing.Font("Tahoma", 13.75F);
            tileItemElement1.Appearance.Hovered.Options.UseFont = true;
            tileItemElement1.Appearance.Normal.Font = new System.Drawing.Font("Tahoma", 13.75F);
            tileItemElement1.Appearance.Normal.Options.UseFont = true;
            tileItemElement1.Appearance.Selected.Font = new System.Drawing.Font("Tahoma", 13.75F);
            tileItemElement1.Appearance.Selected.Options.UseFont = true;
            tileItemElement1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image")));
            tileItemElement1.ImageOptions.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileItemElement1.ImageOptions.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.Stretch;
            tileItemElement1.TextLocation = new System.Drawing.Point(8, 5);
            this.TBuyProducts.Elements.Add(tileItemElement1);
            this.TBuyProducts.Id = 0;
            this.TBuyProducts.ItemSize = DevExpress.XtraEditors.TileItemSize.Medium;
            this.TBuyProducts.Name = "TBuyProducts";
            this.TBuyProducts.ItemClick += new DevExpress.XtraEditors.TileItemClickEventHandler(this.TBuyProducts_ItemClick);
            // 
            // TileGroupSale
            // 
            this.TileGroupSale.Items.Add(this.TSaleProducts);
            this.TileGroupSale.Name = "TileGroupSale";
            this.TileGroupSale.Text = "การขาย";
            // 
            // TSaleProducts
            // 
            this.TSaleProducts.AppearanceItem.Normal.BackColor = System.Drawing.Color.Transparent;
            this.TSaleProducts.AppearanceItem.Normal.Font = new System.Drawing.Font("Kanit", 12F);
            this.TSaleProducts.AppearanceItem.Normal.Options.UseBackColor = true;
            this.TSaleProducts.AppearanceItem.Normal.Options.UseFont = true;
            this.TSaleProducts.BorderVisibility = DevExpress.XtraEditors.TileItemBorderVisibility.Never;
            tileItemElement2.Appearance.Hovered.Font = new System.Drawing.Font("Tahoma", 13.75F);
            tileItemElement2.Appearance.Hovered.Options.UseFont = true;
            tileItemElement2.Appearance.Normal.Font = new System.Drawing.Font("Tahoma", 13.75F);
            tileItemElement2.Appearance.Normal.Options.UseFont = true;
            tileItemElement2.Appearance.Selected.Font = new System.Drawing.Font("Tahoma", 13.75F);
            tileItemElement2.Appearance.Selected.Options.UseFont = true;
            tileItemElement2.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image1")));
            tileItemElement2.ImageOptions.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileItemElement2.ImageOptions.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.Stretch;
            tileItemElement2.TextLocation = new System.Drawing.Point(8, 5);
            this.TSaleProducts.Elements.Add(tileItemElement2);
            this.TSaleProducts.Id = 2;
            this.TSaleProducts.ItemSize = DevExpress.XtraEditors.TileItemSize.Medium;
            this.TSaleProducts.Name = "TSaleProducts";
            this.TSaleProducts.ItemClick += new DevExpress.XtraEditors.TileItemClickEventHandler(this.TSaleProducts_ItemClick);
            // 
            // TileGroupCustomer
            // 
            this.TileGroupCustomer.Items.Add(this.TProducts);
            this.TileGroupCustomer.Name = "TileGroupCustomer";
            this.TileGroupCustomer.Text = "สมาชิก";
            // 
            // TProducts
            // 
            this.TProducts.AppearanceItem.Normal.BackColor = System.Drawing.Color.Transparent;
            this.TProducts.AppearanceItem.Normal.Font = new System.Drawing.Font("Kanit", 12F);
            this.TProducts.AppearanceItem.Normal.Options.UseBackColor = true;
            this.TProducts.AppearanceItem.Normal.Options.UseFont = true;
            this.TProducts.BorderVisibility = DevExpress.XtraEditors.TileItemBorderVisibility.Never;
            tileItemElement3.Appearance.Hovered.Font = new System.Drawing.Font("Tahoma", 13.75F);
            tileItemElement3.Appearance.Hovered.Options.UseFont = true;
            tileItemElement3.Appearance.Normal.Font = new System.Drawing.Font("Tahoma", 13.75F);
            tileItemElement3.Appearance.Normal.Options.UseFont = true;
            tileItemElement3.Appearance.Selected.Font = new System.Drawing.Font("Tahoma", 13.75F);
            tileItemElement3.Appearance.Selected.Options.UseFont = true;
            tileItemElement3.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image2")));
            tileItemElement3.ImageOptions.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileItemElement3.ImageOptions.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.Stretch;
            tileItemElement3.TextLocation = new System.Drawing.Point(8, 5);
            this.TProducts.Elements.Add(tileItemElement3);
            this.TProducts.Id = 3;
            this.TProducts.ItemSize = DevExpress.XtraEditors.TileItemSize.Medium;
            this.TProducts.Name = "TProducts";
            this.TProducts.ItemClick += new DevExpress.XtraEditors.TileItemClickEventHandler(this.TProducts_ItemClick);
            // 
            // TileGroupReport
            // 
            this.TileGroupReport.Items.Add(this.TCustomers);
            this.TileGroupReport.Name = "TileGroupReport";
            this.TileGroupReport.Text = "รายงาน";
            // 
            // TCustomers
            // 
            this.TCustomers.AppearanceItem.Normal.BackColor = System.Drawing.Color.Transparent;
            this.TCustomers.AppearanceItem.Normal.Font = new System.Drawing.Font("Kanit", 12F);
            this.TCustomers.AppearanceItem.Normal.Options.UseBackColor = true;
            this.TCustomers.AppearanceItem.Normal.Options.UseFont = true;
            this.TCustomers.BorderVisibility = DevExpress.XtraEditors.TileItemBorderVisibility.Never;
            tileItemElement4.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image3")));
            tileItemElement4.ImageOptions.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            tileItemElement4.ImageOptions.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.Stretch;
            tileItemElement4.Text = "";
            this.TCustomers.Elements.Add(tileItemElement4);
            this.TCustomers.Id = 1;
            this.TCustomers.ItemSize = DevExpress.XtraEditors.TileItemSize.Medium;
            this.TCustomers.Name = "TCustomers";
            this.TCustomers.ItemClick += new DevExpress.XtraEditors.TileItemClickEventHandler(this.TCustomers_ItemClick);
            // 
            // TileGroupBuyData
            // 
            this.TileGroupBuyData.Items.Add(this.TileBuyData);
            this.TileGroupBuyData.Name = "TileGroupBuyData";
            this.TileGroupBuyData.Text = "ค้นหาการซื้อ";
            // 
            // TileBuyData
            // 
            this.TileBuyData.AppearanceItem.Normal.BorderColor = System.Drawing.Color.Transparent;
            this.TileBuyData.AppearanceItem.Normal.Options.UseBorderColor = true;
            tileItemElement5.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image4")));
            tileItemElement5.ImageOptions.ImageBorderColor = System.Drawing.Color.White;
            tileItemElement5.ImageOptions.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.Stretch;
            tileItemElement5.Text = "";
            this.TileBuyData.Elements.Add(tileItemElement5);
            this.TileBuyData.Id = 4;
            this.TileBuyData.ItemSize = DevExpress.XtraEditors.TileItemSize.Medium;
            this.TileBuyData.Name = "TileBuyData";
            this.TileBuyData.ItemClick += new DevExpress.XtraEditors.TileItemClickEventHandler(this.TileBuyData_ItemClick);
            // 
            // TileGroupSaleData
            // 
            this.TileGroupSaleData.Items.Add(this.TileSaleData);
            this.TileGroupSaleData.Name = "TileGroupSaleData";
            this.TileGroupSaleData.Text = "ค้นหาการขาย";
            // 
            // TileSaleData
            // 
            this.TileSaleData.AppearanceItem.Normal.BorderColor = System.Drawing.Color.Transparent;
            this.TileSaleData.AppearanceItem.Normal.Options.UseBorderColor = true;
            tileItemElement6.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image5")));
            tileItemElement6.ImageOptions.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.Stretch;
            tileItemElement6.Text = "tileItem1";
            this.TileSaleData.Elements.Add(tileItemElement6);
            this.TileSaleData.Id = 5;
            this.TileSaleData.ItemSize = DevExpress.XtraEditors.TileItemSize.Medium;
            this.TileSaleData.Name = "TileSaleData";
            this.TileSaleData.ItemClick += new DevExpress.XtraEditors.TileItemClickEventHandler(this.TileSaleData_ItemClick);
            // 
            // TileGroupAutorize
            // 
            this.TileGroupAutorize.Items.Add(this.TileAutorize);
            this.TileGroupAutorize.Name = "TileGroupAutorize";
            this.TileGroupAutorize.Text = "ตั้งค่าสิทธิ์";
            // 
            // TileAutorize
            // 
            this.TileAutorize.BorderVisibility = DevExpress.XtraEditors.TileItemBorderVisibility.Never;
            tileItemElement7.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image6")));
            tileItemElement7.ImageOptions.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.Stretch;
            tileItemElement7.Text = "tileItem1";
            this.TileAutorize.Elements.Add(tileItemElement7);
            this.TileAutorize.Id = 6;
            this.TileAutorize.ItemSize = DevExpress.XtraEditors.TileItemSize.Medium;
            this.TileAutorize.Name = "TileAutorize";
            this.TileAutorize.ItemClick += new DevExpress.XtraEditors.TileItemClickEventHandler(this.TileAutorize_ItemClick);
            // 
            // XtraDetails
            // 
            this.XtraDetails.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(141)))), ((int)(((byte)(59)))));
            this.XtraDetails.Appearance.Options.UseBackColor = true;
            this.XtraDetails.Controls.Add(this.lblUserName);
            this.XtraDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.XtraDetails.Location = new System.Drawing.Point(0, 0);
            this.XtraDetails.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.XtraDetails.Name = "XtraDetails";
            this.XtraDetails.Size = new System.Drawing.Size(850, 60);
            this.XtraDetails.TabIndex = 74;
            // 
            // lblUserName
            // 
            this.lblUserName.Appearance.Font = new System.Drawing.Font("Kanit", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserName.Appearance.ForeColor = System.Drawing.Color.White;
            this.lblUserName.Appearance.Options.UseFont = true;
            this.lblUserName.Appearance.Options.UseForeColor = true;
            this.lblUserName.Location = new System.Drawing.Point(34, 23);
            this.lblUserName.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(84, 25);
            this.lblUserName.TabIndex = 3;
            this.lblUserName.Text = "............................";
            // 
            // BtnSetUsers
            // 
            this.BtnSetUsers.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(141)))), ((int)(((byte)(59)))));
            this.BtnSetUsers.Appearance.Font = new System.Drawing.Font("Kanit", 12F);
            this.BtnSetUsers.Appearance.Options.UseBackColor = true;
            this.BtnSetUsers.Appearance.Options.UseFont = true;
            this.BtnSetUsers.AppearanceHovered.Font = new System.Drawing.Font("Kanit", 10F);
            this.BtnSetUsers.AppearanceHovered.Options.UseFont = true;
            this.BtnSetUsers.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnSetUsers.Dock = System.Windows.Forms.DockStyle.Right;
            this.BtnSetUsers.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BtnSetUsers.ImageOptions.Image")));
            this.BtnSetUsers.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.BtnSetUsers.Location = new System.Drawing.Point(850, 0);
            this.BtnSetUsers.Margin = new System.Windows.Forms.Padding(0);
            this.BtnSetUsers.Name = "BtnSetUsers";
            this.BtnSetUsers.Size = new System.Drawing.Size(128, 60);
            this.BtnSetUsers.TabIndex = 479;
            this.BtnSetUsers.Text = "ผู้ใช้งาน";
            this.BtnSetUsers.Click += new System.EventHandler(this.BtnSetUsers_Click);
            // 
            // BtnLogOut
            // 
            this.BtnLogOut.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(141)))), ((int)(((byte)(59)))));
            this.BtnLogOut.Appearance.Font = new System.Drawing.Font("Kanit", 12F);
            this.BtnLogOut.Appearance.ForeColor = System.Drawing.Color.Red;
            this.BtnLogOut.Appearance.Options.UseBackColor = true;
            this.BtnLogOut.Appearance.Options.UseFont = true;
            this.BtnLogOut.Appearance.Options.UseForeColor = true;
            this.BtnLogOut.AppearanceHovered.Font = new System.Drawing.Font("Kanit", 10F);
            this.BtnLogOut.AppearanceHovered.Options.UseFont = true;
            this.BtnLogOut.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnLogOut.Dock = System.Windows.Forms.DockStyle.Right;
            this.BtnLogOut.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BtnLogOut.ImageOptions.Image")));
            this.BtnLogOut.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.BtnLogOut.Location = new System.Drawing.Point(978, 0);
            this.BtnLogOut.Margin = new System.Windows.Forms.Padding(0);
            this.BtnLogOut.Name = "BtnLogOut";
            this.BtnLogOut.Size = new System.Drawing.Size(144, 60);
            this.BtnLogOut.TabIndex = 478;
            this.BtnLogOut.Text = "ออกจากระบบ";
            this.BtnLogOut.Click += new System.EventHandler(this.BtnLogOut_Click);
            // 
            // TimerMain
            // 
            this.TimerMain.Tick += new System.EventHandler(this.TimerMain_Tick);
            // 
            // xtraBottom
            // 
            this.xtraBottom.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(141)))), ((int)(((byte)(59)))));
            this.xtraBottom.Appearance.Options.UseBackColor = true;
            this.xtraBottom.Controls.Add(this.lblDay);
            this.xtraBottom.Controls.Add(this.xtraScrollableControl3);
            this.xtraBottom.Controls.Add(this.BtnTools);
            this.xtraBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraBottom.Location = new System.Drawing.Point(0, 0);
            this.xtraBottom.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.xtraBottom.Name = "xtraBottom";
            this.xtraBottom.Size = new System.Drawing.Size(1122, 60);
            this.xtraBottom.TabIndex = 76;
            // 
            // lblDay
            // 
            this.lblDay.Appearance.Font = new System.Drawing.Font("Kanit", 12F);
            this.lblDay.Appearance.ForeColor = System.Drawing.Color.White;
            this.lblDay.Appearance.Options.UseFont = true;
            this.lblDay.Appearance.Options.UseForeColor = true;
            this.lblDay.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblDay.Location = new System.Drawing.Point(13, 35);
            this.lblDay.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.lblDay.Name = "lblDay";
            this.lblDay.Size = new System.Drawing.Size(51, 25);
            this.lblDay.TabIndex = 4;
            this.lblDay.Text = "lblDate";
            // 
            // xtraScrollableControl3
            // 
            this.xtraScrollableControl3.Dock = System.Windows.Forms.DockStyle.Left;
            this.xtraScrollableControl3.Location = new System.Drawing.Point(0, 0);
            this.xtraScrollableControl3.Name = "xtraScrollableControl3";
            this.xtraScrollableControl3.Size = new System.Drawing.Size(13, 60);
            this.xtraScrollableControl3.TabIndex = 481;
            // 
            // BtnTools
            // 
            this.BtnTools.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(141)))), ((int)(((byte)(59)))));
            this.BtnTools.Appearance.Font = new System.Drawing.Font("Kanit", 12F);
            this.BtnTools.Appearance.Options.UseBackColor = true;
            this.BtnTools.Appearance.Options.UseFont = true;
            this.BtnTools.AppearanceHovered.Font = new System.Drawing.Font("Kanit", 10F);
            this.BtnTools.AppearanceHovered.Options.UseFont = true;
            this.BtnTools.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnTools.Dock = System.Windows.Forms.DockStyle.Right;
            this.BtnTools.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BtnTools.ImageOptions.Image")));
            this.BtnTools.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.BtnTools.Location = new System.Drawing.Point(1057, 0);
            this.BtnTools.Margin = new System.Windows.Forms.Padding(0);
            this.BtnTools.Name = "BtnTools";
            this.BtnTools.Size = new System.Drawing.Size(65, 60);
            this.BtnTools.TabIndex = 480;
            this.BtnTools.Click += new System.EventHandler(this.BtnTools_Click);
            // 
            // xtraScrollableControl1
            // 
            this.xtraScrollableControl1.Controls.Add(this.XtraDetails);
            this.xtraScrollableControl1.Controls.Add(this.BtnSetUsers);
            this.xtraScrollableControl1.Controls.Add(this.BtnLogOut);
            this.xtraScrollableControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.xtraScrollableControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraScrollableControl1.Name = "xtraScrollableControl1";
            this.xtraScrollableControl1.Size = new System.Drawing.Size(1122, 60);
            this.xtraScrollableControl1.TabIndex = 480;
            // 
            // xtraScrollableControl2
            // 
            this.xtraScrollableControl2.Controls.Add(this.xtraBottom);
            this.xtraScrollableControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.xtraScrollableControl2.Location = new System.Drawing.Point(0, 780);
            this.xtraScrollableControl2.Name = "xtraScrollableControl2";
            this.xtraScrollableControl2.Size = new System.Drawing.Size(1122, 60);
            this.xtraScrollableControl2.TabIndex = 481;
            // 
            // FrmMain
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1122, 840);
            this.ControlBox = false;
            this.Controls.Add(this.xtraScrollableControl2);
            this.Controls.Add(this.TileMain);
            this.Controls.Add(this.xtraScrollableControl1);
            this.Font = new System.Drawing.Font("Kanit", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.LookAndFeel.SkinName = "The Bezier";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(1024, 768);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "โปรแกรมรับซื้อยางพารา";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.XtraDetails.ResumeLayout(false);
            this.XtraDetails.PerformLayout();
            this.xtraBottom.ResumeLayout(false);
            this.xtraBottom.PerformLayout();
            this.xtraScrollableControl1.ResumeLayout(false);
            this.xtraScrollableControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.TileControl TileMain;
        private DevExpress.XtraEditors.TileItem TBuyProducts;
        private DevExpress.XtraEditors.TileItem TCustomers;
        private DevExpress.XtraEditors.TileItem TSaleProducts;
        private DevExpress.XtraEditors.TileItem TProducts;
        internal DevExpress.XtraEditors.XtraScrollableControl XtraDetails;
        private DevExpress.XtraEditors.SimpleButton BtnLogOut;
        internal DevExpress.XtraEditors.LabelControl lblUserName;
        private System.Windows.Forms.Timer TimerMain;
        private DevExpress.XtraEditors.XtraScrollableControl xtraBottom;
        internal DevExpress.XtraEditors.LabelControl lblDay;
        private DevExpress.XtraEditors.SimpleButton BtnSetUsers;
        private DevExpress.XtraEditors.XtraScrollableControl xtraScrollableControl1;
        private DevExpress.XtraEditors.XtraScrollableControl xtraScrollableControl2;
        private DevExpress.Utils.Behaviors.BehaviorManager behaviorManager1;
        private DevExpress.XtraEditors.TileGroup TileGroupBuy;
        private DevExpress.XtraEditors.TileGroup TileGroupSale;
        private DevExpress.XtraEditors.TileGroup TileGroupCustomer;
        private DevExpress.XtraEditors.TileGroup TileGroupReport;
        private DevExpress.XtraEditors.SimpleButton BtnTools;
        private DevExpress.XtraEditors.TileGroup TileGroupBuyData;
        private DevExpress.XtraEditors.TileItem TileBuyData;
        private DevExpress.XtraEditors.TileGroup TileGroupSaleData;
        private DevExpress.XtraEditors.TileItem TileSaleData;
        private DevExpress.XtraEditors.TileGroup TileGroupAutorize;
        private DevExpress.XtraEditors.TileItem TileAutorize;
        private DevExpress.XtraEditors.XtraScrollableControl xtraScrollableControl3;
    }
}

