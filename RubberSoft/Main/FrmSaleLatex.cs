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
using RubberSoft.Report;
using DevExpress.XtraReports.UI;
using DevExpress.Utils;

namespace RubberSoft.Main
{
    public partial class FrmSaleLatex : XtraForm
    {
        public FrmSaleLatex()
        {
            InitializeComponent();
        }

        readonly SQLCustomer SQLCustomer = new SQLCustomer();
        readonly SQLBuy SQLBuy = new SQLBuy();
        readonly SQLSale SQLSale = new SQLSale();
        readonly SQLReport SQLReport = new SQLReport();
        readonly SQLAuthorized SQLAuthorized = new SQLAuthorized();

        private int sCustomerId, sPriceId, sItemPriceId, sSaveSaleId, sSaleId, ProductId, sProductTypeId, RunNo, sLogId, sOutStandingLogId;
        private string sCustomerName, sSaleNumber, sSaveSaleNumber, sCustomerAddress;
        readonly DataTable dtCustomer = new DataTable();
        DataTable dtSaleProduct = new DataTable();
        DataTable dtPrice = new DataTable();
        DataTable dtTempPrice = new DataTable();
        DataTable dtTempSale = new DataTable();

        public bool OpenSaveSale, IsPrint, PriceDefault;
        private decimal sNetTotal;
        readonly string fileName = Application.StartupPath + @"\SplitSale.xml";

        private void FrmSaleLatex_Load(object sender, EventArgs e)
        {
            RefreshForm();
            LoadProducts(sProductTypeId);
            SaveLayOut();
            GetTempSale();
            GetTempPrice();
        }

        private void RefreshForm()
        {
            try
            {
                sProductTypeId = 2;
                ProductId = 3;
                //LoadLkCustomerGroup();
                LoadLkCustomer(ProductId);
                //SQLCustomer.GetDefaultCustomer();
                //if (ClassProperty.DefaultCustomer != 0)
                //{
                //    sCustomerId = ClassProperty.DefaultCustomer;
                //}
                //else
                //{
                //    sCustomerId = -1;
                //}

                //sCustomerId = -1;
                //SLCustomer.EditValue = sCustomerId;
                sSaveSaleId = 0;
                lblUserName.Text = "ยินดีต้อนรับ : " + ClassProperty.permisUserNm;
                TimerMain.Enabled = true;
                DtSaleDate.DateTime = DateTime.Now;
                LkProduct.Enabled = true;
                LkProduct.EditValue = 3;
                
                LoadProductType(ProductId);
                LoadDataProduct(ProductId);
                ClearValues();
                //Load_UcCustomerType();

                GridViewSaleProduct.OptionsView.ShowFooter = true;
                GridColumn colTotalPrice = GridViewSaleProduct.Columns["TotalPrice"];
                colTotalPrice.SummaryItem.SummaryType = SummaryItemType.Custom;
                colTotalPrice.SummaryItem.DisplayFormat = "{0:n2}";

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
                SpinWeightTotal.Value = 0;
                SpinPaynet.Value = 0;

                TxtCustomerName.Text = "";
                sCustomerName = "";
                TxtLicensePlate.Text = "";
                TxtPhone.Text = "";
                TxtCustomerAddress.Text = "";

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
                SpinPercentage.Value = 0;
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

        private bool GetCustomer(int CustomerId)
        {
            try
            {
                using (var context = new RubberSoftEntities())
                {
                    var query = context.spt_GetCustomer().Where(o => o.CustomerId == CustomerId).ToList();
                    if (query.Count > 0)
                    {
                        foreach (spt_GetCustomer_Result dt in query)
                        {
                            TxtCustomerName.Text = dt.CustomerCode + " : " + dt.CustomerName;
                            //sCustomerCode = dt.CustomerCode;
                            sCustomerName = dt.CustomerName;
                            TxtLicensePlate.Text = dt.LicensePlate;
                            TxtPhone.Text = dt.Phone;
                            sCustomerAddress = dt.CustomerAddress;
                            TxtCustomerAddress.Text = dt.CustomerAddress;

                            LoadLkCustomerPrices(CustomerId);
                            sPriceId = SQLCustomer.GetDefaultCustomerPrice(CustomerId);
                            SLCustomerPrice.EditValue = sPriceId;
                            //GetItemPrice(sPriceId);
                            //SpinTotalPrice_Smoke.Value = Convert.ToDecimal(LkItemPrice.Text);
                            LoadCustomerPrices(CustomerId);
                            GetValueBalance(CustomerId);
                        }
                    }
                    else
                    {
                        ClearData();
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

        //private void LoadLkCustomerGroup()
        //{
        //    try
        //    {
        //        DataTable dt = new DataTable();
        //        DataSet ds = SQLCustomer.Spt_GetCustomerGroup(0);
        //        dt = ds.Tables[0];

        //        LkCustomerGroup.Properties.DataSource = dt;
        //        LkCustomerGroup.Properties.DisplayMember = "CustomerGroupName";
        //        LkCustomerGroup.Properties.ValueMember = "CustomerGroupId";
        //        LkCustomerGroup.Properties.PopulateColumns();
        //        LkCustomerGroup.Properties.Columns["CustomerGroupId"].Visible = false;
        //        LkCustomerGroup.EditValue = 0;
        //    }
        //    catch (Exception ex)
        //    {
        //        XtraMessageBox.Show(ex.Message);
        //    }
        //}

        private void LoadLkCustomer(int CustomerGroupId)
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = SQLSale.GetLKSaleCustomer(CustomerGroupId);
                dt = ds.Tables[0];

                SLCustomer.Properties.DataSource = dt;
                SLCustomer.Properties.DisplayMember = "CustomerName";
                SLCustomer.Properties.ValueMember = "CustomerId";
                SLCustomer.EditValue = null;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void LkCustomerGroup_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit lookUp = sender as LookUpEdit;
            if (lookUp.GetSelectedDataRow() is DataRowView dataRow)
            {
                if (dataRow != null)
                {
                    //CustomerGroupId = Convert.ToInt32(dataRow["CustomerGroupId"]);
                    //LoadLkCustomer(CustomerGroupId);
                }
            }
        }

        private void LoadCustomerPrices(int cus_id)
        {
            try
            {
                DataSet ds = SQLSale.Spt_GetSaleWeightBalance(cus_id);
                dtPrice = ds.Tables[0];
                GridPrice.DataSource = dtPrice;
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
                DataSet ds = SQLBuy.Spt_GetBuyValueBalance(cid);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow drv in dt.Rows)
                    {
                        sNetTotal = Convert.ToDecimal(drv["NetTotal"]);
                        //SpinValueBalance.Value = Convert.ToDecimal(drv["NetTotal"]);
                        SpinValueBalance.Value = 0;
                    }
                }
                else
                {
                    SpinValueBalance.Value = 0;
                    sNetTotal = 0;
                }

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        //private bool GetItemPrice(int PriceId)
        //{
        //    try
        //    {
        //        DataTable dt = new DataTable();
        //        DataSet ds = SQLCustomer.Spt_GetItemPrice(PriceId);
        //        dt = ds.Tables[0];
        //        LkItemPrice.Properties.DataSource = dt;
        //        LkItemPrice.Properties.DisplayMember = "sUnitPrice";
        //        LkItemPrice.Properties.ValueMember = "ItemPriceId";
        //        LkItemPrice.Properties.PopulateColumns();
        //        LkItemPrice.Properties.AppearanceDropDown.TextOptions.HAlignment = HorzAlignment.Center;
        //        LkItemPrice.Properties.Columns["ItemPriceId"].Visible = false;
        //        LkItemPrice.Properties.Columns["PriceId"].Visible = false;
        //        LkItemPrice.Properties.Columns["UnitPrice"].Visible = false;
        //        LkItemPrice.Properties.Columns["Active"].Visible = false;
        //        LkItemPrice.Properties.Columns["IsNew"].Visible = false;
        //        LkItemPrice.Properties.Columns["sUnitPrice"].Caption = "ราคาล่วงหน้า";
        //        LkItemPrice.Properties.Columns["sUnitPrice"].Alignment = HorzAlignment.Center;
        //        LkItemPrice.Properties.Columns["IsDefault"].Alignment = HorzAlignment.Center;
        //        LkItemPrice.Properties.Columns["sUnitPrice"].Width = 100;
        //        LkItemPrice.Properties.Columns["IsDefault"].Width = 80;
        //        LkItemPrice.EditValue = SQLCustomer.GetDefaultItemPrice(PriceId);

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        XtraMessageBox.Show(ex.Message);
        //        return false;
        //    }
        //}

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
                        sSaleNumber = Convert.ToString(drv["ReceiptNo"]);
                        TxtSaleNumber.Text = sSaleNumber;
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

        private void GetTempSale()
        {
            try
            {
                DataSet ds = SQLSale.Spt_GetTempSale();
                dtTempSale = ds.Tables[0];
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void GetTempPrice()
        {
            try
            {
                DataSet ds = SQLBuy.Spt_GetTempWeightBalance();
                dtTempPrice = ds.Tables[0];
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void LkProduct_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit lookUp = sender as LookUpEdit;
            if (lookUp.GetSelectedDataRow() is DataRowView dataRow)
            {
                if (dataRow != null)
                {
                    ProductId = Convert.ToInt32(dataRow["ProductId"]);
                    sProductTypeId = Convert.ToInt32(dataRow["ProductTypeId"]);
                    lblProductName.Text = Convert.ToString(dataRow["ProductName"]);
                    LoadProductType(ProductId);
                    LoadDataProduct(ProductId);
                    LoadLkCustomer(ProductId);
                }
            }
        }

        private void LoadProducts(int ProductTypeId)
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = SQLSale.GetLkProducts(ProductTypeId);
                dt = ds.Tables[0];

                LkProduct.Properties.DataSource = dt;
                LkProduct.Properties.DisplayMember = "ProductName";
                LkProduct.Properties.ValueMember = "ProductId";
                LkProduct.Properties.PopulateColumns();
                LkProduct.Properties.Columns["ProductId"].Visible = false;
                LkProduct.Properties.Columns["ProductTypeId"].Visible = false;
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
                        SpinBuyDrc.Value = Convert.ToDecimal(drv["SaleDrc"]);
                        //sSaleDrc = Convert.ToDouble(drv["SaleDrc"]);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void LoadProductType(int id)
        {
            try
            {
                if (id == 3)
                {
                    lblน้ำหนักPlate.Visible = false;
                    SpinWeightAmount_Plate.Visible = false;
                    lblน้ำหนักสุทธิ.Visible = false;
                    SpinWeightTotal.Visible = false;
                    lblราคา.Text = "ราคา";
                    lblยางแห้ง.Text = "ยางแห้ง";
                    lblยางแห้ง.Visible = true;
                    lblราคายางแห้ง.Visible = true;
                    lblราคายางดิบ.Visible = false;
                    SpinCalRubber.Visible = false;
                    SpinWeightAmount_Raw.Visible = true;
                    lblPercentage.Visible = false;
                    SpinPercentage.Visible = false;
                    lblDrc.Visible = true;
                    SpinDrc.Visible = true;
                    SpinDrc.Value = 0;
                    lblTotalPrice_Smoke.Visible = false;
                    SpinTotalPrice_Smoke.Visible = false;
                    colPriceName.Visible = false;
                    colWeightAmount_Plate.Visible = false;
                    colCalRubber.Visible = false;
                    colยอดยางแห้ง.Visible = true;
                    colยอดยางแห้ง.Caption = "ยางแห้ง";
                    colราคา.Caption = "ราคา";
                    lblค่าลง.Visible = false;
                    SpinDownValue.Visible = false;
                    lblราคาล่วงหน้า.Visible = false;
                    SLCustomerPrice.Visible = false;
                    LkItemPrice.Visible = false;
                    CboProductUsing.Visible = true;

                    BtnAddPrice.Visible = false;
                    GridPrice.Visible = false;
                }
                else
                {
                    lblน้ำหนักPlate.Visible = true;
                    SpinWeightAmount_Plate.Visible = true;
                    lblน้ำหนักสุทธิ.Visible = true;
                    SpinWeightTotal.Visible = true;
                    lblราคา.Text = "ยางดิบ";
                    lblยางแห้ง.Text = "ราคายางรม";
                    lblราคายางดิบ.Visible = true;
                    SpinCalRubber.Visible = true;
                    lblราคายางแห้ง.Visible = false;
                    SpinWeightAmount_Raw.Visible = false;
                    lblPercentage.Visible = true;
                    SpinPercentage.Visible = true;
                    lblDrc.Visible = false;
                    SpinDrc.Visible = false;                  
                    lblTotalPrice_Smoke.Visible = true;
                    SpinTotalPrice_Smoke.Visible = true;
                    SpinPercentage.Value = 0;
                    colPriceName.Visible = true;
                    colWeightAmount_Plate.Visible = true;
                    colยางแห้ง.Caption = "ราคายางรม";
                    colยอดยางแห้ง.Visible = false;
                    colราคา.Caption = "ยางดิบ";
                    colCalRubber.Visible = true;
                    colWeightAmount_Plate.VisibleIndex = 1;
                    colCalRubber.VisibleIndex = 5;
                    lblค่าลง.Visible = true;
                    SpinDownValue.Visible = true;
                    lblราคาล่วงหน้า.Visible = true;
                    SLCustomerPrice.Visible = true;
                    LkItemPrice.Visible = false;
                    CboProductUsing.Visible = false;

                    BtnAddPrice.Visible = true;
                    GridPrice.Visible = true;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void SpinTotalPrice_Raw_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                AddSaleLatex();
            }
        }

        private void BtnAddBuy_Click(object sender, EventArgs e)
        {
            AddSaleLatex();
        }

        private bool AddSaleLatex()
        {
            try
            {
                if (SLCustomer.EditValue == null || sCustomerId == 0)
                {
                    XtraMessageBox.Show("กรุณาระบุลูกค้า !", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    SLCustomer.ShowPopup();
                    return false;
                }

                if (LkProduct.Enabled == true)
                {
                    LkProduct.Enabled = false;
                }

                //if (SpinWeightAmount.Value <= 0)
                //{
                //    XtraMessageBox.Show("กรุณาระบุน้ำหนัก !", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    SpinWeightAmount.Focus();
                //    return false;
                //}

                if (ProductId == 4)
                {
                    if (sPriceId == 0)
                    {
                        XtraMessageBox.Show("กรุณาระบุราคาล่วงหน้า !", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        SLCustomerPrice.ShowPopup();
                        return false;
                    }
                }

                //decimal sPrice = SpinTotalPrice_Raw.Value - SpinWeightAmount_Plate.Value;

                if (CheckBalancePrice(sPriceId, SpinTotalPrice_Raw.Value) == true)
                {
                    if (BtnAddSale.Text != "แก้ไขการขาย")
                    {
                        int maxLevel = 0;
                        //int minLevel = int.MinValue;
                        foreach (DataRow dr in dtSaleProduct.Rows)
                        {
                            int tLevel = dr.Field<int>("RunNo");
                            maxLevel = Math.Max(maxLevel, tLevel);
                            //minLevel = Math.Min(minLevel, tLevel);
                        }

                        RunNo = maxLevel + 1;
                    }

                    if (CheckAdProduct())
                    {
                        GridViewSaleProduct.UpdateTotalSummary();
                    }

                    SLCustomerPrice.EditValue = null;
                    sPriceId = 0;
                    SpinWeightAmount.Focus();

                    RefreshValue();
                    CalEndUpdate();
                }
                else
                {
                    XtraMessageBox.Show("น้ำหนักคงเหลือ มีมูลค่าไม่เพียงพอ !", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    CheckValidated();
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool CalEndUpdate()
        {
            try
            {
                decimal sumWeightAmount = SpinWeightAmount.Value;
                decimal sumWeightAmount_Plate = SpinWeightAmount_Plate.Value;
                decimal sumTotalPrice_Raw = SpinTotalPrice_Raw.Value;

                if (dtSaleProduct.Rows.Count > 1)
                {
                    sumTotalPrice_Raw += Convert.ToDecimal(dtSaleProduct.Compute("SUM(TotalPrice_Raw)", "RunNo <> " + RunNo + ""));
                    sumWeightAmount += Convert.ToDecimal(dtSaleProduct.Compute("SUM(WeightAmount)", "RunNo <> " + RunNo + ""));
                    sumWeightAmount_Plate += Convert.ToDecimal(dtSaleProduct.Compute("SUM(WeightAmount_Plate)", "RunNo <> " + RunNo + ""));
                }

                SpinWeightTotal.Value = (sumWeightAmount - sumWeightAmount_Plate) - sumTotalPrice_Raw;
                //sumTotalPrice_Raw = Convert.ToDecimal(dtSaleProduct.Compute("SUM(TotalPrice_Raw)", string.Empty));

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool CheckBalancePrice(int PriceId, decimal Raw)
        {
            try
            {
                int CkPriceId;
                decimal TotalPrice_Raw;
                decimal TotalRaw = Raw;
                bool IsDefault;

                if (dtSaleProduct.Rows.Count > 0)
                {
                    foreach (DataRow drv in dtSaleProduct.Rows)
                    {
                        if (Convert.ToInt32(drv["RunNo"]) != RunNo)
                        {
                            CkPriceId = Convert.ToInt32(drv["PriceId"]);
                            TotalPrice_Raw = Convert.ToDecimal(drv["TotalPrice_Raw"]);
                            if (PriceId == CkPriceId)
                            {
                                TotalRaw = +TotalPrice_Raw;
                            }
                        }
                    }
                }

                int CekPriceId;
                decimal BeginAmt;

                if (dtPrice.Rows.Count > 0)
                {
                    foreach (DataRow drv in dtPrice.Rows)
                    {
                        CekPriceId = Convert.ToInt32(drv["PriceId"]);
                        BeginAmt = Convert.ToDecimal(drv["BeginAmt"]);
                        IsDefault = Convert.ToBoolean(drv["IsDefault"]);
                        if (IsDefault != true)
                        {
                            if (PriceId == CekPriceId)
                            {
                                if (TotalRaw > BeginAmt)
                                {
                                    return false;
                                }
                            }
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

        private bool CheckValidated()
        {
            try
            {
                SpinTotalPrice_Raw.ErrorText = "น้ำหนักคงเหลือ มีมูลค่าไม่เพียงพอ !";
                SpinTotalPrice_Raw.ErrorImageOptions.Image = DevExpress.Images.ImageResourceCache.Default.GetImage("images/status/warning_16x16.png");

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool CheckAdProduct()
        {

            try
            {
                string sPriceName;
                double sPercentage, sWeightAmount, sWeightAmount_Plate, sDrc;
                decimal sTotalPrice_Smoke, sWeightAmount_Raw, sTotalPrice_Raw, sCalRubber, sTotalPrice;

                sPercentage = Convert.ToDouble(SpinPercentage.Value);
                sWeightAmount = Convert.ToDouble(SpinWeightAmount.Value);
                sWeightAmount_Plate = Convert.ToDouble(SpinWeightAmount_Plate.Value);
                sDrc = Convert.ToDouble(SpinDrc.Value);

                sTotalPrice_Smoke = SpinTotalPrice_Smoke.Value;
                sWeightAmount_Raw = SpinWeightAmount_Raw.Value;
                sTotalPrice_Raw = SpinTotalPrice_Raw.Value;
                sCalRubber = SpinCalRubber.Value;
                sTotalPrice = SpinTotalPrice.Value;
                sPriceName = SLCustomerPrice.Text;

                if (AddDataSaleProduct(RunNo, 0, sSaleId, sPriceId, sItemPriceId, sPriceName, sPercentage, sWeightAmount, sWeightAmount_Plate, sDrc,
                    sTotalPrice_Smoke, sWeightAmount_Raw, sTotalPrice_Raw, sCalRubber, sTotalPrice, TxtRemark.Text, PriceDefault))
                {
                    if (ProductId == 4)
                    {
                        if (dtSaleProduct.Rows.Count > 0)
                        {
                            if (CalWeightBalanceAmt(GridViewPrice, sPriceId, SpinTotalPrice_Raw.Value, true))
                            {
                                //RefreshValue();
                            }
                        }
                    }
                    else
                    {
                        //RefreshValue();
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

        private bool AddDataSaleProduct(int sRunNo, int SaleProductId, int SaleId, int PriceId, int ItemPriceId, string PriceName, double Percentage, double WeightAmount,
            double WeightAmount_Plate, double Drc, decimal TotalPrice_Smoke, decimal WeightAmount_Raw, decimal TotalPrice_Raw,
            decimal CalRubber, decimal TotalPrice, string Remark, bool IsDefault)

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
                        drv["ItemPriceId"] = ItemPriceId;
                        drv["PriceName"] = PriceName;
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
                        drv["IsDefault"] = IsDefault;

                        return true;
                    }

                }

                DataRow dr_New = dt_Added.NewRow();

                dt_Added.BeginInit();

                dr_New["RunNo"] = sRunNo;
                dr_New["SaleProductId"] = SaleProductId;
                dr_New["SaleId"] = SaleId;
                dr_New["PriceId"] = PriceId;
                dr_New["ItemPriceId"] = ItemPriceId;
                dr_New["PriceName"] = PriceName;
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
                dr_New["IsDefault"] = IsDefault;

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

        private bool CalWeightBalanceAmt(ColumnView View, int refPriceId, decimal TotalPrice_Raw, bool type)
        {
            try
            {
                decimal TotalRaw = 0;
                // Obtain the Price column.  
                GridColumn _colPriceId = View.Columns.ColumnByFieldName("PriceId");
                GridColumn _colSalePriceAdvance = View.Columns.ColumnByFieldName("SalePriceAdvance");
                GridColumn _colWeightAmount = View.Columns.ColumnByFieldName("WeightAmount");
                GridColumn _colDeliveryPrice = View.Columns.ColumnByFieldName("DeliveryPrice");
                GridColumn _colWeightBalanceAmt = View.Columns.ColumnByFieldName("WeightBalanceAmt");
                GridColumn _colBeginAmt = View.Columns.ColumnByFieldName("BeginAmt");
                GridColumn _colIsDefault = View.Columns.ColumnByFieldName("IsDefault");

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
                    object IsDefault = View.GetRowCellValue(i, _colIsDefault);
                    decimal sWeightAmount, BalanceAmt;
                    sWeightAmount = Convert.ToDecimal(BeginAmt);

                    if (Convert.ToBoolean(IsDefault) != true)
                    {
                        if (Convert.ToInt32(PriceId) == refPriceId)
                        {
                            if (type == true)
                            {
                                TotalRaw += TotalPrice_Raw;
                                BalanceAmt = Convert.ToDecimal(sWeightAmount) - TotalRaw;

                                View.SetRowCellValue(i, _colWeightBalanceAmt, BalanceAmt);
                            }

                            if (type == false)
                            {
                                TotalRaw += TotalPrice_Raw;
                                BalanceAmt = Convert.ToDecimal(sWeightAmount) + TotalRaw;

                                View.SetRowCellValue(i, _colWeightBalanceAmt, BalanceAmt);
                            }
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

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            int refSaleProductId = Convert.ToInt32(GridViewSaleProduct.GetFocusedRowCellValue("SaleProductId"));
            if (DeleteDataBuyProduct(refSaleProductId))
            {
                CalEndUpdate();
                SLCustomerPrice.EditValue = null;
                sPriceId = 0;
            }
        }

        private bool DeleteDataBuyProduct(int SaleId)
        {
            try
            {
                decimal TotalPriceRaw = Convert.ToDecimal(GridViewSaleProduct.GetFocusedRowCellValue("TotalPrice_Raw"));
                int refPriceId = Convert.ToInt32(GridViewSaleProduct.GetFocusedRowCellValue("PriceId"));

                if (SaleId != 0)
                {
                    if (XtraMessageBox.Show("คุณต้องการยกเลิกรายการขายนี้ ใช่หรือไม่?", "ยืนยัน", MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Warning) == DialogResult.OK)
                    {
                        if (SQLSale.DeleteSaleProduct(SaleId))
                        {
                            if (CalWeightBalanceAmt(GridViewPrice, refPriceId, TotalPriceRaw, false))
                            {
                                RefreshValue();
                                GridViewSaleProduct.DeleteSelectedRows();
                                GridViewSaleProduct.UpdateTotalSummary();
                                CalculateValue();
                            }
                        }
                    }
                }
                else
                {
                    if (CalWeightBalanceAmt(GridViewPrice, refPriceId, TotalPriceRaw, false))
                    {
                        RefreshValue();
                        GridViewSaleProduct.DeleteSelectedRows();
                        GridViewSaleProduct.UpdateTotalSummary();
                        CalculateValue();
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

        private bool GetSaleProduct(int SaleId)
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = SQLSale.Spt_GetSaleProduct(SaleId);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow drv in dt.Rows)
                    {
                        RunNo = dtSaleProduct.Rows.Count + 1;

                        if (AddDataSaleProduct(RunNo, Convert.ToInt32(drv["SaleProductId"]), sSaleId,
                            Convert.ToInt32(drv["PriceId"]), Convert.ToInt32(drv["ItemPriceId"]),
                            Convert.ToString(drv["PriceName"]), Convert.ToDouble(drv["Percentage"]),
                            Convert.ToDouble(drv["WeightAmount"]), Convert.ToDouble(drv["WeightAmount_Plate"]),
                            Convert.ToDouble(drv["Drc"]), Convert.ToDecimal(drv["TotalPrice_Smoke"]),
                            Convert.ToDecimal(drv["WeightAmount_Raw"]), Convert.ToDecimal(drv["TotalPrice_Raw"]),
                            Convert.ToDecimal(drv["CalRubber"]), Convert.ToDecimal(drv["TotalPrice"]),
                            Convert.ToString(drv["Remark"]), Convert.ToBoolean(drv["IsDefault"])))
                        {
                            if (ProductId == 4)
                            {
                                if (CalWeightBalanceAmt(GridViewPrice, Convert.ToInt32(drv["PriceId"]), Convert.ToDecimal(drv["TotalPrice_Raw"]), true))
                                {
                                    RefreshValue();
                                    GridViewSaleProduct.UpdateTotalSummary();
                                    CalculateValue();
                                    CalEndUpdate();
                                }
                            }
                            else
                            {
                                RefreshValue();
                                GridViewSaleProduct.UpdateTotalSummary();
                                CalculateValue();
                                CalEndUpdate();
                            }
                        }
                    }
                }
                else
                {
                    dtSaleProduct = dt;
                }

                GridSaleProduct.DataSource = dtSaleProduct;

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool CheckData(int SaleId)
        {
            try
            {
                using (var context = new RubberSoftEntities())
                {
                    var query = context.spt_GetSale().Where(o => o.SaleId == SaleId).ToList();
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

        private bool ClearSave()
        {
            try
            {
                IsPrint = false;
                if (ClassProperty.DefaultCustomer != 0)
                {
                    sCustomerId = ClassProperty.DefaultCustomer;
                }
                else
                {
                    sCustomerId = 0;
                }

                SLCustomer.EditValue = null;
                //SLCustomer.EditValue = null;
                //sCustomerId = -1;
                ClearValues();
                GridViewSaleProduct.UpdateTotalSummary();
                CalculateValue();

                LkProduct.Enabled = true;
                SLCustomerPrice.EditValue = 0;
                sPriceId = 0;
                LoadProductType(ProductId);
                LoadDataProduct(ProductId);
                LoadCustomerPrices(sCustomerId);
                GetValueBalance(sCustomerId);

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private void Payment()
        {
            try
            {
                if (GridViewSaleProduct.RowCount <= 0)
                {
                    XtraMessageBox.Show("ไม่มีรายการสินค้า", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (ProductId == 4)
                {
                    if (SpinWeightTotal.Value < 0)
                    {
                        XtraMessageBox.Show("มูลค่ายางดิบมากกว่าน้ำหนักยางดิบ", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        SpinWeightTotal.Focus();
                        return;
                    }

                    if (SpinWeightTotal.Value > 0)
                    {
                        XtraMessageBox.Show("น้ำหนักยางดิบคงเหลือ " + SpinWeightTotal.Text, "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        SpinWeightTotal.Focus();
                        return;
                    }
                }

                dtTempSale.Rows.Clear();
                dtTempPrice.Rows.Clear();

                foreach (DataRow drv in dtSaleProduct.Rows)
                {
                    AddTempBuy(sSaleId, sSaleNumber, DtSaleDate.DateTime, sCustomerName, sCustomerAddress, TxtLicensePlate.Text,
                        TxtPhone.Text, SpinPaynet.Value, SpinValueBalance.Value, SpinSubTotal.Value, SpinDownValue.Value,
                        SpinSetOffValue.Value, SpinNetTotal.Value, CboProductUsing.Text, drv);
                }

                foreach (DataRow drv in dtPrice.Rows)
                {
                    if (Convert.ToBoolean(drv["IsDefault"]) != true)
                    {
                        AddTempWeightBalance(Convert.ToDateTime(drv["SaleDate"]), Convert.ToString(drv["PriceName"]), Convert.ToDecimal(drv["SalePriceAdvance"]),
                      Convert.ToDecimal(drv["BeginAmt"]), Convert.ToDecimal(drv["BeginAmt"]) - Convert.ToDecimal(drv["WeightBalanceAmt"]),
                      Convert.ToDecimal(drv["WeightBalanceAmt"]), Convert.ToBoolean(drv["IsDefault"]));
                    }
                }

                if (ProductId == 3)
                {
                    Load_RptSaleBill();
                }
                else
                {
                    Load_RptSaleBill1();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void BtnSaveBuy_Click(object sender, EventArgs e)
        {
            // 11	4 : สิทธิ์ใช้งานหน้าการขาย 	บันทึกข้อมูลการขาย
            int AuthorizeId = 11;

            if (SQLAuthorized.CheckAuthorized(ClassProperty.permisRoleId, AuthorizeId) == false)
            {
                SQLAuthorized.AlertAuthorized("สิทธิ์ใช้งานหน้าการขาย", "ไม่มีสิทธิ์บันทึกข้อมูลการขาย");
                return;
            }

            IsPrint = false;
            //SaveDataSale();
            Payment();
        }

        private void BtnSavePrint_Click(object sender, EventArgs e)
        {
            // 11	4 : สิทธิ์ใช้งานหน้าการขาย 	บันทึกข้อมูลการขาย
            int AuthorizeId = 11;

            if (SQLAuthorized.CheckAuthorized(ClassProperty.permisRoleId, AuthorizeId) == false)
            {
                SQLAuthorized.AlertAuthorized("สิทธิ์ใช้งานหน้าการขาย", "ไม่มีสิทธิ์บันทึกข้อมูลการขาย");
                return;
            }

            IsPrint = true;
            //SaveDataSale();
        }

        private bool SaveDataSale()
        {
            try
            {
            
                LoadMaxSaleId();
                GetReceiptNo(DtSaleDate.DateTime);

                if (SaveData(true) == true)
                {
                    ClearSave();
                }

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private void Load_RptSaleBill()
        {
            try
            {
                string sPrinterName;
                bool IsPrinter;
                //DataSet ds = SQLReport.Spt_GetSaleBill(SaleId, sCustomerId);

                RptSaleBill report = new RptSaleBill();
                report.DataSource = dtTempSale;
                report.DataMember = "Datatable1";

                FrmPayment frm = new FrmPayment();
                frm.DocShowBill.DocumentSource = report;
                frm.sMessage = "คุณยืนยันที่จะปิดข้อมูลการขายนี้ ใช่หรือไม่?";
                frm.BtnCloseBill.Text = "ปิดการขาย";
                frm.PrintType = 1;

                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    sPrinterName = frm.sPrinterName;
                    IsPrinter = frm.IsPrinter;
                    if (SaveDataSale())
                    {
                        if (IsPrinter == true)
                        {
                            report.PrinterName = sPrinterName;
                            report.Print(sPrinterName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void Load_RptSaleBill1()
        {
            try
            {
                string sPrinterName;
                bool IsPrinter;
                //DataSet ds = SQLReport.Spt_GetSaleBill(SaleId, sCustomerId);

                RptSaleBill_1 report = new RptSaleBill_1();
                report.DataSource = dtTempSale;
                report.DataMember = "Datatable1";

                subRptWeightBalance subreport = new subRptWeightBalance();

                //DataSet dss = SQLSale.Rpt_GetWeightBalance(SaleId);
                subreport.DataSource = dtTempPrice;
                subreport.DataMember = "Datatable1";

                report.xrSubWeightBalance.ReportSource = subreport;

                FrmPayment frm = new FrmPayment();
                frm.DocShowBill.DocumentSource = report;
                frm.sMessage = "คุณยืนยันที่จะปิดข้อมูลการขายนี้ ใช่หรือไม่?";
                frm.BtnCloseBill.Text = "ปิดการขาย";
                frm.PrintType = 1;

                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    sPrinterName = frm.sPrinterName;
                    IsPrinter = frm.IsPrinter;
                    if (SaveDataSale())
                    {
                        if (IsPrinter == true)
                        {
                            report.PrinterName = sPrinterName;
                            report.Print(sPrinterName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private bool SaveData(bool Is_Finalze)
        {
            try
            {
                if (CheckData(sSaleId) == true)
                {
                    if (UpdateSale(Is_Finalze))
                    {
                        if (SaveDataProduct())
                        {
                            if (OpenSaveSale == true)
                            {
                                RemoveSaveSale(sSaveSaleId);
                            }

                            XtraMessageBox.Show("บันทึกข้อมูลการขายสำเร็จ", "สถานะ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else
                {
                    if (AddSale(Is_Finalze))
                    {
                        if (SaveDataProduct())
                        {
                            if (OpenSaveSale == true)
                            {
                                RemoveSaveSale(sSaveSaleId);
                            }

                            XtraMessageBox.Show("บันทึกข้อมูลการขายสำเร็จ", "สถานะ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private bool AddSale(bool Is_Finalze)
        {
            try
            {
                using (var context = new RubberSoftEntities())
                {
                    var query = context.spt_AddSale(TxtSaleNumber.Text, sCustomerId, sCustomerName, DtSaleDate.DateTime,
                                         SpinValueBalance.Value, SpinSubTotal.Value, SpinDownValue.Value, SpinNetTotal.Value, 
                                         SpinSetOffValue.Value, SpinValueBalance.Value, ProductId, 
                                         Is_Finalze, ClassProperty.permisUserID, ClassProperty.StrTerminalId);

                    return true;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool UpdateSale(bool Is_Finalze)
        {
            try
            {
                using (var context = new RubberSoftEntities())
                {
                    var query = context.spt_UpdateSale(sSaleId, TxtSaleNumber.Text, sCustomerId, sCustomerName, 
                                         DtSaleDate.DateTime, SpinValueBalance.Value, SpinSubTotal.Value, 
                                         SpinDownValue.Value, SpinNetTotal.Value, SpinSetOffValue.Value,
                                         SpinValueBalance.Value, ProductId, Is_Finalze, ClassProperty.permisUserID);

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

                if (ProductId == 4)
                {
                    SaveTransactionLog(GridViewPrice);
                }

                SaveOutStandingBalance(sCustomerId);

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool AddSaleProduct(int SaleId, int PriceId, int ItemPriceId, string PriceName, decimal Percentage, decimal WeightAmount, decimal WeightAmount_Plate,
            decimal Drc, decimal TotalPrice_Smoke, decimal WeightAmount_Raw, decimal TotalPrice_Raw, decimal CalRubber, decimal TotalPrice,
            string Remark)
        {
            try
            {
                using (var context = new RubberSoftEntities())
                {
                    var query = context.spt_AddSaleProduct(SaleId, PriceId, ItemPriceId, PriceName, Percentage, WeightAmount, WeightAmount_Plate,
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

        private bool UpdateSaleProduct(int SaleProductId, int SaleId, int PriceId, int ItemPriceId, string PriceName, decimal Percentage, decimal WeightAmount,
            decimal WeightAmount_Plate, decimal Drc, decimal TotalPrice_Smoke, decimal WeightAmount_Raw, decimal TotalPrice_Raw,
            decimal CalRubber, decimal TotalPrice, string Remark)
        {
            try
            {
                using (var context = new RubberSoftEntities())
                {
                    var query = context.spt_UpdateSaleProduct(SaleProductId, SaleId, PriceId, ItemPriceId, PriceName, Percentage, WeightAmount,
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

        private bool CheckAddSaleProduct(int SaleProductId, int SaleId, int PriceId, int ItemPriceId, string PriceName, decimal Percentage, decimal WeightAmount,
            decimal WeightAmount_Plate, decimal Drc, decimal TotalPrice_Smoke, decimal WeightAmount_Raw,
            decimal TotalPrice_Raw, decimal CalRubber, decimal TotalPrice, string Remark)
        {
            try
            {

                if (SaleProductId == 0)
                {
                    AddSaleProduct(SaleId, PriceId, ItemPriceId, PriceName, Percentage, WeightAmount, WeightAmount_Plate, Drc, TotalPrice_Smoke,
                                  WeightAmount_Raw, TotalPrice_Raw, CalRubber, TotalPrice, Remark);
                }
                else
                {
                    UpdateSaleProduct(SaleProductId, SaleId, PriceId, ItemPriceId, PriceName, Percentage, WeightAmount, WeightAmount_Plate, Drc,
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
                GridColumn _colItemPriceId = View.Columns.ColumnByFieldName("ItemPriceId");
                GridColumn _colPriceName = View.Columns.ColumnByFieldName("PriceName");
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
                    object ItemPriceId = View.GetRowCellValue(i, _colItemPriceId);
                    object PriceName = View.GetRowCellValue(i, _colPriceName);
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

                    CheckAddSaleProduct(Convert.ToInt32(SaleProductId), sSaleId, Convert.ToInt32(PriceId), Convert.ToInt32(ItemPriceId), 
                        Convert.ToString(PriceName), Convert.ToDecimal(Percentage),
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

        private void LoadLkCustomerPrices(int cus_id)
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = SQLCustomer.Spt_GetCustomerPrices(cus_id);
                dt = ds.Tables[0];
             
                SLCustomerPrice.Properties.DataSource = dt;
                SLCustomerPrice.Properties.DisplayMember = "PriceName";
                SLCustomerPrice.Properties.ValueMember = "PriceId";
                //SLCustomerPrice.EditValue = 0;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void BtnGetCustomer_Click(object sender, EventArgs e)
        {
            if (GridViewSaleProduct.RowCount > 0)
            {
                XtraMessageBox.Show("มีรายการขายค้างอยู่ กรุณาลบรายการขายก่อน", "สถานะ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Load_UcCustomerType();
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
                SpinPercentage.Value = Convert.ToDecimal(view.GetFocusedRowCellValue("Percentage"));
                SpinWeightAmount.Value = Convert.ToDecimal(view.GetFocusedRowCellValue("WeightAmount"));
                SpinWeightAmount_Plate.Value = Convert.ToDecimal(view.GetFocusedRowCellValue("WeightAmount_Plate"));
                SpinDrc.Value = Convert.ToDecimal(view.GetFocusedRowCellValue("Drc"));
                SpinTotalPrice_Raw.Value = Convert.ToDecimal(view.GetFocusedRowCellValue("TotalPrice_Raw"));
                SpinCalRubber.Value = Convert.ToDecimal(view.GetFocusedRowCellValue("CalRubber"));
                sPriceId = Convert.ToInt32(view.GetFocusedRowCellValue("PriceId"));
                SLCustomerPrice.EditValue = sPriceId;
                PriceDefault = Convert.ToBoolean(view.GetFocusedRowCellValue("IsDefault"));
                sItemPriceId = Convert.ToInt32(view.GetFocusedRowCellValue("ItemPriceId"));
                LkItemPrice.EditValue = sItemPriceId;
                if (ProductId == 4)
                {
                    SpinTotalPrice_Smoke.Value = Convert.ToDecimal(view.GetFocusedRowCellValue("TotalPrice_Smoke"));
                }

                view.UpdateTotalSummary();
                CalculateValue();

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool SaveOutStandingBalance(int id)
        {
            try
            {
                if (AddOutStandingLog(SpinValueBalance.Value, SpinNetTotal.Value, sSaleId, 0, 3))
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

        private bool AddOutStandingLog(decimal BeginOutstandingDebt, decimal OutstandingDebt, int RefId, int Status, int LogTypeId)
        {
            try
            {
                using (var context = new RubberSoftEntities())
                {
                    var query = context.spt_AddOutStandingLog(sCustomerId, BeginOutstandingDebt, OutstandingDebt, RefId, Status, LogTypeId);

                    return true;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool AddTransactionLog(int PriceId, decimal BeginAmt, decimal WeightAmount_Raw,
                                       decimal WeightBalanceAmt, int RefId, int LogTypeId)
        {
            try
            {
                using (var context = new RubberSoftEntities())
                {
                    var query = context.spt_AddTransactionLog(sCustomerId, PriceId, BeginAmt, WeightAmount_Raw,
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

                    AddTransactionLog(Convert.ToInt32(PriceId), Convert.ToDecimal(BeginAmt), useAmt, getAmt, sSaleId, 3);

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

        private void SpinDrc_TextChanged(object sender, EventArgs e)
        {
            CalculateValue();
            CalEndUpdate();
            if (SpinDrc.Visible == true)
            {
                SpinPercentage.Value = 100 - SpinDrc.Value;
            }
        }

        private void SpinPercentage_TextChanged(object sender, EventArgs e)
        {
            if (SpinPercentage.Visible == true)
            {
                SpinDrc.Value = 100 - SpinPercentage.Value;
            }
        }

        private void SpinWeightAmount_TextChanged(object sender, EventArgs e)
        {
            CalculateValue();
            CalEndUpdate();
        }

        private void SpinWeightAmount_Plate_TextChanged(object sender, EventArgs e)
        {
            CalculateValue();
            CalEndUpdate();
        }

        private void SpinTotalPrice_Raw_TextChanged(object sender, EventArgs e)
        {
            CalculateValue();
            CalEndUpdate();
        }

        private void SpinSetOffValue_TextChanged(object sender, EventArgs e)
        {
            CalculateValue();
            CalEndUpdate();
        }

        private void SpinDownValue_TextChanged(object sender, EventArgs e)
        {
            CalculateValue();
            CalEndUpdate();
        }

        private void CalculateValue()
        {
            try
            {
                decimal WeightAmount, Drc, WeightAmount_Plate, DownValue,
                    TotalPrice_Raw, TotalPrice, NetTotal, ValueBalance, SumTotal, SetOffValue;

                decimal TotalPrice_Smoke, WeightAmount_Raw, WeightAmount_Cal, cutWeightAmount, sBuyDrc, 
                    CalRubber, Paynet;

                string strWeightAmount, str;

                sBuyDrc = SpinBuyDrc.Value;
                WeightAmount = SpinWeightAmount.Value;
                Drc = SpinDrc.Value;
                WeightAmount_Plate = SpinWeightAmount_Plate.Value;
                TotalPrice_Raw = SpinTotalPrice_Raw.Value;
                ValueBalance = SpinValueBalance.Value;
                DownValue = SpinDownValue.Value;
                SetOffValue = SpinSetOffValue.Value;

                if (ProductId == 3)
                {
                    TotalPrice_Smoke = WeightAmount * Drc;
                    WeightAmount_Raw = TotalPrice_Smoke / 100;
                    WeightAmount_Cal = Math.Round(WeightAmount_Raw, 2);
                    strWeightAmount = WeightAmount_Cal.ToString("N2");

                    if (strWeightAmount.Length > 1)
                    {
                        str = strWeightAmount.Substring(0, strWeightAmount.Length - 1);
                    }
                    else
                    {
                        str = strWeightAmount;
                    }

                    cutWeightAmount = Convert.ToDecimal(str);
                    TotalPrice = cutWeightAmount * TotalPrice_Raw;
                    //SumTotal = (ValueBalance - DownValue) + sumSubTotal;
                    Paynet = sumSubTotal;
                    SumTotal = sumSubTotal - SetOffValue;
                    NetTotal = SumTotal;

                    SpinTotalPrice_Smoke.Value = TotalPrice_Smoke;
                    SpinWeightAmount_Raw.Value = cutWeightAmount;
                    SpinCalRubber.Value = 0;
                    SpinTotalPrice.Value = Math.Floor(TotalPrice);
                    SpinSubTotal.Value = sumSubTotal;
                    SpinPaynet.Value = Paynet;
                    SpinNetTotal.Value = NetTotal;
                }
                else
                {
                    TotalPrice_Smoke = SpinTotalPrice_Smoke.Value;
                    WeightAmount_Raw = 0;
                    cutWeightAmount = 0;
                    CalRubber = TotalPrice_Smoke * (Drc / 100) * (sBuyDrc / 100);
                    TotalPrice = TotalPrice_Raw * (Math.Floor(CalRubber * 100) / 100);
                    //SumTotal = (ValueBalance - DownValue) + sumSubTotal;
                    Paynet = sumSubTotal - DownValue;
                    SumTotal = Paynet - SetOffValue;
                    NetTotal = SumTotal;

                    SpinTotalPrice_Smoke.Value = TotalPrice_Smoke;
                    SpinWeightAmount_Raw.Value = cutWeightAmount;
                    SpinCalRubber.Value = Math.Floor(CalRubber * 100) / 100;
                    SpinTotalPrice.Value = Math.Floor(TotalPrice);
                    SpinSubTotal.Value = sumSubTotal;
                    SpinPaynet.Value = Paynet;
                    SpinNetTotal.Value = NetTotal;
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        decimal sumSubTotal = 0;

        private void GridViewSaleProduct_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
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
                    SpinTotalPrice_Smoke.Value = Convert.ToDecimal(_view.GetFocusedRowCellValue("UnitPrice"));
                    PriceDefault = Convert.ToBoolean(_view.GetFocusedRowCellValue("IsDefault"));
                    //GetItemPrice(sPriceId);
                    CalculateValue();
                    SpinTotalPrice_Raw.Focus();
                }

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private void LkItemPrice_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit lookUp = sender as LookUpEdit;
            if (lookUp.GetSelectedDataRow() is DataRowView dataRow)
            {
                if (dataRow != null)
                {
                    sItemPriceId = Convert.ToInt32(dataRow["ItemPriceId"]);
                    SpinTotalPrice_Smoke.Value = Convert.ToDecimal(dataRow["UnitPrice"]);
                }
            }
        }

        private void BtnGetSale_Click(object sender, EventArgs e)
        {
            Load_UcLoadSale();
        }

        private void Load_UcLoadSale()
        {
            try
            {
                ClassProperty.IsOpenBuy = false;

                FlyoutDialog flyout_Dialog = new FlyoutDialog(FindForm(), new UcLoadSale());
                flyout_Dialog.Properties.Style = FlyoutStyle.Popup;
                {
                    var frm = flyout_Dialog;
                    if (frm.ShowDialog(this) == DialogResult.OK)
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

        private void BtnNotSave_Click(object sender, EventArgs e)
        {
            // 14	4 : สิทธิ์ใช้งานหน้าการขาย 	พักการขาย
            int AuthorizeId = 14;

            if (SQLAuthorized.CheckAuthorized(ClassProperty.permisRoleId, AuthorizeId) == false)
            {
                SQLAuthorized.AlertAuthorized("สิทธิ์ใช้งานหน้าการขาย", "ไม่มีสิทธิ์พักการขาย");
                return;
            }

            FuncSaveBuyData();
        }

        private void FuncSaveBuyData()
        {
            try
            {
                if (GridViewSaleProduct.RowCount <= 0)
                {
                    XtraMessageBox.Show("ไม่มีรายการสินค้า", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (CheckSaveSaleData() == true)
                {
                    ClearSave();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private bool GetSaveSaleNumBer(DateTime date)
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = SQLSale.Spt_GetSaveSaleNumber(date);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow drv in dt.Rows)
                    {
                        sSaveSaleNumber = Convert.ToString(drv["ReceiptNo"]);
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

        private bool CheckSaveSaleData()
        {
            try
            {
                GetSaveSaleNumBer(DateTime.Now);

                if (OpenSaveSale == true)
                {
                    if (CheckSaveSale(sSaveSaleId) == true)
                    {
                        RemoveSaveSale(sSaveSaleId);
                    }
                }

                if (AddSaveSale(sSaveSaleNumber))
                {
                    if (SaveOpenDataProduct())
                    {
                        XtraMessageBox.Show("พักการซื้อสำเร็จ", "สถานะ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private bool CheckSaveSale(int SaveSaleId)
        {
            try
            {
                using (var context = new RubberSoftEntities())
                {
                    var query = context.spt_GetSaveSale().Where(o => o.Active == true && 
                    o.SaveSaleId == SaveSaleId).ToList();
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

        private bool AddSaveSale(string SaveSaleNumber)
        {
            try
            {
                using (var context = new RubberSoftEntities())
                {
                    var query = context.spt_AddSaveSale(SaveSaleNumber, sCustomerId, sCustomerName, DtSaleDate.DateTime,
                                         SpinSubTotal.Value, SpinDownValue.Value, SpinNetTotal.Value, SpinSetOffValue.Value,
                                         SpinValueBalance.Value, ProductId, CboProductUsing.Text, ClassProperty.permisUserID);

                    return true;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool SaveOpenDataProduct()
        {
            try
            {
                sSaveSaleId = SQLSale.GetMaxSaveSaleId();

                if (GridViewSaleProduct.RowCount > 0)
                {
                    CheckSaveSaleProduct(GridViewSaleProduct);
                }

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool CheckAddSaveSaleProduct(int SaleId, int PriceId, int ItemPriceId, string PriceName, decimal Percentage, decimal WeightAmount,
            decimal WeightAmount_Plate, decimal Drc, decimal TotalPrice_Smoke, decimal WeightAmount_Raw,
            decimal TotalPrice_Raw, decimal CalRubber, decimal TotalPrice, string Remark)
        {
            try
            {

                AddSaveSaleProduct(SaleId, PriceId, ItemPriceId, PriceName, Percentage, WeightAmount, WeightAmount_Plate, Drc, TotalPrice_Smoke,
                                  WeightAmount_Raw, TotalPrice_Raw, CalRubber, TotalPrice, Remark);

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool AddSaveSaleProduct(int SaleId, int PriceId, int ItemPriceId, string PriceName, decimal Percentage, decimal WeightAmount, decimal WeightAmount_Plate,
            decimal Drc, decimal TotalPrice_Smoke, decimal WeightAmount_Raw, decimal TotalPrice_Raw, decimal CalRubber, decimal TotalPrice,
            string Remark)
        {
            try
            {
                using (var context = new RubberSoftEntities())
                {
                    var query = context.spt_AddSaveSaleProduct(SaleId, PriceId, ItemPriceId, PriceName, Percentage, WeightAmount, WeightAmount_Plate,
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

        private bool CheckSaveSaleProduct(ColumnView View)
        {
            try
            {

                // Obtain the Price column.  
                GridColumn _colSaleProductId = View.Columns.ColumnByFieldName("SaleProductId");
                GridColumn _colPriceId = View.Columns.ColumnByFieldName("PriceId");
                GridColumn _colItemPriceId = View.Columns.ColumnByFieldName("ItemPriceId");
                GridColumn _colPriceName = View.Columns.ColumnByFieldName("PriceName");
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
                    object ItemPriceId = View.GetRowCellValue(i, _colItemPriceId);
                    object PriceName = View.GetRowCellValue(i, _colPriceName);
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

                    CheckAddSaveSaleProduct(sSaveSaleId, Convert.ToInt32(PriceId), Convert.ToInt32(ItemPriceId), Convert.ToString(PriceName), Convert.ToDecimal(Percentage),
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

        private void BtnNew_Click(object sender, EventArgs e)
        {
            // 13	4 : สิทธิ์ใช้งานหน้าการขาย	 ยกเลิกการขาย (หน้าการขาย)
            int AuthorizeId = 13;

            if (SQLAuthorized.CheckAuthorized(ClassProperty.permisRoleId, AuthorizeId) == false)
            {
                SQLAuthorized.AlertAuthorized("สิทธิ์ใช้งานหน้าการขาย", "ไม่มีสิทธิ์ยกเลิกการขาย (หน้าการขาย)");
                return;
            }

            CancelSale(sSaleId);
        }

        private bool CancelSale(int SaleId)
        {
            try
            {
                if (CheckData(SaleId) == true)
                {
                    if (XtraMessageBox.Show("คุณต้องการยกเลิกข้อมูลการขายนี้ ใช่หรือไม่?", "ยืนยัน", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                    {
                        if (SQLSale.VoidSale(SaleId))
                        {
                            ClearSave();
                        }
                    }
                }
                else
                {
                    if (XtraMessageBox.Show("คุณต้องการยกเลิกข้อมูลการขายนี้ ใช่หรือไม่?", "ยืนยัน", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                    {
                        RefreshForm();
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

        private void lblจ่ายล่วงหน้า_Click(object sender, EventArgs e)
        {

        }

        private void BtnAddPrice_Click(object sender, EventArgs e)
        {
            if (SLCustomer.EditValue == null || sCustomerId == 0)
            {
                XtraMessageBox.Show("กรุณาระบุลูกค้า !", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (GridViewSaleProduct.RowCount > 0)
            {
                XtraMessageBox.Show("มีรายการขายค้างอยู่ กรุณาลบรายการขายก่อน", "สถานะ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Load_UcAddCustomerPrice();
        }

        private void SLCustomer_EditValueChanged(object sender, EventArgs e)
        {
            if (SLCustomer.EditValue == null)
            {
                return;
            }

            if (ProductId == 2)
            {
                if (GridViewSaleProduct.RowCount > 0)
                {
                    if (SLCustomer.EditValue.ToString() != sCustomerId.ToString())
                    {
                        XtraMessageBox.Show("มีรายการขายค้างอยู่ กรุณาลบรายการขายก่อน", "สถานะ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    SLCustomer.EditValue = sCustomerId;
                    return;
                }
            }

            SLSelectCust(GridViewCustomer);
        }

        private bool SLSelectCust(GridView _view)
        {
            try
            {
                if (_view.SelectedRowsCount > 0)
                {
                    sCustomerId = Convert.ToInt32(_view.GetFocusedRowCellValue("CustomerId"));
                    GetCustomer(sCustomerId);
                    GridViewSaleProduct.UpdateTotalSummary();
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

        private void Load_UcAddCustomerPrice()
        {
            try
            {
                ClassProperty.sCustomerId = sCustomerId;

                FlyoutDialog flyout_Dialog = new FlyoutDialog(FindForm(), new UcAddCustomerPrice());
                flyout_Dialog.Properties.Style = FlyoutStyle.Popup;
                {
                    var frm = flyout_Dialog;
                    if (frm.ShowDialog(this) == DialogResult.OK)
                    {
                        LoadLkCustomerPrices(sCustomerId);
                        SLCustomerPrice.EditValue = SQLCustomer.GetDefaultCustomerPrice(sCustomerId);
                        //GetItemPrice(SQLCustomer.GetDefaultCustomerPrice(sCustomerId));
                        //SpinTotalPrice_Smoke.Value = Convert.ToDecimal(LkItemPrice.Text);
                        LoadCustomerPrices(sCustomerId);
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

        private void BtnOpenSaveSale_Click(object sender, EventArgs e)
        {
            if (GridViewSaleProduct.RowCount > 0)
            {
                XtraMessageBox.Show("มีรายการขายค้างอยู่ กรุณาลบรายการขายก่อน", "สถานะ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Load_OpenSaveSale();
        }

        private void Load_OpenSaveSale()
        {
            try
            {
                FlyoutDialog flyout_Dialog = new FlyoutDialog(FindForm(), new UcOpenSaveSale());
                flyout_Dialog.Properties.Style = FlyoutStyle.Popup;
                {
                    var frm = flyout_Dialog;
                    if (frm.ShowDialog(this) == DialogResult.OK)
                    {
                        OpenSaveSale = true;
                        sSaveSaleId = ClassProperty.strSaveSaleId;
                        if (GetSaveBuyData(sSaveSaleId))
                        {
                            //RemoveSaveSale(sSaveSaleId);
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

        private bool GetSaveBuyData(int SaveSaleId)
        {
            try
            {
                using (var context = new RubberSoftEntities())
                {
                    var query = context.spt_GetSaveSale().Where(o => o.SaveSaleId == SaveSaleId).ToList();
                    if (query.Count > 0)
                    {
                        foreach (spt_GetSaveSale_Result dt in query)
                        {
                            GetReceiptNo(DateTime.Now);
                            DtSaleDate.DateTime = DateTime.Now;
                            sCustomerId = dt.CustomerId.Value;
                            TxtCustomerName.Text = dt.CustomerName;
                            SpinSubTotal.Value = dt.SubTotal.Value;
                            SpinDownValue.Value = dt.DownValue.Value;
                            SpinNetTotal.Value = dt.NetTotal.Value;
                            SpinSetOffValue.Value = dt.SetOffValue.Value;
                            SpinValueBalance.Value = dt.ValueBalance.Value;
                            ProductId = dt.ProductTypeId.Value;
                            CboProductUsing.Text = dt.ProductUsing;
                        }

                        SLCustomer.EditValue = sCustomerId;
                        GetCustomer(sCustomerId);
                        GetSaveSaleProduct(SaveSaleId);
                        LoadProductType(ProductId);
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

        private bool RemoveSaveSale(int SaveSaleId)
        {
            try
            {
                if (SQLSale.DeleteSaveSale(SaveSaleId))
                {
                    SQLSale.DeleteSaveSaleProduct(SaveSaleId);
                }

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool GetSaveSaleProduct(int SaleId)
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = SQLSale.Spt_GetSaveSaleProduct(SaleId);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow drv in dt.Rows)
                    {
                        RunNo = dtSaleProduct.Rows.Count + 1;

                        if (AddDataSaleProduct(RunNo, 0, sSaleId,
                            Convert.ToInt32(drv["PriceId"]), Convert.ToInt32(drv["ItemPriceId"]),
                            Convert.ToString(drv["PriceName"]), Convert.ToDouble(drv["Percentage"]),
                            Convert.ToDouble(drv["WeightAmount"]), Convert.ToDouble(drv["WeightAmount_Plate"]),
                            Convert.ToDouble(drv["Drc"]), Convert.ToDecimal(drv["TotalPrice_Smoke"]),
                            Convert.ToDecimal(drv["WeightAmount_Raw"]), Convert.ToDecimal(drv["TotalPrice_Raw"]),
                            Convert.ToDecimal(drv["CalRubber"]), Convert.ToDecimal(drv["TotalPrice"]),
                            Convert.ToString(drv["Remark"]), Convert.ToBoolean(drv["IsDefault"])))
                        {
                            if (ProductId == 4)
                            {
                                if (CalWeightBalanceAmt(GridViewPrice, Convert.ToInt32(drv["PriceId"]), Convert.ToDecimal(drv["TotalPrice_Raw"]), true))
                                {
                                    RefreshValue();
                                    GridViewSaleProduct.UpdateTotalSummary();
                                    CalculateValue();
                                    CalEndUpdate();
                                }
                            }
                            else
                            {
                                RefreshValue();
                                GridViewSaleProduct.UpdateTotalSummary();
                                CalculateValue();
                                CalEndUpdate();
                            }
                        }
                    }
                }
                else
                {
                    dtSaleProduct = dt;
                }

                GridSaleProduct.DataSource = dtSaleProduct;

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            if (GridViewSaleProduct.RowCount > 0)
            {
                if (XtraMessageBox.Show("มีรายการขายค้างอยู่ คุณต้องการยกเลิกข้อมูลการขายนี้ ใช่หรือไม่?", "ยืนยัน",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    SaveLayOut();
                    ClassProperty.sCustomerId = 0;
                    this.Close();

                    //this.FindForm().DialogResult = DialogResult.Cancel;
                    //FrmMain frm = new FrmMain();
                    //{
                    //    this.Close();
                    //    frm.Show();
                    //}
                }
            }
            else
            {
                SaveLayOut();
                ClassProperty.sCustomerId = 0;
                this.Close();

                //this.FindForm().DialogResult = DialogResult.Cancel;
                //FrmMain frm = new FrmMain();
                //{
                //    this.Close();
                //    frm.Show();
                //}
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
                else
                {
                    //Console.WriteLine("File \"{0}\" already exists.", fileName);
                    //return false;
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

        private void Load_UcCustomerType()
        {
            try
            {
                ClassProperty.CustomerGroupId = ProductId;

                FlyoutDialog flyout_Dialog = new FlyoutDialog(FindForm(), new UcCustomerType());
                flyout_Dialog.Properties.Style = FlyoutStyle.Popup;
                {
                    var frm = flyout_Dialog;
                    if (frm.ShowDialog(this) == DialogResult.OK)
                    {
                        sCustomerId = ClassProperty.sCustomerId;
                        SLCustomer.EditValue = sCustomerId;
                        GetCustomer(sCustomerId);
                        LoadLkCustomer(ProductId);
                        LoadCustomerPrices(sCustomerId);
                        GetValueBalance(sCustomerId);
                        GridViewSaleProduct.UpdateTotalSummary();
                        CalculateValue();
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

        private bool AddTempBuy(int SaleId, string SaleNumber, DateTime SaleDate, string CustomerName,
            string CustomerAddress, string LicensePlate, string Phone, decimal BeginBalance, decimal ValueBalance,
            decimal SubTotal, decimal DownValue, decimal SetOffValue, decimal NetTotal, string ProductUsing, DataRow drv)
        {
            DataTable dt_Added = dtTempSale;
            if (dt_Added == null)
                return false;

            try
            {
                DataRow dr_New = dt_Added.NewRow();

                dt_Added.BeginInit();

                dr_New["SaleId"] = SaleId;
                dr_New["SaleNumber"] = SaleNumber;
                dr_New["SaleDate"] = SaleDate;
                dr_New["CustomerName"] = CustomerName;
                dr_New["CustomerAddress"] = CustomerAddress;
                dr_New["LicensePlate"] = LicensePlate;
                dr_New["Phone"] = Phone;
                dr_New["BeginBalance"] = BeginBalance;
                dr_New["ValueBalance"] = ValueBalance;
                dr_New["SubTotal"] = SubTotal;
                dr_New["DownValue"] = DownValue;
                dr_New["SetOffValue"] = SetOffValue;
                dr_New["NetTotal"] = NetTotal;
                dr_New["Percentage"] = drv["Percentage"];
                dr_New["WeightAmount"] = drv["WeightAmount"];
                dr_New["WeightAmount_Plate"] = drv["WeightAmount_Plate"];
                dr_New["Drc"] = drv["Drc"];
                dr_New["TotalPrice_Smoke"] = drv["TotalPrice_Smoke"];
                dr_New["WeightAmount_Raw"] = drv["WeightAmount_Raw"];
                dr_New["TotalPrice_Raw"] = drv["TotalPrice_Raw"];
                dr_New["CalRubber"] = drv["CalRubber"];
                dr_New["TotalPrice"] = drv["TotalPrice"];
                dr_New["Remark"] = drv["Remark"];
                dr_New["ProductUsing"] = ProductUsing;

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

        private bool AddTempWeightBalance(DateTime PriceDate, string PriceName, decimal SalePriceAdvance, decimal BeginAmt,
            decimal WeightAmount_Raw, decimal WeightBalanceAmt, bool IsDefault)
        {
            DataTable dt_Added = dtTempPrice;
            if (dt_Added == null)
                return false;

            try
            {
                DataRow dr_New = dt_Added.NewRow();

                dt_Added.BeginInit();

                dr_New["PriceDate"] = PriceDate;
                dr_New["PriceName"] = PriceName;
                dr_New["SalePriceAdvance"] = SalePriceAdvance;
                dr_New["BeginAmt"] = BeginAmt;
                dr_New["WeightAmount_Raw"] = WeightAmount_Raw;
                dr_New["WeightBalanceAmt"] = WeightBalanceAmt;
                dr_New["IsDefault"] = IsDefault;

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
    }
}
