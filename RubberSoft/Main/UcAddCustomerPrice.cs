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
using System.IO;
using RubberSoft.Data;
using System.Diagnostics;
using RubberSoft.Main;
using DevExpress.XtraBars.Docking2010.Customization;
using DevExpress.XtraBars.Docking2010.Views.WindowsUI;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Columns;

namespace RubberSoft.Main
{
    public partial class UcAddCustomerPrice : XtraUserControl
    {
        public UcAddCustomerPrice()
        {
            InitializeComponent();
        }

        readonly SQLCustomer SQLCustomer = new SQLCustomer();
        readonly SQLBuy SQLBuy = new SQLBuy();
        readonly SQLAuthorized SQLAuthorized = new SQLAuthorized();
        readonly SQLData SQLData = new SQLData();

        public int sCustomerId;
        DateTime getDate;
       
        public DataTable dtCustomerPrice = new DataTable();
        public DataTable dtItemPrice = new DataTable();

        private void UcAddCustomerPrice_Load(object sender, EventArgs e)
        {
            RefreshForm();
        }

        private void RefreshForm()
        {
            try
            {
                sCustomerId = ClassProperty.sCustomerId;

                // 20	5 : สิทธิ์ใช้งานหน้าสมาชิก	แก้ไขราคาล่วงหน้า
                int AuthorizeId = 20;

                if (SQLAuthorized.CheckAuthorized(ClassProperty.permisRoleId, AuthorizeId) == false)
                {
                    colSalePriceAdvance.OptionsColumn.AllowEdit = false;
                }

                ClearValues();
                GetTempustomerPrices();
                GetTempItemPrice();
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
                //sPriceId = 0;
                //sItemPriceId = 0;
                DtSaleDate.DateTime = DateTime.Now;
                DateTime sDate = DtSaleDate.DateTime;
                getDate = Convert.ToDateTime(sDate, SQLData._cultureThInfo);
                TxtPriceName.Text = getDate.ToString("dd-MMMM-yyyy");
                SpinSalePriceAdvance.Value = 0;
                SpinWeightAmount.Value = 0;
                //sWeightAmount = 0;
                SpinDeliveryPrice.Value = 0;
                SpinWeightAmount.ReadOnly = false;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void GetTempustomerPrices()
        {
            try
            {
                DataSet ds = SQLCustomer.Spt_GetCustomerPrices(0);
                dtCustomerPrice = ds.Tables[0];
                GetCustomerPrice(sCustomerId);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void GetCustomerPrice(int cus_id)
        {
            try
            {
                DateTime SaleDate;
                int PriceId;
                string PriceName;
                decimal SalePriceAdvance, WeightBalanceAmt, WeightAmount, DeliveryPrice, UnitPrice;
                bool IsDefault;

                dtCustomerPrice.Rows.Clear();
                DataTable dt = new DataTable();
                DataSet ds = SQLCustomer.Spt_GetCustomerPrices(cus_id);
                dt = ds.Tables[0];
                foreach (DataRow drv in dt.Rows)
                {
                    PriceId = Convert.ToInt32(drv["PriceId"]);
                    SaleDate = Convert.ToDateTime(drv["SaleDate"]);
                    PriceName = Convert.ToString(drv["PriceName"]);
                    SalePriceAdvance = Convert.ToDecimal(drv["SalePriceAdvance"]);
                    UnitPrice = Convert.ToDecimal(drv["UnitPrice"]);
                    WeightAmount = Convert.ToDecimal(drv["WeightAmount"]);
                    WeightBalanceAmt = Convert.ToDecimal(drv["WeightBalanceAmt"]);
                    DeliveryPrice = Convert.ToDecimal(drv["DeliveryPrice"]);
                    IsDefault = Convert.ToBoolean(drv["IsDefault"]);

                    AddDataPriceList(PriceId, SaleDate, PriceName, SalePriceAdvance, UnitPrice, WeightAmount, WeightBalanceAmt, DeliveryPrice, IsDefault);
                }

                GridCustomerPrice.DataSource = dtCustomerPrice;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void BtnAddPrice_Click(object sender, EventArgs e)
        {
            // 19	5 : สิทธิ์ใช้งานหน้าสมาชิก	บันทึกข้อมูลราคาล่วงหน้า
            int AuthorizeId = 19;

            if (SQLAuthorized.CheckAuthorized(ClassProperty.permisRoleId, AuthorizeId) == false)
            {
                SQLAuthorized.AlertAuthorized("สิทธิ์ใช้งานหน้าสมาชิก", "ไม่มีสิทธิ์บันทึกข้อมูลราคาล่วงหน้า");
                return;
            }

            if (SpinWeightAmount.Value <= 0)
            {
                XtraMessageBox.Show("กรุณาระบุราคาจำนวน", "สถานะ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                SpinWeightAmount.Focus();
                return;
            }

            DateTime SaleDate;
            int PriceId;
            string PriceName;
            decimal SalePriceAdvance, WeightAmount, WeightBalanceAmt, DeliveryPrice, UnitPrice;
            bool IsDefault;

            PriceId = 0;
            SaleDate = DtSaleDate.DateTime;
            PriceName = TxtPriceName.Text;
            SalePriceAdvance = SpinSalePriceAdvance.Value;
            UnitPrice = 0;
            WeightAmount = SpinWeightAmount.Value;
            WeightBalanceAmt = SpinWeightAmount.Value;
            DeliveryPrice = SpinDeliveryPrice.Value;
            IsDefault = false;

            if (AddDataPriceList(PriceId, SaleDate, PriceName, SalePriceAdvance, UnitPrice, WeightAmount, WeightBalanceAmt, DeliveryPrice, IsDefault))
            {
                ClearValues();
            } 
        }

        private bool AddDataPriceList(int sPriceId, DateTime sSaleDate, string PriceName, decimal sSalePriceAdvance,
                           decimal UnitPrice, decimal sWeightAmount, decimal WeightBalanceAmt, decimal sDeliveryPrice, bool IsDefault)

        {
            DataTable dt_Added = dtCustomerPrice;
            if (dt_Added == null)
                return false;

            try
            {
                DataRow dr_New = dt_Added.NewRow();

                dt_Added.BeginInit();

                dr_New["PriceId"] = sPriceId;
                dr_New["CustomerId"] = sCustomerId;
                dr_New["SaleDate"] = sSaleDate;
                dr_New["PriceName"] = PriceName;
                dr_New["SalePriceAdvance"] = sSalePriceAdvance;
                dr_New["UnitPrice"] = UnitPrice;
                dr_New["WeightAmount"] = sWeightAmount;
                dr_New["WeightBalanceAmt"] = WeightBalanceAmt;
                dr_New["DeliveryPrice"] = sDeliveryPrice;
                dr_New["sName"] = sPriceId;
                dr_New["IsDefault"] = IsDefault;
                dr_New["Active"] = 1;

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

        private void BtnSave_Click(object sender, EventArgs e)
        {
            // 19	5 : สิทธิ์ใช้งานหน้าสมาชิก	บันทึกข้อมูลราคาล่วงหน้า
            int AuthorizeId = 19;

            if (SQLAuthorized.CheckAuthorized(ClassProperty.permisRoleId, AuthorizeId) == false)
            {
                SQLAuthorized.AlertAuthorized("สิทธิ์ใช้งานหน้าสมาชิก", "ไม่มีสิทธิ์บันทึกข้อมูลราคาล่วงหน้า");
                return;
            }

            bool IsDefault = false;
            foreach (DataRow drv in dtCustomerPrice.Rows)
            {
                if (Convert.ToBoolean(drv["IsDefault"]) == true)
                {
                    IsDefault = true;
                }
            }

            if (IsDefault == false)
            {
                XtraMessageBox.Show("กรุณาระบุราคา Default", "สถานะ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (SaveData() == true)
            {
                //CheckDataBuyProduct(GridViewCustomerPrice);
                ClearValues();
                GetCustomerPrice(sCustomerId);
                XtraMessageBox.Show("บันทึกข้อมูลสำเร็จ", "สถานะ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
        }

        private bool SaveData()
        {
            try
            {
                DateTime SaleDate;
                int PriceId;
                string PriceName;
                decimal SalePriceAdvance, DeliveryPrice;
                double WeightAmount;
                bool IsDefault;

                foreach (DataRow drv in dtCustomerPrice.Rows)
                {
                    PriceId = Convert.ToInt32(drv["PriceId"]);
                    SaleDate = Convert.ToDateTime(drv["SaleDate"]);
                    PriceName = Convert.ToString(drv["PriceName"]);
                    SalePriceAdvance = Convert.ToDecimal(drv["SalePriceAdvance"]);
                    WeightAmount = Convert.ToDouble(drv["WeightAmount"]);
                    DeliveryPrice = Convert.ToDecimal(drv["DeliveryPrice"]);
                    IsDefault = Convert.ToBoolean(drv["IsDefault"]);

                    if (PriceId == 0)
                    {
                        if (SQLCustomer.AddCustomerPrice(sCustomerId, PriceName, SaleDate, SalePriceAdvance, WeightAmount, DeliveryPrice, IsDefault))
                        {
                            int _PriceId = SQLCustomer.GetMaxCustomerPriceID();

                            AddTransactionLog(Convert.ToInt32(_PriceId), Convert.ToDecimal(WeightAmount),
                                   Convert.ToDecimal(WeightAmount), 0, 1);

                            AddWeightBalance(Convert.ToInt32(_PriceId), Convert.ToDecimal(WeightAmount),
                                   Convert.ToDecimal(WeightAmount), 0);

                            SQLCustomer.Spt_AddItemPrice(_PriceId, SpinSalePriceAdvance.Value, true);
                        }
                    }
                    else
                    {
                        SQLCustomer.UpdateCustomerPrice(sCustomerId, PriceId, PriceName, SaleDate, SalePriceAdvance, WeightAmount, DeliveryPrice, IsDefault);

                        if (CheckDataTransactionLog(PriceId) == false)
                        {
                            AddTransactionLog(Convert.ToInt32(PriceId), Convert.ToDecimal(WeightAmount),
                                   Convert.ToDecimal(WeightAmount), 0, 1);
                        }

                        if (CheckDataWeightBalance(PriceId) == false)
                        {
                            AddWeightBalance(Convert.ToInt32(PriceId), Convert.ToDecimal(WeightAmount),
                                    Convert.ToDecimal(WeightAmount), 0);
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

        private bool CheckDataTransactionLog(int id)
        {
            try
            {
                using (var context = new RubberSoftEntities())
                {
                    var query = context.spt_GetTransactionLog().Where(o => o.PriceId == id).ToList();
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

        private bool AddTransactionLog(int PriceId, decimal WeightAmount_Raw,
                                       decimal WeightBalanceAmt, int RefId, int LogTypeId)
        {
            try
            {
                using (var context = new RubberSoftEntities())
                {
                    var query = context.spt_AddTransactionLog(sCustomerId, PriceId, 0, WeightAmount_Raw,
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

        private bool AddWeightBalance(int PriceId, decimal BeginBalanceAmt, decimal WeightBalanceAmt, int LastLogId)
        {
            try
            {
                using (var context = new RubberSoftEntities())
                {
                    var query = context.spt_AddWeightBalance(sCustomerId, PriceId, BeginBalanceAmt,
                                        WeightBalanceAmt, LastLogId);

                    return true;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.FindForm().DialogResult = DialogResult.OK;
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            // 21	5 : สิทธิ์ใช้งานหน้าสมาชิก	ลบข้อมูลราคาล่วงหน้า
            int AuthorizeId = 21;

            if (SQLAuthorized.CheckAuthorized(ClassProperty.permisRoleId, AuthorizeId) == false)
            {
                SQLAuthorized.AlertAuthorized("สิทธิ์ใช้งานหน้าสมาชิก", "ไลบข้อมูลราคาล่วงหน้า");
                return;
            }

            int pid = Convert.ToInt32(GridViewCustomerPrice.GetFocusedRowCellValue("PriceId"));
            bool IsDefault = Convert.ToBoolean(GridViewCustomerPrice.GetFocusedRowCellValue("IsDefault"));

            if (IsDefault == true)
            {
                XtraMessageBox.Show("ไม่สามารถลบราคา Default ได้", "สถานะ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (pid == 0)
            {
                GridViewCustomerPrice.DeleteSelectedRows();
            }
            else
            {
                if (XtraMessageBox.Show("คุณยืนยันที่จะลบข้อมูล ใช่หรือไม่?", "ยืนยัน", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    if (DeleteData(pid))
                    {
                        RefreshForm();
                    }
                }
            }
        }

        private bool DeleteData(int id)
        {
            try
            {
                DataSet ds = SQLBuy.DeleteCustomerPrice(id);

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private void GridViewCustomerPrice_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (!(sender is GridView view)) return;
            int PriceId = Convert.ToInt32(view.GetFocusedRowCellValue("PriceId"));
            //if (GetData(PriceId))
            //{
            //    Get_DataItemPrice(PriceId);
            //}
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            int PriceId = Convert.ToInt32(GridViewCustomerPrice.GetFocusedRowCellValue("PriceId"));
            if (PriceId == 0)
            {
                XtraMessageBox.Show("กรุณาบันทึกข้อมูลก่อน", "สถานะ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            GetData(PriceId);
        }

        private bool GetData(int PriceId)
        {
            try
            {
                DateTime sDate = DtSaleDate.DateTime;
                getDate = Convert.ToDateTime(sDate, SQLData._cultureThInfo);

                foreach (DataRow drv in dtCustomerPrice.Rows)
                {
                    string sPriceName;

                    if (PriceId == Convert.ToInt32(drv["PriceId"]))
                    {
                        //sPriceId = Convert.ToInt32(drv["PriceId"]);
                        //DtSaleDate.DateTime = Convert.ToDateTime(drv["SaleDate"]);                       
                        //SpinWeightAmount.Value = Convert.ToDecimal(drv["WeightAmount"]);
                        //sWeightAmount = Convert.ToDecimal(drv["WeightAmount"]);
                        //SpinDeliveryPrice.Value = Convert.ToDecimal(drv["DeliveryPrice"]);

                        sPriceName = drv["PriceName"].ToString();

                        if (sPriceName == "NULL")
                        {
                            sPriceName = getDate.ToString("dd-MMMM-yyyy");
                        }

                        TxtPriceName.Text = sPriceName;
                        SpinWeightAmount.ReadOnly = true;

                        if (Get_DataItemPrice(PriceId))
                        {
                            Load_AddItemPrice(PriceId, sPriceName);
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

        private void Load_AddItemPrice(int PriceId, string PriceName)
        {
            try
            {
                FrmAddPrice frm = new FrmAddPrice();
                frm.dtItemPrice = dtItemPrice;
                frm.lblPriceName.Text = PriceName;
                frm.sPriceId = PriceId;
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    //Get_DataItemPrice(PriceId);
                    //ClearValues();
                    //GetCustomerPrice(sCustomerId);
                    //CheckDataBuyProduct(GridViewCustomerPrice);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void GetTempItemPrice()
        {
            try
            {
                DataSet ds = SQLCustomer.Spt_GetItemPrice(0);
                dtItemPrice = ds.Tables[0];
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private bool Get_DataItemPrice(int PriceId)
        {
            try
            {
                SpinSalePriceAdvance.Value = 0;
                dtItemPrice.Rows.Clear();

                DataTable dt = new DataTable();
                DataSet ds = SQLCustomer.Spt_GetItemPrice(PriceId);
                dt = ds.Tables[0];

                //LkItemPrice.Properties.DataSource = dt;
                //LkItemPrice.Properties.DisplayMember = "sUnitPrice";
                //LkItemPrice.Properties.ValueMember = "ItemPriceId";
                //LkItemPrice.Properties.PopulateColumns();
                //LkItemPrice.Properties.AppearanceDropDown.TextOptions.HAlignment = HorzAlignment.Center;
                //LkItemPrice.Properties.Columns["ItemPriceId"].Visible = false;
                //LkItemPrice.Properties.Columns["PriceId"].Visible = false;
                //LkItemPrice.Properties.Columns["UnitPrice"].Visible = false;
                //LkItemPrice.Properties.Columns["Active"].Visible = false;
                //LkItemPrice.Properties.Columns["IsNew"].Visible = false;
                //LkItemPrice.Properties.Columns["sUnitPrice"].Caption = "ราคาล่วงหน้า";
                //LkItemPrice.Properties.Columns["sUnitPrice"].Alignment = HorzAlignment.Center;
                //LkItemPrice.Properties.Columns["IsDefault"].Alignment = HorzAlignment.Center;
                //LkItemPrice.Properties.Columns["sUnitPrice"].Width = 100;
                //LkItemPrice.Properties.Columns["IsDefault"].Width = 80;      
                //LkItemPrice.EditValue = 0;

                if (dt.Rows.Count > 0)
                {   
                    foreach (DataRow drv in dt.Rows)
                    {
                        if (Convert.ToBoolean(drv["IsDefault"]) == true)
                        {
                            SpinSalePriceAdvance.Value = Convert.ToDecimal(drv["UnitPrice"]);
                            //LkItemPrice.EditValue = Convert.ToInt32(drv["ItemPriceId"]);
                        }

                        AddItemPrice(drv);
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

        private bool AddItemPrice(DataRow drv)
        {
            try
            {
                DataTable dt_Added = dtItemPrice;
                if (dt_Added == null)
                    return false;

                DataRow dr_New = dt_Added.NewRow();

                dt_Added.BeginInit();

                dr_New["ItemPriceId"] = drv["ItemPriceId"];
                dr_New["PriceId"] = drv["PriceId"];
                dr_New["UnitPrice"] = drv["UnitPrice"];
                dr_New["IsDefault"] = drv["IsDefault"];
                dr_New["Active"] = drv["Active"];
                dr_New["IsNew"] = drv["IsNew"];

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

        //private bool CheckDataBuyProduct(ColumnView View)
        //{
        //    try
        //    {

        //        // Obtain the Price column.  
        //        GridColumn _colPriceId = View.Columns.ColumnByFieldName("PriceId");
        //        GridColumn _colSalePriceAdvance = View.Columns.ColumnByFieldName("SalePriceAdvance");
        //        GridColumn _colWeightAmount = View.Columns.ColumnByFieldName("WeightAmount");
        //        GridColumn _colDeliveryPrice = View.Columns.ColumnByFieldName("DeliveryPrice");

        //        if (_colPriceId == null)
        //            return false;

        //        View.BeginSort();

        //        int dataRowCount = View.DataRowCount;
        //        // Traverse data rows and change the Price field values. 
        //        for (int i = 0; i < dataRowCount; i++)
        //        {
        //            object PriceId = View.GetRowCellValue(i, _colPriceId);
        //            object SalePriceAdvance = View.GetRowCellValue(i, _colSalePriceAdvance);
        //            object WeightAmount = View.GetRowCellValue(i, _colWeightAmount);
        //            object DeliveryPrice = View.GetRowCellValue(i, _colDeliveryPrice);

        //            if (CheckDataTransactionLog(Convert.ToInt32(PriceId)) == false)
        //            {
        //                AddTransactionLog(Convert.ToInt32(PriceId), Convert.ToDecimal(WeightAmount),
        //                    Convert.ToDecimal(WeightAmount), 0, 1);
        //            }

        //            if (CheckDataWeightBalance(Convert.ToInt32(PriceId)) == false)
        //            {
        //                AddWeightBalance(Convert.ToInt32(PriceId), Convert.ToDecimal(WeightAmount),
        //                    Convert.ToDecimal(WeightAmount), 0);
        //            }

        //            //View.SetRowCellValue(i, _colค่าดำเนินการยกมา, ACCquoted);
        //        }

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        XtraMessageBox.Show(ex.Message);
        //        return false;
        //    }
        //    finally
        //    {
        //        View.EndSort();
        //    }
        //}

        private void GridViewCustomerPrice_CellValueChanging(object sender, CellValueChangedEventArgs e)
        {
            if (e.RowHandle < 0) return;
            if (!(sender is GridView view)) return;

            if (view.FocusedColumn.Name == "colIsDefault")
            {
                DataRow dr_Selected = view.GetDataRow(e.RowHandle);
                bool ck = Convert.ToBoolean(dr_Selected["IsDefault"]);
                int id = Convert.ToInt32(dr_Selected["PriceId"]);

                foreach (DataRow drv in dtCustomerPrice.Rows)
                {
                    if (Convert.ToInt32(drv["PriceId"]) != id)
                    {
                        drv["IsDefault"] = false;
                    }
                }
            }
        }

        //private bool CheckData(int id)
        //{
        //    try
        //    {
        //        using (var context = new RubberSoftEntities())
        //        {
        //            var query = context.spt_GetCustomerPrice().Where(o => o.PriceId == id).ToList();
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

    }
}
