
namespace RubberSoft.Main
{
    partial class FrmAddPrice
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
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAddPrice));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions2 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject5 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject6 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject7 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject8 = new DevExpress.Utils.SerializableAppearanceObject();
            this.BtnAddPrice = new DevExpress.XtraEditors.SimpleButton();
            this.lblราคาล่วงหน้า = new System.Windows.Forms.Label();
            this.SpinUnitPrice = new DevExpress.XtraEditors.SpinEdit();
            this.GrMain = new DevExpress.XtraEditors.GroupControl();
            this.GridItemPrice = new DevExpress.XtraGrid.GridControl();
            this.GridViewItemPrice = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colItemPriceId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPriceId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUnitPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.SpinEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.colIsDefault = new DevExpress.XtraGrid.Columns.GridColumn();
            this.CheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colEdit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.BtnEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.MemoEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.DateEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.ButtonEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.lblPriceName = new System.Windows.Forms.Label();
            this.colActive = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.SpinUnitPrice.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrMain)).BeginInit();
            this.GrMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridItemPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewItemPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SpinEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MemoEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateEdit2.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ButtonEdit2)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnAddPrice
            // 
            this.BtnAddPrice.Appearance.Font = new System.Drawing.Font("Kanit", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAddPrice.Appearance.Options.UseFont = true;
            this.BtnAddPrice.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnAddPrice.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.BtnAddPrice.Location = new System.Drawing.Point(277, 75);
            this.BtnAddPrice.LookAndFeel.SkinName = "VS2010";
            this.BtnAddPrice.LookAndFeel.UseDefaultLookAndFeel = false;
            this.BtnAddPrice.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BtnAddPrice.Name = "BtnAddPrice";
            this.BtnAddPrice.Size = new System.Drawing.Size(117, 37);
            this.BtnAddPrice.TabIndex = 653;
            this.BtnAddPrice.Text = "เพิ่ม";
            this.BtnAddPrice.Click += new System.EventHandler(this.BtnAddPrice_Click);
            // 
            // lblราคาล่วงหน้า
            // 
            this.lblราคาล่วงหน้า.AutoSize = true;
            this.lblราคาล่วงหน้า.Font = new System.Drawing.Font("Kanit", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblราคาล่วงหน้า.ForeColor = System.Drawing.SystemColors.InfoText;
            this.lblราคาล่วงหน้า.Location = new System.Drawing.Point(29, 84);
            this.lblราคาล่วงหน้า.Name = "lblราคาล่วงหน้า";
            this.lblราคาล่วงหน้า.Size = new System.Drawing.Size(71, 19);
            this.lblราคาล่วงหน้า.TabIndex = 652;
            this.lblราคาล่วงหน้า.Text = "ราคาล่วงหน้า";
            this.lblราคาล่วงหน้า.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // SpinUnitPrice
            // 
            this.SpinUnitPrice.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.SpinUnitPrice.Location = new System.Drawing.Point(131, 80);
            this.SpinUnitPrice.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.SpinUnitPrice.Name = "SpinUnitPrice";
            this.SpinUnitPrice.Properties.Appearance.Font = new System.Drawing.Font("Kanit", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SpinUnitPrice.Properties.Appearance.Options.UseFont = true;
            this.SpinUnitPrice.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.SpinUnitPrice.Properties.DisplayFormat.FormatString = "{0:n2}";
            this.SpinUnitPrice.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.SpinUnitPrice.Properties.EditFormat.FormatString = "{0:n2}";
            this.SpinUnitPrice.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.SpinUnitPrice.Size = new System.Drawing.Size(127, 26);
            this.SpinUnitPrice.TabIndex = 651;
            // 
            // GrMain
            // 
            this.GrMain.Controls.Add(this.GridItemPrice);
            this.GrMain.Location = new System.Drawing.Point(28, 137);
            this.GrMain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.GrMain.Name = "GrMain";
            this.GrMain.Size = new System.Drawing.Size(426, 360);
            this.GrMain.TabIndex = 654;
            this.GrMain.Text = "ตารางราคาคงเหลือ";
            // 
            // GridItemPrice
            // 
            this.GridItemPrice.Cursor = System.Windows.Forms.Cursors.Hand;
            this.GridItemPrice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridItemPrice.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            gridLevelNode1.RelationName = "Level1";
            this.GridItemPrice.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.GridItemPrice.Location = new System.Drawing.Point(2, 27);
            this.GridItemPrice.LookAndFeel.SkinName = "Seven Classic";
            this.GridItemPrice.LookAndFeel.UseDefaultLookAndFeel = false;
            this.GridItemPrice.MainView = this.GridViewItemPrice;
            this.GridItemPrice.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.GridItemPrice.Name = "GridItemPrice";
            this.GridItemPrice.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.SpinEdit2,
            this.MemoEdit2,
            this.DateEdit2,
            this.ButtonEdit2,
            this.BtnEdit2,
            this.CheckEdit1});
            this.GridItemPrice.Size = new System.Drawing.Size(422, 331);
            this.GridItemPrice.TabIndex = 652;
            this.GridItemPrice.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GridViewItemPrice});
            // 
            // GridViewItemPrice
            // 
            this.GridViewItemPrice.Appearance.ColumnFilterButton.Font = new System.Drawing.Font("Kanit", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.GridViewItemPrice.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.Black;
            this.GridViewItemPrice.Appearance.ColumnFilterButton.Options.UseFont = true;
            this.GridViewItemPrice.Appearance.ColumnFilterButton.Options.UseForeColor = true;
            this.GridViewItemPrice.Appearance.ColumnFilterButtonActive.Font = new System.Drawing.Font("Kanit", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.GridViewItemPrice.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Black;
            this.GridViewItemPrice.Appearance.ColumnFilterButtonActive.Options.UseFont = true;
            this.GridViewItemPrice.Appearance.ColumnFilterButtonActive.Options.UseForeColor = true;
            this.GridViewItemPrice.Appearance.CustomizationFormHint.Font = new System.Drawing.Font("Kanit", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.GridViewItemPrice.Appearance.CustomizationFormHint.ForeColor = System.Drawing.Color.Black;
            this.GridViewItemPrice.Appearance.CustomizationFormHint.Options.UseFont = true;
            this.GridViewItemPrice.Appearance.CustomizationFormHint.Options.UseForeColor = true;
            this.GridViewItemPrice.Appearance.DetailTip.Font = new System.Drawing.Font("Kanit", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.GridViewItemPrice.Appearance.DetailTip.ForeColor = System.Drawing.Color.Black;
            this.GridViewItemPrice.Appearance.DetailTip.Options.UseFont = true;
            this.GridViewItemPrice.Appearance.DetailTip.Options.UseForeColor = true;
            this.GridViewItemPrice.Appearance.Empty.Font = new System.Drawing.Font("Kanit", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.GridViewItemPrice.Appearance.Empty.ForeColor = System.Drawing.Color.Black;
            this.GridViewItemPrice.Appearance.Empty.Options.UseFont = true;
            this.GridViewItemPrice.Appearance.Empty.Options.UseForeColor = true;
            this.GridViewItemPrice.Appearance.EvenRow.Font = new System.Drawing.Font("Kanit", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.GridViewItemPrice.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black;
            this.GridViewItemPrice.Appearance.EvenRow.Options.UseFont = true;
            this.GridViewItemPrice.Appearance.EvenRow.Options.UseForeColor = true;
            this.GridViewItemPrice.Appearance.FilterCloseButton.Font = new System.Drawing.Font("Kanit", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.GridViewItemPrice.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.Black;
            this.GridViewItemPrice.Appearance.FilterCloseButton.Options.UseFont = true;
            this.GridViewItemPrice.Appearance.FilterCloseButton.Options.UseForeColor = true;
            this.GridViewItemPrice.Appearance.FilterPanel.Font = new System.Drawing.Font("Kanit", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.GridViewItemPrice.Appearance.FilterPanel.ForeColor = System.Drawing.Color.Black;
            this.GridViewItemPrice.Appearance.FilterPanel.Options.UseFont = true;
            this.GridViewItemPrice.Appearance.FilterPanel.Options.UseForeColor = true;
            this.GridViewItemPrice.Appearance.FixedLine.Font = new System.Drawing.Font("Kanit", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.GridViewItemPrice.Appearance.FixedLine.ForeColor = System.Drawing.Color.Black;
            this.GridViewItemPrice.Appearance.FixedLine.Options.UseFont = true;
            this.GridViewItemPrice.Appearance.FixedLine.Options.UseForeColor = true;
            this.GridViewItemPrice.Appearance.FocusedCell.Font = new System.Drawing.Font("Kanit", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.GridViewItemPrice.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black;
            this.GridViewItemPrice.Appearance.FocusedCell.Options.UseFont = true;
            this.GridViewItemPrice.Appearance.FocusedCell.Options.UseForeColor = true;
            this.GridViewItemPrice.Appearance.FocusedRow.Font = new System.Drawing.Font("Kanit", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.GridViewItemPrice.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black;
            this.GridViewItemPrice.Appearance.FocusedRow.Options.UseFont = true;
            this.GridViewItemPrice.Appearance.FocusedRow.Options.UseForeColor = true;
            this.GridViewItemPrice.Appearance.FooterPanel.Font = new System.Drawing.Font("Kanit", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.GridViewItemPrice.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black;
            this.GridViewItemPrice.Appearance.FooterPanel.Options.UseFont = true;
            this.GridViewItemPrice.Appearance.FooterPanel.Options.UseForeColor = true;
            this.GridViewItemPrice.Appearance.FooterPanel.Options.UseTextOptions = true;
            this.GridViewItemPrice.Appearance.FooterPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GridViewItemPrice.Appearance.GroupButton.Font = new System.Drawing.Font("Kanit", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.GridViewItemPrice.Appearance.GroupButton.ForeColor = System.Drawing.Color.Black;
            this.GridViewItemPrice.Appearance.GroupButton.Options.UseFont = true;
            this.GridViewItemPrice.Appearance.GroupButton.Options.UseForeColor = true;
            this.GridViewItemPrice.Appearance.GroupFooter.Font = new System.Drawing.Font("Kanit", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.GridViewItemPrice.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black;
            this.GridViewItemPrice.Appearance.GroupFooter.Options.UseFont = true;
            this.GridViewItemPrice.Appearance.GroupFooter.Options.UseForeColor = true;
            this.GridViewItemPrice.Appearance.GroupPanel.Font = new System.Drawing.Font("Kanit", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.GridViewItemPrice.Appearance.GroupPanel.ForeColor = System.Drawing.Color.Black;
            this.GridViewItemPrice.Appearance.GroupPanel.Options.UseFont = true;
            this.GridViewItemPrice.Appearance.GroupPanel.Options.UseForeColor = true;
            this.GridViewItemPrice.Appearance.GroupRow.Font = new System.Drawing.Font("Kanit", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.GridViewItemPrice.Appearance.GroupRow.ForeColor = System.Drawing.Color.Black;
            this.GridViewItemPrice.Appearance.GroupRow.Options.UseFont = true;
            this.GridViewItemPrice.Appearance.GroupRow.Options.UseForeColor = true;
            this.GridViewItemPrice.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.GridViewItemPrice.Appearance.HeaderPanel.Font = new System.Drawing.Font("Kanit", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.GridViewItemPrice.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black;
            this.GridViewItemPrice.Appearance.HeaderPanel.Options.UseBackColor = true;
            this.GridViewItemPrice.Appearance.HeaderPanel.Options.UseFont = true;
            this.GridViewItemPrice.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.GridViewItemPrice.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.GridViewItemPrice.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GridViewItemPrice.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.GridViewItemPrice.Appearance.HideSelectionRow.Font = new System.Drawing.Font("Kanit", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.GridViewItemPrice.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.Black;
            this.GridViewItemPrice.Appearance.HideSelectionRow.Options.UseFont = true;
            this.GridViewItemPrice.Appearance.HideSelectionRow.Options.UseForeColor = true;
            this.GridViewItemPrice.Appearance.HorzLine.Font = new System.Drawing.Font("Kanit", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.GridViewItemPrice.Appearance.HorzLine.ForeColor = System.Drawing.Color.Black;
            this.GridViewItemPrice.Appearance.HorzLine.Options.UseFont = true;
            this.GridViewItemPrice.Appearance.HorzLine.Options.UseForeColor = true;
            this.GridViewItemPrice.Appearance.HotTrackedRow.Font = new System.Drawing.Font("Kanit", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.GridViewItemPrice.Appearance.HotTrackedRow.ForeColor = System.Drawing.Color.Black;
            this.GridViewItemPrice.Appearance.HotTrackedRow.Options.UseFont = true;
            this.GridViewItemPrice.Appearance.HotTrackedRow.Options.UseForeColor = true;
            this.GridViewItemPrice.Appearance.OddRow.Font = new System.Drawing.Font("Kanit", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.GridViewItemPrice.Appearance.OddRow.ForeColor = System.Drawing.Color.Black;
            this.GridViewItemPrice.Appearance.OddRow.Options.UseFont = true;
            this.GridViewItemPrice.Appearance.OddRow.Options.UseForeColor = true;
            this.GridViewItemPrice.Appearance.Preview.Font = new System.Drawing.Font("Kanit", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.GridViewItemPrice.Appearance.Preview.ForeColor = System.Drawing.Color.Black;
            this.GridViewItemPrice.Appearance.Preview.Options.UseFont = true;
            this.GridViewItemPrice.Appearance.Preview.Options.UseForeColor = true;
            this.GridViewItemPrice.Appearance.Row.Font = new System.Drawing.Font("Kanit", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.GridViewItemPrice.Appearance.Row.ForeColor = System.Drawing.Color.Black;
            this.GridViewItemPrice.Appearance.Row.Options.UseFont = true;
            this.GridViewItemPrice.Appearance.Row.Options.UseForeColor = true;
            this.GridViewItemPrice.Appearance.Row.Options.UseTextOptions = true;
            this.GridViewItemPrice.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GridViewItemPrice.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.GridViewItemPrice.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.GridViewItemPrice.Appearance.RowSeparator.Font = new System.Drawing.Font("Kanit", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.GridViewItemPrice.Appearance.RowSeparator.ForeColor = System.Drawing.Color.Black;
            this.GridViewItemPrice.Appearance.RowSeparator.Options.UseFont = true;
            this.GridViewItemPrice.Appearance.RowSeparator.Options.UseForeColor = true;
            this.GridViewItemPrice.Appearance.SelectedRow.Font = new System.Drawing.Font("Kanit", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.GridViewItemPrice.Appearance.SelectedRow.ForeColor = System.Drawing.Color.Black;
            this.GridViewItemPrice.Appearance.SelectedRow.Options.UseFont = true;
            this.GridViewItemPrice.Appearance.SelectedRow.Options.UseForeColor = true;
            this.GridViewItemPrice.Appearance.TopNewRow.Font = new System.Drawing.Font("Kanit", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.GridViewItemPrice.Appearance.TopNewRow.ForeColor = System.Drawing.Color.Black;
            this.GridViewItemPrice.Appearance.TopNewRow.Options.UseFont = true;
            this.GridViewItemPrice.Appearance.TopNewRow.Options.UseForeColor = true;
            this.GridViewItemPrice.Appearance.VertLine.Font = new System.Drawing.Font("Kanit", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.GridViewItemPrice.Appearance.VertLine.ForeColor = System.Drawing.Color.Black;
            this.GridViewItemPrice.Appearance.VertLine.Options.UseFont = true;
            this.GridViewItemPrice.Appearance.VertLine.Options.UseForeColor = true;
            this.GridViewItemPrice.Appearance.ViewCaption.Font = new System.Drawing.Font("Kanit", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.GridViewItemPrice.Appearance.ViewCaption.ForeColor = System.Drawing.Color.Black;
            this.GridViewItemPrice.Appearance.ViewCaption.Options.UseFont = true;
            this.GridViewItemPrice.Appearance.ViewCaption.Options.UseForeColor = true;
            this.GridViewItemPrice.ColumnPanelRowHeight = 37;
            this.GridViewItemPrice.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colItemPriceId,
            this.colPriceId,
            this.colUnitPrice,
            this.colIsDefault,
            this.colActive,
            this.colEdit});
            this.GridViewItemPrice.DetailHeight = 431;
            this.GridViewItemPrice.FixedLineWidth = 3;
            this.GridViewItemPrice.FooterPanelHeight = 37;
            this.GridViewItemPrice.GridControl = this.GridItemPrice;
            this.GridViewItemPrice.GroupRowHeight = 44;
            this.GridViewItemPrice.Name = "GridViewItemPrice";
            this.GridViewItemPrice.OptionsBehavior.AutoExpandAllGroups = true;
            this.GridViewItemPrice.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.GridViewItemPrice.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.GridViewItemPrice.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;
            this.GridViewItemPrice.OptionsView.RowAutoHeight = true;
            this.GridViewItemPrice.OptionsView.ShowFooter = true;
            this.GridViewItemPrice.OptionsView.ShowGroupPanel = false;
            this.GridViewItemPrice.OptionsView.ShowIndicator = false;
            this.GridViewItemPrice.RowHeight = 44;
            this.GridViewItemPrice.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.GridViewItemPrice_RowStyle);
            this.GridViewItemPrice.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.GridViewItemPrice_CellValueChanging);
            // 
            // colItemPriceId
            // 
            this.colItemPriceId.Caption = "ItemPriceId";
            this.colItemPriceId.FieldName = "ItemPriceId";
            this.colItemPriceId.MaxWidth = 117;
            this.colItemPriceId.MinWidth = 117;
            this.colItemPriceId.Name = "colItemPriceId";
            this.colItemPriceId.OptionsColumn.AllowEdit = false;
            this.colItemPriceId.OptionsColumn.AllowMove = false;
            this.colItemPriceId.Width = 117;
            // 
            // colPriceId
            // 
            this.colPriceId.Caption = "PriceId";
            this.colPriceId.FieldName = "PriceId";
            this.colPriceId.MaxWidth = 117;
            this.colPriceId.MinWidth = 117;
            this.colPriceId.Name = "colPriceId";
            this.colPriceId.OptionsColumn.AllowEdit = false;
            this.colPriceId.OptionsColumn.AllowMove = false;
            this.colPriceId.Width = 117;
            // 
            // colUnitPrice
            // 
            this.colUnitPrice.Caption = "ราคาล่วงหน้า";
            this.colUnitPrice.ColumnEdit = this.SpinEdit2;
            this.colUnitPrice.FieldName = "UnitPrice";
            this.colUnitPrice.MaxWidth = 150;
            this.colUnitPrice.MinWidth = 150;
            this.colUnitPrice.Name = "colUnitPrice";
            this.colUnitPrice.Visible = true;
            this.colUnitPrice.VisibleIndex = 0;
            this.colUnitPrice.Width = 150;
            // 
            // SpinEdit2
            // 
            this.SpinEdit2.AutoHeight = false;
            this.SpinEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.SpinEdit2.DisplayFormat.FormatString = "{0:n2}";
            this.SpinEdit2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.SpinEdit2.EditFormat.FormatString = "{0:n2}";
            this.SpinEdit2.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.SpinEdit2.Mask.EditMask = "n2";
            this.SpinEdit2.Name = "SpinEdit2";
            // 
            // colIsDefault
            // 
            this.colIsDefault.Caption = "IsDefault";
            this.colIsDefault.ColumnEdit = this.CheckEdit1;
            this.colIsDefault.FieldName = "IsDefault";
            this.colIsDefault.MaxWidth = 80;
            this.colIsDefault.MinWidth = 80;
            this.colIsDefault.Name = "colIsDefault";
            this.colIsDefault.Visible = true;
            this.colIsDefault.VisibleIndex = 1;
            this.colIsDefault.Width = 80;
            // 
            // CheckEdit1
            // 
            this.CheckEdit1.AutoHeight = false;
            this.CheckEdit1.Name = "CheckEdit1";
            // 
            // colEdit
            // 
            this.colEdit.Caption = "Edit";
            this.colEdit.ColumnEdit = this.BtnEdit2;
            this.colEdit.MaxWidth = 70;
            this.colEdit.MinWidth = 70;
            this.colEdit.Name = "colEdit";
            this.colEdit.OptionsColumn.AllowMove = false;
            this.colEdit.OptionsColumn.ShowCaption = false;
            this.colEdit.Visible = true;
            this.colEdit.VisibleIndex = 3;
            this.colEdit.Width = 70;
            // 
            // BtnEdit2
            // 
            this.BtnEdit2.AutoHeight = false;
            editorButtonImageOptions1.Image = ((System.Drawing.Image)(resources.GetObject("editorButtonImageOptions1.Image")));
            this.BtnEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.BtnEdit2.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.BtnEdit2.Name = "BtnEdit2";
            this.BtnEdit2.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.BtnEdit2.Click += new System.EventHandler(this.BtnEdit2_Click);
            // 
            // MemoEdit2
            // 
            this.MemoEdit2.Appearance.Options.UseTextOptions = true;
            this.MemoEdit2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.MemoEdit2.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.MemoEdit2.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.MemoEdit2.Name = "MemoEdit2";
            // 
            // DateEdit2
            // 
            this.DateEdit2.AutoHeight = false;
            this.DateEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DateEdit2.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DateEdit2.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.ClassicNew;
            this.DateEdit2.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.DateEdit2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.DateEdit2.EditFormat.FormatString = "dd/MM/yyyy";
            this.DateEdit2.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.DateEdit2.Mask.EditMask = "dd/MM/yyyy";
            this.DateEdit2.Name = "DateEdit2";
            this.DateEdit2.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.False;
            // 
            // ButtonEdit2
            // 
            this.ButtonEdit2.AutoHeight = false;
            editorButtonImageOptions2.Image = ((System.Drawing.Image)(resources.GetObject("editorButtonImageOptions2.Image")));
            this.ButtonEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject5, serializableAppearanceObject6, serializableAppearanceObject7, serializableAppearanceObject8, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.ButtonEdit2.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.ButtonEdit2.Name = "ButtonEdit2";
            this.ButtonEdit2.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // BtnCancel
            // 
            this.BtnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Kanit", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnCancel.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.BtnCancel.Location = new System.Drawing.Point(337, 519);
            this.BtnCancel.LookAndFeel.SkinName = "DevExpress Dark Style";
            this.BtnCancel.LookAndFeel.UseDefaultLookAndFeel = false;
            this.BtnCancel.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(117, 38);
            this.BtnCancel.TabIndex = 655;
            this.BtnCancel.Text = "กลับ";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnSave.Appearance.Font = new System.Drawing.Font("Kanit", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.Appearance.Options.UseFont = true;
            this.BtnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.BtnSave.Location = new System.Drawing.Point(30, 524);
            this.BtnSave.LookAndFeel.SkinName = "DevExpress Dark Style";
            this.BtnSave.LookAndFeel.UseDefaultLookAndFeel = false;
            this.BtnSave.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(117, 38);
            this.BtnSave.TabIndex = 656;
            this.BtnSave.Text = "บันทึก";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // lblPriceName
            // 
            this.lblPriceName.AutoSize = true;
            this.lblPriceName.Font = new System.Drawing.Font("Kanit", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPriceName.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblPriceName.Location = new System.Drawing.Point(26, 20);
            this.lblPriceName.Name = "lblPriceName";
            this.lblPriceName.Size = new System.Drawing.Size(141, 25);
            this.lblPriceName.TabIndex = 657;
            this.lblPriceName.Text = "ตารางราคาล่วงหน้า";
            this.lblPriceName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // colActive
            // 
            this.colActive.Caption = "Active";
            this.colActive.ColumnEdit = this.CheckEdit1;
            this.colActive.FieldName = "Active";
            this.colActive.MaxWidth = 80;
            this.colActive.MinWidth = 80;
            this.colActive.Name = "colActive";
            this.colActive.OptionsColumn.AllowMove = false;
            this.colActive.Visible = true;
            this.colActive.VisibleIndex = 2;
            this.colActive.Width = 80;
            // 
            // FrmAddPrice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 584);
            this.ControlBox = false;
            this.Controls.Add(this.lblPriceName);
            this.Controls.Add(this.BtnSave);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.GrMain);
            this.Controls.Add(this.BtnAddPrice);
            this.Controls.Add(this.lblราคาล่วงหน้า);
            this.Controls.Add(this.SpinUnitPrice);
            this.LookAndFeel.SkinName = "The Bezier";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximumSize = new System.Drawing.Size(494, 614);
            this.MinimumSize = new System.Drawing.Size(494, 614);
            this.Name = "FrmAddPrice";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "เพิ่มราคา";
            this.Load += new System.EventHandler(this.FrmAddPrice_Load);
            ((System.ComponentModel.ISupportInitialize)(this.SpinUnitPrice.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrMain)).EndInit();
            this.GrMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridItemPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewItemPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SpinEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MemoEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateEdit2.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ButtonEdit2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton BtnAddPrice;
        private System.Windows.Forms.Label lblราคาล่วงหน้า;
        private DevExpress.XtraEditors.SpinEdit SpinUnitPrice;
        private DevExpress.XtraEditors.GroupControl GrMain;
        internal DevExpress.XtraGrid.GridControl GridItemPrice;
        internal DevExpress.XtraGrid.Views.Grid.GridView GridViewItemPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colPriceId;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit DateEdit2;
        private DevExpress.XtraGrid.Columns.GridColumn colUnitPrice;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit SpinEdit2;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit MemoEdit2;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit ButtonEdit2;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit BtnEdit2;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit CheckEdit1;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private DevExpress.XtraEditors.SimpleButton BtnSave;
        private DevExpress.XtraGrid.Columns.GridColumn colItemPriceId;
        private DevExpress.XtraGrid.Columns.GridColumn colIsDefault;
        private DevExpress.XtraGrid.Columns.GridColumn colEdit;
        public System.Windows.Forms.Label lblPriceName;
        private DevExpress.XtraGrid.Columns.GridColumn colActive;
    }
}