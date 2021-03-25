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

namespace RubberSoft.Tools
{
    public partial class FrmGetBuyData : XtraForm
    {
        public FrmGetBuyData()
        {
            InitializeComponent();
        }

        readonly SQLData SQLData = new SQLData();
        readonly SQLBuy SQLBuy = new SQLBuy();

        private int sCustomerId;
        private DateTime sDate, getDate, StartDate, EndDate, fDate, tDate;
        private TimeSpan StartTime, EndTime;

        private void FrmGetBuyData_Load(object sender, EventArgs e)
        {
            RefreshForm();
            LoadLkCustomer();
        }

        private bool RefreshForm()
        {
            try
            {
                sCustomerId = 0;
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

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
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

        private bool GetBuy()
        {
            try
            {
                SetTime();

                DataTable dt = new DataTable();
                DataSet ds = SQLBuy.GetBuyData(StartDate, EndDate, sCustomerId);
                dt = ds.Tables[0];

                GridBuy.DataSource = dt;

                //using (var context = new RubberSoftEntities())
                //{
                //    SetTime();
                //    var query = context.spt_GetBuy().Where(o => o.BuyDate >= StartDate &&
                //                                               o.BuyDate <= EndDate).ToList();

                //    GridBuy.DataSource = query;

                //}

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

        private void LoadLkCustomer()
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = SQLBuy.GetLKBuyCustomer(0);
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

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();

            //FrmTools frm = new FrmTools();
            //{
            //    this.Close();
            //    frm.Show();
            //}
        }

    }
}
