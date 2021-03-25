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
    public partial class UcOpenSaveBuy : XtraUserControl
    {
        public UcOpenSaveBuy()
        {
            InitializeComponent();
        }

        readonly SQLBuy SQLBuy = new SQLBuy();

        //private string strBuyNumber;
        private int sSaveBuyId;
        private int OpenType;
        public int TypeSave;

        private void UcOpenSaveBuy_Load(object sender, EventArgs e)
        {
            LoadForm();
            GetSaveBuy();
        }

        private void LoadForm()
        {
            try
            {
                OpenType = ClassProperty.OpenSaveSaleType;
                TypeSave = ClassProperty.IsTypeSave;
                if (TypeSave != 0)
                {
                    CboProductUsing.Text = "รายการบันทึกแผ่นดิบ";
                    CboProductUsing.ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            GetSaveBuy();
        }

        private bool GetSaveBuy()
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = SQLBuy.Spt_GetSaveBuyData(CboProductUsing.Text, OpenType, TypeSave);
                dt = ds.Tables[0];
                GridSaveBuy.DataSource = dt;

                //using (var context = new RubberSoftEntities())
                //{
                //    var query = context.spt_GetSaveBuy().Where(o => o.Active == true).OrderBy(o => o.BuyNumber).ToList();

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
            sSaveBuyId = Convert.ToInt32(GridViewSaveBuy.GetFocusedRowCellValue("SaveBuyId"));
            ClassProperty.strSaveBuyId = sSaveBuyId;
            this.FindForm().DialogResult = DialogResult.OK;
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            sSaveBuyId = Convert.ToInt32(GridViewSaveBuy.GetFocusedRowCellValue("SaveBuyId"));
            RemoveSaveBuy(sSaveBuyId);
        }

        private bool RemoveSaveBuy(int SaveBuyId)
        {
            try
            {
                if (XtraMessageBox.Show("คุณต้องการยกเลิกรายการพักการซื้อนี้ ใช่หรือไม่?", "ยืนยัน", MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    if (SQLBuy.DeleteSaveBuy(SaveBuyId))
                    {
                        if (SQLBuy.DeleteSaveBuyProduct(SaveBuyId))
                        {
                            GetSaveBuy();
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
