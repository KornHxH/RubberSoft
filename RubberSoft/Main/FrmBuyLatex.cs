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
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.Utils;

namespace RubberSoft.Main
{
    public partial class FrmBuyLatex : XtraForm
    {
        public FrmBuyLatex()
        {
            InitializeComponent();
        }

        readonly SQLData SQLData = new SQLData();
        readonly SQLCustomer SQLCustomer = new SQLCustomer();
        readonly SQLBuy SQLBuy = new SQLBuy();
        readonly SQLSale SQLSale = new SQLSale();
        readonly SQLReport SQLReport = new SQLReport();
        readonly SQLAuthorized SQLAuthorized = new SQLAuthorized();

        private int sCustomerId, sPriceId, sItemPriceId, sSaveBuyId, sBuyId, ProductId, sProductTypeId, RunNo, sLogId, sOutStandingLogId;
        private string sCustomerName, sBuyNumber, sSaveBuyNumber, sCustomerAddress;
        readonly DataTable dtCustomer = new DataTable();
        DataTable dtBuyProduct = new DataTable();
        DataTable dtPrice = new DataTable();
        DataTable dtTempPrice = new DataTable();
        DataTable dtTempBuy = new DataTable();

        //private TimeSpan TimeNow; 
        public bool OpenSaveSale, OpenConsignment, IsPrint, PriceDefault;
        private decimal sNetTotal;
        readonly string fileName = Application.StartupPath + @"\SplitBuy.xml";

        private void FrmBuyLatex_Load(object sender, EventArgs e)
        {
            RefreshForm();
            LoadProducts(sProductTypeId);
            SaveLayOut();
            GetTempBuy();
            //GetTempBuyProduct();
            GetTempPrice();
        }

        private void RefreshForm()
        {
            try
            {
                sProductTypeId = 1;
                ProductId = 1;
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
                sSaveBuyId = 0;
                lblUserName.Text = "ยินดีต้อนรับ : " + ClassProperty.permisUserNm;
                TimerMain.Enabled = true;
                DtBuyDate.DateTime = DateTime.Now;
                LkProduct.Enabled = true;
                LkProduct.EditValue = 1;
                
                SpinDrc.Value = 100 - SpinPercentage.Value;
                LoadProductType(ProductId);
                LoadDataProduct(ProductId);
                ClearValues();
                //Load_UcCustomerType();

                GridViewByProduct.OptionsView.ShowFooter = true;
                GridColumn colTotalPrice = GridViewByProduct.Columns["TotalPrice"];
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
                sBuyId = 0;
                OpenSaveSale = false;
                ClearData();
                LoadMaxBuyId();
                GetReceiptNo(DtBuyDate.DateTime);
                GetCustomer(sCustomerId);
                GetBuyProduct(sBuyId);
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

                SpinBeginAmt.Value = 0;
                SpinWeightBalanceAmt.Value = 0;
                SpinUseAmount.Value = 0;

                SLCustomerPrice.Enabled = true;

                BtnAddBuy.Text = "เพิ่มการซื้อ";
                RunNo = dtBuyProduct.Rows.Count + 1;
                lblRunNo.Text = "รายการที่ :       " + RunNo.ToString();
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
                            SpinBeginAmt.Value = SQLCustomer.GetUsePrice(sPriceId);
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

        private void LoadLkCustomer(int CustomerGroupId)
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = SQLBuy.GetLKBuyCustomer(CustomerGroupId);
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

        private void LoadCustomerPrices(int cus_id)
        {
            try
            {
                DataSet ds = SQLBuy.Spt_GetBuyWeightBalance(cus_id);
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
                int sCid = cid;

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
                DataSet ds = SQLBuy.Spt_GetBuyNumber(date);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow drv in dt.Rows)
                    {
                        sBuyNumber = Convert.ToString(drv["ReceiptNo"]);
                        TxtBuyNumber.Text = sBuyNumber;
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

        private bool LoadMaxBuyId()
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = SQLBuy.GetMaxBuyId();
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow drv in dt.Rows)
                    {
                        sBuyId = Convert.ToInt32(drv["BuyId"]);
                    }
                }
                else
                {
                    sBuyId = 1;
                }

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private void GetTempBuy()
        {
            try
            {
                DataSet ds = SQLBuy.Spt_GetTempBuy();
                dtTempBuy = ds.Tables[0];
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        //private bool GetTempBuyProduct()
        //{
        //    try
        //    {
        //        DataSet ds = SQLBuy.Spt_GetTempBuyProduct();
        //        dtBuyProduct = ds.Tables[0];

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        XtraMessageBox.Show(ex.Message);
        //        return false;
        //    }
        //}

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
                LkProduct.EditValue = 1;
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
                        SpinBuyDrc.Value = Convert.ToDecimal(drv["BuyDrc"]);
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
                if (id == 1)
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
                    LinkPercentage.Visible = false;
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
                    BtnNotSave.Enabled = true;
                    BtnOpenSaveSale.Enabled = true;
                    BtnConsignment.Enabled = false;
                    BtnOpenConsignment.Enabled = false;
                    BtnSaveData.Visible = false;
                    BtnSaveBuyDataDetails.Visible = false;

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
                    LinkPercentage.Visible = true;
                    SpinPercentage.Visible = true;
                    SpinPercentage.Value = 0;
                    lblDrc.Visible = false;
                    SpinDrc.Visible = false;
                    lblTotalPrice_Smoke.Visible = true;
                    SpinTotalPrice_Smoke.Visible = true;
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
                    BtnNotSave.Enabled = false;
                    BtnOpenSaveSale.Enabled = false;
                    BtnConsignment.Enabled = true;
                    BtnOpenConsignment.Enabled = true;
                    BtnSaveData.Visible = true;
                    BtnSaveBuyDataDetails.Visible = true;

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
                AddBuyLatex();
            }
        }

        private void BtnAddBuy_Click(object sender, EventArgs e)
        { 
            AddBuyLatex();
        }

        private bool AddBuyLatex()
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

                if (ProductId == 2)
                {
                    if (sPriceId == 0)
                    {
                        XtraMessageBox.Show("กรุณาระบุราคาล่วงหน้า !", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        SLCustomerPrice.ShowPopup();
                        return false;
                    }
                }

                if (ProductId == 2)
                {
                    if (SpinWeightTotal.Value < 0)
                    {
                        XtraMessageBox.Show("คุณระบุน้ำหนักเกิน !", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        SpinTotalPrice_Raw.Focus();
                        return false;
                    }

                }
                                                  
                //decimal sPrice = SpinTotalPrice_Raw.Value - SpinWeightAmount_Plate.Value;
                CalEndUpdate();

                if (sPriceId != SQLCustomer.GetDefaultCustomerPrice(sCustomerId))
                {
                    if (ProductId == 2)
                    {
                        if (SpinWeightBalanceAmt.Value < 0)
                        {
                            XtraMessageBox.Show("น้ำหนักคงเหลือ มีมูลค่าไม่เพียงพอ !", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            CheckValidated();
                            SpinTotalPrice_Raw.Focus();
                            return false;
                        }
                    }
                   
                }

                if (BtnAddBuy.Text != "แก้ไขการซื้อ")
                {
                    int maxLevel = 0;
                    //int minLevel = int.MinValue;
                    foreach (DataRow dr in dtBuyProduct.Rows)
                    {
                        int tLevel = dr.Field<int>("RunNo");
                        maxLevel = Math.Max(maxLevel, tLevel);
                        //minLevel = Math.Min(minLevel, tLevel);
                    }

                    RunNo = maxLevel + 1;
                }

                if (CheckAdProduct())
                {
                    GridViewByProduct.UpdateTotalSummary();
                }

                //GridView sview = SLCustomerPrice.Properties.View;
                //DataRow row = sview.GetDataRow(0);
                //string value = row[0].ToString();

                SLCustomerPrice.EditValue = SQLCustomer.GetDefaultCustomerPrice(sCustomerId);
                sPriceId = SQLCustomer.GetDefaultCustomerPrice(sCustomerId);
                SpinBeginAmt.Value = SQLCustomer.GetUsePrice(sPriceId);
                
                SpinWeightAmount.Focus();

                RefreshValue();
                CalEndUpdate();

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
                decimal sumUseRaw = SpinTotalPrice_Raw.Value;

                if (dtBuyProduct.Rows.Count > 0)
                {
                    foreach (DataRow drv in dtBuyProduct.Rows)
                    {
                        if (Convert.ToInt32(drv["RunNo"]) != RunNo)
                        {
                            sumTotalPrice_Raw += Convert.ToDecimal(drv["TotalPrice_Raw"]);
                            sumWeightAmount += Convert.ToDecimal(drv["WeightAmount"]);
                            sumWeightAmount_Plate += Convert.ToDecimal(drv["WeightAmount_Plate"]);
                        }

                        if (Convert.ToInt32(drv["RunNo"]) != RunNo && Convert.ToInt32(drv["PriceId"]) == sPriceId)
                        {
                            sumUseRaw += Convert.ToDecimal(drv["TotalPrice_Raw"]);
                        }
                    }

                    //sumTotalPrice_Raw += Convert.ToDecimal(dtBuyProduct.Compute("SUM(TotalPrice_Raw)", "RunNo <> " + RunNo + ""));
                    //sumWeightAmount += Convert.ToDecimal(dtBuyProduct.Compute("SUM(WeightAmount)", "RunNo <> " + RunNo + ""));
                    //sumWeightAmount_Plate += Convert.ToDecimal(dtBuyProduct.Compute("SUM(WeightAmount_Plate)", "RunNo <> " + RunNo + ""));
                }

                SpinWeightTotal.Value = (sumWeightAmount - sumWeightAmount_Plate) - sumTotalPrice_Raw;
                SpinUseAmount.Value = sumUseRaw;

                if (sPriceId != SQLCustomer.GetDefaultCustomerPrice(sCustomerId))
                {
                    SpinWeightBalanceAmt.Value = SpinBeginAmt.Value - SpinUseAmount.Value;
                }
                else
                {
                    SpinWeightBalanceAmt.Value = 0;
                }

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        //private decimal CheckBalancePrice(int PriceId)
        //{
        //    try
        //    {
        //        int CkPriceId;
        //        decimal TotalPrice_Raw;
        //        decimal TotalRaw = 0;

        //        if (dtBuyProduct.Rows.Count > 0)
        //        {
        //            foreach (DataRow drv in dtBuyProduct.Rows)
        //            {
        //                if (Convert.ToInt32(drv["RunNo"]) != RunNo)
        //                {
        //                    CkPriceId = Convert.ToInt32(drv["PriceId"]);
        //                    TotalPrice_Raw = Convert.ToDecimal(drv["TotalPrice_Raw"]);

        //                    if (CkPriceId == PriceId)
        //                    {
        //                        TotalRaw = +TotalPrice_Raw;
        //                    }
        //                }
        //            }
        //        }

        //        lblRunNo.Text = "รายการที่ :       " + RunNo.ToString();

        //        //int CekPriceId;
        //        //decimal BeginAmt, WeightBalanceAmt;
        //        //bool IsDefault;

        //        //if (dtPrice.Rows.Count > 0)
        //        //{
        //        //    foreach (DataRow drv in dtPrice.Rows)
        //        //    {
        //        //        CekPriceId = Convert.ToInt32(drv["PriceId"]);
        //        //        BeginAmt = Convert.ToDecimal(drv["BeginAmt"]);
        //        //        WeightBalanceAmt = Convert.ToDecimal(drv["WeightBalanceAmt"]);
        //        //        IsDefault = Convert.ToBoolean(drv["IsDefault"]);
        //        //        if (IsDefault != true)
        //        //        {
        //        //            if (PriceId == CekPriceId)
        //        //            {
        //        //                if (TotalRaw > BeginAmt)
        //        //                {
        //        //                    return false;
        //        //                }
        //        //            }
        //        //        }

        //        //        SpinBeginAmt.Value = BeginAmt;
        //        //        SpinWeightBalanceAmt.Value = WeightBalanceAmt;
        //        //        SpinUseAmount.Value = TotalRaw;
        //        //    }
        //        //}

        //        return TotalRaw;
        //    }
        //    catch (Exception ex)
        //    {
        //        XtraMessageBox.Show(ex.Message);
        //        return 0;
        //    }
        //}

        private bool CheckValidated()
        {
            try
            {
                SpinTotalPrice_Raw.ErrorText = "น้ำหนักคงเหลือ มีมูลค่าไม่เพียงพอ !";
                SpinTotalPrice_Raw.ErrorImageOptions.Image = DevExpress.Images.ImageResourceCache.Default.GetImage("images/status/warning_16x16.png");

                //SpinWeightBalanceAmt.ErrorText = "น้ำหนักคงเหลือ มีมูลค่าไม่เพียงพอ !";
                //SpinWeightBalanceAmt.ErrorImageOptions.Image = DevExpress.Images.ImageResourceCache.Default.GetImage("images/status/warning_16x16.png");

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
                //decimal calRaw = 0;
                //int CkPriceId;

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

                if (AddDataBuyProduct(RunNo, 0, sBuyId, sPriceId, sItemPriceId, sPriceName, sPercentage, sWeightAmount, sWeightAmount_Plate, sDrc,
                    sTotalPrice_Smoke, sWeightAmount_Raw, sTotalPrice_Raw, sCalRubber, sTotalPrice, TxtRemark.Text, PriceDefault))
                {
                    if (ProductId == 2)
                    {
                        //foreach (DataRow drv in dtBuyProduct.Rows)
                        //{
                        //    CkPriceId = Convert.ToInt32(drv["PriceId"]);
                        //    calRaw += Convert.ToDecimal(drv["TotalPrice_Raw"]);
                        //}

                        //if (CalWeightBalanceAmt(GridViewPrice, Convert.ToInt32(drv["PriceId"]),
                        //       Convert.ToDecimal(drv["TotalPrice_Raw"]), true))
                        //{
                        //    //RefreshValue();
                        //}

                        if (CalWeightBalanceAmt(GridViewPrice, sPriceId, SpinUseAmount.Value, true))
                        {
                            //RefreshValue();
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

        private bool CalWeightBalanceAmt(ColumnView View, int refPriceId, decimal UseRaw, bool type)
        {
            try
            {
                //decimal TotalRaw = 0;
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
                                //TotalRaw += TotalPrice_Raw;
                                BalanceAmt = Convert.ToDecimal(sWeightAmount) - UseRaw;

                                View.SetRowCellValue(i, _colWeightBalanceAmt, BalanceAmt);
                            }

                            if (type == false)
                            {
                                //TotalRaw += TotalPrice_Raw;
                                BalanceAmt = Convert.ToDecimal(WeightBalanceAmt) + UseRaw;

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

        private void BtnEditBuyProduct_Click(object sender, EventArgs e)
        {
            //int refBuyProductId = Convert.ToInt32(GridViewByProduct.GetFocusedRowCellValue("BuyProductId"));
            BtnAddBuy.Text = "แก้ไขการซื้อ";
            GetEditData(GridViewByProduct);
        }

        private void GridViewByProduct_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            BtnAddBuy.Text = "แก้ไขการซื้อ";
            GetEditData(GridViewByProduct);
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
                SLCustomerPrice.EditValue = Convert.ToInt32(view.GetFocusedRowCellValue("PriceId"));
                sPriceId = Convert.ToInt32(view.GetFocusedRowCellValue("PriceId"));
                SpinBeginAmt.Value = SQLCustomer.GetUsePrice(Convert.ToInt32(view.GetFocusedRowCellValue("PriceId")));             
                PriceDefault = Convert.ToBoolean(view.GetFocusedRowCellValue("IsDefault"));
                sItemPriceId = Convert.ToInt32(view.GetFocusedRowCellValue("ItemPriceId"));
                lblRunNo.Text = "รายการที่ :       " + RunNo.ToString();

                LkItemPrice.EditValue = sItemPriceId;
                if (ProductId == 2)
                {
                    SpinTotalPrice_Smoke.Value = Convert.ToDecimal(view.GetFocusedRowCellValue("TotalPrice_Smoke"));
                }

                //if (PriceDefault == true)
                //{
                //    SLCustomerPrice.Enabled = true;
                //}
                //else
                //{
                //    SLCustomerPrice.Enabled = false;
                //}

                //SpinUseAmount.Value = SpinTotalPrice_Raw.Value;
                //SpinWeightBalanceAmt.Value = SpinBeginAmt.Value - SpinUseAmount.Value;

                CalculateValue();
                CalEndUpdate();

                view.UpdateTotalSummary();

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
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
                    SpinBeginAmt.Value = SQLCustomer.GetUsePrice(Convert.ToInt32(_view.GetFocusedRowCellValue("PriceId")));
                    PriceDefault = Convert.ToBoolean(_view.GetFocusedRowCellValue("IsDefault"));
                    //GetItemPrice(sPriceId);

                    CalculateValue();
                    CalEndUpdate();

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

        private void SpinUseAmount_TextChanged(object sender, EventArgs e)
        {
            if (sPriceId != SQLCustomer.GetDefaultCustomerPrice(sCustomerId))
            {
                SpinWeightBalanceAmt.Value = SpinBeginAmt.Value - SpinUseAmount.Value;
            }
        }

        private bool AddDataBuyProduct(int sRunNo, int BuyProductId, int BuyId, int PriceId, int ItemPriceId, string PriceName, double Percentage, double WeightAmount,
            double WeightAmount_Plate, double Drc, decimal TotalPrice_Smoke, decimal WeightAmount_Raw, decimal TotalPrice_Raw,
            decimal CalRubber, decimal TotalPrice, string Remark, bool IsDefault)

        {
            DataTable dt_Added = dtBuyProduct;
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
                dr_New["BuyProductId"] = BuyProductId;
                dr_New["BuyId"] = BuyId;
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

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            int refBuyProductId = Convert.ToInt32(GridViewByProduct.GetFocusedRowCellValue("BuyProductId"));
            if (DeleteDataBuyProduct(refBuyProductId))
            {
                CalculateValue();
                RefreshValue();
                GridViewByProduct.UpdateTotalSummary();
            }
        }

        private bool DeleteDataBuyProduct(int id)
        {
            try
            {
                decimal TotalPriceRaw = Convert.ToDecimal(GridViewByProduct.GetFocusedRowCellValue("TotalPrice_Raw"));
                int refPriceId = Convert.ToInt32(GridViewByProduct.GetFocusedRowCellValue("PriceId"));

                if (id != 0)
                {
                    if (XtraMessageBox.Show("คุณต้องการยกเลิกรายการซื้อนี้ ใช่หรือไม่?", "ยืนยัน", MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Warning) == DialogResult.OK)
                    {
                        if (CalWeightBalanceAmt(GridViewPrice, refPriceId, TotalPriceRaw, false))
                        {
                            SQLBuy.DeleteBuyProduct(id);
                            SQLData.DeleteSelectedRows(GridViewByProduct);
                        }
                    }
                }
                else
                {
                    if (CalWeightBalanceAmt(GridViewPrice, refPriceId, TotalPriceRaw, false))
                    {
                        SQLData.DeleteSelectedRows(GridViewByProduct);      
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

        private bool GetBuyProduct(int BuyId)
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = SQLBuy.Spt_GetBuyProduct(BuyId);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow drv in dt.Rows)
                    {
                        RunNo = dtBuyProduct.Rows.Count + 1;

                        if (AddDataBuyProduct(RunNo, Convert.ToInt32(drv["BuyProductId"]), sBuyId,
                            Convert.ToInt32(drv["PriceId"]), Convert.ToInt32(drv["ItemPriceId"]),
                            Convert.ToString(drv["PriceName"]), Convert.ToDouble(drv["Percentage"]),
                            Convert.ToDouble(drv["WeightAmount"]), Convert.ToDouble(drv["WeightAmount_Plate"]),
                            Convert.ToDouble(drv["Drc"]), Convert.ToDecimal(drv["TotalPrice_Smoke"]),
                            Convert.ToDecimal(drv["WeightAmount_Raw"]), Convert.ToDecimal(drv["TotalPrice_Raw"]),
                            Convert.ToDecimal(drv["CalRubber"]), Convert.ToDecimal(drv["TotalPrice"]),
                            Convert.ToString(drv["Remark"]), Convert.ToBoolean(drv["IsDefault"])))
                        {
                            if (ProductId == 2)
                            {
                                if (CalWeightBalanceAmt(GridViewPrice, Convert.ToInt32(drv["PriceId"]), Convert.ToDecimal(drv["TotalPrice_Raw"]), true))
                                {
                                    RefreshValue();
                                    GridViewByProduct.UpdateTotalSummary();
                                    CalculateValue();
                                    CalEndUpdate();
                                }
                            }
                            else
                            {
                                RefreshValue();
                                GridViewByProduct.UpdateTotalSummary();
                                CalculateValue();
                                CalEndUpdate();
                            }
                        }
                    }
                }
                else
                {
                    dtBuyProduct = dt;
                }

                GridBuyProduct.DataSource = dtBuyProduct;

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
                    var query = context.spt_GetBuy().Where(o => o.BuyId == id).ToList();
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
                GridViewByProduct.UpdateTotalSummary();
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

        private void BtnSavePrint_Click(object sender, EventArgs e)
        {
            // 6	3 : สิทธิ์ใช้งานหน้าการซื้อ บันทึกข้อมูลการซื้อ
            int AuthorizeId = 11;

            if (SQLAuthorized.CheckAuthorized(ClassProperty.permisRoleId, AuthorizeId) == false)
            {
                SQLAuthorized.AlertAuthorized("สิทธิ์ใช้งานหน้าการซื้อ", "ไม่มีสิทธิ์บันทึกข้อมูลการซื้อ");
                return;
            }

            IsPrint = true;
            //SaveDataBuy();   
        }

        private void Payment()
        {
            try
            {
                if (GridViewByProduct.RowCount <= 0)
                {
                    XtraMessageBox.Show("ไม่มีรายการสินค้า", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (ProductId == 2)
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

                dtTempBuy.Rows.Clear();
                dtTempPrice.Rows.Clear();

                foreach (DataRow drv in dtBuyProduct.Rows)
                {
                    AddTempBuy(sBuyId, sBuyNumber, DtBuyDate.DateTime, sCustomerName, sCustomerAddress, TxtLicensePlate.Text,
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

                if (ProductId == 1)
                {
                    Load_RptBuyBill();
                }
                else
                {
                    Load_RptBuyBill1();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void BtnSaveBuy_Click(object sender, EventArgs e)
        {
            // 6	3 : สิทธิ์ใช้งานหน้าการซื้อ บันทึกข้อมูลการซื้อ
            int AuthorizeId = 11;

            if (SQLAuthorized.CheckAuthorized(ClassProperty.permisRoleId, AuthorizeId) == false)
            {
                SQLAuthorized.AlertAuthorized("สิทธิ์ใช้งานหน้าการซื้อ", "ไม่มีสิทธิ์บันทึกข้อมูลการซื้อ");
                return;
            }

            IsPrint = false;
            //SaveDataBuy();

            Payment();
        }

        private bool SaveDataBuy()
        {
            try
            {
              
                LoadMaxBuyId();
                GetReceiptNo(DtBuyDate.DateTime);

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

        private void Load_RptBuyBill()
        {
            try
            {
                string sPrinterName;
                bool IsPrinter;
                //DataSet ds = SQLReport.Spt_GetBuyBill(BuyId);

                RptBuyBill report = new RptBuyBill();
                report.DataSource = dtTempBuy;
                report.DataMember = "Datatable1";


                FrmPayment frm = new FrmPayment();
                frm.DocShowBill.DocumentSource = report;
                frm.sMessage = "คุณยืนยันที่จะปิดข้อมูลการซื้อนี้ ใช่หรือไม่?";
                frm.BtnCloseBill.Text = "ปิดการซื้อ";
                frm.PrintType = 1;

                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    sPrinterName = frm.sPrinterName;
                    IsPrinter = frm.IsPrinter;
                    if (SaveDataBuy())
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

        private void Load_RptBuyBill1()
        {
            try
            {
                string sPrinterName;
                bool IsPrinter;
                //DataSet ds = SQLReport.Spt_GetBuyBill(BuyId);

                RptBuyBill_1 report = new RptBuyBill_1();
                report.DataSource = dtTempBuy;
                report.DataMember = "Datatable1";

                subRptWeightBalance subreport = new subRptWeightBalance();
                //DataSet dss = SQLBuy.Rpt_GetWeightBalance(BuyId);
                subreport.DataSource = dtTempPrice;
                subreport.DataMember = "Datatable1";

                report.xrSubWeightBalance.ReportSource = subreport;


                FrmPayment frm = new FrmPayment();
                frm.DocShowBill.DocumentSource = report;
                frm.sMessage = "คุณยืนยันที่จะปิดข้อมูลการซื้อนี้ ใช่หรือไม่?";
                frm.BtnCloseBill.Text = "ปิดการซื้อ";
                frm.PrintType = 1;

                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    sPrinterName = frm.sPrinterName;
                    IsPrinter = frm.IsPrinter;
                    if (SaveDataBuy())
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
                if (CheckData(sBuyId) == true)
                {
                    if (UpdateBuy(Is_Finalze))
                    {
                        if (SaveDataProduct())
                        {
                            if (OpenSaveSale == true)
                            {
                                RemoveSaveBuy(sSaveBuyId);
                            }

                            XtraMessageBox.Show("บันทึกข้อมูลการซื้อสำเร็จ", "สถานะ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else
                {
                    if (AddBuy(Is_Finalze))
                    {
                        if (SaveDataProduct())
                        {
                            if (OpenSaveSale == true)
                            {
                                RemoveSaveBuy(sSaveBuyId);
                            }

                            XtraMessageBox.Show("บันทึกข้อมูลการซื้อสำเร็จ", "สถานะ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private bool AddBuy(bool Is_Finalze)
        {
            try
            {
                using (var context = new RubberSoftEntities())
                {
                    var query = context.spt_AddBuy(TxtBuyNumber.Text, sCustomerId, sCustomerName, DtBuyDate.DateTime,
                                         SpinPaynet.Value, SpinSubTotal.Value, SpinDownValue.Value, SpinNetTotal.Value, 
                                         SpinSetOffValue.Value, SpinValueBalance.Value, ProductId, CboProductUsing.Text, Is_Finalze, 
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

        private bool UpdateBuy(bool Is_Finalze)
        {
            try
            {
                using (var context = new RubberSoftEntities())
                {
                    var query = context.spt_UpdateBuy(sBuyId, TxtBuyNumber.Text, sCustomerId, sCustomerName, DtBuyDate.DateTime,
                                         SpinPaynet.Value, SpinSubTotal.Value, SpinDownValue.Value, SpinNetTotal.Value, 
                                         SpinSetOffValue.Value, SpinValueBalance.Value, ProductId, CboProductUsing.Text, 
                                         Is_Finalze, ClassProperty.permisUserID);

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
                if (GridViewByProduct.RowCount > 0)
                {
                    CheckDataBuyProduct(GridViewByProduct);
                }

                if (ProductId == 2)
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

        private bool AddBuyProduct(int BuyId, int PriceId, int ItemPriceId, string PriceName, decimal Percentage, decimal WeightAmount, decimal WeightAmount_Plate,
            decimal Drc, decimal TotalPrice_Smoke, decimal WeightAmount_Raw, decimal TotalPrice_Raw, decimal CalRubber, decimal TotalPrice, 
            string Remark)
        {
            try
            {
                using (var context = new RubberSoftEntities())
                {
                    var query = context.spt_AddBuyProduct(BuyId, PriceId, ItemPriceId, PriceName, Percentage, WeightAmount, WeightAmount_Plate, 
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

        private bool UpdateBuyProduct(int BuyProductId, int BuyId, int PriceId, int ItemPriceId, string PriceName, decimal Percentage, decimal WeightAmount, 
            decimal WeightAmount_Plate, decimal Drc, decimal TotalPrice_Smoke, decimal WeightAmount_Raw, decimal TotalPrice_Raw,
            decimal CalRubber, decimal TotalPrice, string Remark)
        {
            try
            {
                using (var context = new RubberSoftEntities())
                {
                    var query = context.spt_UpdateBuyProduct(BuyProductId, BuyId, PriceId, ItemPriceId, PriceName, Percentage, WeightAmount, 
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

        private bool CheckAddBuyProduct(int BuyProductId, int BuyId, int PriceId, int ItemPriceId, string PriceName, decimal Percentage, decimal WeightAmount, 
            decimal WeightAmount_Plate, decimal Drc, decimal TotalPrice_Smoke, decimal WeightAmount_Raw, 
            decimal TotalPrice_Raw, decimal CalRubber, decimal TotalPrice, string Remark)
        {
            try
            {

                if (BuyProductId == 0)
                {
                    AddBuyProduct(BuyId, PriceId, ItemPriceId, PriceName, Percentage, WeightAmount, WeightAmount_Plate, Drc, TotalPrice_Smoke,
                                  WeightAmount_Raw, TotalPrice_Raw, CalRubber, TotalPrice, Remark);
                }
                else
                {
                    UpdateBuyProduct(BuyProductId, BuyId, PriceId, ItemPriceId, PriceName, Percentage, WeightAmount, WeightAmount_Plate, Drc, 
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

        private bool CheckDataBuyProduct(ColumnView View)
        {
            try
            {
                // Obtain the Price column.  
                GridColumn _colBuyProductId = View.Columns.ColumnByFieldName("BuyProductId");
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

                if (_colBuyProductId == null)
                    return false;

                View.BeginSort();

                int dataRowCount = View.DataRowCount;
                // Traverse data rows and change the Price field values. 
                for (int i = 0; i < dataRowCount; i++)
                {
                    object BuyProductId = View.GetRowCellValue(i, _colBuyProductId);
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

                    CheckAddBuyProduct(Convert.ToInt32(BuyProductId), sBuyId, Convert.ToInt32(PriceId), Convert.ToInt32(ItemPriceId),
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
            if (GridViewByProduct.RowCount > 0)
            {
                XtraMessageBox.Show("มีรายการซื้อค้างอยู่ กรุณาลบรายการซื้อก่อน", "สถานะ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Load_UcCustomerType();
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
                        GridViewByProduct.UpdateTotalSummary();
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

        private void SLCustomer_EditValueChanged(object sender, EventArgs e)
        {
            if (SLCustomer.EditValue == null)
            {
                return;
            }

            if (ProductId == 2)
            {
                if (GridViewByProduct.RowCount > 0)
                {
                    if (SLCustomer.EditValue.ToString() != sCustomerId.ToString())
                    {
                        XtraMessageBox.Show("มีรายการซื้อค้างอยู่ กรุณาลบรายการซื้อก่อน", "สถานะ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    GridViewByProduct.UpdateTotalSummary();
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

        private bool SaveOutStandingBalance(int id)
        {
            try
            {
                if (AddOutStandingLog(sNetTotal, SpinNetTotal.Value, sBuyId, 0, 2))
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

                    AddTransactionLog(Convert.ToInt32(PriceId), Convert.ToDecimal(BeginAmt), useAmt, getAmt, sBuyId, 2);

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

        private void SpinWeightAmount_TextChanged(object sender, EventArgs e)
        {
            CalculateValue();
            CalEndUpdate();
        }

        private void SpinPercentage_TextChanged(object sender, EventArgs e)
        {
            if (SpinPercentage.Visible == true)
            {
                SpinDrc.Value = 100 - SpinPercentage.Value;
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

        private void SpinTotalPrice_Raw_TextChanged(object sender, EventArgs e)
        {
            CalculateValue();
            CalEndUpdate();
        }

        private void SpinTotalPrice_Raw_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void SpinWeightAmount_Plate_TextChanged(object sender, EventArgs e)
        {
            CalculateValue();
            CalEndUpdate();
        }

        private void SpinDownValue_TextChanged(object sender, EventArgs e)
        {
            CalculateValue();
            //CalEndUpdate();
        }

        private void SpinSetOffValue_TextChanged(object sender, EventArgs e)
        {
            CalculateValue();
            //CalEndUpdate();
        }

        private void SpinTotalPrice_Smoke_TextChanged(object sender, EventArgs e)
        {
            CalculateValue();
            CalEndUpdate();
        }

        private void SpinBuyDrc_TextChanged(object sender, EventArgs e)
        {
            CalculateValue();
            //CalEndUpdate();
        }

        private void CalculateValue()
        {
            try
            {
                decimal WeightAmount, Drc, WeightAmount_Plate, DownValue,
                    TotalPrice_Raw, TotalPrice, NetTotal, ValueBalance, SumTotal, SetOffValue;

                decimal TotalPrice_Smoke, WeightAmount_Raw, cutWeightAmount, CalRubber, 
                    sBuyDrc, Paynet;

                string strWeightAmount, str;
                decimal WeightAmount_Cal;

                sBuyDrc = SpinBuyDrc.Value;
                WeightAmount = SpinWeightAmount.Value;
                Drc = SpinDrc.Value;
                WeightAmount_Plate = SpinWeightAmount_Plate.Value;
                TotalPrice_Raw = SpinTotalPrice_Raw.Value;
                ValueBalance = SpinValueBalance.Value;
                DownValue = SpinDownValue.Value;
                SetOffValue = SpinSetOffValue.Value;

                if (ProductId == 1)
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

        private void BtnGetBuy_Click(object sender, EventArgs e)
        {
            Load_UcLoadBuy();
        }

        private void Load_UcLoadBuy()
        {
            try
            {
                ClassProperty.IsOpenBuy = false;

                FlyoutDialog flyout_Dialog = new FlyoutDialog(FindForm(), new UcLoadBuy());
                flyout_Dialog.Properties.Style = FlyoutStyle.Popup;
                {
                    var frm = flyout_Dialog;
                    if (frm.ShowDialog(this) == DialogResult.OK)
                    {
                        //sCustomerId = ClassProperty.sCustomerId;
                        //GetCustomer(sCustomerId);
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
            // 9	3 : 3 : สิทธิ์ใช้งานหน้าการซื้อ 	พักการซื้อ
            int AuthorizeId = 9;

            if (SQLAuthorized.CheckAuthorized(ClassProperty.permisRoleId, AuthorizeId) == false)
            {
                SQLAuthorized.AlertAuthorized("สิทธิ์ใช้งานหน้าการซื้อ", "ไม่มีสิทธิ์พักการซื้อ");
                return;
            }

            FuncSaveBuyData(0);
        }

        private void FuncSaveBuyData(int typeSave)
        {
            try
            {
                if (GridViewByProduct.RowCount <= 0)
                {
                    XtraMessageBox.Show("ไม่มีรายการสินค้า", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (CheckSaveBuyData(typeSave) == true)
                {
                    ClearSave();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private bool GetSaveBuyNumBer(DateTime date)
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = SQLBuy.Spt_GetSaveBuyNumber(date);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow drv in dt.Rows)
                    {
                        sSaveBuyNumber = Convert.ToString(drv["ReceiptNo"]);
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

        private bool CheckSaveBuyData(int typeSave)
        {
            try
            {
                GetSaveBuyNumBer(DateTime.Now);

                if (OpenSaveSale == true)
                {
                    if (CheckSaveBuy(sSaveBuyId) == true)
                    {
                        RemoveSaveBuy(sSaveBuyId);
                    }
                }

                if (AddSaveBuy(sSaveBuyNumber, typeSave))
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

        private bool CheckSaveBuy(int SaveBuyId)
        {
            try
            {
                using (var context = new RubberSoftEntities())
                {
                    var query = context.spt_GetSaveBuy().Where(o => o.Active == true &&
                    o.SaveBuyId == SaveBuyId).ToList();
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

        private bool AddSaveBuy(string SaveBuyNumber, int typeSave)
        {
            try
            {
                using (var context = new RubberSoftEntities())
                {
                    var query = context.spt_AddSaveBuy(SaveBuyNumber, sCustomerId, sCustomerName, DtBuyDate.DateTime,
                                         SpinSubTotal.Value, SpinDownValue.Value, SpinNetTotal.Value, SpinSetOffValue.Value,
                                         SpinValueBalance.Value, ProductId, CboProductUsing.Text, ClassProperty.permisUserID, typeSave);

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
                sSaveBuyId = SQLBuy.GetMaxSaveBuyId();

                if (GridViewByProduct.RowCount > 0)
                {
                    CheckSaveBuyProduct(GridViewByProduct);
                }

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool CheckAddSaveBuyProduct(int BuyId, int PriceId, int ItemPriceId, string PriceName, decimal Percentage, decimal WeightAmount,
            decimal WeightAmount_Plate, decimal Drc, decimal TotalPrice_Smoke, decimal WeightAmount_Raw,
            decimal TotalPrice_Raw, decimal CalRubber, decimal TotalPrice, string Remark)
        {
            try
            {

                AddSaveBuyProduct(BuyId, PriceId, ItemPriceId, PriceName, Percentage, WeightAmount, WeightAmount_Plate, Drc, TotalPrice_Smoke,
                                  WeightAmount_Raw, TotalPrice_Raw, CalRubber, TotalPrice, Remark);

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool AddSaveBuyProduct(int BuyId, int PriceId, int ItemPriceId, string PriceName, decimal Percentage, decimal WeightAmount, decimal WeightAmount_Plate,
            decimal Drc, decimal TotalPrice_Smoke, decimal WeightAmount_Raw, decimal TotalPrice_Raw, decimal CalRubber, decimal TotalPrice,
            string Remark)
        {
            try
            {
                using (var context = new RubberSoftEntities())
                {
                    var query = context.spt_AddSaveBuyProduct(BuyId, PriceId, ItemPriceId, PriceName, Percentage, WeightAmount, WeightAmount_Plate,
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

        private bool CheckSaveBuyProduct(ColumnView View)
        {
            try
            {
                // Obtain the Price column.  
                GridColumn _colBuyProductId = View.Columns.ColumnByFieldName("BuyProductId");
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

                if (_colBuyProductId == null)
                    return false;

                View.BeginSort();

                int dataRowCount = View.DataRowCount;
                // Traverse data rows and change the Price field values. 
                for (int i = 0; i < dataRowCount; i++)
                {
                    object BuyProductId = View.GetRowCellValue(i, _colBuyProductId);
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

                    CheckAddSaveBuyProduct(sSaveBuyId, Convert.ToInt32(PriceId), Convert.ToInt32(ItemPriceId), Convert.ToString(PriceName), Convert.ToDecimal(Percentage),
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
            // 8	3 : 3 : สิทธิ์ใช้งานหน้าการซื้อ	 ยกเลิกการซื้อ (หน้าการซื้อ)
            int AuthorizeId = 8;

            if (SQLAuthorized.CheckAuthorized(ClassProperty.permisRoleId, AuthorizeId) == false)
            {
                SQLAuthorized.AlertAuthorized("3 : สิทธิ์ใช้งานหน้าการซื้อ", "ไม่มีสิทธิ์ยกเลิกการซื้อ (หน้าการซื้อ)");
                return;
            }

            CancelBuy(sBuyId);
        }

        private bool CancelBuy(int BuyId)
        {
            try
            {
                if (CheckData(BuyId) == true)
                {
                    if (XtraMessageBox.Show("คุณต้องการยกเลิกข้อมูลการซื้อนี้ ใช่หรือไม่?", "ยืนยัน", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                    {
                        if (SQLBuy.VoidBuy(BuyId))
                        {
                            //XtraMessageBox.Show("ยกเลิกข้อมูลการซื้อสำเร็จ", "สถานะ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ClearSave();
                        }
                    }
                }
                else
                {
                    if (XtraMessageBox.Show("คุณต้องการยกเลิกข้อมูลการซื้อนี้ ใช่หรือไม่?", "ยืนยัน", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                    {
                        //XtraMessageBox.Show("ยกเลิกข้อมูลการซื้อสำเร็จ", "สถานะ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void BtnAddPrice_Click(object sender, EventArgs e)
        {
            if (SLCustomer.EditValue == null || sCustomerId == 0)
            {
                XtraMessageBox.Show("กรุณาระบุลูกค้า !", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (GridViewByProduct.RowCount > 0)
            {
                XtraMessageBox.Show("มีรายการซื้อค้างอยู่ กรุณาลบรายการซื้อก่อน", "สถานะ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Load_UcAddCustomerPrice();
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
            if (GridViewByProduct.RowCount > 0)
            {
                XtraMessageBox.Show("มีรายการซื้อค้างอยู่ กรุณาลบรายการซื้อก่อน", "สถานะ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ClassProperty.OpenSaveSaleType = 1;

            Load_OpenSaveBuy(0);
        }

        private void Load_OpenSaveBuy(int TypeSave)
        {
            try
            {
                ClassProperty.IsTypeSave = TypeSave;
                FlyoutDialog flyout_Dialog = new FlyoutDialog(FindForm(), new UcOpenSaveBuy());
                flyout_Dialog.Properties.Style = FlyoutStyle.Popup;
                {
                    var frm = flyout_Dialog;
                    if (frm.ShowDialog(this) == DialogResult.OK)
                    {
                        OpenSaveSale = true;
                        sSaveBuyId = ClassProperty.strSaveBuyId;
                        if (GetSaveBuyData(sSaveBuyId))
                        {
                            //RemoveSaveBuy(sSaveBuyId);
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

        private bool GetSaveBuyData(int SaveBuyId)
        {
            try
            {
                using (var context = new RubberSoftEntities())
                {
                    var query = context.spt_GetSaveBuy().Where(o => o.SaveBuyId == SaveBuyId).ToList();
                    if (query.Count > 0)
                    {
                        foreach (spt_GetSaveBuy_Result dt in query)
                        {
                            GetReceiptNo(DateTime.Now);
                            //TxtBuyNumber.Text = dt.BuyNumber;
                            DtBuyDate.DateTime = DateTime.Now;
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
                        GetSaveBuyProduct(SaveBuyId);
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

        private bool RemoveSaveBuy(int SaveBuyId)
        {
            try
            {
                if (SQLBuy.DeleteSaveBuy(SaveBuyId))
                {
                    SQLBuy.DeleteSaveBuyProduct(SaveBuyId);
                }

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool GetSaveBuyProduct(int BuyId)
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = SQLBuy.Spt_GetSaveBuyProduct(BuyId);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow drv in dt.Rows)
                    {
                        RunNo = dtBuyProduct.Rows.Count + 1;

                        if (AddDataBuyProduct(RunNo, 0, sBuyId,
                            Convert.ToInt32(drv["PriceId"]), Convert.ToInt32(drv["ItemPriceId"]),
                            Convert.ToString(drv["PriceName"]), Convert.ToDouble(drv["Percentage"]),
                            Convert.ToDouble(drv["WeightAmount"]), Convert.ToDouble(drv["WeightAmount_Plate"]),
                            Convert.ToDouble(drv["Drc"]), Convert.ToDecimal(drv["TotalPrice_Smoke"]),
                            Convert.ToDecimal(drv["WeightAmount_Raw"]), Convert.ToDecimal(drv["TotalPrice_Raw"]),
                            Convert.ToDecimal(drv["CalRubber"]), Convert.ToDecimal(drv["TotalPrice"]),
                            Convert.ToString(drv["Remark"]), Convert.ToBoolean(drv["IsDefault"])))
                        {
                            if (ProductId == 2)
                            {
                                if (CalWeightBalanceAmt(GridViewPrice, Convert.ToInt32(drv["PriceId"]), Convert.ToDecimal(drv["TotalPrice_Raw"]), true))
                                {
                                    RefreshValue();
                                    GridViewByProduct.UpdateTotalSummary();
                                    CalculateValue();
                                    CalEndUpdate();
                                }
                            }
                            else
                            {
                                RefreshValue();
                                GridViewByProduct.UpdateTotalSummary();
                                CalculateValue();
                                CalEndUpdate();
                            }
                        }
                    }
                }
                else
                {
                    dtBuyProduct = dt;
                }

                GridBuyProduct.DataSource = dtBuyProduct;

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
            if (GridViewByProduct.RowCount > 0)
            {
                if (XtraMessageBox.Show("มีรายการซื้อค้างอยู่ คุณต้องการยกเลิกข้อมูลการซื้อนี้ ใช่หรือไม่?", "ยืนยัน",
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

        private bool AddTempBuy(int BuyId, string BuyNumber, DateTime BuyDate, string CustomerName, 
            string CustomerAddress, string LicensePlate, string Phone, decimal BeginBalance, decimal ValueBalance, 
            decimal SubTotal, decimal DownValue, decimal SetOffValue, decimal NetTotal, string ProductUsing, DataRow drv)
        {
            DataTable dt_Added = dtTempBuy;
            if (dt_Added == null)
                return false;

            try
            {
                DataRow dr_New = dt_Added.NewRow();

                dt_Added.BeginInit();

                dr_New["BuyId"] = BuyId;
                dr_New["BuyNumber"] = BuyNumber;
                dr_New["BuyDate"] = BuyDate;
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

        private void LinkPercentage_Click(object sender, EventArgs e)
        {
            if (GridViewByProduct.RowCount <= 0)
            {
                XtraMessageBox.Show("ไม่มีรายการสินค้า", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Load_AddPercent();
        }

        private void Load_AddPercent()
        {
            try
            {
                FrmAddPercent frm = new FrmAddPercent();
                frm.dtTempBuyProduct = dtBuyProduct;

                if (frm.ShowDialog(this) == DialogResult.OK)
                {

                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void BtnOpenConsignment_Click(object sender, EventArgs e)
        {
            if (GridViewByProduct.RowCount > 0)
            {
                XtraMessageBox.Show("มีรายการซื้อค้างอยู่ กรุณาลบรายการซื้อก่อน", "สถานะ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ClassProperty.OpenSaveSaleType = 2;

            Load_OpenSaveBuy(0);
        }

        //private bool GetConsignmentNumber(DateTime date)
        //{
        //    try
        //    {
        //        DataTable dt = new DataTable();
        //        DataSet ds = SQLBuy.Spt_GetConsignmentNumber(date);
        //        dt = ds.Tables[0];
        //        if (dt.Rows.Count > 0)
        //        {
        //            foreach (DataRow drv in dt.Rows)
        //            {
        //                ConsignmentNumber = Convert.ToString(drv["ReceiptNo"]);
        //            }
        //        }

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        XtraMessageBox.Show(ex.Message);
        //        return false;
        //    }
        //}

        private void BtnConsignment_Click(object sender, EventArgs e)
        {
            // 9	3 : 3 : สิทธิ์ใช้งานหน้าการซื้อ 	พักการซื้อ
            int AuthorizeId = 9;

            if (SQLAuthorized.CheckAuthorized(ClassProperty.permisRoleId, AuthorizeId) == false)
            {
                SQLAuthorized.AlertAuthorized("สิทธิ์ใช้งานหน้าการซื้อ", "ไม่มีสิทธิ์พักการซื้อ");
                return;
            }

            FuncSaveBuyData(0);
            //Func_SaveConsignmentData();
        }

        private void BtnSaveData_Click(object sender, EventArgs e)
        {
            // 9	3 : 3 : สิทธิ์ใช้งานหน้าการซื้อ 	พักการซื้อ
            int AuthorizeId = 9;

            if (SQLAuthorized.CheckAuthorized(ClassProperty.permisRoleId, AuthorizeId) == false)
            {
                SQLAuthorized.AlertAuthorized("สิทธิ์ใช้งานหน้าการซื้อ", "ไม่มีสิทธิ์พักการซื้อ");
                return;
            }

            FuncSaveBuyData(1);
        }

        private void BtnSaveBuyDataDetails_Click(object sender, EventArgs e)
        {
            if (GridViewByProduct.RowCount > 0)
            {
                XtraMessageBox.Show("มีรายการซื้อค้างอยู่ กรุณาลบรายการซื้อก่อน", "สถานะ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ClassProperty.OpenSaveSaleType = 3;

            Load_OpenSaveBuy(1);
        }

        //private void Func_SaveConsignmentData()
        //{
        //    try
        //    {
        //        if (GridViewByProduct.RowCount <= 0)
        //        {
        //            XtraMessageBox.Show("ไม่มีรายการสินค้า", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            return;
        //        }

        //        if (Check_SaveConsignmentData() == true)
        //        {
        //            ClearSave();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        XtraMessageBox.Show(ex.Message);
        //    }
        //}

        //private bool Check_SaveConsignmentData()
        //{
        //    try
        //    {
        //        GetConsignmentNumber(DateTime.Now);

        //        if (OpenConsignment == true)
        //        {
        //            if (CheckSaveConsignment(ConsignmentId) == true)
        //            {
        //                //RemoveSaveBuy(sSaveBuyId);
        //            }
        //        }

        //        if (AddConsignment(ConsignmentNumber))
        //        {
        //            if (SaveOpenConsignment())
        //            {
        //                XtraMessageBox.Show("พักการซื้อสำเร็จ", "สถานะ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            }
        //        }

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        XtraMessageBox.Show(ex.Message);
        //        return false;
        //    }
        //}

        //private bool CheckSaveConsignment(int ConsignmentId)
        //{
        //    try
        //    {
        //        using (var context = new RubberSoftEntities())
        //        {
        //            var query = context.spt_GetConsignment().Where(o => o.Active == true &&
        //            o.ConsignmentId == ConsignmentId).ToList();
        //            if (query.Count > 0)
        //            {
        //                return true;
        //            }
        //            else
        //            {
        //                return false;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        XtraMessageBox.Show(ex.Message);
        //        return false;
        //    }
        //}

        //private bool AddConsignment(string ConsignmentNumber)
        //{
        //    try
        //    {
        //        using (var context = new RubberSoftEntities())
        //        {
        //            var query = context.spt_AddConsignment(ConsignmentNumber, sCustomerId, sCustomerName, DtBuyDate.DateTime,
        //                                 SpinSubTotal.Value, SpinDownValue.Value, SpinNetTotal.Value, SpinSetOffValue.Value,
        //                                 SpinValueBalance.Value, ProductId, CboProductUsing.Text, ClassProperty.permisUserID);

        //            return true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        XtraMessageBox.Show(ex.Message);
        //        return false;
        //    }
        //}

        //private bool SaveOpenConsignment()
        //{
        //    try
        //    {
        //        ConsignmentId = SQLBuy.GetMaxConsignmentId();

        //        if (GridViewByProduct.RowCount > 0)
        //        {
        //            CheckSaveConsignmentProduct();
        //        }

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        XtraMessageBox.Show(ex.Message);
        //        return false;
        //    }
        //}

        //private bool CheckSaveConsignmentProduct()
        //{
        //    try
        //    {
        //        //int ConsignmentProductId, ConsignmentId, PriceId, ItemPriceId;
        //        //string PriceName, Remark;
        //        //double Percentage, WeightAmount, WeightAmount_Plate, Drc;
        //        //decimal TotalPrice_Smoke, WeightAmount_Ra, TotalPrice_Raw, CalRubber, TotalPrice;
        //        //bool IsDefault;

        //        //DataTable dt = new DataTable();
        //        ////DataSet ds = SQLBuy.Spt_GetSaveBuyProduct(BuyId);
        //        ////dt = ds.Tables[0];
        //        //if (dt.Rows.Count > 0)
        //        //{
        //        //    foreach (DataRow drv in dt.Rows)
        //        //    {
        //        //        ConsignmentProductId = Convert.ToInt32(drv["ConsignmentProductId"]);
        //        //        ConsignmentId = Convert.ToInt32(drv["ConsignmentId"]);

        //        //        if (AddDataBuyProduct(RunNo, 0, sBuyId,
        //        //            Convert.ToInt32(drv["PriceId"]), Convert.ToInt32(drv["ItemPriceId"]),
        //        //            Convert.ToString(drv["PriceName"]), Convert.ToDouble(drv["Percentage"]),
        //        //            Convert.ToDouble(drv["WeightAmount"]), Convert.ToDouble(drv["WeightAmount_Plate"]),
        //        //            Convert.ToDouble(drv["Drc"]), Convert.ToDecimal(drv["TotalPrice_Smoke"]),
        //        //            Convert.ToDecimal(drv["WeightAmount_Raw"]), Convert.ToDecimal(drv["TotalPrice_Raw"]),
        //        //            Convert.ToDecimal(drv["CalRubber"]), Convert.ToDecimal(drv["TotalPrice"]),
        //        //            Convert.ToString(drv["Remark"]), Convert.ToBoolean(drv["IsDefault"])))
        //        //        {

        //        //        }
        //        //    }
        //        //}

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        XtraMessageBox.Show(ex.Message);
        //        return false;
        //    }
        //}

        //private bool CheckAddConsignmentProduct(int BuyId, int PriceId, int ItemPriceId, string PriceName, decimal Percentage, decimal WeightAmount,
        //    decimal WeightAmount_Plate, decimal Drc, decimal TotalPrice_Smoke, decimal WeightAmount_Raw,
        //    decimal TotalPrice_Raw, decimal CalRubber, decimal TotalPrice, string Remark)
        //{
        //    try
        //    {

        //        AddConsignmentProduct(BuyId, PriceId, ItemPriceId, PriceName, Percentage, WeightAmount, WeightAmount_Plate, Drc, TotalPrice_Smoke,
        //                          WeightAmount_Raw, TotalPrice_Raw, CalRubber, TotalPrice, Remark);

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        XtraMessageBox.Show(ex.Message);
        //        return false;
        //    }
        //}

        //private bool AddConsignmentProduct(int ConsignmentId, int PriceId, int ItemPriceId, string PriceName, decimal Percentage, decimal WeightAmount, decimal WeightAmount_Plate,
        //    decimal Drc, decimal TotalPrice_Smoke, decimal WeightAmount_Raw, decimal TotalPrice_Raw, decimal CalRubber, decimal TotalPrice,
        //    string Remark)
        //{
        //    try
        //    {
        //        using (var context = new RubberSoftEntities())
        //        {
        //            var query = context.spt_AddConsignmentProduct(ConsignmentId, PriceId, ItemPriceId, PriceName, Percentage, WeightAmount, WeightAmount_Plate,
        //                Drc, TotalPrice_Smoke, WeightAmount_Raw, TotalPrice_Raw, CalRubber, TotalPrice,
        //                Remark, ClassProperty.permisUserID);

        //            return true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        XtraMessageBox.Show(ex.Message);
        //        return false;
        //    }
        //}

    }
}
