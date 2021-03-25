namespace RubberSoft.Main
{
    partial class UcOpenSaveBuy
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions2 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject5 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject6 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject7 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject8 = new DevExpress.Utils.SerializableAppearanceObject();
            this.XtraBand = new DevExpress.XtraEditors.XtraScrollableControl();
            this.XtraBottom = new DevExpress.XtraEditors.XtraScrollableControl();
            this.lblName = new System.Windows.Forms.Label();
            this.BtnBack = new DevExpress.XtraEditors.SimpleButton();
            this.GridSaveBuy = new DevExpress.XtraGrid.GridControl();
            this.GridViewSaveBuy = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSaveBuyId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCustomerId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductUsing = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBuyDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.DtDate = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.colCustomerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBuyNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TxtText = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colWeightAmountTotal = new DevExpress.XtraGrid.Columns.GridColumn();
            this.SpinNumber = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.colNetTotal = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colValueBalance = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSelect = new DevExpress.XtraGrid.Columns.GridColumn();
            this.BtnSelect = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.colDelete = new DevExpress.XtraGrid.Columns.GridColumn();
            this.BtnDelete = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.CboProductUsing = new DevExpress.XtraEditors.ComboBoxEdit();
            this.BtnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.label1 = new System.Windows.Forms.Label();
            this.XtraBand.SuspendLayout();
            this.XtraBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridSaveBuy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewSaveBuy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DtDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DtDate.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SpinNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnSelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnDelete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CboProductUsing.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // XtraBand
            // 
            this.XtraBand.Controls.Add(this.XtraBottom);
            this.XtraBand.Controls.Add(this.BtnBack);
            this.XtraBand.Dock = System.Windows.Forms.DockStyle.Top;
            this.XtraBand.Location = new System.Drawing.Point(0, 0);
            this.XtraBand.Margin = new System.Windows.Forms.Padding(3, 9, 3, 9);
            this.XtraBand.Name = "XtraBand";
            this.XtraBand.Size = new System.Drawing.Size(933, 40);
            this.XtraBand.TabIndex = 16;
            // 
            // XtraBottom
            // 
            this.XtraBottom.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(141)))), ((int)(((byte)(59)))));
            this.XtraBottom.Appearance.Options.UseBackColor = true;
            this.XtraBottom.Controls.Add(this.lblName);
            this.XtraBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.XtraBottom.Location = new System.Drawing.Point(0, 0);
            this.XtraBottom.Margin = new System.Windows.Forms.Padding(2, 6, 2, 6);
            this.XtraBottom.Name = "XtraBottom";
            this.XtraBottom.Size = new System.Drawing.Size(800, 40);
            this.XtraBottom.TabIndex = 11;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Kanit", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.ForeColor = System.Drawing.Color.White;
            this.lblName.Location = new System.Drawing.Point(11, 12);
            this.lblName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(104, 20);
            this.lblName.TabIndex = 614;
            this.lblName.Text = "รายการพักการซื้อ";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // BtnBack
            // 
            this.BtnBack.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(141)))), ((int)(((byte)(59)))));
            this.BtnBack.Appearance.Font = new System.Drawing.Font("Kanit", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnBack.Appearance.ForeColor = System.Drawing.Color.Red;
            this.BtnBack.Appearance.Options.UseBackColor = true;
            this.BtnBack.Appearance.Options.UseFont = true;
            this.BtnBack.Appearance.Options.UseForeColor = true;
            this.BtnBack.AppearanceHovered.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Underline);
            this.BtnBack.AppearanceHovered.Options.UseFont = true;
            this.BtnBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnBack.Dock = System.Windows.Forms.DockStyle.Right;
            this.BtnBack.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.BtnBack.Location = new System.Drawing.Point(800, 0);
            this.BtnBack.Margin = new System.Windows.Forms.Padding(2, 6, 2, 6);
            this.BtnBack.Name = "BtnBack";
            this.BtnBack.Size = new System.Drawing.Size(133, 40);
            this.BtnBack.TabIndex = 442;
            this.BtnBack.Text = "ปิดหน้าต่าง";
            this.BtnBack.Click += new System.EventHandler(this.BtnBack_Click);
            // 
            // GridSaveBuy
            // 
            this.GridSaveBuy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.GridSaveBuy.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2, 6, 2, 6);
            this.GridSaveBuy.Font = new System.Drawing.Font("Trebuchet MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            gridLevelNode1.RelationName = "Level1";
            this.GridSaveBuy.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.GridSaveBuy.Location = new System.Drawing.Point(15, 105);
            this.GridSaveBuy.LookAndFeel.SkinName = "Seven Classic";
            this.GridSaveBuy.LookAndFeel.UseDefaultLookAndFeel = false;
            this.GridSaveBuy.MainView = this.GridViewSaveBuy;
            this.GridSaveBuy.Margin = new System.Windows.Forms.Padding(2, 6, 2, 6);
            this.GridSaveBuy.Name = "GridSaveBuy";
            this.GridSaveBuy.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.SpinNumber,
            this.TxtText,
            this.DtDate,
            this.BtnSelect,
            this.BtnDelete});
            this.GridSaveBuy.Size = new System.Drawing.Size(897, 440);
            this.GridSaveBuy.TabIndex = 579;
            this.GridSaveBuy.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GridViewSaveBuy});
            // 
            // GridViewSaveBuy
            // 
            this.GridViewSaveBuy.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.Black;
            this.GridViewSaveBuy.Appearance.ColumnFilterButton.Options.UseFont = true;
            this.GridViewSaveBuy.Appearance.ColumnFilterButton.Options.UseForeColor = true;
            this.GridViewSaveBuy.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Black;
            this.GridViewSaveBuy.Appearance.ColumnFilterButtonActive.Options.UseFont = true;
            this.GridViewSaveBuy.Appearance.ColumnFilterButtonActive.Options.UseForeColor = true;
            this.GridViewSaveBuy.Appearance.CustomizationFormHint.ForeColor = System.Drawing.Color.Black;
            this.GridViewSaveBuy.Appearance.CustomizationFormHint.Options.UseFont = true;
            this.GridViewSaveBuy.Appearance.CustomizationFormHint.Options.UseForeColor = true;
            this.GridViewSaveBuy.Appearance.DetailTip.ForeColor = System.Drawing.Color.Black;
            this.GridViewSaveBuy.Appearance.DetailTip.Options.UseFont = true;
            this.GridViewSaveBuy.Appearance.DetailTip.Options.UseForeColor = true;
            this.GridViewSaveBuy.Appearance.Empty.ForeColor = System.Drawing.Color.Black;
            this.GridViewSaveBuy.Appearance.Empty.Options.UseFont = true;
            this.GridViewSaveBuy.Appearance.Empty.Options.UseForeColor = true;
            this.GridViewSaveBuy.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black;
            this.GridViewSaveBuy.Appearance.EvenRow.Options.UseFont = true;
            this.GridViewSaveBuy.Appearance.EvenRow.Options.UseForeColor = true;
            this.GridViewSaveBuy.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.Black;
            this.GridViewSaveBuy.Appearance.FilterCloseButton.Options.UseFont = true;
            this.GridViewSaveBuy.Appearance.FilterCloseButton.Options.UseForeColor = true;
            this.GridViewSaveBuy.Appearance.FilterPanel.ForeColor = System.Drawing.Color.Black;
            this.GridViewSaveBuy.Appearance.FilterPanel.Options.UseFont = true;
            this.GridViewSaveBuy.Appearance.FilterPanel.Options.UseForeColor = true;
            this.GridViewSaveBuy.Appearance.FixedLine.ForeColor = System.Drawing.Color.Black;
            this.GridViewSaveBuy.Appearance.FixedLine.Options.UseFont = true;
            this.GridViewSaveBuy.Appearance.FixedLine.Options.UseForeColor = true;
            this.GridViewSaveBuy.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black;
            this.GridViewSaveBuy.Appearance.FocusedCell.Options.UseFont = true;
            this.GridViewSaveBuy.Appearance.FocusedCell.Options.UseForeColor = true;
            this.GridViewSaveBuy.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black;
            this.GridViewSaveBuy.Appearance.FocusedRow.Options.UseFont = true;
            this.GridViewSaveBuy.Appearance.FocusedRow.Options.UseForeColor = true;
            this.GridViewSaveBuy.Appearance.FooterPanel.Font = new System.Drawing.Font("Kanit", 10F, System.Drawing.FontStyle.Bold);
            this.GridViewSaveBuy.Appearance.FooterPanel.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.GridViewSaveBuy.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black;
            this.GridViewSaveBuy.Appearance.FooterPanel.Options.UseFont = true;
            this.GridViewSaveBuy.Appearance.FooterPanel.Options.UseForeColor = true;
            this.GridViewSaveBuy.Appearance.FooterPanel.Options.UseTextOptions = true;
            this.GridViewSaveBuy.Appearance.FooterPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GridViewSaveBuy.Appearance.GroupButton.ForeColor = System.Drawing.Color.Black;
            this.GridViewSaveBuy.Appearance.GroupButton.Options.UseFont = true;
            this.GridViewSaveBuy.Appearance.GroupButton.Options.UseForeColor = true;
            this.GridViewSaveBuy.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black;
            this.GridViewSaveBuy.Appearance.GroupFooter.Options.UseFont = true;
            this.GridViewSaveBuy.Appearance.GroupFooter.Options.UseForeColor = true;
            this.GridViewSaveBuy.Appearance.GroupPanel.ForeColor = System.Drawing.Color.Black;
            this.GridViewSaveBuy.Appearance.GroupPanel.Options.UseFont = true;
            this.GridViewSaveBuy.Appearance.GroupPanel.Options.UseForeColor = true;
            this.GridViewSaveBuy.Appearance.GroupRow.ForeColor = System.Drawing.Color.Black;
            this.GridViewSaveBuy.Appearance.GroupRow.Options.UseFont = true;
            this.GridViewSaveBuy.Appearance.GroupRow.Options.UseForeColor = true;
            this.GridViewSaveBuy.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.GridViewSaveBuy.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black;
            this.GridViewSaveBuy.Appearance.HeaderPanel.Options.UseBackColor = true;
            this.GridViewSaveBuy.Appearance.HeaderPanel.Options.UseFont = true;
            this.GridViewSaveBuy.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.GridViewSaveBuy.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.GridViewSaveBuy.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GridViewSaveBuy.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.GridViewSaveBuy.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.Black;
            this.GridViewSaveBuy.Appearance.HideSelectionRow.Options.UseFont = true;
            this.GridViewSaveBuy.Appearance.HideSelectionRow.Options.UseForeColor = true;
            this.GridViewSaveBuy.Appearance.HorzLine.ForeColor = System.Drawing.Color.Black;
            this.GridViewSaveBuy.Appearance.HorzLine.Options.UseFont = true;
            this.GridViewSaveBuy.Appearance.HorzLine.Options.UseForeColor = true;
            this.GridViewSaveBuy.Appearance.HotTrackedRow.ForeColor = System.Drawing.Color.Black;
            this.GridViewSaveBuy.Appearance.HotTrackedRow.Options.UseFont = true;
            this.GridViewSaveBuy.Appearance.HotTrackedRow.Options.UseForeColor = true;
            this.GridViewSaveBuy.Appearance.OddRow.ForeColor = System.Drawing.Color.Black;
            this.GridViewSaveBuy.Appearance.OddRow.Options.UseFont = true;
            this.GridViewSaveBuy.Appearance.OddRow.Options.UseForeColor = true;
            this.GridViewSaveBuy.Appearance.Preview.ForeColor = System.Drawing.Color.Black;
            this.GridViewSaveBuy.Appearance.Preview.Options.UseFont = true;
            this.GridViewSaveBuy.Appearance.Preview.Options.UseForeColor = true;
            this.GridViewSaveBuy.Appearance.Row.ForeColor = System.Drawing.Color.Black;
            this.GridViewSaveBuy.Appearance.Row.Options.UseFont = true;
            this.GridViewSaveBuy.Appearance.Row.Options.UseForeColor = true;
            this.GridViewSaveBuy.Appearance.Row.Options.UseTextOptions = true;
            this.GridViewSaveBuy.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GridViewSaveBuy.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.GridViewSaveBuy.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.GridViewSaveBuy.Appearance.RowSeparator.ForeColor = System.Drawing.Color.Black;
            this.GridViewSaveBuy.Appearance.RowSeparator.Options.UseFont = true;
            this.GridViewSaveBuy.Appearance.RowSeparator.Options.UseForeColor = true;
            this.GridViewSaveBuy.Appearance.SelectedRow.ForeColor = System.Drawing.Color.Black;
            this.GridViewSaveBuy.Appearance.SelectedRow.Options.UseFont = true;
            this.GridViewSaveBuy.Appearance.SelectedRow.Options.UseForeColor = true;
            this.GridViewSaveBuy.Appearance.TopNewRow.ForeColor = System.Drawing.Color.Black;
            this.GridViewSaveBuy.Appearance.TopNewRow.Options.UseFont = true;
            this.GridViewSaveBuy.Appearance.TopNewRow.Options.UseForeColor = true;
            this.GridViewSaveBuy.Appearance.VertLine.ForeColor = System.Drawing.Color.Black;
            this.GridViewSaveBuy.Appearance.VertLine.Options.UseFont = true;
            this.GridViewSaveBuy.Appearance.VertLine.Options.UseForeColor = true;
            this.GridViewSaveBuy.Appearance.ViewCaption.ForeColor = System.Drawing.Color.Black;
            this.GridViewSaveBuy.Appearance.ViewCaption.Options.UseFont = true;
            this.GridViewSaveBuy.Appearance.ViewCaption.Options.UseForeColor = true;
            this.GridViewSaveBuy.ColumnPanelRowHeight = 40;
            this.GridViewSaveBuy.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSaveBuyId,
            this.colCustomerId,
            this.colProductUsing,
            this.colBuyDate,
            this.colCustomerName,
            this.colBuyNumber,
            this.colWeightAmountTotal,
            this.colNetTotal,
            this.colValueBalance,
            this.colSelect,
            this.colDelete});
            this.GridViewSaveBuy.DetailHeight = 596;
            this.GridViewSaveBuy.FixedLineWidth = 4;
            this.GridViewSaveBuy.FooterPanelHeight = 35;
            this.GridViewSaveBuy.GridControl = this.GridSaveBuy;
            this.GridViewSaveBuy.GroupCount = 1;
            this.GridViewSaveBuy.GroupRowHeight = 40;
            this.GridViewSaveBuy.Name = "GridViewSaveBuy";
            this.GridViewSaveBuy.OptionsBehavior.AutoExpandAllGroups = true;
            this.GridViewSaveBuy.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.GridViewSaveBuy.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;
            this.GridViewSaveBuy.OptionsView.RowAutoHeight = true;
            this.GridViewSaveBuy.OptionsView.ShowFooter = true;
            this.GridViewSaveBuy.OptionsView.ShowGroupPanel = false;
            this.GridViewSaveBuy.OptionsView.ShowIndicator = false;
            this.GridViewSaveBuy.RowHeight = 40;
            this.GridViewSaveBuy.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colProductUsing, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // colSaveBuyId
            // 
            this.colSaveBuyId.Caption = "SaveBuyId";
            this.colSaveBuyId.FieldName = "SaveBuyId";
            this.colSaveBuyId.MaxWidth = 117;
            this.colSaveBuyId.MinWidth = 117;
            this.colSaveBuyId.Name = "colSaveBuyId";
            this.colSaveBuyId.OptionsColumn.AllowEdit = false;
            this.colSaveBuyId.Width = 117;
            // 
            // colCustomerId
            // 
            this.colCustomerId.Caption = "CustomerId";
            this.colCustomerId.FieldName = "CustomerId";
            this.colCustomerId.MaxWidth = 117;
            this.colCustomerId.MinWidth = 117;
            this.colCustomerId.Name = "colCustomerId";
            this.colCustomerId.OptionsColumn.AllowEdit = false;
            this.colCustomerId.Width = 117;
            // 
            // colProductUsing
            // 
            this.colProductUsing.Caption = "ประเภทการส่ง";
            this.colProductUsing.FieldName = "ProductUsing";
            this.colProductUsing.MinWidth = 100;
            this.colProductUsing.Name = "colProductUsing";
            this.colProductUsing.OptionsColumn.AllowEdit = false;
            this.colProductUsing.Visible = true;
            this.colProductUsing.VisibleIndex = 2;
            this.colProductUsing.Width = 100;
            // 
            // colBuyDate
            // 
            this.colBuyDate.Caption = "วันที่";
            this.colBuyDate.ColumnEdit = this.DtDate;
            this.colBuyDate.FieldName = "BuyDate";
            this.colBuyDate.MaxWidth = 120;
            this.colBuyDate.MinWidth = 120;
            this.colBuyDate.Name = "colBuyDate";
            this.colBuyDate.OptionsColumn.AllowEdit = false;
            this.colBuyDate.Visible = true;
            this.colBuyDate.VisibleIndex = 1;
            this.colBuyDate.Width = 120;
            // 
            // DtDate
            // 
            this.DtDate.AutoHeight = false;
            this.DtDate.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DtDate.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DtDate.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.ClassicNew;
            this.DtDate.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.DtDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.DtDate.EditFormat.FormatString = "dd/MM/yyyy";
            this.DtDate.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.DtDate.Mask.EditMask = "dd/MM/yyyy";
            this.DtDate.Name = "DtDate";
            this.DtDate.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.False;
            // 
            // colCustomerName
            // 
            this.colCustomerName.AppearanceCell.Options.UseTextOptions = true;
            this.colCustomerName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colCustomerName.Caption = "ชื่อลูกค้า";
            this.colCustomerName.FieldName = "CustomerName";
            this.colCustomerName.MinWidth = 175;
            this.colCustomerName.Name = "colCustomerName";
            this.colCustomerName.OptionsColumn.AllowEdit = false;
            this.colCustomerName.Visible = true;
            this.colCustomerName.VisibleIndex = 3;
            this.colCustomerName.Width = 227;
            // 
            // colBuyNumber
            // 
            this.colBuyNumber.AppearanceCell.Options.UseTextOptions = true;
            this.colBuyNumber.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colBuyNumber.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colBuyNumber.Caption = "การซื้อเลขที่";
            this.colBuyNumber.ColumnEdit = this.TxtText;
            this.colBuyNumber.FieldName = "BuyNumber";
            this.colBuyNumber.MaxWidth = 175;
            this.colBuyNumber.MinWidth = 175;
            this.colBuyNumber.Name = "colBuyNumber";
            this.colBuyNumber.OptionsColumn.AllowEdit = false;
            this.colBuyNumber.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "BuyNumber", "{0:n2} : รายการ")});
            this.colBuyNumber.Visible = true;
            this.colBuyNumber.VisibleIndex = 2;
            this.colBuyNumber.Width = 175;
            // 
            // TxtText
            // 
            this.TxtText.Appearance.Options.UseTextOptions = true;
            this.TxtText.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.TxtText.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.TxtText.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.TxtText.Name = "TxtText";
            // 
            // colWeightAmountTotal
            // 
            this.colWeightAmountTotal.Caption = "น้ำหนัก";
            this.colWeightAmountTotal.ColumnEdit = this.SpinNumber;
            this.colWeightAmountTotal.FieldName = "WeightAmountTotal";
            this.colWeightAmountTotal.MaxWidth = 150;
            this.colWeightAmountTotal.MinWidth = 150;
            this.colWeightAmountTotal.Name = "colWeightAmountTotal";
            this.colWeightAmountTotal.OptionsColumn.AllowEdit = false;
            this.colWeightAmountTotal.OptionsColumn.AllowMove = false;
            this.colWeightAmountTotal.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "WeightAmountTotal", "{0:N2}")});
            this.colWeightAmountTotal.Visible = true;
            this.colWeightAmountTotal.VisibleIndex = 4;
            this.colWeightAmountTotal.Width = 150;
            // 
            // SpinNumber
            // 
            this.SpinNumber.AutoHeight = false;
            this.SpinNumber.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.SpinNumber.DisplayFormat.FormatString = "{0:n2}";
            this.SpinNumber.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.SpinNumber.EditFormat.FormatString = "{0:n2}";
            this.SpinNumber.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.SpinNumber.Name = "SpinNumber";
            // 
            // colNetTotal
            // 
            this.colNetTotal.Caption = "Total";
            this.colNetTotal.ColumnEdit = this.SpinNumber;
            this.colNetTotal.FieldName = "NetTotal";
            this.colNetTotal.MaxWidth = 175;
            this.colNetTotal.MinWidth = 175;
            this.colNetTotal.Name = "colNetTotal";
            this.colNetTotal.OptionsColumn.AllowEdit = false;
            this.colNetTotal.Width = 175;
            // 
            // colValueBalance
            // 
            this.colValueBalance.Caption = "ValueBalance";
            this.colValueBalance.FieldName = "ValueBalance";
            this.colValueBalance.MaxWidth = 140;
            this.colValueBalance.MinWidth = 140;
            this.colValueBalance.Name = "colValueBalance";
            this.colValueBalance.OptionsColumn.AllowEdit = false;
            this.colValueBalance.Width = 140;
            // 
            // colSelect
            // 
            this.colSelect.Caption = "Select";
            this.colSelect.ColumnEdit = this.BtnSelect;
            this.colSelect.MaxWidth = 82;
            this.colSelect.MinWidth = 82;
            this.colSelect.Name = "colSelect";
            this.colSelect.OptionsColumn.ShowCaption = false;
            this.colSelect.Visible = true;
            this.colSelect.VisibleIndex = 0;
            this.colSelect.Width = 103;
            // 
            // BtnSelect
            // 
            this.BtnSelect.AutoHeight = false;
            this.BtnSelect.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "เลือก", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.BtnSelect.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.BtnSelect.Name = "BtnSelect";
            this.BtnSelect.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.BtnSelect.Click += new System.EventHandler(this.BtnSelect_Click);
            // 
            // colDelete
            // 
            this.colDelete.Caption = "Delete";
            this.colDelete.ColumnEdit = this.BtnDelete;
            this.colDelete.MaxWidth = 70;
            this.colDelete.MinWidth = 70;
            this.colDelete.Name = "colDelete";
            this.colDelete.OptionsColumn.ShowCaption = false;
            this.colDelete.Visible = true;
            this.colDelete.VisibleIndex = 5;
            this.colDelete.Width = 70;
            // 
            // BtnDelete
            // 
            this.BtnDelete.AutoHeight = false;
            this.BtnDelete.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "ลบ", -1, true, true, false, editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject5, serializableAppearanceObject6, serializableAppearanceObject7, serializableAppearanceObject8, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.BtnDelete.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // CboProductUsing
            // 
            this.CboProductUsing.Location = new System.Drawing.Point(119, 56);
            this.CboProductUsing.Name = "CboProductUsing";
            this.CboProductUsing.Properties.Appearance.Font = new System.Drawing.Font("Kanit", 12F);
            this.CboProductUsing.Properties.Appearance.Options.UseFont = true;
            this.CboProductUsing.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.CboProductUsing.Properties.Items.AddRange(new object[] {
            "-- แสดงทั้งหมด --",
            "ส่งโรงรีด",
            "ส่งโรงงาน"});
            this.CboProductUsing.Properties.LookAndFeel.SkinName = "DevExpress Style";
            this.CboProductUsing.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.CboProductUsing.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.CboProductUsing.Size = new System.Drawing.Size(227, 32);
            this.CboProductUsing.TabIndex = 10002;
            // 
            // BtnSearch
            // 
            this.BtnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSearch.Appearance.Font = new System.Drawing.Font("Kanit", 10F);
            this.BtnSearch.Appearance.Options.UseFont = true;
            this.BtnSearch.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.BtnSearch.Location = new System.Drawing.Point(352, 57);
            this.BtnSearch.LookAndFeel.SkinName = "DevExpress Dark Style";
            this.BtnSearch.LookAndFeel.UseDefaultLookAndFeel = false;
            this.BtnSearch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BtnSearch.Name = "BtnSearch";
            this.BtnSearch.Size = new System.Drawing.Size(105, 31);
            this.BtnSearch.TabIndex = 10003;
            this.BtnSearch.Text = "ค้นหา";
            this.BtnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 19);
            this.label1.TabIndex = 10004;
            this.label1.Text = "เลือกประเภทการส่ง";
            // 
            // UcOpenSaveBuy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BtnSearch);
            this.Controls.Add(this.CboProductUsing);
            this.Controls.Add(this.GridSaveBuy);
            this.Controls.Add(this.XtraBand);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "UcOpenSaveBuy";
            this.Size = new System.Drawing.Size(933, 562);
            this.Load += new System.EventHandler(this.UcOpenSaveBuy_Load);
            this.XtraBand.ResumeLayout(false);
            this.XtraBottom.ResumeLayout(false);
            this.XtraBottom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridSaveBuy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewSaveBuy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DtDate.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DtDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtText)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SpinNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnSelect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnDelete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CboProductUsing.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.XtraScrollableControl XtraBand;
        internal DevExpress.XtraEditors.XtraScrollableControl XtraBottom;
        private System.Windows.Forms.Label lblName;
        internal DevExpress.XtraEditors.SimpleButton BtnBack;
        internal DevExpress.XtraGrid.GridControl GridSaveBuy;
        internal DevExpress.XtraGrid.Views.Grid.GridView GridViewSaveBuy;
        private DevExpress.XtraGrid.Columns.GridColumn colSaveBuyId;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomerId;
        private DevExpress.XtraGrid.Columns.GridColumn colBuyDate;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit DtDate;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomerName;
        private DevExpress.XtraGrid.Columns.GridColumn colBuyNumber;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit TxtText;
        private DevExpress.XtraGrid.Columns.GridColumn colNetTotal;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit SpinNumber;
        private DevExpress.XtraGrid.Columns.GridColumn colValueBalance;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit BtnSelect;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit BtnDelete;
        private DevExpress.XtraGrid.Columns.GridColumn colSelect;
        private DevExpress.XtraGrid.Columns.GridColumn colDelete;
        private DevExpress.XtraGrid.Columns.GridColumn colWeightAmountTotal;
        private DevExpress.XtraGrid.Columns.GridColumn colProductUsing;
        private DevExpress.XtraEditors.ComboBoxEdit CboProductUsing;
        private DevExpress.XtraEditors.SimpleButton BtnSearch;
        private System.Windows.Forms.Label label1;
    }
}
