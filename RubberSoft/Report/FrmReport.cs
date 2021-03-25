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

namespace RubberSoft.Report
{
    public partial class FrmReport : XtraForm
    {
        public FrmReport()
        {
            InitializeComponent();
        }

        readonly SQLReport SQLReport = new SQLReport();
        readonly SQLData SQLData = new SQLData();
        readonly SQLAuthorized SQLAuthorized = new SQLAuthorized();

        private DateTime sDate, getDate, StartDate, EndDate, fDate, tDate;

        private TimeSpan StartTime, EndTime;

        private int sReportId, sReportGroupId, sCustomerId;

        private void FrmReport_Load(object sender, EventArgs e)
        {
            GetReportGroups();
            GetReport(0);
            RefreshForm();
            LoadLkCustomer();
        }

        private void RefreshForm()
        {
            try
            {
                lblUserName.Text = "ยินดีต้อนรับ : " + ClassProperty.permisUserNm;
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

                DtEndDate.DateTime = EndDate;
                TimeSpanTo.TimeSpan = EndTime;

                DtMonths.DateTime = StartDate;
                DtYear.DateTime = StartDate;
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
                tDate = DtEndDate.DateTime;
               
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

        private void LoadLkCustomer()
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = SQLReport.GetLKBuyCustomer(sReportGroupId);
                dt = ds.Tables[0];

                SLCustomer.Properties.DataSource = dt;
                SLCustomer.Properties.DisplayMember = "CustomerName";
                SLCustomer.Properties.ValueMember = "CustomerId";
                SLCustomer.EditValue = 0;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void BtnShowReport_Click(object sender, EventArgs e)
        {
            LoadReport();
        }

        private void LoadReport()
        {
            try
            {
                if (RadioFromDateToDate.Checked == true)
                {
                    SetTime();

                    ///////////////////// รายงานการซื้อ รายวัน ///////////////////////////////////////////

                    //รายงานการรับซื้อน้ำยาง
                    if (sReportId == 1)
                    {
                        // 24	6 : สิทธิ์ใช้งานหน้ารายงาน	ใช้งานรายงานการรับซื้อน้ำยาง
                        int AuthorizeId = 24;

                        if (SQLAuthorized.CheckAuthorized(ClassProperty.permisRoleId, AuthorizeId) == false)
                        {
                            SQLAuthorized.AlertAuthorized("สิทธิ์ใช้งานหน้ารายงาน	", "ไม่มีสิทธิ์ใช้งานรายงานการรับซื้อน้ำยาง");
                            return;
                        }

                        Report__BuyByDate_1(StartDate, EndDate, 1, CboProductUsing.Text);
                    }

                    //รายงานการรับซื้อยางแผ่นดิบ
                    if (sReportId == 2)
                    {
                        // 25	6 : สิทธิ์ใช้งานหน้ารายงาน	ใช้งานรายงานการรับซื้อแผ่นยางดิบ
                        int AuthorizeId = 25;

                        if (SQLAuthorized.CheckAuthorized(ClassProperty.permisRoleId, AuthorizeId) == false)
                        {
                            SQLAuthorized.AlertAuthorized("สิทธิ์ใช้งานหน้ารายงาน	", "ไม่มีสิทธิ์ใช้งานรายงานการรับซื้อแผ่นยางดิบ");
                            return;
                        }

                        Report__BuyByDate_2(StartDate, EndDate, 2);
                    }

                    //รายงานยอดคงเหลือรับซื้อยางพารา
                    if (sReportId == 3)
                    {
                        // 26	6 : สิทธิ์ใช้งานหน้ารายงาน	ใช้งานรายงานยอดคงเหลือรับซื้อยางพารา
                        int AuthorizeId = 26;

                        if (SQLAuthorized.CheckAuthorized(ClassProperty.permisRoleId, AuthorizeId) == false)
                        {
                            SQLAuthorized.AlertAuthorized("สิทธิ์ใช้งานหน้ารายงาน	", "ไม่มีสิทธิ์ใช้งานรายงานยอดคงเหลือรับซื้อยางพารา");
                            return;
                        }

                        Report_BuyweightBalanceByDate(StartDate, EndDate);
                    }
                }

                ///////////////////// รายงานการซื้อ รายเดือน ///////////////////////////////////////////

                if (RadioFromMonth.Checked == true)
                {
                    //รายงานการรับซื้อน้ำยาง
                    if (sReportId == 1)
                    {
                        // 24	6 : สิทธิ์ใช้งานหน้ารายงาน	ใช้งานรายงานการรับซื้อน้ำยาง
                        int AuthorizeId = 24;

                        if (SQLAuthorized.CheckAuthorized(ClassProperty.permisRoleId, AuthorizeId) == false)
                        {
                            SQLAuthorized.AlertAuthorized("สิทธิ์ใช้งานหน้ารายงาน	", "ไม่มีสิทธิ์ใช้งานรายงานการรับซื้อน้ำยาง");
                            return;
                        }

                        Report__BuyByMonth_1(DtMonths.DateTime, 1, CboProductUsing.Text);
                    }

                    //รายงานการรับซื้อยางแผ่นดิบ
                    if (sReportId == 2)
                    {
                        // 25	6 : สิทธิ์ใช้งานหน้ารายงาน	ใช้งานรายงานการรับซื้อแผ่นยางดิบ
                        int AuthorizeId = 25;

                        if (SQLAuthorized.CheckAuthorized(ClassProperty.permisRoleId, AuthorizeId) == false)
                        {
                            SQLAuthorized.AlertAuthorized("สิทธิ์ใช้งานหน้ารายงาน	", "ไม่มีสิทธิ์ใช้งานรายงานการรับซื้อแผ่นยางดิบ");
                            return;
                        }

                        Report__BuyByMonth_2(DtMonths.DateTime, 2);
                    }

                    //รายงานยอดคงเหลือรับซื้อยางพารา
                    if (sReportId == 3)
                    {
                        // 26	6 : สิทธิ์ใช้งานหน้ารายงาน	ใช้งานรายงานยอดคงเหลือรับซื้อยางพารา
                        int AuthorizeId = 26;

                        if (SQLAuthorized.CheckAuthorized(ClassProperty.permisRoleId, AuthorizeId) == false)
                        {
                            SQLAuthorized.AlertAuthorized("สิทธิ์ใช้งานหน้ารายงาน	", "ไม่มีสิทธิ์ใช้งานรายงานยอดคงเหลือรับซื้อยางพารา");
                            return;
                        }

                        Report_WeightByMonth(DtMonths.DateTime);
                    }
                }

                ///////////////////// รายงานการซื้อ รายปี ///////////////////////////////////////////

                if (RadioFromYear.Checked == true)
                {
                    //รายงานการรับซื้อน้ำยาง
                    if (sReportId == 1)
                    {
                        // 24	6 : สิทธิ์ใช้งานหน้ารายงาน	ใช้งานรายงานการรับซื้อน้ำยาง
                        int AuthorizeId = 24;

                        if (SQLAuthorized.CheckAuthorized(ClassProperty.permisRoleId, AuthorizeId) == false)
                        {
                            SQLAuthorized.AlertAuthorized("สิทธิ์ใช้งานหน้ารายงาน	", "ไม่มีสิทธิ์ใช้งานรายงานการรับซื้อน้ำยาง");
                            return;
                        }

                        Report__BuyByYear_1(DtYear.DateTime, 1, CboProductUsing.Text);
                    }

                    //รายงานการรับซื้อยางแผ่นดิบ
                    if (sReportId == 2)
                    {
                        // 25	6 : สิทธิ์ใช้งานหน้ารายงาน	ใช้งานรายงานการรับซื้อแผ่นยางดิบ
                        int AuthorizeId = 25;

                        if (SQLAuthorized.CheckAuthorized(ClassProperty.permisRoleId, AuthorizeId) == false)
                        {
                            SQLAuthorized.AlertAuthorized("สิทธิ์ใช้งานหน้ารายงาน	", "ไม่มีสิทธิ์ใช้งานรายงานการรับซื้อแผ่นยางดิบ");
                            return;
                        }

                        Report__BuyByYear_2(DtYear.DateTime, 2);
                    }

                    //รายงานยอดคงเหลือรับซื้อยางพารา
                    if (sReportId == 3)
                    {
                        // 26	6 : สิทธิ์ใช้งานหน้ารายงาน	ใช้งานรายงานยอดคงเหลือรับซื้อยางพารา
                        int AuthorizeId = 26;

                        if (SQLAuthorized.CheckAuthorized(ClassProperty.permisRoleId, AuthorizeId) == false)
                        {
                            SQLAuthorized.AlertAuthorized("สิทธิ์ใช้งานหน้ารายงาน	", "ไม่มีสิทธิ์ใช้งานรายงานยอดคงเหลือรับซื้อยางพารา");
                            return;
                        }

                        Report_BuyWeightBalanceByYear(DtYear.DateTime);
                    }
                }

                if (RadioFromDateToDate.Checked == true)
                {
                    SetTime();

                    ///////////////////// รายงานการขาย รายวัน ///////////////////////////////////////////

                    //รายงานการรับขายน้ำยาง
                    if (sReportId == 4)
                    {
                        // 27	6 : สิทธิ์ใช้งานหน้ารายงาน	ใช้งานรายงานการขายน้ำยาง
                        int AuthorizeId = 27;

                        if (SQLAuthorized.CheckAuthorized(ClassProperty.permisRoleId, AuthorizeId) == false)
                        {
                            SQLAuthorized.AlertAuthorized("สิทธิ์ใช้งานหน้ารายงาน	", "ไม่มีสิทธิ์ใช้งานรายงานการขายน้ำยาง");
                            return;
                        }

                        Report_SaleByDate_1(StartDate, EndDate, 3);
                    }

                    //รายงานการรับขายยางแผ่นดิบ
                    if (sReportId == 5)
                    {
                        // 28	6 : สิทธิ์ใช้งานหน้ารายงาน	ใช้งานรายงานการขายอแผ่นยางดิบ
                        int AuthorizeId = 28;

                        if (SQLAuthorized.CheckAuthorized(ClassProperty.permisRoleId, AuthorizeId) == false)
                        {
                            SQLAuthorized.AlertAuthorized("สิทธิ์ใช้งานหน้ารายงาน	", "ไม่มีสิทธิ์ใช้ใช้งานรายงานการขายอแผ่นยางดิบ");
                            return;
                        }

                        Report_SaleByDate_2(StartDate, EndDate, 4);
                    }

                    //รายงานยอดคงเหลือรับขายยางพารา
                    if (sReportId == 6)
                    {
                        // 29	6 : สิทธิ์ใช้งานหน้ารายงาน	ใช้งานรายงานยอดคงเหลือขายยางพารา
                        int AuthorizeId = 29;

                        if (SQLAuthorized.CheckAuthorized(ClassProperty.permisRoleId, AuthorizeId) == false)
                        {
                            SQLAuthorized.AlertAuthorized("สิทธิ์ใช้งานหน้ารายงาน	", "ไม่มีสิทธิ์ใช้งานรายงานยอดคงเหลือขายยางพารา");
                            return;
                        }

                        Report_SaleWeightBalanceByDate(StartDate, EndDate);
                    }
                }

                ///////////////////// รายงานการขาย รายเดือน ///////////////////////////////////////////

                if (RadioFromMonth.Checked == true)
                {
                    //รายงานการรับขายน้ำยาง
                    if (sReportId == 4)
                    {
                        // 27	6 : สิทธิ์ใช้งานหน้ารายงาน	ใช้งานรายงานการขายน้ำยาง
                        int AuthorizeId = 27;

                        if (SQLAuthorized.CheckAuthorized(ClassProperty.permisRoleId, AuthorizeId) == false)
                        {
                            SQLAuthorized.AlertAuthorized("สิทธิ์ใช้งานหน้ารายงาน	", "ไม่มีสิทธิ์ใช้งานรายงานการขายน้ำยาง");
                            return;
                        }

                        Report__SaleByMonth_1(DtMonths.DateTime, 3);
                    }

                    //รายงานการขายยางแผ่นดิบ
                    if (sReportId == 5)
                    {
                        // 28	6 : สิทธิ์ใช้งานหน้ารายงาน	ใช้งานรายงานการขายอแผ่นยางดิบ
                        int AuthorizeId = 28;

                        if (SQLAuthorized.CheckAuthorized(ClassProperty.permisRoleId, AuthorizeId) == false)
                        {
                            SQLAuthorized.AlertAuthorized("สิทธิ์ใช้งานหน้ารายงาน	", "ไม่มีสิทธิ์ใช้ใช้งานรายงานการขายอแผ่นยางดิบ");
                            return;
                        }

                        Report__SaleByMonth_2(DtMonths.DateTime, 4);
                    }

                    //รายงานยอดคงเหลือรับขายยางพารา
                    if (sReportId == 6)
                    {
                        // 29	6 : สิทธิ์ใช้งานหน้ารายงาน	ใช้งานรายงานยอดคงเหลือขายยางพารา
                        int AuthorizeId = 29;

                        if (SQLAuthorized.CheckAuthorized(ClassProperty.permisRoleId, AuthorizeId) == false)
                        {
                            SQLAuthorized.AlertAuthorized("สิทธิ์ใช้งานหน้ารายงาน	", "ไม่มีสิทธิ์ใช้งานรายงานยอดคงเหลือขายยางพารา");
                            return;
                        }

                        Report_SaleWeightBalanceByMonth(DtMonths.DateTime);
                    }
                }

                ///////////////////// รายงานการขาย รายปี ///////////////////////////////////////////

                if (RadioFromYear.Checked == true)
                {
                    //รายงานการรับขายน้ำยาง
                    if (sReportId == 4)
                    {
                        // 27	6 : สิทธิ์ใช้งานหน้ารายงาน	ใช้งานรายงานการขายน้ำยาง
                        int AuthorizeId = 27;

                        if (SQLAuthorized.CheckAuthorized(ClassProperty.permisRoleId, AuthorizeId) == false)
                        {
                            SQLAuthorized.AlertAuthorized("สิทธิ์ใช้งานหน้ารายงาน	", "ไม่มีสิทธิ์ใช้งานรายงานการขายน้ำยาง");
                            return;
                        }

                        Report_SaleByYear_1(DtYear.DateTime, 3);
                    }

                    //รายงานการรับขายยางแผ่นดิบ
                    if (sReportId == 5)
                    {
                        // 28	6 : สิทธิ์ใช้งานหน้ารายงาน	ใช้งานรายงานการขายอแผ่นยางดิบ
                        int AuthorizeId = 28;

                        if (SQLAuthorized.CheckAuthorized(ClassProperty.permisRoleId, AuthorizeId) == false)
                        {
                            SQLAuthorized.AlertAuthorized("สิทธิ์ใช้งานหน้ารายงาน	", "ไม่มีสิทธิ์ใช้ใช้งานรายงานการขายอแผ่นยางดิบ");
                            return;
                        }

                        Report_SaleByYear_2(DtYear.DateTime, 4);
                    }

                    //รายงานยอดคงเหลือรับขายยางพารา
                    if (sReportId == 6)
                    {
                        // 29	6 : สิทธิ์ใช้งานหน้ารายงาน	ใช้งานรายงานยอดคงเหลือขายยางพารา
                        int AuthorizeId = 29;

                        if (SQLAuthorized.CheckAuthorized(ClassProperty.permisRoleId, AuthorizeId) == false)
                        {
                            SQLAuthorized.AlertAuthorized("สิทธิ์ใช้งานหน้ารายงาน	", "ไม่มีสิทธิ์ใช้งานรายงานยอดคงเหลือขายยางพารา");
                            return;
                        }

                        Report_SaleWeightBalanceByYear(DtYear.DateTime);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void PrintReport(XtraReport rpt)
        {
            try
            {
                //report.lblReportName.Text = reportName;
                rpt.CreateDocument();

                ReportPrintTool pt = new ReportPrintTool(rpt)
                {
                    AutoShowParametersPanel = false
                };

                pt.Report.CreateDocument();
                pt.ShowPreview();
            }

            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        // รายงานการรับซื้อน้ำยาง
        private bool Report__BuyByDate_1(DateTime strFromDate, DateTime strToDate, int ProductTypeId, string ProductUsing)
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = SQLReport.Rpt_BuyByDate_1(strFromDate, strToDate, ProductTypeId, sCustomerId, ProductUsing);
                dt = ds.Tables[0];

                Rpt_BuyReport_1 report = new Rpt_BuyReport_1
                {
                    DataSource = dt,
                    DataMember = "Datatable1"
                };

                report.lblSearchText.Text = "ระหว่าง " +
                   strFromDate.ToString("วันdddd ที่ dd MMMM yyyy hh:mm", SQLData._cultureThInfo) +
                   " ถึง " + strToDate.ToString("วันdddd ที่ dd MMMM yyyy hh:mm", SQLData._cultureThInfo);

                PrintReport(report);

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }
        
        private bool Report__BuyByDate_2(DateTime strFromDate, DateTime strToDate, int ProductTypeId)
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = SQLReport.Rpt_BuyByDate_2(strFromDate, strToDate, ProductTypeId, sCustomerId);
                dt = ds.Tables[0];

                Rpt_BuyReport_2 report = new Rpt_BuyReport_2
                {
                    DataSource = dt,
                    DataMember = "Datatable1"
                };

                report.lblSearchText.Text = "ระหว่าง " +
                   strFromDate.ToString("วันdddd ที่ dd MMMM yyyy hh:mm", SQLData._cultureThInfo) +
                   " ถึง " + strToDate.ToString("วันdddd ที่ dd MMMM yyyy hh:mm", SQLData._cultureThInfo);

                PrintReport(report);

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool Report_BuyweightBalanceByDate(DateTime strFromDate, DateTime strToDate)
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = SQLReport.Rpt_BuyWeightBalanceBydate(strFromDate, strToDate, sCustomerId);
                dt = ds.Tables[0];

                Rpt_BuyWeightBalance report = new Rpt_BuyWeightBalance
                {
                    DataSource = dt,
                    DataMember = "Datatable1"
                };

                report.lblSearchText.Text = "ระหว่าง " +
                   strFromDate.ToString("วันdddd ที่ dd MMMM yyyy hh:mm", SQLData._cultureThInfo) +
                   " ถึง " + strToDate.ToString("วันdddd ที่ dd MMMM yyyy hh:mm", SQLData._cultureThInfo);

                PrintReport(report);

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }
        
       
        private bool Report__BuyByMonth_1(DateTime strFromDate, int ProductTypeId, string ProductUsing)
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = SQLReport.Rpt_BuyByMonth_1(strFromDate, ProductTypeId, sCustomerId, ProductUsing);
                dt = ds.Tables[0];

                Rpt_BuyReport_1 report = new Rpt_BuyReport_1
                {
                    DataSource = dt,
                    DataMember = "Datatable1"
                };

                report.lblSearchText.Text = "ช่วงเดือน " +
                   strFromDate.ToString("MMMM ปี yyyy", SQLData._cultureThInfo);

                PrintReport(report);

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool Report__BuyByMonth_2(DateTime strFromDate, int ProductTypeId)
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = SQLReport.Rpt_BuyByMonth_2(strFromDate, ProductTypeId, sCustomerId);
                dt = ds.Tables[0];

                Rpt_BuyReport_2 report = new Rpt_BuyReport_2
                {
                    DataSource = dt,
                    DataMember = "Datatable1"
                };

                report.lblSearchText.Text = "ช่วงเดือน " +
                   strFromDate.ToString("MMMM ปี yyyy", SQLData._cultureThInfo);

                PrintReport(report);

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool Report_WeightByMonth(DateTime strFromDate)
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = SQLReport.Rpt_BuyWeightBalanceByMonth(strFromDate, sCustomerId);
                dt = ds.Tables[0];

                Rpt_BuyWeightBalance report = new Rpt_BuyWeightBalance
                {
                    DataSource = dt,
                    DataMember = "Datatable1"
                };

                report.lblSearchText.Text = "ช่วงเดือน " +
                   strFromDate.ToString("MMMM ปี yyyy", SQLData._cultureThInfo);

                PrintReport(report);

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool Report__BuyByYear_1(DateTime strFromDate, int ProductTypeId, string ProductUsing)
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = SQLReport.Rpt_BuyByYear_1(strFromDate, ProductTypeId, sCustomerId, ProductUsing);
                dt = ds.Tables[0];

                Rpt_BuyReport_1 report = new Rpt_BuyReport_1
                {
                    DataSource = dt,
                    DataMember = "Datatable1"
                };

                report.lblSearchText.Text =  strFromDate.ToString("ช่วงปี yyyy", SQLData._cultureThInfo);

                PrintReport(report);

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool Report__BuyByYear_2(DateTime strFromDate, int ProductTypeId)
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = SQLReport.Rpt_BuyByYear_2(strFromDate, ProductTypeId, sCustomerId);
                dt = ds.Tables[0];

                Rpt_BuyReport_2 report = new Rpt_BuyReport_2
                {
                    DataSource = dt,
                    DataMember = "Datatable1"
                };

                report.lblSearchText.Text = strFromDate.ToString("ช่วงปี yyyy", SQLData._cultureThInfo);

                PrintReport(report);

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool Report_BuyWeightBalanceByYear(DateTime strFromDate)
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = SQLReport.Rpt_BuyWeightBalanceByYear(strFromDate, sCustomerId);
                dt = ds.Tables[0];

                Rpt_BuyWeightBalance report = new Rpt_BuyWeightBalance
                {
                    DataSource = dt,
                    DataMember = "Datatable1"
                };

                report.lblSearchText.Text = strFromDate.ToString("ช่วงปี yyyy", SQLData._cultureThInfo);

                PrintReport(report);

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        // รายงานการขายน้ำยาง
        private bool Report_SaleByDate_1(DateTime strFromDate, DateTime strToDate, int ProductTypeId)
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = SQLReport.Rpt_SaleByDate_1(strFromDate, strToDate, ProductTypeId, sCustomerId);
                dt = ds.Tables[0];

                Rpt_SaleReport_1 report = new Rpt_SaleReport_1
                {
                    DataSource = dt,
                    DataMember = "Datatable1"
                };

                report.lblSearchText.Text = "ระหว่าง " +
                   strFromDate.ToString("วันdddd ที่ dd MMMM yyyy hh:mm", SQLData._cultureThInfo) +
                   " ถึง " + strToDate.ToString("วันdddd ที่ dd MMMM yyyy hh:mm", SQLData._cultureThInfo);

                PrintReport(report);

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool Report__SaleByMonth_1(DateTime strFromDate, int ProductTypeId)
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = SQLReport.Rpt_SaleByMonth_1(strFromDate, ProductTypeId, sCustomerId);
                dt = ds.Tables[0];

                Rpt_SaleReport_1 report = new Rpt_SaleReport_1
                {
                    DataSource = dt,
                    DataMember = "Datatable1"
                };

                report.lblSearchText.Text = "ช่วงเดือน " +
                   strFromDate.ToString("MMMM ปี yyyy", SQLData._cultureThInfo);

                PrintReport(report);

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool Report_SaleByYear_1(DateTime strFromDate, int ProductTypeId)
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = SQLReport.Rpt_SaleByYear_1(strFromDate, ProductTypeId, sCustomerId);
                dt = ds.Tables[0];

                Rpt_SaleReport_1 report = new Rpt_SaleReport_1
                {
                    DataSource = dt,
                    DataMember = "Datatable1"
                };

                report.lblSearchText.Text = strFromDate.ToString("ช่วงปี yyyy", SQLData._cultureThInfo);

                PrintReport(report);

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        // รายงานการขายอแผ่นยางดิบ
        private bool Report_SaleByDate_2(DateTime strFromDate, DateTime strToDate, int ProductTypeId)
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = SQLReport.Rpt_SaleByDate_2(strFromDate, strToDate, ProductTypeId, sCustomerId);
                dt = ds.Tables[0];

                Rpt_SaleReport_2 report = new Rpt_SaleReport_2
                {
                    DataSource = dt,
                    DataMember = "Datatable1"
                };

                report.lblSearchText.Text = "ระหว่าง " +
                   strFromDate.ToString("วันdddd ที่ dd MMMM yyyy hh:mm", SQLData._cultureThInfo) +
                   " ถึง " + strToDate.ToString("วันdddd ที่ dd MMMM yyyy hh:mm", SQLData._cultureThInfo);

                PrintReport(report);

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool Report__SaleByMonth_2(DateTime strFromDate, int ProductTypeId)
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = SQLReport.Rpt_SaleByMonth_2(strFromDate, ProductTypeId, sCustomerId);
                dt = ds.Tables[0];

                Rpt_SaleReport_2 report = new Rpt_SaleReport_2
                {
                    DataSource = dt,
                    DataMember = "Datatable1"
                };

                report.lblSearchText.Text = "ช่วงเดือน " +
                   strFromDate.ToString("MMMM ปี yyyy", SQLData._cultureThInfo);

                PrintReport(report);

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool Report_SaleByYear_2(DateTime strFromDate, int ProductTypeId)
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = SQLReport.Rpt_SaleByYear_2(strFromDate, ProductTypeId, sCustomerId);
                dt = ds.Tables[0];

                Rpt_SaleReport_2 report = new Rpt_SaleReport_2
                {
                    DataSource = dt,
                    DataMember = "Datatable1"
                };

                report.lblSearchText.Text = strFromDate.ToString("ช่วงปี yyyy", SQLData._cultureThInfo);

                PrintReport(report);

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        // รายงานยอดคงเหลือขายยางพารา
        private bool Report_SaleWeightBalanceByDate(DateTime strFromDate, DateTime strToDate)
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = SQLReport.Rpt_SaleWeightBalanceBydate(strFromDate, strToDate, sCustomerId);
                dt = ds.Tables[0];

                Rpt_SaleWeightBalance report = new Rpt_SaleWeightBalance
                {
                    DataSource = dt,
                    DataMember = "Datatable1"
                };

                report.lblSearchText.Text = "ระหว่าง " +
                                    strFromDate.ToString("วันdddd ที่ dd MMMM yyyy hh:mm", SQLData._cultureThInfo) +
                                  " ถึง " + strToDate.ToString("วันdddd ที่ dd MMMM yyyy hh:mm", SQLData._cultureThInfo);
                PrintReport(report);

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool Report_SaleWeightBalanceByMonth(DateTime strFromDate)
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = SQLReport.Rpt_SaleWeightBalanceByMonth(strFromDate, sCustomerId);
                dt = ds.Tables[0];

                Rpt_SaleWeightBalance report = new Rpt_SaleWeightBalance
                {
                    DataSource = dt,
                    DataMember = "Datatable1"
                };

                report.lblSearchText.Text = "ช่วงเดือน " +
                   strFromDate.ToString("MMMM ปี yyyy", SQLData._cultureThInfo);

                PrintReport(report);

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool Report_SaleWeightBalanceByYear(DateTime strFromDate)
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = SQLReport.Rpt_SaleWeightBalanceByYear(strFromDate, sCustomerId);
                dt = ds.Tables[0];

                Rpt_SaleWeightBalance report = new Rpt_SaleWeightBalance
                {
                    DataSource = dt,
                    DataMember = "Datatable1"
                };

                report.lblSearchText.Text = strFromDate.ToString("ช่วงปี yyyy", SQLData._cultureThInfo);

                PrintReport(report);

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }



        private void SLCustomer_EditValueChanged(object sender, EventArgs e)
        {
            SLPriceSelect(GridViewCustomer);
        }

        private bool SLPriceSelect(GridView _view)
        {
            try
            {
                if (_view.SelectedRowsCount > 0)
                {
                    sCustomerId = Convert.ToInt32(_view.GetFocusedRowCellValue("CustomerId"));
                }

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private void SLKReportGroup_EditValueChanged(object sender, EventArgs e)
        {
            SLGroupSelect(GridViewReportGroup);
        }

        private bool SLGroupSelect(GridView _view)
        {
            try
            {
                if (_view.SelectedRowsCount > 0)
                {
                    sReportGroupId = Convert.ToInt32(_view.GetFocusedRowCellValue("ReportGroupId"));
                    GetReport(sReportGroupId);
                    LoadLkCustomer();
                }

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private void GetReportGroups()
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = SQLReport.Spt_GetReportGroups();
                dt = ds.Tables[0];

                SLKReportGroup.Properties.DataSource = dt;
                SLKReportGroup.Properties.DisplayMember = "ReportGroupName";
                SLKReportGroup.Properties.ValueMember = "ReportGroupId";
                SLKReportGroup.EditValue = 0;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void SLKReportList_EditValueChanged(object sender, EventArgs e)
        {
            SLReportSelect(GridViewReportList);
        }

        private bool SLReportSelect(GridView _view)
        {
            try
            {
                if (_view.SelectedRowsCount > 0)
                {
                    sReportId = Convert.ToInt32(_view.GetFocusedRowCellValue("ReportId"));
                }

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private void GetReport(int gid)
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = SQLReport.Spt_GetReport(gid);
                dt = ds.Tables[0];

                SLKReportList.Properties.DataSource = dt;
                SLKReportList.Properties.DisplayMember = "ReportName";
                SLKReportList.Properties.ValueMember = "ReportId";
                SLKReportList.EditValue = 0;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();

            //FrmMain frm = new FrmMain();
            //{
            //    this.Close();
            //    frm.Show();
            //}
        }
    }
}
