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
    class SQLSale
    {
        readonly SQLData SQLData = new SQLData();

        private int sSaveSaleId;

        public DataSet GetSaleCustomerTypes()
        {
            string sSql = @"SELECT CustomerTypeId, CustomerTypeName 
                            FROM CustomerTypes
                            WHERE CustomerTypeId IN (2) ORDER BY CustomerTypeId";

            SqlParameter[] param = new SqlParameter[3];
            //param[0] = new SqlParameter("@UnitId", id);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public DataSet GetLKSaleCustomer(int CustomerGroupId)
        {
            string sSql = @"SELECT CustomerId, A.CustomerTypeId, CustomerTypeName, CustomerCode, CustomerName, 
                            CustomerAddress, LicensePlate, Phone
                            FROM Customer A
                            LEFT OUTER JOIN CustomerTypes B ON A.CustomerTypeId = B.CustomerTypeId
                            WHERE A.Active = 1 AND A.CustomerTypeId IN (2)";

            if (CustomerGroupId != 0)
            {
                sSql += @" AND CustomerGroupId = @CustomerGroupId";
            }

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@CustomerGroupId", CustomerGroupId);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public DataSet GetLkProducts(int ProductTypeId)
        {
            string sSql = @"SELECT ProductId, ProductTypeId, ProductName FROM Products 
                            WHERE Active = 1 AND ProductTypeId=@ProductTypeId";

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@ProductTypeId", ProductTypeId);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public DataSet Spt_GetSaleNumber(DateTime date)
        {
            string sSql = "spt_GetSaleNumber";

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@SaleDate", date);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.StoredProcedure, sSql, param);
        }

        public DataSet Spt_GetSaveSaleNumber(DateTime date)
        {
            string sSql = "spt_GetSaveSaleNumber";

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@SaleDate", date);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.StoredProcedure, sSql, param);
        }

        public DataSet GetMaxSaleId()
        {
            string sSql = @"SELECT ISNULL(SaleId, 0)  + 1 AS SaleId FROM (
                            SELECT MAX(SaleId) SaleId FROM Sale) Sale";

            SqlParameter[] param = new SqlParameter[3];
            //param[0] = new SqlParameter("@BuyDate", date);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public DataSet GetMaxTransactionLogId()
        {
            string sSql = @"SELECT ISNULL(LogId, 1) AS LogId FROM (
                            SELECT MAX(LogId) LogId FROM TransactionLog) TransactionLog";

            SqlParameter[] param = new SqlParameter[3];
            //param[0] = new SqlParameter("@BuyDate", date);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public DataSet GetMaxOutStandingLogId()
        {
            string sSql = @"SELECT ISNULL(OutStandingLogId, 1) AS OutStandingLogId FROM (
                            SELECT MAX(OutStandingLogId) OutStandingLogId FROM OutStandingLog) OutStandingLog";

            SqlParameter[] param = new SqlParameter[3];
            //param[0] = new SqlParameter("@BuyDate", date);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public DataSet Spt_GetSaleProduct(int SaleId)
        {
            string sSql = @"SELECT SaleProductId AS RunNo, SaleProductId, SaleId, A.PriceId, ItemPriceId, A.PriceName, [Percentage], 
                            A.WeightAmount, WeightAmount_Plate, Drc, TotalPrice_Smoke, WeightAmount_Raw,  
                            TotalPrice_Raw, CalRubber, TotalPrice, TotalPrice AS sTotalPrice, Remark, 
							ISNULL(WeightBalanceAmt, 0) WeightBalanceAmt, C.IsDefault
                            FROM SaleProduct A
							LEFT JOIN WeightBalance B ON A.PriceId = B.PriceId
							INNER JOIN CustomerPrice C ON A.PriceId = C.PriceId
                            WHERE SaleId = @SaleId AND A.Active = 1";

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@SaleId", SaleId);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public DataSet Spt_GetSaveSaleData()
        {
            string sSql = @"SELECT SaveSaleId, SaleNumber, CustomerId, CustomerName, SaleDate, SubTotal, DownValue, NetTotal, SetOffValue, 
                            ValueBalance, ProductTypeId, ProductUsing, InsertDate, InsertBy, Active, 
                            CASE WHEN ProductTypeId = 3 THEN SumWeightAmount ELSE SumTotalPrice_Raw END AS WeightAmountTotal
                            FROM SaveSale A
                            INNER JOIN (
                            SELECT SaleId, SUM(TotalPrice_Raw) SumTotalPrice_Raw, SUM(WeightAmount) SumWeightAmount 
                            FROM SaveSaleProduct WHERE Active = 1
                            GROUP BY SaleId
                            ) B ON A.SaveSaleId = B.SaleId
                            WHERE Active = 1 ORDER BY SaleNumber";

            SqlParameter[] param = new SqlParameter[3];
            //param[0] = new SqlParameter("@BuyId", BuyId);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public DataSet Spt_GetSaveSaleProduct(int SaleId)
        {
            string sSql = @"SELECT SaleProductId AS RunNo, SaleProductId, SaleId, A.PriceId, ItemPriceId, A.PriceName, [Percentage], A.WeightAmount, 
                            WeightAmount_Plate, Drc, TotalPrice_Smoke, WeightAmount_Raw,  
                            TotalPrice_Raw, CalRubber, TotalPrice, TotalPrice AS sTotalPrice, Remark, A.Active, IsDefault
                            FROM SaveSaleProduct A
							LEFT JOIN WeightBalance B ON A.PriceId = B.PriceId
							INNER JOIN CustomerPrice C ON A.PriceId = C.PriceId
                            WHERE A.Active = 1 AND A.SaleId = @SaleId";

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@SaleId", SaleId);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public DataSet Spt_GetSaleProductType(int SaleId)
        {
            string sSql = @"SELECT SaleId,  ProductTypeId
                            FROM (
                            SELECT SaleId, CASE WHEN CalRubber = 0 THEN 1 ELSE 2 END AS ProductTypeId
                            FROM SaleProduct ) SaleProduct 
                            WHERE SaleId = @SaleId
                            GROUP BY SaleId,  ProductTypeId";

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@SaleId", SaleId);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public DataSet Spt_GetSaleValueBalance(int cid)
        {
            string sSql = @"SELECT ISNULL(OutstandingDebt, 0) AS NetTotal FROM Customer A
                            LEFT JOIN OutStandingBalance B ON A.CustomerId = B.CustomerId
                            WHERE A.CustomerId = @CustomerId";

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@CustomerId", cid);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public DataSet Spt_GetSaleWeightBalance(int cid)
        {
            string sSql = @"SELECT PriceId, CustomerId, sName, SaleDate, SalePriceAdvance, WeightAmount, BeginAmt, DeliveryPrice, 
                            BeginBalanceAmt, WeightBalanceAmt, LastLogId, Active, ItemPriceId, UnitPrice, IsDefault
                            FROM (

                            SELECT A.PriceId, A.CustomerId, A.PriceId AS sName, SaleDate, SalePriceAdvance, WeightAmount, DeliveryPrice, 
                            ISNULL(BeginBalanceAmt, 0) BeginBalanceAmt, ISNULL(WeightBalanceAmt, 0) WeightBalanceAmt, 
							ISNULL(WeightBalanceAmt, 0) AS BeginAmt, LastLogId, A.Active, ItemPriceId, ISNULL(UnitPrice, 0) UnitPrice, A.IsDefault
                            FROM CustomerPrice A
                            INNER JOIN Customer B ON A.CustomerId = B.CustomerId
							LEFT OUTER JOIN (

							SELECT DD.WeightBalanceId, DD.PriceId, BeginBalanceAmt, WeightBalanceAmt, LastLogId 
                            FROM (
							SELECT MAX(WeightBalanceId) AS WeightBalanceId, PriceId FROM WeightBalance 
							WHERE CustomerId = @CustomerId 
							GROUP BY WeightBalanceId, PriceId) DT
							INNER JOIN WeightBalance DD ON Dt.WeightBalanceId = DD.WeightBalanceId

							) C ON A.PriceId = C.PriceId
							LEFT OUTER JOIN (		

							SELECT ItemPriceId, PriceId, UnitPrice 
							FROM ItemPrice WHERE Active = 1 AND IsDefault = 1

							) D ON A.PriceId = D.PriceId

                            WHERE A.Active = 1 AND A.CustomerId = @CustomerId
                            AND (WeightBalanceAmt > 0 OR A.IsDefault = 1)

                            ) WeightBalance";

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@CustomerId", cid);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public DataSet Spt_DeleteSaleProduct(int SaleProductId, int DeleteBy)
        {
            string sSql = @"Update SaleProduct SET Active=0, DeleteDate=GETDATE(), DeleteBy=@DeleteBy 
                            WHERE SaleProductId=@SaleProductId";

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@SaleProductId", SaleProductId);
            param[1] = new SqlParameter("@DeleteBy", DeleteBy);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public bool DeleteSaleProduct(int SaleProductId)
        {
            try
            {
                DataSet ds = Spt_DeleteSaleProduct(SaleProductId, ClassProperty.permisUserID);

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        public DataSet Spt_GetCancelSale(int CustomerId)
        {
            string sSql = @"SELECT CustomerId, RefId, ValueBalance, SubTotal, SetOffValue, DownValue, NetTotal, 
                            OutstandingDebt, OutStandingStatus, LogTypeId, InsertDate 
                            FROM (
                            SELECT CustomerId, SaleId RefId, 0 OutStandingStatus, 5 LogTypeId, InsertDate, 
                            ValueBalance, SubTotal, SetOffValue, DownValue, NetTotal, 
                            CASE 
                            WHEN (DownValue + SetOffValue) > 0 THEN (SubTotal -(DownValue + SetOffValue))
                            ELSE SubTotal END AS OutstandingDebt
                            FROM Sale 
                            WHERE CustomerId=@CustomerId
                            ) CancelSale";

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@CustomerId", CustomerId);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public DataSet Spt_DeleteSale(int SaleId, int DeleteBy)
        {
            string sSql = @"Update Sale SET Active=0, DeleteDate=GETDATE(), DeleteBy=@DeleteBy 
                            WHERE SaleId=@SaleId";

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@SaleId", SaleId);
            param[1] = new SqlParameter("@DeleteBy", DeleteBy);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public bool DeleteSale(int SaleId)
        {
            try
            {
                DataSet ds = Spt_DeleteSale(SaleId, ClassProperty.permisUserID);

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        public bool AddLogCancelSale(int SaleId)
        {
            try
            {
                DataSet ds = Spt_AddLogCancelSale(SaleId);

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        public DataSet Spt_AddLogCancelSale(int SaleId)
        {
            string sSql = @"INSERT INTO OutStandingLog (CustomerId, BeginOutstandingDebt, OutstandingDebt, RefId, OutStandingStatus, LogTypeId , InsertDate)

                            SELECT CustomerId, 
							CASE 
                            WHEN BeginOutstandingDebt > 0 THEN ABS(BeginOutstandingDebt)
                            ELSE BeginOutstandingDebt 
							END AS BeginOutstandingDebt, 
							OutstandingDebt, RefId, OutStandingStatus, LogTypeId, InsertDate 
                            FROM (
                            SELECT A.CustomerId, SaleId RefId, 0 OutStandingStatus, 5 LogTypeId, GETDATE() InsertDate, 
                            ValueBalance, SubTotal, SetOffValue, DownValue, NetTotal, 
                            CASE 
                            WHEN (DownValue + SetOffValue) > 0 THEN (SubTotal -(DownValue + SetOffValue))
                            ELSE SubTotal END AS OutstandingDebt, OutstandingDebt BeginOutstandingDebt
                            FROM Sale A
							INNER JOIN OutStandingBalance B ON A.CustomerId = B.CustomerId 
                            WHERE SaleId=@SaleId

                            ) CancelSale";

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@SaleId", SaleId);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public bool VoidSale(int SaleId)
        {
            try
            {
                if (AddLogCancelSale(SaleId))
                {
                    if (DeleteSale(SaleId))
                    {

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

        public DataSet Spt_DeleteBuyProduct(int BuyProductId, int DeleteBy)
        {
            string sSql = @"Update BuyProduct SET Active=0, DeleteDate=GETDATE(), DeleteBy=@DeleteBy 
                            WHERE BuyProductId=@BuyProductId";

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@BuyProductId", BuyProductId);
            param[1] = new SqlParameter("@DeleteBy", DeleteBy);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public bool DeleteBuyProduct(int BuyProductId)
        {
            try
            {
                DataSet ds = Spt_DeleteBuyProduct(BuyProductId, ClassProperty.permisUserID);

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        public DataSet DeleteCustomerPrice(int pid)
        {
            string sSql = @"Update CustomerPrice SET Active=0, UpdateDate=GETDATE() 
                            WHERE PriceId=@PriceId";

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@PriceId", pid);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public DataSet Spt_DeleteSaveSale(int SaveSaleId)
        {
            string sSql = @"UPDATE SaveSale SET Active = 0 WHERE SaveSaleId=@SaveSaleId";

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@SaveSaleId", SaveSaleId);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public DataSet Spt_DeleteSaveSaleProduct(int SaleId)
        {
            string sSql = @"UPDATE SaveSaleProduct SET Active = 0 WHERE SaleId=@SaleId";

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@SaleId", SaleId);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public bool DeleteSaveSale(int SaveSaleId)
        {
            try
            {
                DataSet ds = Spt_DeleteSaveSale(SaveSaleId);

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        public bool DeleteSaveSaleProduct(int SaveSaleId)
        {
            try
            {
                DataSet ds = Spt_DeleteSaveSaleProduct(SaveSaleId);

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        public DataSet Spt_GetMaxsSaveSaleId()
        {
            string sSql = @"SELECT ISNULL(SaveSaleId, 1) AS SaveSaleId FROM SaveSale";

            SqlParameter[] param = new SqlParameter[3];
            //param[0] = new SqlParameter("@CustomerId", CustomerId);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public int GetMaxsSaveSaleId()
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = Spt_GetMaxsSaveSaleId();
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow drv in dt.Rows)
                    {
                        sSaveSaleId = Convert.ToInt32(drv["SaveSaleId"]);
                    }
                }

                return sSaveSaleId;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return 0;
            }
        }

        public DataSet Spt_GetOutstandingDebt(int SaleId)
        {
            string sSql = @"SELECT CustomerId, RefId, ValueBalance, SubTotal, SetOffValue, DownValue, NetTotal, 
                            ABS(OutstandingDebt) OutstandingDebt, OutstandingDebt Refund, OutStandingStatus, 
                            LogTypeId, InsertDate 
                            FROM (
                            SELECT CustomerId, SaleId RefId, 0 OutStandingStatus, 5 LogTypeId, GETDATE() InsertDate, 
                            ValueBalance, SubTotal, SetOffValue, DownValue, NetTotal, 
                            CASE 
                            WHEN (DownValue + SetOffValue) > 0 THEN (SubTotal -(DownValue + SetOffValue))
                            ELSE SubTotal END AS OutstandingDebt
                            FROM Sale 
                            WHERE SaleId=@SaleId
                            ) CancelSale";

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@SaleId", SaleId);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public DataSet Spt_GetMaxSaveSaleId()
        {
            string sSql = @"SELECT ISNULL(SaveSaleId, 1) AS SaveSaleId FROM SaveSale";

            SqlParameter[] param = new SqlParameter[3];
            //param[0] = new SqlParameter("@CustomerId", CustomerId);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public int GetMaxSaveSaleId()
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = Spt_GetMaxSaveSaleId();
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow drv in dt.Rows)
                    {
                        sSaveSaleId = Convert.ToInt32(drv["SaveSaleId"]);
                    }
                }

                return sSaveSaleId;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return 0;
            }
        }

        public DataSet Rpt_GetWeightBalance(int SaleId)
        {
            string sSql = @"SELECT A.PriceId, D.SaleDate PriceDate, SalePriceAdvance, B.SaleId, SaleNumber, B.SaleDate, 
                            A.BeginAmt, A.WeightAmount_Raw, WeightBalanceAmt, LogId, A.LogTypeId, LogName, D.IsDefault
                            FROM TransactionLog A
							LEFT JOIN Sale B ON A.RefId = B.SaleId
							INNER JOIN LogType C ON A.LogTypeId = C.LogTypeId
							INNER JOIN CustomerPrice D ON A.PriceId = D.PriceId
							WHERE A.LogTypeId = 3 AND SalePriceAdvance != 0
							AND SaleId = @SaleId
							ORDER BY A.PriceId, LogId";

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@SaleId", SaleId);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public DataSet Spt_GetTempSale()
        {
            string sSql = @"SELECT SaleId, SaleNumber, SaleDate, CustomerId, CustomerName, CustomerAddress, LicensePlate, Phone, 
                            BeginBalance, ValueBalance, SubTotal, DownValue, SetOffValue, 
                            NetTotal, ProductTypeId, ProductUsing, IsFinalze, Active,
                            SaleProductId, PriceId, [Percentage], WeightAmount, WeightAmount_Plate, 
                            Drc, TotalPrice_Smoke, WeightAmount_Raw, TotalPrice_Raw, CalRubber, TotalPrice, Remark
                            FROM (
                            SELECT CAST(0 AS INT) SaleId, CAST('' AS nvarchar(150)) SaleNumber, CAST('' AS DATETIME) SaleDate, CAST(0 AS INT) CustomerId, 
							CAST('' AS nvarchar(150)) CustomerName, CAST('' AS nvarchar(150)) CustomerAddress, CAST('' AS nvarchar(150)) LicensePlate, 
							CAST('' AS nvarchar(150)) Phone, CAST('' AS money) BeginBalance, CAST('' AS money) ValueBalance, CAST('' AS money) SubTotal, 
							CAST('' AS money) DownValue, CAST('' AS money) SetOffValue, CAST('' AS money) NetTotal, CAST(0 AS INT) ProductTypeId, 
							CAST(0 AS BIT) IsFinalze,CAST(0 AS BIT) Active, CAST(0 AS INT) SaleProductId, CAST(0 AS INT) PriceId, CAST('' AS money) [Percentage], 
							CAST('' AS money) WeightAmount, CAST('' AS money) WeightAmount_Plate, CAST('' AS money) Drc, CAST('' AS money) TotalPrice_Smoke, 
							CAST('' AS money) WeightAmount_Raw, CAST('' AS money) TotalPrice_Raw, CAST('' AS money) CalRubber, CAST('' AS money) TotalPrice, 
							CAST('' AS nvarchar(250)) Remark, CAST('' AS nvarchar(250)) ProductUsing
                            ) Sale WHERE SaleId <> 0";

            SqlParameter[] param = new SqlParameter[3];
            //param[0] = new SqlParameter("@BuyId", BuyId);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

    }
}
