
namespace RubberSoft.Main
{
    partial class FrmAddPercent
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            this.XtraBack = new DevExpress.XtraEditors.XtraScrollableControl();
            this.LinkSelect = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.GridBuyProduct = new DevExpress.XtraGrid.GridControl();
            this.GridViewByProduct = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colหมายเหตุ = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRunNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBuyProductId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsSelect = new DevExpress.XtraGrid.Columns.GridColumn();
            this.CkIsSelect = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colPriceName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPercentage = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colน้ำหนัก = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colWeightAmount_Plate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.SpinBuy = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.colDrc = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colยางแห้ง = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colยอดยางแห้ง = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colราคา = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCalRubber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colจำนวนเงิน = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coPriceId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colItemPriceId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsDefault2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEdit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDelete = new DevExpress.XtraGrid.Columns.GridColumn();
            this.SpinPercentage = new DevExpress.XtraEditors.SpinEdit();
            this.lblPercentage = new System.Windows.Forms.Label();
            this.XtraBack.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridBuyProduct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewByProduct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CkIsSelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SpinBuy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SpinPercentage.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // XtraBack
            // 
            this.XtraBack.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
            this.XtraBack.Appearance.Options.UseBackColor = true;
            this.XtraBack.Controls.Add(this.LinkSelect);
            this.XtraBack.Controls.Add(this.BtnSave);
            this.XtraBack.Controls.Add(this.BtnCancel);
            this.XtraBack.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.XtraBack.Location = new System.Drawing.Point(0, 438);
            this.XtraBack.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.XtraBack.Name = "XtraBack";
            this.XtraBack.Size = new System.Drawing.Size(1028, 60);
            this.XtraBack.TabIndex = 15;
            // 
            // LinkSelect
            // 
            this.LinkSelect.Appearance.Font = new System.Drawing.Font("Kanit", 12F);
            this.LinkSelect.Appearance.Options.UseFont = true;
            this.LinkSelect.Location = new System.Drawing.Point(25, 9);
            this.LinkSelect.Name = "LinkSelect";
            this.LinkSelect.Size = new System.Drawing.Size(83, 25);
            this.LinkSelect.TabIndex = 659;
            this.LinkSelect.Text = "เลือกทั้งหมด";
            this.LinkSelect.Click += new System.EventHandler(this.LinkSelect_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnSave.Appearance.Font = new System.Drawing.Font("Kanit", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.Appearance.Options.UseFont = true;
            this.BtnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.BtnSave.Location = new System.Drawing.Point(736, 9);
            this.BtnSave.LookAndFeel.SkinName = "DevExpress Dark Style";
            this.BtnSave.LookAndFeel.UseDefaultLookAndFeel = false;
            this.BtnSave.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(117, 38);
            this.BtnSave.TabIndex = 658;
            this.BtnSave.Text = "บันทึก";
            // 
            // BtnCancel
            // 
            this.BtnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Kanit", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnCancel.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.BtnCancel.Location = new System.Drawing.Point(889, 9);
            this.BtnCancel.LookAndFeel.SkinName = "DevExpress Dark Style";
            this.BtnCancel.LookAndFeel.UseDefaultLookAndFeel = false;
            this.BtnCancel.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(117, 38);
            this.BtnCancel.TabIndex = 657;
            this.BtnCancel.Text = "กลับ";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // GridBuyProduct
            // 
            this.GridBuyProduct.Cursor = System.Windows.Forms.Cursors.Hand;
            this.GridBuyProduct.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            gridLevelNode1.RelationName = "Level1";
            this.GridBuyProduct.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.GridBuyProduct.Location = new System.Drawing.Point(25, 76);
            this.GridBuyProduct.LookAndFeel.SkinName = "Seven Classic";
            this.GridBuyProduct.LookAndFeel.UseDefaultLookAndFeel = false;
            this.GridBuyProduct.MainView = this.GridViewByProduct;
            this.GridBuyProduct.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.GridBuyProduct.Name = "GridBuyProduct";
            this.GridBuyProduct.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.SpinBuy,
            this.CkIsSelect});
            this.GridBuyProduct.Size = new System.Drawing.Size(964, 338);
            this.GridBuyProduct.TabIndex = 581;
            this.GridBuyProduct.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GridViewByProduct});
            // 
            // GridViewByProduct
            // 
            this.GridViewByProduct.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.Black;
            this.GridViewByProduct.Appearance.ColumnFilterButton.Options.UseFont = true;
            this.GridViewByProduct.Appearance.ColumnFilterButton.Options.UseForeColor = true;
            this.GridViewByProduct.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Black;
            this.GridViewByProduct.Appearance.ColumnFilterButtonActive.Options.UseFont = true;
            this.GridViewByProduct.Appearance.ColumnFilterButtonActive.Options.UseForeColor = true;
            this.GridViewByProduct.Appearance.CustomizationFormHint.ForeColor = System.Drawing.Color.Black;
            this.GridViewByProduct.Appearance.CustomizationFormHint.Options.UseFont = true;
            this.GridViewByProduct.Appearance.CustomizationFormHint.Options.UseForeColor = true;
            this.GridViewByProduct.Appearance.DetailTip.ForeColor = System.Drawing.Color.Black;
            this.GridViewByProduct.Appearance.DetailTip.Options.UseFont = true;
            this.GridViewByProduct.Appearance.DetailTip.Options.UseForeColor = true;
            this.GridViewByProduct.Appearance.Empty.ForeColor = System.Drawing.Color.Black;
            this.GridViewByProduct.Appearance.Empty.Options.UseFont = true;
            this.GridViewByProduct.Appearance.Empty.Options.UseForeColor = true;
            this.GridViewByProduct.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black;
            this.GridViewByProduct.Appearance.EvenRow.Options.UseFont = true;
            this.GridViewByProduct.Appearance.EvenRow.Options.UseForeColor = true;
            this.GridViewByProduct.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.Black;
            this.GridViewByProduct.Appearance.FilterCloseButton.Options.UseFont = true;
            this.GridViewByProduct.Appearance.FilterCloseButton.Options.UseForeColor = true;
            this.GridViewByProduct.Appearance.FilterPanel.ForeColor = System.Drawing.Color.Black;
            this.GridViewByProduct.Appearance.FilterPanel.Options.UseFont = true;
            this.GridViewByProduct.Appearance.FilterPanel.Options.UseForeColor = true;
            this.GridViewByProduct.Appearance.FixedLine.ForeColor = System.Drawing.Color.Black;
            this.GridViewByProduct.Appearance.FixedLine.Options.UseFont = true;
            this.GridViewByProduct.Appearance.FixedLine.Options.UseForeColor = true;
            this.GridViewByProduct.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black;
            this.GridViewByProduct.Appearance.FocusedCell.Options.UseFont = true;
            this.GridViewByProduct.Appearance.FocusedCell.Options.UseForeColor = true;
            this.GridViewByProduct.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black;
            this.GridViewByProduct.Appearance.FocusedRow.Options.UseFont = true;
            this.GridViewByProduct.Appearance.FocusedRow.Options.UseForeColor = true;
            this.GridViewByProduct.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black;
            this.GridViewByProduct.Appearance.FooterPanel.Options.UseFont = true;
            this.GridViewByProduct.Appearance.FooterPanel.Options.UseForeColor = true;
            this.GridViewByProduct.Appearance.FooterPanel.Options.UseTextOptions = true;
            this.GridViewByProduct.Appearance.FooterPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GridViewByProduct.Appearance.GroupButton.ForeColor = System.Drawing.Color.Black;
            this.GridViewByProduct.Appearance.GroupButton.Options.UseFont = true;
            this.GridViewByProduct.Appearance.GroupButton.Options.UseForeColor = true;
            this.GridViewByProduct.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black;
            this.GridViewByProduct.Appearance.GroupFooter.Options.UseFont = true;
            this.GridViewByProduct.Appearance.GroupFooter.Options.UseForeColor = true;
            this.GridViewByProduct.Appearance.GroupPanel.ForeColor = System.Drawing.Color.Black;
            this.GridViewByProduct.Appearance.GroupPanel.Options.UseFont = true;
            this.GridViewByProduct.Appearance.GroupPanel.Options.UseForeColor = true;
            this.GridViewByProduct.Appearance.GroupRow.ForeColor = System.Drawing.Color.Black;
            this.GridViewByProduct.Appearance.GroupRow.Options.UseFont = true;
            this.GridViewByProduct.Appearance.GroupRow.Options.UseForeColor = true;
            this.GridViewByProduct.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.GridViewByProduct.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black;
            this.GridViewByProduct.Appearance.HeaderPanel.Options.UseBackColor = true;
            this.GridViewByProduct.Appearance.HeaderPanel.Options.UseFont = true;
            this.GridViewByProduct.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.GridViewByProduct.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.GridViewByProduct.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GridViewByProduct.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.GridViewByProduct.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.Black;
            this.GridViewByProduct.Appearance.HideSelectionRow.Options.UseFont = true;
            this.GridViewByProduct.Appearance.HideSelectionRow.Options.UseForeColor = true;
            this.GridViewByProduct.Appearance.HorzLine.ForeColor = System.Drawing.Color.Black;
            this.GridViewByProduct.Appearance.HorzLine.Options.UseFont = true;
            this.GridViewByProduct.Appearance.HorzLine.Options.UseForeColor = true;
            this.GridViewByProduct.Appearance.HotTrackedRow.ForeColor = System.Drawing.Color.Black;
            this.GridViewByProduct.Appearance.HotTrackedRow.Options.UseFont = true;
            this.GridViewByProduct.Appearance.HotTrackedRow.Options.UseForeColor = true;
            this.GridViewByProduct.Appearance.OddRow.ForeColor = System.Drawing.Color.Black;
            this.GridViewByProduct.Appearance.OddRow.Options.UseFont = true;
            this.GridViewByProduct.Appearance.OddRow.Options.UseForeColor = true;
            this.GridViewByProduct.Appearance.Preview.ForeColor = System.Drawing.Color.Black;
            this.GridViewByProduct.Appearance.Preview.Options.UseFont = true;
            this.GridViewByProduct.Appearance.Preview.Options.UseForeColor = true;
            this.GridViewByProduct.Appearance.Row.ForeColor = System.Drawing.Color.Black;
            this.GridViewByProduct.Appearance.Row.Options.UseFont = true;
            this.GridViewByProduct.Appearance.Row.Options.UseForeColor = true;
            this.GridViewByProduct.Appearance.Row.Options.UseTextOptions = true;
            this.GridViewByProduct.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GridViewByProduct.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.GridViewByProduct.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.GridViewByProduct.Appearance.RowSeparator.ForeColor = System.Drawing.Color.Black;
            this.GridViewByProduct.Appearance.RowSeparator.Options.UseFont = true;
            this.GridViewByProduct.Appearance.RowSeparator.Options.UseForeColor = true;
            this.GridViewByProduct.Appearance.SelectedRow.ForeColor = System.Drawing.Color.Black;
            this.GridViewByProduct.Appearance.SelectedRow.Options.UseFont = true;
            this.GridViewByProduct.Appearance.SelectedRow.Options.UseForeColor = true;
            this.GridViewByProduct.Appearance.TopNewRow.ForeColor = System.Drawing.Color.Black;
            this.GridViewByProduct.Appearance.TopNewRow.Options.UseFont = true;
            this.GridViewByProduct.Appearance.TopNewRow.Options.UseForeColor = true;
            this.GridViewByProduct.Appearance.VertLine.ForeColor = System.Drawing.Color.Black;
            this.GridViewByProduct.Appearance.VertLine.Options.UseFont = true;
            this.GridViewByProduct.Appearance.VertLine.Options.UseForeColor = true;
            this.GridViewByProduct.Appearance.ViewCaption.ForeColor = System.Drawing.Color.Black;
            this.GridViewByProduct.Appearance.ViewCaption.Options.UseFont = true;
            this.GridViewByProduct.Appearance.ViewCaption.Options.UseForeColor = true;
            this.GridViewByProduct.AutoFillColumn = this.colหมายเหตุ;
            this.GridViewByProduct.ColumnPanelRowHeight = 28;
            this.GridViewByProduct.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colRunNo,
            this.colBuyProductId,
            this.colIsSelect,
            this.colPriceName,
            this.colPercentage,
            this.colน้ำหนัก,
            this.colWeightAmount_Plate,
            this.colDrc,
            this.colยางแห้ง,
            this.colยอดยางแห้ง,
            this.colราคา,
            this.colCalRubber,
            this.colจำนวนเงิน,
            this.colหมายเหตุ,
            this.coPriceId,
            this.colItemPriceId,
            this.colIsDefault2,
            this.colEdit,
            this.colDelete});
            this.GridViewByProduct.DetailHeight = 453;
            this.GridViewByProduct.FixedLineWidth = 3;
            this.GridViewByProduct.FooterPanelHeight = 28;
            this.GridViewByProduct.GridControl = this.GridBuyProduct;
            this.GridViewByProduct.Name = "GridViewByProduct";
            this.GridViewByProduct.OptionsBehavior.AutoExpandAllGroups = true;
            this.GridViewByProduct.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.GridViewByProduct.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.GridViewByProduct.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;
            this.GridViewByProduct.OptionsView.RowAutoHeight = true;
            this.GridViewByProduct.OptionsView.ShowFooter = true;
            this.GridViewByProduct.OptionsView.ShowGroupPanel = false;
            this.GridViewByProduct.OptionsView.ShowIndicator = false;
            this.GridViewByProduct.RowHeight = 28;
            // 
            // colหมายเหตุ
            // 
            this.colหมายเหตุ.Caption = "หมายเหตุ";
            this.colหมายเหตุ.FieldName = "Remark";
            this.colหมายเหตุ.MinWidth = 114;
            this.colหมายเหตุ.Name = "colหมายเหตุ";
            this.colหมายเหตุ.OptionsColumn.AllowEdit = false;
            this.colหมายเหตุ.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "Remark", "{0:n2} : รายการ")});
            this.colหมายเหตุ.Visible = true;
            this.colหมายเหตุ.VisibleIndex = 8;
            this.colหมายเหตุ.Width = 114;
            // 
            // colRunNo
            // 
            this.colRunNo.Caption = "RunNo";
            this.colRunNo.FieldName = "RunNo";
            this.colRunNo.MaxWidth = 57;
            this.colRunNo.MinWidth = 57;
            this.colRunNo.Name = "colRunNo";
            this.colRunNo.OptionsColumn.AllowEdit = false;
            this.colRunNo.Width = 57;
            // 
            // colBuyProductId
            // 
            this.colBuyProductId.Caption = "BuyProductId";
            this.colBuyProductId.FieldName = "BuyProductId";
            this.colBuyProductId.MaxWidth = 114;
            this.colBuyProductId.MinWidth = 114;
            this.colBuyProductId.Name = "colBuyProductId";
            this.colBuyProductId.OptionsColumn.AllowEdit = false;
            this.colBuyProductId.Width = 114;
            // 
            // colIsSelect
            // 
            this.colIsSelect.Caption = "เลือก";
            this.colIsSelect.ColumnEdit = this.CkIsSelect;
            this.colIsSelect.FieldName = "IsSelect";
            this.colIsSelect.MaxWidth = 60;
            this.colIsSelect.MinWidth = 60;
            this.colIsSelect.Name = "colIsSelect";
            this.colIsSelect.Visible = true;
            this.colIsSelect.VisibleIndex = 0;
            this.colIsSelect.Width = 60;
            // 
            // CkIsSelect
            // 
            this.CkIsSelect.AutoHeight = false;
            this.CkIsSelect.Name = "CkIsSelect";
            // 
            // colPriceName
            // 
            this.colPriceName.Caption = "ชื่อราคา";
            this.colPriceName.FieldName = "PriceName";
            this.colPriceName.MinWidth = 100;
            this.colPriceName.Name = "colPriceName";
            this.colPriceName.OptionsColumn.AllowEdit = false;
            this.colPriceName.Visible = true;
            this.colPriceName.VisibleIndex = 1;
            this.colPriceName.Width = 100;
            // 
            // colPercentage
            // 
            this.colPercentage.Caption = "Percen(%)";
            this.colPercentage.FieldName = "Percentage";
            this.colPercentage.MaxWidth = 114;
            this.colPercentage.MinWidth = 114;
            this.colPercentage.Name = "colPercentage";
            this.colPercentage.OptionsColumn.AllowEdit = false;
            this.colPercentage.Width = 114;
            // 
            // colน้ำหนัก
            // 
            this.colน้ำหนัก.Caption = "น้ำหนัก";
            this.colน้ำหนัก.ColumnEdit = this.SpinBuy;
            this.colน้ำหนัก.FieldName = "WeightAmount";
            this.colน้ำหนัก.MinWidth = 114;
            this.colน้ำหนัก.Name = "colน้ำหนัก";
            this.colน้ำหนัก.OptionsColumn.AllowEdit = false;
            this.colน้ำหนัก.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "WeightAmount", "{0:n2}")});
            this.colน้ำหนัก.Visible = true;
            this.colน้ำหนัก.VisibleIndex = 2;
            this.colน้ำหนัก.Width = 114;
            // 
            // colWeightAmount_Plate
            // 
            this.colWeightAmount_Plate.Caption = "Plate";
            this.colWeightAmount_Plate.ColumnEdit = this.SpinBuy;
            this.colWeightAmount_Plate.FieldName = "WeightAmount_Plate";
            this.colWeightAmount_Plate.MaxWidth = 114;
            this.colWeightAmount_Plate.MinWidth = 114;
            this.colWeightAmount_Plate.Name = "colWeightAmount_Plate";
            this.colWeightAmount_Plate.OptionsColumn.AllowFocus = false;
            this.colWeightAmount_Plate.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "WeightAmount_Plate", "{0:N2}")});
            this.colWeightAmount_Plate.Width = 114;
            // 
            // SpinBuy
            // 
            this.SpinBuy.AutoHeight = false;
            this.SpinBuy.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.SpinBuy.DisplayFormat.FormatString = "{0:N2}";
            this.SpinBuy.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.SpinBuy.EditFormat.FormatString = "{0:N2}";
            this.SpinBuy.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.SpinBuy.Mask.EditMask = "N2";
            this.SpinBuy.Name = "SpinBuy";
            // 
            // colDrc
            // 
            this.colDrc.Caption = "Drc%";
            this.colDrc.ColumnEdit = this.SpinBuy;
            this.colDrc.FieldName = "Drc";
            this.colDrc.MaxWidth = 91;
            this.colDrc.MinWidth = 69;
            this.colDrc.Name = "colDrc";
            this.colDrc.OptionsColumn.AllowEdit = false;
            this.colDrc.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "Drc", "{0}")});
            this.colDrc.Visible = true;
            this.colDrc.VisibleIndex = 3;
            this.colDrc.Width = 69;
            // 
            // colยางแห้ง
            // 
            this.colยางแห้ง.Caption = "ยางแห้ง";
            this.colยางแห้ง.ColumnEdit = this.SpinBuy;
            this.colยางแห้ง.FieldName = "TotalPrice_Smoke";
            this.colยางแห้ง.MaxWidth = 114;
            this.colยางแห้ง.MinWidth = 114;
            this.colยางแห้ง.Name = "colยางแห้ง";
            this.colยางแห้ง.OptionsColumn.AllowEdit = false;
            this.colยางแห้ง.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TotalPrice_Smoke", "{0:n2}")});
            this.colยางแห้ง.Visible = true;
            this.colยางแห้ง.VisibleIndex = 4;
            this.colยางแห้ง.Width = 114;
            // 
            // colยอดยางแห้ง
            // 
            this.colยอดยางแห้ง.Caption = "ยอดยางแห้ง";
            this.colยอดยางแห้ง.ColumnEdit = this.SpinBuy;
            this.colยอดยางแห้ง.FieldName = "WeightAmount_Raw";
            this.colยอดยางแห้ง.MaxWidth = 114;
            this.colยอดยางแห้ง.MinWidth = 114;
            this.colยอดยางแห้ง.Name = "colยอดยางแห้ง";
            this.colยอดยางแห้ง.OptionsColumn.AllowEdit = false;
            this.colยอดยางแห้ง.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "WeightAmount_Raw", "{0:n2}")});
            this.colยอดยางแห้ง.Visible = true;
            this.colยอดยางแห้ง.VisibleIndex = 5;
            this.colยอดยางแห้ง.Width = 114;
            // 
            // colราคา
            // 
            this.colราคา.Caption = "ราคา";
            this.colราคา.ColumnEdit = this.SpinBuy;
            this.colราคา.FieldName = "TotalPrice_Raw";
            this.colราคา.MaxWidth = 114;
            this.colราคา.MinWidth = 114;
            this.colราคา.Name = "colราคา";
            this.colราคา.OptionsColumn.AllowEdit = false;
            this.colราคา.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TotalPrice_Raw", "{0:n2}")});
            this.colราคา.Visible = true;
            this.colราคา.VisibleIndex = 6;
            this.colราคา.Width = 114;
            // 
            // colCalRubber
            // 
            this.colCalRubber.Caption = "คำนวณยาง";
            this.colCalRubber.FieldName = "CalRubber";
            this.colCalRubber.MaxWidth = 114;
            this.colCalRubber.MinWidth = 100;
            this.colCalRubber.Name = "colCalRubber";
            this.colCalRubber.OptionsColumn.AllowEdit = false;
            this.colCalRubber.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "CalRubber", "{0:n2}")});
            this.colCalRubber.Width = 114;
            // 
            // colจำนวนเงิน
            // 
            this.colจำนวนเงิน.Caption = "จำนวนเงิน";
            this.colจำนวนเงิน.ColumnEdit = this.SpinBuy;
            this.colจำนวนเงิน.FieldName = "TotalPrice";
            this.colจำนวนเงิน.MaxWidth = 114;
            this.colจำนวนเงิน.MinWidth = 114;
            this.colจำนวนเงิน.Name = "colจำนวนเงิน";
            this.colจำนวนเงิน.OptionsColumn.AllowEdit = false;
            this.colจำนวนเงิน.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TotalPrice", "{0:n2}")});
            this.colจำนวนเงิน.Visible = true;
            this.colจำนวนเงิน.VisibleIndex = 7;
            this.colจำนวนเงิน.Width = 114;
            // 
            // coPriceId
            // 
            this.coPriceId.Caption = "PriceId";
            this.coPriceId.FieldName = "PriceId";
            this.coPriceId.MaxWidth = 134;
            this.coPriceId.MinWidth = 11;
            this.coPriceId.Name = "coPriceId";
            this.coPriceId.OptionsColumn.AllowEdit = false;
            this.coPriceId.Width = 134;
            // 
            // colItemPriceId
            // 
            this.colItemPriceId.Caption = "ItemPriceId";
            this.colItemPriceId.FieldName = "ItemPriceId";
            this.colItemPriceId.MaxWidth = 100;
            this.colItemPriceId.MinWidth = 100;
            this.colItemPriceId.Name = "colItemPriceId";
            this.colItemPriceId.OptionsColumn.AllowEdit = false;
            this.colItemPriceId.Width = 100;
            // 
            // colIsDefault2
            // 
            this.colIsDefault2.Caption = "IsDefault";
            this.colIsDefault2.FieldName = "IsDefault";
            this.colIsDefault2.MaxWidth = 100;
            this.colIsDefault2.MinWidth = 100;
            this.colIsDefault2.Name = "colIsDefault2";
            this.colIsDefault2.OptionsColumn.AllowEdit = false;
            this.colIsDefault2.Width = 100;
            // 
            // colEdit
            // 
            this.colEdit.Caption = "Edit";
            this.colEdit.MaxWidth = 57;
            this.colEdit.MinWidth = 57;
            this.colEdit.Name = "colEdit";
            this.colEdit.OptionsColumn.AllowEdit = false;
            this.colEdit.OptionsColumn.ShowCaption = false;
            this.colEdit.Width = 57;
            // 
            // colDelete
            // 
            this.colDelete.AppearanceCell.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colDelete.AppearanceCell.Options.UseFont = true;
            this.colDelete.AppearanceHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F);
            this.colDelete.AppearanceHeader.Options.UseFont = true;
            this.colDelete.Caption = "Delete";
            this.colDelete.MaxWidth = 57;
            this.colDelete.MinWidth = 57;
            this.colDelete.Name = "colDelete";
            this.colDelete.OptionsColumn.ShowCaption = false;
            this.colDelete.Width = 57;
            // 
            // SpinPercentage
            // 
            this.SpinPercentage.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.SpinPercentage.Location = new System.Drawing.Point(174, 19);
            this.SpinPercentage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.SpinPercentage.Name = "SpinPercentage";
            this.SpinPercentage.Properties.Appearance.Font = new System.Drawing.Font("Kanit", 15F);
            this.SpinPercentage.Properties.Appearance.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.SpinPercentage.Properties.Appearance.Options.UseFont = true;
            this.SpinPercentage.Properties.Appearance.Options.UseForeColor = true;
            this.SpinPercentage.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.SpinPercentage.Properties.DisplayFormat.FormatString = "{0:N}";
            this.SpinPercentage.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.SpinPercentage.Properties.EditFormat.FormatString = "{0:N}";
            this.SpinPercentage.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.SpinPercentage.Properties.LookAndFeel.SkinName = "Office 2019 Colorful";
            this.SpinPercentage.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.SpinPercentage.Properties.MaxValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.SpinPercentage.Size = new System.Drawing.Size(179, 38);
            this.SpinPercentage.TabIndex = 652;
            // 
            // lblPercentage
            // 
            this.lblPercentage.AutoSize = true;
            this.lblPercentage.Font = new System.Drawing.Font("Kanit", 15F);
            this.lblPercentage.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblPercentage.Location = new System.Drawing.Point(36, 22);
            this.lblPercentage.Name = "lblPercentage";
            this.lblPercentage.Size = new System.Drawing.Size(132, 32);
            this.lblPercentage.TabIndex = 653;
            this.lblPercentage.Text = "Percentage%";
            this.lblPercentage.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // FrmAddPercent
            // 
            this.Appearance.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 498);
            this.ControlBox = false;
            this.Controls.Add(this.SpinPercentage);
            this.Controls.Add(this.lblPercentage);
            this.Controls.Add(this.GridBuyProduct);
            this.Controls.Add(this.XtraBack);
            this.LookAndFeel.SkinName = "The Bezier";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximumSize = new System.Drawing.Size(1028, 528);
            this.MinimumSize = new System.Drawing.Size(1028, 528);
            this.Name = "FrmAddPercent";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ข้อมูลการซื้อ";
            this.Load += new System.EventHandler(this.FrmAddPercent_Load);
            this.XtraBack.ResumeLayout(false);
            this.XtraBack.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridBuyProduct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewByProduct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CkIsSelect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SpinBuy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SpinPercentage.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.XtraScrollableControl XtraBack;
        private DevExpress.XtraEditors.SimpleButton BtnSave;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        internal DevExpress.XtraGrid.GridControl GridBuyProduct;
        internal DevExpress.XtraGrid.Views.Grid.GridView GridViewByProduct;
        private DevExpress.XtraGrid.Columns.GridColumn colหมายเหตุ;
        private DevExpress.XtraGrid.Columns.GridColumn colRunNo;
        private DevExpress.XtraGrid.Columns.GridColumn colBuyProductId;
        private DevExpress.XtraGrid.Columns.GridColumn colEdit;
        private DevExpress.XtraGrid.Columns.GridColumn colDelete;
        private DevExpress.XtraGrid.Columns.GridColumn colPriceName;
        private DevExpress.XtraGrid.Columns.GridColumn colPercentage;
        private DevExpress.XtraGrid.Columns.GridColumn colน้ำหนัก;
        private DevExpress.XtraGrid.Columns.GridColumn colWeightAmount_Plate;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit SpinBuy;
        private DevExpress.XtraGrid.Columns.GridColumn colDrc;
        private DevExpress.XtraGrid.Columns.GridColumn colยางแห้ง;
        private DevExpress.XtraGrid.Columns.GridColumn colยอดยางแห้ง;
        private DevExpress.XtraGrid.Columns.GridColumn colราคา;
        private DevExpress.XtraGrid.Columns.GridColumn colCalRubber;
        private DevExpress.XtraGrid.Columns.GridColumn colจำนวนเงิน;
        private DevExpress.XtraGrid.Columns.GridColumn coPriceId;
        private DevExpress.XtraGrid.Columns.GridColumn colItemPriceId;
        private DevExpress.XtraGrid.Columns.GridColumn colIsDefault2;
        private DevExpress.XtraEditors.SpinEdit SpinPercentage;
        private System.Windows.Forms.Label lblPercentage;
        private DevExpress.XtraEditors.HyperlinkLabelControl LinkSelect;
        private DevExpress.XtraGrid.Columns.GridColumn colIsSelect;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit CkIsSelect;
    }
}