using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RubberSoft.Data
{
    class SQLClearData
    {
        readonly SQLData SQLData = new SQLData();

        //เคลีย Log การใช้งาน
        public DataSet Spt_ClearLogDetails()
        {
            string sSql = "TRUNCATE TABLE LogDetails";

            SqlParameter[] param = new SqlParameter[3];

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public bool ClearLogDetails()
        {
            try
            {
                DataSet ds = Spt_ClearLogDetails();
              
                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        //เคลียร์ เครื่องใช้งาน
        public DataSet Spt_ClearTerminal()
        {
            string sSql = "TRUNCATE TABLE Terminal";

            SqlParameter[] param = new SqlParameter[3];

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public bool ClearTerminal()
        {
            try
            {
                DataSet ds = Spt_ClearTerminal();

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        //เคลียร์ ประเภทสิทธิ์การใช้งาน
        public DataSet Spt_ClearUserRole()
        {
            string sSql = "TRUNCATE TABLE UserRole";

            SqlParameter[] param = new SqlParameter[3];

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public bool ClearUserRole()
        {
            try
            {
                DataSet ds = Spt_ClearUserRole();

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        //เคลียร์ ข้อมูลผู้ใช้งาน
        public DataSet Spt_ClearUsers()
        {
            string sSql = "TRUNCATE TABLE Users";

            SqlParameter[] param = new SqlParameter[3];

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public bool ClearUsers()
        {
            try
            {
                DataSet ds = Spt_ClearUsers();

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        //เคลียร์ สิทธิ์การใช้งาน
        public DataSet Spt_ClearAuthorizedUser()
        {
            string sSql = "TRUNCATE TABLE AuthorizedUser";

            SqlParameter[] param = new SqlParameter[3];

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public bool ClearAuthorizedUser()
        {
            try
            {
                DataSet ds = Spt_ClearAuthorizedUser();

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        //เคลียร์ ข้อมูลลูกค้า
        public DataSet Spt_ClearCustomer()
        {
            string sSql = "TRUNCATE TABLE Customer";

            SqlParameter[] param = new SqlParameter[3];

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public bool ClearCustomer()
        {
            try
            {
                DataSet ds = Spt_ClearCustomer();

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        //เคลียร์ ข้อมูลราคาล่วงหน้า
        public DataSet Spt_ClearCustomerPrice()
        {
            string sSql = "TRUNCATE TABLE CustomerPrice";

            SqlParameter[] param = new SqlParameter[3];

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public bool ClearCustomerPrice()
        {
            try
            {
                DataSet ds = Spt_ClearCustomerPrice();

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        //เคลียร์ ข้อมูลการซื้อ
        public DataSet Spt_ClearBuy()
        {
            string sSql = "TRUNCATE TABLE Buy";

            SqlParameter[] param = new SqlParameter[3];

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public bool ClearBuy()
        {
            try
            {
                DataSet ds = Spt_ClearBuy();

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        //เคลียร์ ข้อมูลการซื้อสินค้า
        public DataSet Spt_ClearBuyProduct()
        {
            string sSql = "TRUNCATE TABLE BuyProduct";

            SqlParameter[] param = new SqlParameter[3];

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public bool ClearBuyProduct()
        {
            try
            {
                DataSet ds = Spt_ClearBuyProduct();

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        //เคลียร์ ข้อมูลพักการซื้อ
        public DataSet Spt_ClearSaveBuy()
        {
            string sSql = "TRUNCATE TABLE SaveBuy";

            SqlParameter[] param = new SqlParameter[3];

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public bool ClearSaveBuy()
        {
            try
            {
                DataSet ds = Spt_ClearSaveBuy();

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        //เคลียร์ ข้อมูลพักการซื้อสินค้า
        public DataSet Spt_ClearSaveBuyProduct()
        {
            string sSql = "TRUNCATE TABLE SaveBuyProduct";

            SqlParameter[] param = new SqlParameter[3];

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public bool ClearSaveBuyProduct()
        {
            try
            {
                DataSet ds = Spt_ClearSaveBuyProduct();

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        //เคลียร์ ข้อมูลการขาย
        public DataSet Spt_ClearSale()
        {
            string sSql = "TRUNCATE TABLE Sale";

            SqlParameter[] param = new SqlParameter[3];

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public bool ClearSale()
        {
            try
            {
                DataSet ds = Spt_ClearSale();

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        //เคลียร์ ข้อมูลการขายสินค้า
        public DataSet Spt_ClearSaleProduct()
        {
            string sSql = "TRUNCATE TABLE SaleProduct";

            SqlParameter[] param = new SqlParameter[3];

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public bool ClearSaleProduct()
        {
            try
            {
                DataSet ds = Spt_ClearSaleProduct();

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        //เคลียร์ ข้อมูลพักการขาย
        public DataSet Spt_ClearSaveSale()
        {
            string sSql = "TRUNCATE TABLE SaveSale";

            SqlParameter[] param = new SqlParameter[3];

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public bool ClearSaveSale()
        {
            try
            {
                DataSet ds = Spt_ClearSaveSale();

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        //เคลียร์ ข้อมูลพักการขายสินค้า
        public DataSet Spt_ClearSaveSaleProduct()
        {
            string sSql = "TRUNCATE TABLE SaveSaleProduct";

            SqlParameter[] param = new SqlParameter[3];

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public bool ClearSaveSaleProduct()
        {
            try
            {
                DataSet ds = Spt_ClearSaveSaleProduct();

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        //เคลียร์ ข้อมูล Log น้ำหนักยาง
        public DataSet Spt_ClearTransactionLog()
        {
            string sSql = "TRUNCATE TABLE TransactionLog";

            SqlParameter[] param = new SqlParameter[3];

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public bool ClearTransactionLog()
        {
            try
            {
                DataSet ds = Spt_ClearTransactionLog();

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        //เคลียร์ ข้อมูลน้ำหนักยาง
        public DataSet Spt_ClearWeightBalance()
        {
            string sSql = "TRUNCATE TABLE WeightBalance";

            SqlParameter[] param = new SqlParameter[3];

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public bool ClearWeightBalance()
        {
            try
            {
                DataSet ds = Spt_ClearWeightBalance();

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        //เคลียร์ ข้อมูล Log ยอดคงเหลือลูกค้า
        public DataSet Spt_ClearOutStandingLog()
        {
            string sSql = "TRUNCATE TABLE OutStandingLog";

            SqlParameter[] param = new SqlParameter[3];

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public bool ClearOutStandingLog()
        {
            try
            {
                DataSet ds = Spt_ClearOutStandingLog();

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        //เคลียร์ ข้อมูลยอดคงเหลือลูกค้า
        public DataSet Spt_ClearOutStandingBalance()
        {
            string sSql = "TRUNCATE TABLE OutStandingBalance";

            SqlParameter[] param = new SqlParameter[3];

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public bool ClearOutStandingBalance()
        {
            try
            {
                DataSet ds = Spt_ClearOutStandingBalance();

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        public bool ClearBuySaleData()
        {
            try
            {
                // เคลีย ข้อมูลลูกค้า
                ClearCustomer();
                // เคลีย ข้อมูลราคาล่วงหน้า
                ClearCustomerPrice();
                // เคลีย ข้อมูลการซื้อ
                ClearBuy();
                // เคลีย ข้อมูลการซื้อสินค้า
                ClearBuyProduct();
                // เคลีย ข้อมูลพักการซื้อ
                ClearSaveBuy();
                // เคลีย ข้อมูลพักการซื้อสินค้า
                ClearSaveBuyProduct();
                // เคลีย ข้อมูลการขาย
                ClearSale();
                // เคลีย ข้อมูลการขายสินค้า
                ClearSaleProduct();
                // เคลีย ข้อมูลพักการขาย
                ClearSaveSale();
                // เคลีย ข้อมูลพักการขายสินค้า
                ClearSaveSaleProduct();
                // เคลีย ข้อมูล Log น้ำหนักยาง
                ClearTransactionLog();
                // เคลีย ข้อมูลน้ำหนักยาง
                ClearWeightBalance();
                // เคลีย ข้อมูล Log ยอดคงเหลือลูกค้า
                ClearOutStandingLog();
                // เคลีย ข้อมูลยอดคงเหลือลูกค้า
                ClearOutStandingBalance();

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        public bool ClearDetailsData()
        {
            try
            {
                // เคลีย Log การใช้งาน
                ClearLogDetails();
                // เคลีย เครื่องใช้งาน
                ClearTerminal();
                // เคลีย ประเภทสิทธิ์การใช้งาน
                ClearUserRole();
                // เคลีย ข้อมูลผู้ใช้งาน
                ClearUsers();
                // เคลีย สิทธิ์การใช้งาน
                ClearAuthorizedUser();
            
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
