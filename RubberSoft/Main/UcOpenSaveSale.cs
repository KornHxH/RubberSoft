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
    public partial class UcOpenSaveSale : XtraUserControl
    {
        public UcOpenSaveSale()
        {
            InitializeComponent();
        }

        readonly SQLSale SQLSale = new SQLSale();

        private int sSaveSaleId;

        private void UcOpenSaveSale_Load(object sender, EventArgs e)
        {
            ClearValues();
            GetSaveSale();
        }

        private void ClearValues()
        {
            try
            {
                //strBuyNumber = "";
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private bool GetSaveSale()
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = SQLSale.Spt_GetSaveSaleData();
                dt = ds.Tables[0];
                GridSaveBuy.DataSource = dt;

                //using (var context = new RubberSoftEntities())
                //{
                //    var query = context.spt_GetSaveSale().Where(o => o.Active == true).OrderBy(o => o.SaleNumber).ToList();

                //    GridSaveBuy.DataSource = query;
                //}

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
            this.FindForm().DialogResult = DialogResult.Cancel;
        }

        private void BtnSelect_Click(object sender, EventArgs e)
        {
            sSaveSaleId = Convert.ToInt32(GridViewSaveBuy.GetFocusedRowCellValue("SaveSaleId"));
            ClassProperty.strSaveSaleId = sSaveSaleId;
            this.FindForm().DialogResult = DialogResult.OK;
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            sSaveSaleId = Convert.ToInt32(GridViewSaveBuy.GetFocusedRowCellValue("SaveSaleId"));
            RemoveSaveSale(sSaveSaleId);
        }

        private bool RemoveSaveSale(int SaveSaleId)
        {
            try
            {
                if (XtraMessageBox.Show("คุณต้องการยกเลิกรายการพักการขายนี้ ใช่หรือไม่?", "ยืนยัน", MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    if (SQLSale.DeleteSaveSale(SaveSaleId))
                    {
                        if (SQLSale.DeleteSaveSaleProduct(SaveSaleId))
                        {
                            GetSaveSale();
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
    }
}
