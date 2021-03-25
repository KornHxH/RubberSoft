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
using DevExpress.XtraEditors.Repository;

namespace RubberSoft.Main
{
    public partial class UcCustomerType : XtraUserControl
    {
        public UcCustomerType()
        {
            InitializeComponent();
        }

        readonly SQLCustomer SQLCustomer = new SQLCustomer();
        readonly SQLData SQLData = new SQLData();
        readonly SQLBuy SQLBuy = new SQLBuy();
        readonly SQLSale SQLSale = new SQLSale();
        readonly SQLAuthorized SQLAuthorized = new SQLAuthorized();

        public int sCustomerTypeId, stCustomerTypeId, CustomerGroupId, sCustomerGroupId, sCustomerId, sPriceId, sOutStandingLogId;
        public string sCustomerCode, sCustomerName, stCustomerCode, stCustomerName;
        public DataTable dtCustomer = new DataTable();
        private decimal sNetTotal;
        bool _IsDefault;

        private void UcCustomerType_Load(object sender, EventArgs e)
        {
            RefreshForm();
        }

        private void RefreshForm()
        {
            try
            {
                ClearValues();
                LoadLkCustomerTypes();
                LoadLkSearchCustomerType();
                LoadLkSearchCustomerGroup(sCustomerTypeId);
                LoadLkCustomerGroup(sCustomerTypeId);
                LoadCustomer();
                sCustomerId = ClassProperty.sCustomerId;
                if (sCustomerId != 0)
                {
                    GetData(sCustomerId);
                    GridViewCustomer.FocusedRowHandle = GridViewCustomer.LocateByValue("CustomerName", stCustomerName);
                }
                if (ClassProperty.IsCusType == 0)
                {
                    BtnSelect.Visible = false;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void GenCustomerCode()
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = SQLCustomer.Spt_GenCustomerCode();
                dt = ds.Tables[0];
                foreach (DataRow drv in dt.Rows)
                {
                    TxtCustomerCode.Text = drv["CustomerCode"].ToString();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            ClearValues();
        }

        private void ClearValues()
        {
            try
            {
                sCustomerTypeId = ClassProperty.IsCusType;
                sCustomerGroupId = ClassProperty.CustomerGroupId;
                sCustomerId = 0;
              
                sNetTotal = 0;

                TxtCustomerName.Select();
                TxtCustomerName.Text = "";
                TxtCustomerAddress.Text = "";
                TxtLicensePlate.Text = "";
                TxtPhone.Text = "";

                sCustomerCode = "";
                sCustomerName = "";
                stCustomerCode = "";
                stCustomerName = "";
                _IsDefault = false;

                SpinNetTotal.ReadOnly = true;
                BtnAdjust.Enabled = false;
                BtnSaveAdjust.Enabled = false;

                GenCustomerCode();
                LoadCustomerPrices(0);
                SetDefaultCustomer(false);
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
                DataTable dt = new DataTable();
                DataSet ds = SQLBuy.Spt_GetBuyWeightBalance(cus_id);
                dt = ds.Tables[0];
                GridCustomerPrice.DataSource = dt;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadCustomer();
        }

        private bool LoadCustomer()
        {
            try
            {
                string sText = "";
                sText = TxtSearch.Text.Trim();

                if (LkSearchCustomerGroup.EditValue == null)
                {
                    sCustomerGroupId = 0;
                }

                DataSet ds = SQLCustomer.Spt_GetCustomers(sText, sCustomerTypeId, sCustomerGroupId);
                dtCustomer = ds.Tables[0];

                GridCustomer.DataSource = dtCustomer;

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private void GridViewCustomer_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (!(sender is GridView view)) return;
            sCustomerId = Convert.ToInt32(view.GetFocusedRowCellValue("CustomerId"));
            GetData(sCustomerId);
        }

        private bool GetData(int id)
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
                            TxtCustomerCode.Text = dt.CustomerCode;
                            stCustomerCode = dt.CustomerCode;
                            TxtCustomerName.Text = dt.CustomerName;
                            stCustomerName = dt.CustomerName;
                            TxtLicensePlate.Text = dt.LicensePlate;
                            TxtPhone.Text = dt.Phone;
                            TxtCustomerAddress.Text = dt.CustomerAddress;
                            stCustomerTypeId = dt.CustomerTypeId.Value;
                            _IsDefault = dt.IsDefault;

                            if (_IsDefault == true)
                            {
                                LkCustomerTypes.EditValue = 0;
                                LkCustomerGroup.EditValue = 0;
                                SetDefaultCustomer(true);
                            }
                            else
                            {
                                LkCustomerTypes.EditValue = dt.CustomerTypeId;
                                LkCustomerGroup.EditValue = dt.CustomerGroupId;
                                SetDefaultCustomer(false);
                            }

                            SpinNetTotal.ReadOnly = true;
                            BtnAdjust.Enabled = true;

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

        private bool SetDefaultCustomer(bool set)
        {
            try
            {
                TxtCustomerName.ReadOnly = set;
                TxtLicensePlate.ReadOnly = set;
                TxtPhone.ReadOnly = set;
                TxtCustomerAddress.ReadOnly = set;
                LkCustomerTypes.ReadOnly = set;
                LkCustomerGroup.ReadOnly = set;

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
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
                        SpinNetTotal.Value = Convert.ToDecimal(drv["NetTotal"]);                 
                    }
                }
                else
                {
                    sNetTotal = 0;
                    SpinNetTotal.Value = 0;
                }

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
                    var query = context.spt_GetCustomer().Where(o => o.CustomerId == id).ToList();
                    if (query.Count > 0)
                    {
                        foreach (spt_GetCustomer_Result dt in query)
                        {
                            sCustomerId = dt.CustomerId;
                        }

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

        private void BtnSave_Click(object sender, EventArgs e)
        {
            // 16	5 : สิทธิ์ใช้งานหน้าสมาชิก	บันทึกข้อมูลสมาชิก
            int AuthorizeId = 16;

            if (SQLAuthorized.CheckAuthorized(ClassProperty.permisRoleId, AuthorizeId) == false)
            {
                SQLAuthorized.AlertAuthorized("สิทธิ์ใช้งานหน้าสมาชิก", "ไม่มีสิทธิ์บันทึกข้อมูลสมาชิก");
                return;
            }

            if (stCustomerTypeId == 0)
            {
                XtraMessageBox.Show("กรุณาระบุประเภทลูกค้า !", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                LkCustomerTypes.ShowPopup();
                return;
            }
            if (LkCustomerGroup.EditValue == null)
            {
                XtraMessageBox.Show("กรุณาระบุกลุ่มลูกค้า !", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                LkCustomerGroup.ShowPopup();
                return;
            }

            if (TxtCustomerName.Text.Trim() == string.Empty)
            {
                XtraMessageBox.Show("กรุณาระบุชื่อลูกค้า !", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TxtCustomerName.Focus();
                return;
            }

            sCustomerCode = TxtCustomerCode.Text;
            sCustomerName = TxtCustomerName.Text;

            if (SaveData() == true)
            {
                LoadCustomer();
                GridViewCustomer.FocusedRowHandle = GridViewCustomer.LocateByValue("CustomerName", TxtCustomerName.Text);
                sCustomerId = Convert.ToInt32(GridViewCustomer.GetFocusedRowCellValue("CustomerId"));
                if (CheckCustomerPrice(sCustomerId))
                {
                    GetData(sCustomerId);
                }
            }
        }

        private bool SaveData()
        {
            try
            {
                if (CheckData(sCustomerId) == true)
                {
                    if (CheckDuplicate(sCustomerId) == false)
                    {
                        if (UpdateData() == true)
                        {
                            if (SaveOutStandingBalance(sCustomerId))
                            {
                                XtraMessageBox.Show("บันทึกข้อมูลสำเร็จ", "สถานะ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
                else
                {
                    if (CheckDuplicate(sCustomerId) == false)
                    {
                        GenCustomerCode();

                        if (AddData() == true)
                        {
                            if (SaveOutStandingBalance(sCustomerId))
                            {
                                XtraMessageBox.Show("เพิ่มข้อมูลสำเร็จ", "สถานะ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private bool CheckDuplicate(int id)
        {
            try
            {
                using (var context = new RubberSoftEntities())
                {
                    if (CheckData(id) == true)
                    {
                        //if (sCustomerName != stCustomerName)
                        //{
                        //    var query = context.spt_GetCustomer().Where(o => o.CustomerName == sCustomerName).ToList();
                        //    if (query.Count > 0)
                        //    {
                        //        XtraMessageBox.Show("ชื่อนี้มีในระบบแล้ว !", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //        return true;
                        //    }
                        //}
                    }
                    else
                    {
                        //if (sCustomerName != "")
                        //{
                        //    var query = context.spt_GetCustomer().Where(o => o.CustomerName == sCustomerName).ToList();
                        //    if (query.Count > 0)
                        //    {
                        //        XtraMessageBox.Show("ชื่อนี้มีในระบบแล้ว !", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //        return true;
                        //    }
                        //}
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool AddData()
        {
            try
            {
                using (var context = new RubberSoftEntities())
                {
                    var query = context.spt_AddCustomer(stCustomerTypeId, CustomerGroupId, sCustomerCode, sCustomerName, TxtCustomerAddress.Text, 
                        TxtLicensePlate.Text, TxtPhone.Text, false, ClassProperty.permisUserID);

                    return true;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool UpdateData()
        {
            try
            {
            
                using (var context = new RubberSoftEntities())
                {
                    var query = context.spt_UpdateCustomer(sCustomerId, stCustomerTypeId, CustomerGroupId, sCustomerCode, sCustomerName, 
                        TxtCustomerAddress.Text, TxtLicensePlate.Text, TxtPhone.Text, false, ClassProperty.permisUserID);

                    return true;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            int cid = Convert.ToInt32(GridViewCustomer.GetFocusedRowCellValue("CustomerId"));

            // 17	5 : สิทธิ์ใช้งานหน้าสมาชิก	ลบข้อมูลสมาชิก
            int AuthorizeId = 17;

            if (SQLAuthorized.CheckAuthorized(ClassProperty.permisRoleId, AuthorizeId) == false)
            {
                SQLAuthorized.AlertAuthorized("สิทธิ์ใช้งานหน้าสมาชิก", "ไม่มีสิทธิ์ลบข้อมูลสมาชิก");
                return;
            }

            if (CheckData(cid) == true)
            {
                if (XtraMessageBox.Show("คุณยืนยันที่จะลบข้อมูล ใช่หรือไม่?", "ยืนยัน", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    if (DeleteData(cid))
                    {
                        LoadCustomer();
                    }
                }
            }
        }

        private bool DeleteData(int cid)
        {
            try
            {
                using (var context = new RubberSoftEntities())
                {
                    var query = context.spt_DeleteCustomer(cid, ClassProperty.permisUserID);

                    return true;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool CheckCustomerPrice(int CustomerId)
        {
            try
            {
                DateTime getDate;
                DateTime sDate = DateTime.Now;
                getDate = Convert.ToDateTime(sDate, SQLData._cultureThInfo);
                //string PriceName = getDate.ToString("dd-MMMM-yyyy");
                string PriceName = "สด";

                DataTable dt = new DataTable();
                DataSet ds = SQLCustomer.Spt_CheckCustomerPrice(CustomerId);
                dt = ds.Tables[0];
                if (dt.Rows.Count <= 0)
                {
                    if (SQLCustomer.AddCustomerPrice(CustomerId, PriceName, sDate, 0, 0, 0, true))
                    {
                        int _PriceId = SQLCustomer.GetMaxCustomerPriceID();
                        SQLCustomer.Spt_AddItemPrice(_PriceId, 0, true);
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
            // 18	5 : สิทธิ์ใช้งานหน้าสมาชิก	เข้าใช้งานหน้าราคาล่วงหน้า
            int AuthorizeId = 18;

            if (SQLAuthorized.CheckAuthorized(ClassProperty.permisRoleId, AuthorizeId) == false)
            {
                SQLAuthorized.AlertAuthorized("สิทธิ์ใช้งานหน้าสมาชิก", "ไม่มีสิทธิ์เข้าใช้งานหน้าราคาล่วงหน้า");
                return;
            }

            if (CheckData(sCustomerId) == true)
            {
                ClassProperty.sCustomerId = sCustomerId;
                Load_UcAddCustomerPrice();
            }  
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

        private void LoadLkCustomerTypes()
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();

                if (ClassProperty.IsCusType == 1)
                {
                    ds = SQLBuy.GetBuyCustomerTypes();
                }
                if (ClassProperty.IsCusType == 2)
                {
                    ds = SQLSale.GetSaleCustomerTypes();
                }
                if (ClassProperty.IsCusType == 0)
                {
                    ds = SQLCustomer.GetLkCustomerTypes();
                }

                dt = ds.Tables[0];

                LkCustomerTypes.Properties.DataSource = dt;
                LkCustomerTypes.Properties.DisplayMember = "CustomerTypeName";
                LkCustomerTypes.Properties.ValueMember = "CustomerTypeId";
                LkCustomerTypes.Properties.PopulateColumns();
                LkCustomerTypes.Properties.Columns["CustomerTypeId"].Visible = false;
                LkCustomerTypes.EditValue = sCustomerTypeId;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void LoadLkSearchCustomerType()
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();

                if (ClassProperty.IsCusType == 1)
                {
                    ds = SQLBuy.GetBuyCustomerTypes();
                }
                if (ClassProperty.IsCusType == 2)
                {
                    ds = SQLSale.GetSaleCustomerTypes();
                }
                if (ClassProperty.IsCusType == 0)
                {
                    ds = SQLCustomer.GetLkCustomerTypes();
                }

                dt = ds.Tables[0];

                LkSearchCustomerType.Properties.DataSource = dt;
                LkSearchCustomerType.Properties.DisplayMember = "CustomerTypeName";
                LkSearchCustomerType.Properties.ValueMember = "CustomerTypeId";
                LkSearchCustomerType.Properties.PopulateColumns();
                LkSearchCustomerType.Properties.Columns["CustomerTypeId"].Visible = false;
                LkSearchCustomerType.EditValue = sCustomerTypeId;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void LoadLkSearchCustomerGroup(int ProductTypeId)
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = SQLCustomer.GetLkProducts(ProductTypeId);
                dt = ds.Tables[0];

                LkSearchCustomerGroup.Properties.DataSource = dt;
                LkSearchCustomerGroup.Properties.DisplayMember = "ProductName";
                LkSearchCustomerGroup.Properties.ValueMember = "ProductId";
                LkSearchCustomerGroup.Properties.PopulateColumns();
                LkSearchCustomerGroup.Properties.Columns["ProductId"].Visible = false;
                LkSearchCustomerGroup.Properties.Columns["ProductTypeId"].Visible = false;
                LkSearchCustomerGroup.EditValue = null;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void LoadLkCustomerGroup(int ProductTypeId)
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = SQLSale.GetLkProducts(ProductTypeId);
                dt = ds.Tables[0];

                LkCustomerGroup.Properties.DataSource = dt;
                LkCustomerGroup.Properties.DisplayMember = "ProductName";
                LkCustomerGroup.Properties.ValueMember = "ProductId";
                LkCustomerGroup.Properties.PopulateColumns();
                LkCustomerGroup.Properties.Columns["ProductId"].Visible = false;
                LkCustomerGroup.Properties.Columns["ProductTypeId"].Visible = false;
                LkCustomerGroup.EditValue = null;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void BtnAdjust_Click(object sender, EventArgs e)
        {
            // 22	5 : สิทธิ์ใช้งานหน้าสมาชิก	ปรับยอดคงเหลือสมาชิก
            int AuthorizeId = 22;

            if (SQLAuthorized.CheckAuthorized(ClassProperty.permisRoleId, AuthorizeId) == false)
            {
                SQLAuthorized.AlertAuthorized("สิทธิ์ใช้งานหน้าสมาชิก", "ไม่มีสิทธิ์ปรับยอดคงเหลือสมาชิก");
                return;
            }

            SpinNetTotal.ReadOnly = false;
            BtnSaveAdjust.Enabled = true;
        }

        decimal DiffValue;

        private void SpinNetTotal_TextChanged(object sender, EventArgs e)
        {
            CalAdjust(SpinNetTotal.Value);
        }

        private bool CalAdjust(decimal value)
        {
            try
            {
                DiffValue = value - sNetTotal;
                SpinDiff.Value = DiffValue;

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private void BtnSaveAdjust_Click(object sender, EventArgs e)
        {
            if (CheckData(sCustomerId) == true)
            {
               if (SaveOutStandingBalance(sCustomerId))
                {
                    XtraMessageBox.Show("บันทึกข้อมูลสำเร็จ", "สถานะ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SpinNetTotal.ReadOnly = true;
                    BtnSaveAdjust.Enabled = false;
                    GetValueBalance(sCustomerId);
                    CalAdjust(SpinNetTotal.Value);
                }
            }
        }

        private bool SaveOutStandingBalance(int id)
        {
            try
            {
                if (AddOutStandingLog(sNetTotal, SpinNetTotal.Value, 0, 0, 6))
                {
                    sOutStandingLogId = SQLBuy.GetOutStandingLogId();
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

        private void LkSearchCustomerType_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit lookUp = sender as LookUpEdit;
            if (lookUp.GetSelectedDataRow() is DataRowView dataRow)
            {
                if (dataRow != null)
                {
                    sCustomerTypeId = Convert.ToInt32(dataRow["CustomerTypeId"]);
                }

                LoadLkSearchCustomerGroup(sCustomerTypeId);
                LoadCustomer();
            }
        }

        private void LkSearchCustomerGroup_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit lookUp = sender as LookUpEdit;
            if (lookUp.GetSelectedDataRow() is DataRowView dataRow)
            {
                if (dataRow != null)
                {
                    sCustomerGroupId = Convert.ToInt32(dataRow["ProductId"]);
                }

                LoadCustomer();
            }
        }

        private void LkCustomerTypes_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit lookUp = sender as LookUpEdit;
            if (lookUp.GetSelectedDataRow() is DataRowView dataRow)
            {
                if (dataRow != null)
                {
                    stCustomerTypeId = Convert.ToInt32(dataRow["CustomerTypeId"]);
                }

                LoadLkCustomerGroup(stCustomerTypeId);
            }
        }

        private void LkCustomerGroup_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit lookUp = sender as LookUpEdit;
            if (lookUp.GetSelectedDataRow() is DataRowView dataRow)
            {
                if (dataRow != null)
                {
                    CustomerGroupId = Convert.ToInt32(dataRow["ProductId"]);
                }
            }
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.FindForm().DialogResult = DialogResult.Cancel;
        }

        private void BtnSelect_Click(object sender, EventArgs e)
        {
            if (CheckData(sCustomerId) == true)
            {
                ClassProperty.sCustomerId = sCustomerId;
                this.FindForm().DialogResult = DialogResult.OK;
            }
            else
            {
                XtraMessageBox.Show("กรุณาเลือกลูกค้า", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void GridViewCustomer_DoubleClick(object sender, EventArgs e)
        {
            if (!(sender is GridView view)) return;
            sCustomerId = Convert.ToInt32(view.GetFocusedRowCellValue("CustomerId"));

            if (ClassProperty.IsOpenBuy == false)
            {
                if (CheckData(sCustomerId) == true)
                {
                    ClassProperty.sCustomerId = sCustomerId;
                    this.FindForm().DialogResult = DialogResult.OK;
                }
                else
                {
                    XtraMessageBox.Show("กรุณาเลือกลูกค้า", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        //private DataTable PriceList(int PriceId)
        //{
        //    try
        //    {
        //        DataTable dt = new DataTable();
        //        DataSet ds = SQLCustomer.Spt_GetLkItemPrice(PriceId);
        //        dt = ds.Tables[0];

        //        return dt;
        //    }
        //    catch (Exception ex)
        //    {
        //        XtraMessageBox.Show(ex.Message);
        //        return null;
        //    }
        //}

        private void GridViewCustomerPrice_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            if (!(sender is GridView view)) return;
            if (e.Column.FieldName == "UnitPrice")
            {
                //int PriceId = Convert.ToInt32(view.GetRowCellValue(e.RowHandle, "PriceId").ToString());
                //RepositoryItemLookUpEdit riLookup = new RepositoryItemLookUpEdit();

                //if (riLookup.DataSource == null)
                //{
                //    riLookup.DataSource = PriceList(PriceId);
                //    riLookup.DisplayMember = "UnitPrice";
                //    riLookup.ValueMember = "ItemPriceId";
                //    riLookup.PopulateColumns();
                //    riLookup.AppearanceDropDown.TextOptions.HAlignment = HorzAlignment.Center;
                //    riLookup.Columns["ItemPriceId"].Visible = false;
                //    riLookup.Columns["UnitPrice"].Caption = "ราคาล่วงหน้า";
                //    //riLookup.AutoSearchColumnIndex = 0;

                //    //if (editor.Items.Count == 0)
                //    //{
                //    //    if (view.GetRowCellValue(e.RowHandle, "PriceId") != null)
                //    //    {
                //    //        if (true)
                //    //        {
                //    //            int PriceId = Convert.ToInt32(view.GetRowCellValue(e.RowHandle, "PriceId").ToString());
                //    //            //DataTable dt = PriceList(PriceId);
                //    //            //if (dt.Rows.Count > 0)
                //    //            //{
                //    //            //    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                //    //            //        editor.Items.Add(dt.Rows[i][0].ToString());
                //    //            //}
                //    //        }
                //    //    }
                //    //}

                //    e.RepositoryItem = riLookup;
                //}
            }
        }

        private void GridViewCustomer_RowStyle(object sender, RowStyleEventArgs e)
        {
            GridView view = sender as GridView;

            e.Appearance.ForeColor = Color.Black;
            e.Appearance.BackColor = Color.White;

            view.Appearance.FocusedRow.ForeColor = Color.White;
            view.Appearance.FocusedRow.BackColor = Color.FromArgb(106, 141, 59);

            //if (((e.State & GridRowCellState.Focused) != 0) && ((e.State & GridRowCellState.GridFocused) != 0))
            if (view.FocusedRowHandle == e.RowHandle)
                e.Appearance.Assign(view.PaintAppearance.FocusedRow);

            e.HighPriority = true;   // override any other formatting 
        }

    }
}
