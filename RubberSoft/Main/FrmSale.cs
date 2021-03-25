using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using System.IO;
using RubberSoft.Data;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraBars.Docking2010.Customization;
using DevExpress.XtraBars.Docking2010.Views.WindowsUI;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.Data;
using DevExpress.XtraEditors.Controls;
namespace RubberSoft.Main
{
    public partial class FrmSale : XtraForm
    {
        public FrmSale()
        {
            InitializeComponent();
        }

        readonly SQLCustomer SQLCustomer = new SQLCustomer();
        readonly SQLBuy SQLBuy = new SQLBuy();
        readonly SQLSale SQLSale = new SQLSale();

        private int sCustomerId, sPriceId, sSaleId, sProductId, RunNo, sLogId, sOutStandingLogId;
        private string sCustomerName, sBuyNumber;
        readonly DataTable dtCustomer = new DataTable();
        DataTable dtSaleProduct = new DataTable();

        //private TimeSpan TimeNow;
        private bool OpenSaveSale;
        private double sSaleDrc;

        private decimal sNetTotal;
        readonly string fileName = Application.StartupPath + @"\SplitSale.xml";

        private void FrmSale_Load(object sender, EventArgs e)
        {
            RefreshForm();
            LoadProducts();
            SaveLayOut();
        }

        private void BtnNew_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("คุณต้องการยกเลิกข้อมูลการขายนี้ ใช่หรือไม่?", "ยืนยัน", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                RefreshForm();
            }
        }

        private void RefreshForm()
        {
            try
            {
                lblUserName.Text = "ยินดีต้อนรับ : " + ClassProperty.permisUserNm;
                TimerMain.Enabled = true;
                DtSaleDate.DateTime = DateTime.Now;
                LkProduct.Enabled = true;
                LkProduct.EditValue = 1;
                sProductId = 1;
                LoadDataProduct(sProductId);
                ClearValues();
                Load_UcCustomerType();

                GridViewSaleProduct.OptionsView.ShowFooter = true;
                GridColumn column = GridViewSaleProduct.Columns["TotalPrice"];
                column.SummaryItem.SummaryType = SummaryItemType.Custom;
                column.SummaryItem.DisplayFormat = "{0:n2}";

                SplitMain.RestoreLayoutFromXml(fileName);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void ClearValues()
        {
            try
            {
                sCustomerId = 0;
                sSaleId = 0;
                OpenSaveSale = false;
                ClearData();
                LoadMaxSaleId();
                GetReceiptNo(DtSaleDate.DateTime);
                GetCustomer(sCustomerId);
                GetSaleProduct(sSaleId);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void ClearData()
        {
            try
            {
                SpinSubTotal.Value = 0;
                SpinDownValue.Value = 0;
                SpinNetTotal.Value = 0;
                SpinSetOffValue.Value = 0;
                SpinValueBalance.Value = 0;

                RefreshValue();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void RefreshValue()
        {
            try
            {
                SpinWeightAmount.Value = 0;
                SpinWeightAmount_Plate.Value = 0;
                SpinDrc.Value = 0;
                SpinTotalPrice_Smoke.Value = 0;
                SpinWeightAmount_Raw.Value = 0;
                SpinTotalPrice_Raw.Value = 0;
                SpinCalRubber.Value = 0;
                SpinTotalPrice.Value = 0;
                TxtRemark.Text = "";

                BtnAddSale.Text = "เพิ่มการขาย";
                RunNo = dtSaleProduct.Rows.Count + 1;

                SpinWeightAmount.Select();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private bool GetCustomer(int id)
        {
            try
            {
                using (var context = new RubberSoftEntities())
                {
                    var query = context.spt_GetCustomer().Where(o => o.CustomerId == id).ToList();
                    if (query.Count > 0)
                    {
                        foreach (spt_GetCustomer_Result dt in query)
                        {
                            TxtCustomerName.Text = dt.CustomerCode + " : " + dt.CustomerName;
                            //sCustomerCode = dt.CustomerCode;
                            sCustomerName = dt.CustomerName;
                            TxtLicensePlate.Text = dt.LicensePlate;
                            TxtPhone.Text = dt.Phone;
                            //sCustomerTypeId = dt.CustomerTypeId.Value;

                            LoadLkCustomerPrices(id);
                            LoadCustomerPrices(id);
                            GetValueBalance(id);
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private void LoadCustomerPrices(int cus_id)
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = SQLSale.GetSaleWeightBalance(cus_id);
                dt = ds.Tables[0];
                GridPrice.DataSource = dt;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private bool GetValueBalance(int cid)
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = SQLSale.GetSaleValueBalance(cid);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow drv in dt.Rows)
                    {
                        sNetTotal = Convert.ToDecimal(drv["NetTotal"]);
                        SpinValueBalance.Value = Convert.ToDecimal(drv["NetTotal"]);
                    }
                }
                else
                {
                    SpinValueBalance.Value = 0;
                }

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool GetReceiptNo(DateTime date)
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = SQLSale.Spt_GetSaleNumber(date);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow drv in dt.Rows)
                    {
                        sBuyNumber = Convert.ToString(drv["ReceiptNo"]);
                        TxtSaleNumber.Text = sBuyNumber;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool LoadMaxSaleId()
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = SQLSale.GetMaxSaleId();
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow drv in dt.Rows)
                    {
                        sSaleId = Convert.ToInt32(drv["SaleId"]);
                    }
                }
                else
                {
                    sSaleId = 1;
                }

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool GetSaleProduct(int BuyId)
        {
            try
            {
                DataSet ds = SQLSale.GetSaleProduct(BuyId);
                dtSaleProduct = ds.Tables[0];

                GridSaleProduct.DataSource = dtSaleProduct;

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private void BtnAddBuy_Click(object sender, EventArgs e)
        {
            if (LkProduct.Enabled == true)
            {
                LkProduct.Enabled = false;
            }

            if (sPriceId == 0)
            {
                XtraMessageBox.Show("กรุณาระบุราคาล่วงหน้า !", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                SLCustomerPrice.ShowPopup();
                return;
            }

            if (BtnAddSale.Text != "แก้ไขการขาย")
            {
                RunNo = dtSaleProduct.Rows.Count + 1;
            }

            if (CheckAdProduct())
            {
                GridViewSaleProduct.UpdateTotalSummary();
                CalculateValue();
            }

            CalEndUpdate();
        }

        private bool CalEndUpdate()
        {
            try
            {
                SLCustomerPrice.EditValue = 0;
                sPriceId = 0;

                decimal sumWeightAmount;
                decimal sumWeightAmount_Plate;
                decimal sumTotalPrice_Raw;
                //Convert.ToInt32(dt.Compute("SUM(Salary)", "EmployeeId > 2"));
                sumTotalPrice_Raw = Convert.ToDecimal(dtSaleProduct.Compute("SUM(TotalPrice_Raw)", string.Empty));
                sumWeightAmount = Convert.ToDecimal(dtSaleProduct.Compute("SUM(WeightAmount)", string.Empty));
                sumWeightAmount_Plate = Convert.ToDecimal(dtSaleProduct.Compute("SUM(WeightAmount_Plate)", string.Empty));

                SpinWeightTotal.Value = (sumWeightAmount - sumWeightAmount_Plate) - sumTotalPrice_Raw;

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private void SpinTotalPrice_Raw_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (LkProduct.Enabled == true)
                {
                    LkProduct.Enabled = false;
                }

                if (sPriceId == 0)
                {
                    XtraMessageBox.Show("กรุณาระบุราคาล่วงหน้า !", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    SLCustomerPrice.ShowPopup();
                    return;
                }

                if (BtnAddSale.Text != "แก้ไขการขาย")
                {
                    RunNo = dtSaleProduct.Rows.Count + 1;
                }

                if (CheckAdProduct())
                {
                    GridViewSaleProduct.UpdateTotalSummary();
                    CalculateValue();
                }

                CalEndUpdate();
            }
        }

        private bool CheckAdProduct()
        {
            try
            {
                double sPercentage, sWeightAmount, sWeightAmount_Plate, sDrc;
                decimal sTotalPrice_Smoke, sWeightAmount_Raw, sTotalPrice_Raw, sCalRubber, sTotalPrice;

                sPercentage = 0;
                sWeightAmount = Convert.ToDouble(SpinWeightAmount.Value);
                sWeightAmount_Plate = Convert.ToDouble(SpinWeightAmount_Plate.Value);
                sDrc = Convert.ToDouble(SpinDrc.Value);

                sTotalPrice_Smoke = SpinTotalPrice_Smoke.Value;
                sWeightAmount_Raw = SpinWeightAmount_Raw.Value;
                sTotalPrice_Raw = SpinTotalPrice_Raw.Value;
                sCalRubber = SpinCalRubber.Value;
                sTotalPrice = SpinTotalPrice.Value;

                if (AddDataSaleProduct(RunNo, 0, sSaleId, sPriceId, sPercentage, sWeightAmount, sWeightAmount_Plate, sDrc,
                    sTotalPrice_Smoke, sWeightAmount_Raw, sTotalPrice_Raw, sCalRubber, sTotalPrice, TxtRemark.Text))
                {
                    if (CalWeightBalanceAmt(GridViewPrice, sPriceId, SpinTotalPrice_Raw.Value, true))
                    {
                        RefreshValue();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool AddDataSaleProduct(int sRunNo, int SaleProductId, int SaleId, int PriceId, double Percentage, double WeightAmount,
            double WeightAmount_Plate, double Drc, decimal TotalPrice_Smoke, decimal WeightAmount_Raw, decimal TotalPrice_Raw,
            decimal CalRubber, decimal TotalPrice, string Remark)

        {
            DataTable dt_Added = dtSaleProduct;
            if (dt_Added == null)
                return false;

            try
            {
                foreach (DataRow drv in dt_Added.Rows)
                {
                    if (sRunNo == Convert.ToInt32(drv["RunNo"]))
                    {
                        drv["PriceId"] = PriceId;
                        drv["Percentage"] = Percentage;
                        drv["WeightAmount"] = WeightAmount;
                        drv["WeightAmount_Plate"] = WeightAmount_Plate;
                        drv["Drc"] = Drc;
                        drv["TotalPrice_Smoke"] = TotalPrice_Smoke;
                        drv["WeightAmount_Raw"] = WeightAmount_Raw;
                        drv["TotalPrice_Raw"] = TotalPrice_Raw;
                        drv["CalRubber"] = CalRubber;
                        drv["TotalPrice"] = TotalPrice;
                        drv["Remark"] = Remark;

                        return true;
                    }

                }

                DataRow dr_New = dt_Added.NewRow();

                dt_Added.BeginInit();

                dr_New["RunNo"] = sRunNo;
                dr_New["SaleProductId"] = SaleProductId;
                dr_New["SaleId"] = SaleId;
                dr_New["PriceId"] = PriceId;
                dr_New["Percentage"] = Percentage;
                dr_New["WeightAmount"] = WeightAmount;
                dr_New["WeightAmount_Plate"] = WeightAmount_Plate;
                dr_New["Drc"] = Drc;
                dr_New["TotalPrice_Smoke"] = TotalPrice_Smoke;
                dr_New["WeightAmount_Raw"] = WeightAmount_Raw;
                dr_New["TotalPrice_Raw"] = TotalPrice_Raw;
                dr_New["CalRubber"] = CalRubber;
                dr_New["TotalPrice"] = TotalPrice;
                dr_New["Remark"] = Remark;

                dt_Added.EndInit();
                dt_Added.Rows.Add(dr_New);

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool CheckData(int id)
        {
            try
            {
                using (var context = new RubberSoftEntities())
                {
                    var query = context.spt_GetSale(true).Where(o => o.SaleId == id).ToList();
                    if (query.Count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private void LkProduct_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit lookUp = sender as LookUpEdit;
            if (lookUp.GetSelectedDataRow() is DataRowView dataRow)
            {
                if (dataRow != null)
                {
                    sProductId = Convert.ToInt32(dataRow["ProductId"]);
                    lblProductName.Text = Convert.ToString(dataRow["ProductName"]);
                    LoadDataProduct(sProductId);
                }
            }
        }

        private void LoadProducts()
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = SQLSale.GetSaleProducts();
                dt = ds.Tables[0];

                LkProduct.Properties.DataSource = dt;
                LkProduct.Properties.DisplayMember = "ProductName";
                LkProduct.Properties.ValueMember = "ProductId";
                LkProduct.Properties.PopulateColumns();
                LkProduct.Properties.Columns["ProductId"].Visible = false;
                LkProduct.EditValue = 3;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void LoadDataProduct(int id)
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = SQLBuy.GetProductById(id);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow drv in dt.Rows)
                    {
                        sSaleDrc = Convert.ToDouble(drv["SaleDrc"]);
                        //sSaleDrc = Convert.ToDouble(drv["SaleDrc"]);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void LoadLkCustomerPrices(int cus_id)
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = SQLCustomer.GetLkCustomerPrices(cus_id);
                dt = ds.Tables[0];

                SLCustomerPrice.Properties.DataSource = dt;
                SLCustomerPrice.Properties.DisplayMember = "SalePriceAdvance";
                SLCustomerPrice.Properties.ValueMember = "PriceId";
                SLCustomerPrice.EditValue = 0;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void Load_UcCustomerType()
        {
            try
            {
                FlyoutDialog flyout_Dialog = new FlyoutDialog(FindForm(), new UcCustomerType());
                flyout_Dialog.Properties.Style = FlyoutStyle.Popup;
                {
                    var frm = flyout_Dialog;
                    if (frm.ShowDialog(this) == DialogResult.OK)
                    {
                        sCustomerId = ClassProperty.sCustomerId;
                        GetCustomer(sCustomerId);
                        GridViewSaleProduct.UpdateTotalSummary();
                        CalculateValue();
                    }
                    else
                    {
                        if (sCustomerId == 0)
                        {
                            Load_UcCustomerType();
                        }
                    }

                    frm.Dispose();
                }

                this.Show();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void CalculateValue()
        {
            try
            {
                decimal WeightAmount, Drc, WeightAmount_Plate, DownValue,
                    TotalPrice_Raw, TotalPrice, NetTotal, ValueBalance, SumTotal, SetOffValue;

                double TotalPrice_Smoke, cutWeightAmount, CalRubber;

                WeightAmount = SpinWeightAmount.Value;
                Drc = SpinDrc.Value;
                WeightAmount_Plate = SpinWeightAmount_Plate.Value;
                TotalPrice_Raw = SpinTotalPrice_Raw.Value;
                ValueBalance = SpinValueBalance.Value;
                DownValue = SpinDownValue.Value;
                SetOffValue = SpinSetOffValue.Value;

                TotalPrice_Smoke = Convert.ToDouble(SpinTotalPrice_Smoke.Value);
                cutWeightAmount = 0;
                CalRubber = Convert.ToDouble(TotalPrice_Smoke * Convert.ToDouble((Drc / 100)) * (sSaleDrc / 100));
                TotalPrice = TotalPrice_Raw * Convert.ToDecimal(CalRubber);
                SumTotal = (ValueBalance - DownValue) + sumSubTotal;
                NetTotal = SumTotal - SetOffValue;

                SpinTotalPrice_Smoke.Value = Convert.ToDecimal(TotalPrice_Smoke);
                SpinWeightAmount_Raw.Value = Convert.ToDecimal(cutWeightAmount);
                SpinCalRubber.Value = Convert.ToDecimal(CalRubber);
                SpinTotalPrice.Value = TotalPrice;
                SpinSubTotal.Value = sumSubTotal;
                SpinNetTotal.Value = NetTotal;

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSaveBuy_Click(object sender, EventArgs e)
        {
            if (GridViewSaleProduct.RowCount <= 0)
            {
                XtraMessageBox.Show("ไม่มีรายการสินค้า", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (OpenSaveSale == false)
            {
                LoadMaxSaleId();
                GetReceiptNo(DtSaleDate.DateTime);
            }

            if (SaveData() == true)
            {
                ClearValues();
                GridViewSaleProduct.UpdateTotalSummary();
                CalculateValue();

                LkProduct.Enabled = true;
                SLCustomerPrice.EditValue = 0;
                sPriceId = 0;
                LoadDataProduct(sProductId);

                Load_UcCustomerType();
            }
        }

        private bool SaveData()
        {
            try
            {
                if (CheckData(sSaleId) == true)
                {
                    if (UpdateSale())
                    {
                        if (SaveDataProduct())
                        {
                            XtraMessageBox.Show("บันทึกข้อมูลการขายสำเร็จ", "สถานะ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else
                {
                    if (AddSale())
                    {
                        if (SaveDataProduct())
                        {
                            XtraMessageBox.Show("เพิ่มข้อมูลการขายสำเร็จ", "สถานะ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool AddSale()
        {
            try
            {
                using (var context = new RubberSoftEntities())
                {
                    var query = context.spt_AddSale(TxtSaleNumber.Text, sCustomerId, sCustomerName, DtSaleDate.DateTime,
                                         SpinSubTotal.Value, SpinDownValue.Value, SpinNetTotal.Value, SpinSetOffValue.Value,
                                         SpinValueBalance.Value, sProductId, true, 
                                         ClassProperty.permisUserID, ClassProperty.StrTerminalId);

                    return true;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool UpdateSale()
        {
            try
            {
                using (var context = new RubberSoftEntities())
                {
                    var query = context.spt_UpdateSale(sSaleId, TxtSaleNumber.Text, sCustomerId, sCustomerName, DtSaleDate.DateTime,
                                         SpinSubTotal.Value, SpinDownValue.Value, SpinNetTotal.Value, SpinSetOffValue.Value,
                                         SpinValueBalance.Value, sProductId, true, ClassProperty.permisUserID);

                    return true;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool SaveDataProduct()
        {
            try
            {
                if (GridViewSaleProduct.RowCount > 0)
                {
                    CheckDataSaleProduct(GridViewSaleProduct);
                }

                SaveTransactionLog(GridViewPrice);

                SaveOutStandingBalance(sCustomerId);

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool AddSaleProduct(int SaleId, int PriceId, decimal Percentage, decimal WeightAmount, decimal WeightAmount_Plate,
            decimal Drc, decimal TotalPrice_Smoke, decimal WeightAmount_Raw, decimal TotalPrice_Raw, decimal CalRubber, decimal TotalPrice,
            string Remark)
        {
            try
            {
                using (var context = new RubberSoftEntities())
                {
                    var query = context.spt_AddSaleProduct(SaleId, PriceId, Percentage, WeightAmount, WeightAmount_Plate,
                        Drc, TotalPrice_Smoke, WeightAmount_Raw, TotalPrice_Raw, CalRubber, TotalPrice,
                        Remark, ClassProperty.permisUserID);

                    return true;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool UpdateSaleProduct(int SaleProductId, int SaleId, int PriceId, decimal Percentage, decimal WeightAmount,
            decimal WeightAmount_Plate, decimal Drc, decimal TotalPrice_Smoke, decimal WeightAmount_Raw, decimal TotalPrice_Raw,
            decimal CalRubber, decimal TotalPrice, string Remark)
        {
            try
            {
                using (var context = new RubberSoftEntities())
                {
                    var query = context.spt_UpdateSaleProduct(SaleProductId, SaleId, PriceId, Percentage, WeightAmount,
                        WeightAmount_Plate, Drc, TotalPrice_Smoke, WeightAmount_Raw, TotalPrice_Raw, CalRubber, TotalPrice,
                        Remark, ClassProperty.permisUserID);

                    return true;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool CheckAddSaleProduct(int SaleProductId, int SaleId, int PriceId, decimal Percentage, decimal WeightAmount,
            decimal WeightAmount_Plate, decimal Drc, decimal TotalPrice_Smoke, decimal WeightAmount_Raw,
            decimal TotalPrice_Raw, decimal CalRubber, decimal TotalPrice, string Remark)
        {
            try
            {

                if (SaleProductId == 0)
                {
                    AddSaleProduct(SaleId, PriceId, Percentage, WeightAmount, WeightAmount_Plate, Drc, TotalPrice_Smoke,
                                  WeightAmount_Raw, TotalPrice_Raw, CalRubber, TotalPrice, Remark);
                }
                else
                {
                    UpdateSaleProduct(SaleProductId, SaleId, PriceId, Percentage, WeightAmount, WeightAmount_Plate, Drc,
                        TotalPrice_Smoke, WeightAmount_Raw, TotalPrice_Raw, CalRubber, TotalPrice, Remark);
                }

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool CheckDataSaleProduct(ColumnView View)
        {
            try
            {

                // Obtain the Price column.  
                GridColumn _colSaleProductId = View.Columns.ColumnByFieldName("SaleProductId");
                GridColumn _colPriceId = View.Columns.ColumnByFieldName("PriceId");
                GridColumn _colPercentage = View.Columns.ColumnByFieldName("Percentage");
                GridColumn _colWeightAmount = View.Columns.ColumnByFieldName("WeightAmount");
                GridColumn _colWeightAmount_Plate = View.Columns.ColumnByFieldName("WeightAmount_Plate");
                GridColumn _colDrc = View.Columns.ColumnByFieldName("Drc");
                GridColumn _colTotalPrice_Smoke = View.Columns.ColumnByFieldName("TotalPrice_Smoke");
                GridColumn _colWeightAmount_Raw = View.Columns.ColumnByFieldName("WeightAmount_Raw");
                GridColumn _colTotalPrice_Raw = View.Columns.ColumnByFieldName("TotalPrice_Raw");
                GridColumn _colCalRubber = View.Columns.ColumnByFieldName("CalRubber");
                GridColumn _colTotalPrice = View.Columns.ColumnByFieldName("TotalPrice");
                GridColumn _colRemark = View.Columns.ColumnByFieldName("Remark");

                if (_colSaleProductId == null)
                    return false;

                View.BeginSort();

                int dataRowCount = View.DataRowCount;
                // Traverse data rows and change the Price field values. 
                for (int i = 0; i < dataRowCount; i++)
                {
                    object SaleProductId = View.GetRowCellValue(i, _colSaleProductId);
                    object PriceId = View.GetRowCellValue(i, _colPriceId);
                    object Percentage = View.GetRowCellValue(i, _colPercentage);
                    object WeightAmount = View.GetRowCellValue(i, _colWeightAmount);
                    object WeightAmount_Plate = View.GetRowCellValue(i, _colWeightAmount_Plate);
                    object Drc = View.GetRowCellValue(i, _colDrc);
                    object TotalPrice_Smoke = View.GetRowCellValue(i, _colTotalPrice_Smoke);
                    object WeightAmount_Raw = View.GetRowCellValue(i, _colWeightAmount_Raw);
                    object TotalPrice_Raw = View.GetRowCellValue(i, _colTotalPrice_Raw);
                    object CalRubber = View.GetRowCellValue(i, _colCalRubber);
                    object TotalPrice = View.GetRowCellValue(i, _colTotalPrice);
                    object Remark = View.GetRowCellValue(i, _colRemark);

                    CheckAddSaleProduct(Convert.ToInt32(SaleProductId), sSaleId, Convert.ToInt32(PriceId), Convert.ToDecimal(Percentage),
                        Convert.ToDecimal(WeightAmount), Convert.ToDecimal(WeightAmount_Plate), Convert.ToDecimal(Drc),
                        Convert.ToDecimal(TotalPrice_Smoke), Convert.ToDecimal(WeightAmount_Raw), Convert.ToDecimal(TotalPrice_Raw),
                        Convert.ToDecimal(CalRubber), Convert.ToDecimal(TotalPrice), Convert.ToString(Remark));

                    //View.SetRowCellValue(i, _colค่าดำเนินการยกมา, ACCquoted);
                }

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                View.EndSort();
            }
        }

        private bool SaveOutStandingBalance(int id)
        {
            try
            {
                if (AddOutStandingLog(SpinNetTotal.Value, sSaleId, 0, 3))
                {
                    LoadOutStandingLogId();
                }

                if (CheckDataOutStandingBalance(id) == false)
                {
                    AddOutStandingBalance(sNetTotal, SpinNetTotal.Value, sOutStandingLogId);
                }
                else
                {
                    UpdateOutStandingBalance(sNetTotal, SpinNetTotal.Value, sOutStandingLogId);
                }

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool CheckDataOutStandingBalance(int id)
        {
            try
            {
                using (var context = new RubberSoftEntities())
                {
                    var query = context.spt_GetOutStandingBalance().Where(o => o.CustomerId == id).ToList();
                    if (query.Count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool AddOutStandingBalance(decimal BeginDebt, decimal OutstandingDebt, int LastLogId)
        {
            try
            {
                using (var context = new RubberSoftEntities())
                {
                    var query = context.spt_AddOutStandingBalance(sCustomerId, BeginDebt, OutstandingDebt, LastLogId);

                    return true;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool UpdateOutStandingBalance(decimal BeginDebt, decimal OutstandingDebt, int LastLogId)
        {
            try
            {
                using (var context = new RubberSoftEntities())
                {
                    var query = context.spt_UpdateOutStandingBalance(sCustomerId, BeginDebt, OutstandingDebt, LastLogId);

                    return true;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool AddOutStandingLog(decimal OutstandingDebt, int RefId, int Status, int LogTypeId)
        {
            try
            {
                using (var context = new RubberSoftEntities())
                {
                    var query = context.spt_AddOutStandingLog(sCustomerId, OutstandingDebt, RefId, Status, LogTypeId);

                    return true;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool CalWeightBalanceAmt(ColumnView View, int refPriceId, decimal TotalPrice_Raw, bool type)
        {
            try
            {
                // Obtain the Price column.  
                GridColumn _colPriceId = View.Columns.ColumnByFieldName("PriceId");
                GridColumn _colSalePriceAdvance = View.Columns.ColumnByFieldName("SalePriceAdvance");
                GridColumn _colWeightAmount = View.Columns.ColumnByFieldName("WeightAmount");
                GridColumn _colDeliveryPrice = View.Columns.ColumnByFieldName("DeliveryPrice");
                GridColumn _colWeightBalanceAmt = View.Columns.ColumnByFieldName("WeightBalanceAmt");
                GridColumn _colBeginAmt = View.Columns.ColumnByFieldName("BeginAmt");

                if (_colPriceId == null)
                    return false;

                View.BeginSort();

                int dataRowCount = View.DataRowCount;
                // Traverse data rows and change the Price field values. 
                for (int i = 0; i < dataRowCount; i++)
                {
                    object PriceId = View.GetRowCellValue(i, _colPriceId);
                    object SalePriceAdvance = View.GetRowCellValue(i, _colSalePriceAdvance);
                    object WeightAmount = View.GetRowCellValue(i, _colWeightAmount);
                    object DeliveryPrice = View.GetRowCellValue(i, _colDeliveryPrice);
                    object WeightBalanceAmt = View.GetRowCellValue(i, _colWeightBalanceAmt);
                    object BeginAmt = View.GetRowCellValue(i, _colBeginAmt);

                    decimal sWeightBalanceAmt;

                    if (Convert.ToInt32(PriceId) == refPriceId)
                    {
                        if (type == true)
                        {
                            sWeightBalanceAmt = Convert.ToDecimal(BeginAmt) - TotalPrice_Raw;

                            View.SetRowCellValue(i, _colWeightBalanceAmt, sWeightBalanceAmt);
                        }

                        if (type == false)
                        {
                            sWeightBalanceAmt = Convert.ToDecimal(WeightBalanceAmt) + TotalPrice_Raw;

                            View.SetRowCellValue(i, _colWeightBalanceAmt, sWeightBalanceAmt);
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                View.EndSort();
            }
        }

        private bool AddTransactionLog(int PriceId, decimal WeightAmount_Raw,
                                       decimal WeightBalanceAmt, int RefId, int LogTypeId)
        {
            try
            {
                using (var context = new RubberSoftEntities())
                {
                    var query = context.spt_AddTransactionLog(sCustomerId, PriceId, WeightAmount_Raw,
                                        WeightBalanceAmt, RefId, LogTypeId);

                    return true;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool CheckDataWeightBalance(int id)
        {
            try
            {
                using (var context = new RubberSoftEntities())
                {
                    var query = context.spt_GetWeightBalance().Where(o => o.PriceId == id).ToList();
                    if (query.Count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool AddWeightBalance(int PriceId, decimal BeginBalanceAmt, decimal WeightBalanceAmt)
        {
            try
            {
                using (var context = new RubberSoftEntities())
                {
                    var query = context.spt_AddWeightBalance(sCustomerId, PriceId, BeginBalanceAmt,
                                        WeightBalanceAmt, sLogId);

                    return true;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool UpdateWeightBalance(int PriceId, decimal BeginBalanceAmt, decimal WeightBalanceAmt)
        {
            try
            {
                using (var context = new RubberSoftEntities())
                {
                    var query = context.spt_UpdateWeightBalance(sCustomerId, PriceId, BeginBalanceAmt,
                                        WeightBalanceAmt, sSaleId);

                    return true;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        public bool SaveTransactionLog(ColumnView View)
        {
            try
            {
                // Obtain the Price column.  
                GridColumn _colPriceId = View.Columns.ColumnByFieldName("PriceId");
                GridColumn _colSalePriceAdvance = View.Columns.ColumnByFieldName("SalePriceAdvance");
                GridColumn _colWeightAmount = View.Columns.ColumnByFieldName("WeightAmount");
                GridColumn _colDeliveryPrice = View.Columns.ColumnByFieldName("DeliveryPrice");
                GridColumn _colWeightBalanceAmt = View.Columns.ColumnByFieldName("WeightBalanceAmt");
                GridColumn _colBeginAmt = View.Columns.ColumnByFieldName("BeginAmt");

                if (_colPriceId == null)
                    return false;

                View.BeginSort();

                int dataRowCount = View.DataRowCount;
                // Traverse data rows and change the Price field values. 
                for (int i = 0; i < dataRowCount; i++)
                {
                    object PriceId = View.GetRowCellValue(i, _colPriceId);
                    object SalePriceAdvance = View.GetRowCellValue(i, _colSalePriceAdvance);
                    object WeightAmount = View.GetRowCellValue(i, _colWeightAmount);
                    object DeliveryPrice = View.GetRowCellValue(i, _colDeliveryPrice);
                    object WeightBalanceAmt = View.GetRowCellValue(i, _colWeightBalanceAmt);
                    object BeginAmt = View.GetRowCellValue(i, _colBeginAmt);

                    decimal useAmt = (Convert.ToDecimal(BeginAmt) - Convert.ToDecimal(WeightBalanceAmt));
                    decimal getAmt = Convert.ToDecimal(WeightBalanceAmt);

                    AddTransactionLog(Convert.ToInt32(PriceId), useAmt, getAmt, sSaleId, 3);

                    LoadTransactionLogId();

                    if (CheckDataWeightBalance(Convert.ToInt32(PriceId)) == false)
                    {
                        AddWeightBalance(Convert.ToInt32(PriceId), Convert.ToDecimal(BeginAmt), getAmt);
                    }
                    else
                    {
                        UpdateWeightBalance(Convert.ToInt32(PriceId), Convert.ToDecimal(BeginAmt), getAmt);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                View.EndSort();
            }
        }

        private bool LoadTransactionLogId()
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = SQLSale.GetMaxTransactionLogId();
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow drv in dt.Rows)
                    {
                        sLogId = Convert.ToInt32(drv["LogId"]);
                    }
                }
               
                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool LoadOutStandingLogId()
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = SQLSale.GetMaxOutStandingLogId();
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow drv in dt.Rows)
                    {
                        sOutStandingLogId = Convert.ToInt32(drv["OutStandingLogId"]);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private void GridViewSaleProduct_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            BtnAddSale.Text = "แก้ไขการขาย";
            GetEditData(GridViewSaleProduct);
        }

        private bool GetEditData(GridView view)
        {
            try
            {
                RunNo = Convert.ToInt32(view.GetFocusedRowCellValue("RunNo"));
                SpinWeightAmount.Value = Convert.ToDecimal(view.GetFocusedRowCellValue("WeightAmount"));
                SpinWeightAmount_Plate.Value = Convert.ToDecimal(view.GetFocusedRowCellValue("WeightAmount_Plate"));
                SpinDrc.Value = Convert.ToDecimal(view.GetFocusedRowCellValue("Drc"));
                SpinTotalPrice_Raw.Value = Convert.ToDecimal(view.GetFocusedRowCellValue("TotalPrice_Raw"));
                SpinCalRubber.Value = Convert.ToDecimal(view.GetFocusedRowCellValue("CalRubber"));
                SLCustomerPrice.EditValue = Convert.ToInt32(view.GetFocusedRowCellValue("PriceId"));
                sPriceId = Convert.ToInt32(view.GetFocusedRowCellValue("PriceId"));
                SpinTotalPrice_Smoke.Value = Convert.ToDecimal(view.GetFocusedRowCellValue("TotalPrice_Smoke"));

                GridViewSaleProduct.UpdateTotalSummary();
                CalculateValue();

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            decimal TotalPriceRaw = Convert.ToDecimal(GridViewSaleProduct.GetFocusedRowCellValue("TotalPrice_Raw"));
            int refPriceId = Convert.ToInt32(GridViewSaleProduct.GetFocusedRowCellValue("PriceId"));
            if (CalWeightBalanceAmt(GridViewPrice, refPriceId, TotalPriceRaw, false))
            {
                GridViewSaleProduct.DeleteSelectedRows();
                GridViewSaleProduct.UpdateTotalSummary();
                CalculateValue();
            }
        }

        private void SpinWeightAmount_TextChanged(object sender, EventArgs e)
        {
            CalculateValue();
        }

        private void SpinWeightAmount_Plate_TextChanged(object sender, EventArgs e)
        {
            CalculateValue();
        }

        private void SpinDrc_TextChanged(object sender, EventArgs e)
        {
            CalculateValue();
        }

        private void SpinTotalPrice_Raw_TextChanged(object sender, EventArgs e)
        {
            CalculateValue();
        }

        private void SpinDownValue_TextChanged(object sender, EventArgs e)
        {
            CalculateValue();
        }

        private void SpinSetOffValue_TextChanged(object sender, EventArgs e)
        {
            CalculateValue();
        }

        private void SLCustomerPrice_EditValueChanged(object sender, EventArgs e)
        {
            SLPriceSelect(GridViewCustomerPrice);
        }

        private bool SLPriceSelect(GridView _view)
        {
            try
            {
                if (_view.SelectedRowsCount > 0)
                {
                    sPriceId = Convert.ToInt32(_view.GetFocusedRowCellValue("PriceId"));
                    //TxtLicensePlate.Text = _view.GetFocusedRowCellValue("PriceId").ToString();
                    SpinTotalPrice_Smoke.Value = Convert.ToDecimal(_view.GetFocusedRowCellValue("SalePriceAdvance"));
                    CalculateValue();
                }

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        decimal sumSubTotal = 0;

        private void GridViewByProduct_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.IsTotalSummary && (e.Item as GridSummaryItem).FieldName == "TotalPrice")
            {
                GridSummaryItem item = e.Item as GridSummaryItem;
                if (item.FieldName == "TotalPrice")
                {
                    switch (e.SummaryProcess)
                    {
                        case CustomSummaryProcess.Start:
                            sumSubTotal = 0;
                            break;
                        case CustomSummaryProcess.Calculate:
                            sumSubTotal += (decimal)e.FieldValue;
                            break;
                        case CustomSummaryProcess.Finalize:
                            e.TotalValue = sumSubTotal;
                            break;
                    }
                }
            }
        }

        private void BtnAddPrice_Click(object sender, EventArgs e)
        {
            Load_UcAddCustomerPrice();
        }

        private void Load_UcAddCustomerPrice()
        {
            try
            {
                FlyoutDialog flyout_Dialog = new FlyoutDialog(FindForm(), new UcAddCustomerPrice());
                flyout_Dialog.Properties.Style = FlyoutStyle.Popup;
                {
                    var frm = flyout_Dialog;
                    if (frm.ShowDialog(this) == DialogResult.OK)
                    {
                        LoadLkCustomerPrices(sCustomerId);
                        LoadCustomerPrices(sCustomerId);
                    }
                    else
                    {

                    }

                    frm.Dispose();
                }
                this.Show();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void BtnGetCustomer_Click(object sender, EventArgs e)
        {
            Load_UcCustomerType();
        }

        private void BtnGetBuy_Click(object sender, EventArgs e)
        {
            Load_UcLoadBuy();
        }

        private void Load_UcLoadBuy()
        {
            try
            {
                FlyoutDialog flyout_Dialog = new FlyoutDialog(FindForm(), new UcLoadBuy());
                flyout_Dialog.Properties.Style = FlyoutStyle.Popup;
                {
                    var frm = flyout_Dialog;
                    if (frm.ShowDialog(this) == DialogResult.OK)
                    {
                        //sCustomerId = ClassProperty.sCustomerId;
                        //GetCustomer(sCustomerId);
                    }
                    else
                    {

                    }

                    frm.Dispose();
                }
                this.Show();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            SaveLayOut();

            //this.FindForm().DialogResult = DialogResult.Cancel;
            FrmMain frm = new FrmMain();
            {
                this.Close();
                frm.Show();
            }
        }

        private bool SaveLayOut()
        {
            try
            {
                if (!File.Exists(fileName))
                {
                    using (FileStream fs = File.Create(fileName))
                    {
                        for (byte i = 0; i < 100; i++)
                        {
                            fs.WriteByte(i);
                        }
                    }
                }

                SplitMain.SaveLayoutToXml(fileName);

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }
    }
}
