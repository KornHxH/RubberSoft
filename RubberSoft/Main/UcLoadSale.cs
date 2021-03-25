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
    public partial class UcLoadSale : XtraUserControl
    {
        public UcLoadSale()
        {
            InitializeComponent();
        }

        readonly SQLData SQLData = new SQLData();
        readonly SQLReport SQLReport = new SQLReport();
        readonly SQLBuy SQLBuy = new SQLBuy();
        readonly SQLSale SQLSale = new SQLSale();
        readonly SQLAuthorized SQLAuthorized = new SQLAuthorized();
        DataTable dtWeightBalance = new DataTable();

        private int sCustomerId, sSaleId, sProductTypeId, sOutStandingLogId, sTransactionLogId;
        private string strSaleNumber;
        private bool sActive;

        private DateTime sDate, getDate, StartDate, EndDate, fDate, tDate;

        private TimeSpan StartTime, EndTime;

        private void UcLoadSale_Load(object sender, EventArgs e)
        {
            if (RefreshForm())
            {
                GetSale();
            }
        }

        private bool RefreshForm()
        {
            try
            {
                sDate = DateTime.Now;
                getDate = Convert.ToDateTime(sDate, SQLData._cultureThInfo);
                fDate = new DateTime(getDate.Year, getDate.Month, getDate.Day, 0, 0, 0);
                tDate = new DateTime(getDate.Year, getDate.Month, getDate.Day, 23, 59, 59);
                StartTime = new TimeSpan(fDate.Hour, fDate.Minute, fDate.Second);
                EndTime = new TimeSpan(tDate.Hour, tDate.Minute, tDate.Second);
                StartDate = getDate;
                EndDate = getDate;

                DtStartDate.DateTime = StartDate;
                TimeSpanFrom.TimeSpan = StartTime;

                DateEndDate.DateTime = EndDate;
                TimeSpanTo.TimeSpan = EndTime;

                DtSaleDate.DateTime = StartDate;
                sProductTypeId = 1;
                ClearValues();

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private void ClearValues()
        {
            try
            {
                sSaleId = 0;
                BtnRefSale.Visible = false;
                ClearData();
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
                SpinOutstandingDebt.Value = 0;

                TxtLicensePlate.Text = "";
                TxtPhone.Text = "";
                TxtAddress.Text = "";
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void SetTime()
        {
            try
            {
                StartTime = TimeSpanFrom.TimeSpan;
                EndTime = TimeSpanTo.TimeSpan;
                fDate = DtStartDate.DateTime;
                tDate = DateEndDate.DateTime;

                StartDate = new DateTime(fDate.Year, fDate.Month, fDate.Day,
                    StartTime.Hours, StartTime.Minutes, StartTime.Seconds);

                EndDate = new DateTime(tDate.Year, tDate.Month, tDate.Day,
                    EndTime.Hours, EndTime.Minutes, EndTime.Seconds);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            GetSale();
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            GetSale();
        }

        private bool GetSale()
        {
            try
            {
                string text = TxtSearch.Text.Trim();

                using (var context = new RubberSoftEntities())
                {
                    SetTime();

                    if (text == "")
                    {
                        var query = context.spt_GetSale().Where(o => o.SaleDate >= StartDate &&
                        o.SaleDate <= EndDate).ToList();

                        GridSale.DataSource = query;
                    }
                    else
                    {
                        var query = context.spt_GetSale().Where(o => o.SaleDate >= StartDate &&
                       o.SaleDate <= EndDate && (o.SaleNumber.Contains(text) ||
                       o.CustomerName.Contains(text))).ToList();

                        GridSale.DataSource = query;
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

        private void GridViewSale_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (!(sender is GridView view)) return;
            GetSaleData(view);
        }

        private bool GetSaleData(GridView view)
        {
            try
            {
                sSaleId = Convert.ToInt32(view.GetFocusedRowCellValue("SaleId"));

                using (var context = new RubberSoftEntities())
                {
                    var query = context.spt_GetSale().Where(o => o.SaleId == sSaleId).ToList();
                    if (query.Count > 0)
                    {
                        foreach (spt_GetSale_Result dt in query)
                        {
                            strSaleNumber = dt.SaleNumber;
                            TxtSaleNumber.Text = dt.SaleNumber;
                            DtSaleDate.DateTime = dt.SaleDate.Value;
                            sCustomerId = dt.CustomerId.Value;
                            TxtCustomerName.Text = dt.CustomerName;
                            SpinSubTotal.Value = dt.SubTotal.Value;
                            SpinDownValue.Value = dt.DownValue.Value;
                            SpinNetTotal.Value = dt.NetTotal.Value;
                            SpinSetOffValue.Value = dt.SetOffValue.Value;
                            SpinValueBalance.Value = dt.ValueBalance.Value;
                            sActive = dt.Active.Value;
                            sProductTypeId = dt.ProductTypeId.Value;
                        }

                        GetCustomer(sCustomerId);
                        GetSaleProduct(sSaleId);
                        LoadProductType(sProductTypeId);
                        GetValueBalance(sCustomerId);

                        if (sActive == false)
                        {
                            BtnCancelSale.Enabled = false;
                        }
                        else
                        {
                            BtnCancelSale.Enabled = true;
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
                            TxtLicensePlate.Text = dt.LicensePlate;
                            TxtPhone.Text = dt.Phone;
                            TxtAddress.Text = dt.CustomerAddress;
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

        private bool GetSaleProduct(int SaleId)
        {
            try
            {
                DataSet ds = SQLSale.Spt_GetSaleProduct(SaleId);
                dtWeightBalance = ds.Tables[0];

                GridSaleProduct.DataSource = dtWeightBalance;

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private void LoadProductType(int id)
        {
            try
            {
                if (id == 1)
                {
                    colWeightAmount_Plate.Visible = false;
                    colCalRubber.Visible = false;
                    colยอดยางแห้ง.Visible = true;
                    colยอดยางแห้ง.Caption = "ยางแห้ง";
                    colราคา.Caption = "ราคา";
                }
                else
                {
                    colWeightAmount_Plate.Visible = true;
                    colยางแห้ง.Caption = "ราคายางรม";
                    colยอดยางแห้ง.Visible = false;
                    colราคา.Caption = "ยางดิบ";
                    colCalRubber.Visible = true;
                    colWeightAmount_Plate.VisibleIndex = 1;
                    colCalRubber.VisibleIndex = 5;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void BtnCancelSale_Click(object sender, EventArgs e)
        {
            // 12	4 : สิทธิ์ใช้งานหน้าการขาย 	ยกเลิกข้อมูลการขาย (หน้าค้นหารายการขาย)
            int AuthorizeId = 12;

            if (SQLAuthorized.CheckAuthorized(ClassProperty.permisRoleId, AuthorizeId) == false)
            {
                SQLAuthorized.AlertAuthorized("สิทธิ์ใช้งานหน้าการขาย", "ไม่มีสิทธิ์ยกเลิกข้อมูลการขาย");
                return;
            }

            if (XtraMessageBox.Show("คุณต้องการยกเลิกข้อมูลการขายนี้ ใช่หรือไม่?", "ยืนยัน", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                if (CancelSale(sSaleId))
                {
                    if (SaveWeightBalance())
                    {
                        XtraMessageBox.Show("ยกเลิกข้อมูลการขายสำเร็จ", "สถานะ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        GetSale();
                        GridViewSale.FocusedRowHandle = GridViewSale.LocateByValue("SaleNumber", strSaleNumber);
                        GetSaleData(GridViewSale);
                    }
                }
            }
        }

        private bool CancelSale(int SaleId)
        {
            try
            {
                if (CheckData(SaleId) == true)
                {
                    if (SQLSale.VoidSale(SaleId))
                    {
                        if (SetOutStandingBalance())
                        {
                            SQLBuy.UpdateOutStandingBalance(sCustomerId, SetValue, sOutStandingLogId);
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

        decimal SetValue, sOutstandingDebt, sWeightBalanceAmt, sRefund;

        private bool SetOutStandingBalance()
        {
            try
            {
                decimal ValueBalance = SpinOutstandingDebt.Value;

                sOutStandingLogId = SQLBuy.GetOutStandingLogId();

                GetOutstandingDebtValue(sSaleId);

                if (sRefund < 0)
                {
                    SetValue = ValueBalance + sOutstandingDebt;
                }
                else
                {
                    SetValue = ValueBalance - sOutstandingDebt;
                }

                SpinSetValue.Value = SetValue;

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        public bool GetOutstandingDebtValue(int SaleId)
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = SQLSale.Spt_GetOutstandingDebt(SaleId);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow drv in dt.Rows)
                    {
                        sOutstandingDebt = Convert.ToDecimal(drv["OutstandingDebt"]);
                        sRefund = Convert.ToDecimal(drv["Refund"]);
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

        private bool SaveWeightBalance()
        {
            try
            {
                if (sProductTypeId == 4)
                {
                    foreach (DataRow drv in dtWeightBalance.Rows)
                    {
                        SetWeightBalance(drv);
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

        private bool SetWeightBalance(DataRow drv)
        {
            try
            {
                decimal TotalPrice_Raw = Convert.ToDecimal(drv["TotalPrice_Raw"]);
                decimal WeightBalanceAmt = Convert.ToDecimal(drv["WeightBalanceAmt"]);
                int _PriceId = Convert.ToInt32(drv["PriceId"]);
                decimal Refund_Raw;

                GetWeightBalanceValue(_PriceId);

                Refund_Raw = TotalPrice_Raw + sWeightBalanceAmt;

                if (AddTransactionLog(_PriceId, WeightBalanceAmt, TotalPrice_Raw, Refund_Raw, sSaleId, 5))
                {
                    sTransactionLogId = SQLBuy.GetTransactionLogId();
                    SQLBuy.UpdateWeightBalance(_PriceId, Refund_Raw, sTransactionLogId);
                }

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private decimal GetWeightBalanceValue(int PriceId)
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = SQLBuy.Spt_GetWeightBalanceValue(PriceId);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow drv in dt.Rows)
                    {
                        sWeightBalanceAmt = Convert.ToDecimal(drv["WeightBalanceAmt"]);
                    }

                    return sWeightBalanceAmt;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return 0;
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
                        SpinOutstandingDebt.Value = Convert.ToDecimal(drv["NetTotal"]);
                    }
                }
                else
                {
                    SpinOutstandingDebt.Value = 0;
                }

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

        private void Btnพิมพ์ใบเสร็จ_Click(object sender, EventArgs e)
        {
            if (CheckData(sSaleId) == true)
            {
                if (sProductTypeId == 3)
                {
                    Load_RptSaleBill(sSaleId);
                }
                else
                {
                    Load_RptSaleBill1(sSaleId);
                }
            }
        }

        private void Load_RptSaleBill(int SaleId)
        {
            try
            {
                string sPrinterName;

                DataSet ds = SQLReport.Spt_GetSaleBill(SaleId, sCustomerId);

                RptSaleBill report = new RptSaleBill();
                report.DataSource = ds.Tables[0];
                report.DataMember = "Datatable1";

                if (sActive == false)
                {
                    report.lblHeader.Text = "ใบยกเลิกรายการขายน้ำยาง";
                }

                FrmPayment frm = new FrmPayment();
                frm.DocShowBill.DocumentSource = report;
                frm.sMessage = "";
                frm.BtnCloseBill.Text = "พิมพ์";
                frm.PrintType = 2;

                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    sPrinterName = frm.sPrinterName;
                    report.PrinterName = sPrinterName;
                    report.Print(sPrinterName);
                }

                //PrintBill(report);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void Load_RptSaleBill1(int SaleId)
        {
            try
            {
                string sPrinterName;

                DataSet ds = SQLReport.Spt_GetSaleBill(SaleId, sCustomerId);

                RptSaleBill_1 report = new RptSaleBill_1();
                report.DataSource = ds.Tables[0];
                report.DataMember = "Datatable1";

                subRptWeightBalance subreport = new subRptWeightBalance();

                DataSet dss = SQLSale.Rpt_GetWeightBalance(SaleId);
                subreport.DataSource = dss.Tables[0];
                subreport.DataMember = "Datatable1";

                report.xrSubWeightBalance.ReportSource = subreport;

                if (sActive == false)
                {
                    report.lblHeader.Text = "ใบยกเลิกรายการขายยางแผ่นดิบ";
                }

                FrmPayment frm = new FrmPayment();
                frm.DocShowBill.DocumentSource = report;
                frm.sMessage = "";
                frm.BtnCloseBill.Text = "พิมพ์";
                frm.PrintType = 2;

                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    sPrinterName = frm.sPrinterName;
                    report.PrinterName = sPrinterName;
                    report.Print(sPrinterName);
                }

                //PrintBill(report);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        //private void PrintBill(XtraReport report)
        //{
        //    try
        //    {
        //        report.CreateDocument();

        //        ReportPrintTool pt = new ReportPrintTool(report)
        //        {
        //            AutoShowParametersPanel = false
        //        };

        //        pt.Report.CreateDocument();
        //        pt.ShowPreview();
        //    }

        //    catch (Exception ex)
        //    {
        //        XtraMessageBox.Show(ex.Message);
        //    }
        //}

        private void GridViewSale_RowStyle(object sender, RowStyleEventArgs e)
        {
            GridView view = sender as GridView;

            bool active = Convert.ToBoolean(view.GetRowCellValue(e.RowHandle, "Active"));

            e.Appearance.ForeColor = Color.Black;
            e.Appearance.BackColor = Color.White;

            view.Appearance.FocusedRow.ForeColor = Color.White;
            view.Appearance.FocusedRow.BackColor = Color.FromArgb(106, 141, 59);

            if (e.RowHandle >= 0)
            {
                if (active == false)
                {
                    e.Appearance.ForeColor = Color.Red;
                    e.Appearance.BackColor = Color.White;

                    view.Appearance.FocusedRow.ForeColor = Color.Yellow;
                    view.Appearance.FocusedRow.BackColor = Color.OrangeRed;
                }
            }

            //if (((e.State & GridRowCellState.Focused) != 0) && ((e.State & GridRowCellState.GridFocused) != 0))
            if (view.FocusedRowHandle == e.RowHandle)
                e.Appearance.Assign(view.PaintAppearance.FocusedRow);

            e.HighPriority = true;   // override any other formatting 
        }
    }
}
