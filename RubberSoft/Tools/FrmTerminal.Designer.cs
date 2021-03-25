namespace RubberSoft.Tools
{
    partial class FrmTerminal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTerminal));
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
            this.XtraBottom = new DevExpress.XtraEditors.XtraScrollableControl();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.BtnBack = new DevExpress.XtraEditors.SimpleButton();
            this.GridTerminal = new DevExpress.XtraGrid.GridControl();
            this.GridViewTerminal = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colTerminalName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTerminalId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIPMachine = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMachineName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDownValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.DtDate = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.colActive = new DevExpress.XtraGrid.Columns.GridColumn();
            this.CheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.SpinNumber = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.TxtText = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.BtnEdit = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.BtnDelete = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.XtraBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridTerminal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewTerminal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DtDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DtDate.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SpinNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnDelete)).BeginInit();
            this.SuspendLayout();
            // 
            // XtraBottom
            // 
            this.XtraBottom.Controls.Add(this.BtnSave);
            this.XtraBottom.Controls.Add(this.BtnBack);
            this.XtraBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.XtraBottom.Location = new System.Drawing.Point(0, 412);
            this.XtraBottom.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.XtraBottom.Name = "XtraBottom";
            this.XtraBottom.Size = new System.Drawing.Size(903, 86);
            this.XtraBottom.TabIndex = 46;
            // 
            // BtnSave
            // 
            this.BtnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSave.Appearance.Font = new System.Drawing.Font("Century", 12F);
            this.BtnSave.Appearance.Options.UseFont = true;
            this.BtnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnSave.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BtnSave.ImageOptions.Image")));
            this.BtnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.BtnSave.Location = new System.Drawing.Point(12, 13);
            this.BtnSave.LookAndFeel.SkinName = "VS2010";
            this.BtnSave.LookAndFeel.UseDefaultLookAndFeel = false;
            this.BtnSave.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(125, 56);
            this.BtnSave.TabIndex = 443;
            this.BtnSave.Text = "บันทึก";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnBack
            // 
            this.BtnBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnBack.Appearance.Font = new System.Drawing.Font("Century", 12F);
            this.BtnBack.Appearance.Options.UseFont = true;
            this.BtnBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnBack.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BtnBack.ImageOptions.Image")));
            this.BtnBack.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.BtnBack.Location = new System.Drawing.Point(766, 10);
            this.BtnBack.LookAndFeel.SkinName = "VS2010";
            this.BtnBack.LookAndFeel.UseDefaultLookAndFeel = false;
            this.BtnBack.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BtnBack.Name = "BtnBack";
            this.BtnBack.Size = new System.Drawing.Size(125, 56);
            this.BtnBack.TabIndex = 442;
            this.BtnBack.Text = "ปิดโปรแกรม";
            this.BtnBack.Click += new System.EventHandler(this.BtnBack_Click);
            // 
            // GridTerminal
            // 
            this.GridTerminal.Cursor = System.Windows.Forms.Cursors.Hand;
            this.GridTerminal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridTerminal.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2, 6, 2, 6);
            this.GridTerminal.Font = new System.Drawing.Font("Trebuchet MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            gridLevelNode1.RelationName = "Level1";
            this.GridTerminal.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.GridTerminal.Location = new System.Drawing.Point(0, 0);
            this.GridTerminal.LookAndFeel.SkinName = "Seven Classic";
            this.GridTerminal.LookAndFeel.UseDefaultLookAndFeel = false;
            this.GridTerminal.MainView = this.GridViewTerminal;
            this.GridTerminal.Margin = new System.Windows.Forms.Padding(2, 6, 2, 6);
            this.GridTerminal.Name = "GridTerminal";
            this.GridTerminal.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.SpinNumber,
            this.TxtText,
            this.DtDate,
            this.BtnEdit,
            this.BtnDelete,
            this.CheckEdit1});
            this.GridTerminal.Size = new System.Drawing.Size(903, 412);
            this.GridTerminal.TabIndex = 580;
            this.GridTerminal.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GridViewTerminal});
            // 
            // GridViewTerminal
            // 
            this.GridViewTerminal.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.Black;
            this.GridViewTerminal.Appearance.ColumnFilterButton.Options.UseFont = true;
            this.GridViewTerminal.Appearance.ColumnFilterButton.Options.UseForeColor = true;
            this.GridViewTerminal.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Black;
            this.GridViewTerminal.Appearance.ColumnFilterButtonActive.Options.UseFont = true;
            this.GridViewTerminal.Appearance.ColumnFilterButtonActive.Options.UseForeColor = true;
            this.GridViewTerminal.Appearance.CustomizationFormHint.ForeColor = System.Drawing.Color.Black;
            this.GridViewTerminal.Appearance.CustomizationFormHint.Options.UseFont = true;
            this.GridViewTerminal.Appearance.CustomizationFormHint.Options.UseForeColor = true;
            this.GridViewTerminal.Appearance.DetailTip.ForeColor = System.Drawing.Color.Black;
            this.GridViewTerminal.Appearance.DetailTip.Options.UseFont = true;
            this.GridViewTerminal.Appearance.DetailTip.Options.UseForeColor = true;
            this.GridViewTerminal.Appearance.Empty.ForeColor = System.Drawing.Color.Black;
            this.GridViewTerminal.Appearance.Empty.Options.UseFont = true;
            this.GridViewTerminal.Appearance.Empty.Options.UseForeColor = true;
            this.GridViewTerminal.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black;
            this.GridViewTerminal.Appearance.EvenRow.Options.UseFont = true;
            this.GridViewTerminal.Appearance.EvenRow.Options.UseForeColor = true;
            this.GridViewTerminal.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.Black;
            this.GridViewTerminal.Appearance.FilterCloseButton.Options.UseFont = true;
            this.GridViewTerminal.Appearance.FilterCloseButton.Options.UseForeColor = true;
            this.GridViewTerminal.Appearance.FilterPanel.ForeColor = System.Drawing.Color.Black;
            this.GridViewTerminal.Appearance.FilterPanel.Options.UseFont = true;
            this.GridViewTerminal.Appearance.FilterPanel.Options.UseForeColor = true;
            this.GridViewTerminal.Appearance.FixedLine.ForeColor = System.Drawing.Color.Black;
            this.GridViewTerminal.Appearance.FixedLine.Options.UseFont = true;
            this.GridViewTerminal.Appearance.FixedLine.Options.UseForeColor = true;
            this.GridViewTerminal.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black;
            this.GridViewTerminal.Appearance.FocusedCell.Options.UseFont = true;
            this.GridViewTerminal.Appearance.FocusedCell.Options.UseForeColor = true;
            this.GridViewTerminal.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black;
            this.GridViewTerminal.Appearance.FocusedRow.Options.UseFont = true;
            this.GridViewTerminal.Appearance.FocusedRow.Options.UseForeColor = true;
            this.GridViewTerminal.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black;
            this.GridViewTerminal.Appearance.FooterPanel.Options.UseFont = true;
            this.GridViewTerminal.Appearance.FooterPanel.Options.UseForeColor = true;
            this.GridViewTerminal.Appearance.FooterPanel.Options.UseTextOptions = true;
            this.GridViewTerminal.Appearance.FooterPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GridViewTerminal.Appearance.GroupButton.ForeColor = System.Drawing.Color.Black;
            this.GridViewTerminal.Appearance.GroupButton.Options.UseFont = true;
            this.GridViewTerminal.Appearance.GroupButton.Options.UseForeColor = true;
            this.GridViewTerminal.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black;
            this.GridViewTerminal.Appearance.GroupFooter.Options.UseFont = true;
            this.GridViewTerminal.Appearance.GroupFooter.Options.UseForeColor = true;
            this.GridViewTerminal.Appearance.GroupPanel.ForeColor = System.Drawing.Color.Black;
            this.GridViewTerminal.Appearance.GroupPanel.Options.UseFont = true;
            this.GridViewTerminal.Appearance.GroupPanel.Options.UseForeColor = true;
            this.GridViewTerminal.Appearance.GroupRow.ForeColor = System.Drawing.Color.Black;
            this.GridViewTerminal.Appearance.GroupRow.Options.UseFont = true;
            this.GridViewTerminal.Appearance.GroupRow.Options.UseForeColor = true;
            this.GridViewTerminal.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.GridViewTerminal.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black;
            this.GridViewTerminal.Appearance.HeaderPanel.Options.UseBackColor = true;
            this.GridViewTerminal.Appearance.HeaderPanel.Options.UseFont = true;
            this.GridViewTerminal.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.GridViewTerminal.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.GridViewTerminal.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GridViewTerminal.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.GridViewTerminal.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.Black;
            this.GridViewTerminal.Appearance.HideSelectionRow.Options.UseFont = true;
            this.GridViewTerminal.Appearance.HideSelectionRow.Options.UseForeColor = true;
            this.GridViewTerminal.Appearance.HorzLine.ForeColor = System.Drawing.Color.Black;
            this.GridViewTerminal.Appearance.HorzLine.Options.UseFont = true;
            this.GridViewTerminal.Appearance.HorzLine.Options.UseForeColor = true;
            this.GridViewTerminal.Appearance.HotTrackedRow.ForeColor = System.Drawing.Color.Black;
            this.GridViewTerminal.Appearance.HotTrackedRow.Options.UseFont = true;
            this.GridViewTerminal.Appearance.HotTrackedRow.Options.UseForeColor = true;
            this.GridViewTerminal.Appearance.OddRow.ForeColor = System.Drawing.Color.Black;
            this.GridViewTerminal.Appearance.OddRow.Options.UseFont = true;
            this.GridViewTerminal.Appearance.OddRow.Options.UseForeColor = true;
            this.GridViewTerminal.Appearance.Preview.ForeColor = System.Drawing.Color.Black;
            this.GridViewTerminal.Appearance.Preview.Options.UseFont = true;
            this.GridViewTerminal.Appearance.Preview.Options.UseForeColor = true;
            this.GridViewTerminal.Appearance.Row.ForeColor = System.Drawing.Color.Black;
            this.GridViewTerminal.Appearance.Row.Options.UseFont = true;
            this.GridViewTerminal.Appearance.Row.Options.UseForeColor = true;
            this.GridViewTerminal.Appearance.Row.Options.UseTextOptions = true;
            this.GridViewTerminal.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.GridViewTerminal.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.GridViewTerminal.Appearance.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.GridViewTerminal.Appearance.RowSeparator.ForeColor = System.Drawing.Color.Black;
            this.GridViewTerminal.Appearance.RowSeparator.Options.UseFont = true;
            this.GridViewTerminal.Appearance.RowSeparator.Options.UseForeColor = true;
            this.GridViewTerminal.Appearance.SelectedRow.ForeColor = System.Drawing.Color.Black;
            this.GridViewTerminal.Appearance.SelectedRow.Options.UseFont = true;
            this.GridViewTerminal.Appearance.SelectedRow.Options.UseForeColor = true;
            this.GridViewTerminal.Appearance.TopNewRow.ForeColor = System.Drawing.Color.Black;
            this.GridViewTerminal.Appearance.TopNewRow.Options.UseFont = true;
            this.GridViewTerminal.Appearance.TopNewRow.Options.UseForeColor = true;
            this.GridViewTerminal.Appearance.VertLine.ForeColor = System.Drawing.Color.Black;
            this.GridViewTerminal.Appearance.VertLine.Options.UseFont = true;
            this.GridViewTerminal.Appearance.VertLine.Options.UseForeColor = true;
            this.GridViewTerminal.Appearance.ViewCaption.ForeColor = System.Drawing.Color.Black;
            this.GridViewTerminal.Appearance.ViewCaption.Options.UseFont = true;
            this.GridViewTerminal.Appearance.ViewCaption.Options.UseForeColor = true;
            this.GridViewTerminal.ColumnPanelRowHeight = 37;
            this.GridViewTerminal.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colTerminalName,
            this.colTerminalId,
            this.colIPMachine,
            this.colMachineName,
            this.colDownValue,
            this.colActive});
            this.GridViewTerminal.DetailHeight = 596;
            this.GridViewTerminal.FixedLineWidth = 4;
            this.GridViewTerminal.FooterPanelHeight = 37;
            this.GridViewTerminal.GridControl = this.GridTerminal;
            this.GridViewTerminal.GroupRowHeight = 37;
            this.GridViewTerminal.Name = "GridViewTerminal";
            this.GridViewTerminal.OptionsBehavior.AutoExpandAllGroups = true;
            this.GridViewTerminal.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.GridViewTerminal.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.GridViewTerminal.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;
            this.GridViewTerminal.OptionsView.RowAutoHeight = true;
            this.GridViewTerminal.OptionsView.ShowFooter = true;
            this.GridViewTerminal.OptionsView.ShowGroupPanel = false;
            this.GridViewTerminal.OptionsView.ShowIndicator = false;
            this.GridViewTerminal.RowHeight = 37;
            // 
            // colTerminalName
            // 
            this.colTerminalName.Caption = "TerminalName";
            this.colTerminalName.FieldName = "TerminalName";
            this.colTerminalName.MaxWidth = 200;
            this.colTerminalName.MinWidth = 200;
            this.colTerminalName.Name = "colTerminalName";
            this.colTerminalName.OptionsColumn.AllowEdit = false;
            this.colTerminalName.Visible = true;
            this.colTerminalName.VisibleIndex = 1;
            this.colTerminalName.Width = 200;
            // 
            // colTerminalId
            // 
            this.colTerminalId.AppearanceCell.Options.UseTextOptions = true;
            this.colTerminalId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colTerminalId.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colTerminalId.Caption = "TerminalId";
            this.colTerminalId.FieldName = "TerminalId";
            this.colTerminalId.MaxWidth = 100;
            this.colTerminalId.MinWidth = 100;
            this.colTerminalId.Name = "colTerminalId";
            this.colTerminalId.OptionsColumn.AllowEdit = false;
            this.colTerminalId.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "TerminalId", "{0:n2} : Terminal")});
            this.colTerminalId.Visible = true;
            this.colTerminalId.VisibleIndex = 0;
            this.colTerminalId.Width = 100;
            // 
            // colIPMachine
            // 
            this.colIPMachine.Caption = "IPMachine";
            this.colIPMachine.FieldName = "IPMachine";
            this.colIPMachine.MaxWidth = 150;
            this.colIPMachine.MinWidth = 150;
            this.colIPMachine.Name = "colIPMachine";
            this.colIPMachine.OptionsColumn.AllowEdit = false;
            this.colIPMachine.Visible = true;
            this.colIPMachine.VisibleIndex = 2;
            this.colIPMachine.Width = 150;
            // 
            // colMachineName
            // 
            this.colMachineName.Caption = "MachineName";
            this.colMachineName.FieldName = "MachineName";
            this.colMachineName.MaxWidth = 200;
            this.colMachineName.MinWidth = 200;
            this.colMachineName.Name = "colMachineName";
            this.colMachineName.OptionsColumn.AllowEdit = false;
            this.colMachineName.Visible = true;
            this.colMachineName.VisibleIndex = 3;
            this.colMachineName.Width = 200;
            // 
            // colDownValue
            // 
            this.colDownValue.Caption = "CreateDate";
            this.colDownValue.ColumnEdit = this.DtDate;
            this.colDownValue.FieldName = "CreateDate";
            this.colDownValue.MaxWidth = 150;
            this.colDownValue.MinWidth = 150;
            this.colDownValue.Name = "colDownValue";
            this.colDownValue.OptionsColumn.AllowEdit = false;
            this.colDownValue.Visible = true;
            this.colDownValue.VisibleIndex = 4;
            this.colDownValue.Width = 150;
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
            // colActive
            // 
            this.colActive.Caption = "Active";
            this.colActive.ColumnEdit = this.CheckEdit1;
            this.colActive.FieldName = "Active";
            this.colActive.MaxWidth = 60;
            this.colActive.MinWidth = 60;
            this.colActive.Name = "colActive";
            this.colActive.Visible = true;
            this.colActive.VisibleIndex = 5;
            this.colActive.Width = 60;
            // 
            // CheckEdit1
            // 
            this.CheckEdit1.AutoHeight = false;
            this.CheckEdit1.Name = "CheckEdit1";
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
            // TxtText
            // 
            this.TxtText.Appearance.Options.UseTextOptions = true;
            this.TxtText.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.TxtText.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.TxtText.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.TxtText.Name = "TxtText";
            // 
            // BtnEdit
            // 
            this.BtnEdit.AutoHeight = false;
            editorButtonImageOptions1.Image = ((System.Drawing.Image)(resources.GetObject("editorButtonImageOptions1.Image")));
            this.BtnEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.BtnEdit.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.BtnEdit.Name = "BtnEdit";
            this.BtnEdit.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // BtnDelete
            // 
            this.BtnDelete.AutoHeight = false;
            editorButtonImageOptions2.Image = ((System.Drawing.Image)(resources.GetObject("editorButtonImageOptions2.Image")));
            this.BtnDelete.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject5, serializableAppearanceObject6, serializableAppearanceObject7, serializableAppearanceObject8, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.BtnDelete.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // FrmTerminal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(903, 498);
            this.ControlBox = false;
            this.Controls.Add(this.GridTerminal);
            this.Controls.Add(this.XtraBottom);
            this.LookAndFeel.SkinName = "The Bezier";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FrmTerminal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "เครื่องใช้งาน";
            this.Load += new System.EventHandler(this.FrmTerminal_Load);
            this.XtraBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridTerminal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewTerminal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DtDate.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DtDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SpinNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TxtText)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BtnDelete)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal DevExpress.XtraEditors.XtraScrollableControl XtraBottom;
        internal DevExpress.XtraEditors.SimpleButton BtnBack;
        internal DevExpress.XtraGrid.GridControl GridTerminal;
        internal DevExpress.XtraGrid.Views.Grid.GridView GridViewTerminal;
        private DevExpress.XtraGrid.Columns.GridColumn colTerminalName;
        private DevExpress.XtraGrid.Columns.GridColumn colTerminalId;
        private DevExpress.XtraGrid.Columns.GridColumn colIPMachine;
        private DevExpress.XtraGrid.Columns.GridColumn colMachineName;
        private DevExpress.XtraGrid.Columns.GridColumn colDownValue;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit DtDate;
        private DevExpress.XtraGrid.Columns.GridColumn colActive;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit CheckEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit SpinNumber;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit TxtText;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit BtnEdit;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit BtnDelete;
        internal DevExpress.XtraEditors.SimpleButton BtnSave;
    }
}