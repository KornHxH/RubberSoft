using DevExpress.XtraEditors;
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
    public partial class FrmAddPercent : XtraForm
    {
        public FrmAddPercent()
        {
            InitializeComponent();
        }

        readonly SQLBuy SQLBuy = new SQLBuy();
        public DataTable dtTempBuyProduct = new DataTable();
        public DataTable dtBuyProduct = new DataTable();
        private void FrmAddPercent_Load(object sender, EventArgs e)
        {
            GetTempBuyProduct();
        }

        private bool GetTempBuyProduct()
        {
            try
            {
                DataSet ds = SQLBuy.Spt_GetTempBuyProduct();
                dtBuyProduct = ds.Tables[0];
                foreach (DataRow drv in dtTempBuyProduct.Rows)
                {
                    AddDataBuyProduct(drv);
                }

                GridBuyProduct.DataSource = dtBuyProduct;

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool AddDataBuyProduct(DataRow dr)

        {
            DataTable dt_Added = dtBuyProduct;
            if (dt_Added == null)
                return false;

            try
            {
                foreach (DataRow drv in dt_Added.Rows)
                {
                    if (Convert.ToInt32(dr["RunNo"]) == Convert.ToInt32(drv["RunNo"]))
                    {
                        drv["PriceId"] = dr["PriceId"];
                        drv["ItemPriceId"] = dr["ItemPriceId"];
                        drv["PriceName"] = dr["PriceName"];
                        drv["Percentage"] = dr["Percentage"];
                        drv["WeightAmount"] = dr["WeightAmount"];
                        drv["WeightAmount_Plate"] = dr["WeightAmount_Plate"];
                        drv["Drc"] = dr["Drc"];
                        drv["TotalPrice_Smoke"] = dr["TotalPrice_Smoke"];
                        drv["WeightAmount_Raw"] = dr["WeightAmount_Raw"];
                        drv["TotalPrice_Raw"] = dr["TotalPrice_Raw"];
                        drv["CalRubber"] = dr["CalRubber"];
                        drv["TotalPrice"] = dr["TotalPrice"];
                        drv["Remark"] = dr["Remark"];
                        drv["IsDefault"] = dr["IsDefault"];

                        return true;
                    }
                }

                DataRow dr_New = dt_Added.NewRow();

                dt_Added.BeginInit();

                dr_New["RunNo"] = dr["RunNo"];
                dr_New["BuyProductId"] = dr["BuyProductId"];
                dr_New["BuyId"] = dr["BuyId"];
                dr_New["PriceId"] = dr["PriceId"];
                dr_New["ItemPriceId"] = dr["ItemPriceId"];
                dr_New["PriceName"] = dr["PriceName"];
                dr_New["Percentage"] = dr["Percentage"];
                dr_New["WeightAmount"] = dr["WeightAmount"];
                dr_New["WeightAmount_Plate"] = dr["WeightAmount_Plate"];
                dr_New["Drc"] = dr["Drc"];
                dr_New["TotalPrice_Smoke"] = dr["TotalPrice_Smoke"];
                dr_New["WeightAmount_Raw"] = dr["WeightAmount_Raw"];
                dr_New["TotalPrice_Raw"] = dr["TotalPrice_Raw"];
                dr_New["CalRubber"] = dr["CalRubber"];
                dr_New["TotalPrice"] = dr["TotalPrice"];
                dr_New["Remark"] = dr["Remark"];
                dr_New["IsDefault"] = dr["IsDefault"];
                dr_New["IsSelect"] = false;

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

        private void LinkSelect_Click(object sender, EventArgs e)
        {
            if (LinkSelect.Text == "เลือกทั้งหมด")
            {
                SetSelect(true);
                LinkSelect.Text = "ไม่เลือกทั้งหมด";
            }
            else
            {
                SetSelect(false);
                LinkSelect.Text = "เลือกทั้งหมด";
            }
        }

        private bool SetSelect(bool set)
        {
            try
            {
                GridViewByProduct.ClearSelection();
                foreach (DataRow drv in dtBuyProduct.Rows)
                {
                    drv["IsSelect"] = set;
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
