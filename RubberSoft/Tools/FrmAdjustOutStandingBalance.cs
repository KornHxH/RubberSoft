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
    public partial class FrmAdjustOutStandingBalance : XtraForm
    {
        public FrmAdjustOutStandingBalance()
        {
            InitializeComponent();
        }

        readonly SQLData SQLData = new SQLData();
        readonly SQLCustomer SQLCustomer = new SQLCustomer();
        readonly SQLBuy SQLBuy = new SQLBuy();

        private int sCustomerId, sCustomerTypeId;
        private DateTime sDate, getDate, StartDate, EndDate, fDate, tDate;
        private TimeSpan StartTime, EndTime;

        private void FrmAdjustOutStandingBalance_Load(object sender, EventArgs e)
        {
            RefreshForm();
            LoadLkSearchCustomerType();
            LoadCustomer();
        }

        private bool RefreshForm()
        {
            try
            {
                sCustomerId = 0;
                sCustomerTypeId = 0;
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
            GetCancelBuy();
        }

        private void BtnAdjust_Click(object sender, EventArgs e)
        {

        }

        private bool GetCancelBuy()
        {
            try
            {
                SetTime();

                DataTable dt = new DataTable();
                DataSet ds = SQLBuy.Spt_GetCancelBuy(sCustomerId);
                dt = ds.Tables[0];

                GridData.DataSource = dt;

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool LoadCustomer()
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = SQLCustomer.GetCustomer("", sCustomerTypeId);
                dt = ds.Tables[0];

                SLCustomer.Properties.DataSource = dt;
                SLCustomer.Properties.DisplayMember = "CustomerName";
                SLCustomer.Properties.ValueMember = "CustomerId";
                SLCustomer.EditValue = 0;

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

        private void LkCustomerTypes_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit lookUp = sender as LookUpEdit;
            if (lookUp.GetSelectedDataRow() is DataRowView dataRow)
            {
                if (dataRow != null)
                {
                    sCustomerTypeId = Convert.ToInt32(dataRow["CustomerTypeId"]);
                }

                LoadCustomer();
            }
        }

        private void LoadLkSearchCustomerType()
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                ds = SQLCustomer.GetLkCustomerTypes();
                dt = ds.Tables[0];

                LkCustomerTypes.Properties.DataSource = dt;
                LkCustomerTypes.Properties.DisplayMember = "CustomerTypeName";
                LkCustomerTypes.Properties.ValueMember = "CustomerTypeId";
                LkCustomerTypes.Properties.PopulateColumns();
                LkCustomerTypes.Properties.Columns["CustomerTypeId"].Visible = false;
                LkCustomerTypes.EditValue = 0;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            FrmTools frm = new FrmTools();
            {
                this.Close();
                frm.Show();
            }
        }
    }
}
