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
    public partial class UcLoadBuy : XtraUserControl
    {
        public UcLoadBuy()
        {
            InitializeComponent();
        }

        readonly SQLData SQLData = new SQLData();
        readonly SQLReport SQLReport = new SQLReport();
        readonly SQLBuy SQLBuy = new SQLBuy();
        readonly SQLSale SQLSale = new SQLSale();
        readonly SQLAuthorized SQLAuthorized = new SQLAuthorized();

        private int sCustomerId, sBuyId, sProductTypeId, sOutStandingLogId, sTransactionLogId;
        private string strBuyNumber;
        private bool sActive;
        DataTable dtWeightBalance = new DataTable();

        private DateTime sDate, getDate, StartDate, EndDate, fDate, tDate;
        private TimeSpan StartTime, EndTime;

        private void UcLoadBuy_Load(object sender, EventArgs e)
        {
            if (RefreshForm())
            {
                GetBuy();
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

                DtBuyDate.DateTime = StartDate;
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
                sBuyId = 0;
                BtnRefBuy.Visible = false;
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

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            GetBuy();
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            GetBuy();
        }

        private bool GetBuy()
        {
            try
            {
                string text = TxtSearch.Text.Trim();

                using (var context = new RubberSoftEntities())
                {
                    SetTime();

                    if (text == "")
                    {
                        var query = context.spt_GetBuy().Where(o => o.BuyDate >= StartDate &&
                        o.BuyDate <= EndDate).ToList();

                        GridBuy.DataSource = query;
                    }
                    else
                    {
                        var query = context.spt_GetBuy().Where(o => o.BuyDate >= StartDate &&
                       o.BuyDate <= EndDate && (o.BuyNumber.Contains(text) ||
                       o.CustomerName.Contains(text))).ToList();

                        GridBuy.DataSource = query;
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

        private void GridViewBuy_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            GetBuyData(GridViewBuy);
        }

        private bool GetBuyData(GridView view)
        {
            try
            {
                sBuyId = Convert.ToInt32(view.GetFocusedRowCellValue("BuyId"));

                using (var context = new RubberSoftEntities())
                {
                    var query = context.spt_GetBuy().Where(o => o.BuyId == sBuyId).ToList();
                    if (query.Count > 0)
                    {
                        foreach (spt_GetBuy_Result dt in query)
                        {
                            strBuyNumber = dt.BuyNumber;
                            TxtBuyNumber.Text = dt.BuyNumber;
                            DtBuyDate.DateTime = dt.BuyDate.Value;
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
                        GetBuyProduct(sBuyId);
                        LoadProductType(sProductTypeId);
                        GetValueBalance(sCustomerId);

                        if (sActive == false)
                        {
                            BtnEditBuy.Enabled = false;
                            //if (ClassProperty.IsOpenBuy == false)
                            //{
                            //    BtnRefBuy.Visible = true;
                            //}
                        }
                        else
                        {
                            BtnEditBuy.Enabled = true;
                            //BtnRefBuy.Visible = false;
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

        private bool GetBuyProduct(int BuyId)
        {
            try
            {
                DataSet ds = SQLBuy.Spt_GetBuyProduct(BuyId);
                dtWeightBalance = ds.Tables[0];

                GridBuyProduct.DataSource = dtWeightBalance;

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

        private void GridViewBuy_DoubleClick(object sender, EventArgs e)
        {
            if (!(sender is GridView view)) return;
            sBuyId = Convert.ToInt32(view.GetFocusedRowCellValue("BuyId"));
            if (ClassProperty.IsOpenBuy == false)
            {
                SeletBuy(sBuyId);
            }
        }

        private void BtnSelect_Click(object sender, EventArgs e)
        {
            SeletBuy(sBuyId);
        }

        private bool SeletBuy(int id)
        {
            try
            {
                if (CheckData(id) == true)
                {
                    ClassProperty.strBuyId = id;
                    this.FindForm().DialogResult = DialogResult.OK;
                }
                else
                {
                    XtraMessageBox.Show("กรุณาเลือกรายการซื้อ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void BtnEditBuy_Click(object sender, EventArgs e)
        {
            //SetOutStandingBalance();

            // 7	3 : สิทธิ์ใช้งานหน้าการซื้อ	ยกเลิกข้อมูลการซื้อ (หน้าค้นหารายการซื้อ)
            int AuthorizeId = 7;

            if (SQLAuthorized.CheckAuthorized(ClassProperty.permisRoleId, AuthorizeId) == false)
            {
                SQLAuthorized.AlertAuthorized("สิทธิ์ใช้งานหน้าการซื้อ", "ไม่มีสิทธิ์ยกเลิกข้อมูลการซื้อ");
                return;
            }

            if (XtraMessageBox.Show("คุณต้องการยกเลิกข้อมูลการซื้อนี้ ใช่หรือไม่?", "ยืนยัน", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                if (CancelBuy(sBuyId))
                {
                    if (SaveWeightBalance())
                    {
                        XtraMessageBox.Show("ยกเลิกข้อมูลการซื้อสำเร็จ", "สถานะ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        GetBuy();
                        GridViewBuy.FocusedRowHandle = GridViewBuy.LocateByValue("BuyNumber", strBuyNumber);
                        GetBuyData(GridViewBuy);
                    }
                }
            }
               
        }

        private bool CancelBuy(int id)
        {
            try
            {
                if (CheckData(id) == true)
                {
                    if (SQLBuy.VoidBuy(id))
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

                GetOutstandingDebtValue(sBuyId);

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

        private bool GetOutstandingDebtValue(int BuyId)
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = SQLBuy.Spt_GetOutstandingDebt(BuyId);
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
                if (sProductTypeId == 2)
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

                if (AddTransactionLog(_PriceId, WeightBalanceAmt, TotalPrice_Raw, Refund_Raw, sBuyId, 5))
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

        private void BtnRefBuy_Click(object sender, EventArgs e)
        {

        }

        private void Btnพิมพ์ใบเสร็จ_Click(object sender, EventArgs e)
        {
            if (CheckData(sBuyId) == true)
            {
                if (sProductTypeId == 1)
                {
                    Load_RptBuyBill(sBuyId);
                }
                else
                {
                    Load_RptBuyBill1(sBuyId);
                }
            }
        }

        private void Load_RptBuyBill(int BuyId)
        {
            try
            {
                string sPrinterName;

                DataSet ds = SQLReport.Spt_GetBuyBill(BuyId);

                RptBuyBill report = new RptBuyBill();
                report.DataSource = ds.Tables[0];
                report.DataMember = "Datatable1";

                if (sActive == false)
                {
                    report.lblHeader.Text = "ใบยกเลิกรายการรับซื้อน้ำยาง";
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

        private void Load_RptBuyBill1(int BuyId)
        {
            try
            {
                string sPrinterName;

                DataSet ds = SQLReport.Spt_GetBuyBill(BuyId);

                RptBuyBill_1 report = new RptBuyBill_1();
                report.DataSource = ds.Tables[0];
                report.DataMember = "Datatable1";

                subRptWeightBalance subreport = new subRptWeightBalance();

                DataSet dss = SQLBuy.Rpt_GetWeightBalance(BuyId);
                subreport.DataSource = dss.Tables[0];
                subreport.DataMember = "Datatable1";

                report.xrSubWeightBalance.ReportSource = subreport;

                if (sActive == false)
                {
                    report.lblHeader.Text = "ใบยกเลิกรายการรับซื้อยางแผ่นดิบ";
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
        //        //DocumentViewer1.DocumentSource = report;

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

        private void GridViewBuy_RowStyle(object sender, RowStyleEventArgs e)
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

        private void UcLoadBuy_Leave(object sender, EventArgs e)
        {

        }

    }
}
