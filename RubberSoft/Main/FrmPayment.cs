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
using DevExpress.XtraEditors.DXErrorProvider;
using System.Drawing.Printing;

namespace RubberSoft.Main
{
    public partial class FrmPayment : XtraForm
    {
        public FrmPayment()
        {
            InitializeComponent();
        }

        readonly SQLTerminal SQLTerminal = new SQLTerminal();

        public string sMessage, sPrinterName;
        public int PrintType;
        int sOptionId;
        public bool IsPrinter;

        private void FrmPayment_Load(object sender, EventArgs e)
        {
            GetPrinters();
            GetOptins();
        }


        private bool GetOptins()
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = SQLTerminal.Spt_GetPrinter(ClassProperty.StrTerminalId, "SetPrinter");
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow drv in dt.Rows)
                    {
                        sOptionId = Convert.ToInt32(drv["OptionID"]);
                        sPrinterName = Convert.ToString(drv["OptionValue"]);
                        CboPrinterList.Text = Convert.ToString(drv["OptionValue"]);
                        CkShowPrinter.Checked = Convert.ToBoolean(drv["Active"]);
                        CkIsPrinter.Checked = Convert.ToBoolean(drv["IsTrue"]);
                    }
                }
                else
                {
                    sOptionId = 0;
                    sPrinterName = "";
                    CboPrinterList.SelectedIndex = 0;
                    CkShowPrinter.Checked = true;
                    CkIsPrinter.Checked = true;
                }

                if (CkShowPrinter.Checked == true)
                {
                    CboPrinterList.Visible = true;
                }
                else
                {
                    CboPrinterList.Visible = false;
                }

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }


        public static PrinterSettings.StringCollection InstalledPrinters { get; }

        private void GetPrinters()
        {
            string pkInstalledPrinters;
            CboPrinterList.Properties.Items.Clear();

            for (int i = 0; i <= PrinterSettings.InstalledPrinters.Count - 1; i++)
            {
                pkInstalledPrinters = PrinterSettings.InstalledPrinters[i];
                CboPrinterList.Properties.Items.Add(pkInstalledPrinters);
            }
            if (CboPrinterList.Properties.Items.Count > 0)
                CboPrinterList.SelectedIndex = 0;
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.FindForm().DialogResult = DialogResult.Cancel;
        }

        private void BtnCloseBill_Click(object sender, EventArgs e)
        {
            if (PrintType == 1)
            {
                if (XtraMessageBox.Show(sMessage, "ยืนยัน", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    SaveOptins();
                    this.FindForm().DialogResult = DialogResult.OK;
                }
            }
            else
            {
                SaveOptins();
                this.FindForm().DialogResult = DialogResult.OK;
            }
        }

        private void CkIsPrinter_CheckedChanged(object sender, EventArgs e)
        {
            if (CkShowPrinter.Checked == true)
            {
                CboPrinterList.Visible = true;
            }
            else
            {
                CboPrinterList.Visible = false;
            }
        }

        private bool SaveOptins()
        {
            try
            {
                sPrinterName = CboPrinterList.Text;
                IsPrinter = CkIsPrinter.Checked;

                if (sOptionId == 0)
                {
                    SQLTerminal.AddPrinter(CboPrinterList.Text, CkIsPrinter.Checked, CkShowPrinter.Checked);
                }
                else
                {
                    SQLTerminal.UpdatePrinter(sOptionId, CboPrinterList.Text, CkIsPrinter.Checked, CkShowPrinter.Checked);
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
