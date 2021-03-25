using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using RubberSoft.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RubberSoft.Main
{
    public partial class FrmAddPrice : XtraForm
    {
        public FrmAddPrice()
        {
            InitializeComponent();
        }

        readonly SQLCustomer SQLCustomer = new SQLCustomer();
        readonly SQLData SQLData = new SQLData();

        public int sCustomerId, sPriceId, sItemPriceId;
       
        public DataTable dtItemPrice = new DataTable();
        public DataTable dtAddItemPrice = new DataTable();

        private void FrmAddPrice_Load(object sender, EventArgs e)
        {
            RefreshForm();
        }

        private void RefreshForm()
        {
            try
            {
                ClearValues();
                GetItemPrice(0);
                Get_DataItemPrice(dtItemPrice);
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
                SpinUnitPrice.Value = 0;
                sItemPriceId = 0;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void GetItemPrice(int PriceId)
        {
            try
            {
                DataSet ds = SQLCustomer.Spt_GetItemPrice(PriceId);
                dtAddItemPrice = ds.Tables[0];
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void Get_DataItemPrice(DataTable dt)
        {
            try
            {
                int ItemPriceId;
                int PriceId;
                decimal UnitPrice;
                bool IsDefault;
                bool Active;
                bool IsNew;

                foreach (DataRow drv in dt.Rows)
                {
                    ItemPriceId = Convert.ToInt32(drv["ItemPriceId"]);
                    PriceId = Convert.ToInt32(drv["PriceId"]);
                    UnitPrice = Convert.ToDecimal(drv["UnitPrice"]);
                    IsDefault = Convert.ToBoolean(drv["IsDefault"]);
                    Active = Convert.ToBoolean(drv["Active"]);
                    IsNew = Convert.ToBoolean(drv["IsNew"]);

                    AddDataItemPrice(ItemPriceId, PriceId, UnitPrice, IsDefault, Active, IsNew);
                }

                GridItemPrice.DataSource = dtAddItemPrice;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private bool AddDataItemPrice(int ItemPriceId, int PriceId, decimal UnitPrice, bool IsDefault, bool Active, bool IsNew)
        {
            try
            {
                DataTable dt_Added = dtAddItemPrice;
                if (dt_Added == null)
                    return false;

                DataRow dr_New = dt_Added.NewRow();

                dt_Added.BeginInit();

                dr_New["ItemPriceId"] = ItemPriceId;
                dr_New["PriceId"] = PriceId;
                dr_New["UnitPrice"] = UnitPrice;
                dr_New["IsDefault"] = IsDefault;
                dr_New["Active"] = Active;
                dr_New["IsNew"] = IsNew;

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

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.FindForm().DialogResult = DialogResult.Cancel;
        }

        private void GridViewItemPrice_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.RowHandle < 0) return;
            if (!(sender is GridView view)) return;

            if (view.FocusedColumn.Name == "colIsDefault")
            {
                DataRow dr_Selected = view.GetDataRow(e.RowHandle);
                bool ck = Convert.ToBoolean(dr_Selected["IsDefault"]);
                int id = Convert.ToInt32(dr_Selected["ItemPriceId"]);

                foreach (DataRow drv in dtAddItemPrice.Rows)
                {
                    if (Convert.ToInt32(drv["ItemPriceId"]) != id)
                    {
                        drv["IsDefault"] = false;
                    }
                }
            }
        }

        private void BtnAddPrice_Click(object sender, EventArgs e)
        {
            if (SpinUnitPrice.Value <= 0)
            {
                XtraMessageBox.Show("กรุณาระบุราคาล่วงหน้า", "สถานะ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                SpinUnitPrice.Focus();
                return;
            }

            AddItemPrice();
        }

        private void AddItemPrice()
        {
            try
            {
                int ItemPriceId, RunNo;
                int PriceId;
                decimal UnitPrice;
                bool IsDefault;
                bool Active;
                bool IsNew;
                int maxLevel = 0;

                foreach (DataRow dr in dtAddItemPrice.Rows)
                {
                    int tLevel = dr.Field<int>("ItemPriceId");
                    maxLevel = Math.Max(maxLevel, tLevel);
                }

                RunNo = maxLevel + 1;
                ItemPriceId = RunNo;
                PriceId = sPriceId;
                UnitPrice = SpinUnitPrice.Value;
                IsDefault = false;
                Active = true;
                IsNew = true;

                AddDataItemPrice(ItemPriceId, PriceId, UnitPrice, IsDefault, Active, IsNew);

                GridItemPrice.DataSource = dtAddItemPrice;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void BtnEdit2_Click(object sender, EventArgs e)
        {
            bool IsDefault = Convert.ToBoolean(GridViewItemPrice.GetFocusedRowCellValue("IsDefault"));
            bool IsNew = Convert.ToBoolean(GridViewItemPrice.GetFocusedRowCellValue("IsNew"));
            int ItemPriceId = Convert.ToInt32(GridViewItemPrice.GetFocusedRowCellValue("ItemPriceId"));
            decimal UnitPrice = Convert.ToDecimal(GridViewItemPrice.GetFocusedRowCellValue("UnitPrice"));

            if (IsDefault == true)
            {
                XtraMessageBox.Show("ไม่สามารถลบราคา Default ได้", "สถานะ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (IsNew == true)
            {
                GridViewItemPrice.DeleteSelectedRows();
            }
            else
            {
                if (XtraMessageBox.Show("คุณยืนยันที่จะลบข้อมูล ใช่หรือไม่?", "ยืนยัน", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    SQLCustomer.Spt_UpdateItemPrice(ItemPriceId, UnitPrice, IsDefault, false);
                    GridViewItemPrice.DeleteSelectedRows();
                }   
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            bool IsDefault = false;
            foreach (DataRow drv in dtAddItemPrice.Rows)
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
                XtraMessageBox.Show("บันทึกข้อมูลสำเร็จ", "สถานะ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.FindForm().DialogResult = DialogResult.OK;
            }
        }

        private bool SaveData()
        {
            try
            {
                bool IsNew;
                int ItemPriceId;
                decimal UnitPrice;
                bool IsDefault;
                bool Active;

                foreach (DataRow drv in dtAddItemPrice.Rows)
                {
                    IsNew = Convert.ToBoolean(drv["IsNew"]);
                    ItemPriceId = Convert.ToInt32(drv["ItemPriceId"]);
                    UnitPrice = Convert.ToDecimal(drv["UnitPrice"]);
                    IsDefault = Convert.ToBoolean(drv["IsDefault"]);
                    Active = Convert.ToBoolean(drv["Active"]);

                    CheckSaveData(IsNew, ItemPriceId, UnitPrice, IsDefault, Active);
                }

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool CheckSaveData(bool IsNew, int ItemPriceId, decimal UnitPrice, bool IsDefault, bool Active)
        {
            try
            {
                if (IsNew == false)
                {
                    SQLCustomer.Spt_UpdateItemPrice(ItemPriceId, UnitPrice, IsDefault, Active);
                }
                else
                {
                    SQLCustomer.Spt_AddItemPrice(sPriceId, UnitPrice, IsDefault);
                }

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private void GridViewItemPrice_RowStyle(object sender, RowStyleEventArgs e)
        {
            GridView view = sender as GridView;

            bool IsNew = Convert.ToBoolean(view.GetRowCellValue(e.RowHandle, "IsNew"));

            e.Appearance.ForeColor = Color.Black;
            e.Appearance.BackColor = Color.White;

            view.Appearance.FocusedRow.ForeColor = Color.Black;
            view.Appearance.FocusedRow.BackColor = Color.WhiteSmoke;

            if (e.RowHandle >= 0)
            {
                if (IsNew == true)
                {
                    e.Appearance.ForeColor = Color.Blue;
                    e.Appearance.BackColor = Color.Yellow;

                    view.Appearance.FocusedRow.ForeColor = Color.Blue;
                    view.Appearance.FocusedRow.BackColor = Color.LightYellow;
                }
            }

            if (view.FocusedRowHandle == e.RowHandle)
                e.Appearance.Assign(view.PaintAppearance.FocusedRow);

            e.HighPriority = true;   // override any other formatting 
        }

    }
}
