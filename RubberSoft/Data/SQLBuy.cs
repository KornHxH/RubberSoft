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
    class SQLBuy
    {
        readonly SQLData SQLData = new SQLData();

        private int OutStandingLogId, TransactionLogId, sSaveBuyId;
        //private decimal OutstandingDebt;

        public DataSet GetBuyCustomerTypes()
        {
            string sSql = @"SELECT CustomerTypeId, CustomerTypeName 
                            FROM CustomerTypes
                            WHERE CustomerTypeId IN (1) ORDER BY CustomerTypeId";

            SqlParameter[] param = new SqlParameter[3];
            //param[0] = new SqlParameter("@UnitId", id);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public DataSet GetLKBuyCustomer(int CustomerGroupId)
        {
            string sSql = @"SELECT CustomerId, A.CustomerTypeId, CustomerTypeName, CustomerCode, CustomerName, 
                            CustomerAddress, LicensePlate, Phone
                            FROM Customer A
                            LEFT OUTER JOIN CustomerTypes B ON A.CustomerTypeId = B.CustomerTypeId
                            WHERE A.Active = 1 AND A.CustomerTypeId IN (1)";

            if (CustomerGroupId != 0)
            {
                sSql += @" AND CustomerGroupId = @CustomerGroupId";
            }

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@CustomerGroupId", CustomerGroupId);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public DataSet Spt_GetBuyNumber(DateTime date)
        {
            string sSql = "spt_GetBuyNumber";

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@BuyDate", date);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.StoredProcedure, sSql, param);
        }

        public DataSet Spt_GetConsignmentNumber(DateTime date)
        {
            string sSql = "spt_GetConsignmentNumber";

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@BuyDate", date);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.StoredProcedure, sSql, param);
        }

        public DataSet Spt_GetSaveBuyNumber(DateTime date)
        {
            string sSql = "spt_GetSaveBuyNumber";

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@BuyDate", date);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.StoredProcedure, sSql, param);
        }

        public DataSet GetBuyData(DateTime Start, DateTime End, int CustomerId)
        {
            string sSql = @"SELECT BuyId, BuyNumber, CustomerId, CustomerName, BuyDate, 
                            ValueBalance, SubTotal, DownValue, SetOffValue, 
                            NetTotal, ProductTypeId, IsFinalze, Active, InsertDate, InsertBy, 
                            UpdateDate, UpdateBy, DeleteDate, DeleteBy
                            FROM Buy
                            WHERE Active = 1 
                            AND BuyDate BETWEEN @Start AND @End ";

            if (CustomerId != 0)
            {
                sSql += @"AND CustomerId = @CustomerId ";
            }

            sSql += @"ORDER BY BuyId";

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Start", Start);
            param[1] = new SqlParameter("@End", End);
            param[2] = new SqlParameter("@CustomerId", CustomerId);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public DataSet GetProductById(int pid)
        {
            string sSql = @"SELECT ProductId, ProductName, BuyDrc, SaleDrc FROM Products 
                            WHERE ProductId = @ProductId";

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@ProductId", pid);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public DataSet GetMaxBuyId()
        {
            string sSql = @"SELECT ISNULL(BuyId, 0)  + 1 AS BuyId FROM (
                            SELECT MAX(BuyId) BuyId FROM Buy) Buy";

            SqlParameter[] param = new SqlParameter[3];
            //param[0] = new SqlParameter("@BuyDate", date);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public DataSet Spt_GetTempBuy()
        {
            string sSql = @"SELECT BuyId, BuyNumber, BuyDate, CustomerId, CustomerName, CustomerAddress, LicensePlate, Phone, 
                            BeginBalance, ValueBalance, SubTotal, DownValue, SetOffValue, 
                            NetTotal, ProductTypeId, ProductUsing, IsFinalze, Active,
                            BuyProductId, PriceId, [Percentage], WeightAmount, WeightAmount_Plate, 
                            Drc, TotalPrice_Smoke, WeightAmount_Raw,  
                            TotalPrice_Raw, CalRubber, TotalPrice, Remark
                            FROM (
                            SELECT CAST(0 AS INT) BuyId, CAST('' AS nvarchar(150)) BuyNumber, CAST('' AS DATETIME) BuyDate, CAST(0 AS INT) CustomerId, 
							CAST('' AS nvarchar(150)) CustomerName, CAST('' AS nvarchar(150)) CustomerAddress, CAST('' AS nvarchar(150)) LicensePlate, 
							CAST('' AS nvarchar(150)) Phone, CAST('' AS money) BeginBalance, CAST('' AS money) ValueBalance, CAST('' AS money) SubTotal, 
							CAST('' AS money) DownValue, CAST('' AS money) SetOffValue, CAST('' AS money) NetTotal, CAST(0 AS INT) ProductTypeId, 
							CAST(0 AS BIT) IsFinalze,CAST(0 AS BIT) Active, CAST(0 AS INT) BuyProductId, CAST(0 AS INT) PriceId, CAST('' AS money) [Percentage], 
							CAST('' AS money) WeightAmount, CAST('' AS money) WeightAmount_Plate, CAST('' AS money) Drc, CAST('' AS money) TotalPrice_Smoke, 
							CAST('' AS money) WeightAmount_Raw, CAST('' AS money) TotalPrice_Raw, CAST('' AS money) CalRubber, CAST('' AS money) TotalPrice, 
							CAST('' AS nvarchar(250)) Remark, CAST('' AS nvarchar(250)) ProductUsing
                            ) Buy WHERE BuyId <> 0";

            SqlParameter[] param = new SqlParameter[3];
            //param[0] = new SqlParameter("@BuyId", BuyId);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public DataSet Spt_GetTempBuyProduct()
        {
            string sSql = @"SELECT RunNo, BuyProductId, BuyId, PriceId, ItemPriceId, PriceName, [Percentage], WeightAmount, WeightAmount_Plate, Drc, TotalPrice_Smoke, WeightAmount_Raw,  
                            TotalPrice_Raw, CalRubber, TotalPrice, sTotalPrice, Remark, WeightBalanceAmt, IsDefault, IsSelect
						    FROM (
                            SELECT CAST(0 AS INT) RunNo, CAST(0 AS INT) BuyProductId, CAST(0 AS INT) BuyId, CAST(0 AS INT) PriceId, CAST(0 AS INT) ItemPriceId, 
                            CAST('' AS NVARCHAR(250)) PriceName, CAST(0 AS MONEY) [Percentage], CAST(0 AS MONEY) WeightAmount,  
							CAST(0 AS MONEY) WeightAmount_Plate, CAST(0 AS MONEY) Drc, CAST(0 AS MONEY) TotalPrice_Smoke, CAST(0 AS MONEY) WeightAmount_Raw, 
                            CAST(0 AS MONEY) TotalPrice_Raw, CAST(0 AS MONEY) CalRubber, CAST(0 AS MONEY) TotalPrice, CAST(0 AS MONEY) sTotalPrice, 
							CAST('' AS NVARCHAR(500)) Remark, CAST(0 AS MONEY) WeightBalanceAmt, CAST(0 AS BIT) IsDefault, CAST(0 AS BIT) IsSelect
                            ) BuyProduct WHERE RunNo IS NULL";

            SqlParameter[] param = new SqlParameter[3];
            //param[0] = new SqlParameter("@BuyId", BuyId);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public DataSet Spt_GetBuyProduct(int BuyId)
        {
            string sSql = @"SELECT BuyProductId AS RunNo, BuyProductId, BuyId, A.PriceId, ItemPriceId, A.PriceName, [Percentage], 
                            A.WeightAmount, WeightAmount_Plate, Drc, TotalPrice_Smoke, WeightAmount_Raw, 
                            TotalPrice_Raw, CalRubber, TotalPrice, TotalPrice AS sTotalPrice, Remark,
							ISNULL(WeightBalanceAmt, 0) WeightBalanceAmt, C.IsDefault
                            FROM BuyProduct A
							LEFT JOIN WeightBalance B ON A.PriceId = B.PriceId
							INNER JOIN CustomerPrice C ON A.PriceId = C.PriceId
                            WHERE BuyId = @BuyId AND A.Active=1";

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@BuyId", BuyId);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public DataSet Spt_GetSaveBuyData(string ProductUsing, int ProductTypeId, int TypeSave)
        {
            string sSql = @"SELECT SaveBuyId, BuyNumber, CustomerId, CustomerName, BuyDate, SubTotal, DownValue, NetTotal, SetOffValue, 
                            ValueBalance, ProductTypeId, ProductUsing, InsertDate, InsertBy, Active, ISNULL(TypeSave, 0) TypeSave, 
                            CASE WHEN ProductTypeId = 1 THEN SumWeightAmount ELSE SumTotalPrice_Raw END AS WeightAmountTotal
                            FROM SaveBuy A
                            INNER JOIN (
                            SELECT BuyId, SUM(TotalPrice_Raw) SumTotalPrice_Raw, SUM(WeightAmount) SumWeightAmount 
                            FROM SaveBuyProduct WHERE Active = 1
                            GROUP BY BuyId
                            ) B ON A.SaveBuyId = B.BuyId
                            WHERE Active = 1";

            if (TypeSave == 0)
            {
                if (!string.IsNullOrEmpty(ProductUsing))
                {
                    sSql += @" AND ProductUsing = @ProductUsing";
                }

                if (ProductTypeId != 0)
                {
                    sSql += @" AND ProductTypeId = @ProductTypeId";
                }

                sSql += @" AND ISNULL(TypeSave, 0) = @TypeSave";
            }
            else
            {
                sSql += @" AND TypeSave = @TypeSave";
            }  

            sSql += " ORDER BY BuyNumber";

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@ProductUsing", ProductUsing);
            param[1] = new SqlParameter("@ProductTypeId", ProductTypeId);
            param[2] = new SqlParameter("@TypeSave", TypeSave);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public DataSet Spt_GetSaveBuyProduct(int BuyId)
        {
            string sSql = @"SELECT BuyProductId AS RunNo, BuyProductId, BuyId, A.PriceId, ItemPriceId, A.PriceName, [Percentage], 
                            A.WeightAmount, WeightAmount_Plate, Drc, TotalPrice_Smoke, WeightAmount_Raw, 
                            TotalPrice_Raw, CalRubber, TotalPrice, TotalPrice AS sTotalPrice, Remark, IsDefault
                            FROM SaveBuyProduct A
							LEFT JOIN WeightBalance B ON A.PriceId = B.PriceId
							INNER JOIN CustomerPrice C ON A.PriceId = C.PriceId
                            WHERE A.Active = 1 AND A.BuyId = @BuyId";

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@BuyId", BuyId);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public DataSet Spt_GetBuyProductType(int BuyId)
        {
            string sSql = @"SELECT BuyId,  ProductTypeId
                            FROM (
                            SELECT BuyId, CASE WHEN CalRubber = 0 THEN 1 ELSE 2 END AS ProductTypeId
                            FROM BuyProduct ) BuyProduct 
                            WHERE BuyId = @BuyId
                            GROUP BY BuyId,  ProductTypeId";

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@BuyId", BuyId);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public DataSet Spt_GetBuyValueBalance(int cid)
        {
            string sSql = @"SELECT ISNULL(OutstandingDebt, 0) AS NetTotal FROM OutStandingBalance
                            WHERE CustomerId = @CustomerId";

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@CustomerId", cid);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public DataSet Spt_GetBuyWeightBalance(int cid)
        {
            string sSql = @"SELECT PriceId, CustomerId, sName, PriceName, SaleDate, SalePriceAdvance, WeightAmount, BeginAmt, 
                            DeliveryPrice, BeginBalanceAmt, WeightBalanceAmt, LastLogId, Active, ItemPriceId, UnitPrice, IsDefault
                            FROM (

                            SELECT A.PriceId, A.CustomerId, A.PriceId AS sName, PriceName, SaleDate, SalePriceAdvance, WeightAmount, 
                            DeliveryPrice, ISNULL(BeginBalanceAmt, 0) BeginBalanceAmt, ISNULL(WeightBalanceAmt, 0) WeightBalanceAmt, 
							ISNULL(WeightBalanceAmt, 0) AS BeginAmt, LastLogId, A.Active, ItemPriceId, 
                            ISNULL(UnitPrice, 0) UnitPrice, A.IsDefault, CAST(0 AS MONEY) CalAmount
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

        public DataSet Spt_GetCancelBuy(int CustomerId)
        {
            string sSql = @"SELECT CustomerId, RefId, ValueBalance, SubTotal, SetOffValue, DownValue, NetTotal, 
                            OutstandingDebt, OutStandingStatus, LogTypeId, InsertDate 
                            FROM (
                            SELECT CustomerId, BuyId RefId, 0 OutStandingStatus, 5 LogTypeId, InsertDate, 
                            ValueBalance, SubTotal, SetOffValue, DownValue, NetTotal, 
                            CASE 
                            WHEN (DownValue + SetOffValue) > 0 THEN (SubTotal -(DownValue + SetOffValue))
                            ELSE SubTotal END AS OutstandingDebt
                            FROM Buy 
                            WHERE CustomerId=@CustomerId
                            ) CancelBuy";

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@CustomerId", CustomerId);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public DataSet Spt_DeleteBuy(int BuyId, int DeleteBy)
        {
            string sSql = @"Update Buy SET Active=0, DeleteDate=GETDATE(), DeleteBy=@DeleteBy 
                            WHERE BuyId=@BuyId";

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@BuyId", BuyId);
            param[1] = new SqlParameter("@DeleteBy", DeleteBy);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public bool DeleteBuy(int BuyId)
        {
            try
            {
                DataSet ds = Spt_DeleteBuy(BuyId, ClassProperty.permisUserID);

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        public bool AddLogCancelBuy(int BuyId)
        {
            try
            {
                DataSet ds = Spt_AddLogCancelBuy(BuyId);

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        public DataSet Spt_AddLogCancelBuy(int BuyId)
        {
            string sSql = @"INSERT INTO OutStandingLog (CustomerId, BeginOutstandingDebt, OutstandingDebt, RefId, OutStandingStatus, LogTypeId , InsertDate)
                            
                            SELECT CustomerId, 
							CASE 
                            WHEN BeginOutstandingDebt > 0 THEN ABS(BeginOutstandingDebt)
                            ELSE BeginOutstandingDebt 
							END AS BeginOutstandingDebt, 
							OutstandingDebt, RefId, OutStandingStatus, LogTypeId, InsertDate 
							FROM (
                            SELECT A.CustomerId, BuyId RefId, 0 OutStandingStatus, 5 LogTypeId, GETDATE() InsertDate, 
                            ValueBalance, SubTotal, SetOffValue, DownValue, NetTotal, 
                            CASE 
                            WHEN (DownValue + SetOffValue) > 0 THEN (SubTotal -(DownValue + SetOffValue))
                            ELSE SubTotal END AS OutstandingDebt, OutstandingDebt BeginOutstandingDebt
                            FROM Buy A
							INNER JOIN OutStandingBalance B ON A.CustomerId = B.CustomerId
                            WHERE BuyId=@BuyId
                            ) CancelBuy";

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@BuyId", BuyId);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public bool VoidBuy(int BuyId)
        {
            try
            {
                if (AddLogCancelBuy(BuyId))
                {
                    if (DeleteBuy(BuyId))
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

        public DataSet Spt_UpdateOutStandingBalance(int CustomerId, decimal OutstandingDebt, int LastLogId)
        {
            string sSql = @"Update OutStandingBalance SET OutstandingDebt=@OutstandingDebt, 
                            LastLogId=@LastLogId, UpdateDate=GETDATE() 
                            WHERE CustomerId=@CustomerId";

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@CustomerId", CustomerId);
            param[1] = new SqlParameter("@OutstandingDebt", OutstandingDebt);
            param[2] = new SqlParameter("@LastLogId", LastLogId);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public bool UpdateOutStandingBalance(int CustomerId, decimal OutstandingDebt, int LastLogId)
        {
            try
            {
                DataSet ds = Spt_UpdateOutStandingBalance(CustomerId, OutstandingDebt, LastLogId);

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        public DataSet Spt_GetOutstandingDebt(int BuyId)
        {
            string sSql = @"SELECT CustomerId, RefId, ValueBalance, SubTotal, SetOffValue, DownValue, NetTotal, 
                            ABS(OutstandingDebt) OutstandingDebt, OutstandingDebt Refund, OutStandingStatus, 
                            LogTypeId, InsertDate 
                            FROM (
                            SELECT CustomerId, BuyId RefId, 0 OutStandingStatus, 5 LogTypeId, GETDATE() InsertDate, 
                            ValueBalance, SubTotal, SetOffValue, DownValue, NetTotal, 
                            CASE 
                            WHEN (DownValue + SetOffValue) > 0 THEN (SubTotal -(DownValue + SetOffValue))
                            ELSE SubTotal END AS OutstandingDebt
                            FROM Buy 
                            WHERE BuyId=@BuyId
                            ) CancelBuy";

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@BuyId", BuyId);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public DataSet Spt_GetMaxOutStandingLogId()
        {
            string sSql = @"SELECT ISNULL(MAX(OutStandingLogId), 1) AS OutStandingLogId 
                            FROM OutStandingLog";

            SqlParameter[] param = new SqlParameter[3];
            //param[0] = new SqlParameter("@CustomerId", CustomerId);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public int GetOutStandingLogId()
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = Spt_GetMaxOutStandingLogId();
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow drv in dt.Rows)
                    {
                        OutStandingLogId = Convert.ToInt32(drv["OutStandingLogId"]);
                    }
                }

                return OutStandingLogId;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return 0;
            }
        }

        public DataSet Spt_GetOutStandingLog(int LogId)
        {
            string sSql = @"SELECT OutStandingLogId, CustomerId, OutstandingDebt, RefId, OutStandingStatus, LogTypeId 
                            FROM OutStandingLog
                            WHERE OutStandingLogId=@LogId";

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@LogId", LogId);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public DataSet Spt_GetMaxTransactionLogId(int CustomerId)
        {
            string sSql = @"SELECT ISNULL(MAX(OutStandingLogId), 1) AS OutStandingLogId 
                            FROM OutStandingLog WHERE CustomerId=@CustomerId";

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@CustomerId", CustomerId);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public DataSet Spt_GetMaxTransactionLogId()
        {
            string sSql = @"SELECT ISNULL(MAX(LogId), 1) AS LogId FROM TransactionLog";

            SqlParameter[] param = new SqlParameter[3];
            //param[0] = new SqlParameter("@CustomerId", CustomerId);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public int GetTransactionLogId()
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = Spt_GetMaxTransactionLogId();
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow drv in dt.Rows)
                    {
                        TransactionLogId = Convert.ToInt32(drv["LogId"]);
                    }
                }

                return TransactionLogId;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return 0;
            }
        }

        public DataSet Spt_GetWeightBalanceValue(int PriceId)
        {
            string sSql = @"SELECT WeightBalanceId, PriceId, BeginBalanceAmt, WeightBalanceAmt
                            FROM WeightBalance 
                            WHERE PriceId=@PriceId";

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@PriceId", PriceId);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public DataSet Spt_UpdateWeightBalance(int PriceId, decimal WeightBalanceAmt, int LastLogId)
        {
            string sSql = @"Update WeightBalance SET WeightBalanceAmt=@WeightBalanceAmt, 
                            LastLogId=@LastLogId, UpdateDate=GETDATE() 
                            WHERE PriceId=@PriceId";

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@PriceId", PriceId);
            param[1] = new SqlParameter("@WeightBalanceAmt", WeightBalanceAmt);
            param[2] = new SqlParameter("@LastLogId", LastLogId);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public bool UpdateWeightBalance(int PriceId, decimal WeightBalanceAmt, int LastLogId)
        {
            try
            {
                DataSet ds = Spt_UpdateWeightBalance(PriceId, WeightBalanceAmt, LastLogId);

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

        public DataSet Spt_DeleteSaveBuy(int SaveBuyId)
        {
            string sSql = @"UPDATE SaveBuy SET Active = 0 WHERE SaveBuyId=@SaveBuyId";

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@SaveBuyId", SaveBuyId);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public DataSet Spt_DeleteSaveBuyProduct(int BuyId)
        {
            string sSql = @"UPDATE SaveBuyProduct SET Active = 0 WHERE BuyId=@BuyId";

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@BuyId", BuyId);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public bool DeleteSaveBuy(int SaveBuyId)
        {
            try
            {
                DataSet ds = Spt_DeleteSaveBuy(SaveBuyId);

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        public bool DeleteSaveBuyProduct(int BuyId)
        {
            try
            {
                DataSet ds = Spt_DeleteSaveBuyProduct(BuyId);

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        public DataSet Spt_GetMaxSaveBuyId()
        {
            string sSql = @"SELECT ISNULL(SaveBuyId, 1) AS SaveBuyId FROM SaveBuy";

            SqlParameter[] param = new SqlParameter[3];
            //param[0] = new SqlParameter("@CustomerId", CustomerId);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public int GetMaxSaveBuyId()
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = Spt_GetMaxSaveBuyId();
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow drv in dt.Rows)
                    {
                        sSaveBuyId = Convert.ToInt32(drv["SaveBuyId"]);
                    }
                }

                return sSaveBuyId;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return 0;
            }
        }

        public DataSet Spt_GetMaxConsignmentId()
        {
            string sSql = @"SELECT ISNULL(ConsignmentId, 1) AS ConsignmentId FROM Consignment";

            SqlParameter[] param = new SqlParameter[3];
            //param[0] = new SqlParameter("@CustomerId", CustomerId);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public int GetMaxConsignmentId()
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = Spt_GetMaxConsignmentId();
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow drv in dt.Rows)
                    {
                        sSaveBuyId = Convert.ToInt32(drv["ConsignmentId"]);
                    }
                }

                return sSaveBuyId;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return 0;
            }
        }

        public DataSet Rpt_GetWeightBalance(int BuyId)
        {
            string sSql = @"SELECT A.PriceId, D.SaleDate PriceDate, SalePriceAdvance, B.BuyId, BuyNumber, BuyDate, 
                            A.BeginAmt, A.WeightAmount_Raw, WeightBalanceAmt, LogId, A.LogTypeId, LogName, D.IsDefault, D.PriceName
                            FROM TransactionLog A
							LEFT JOIN Buy B ON A.RefId = B.BuyId
							INNER JOIN LogType C ON A.LogTypeId = C.LogTypeId
							INNER JOIN CustomerPrice D ON A.PriceId = D.PriceId
							WHERE A.LogTypeId = 2 
							AND BuyId = @BuyId
							ORDER BY A.PriceId, LogId";

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@BuyId", BuyId);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public DataSet Spt_GetTempWeightBalance()
        {
            string sSql = @"SELECT PriceDate, PriceName, SalePriceAdvance, BeginAmt, WeightAmount_Raw, WeightBalanceAmt, IsDefault 
                           FROM (
                           SELECT CAST('' AS DATETIME) PriceDate, CAST('' AS nvarchar(150)) PriceName, CAST('' AS money) SalePriceAdvance, 
                           CAST('' AS money) BeginAmt, CAST('' AS money) WeightAmount_Raw, CAST('' AS money) WeightBalanceAmt, CAST('' AS BIT) IsDefault
						   ) TempWeightBalance 
                           WHERE PriceDate IS NULL";

            SqlParameter[] param = new SqlParameter[3];
            //param[0] = new SqlParameter("@BuyId", BuyId);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }
    }
}
