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
    class SQLReport
    {
        readonly SQLData SQLData = new SQLData();

        public DataSet Spt_GetReportGroups()
        {
            string sSql = @"SELECT 0 AS ReportGroupId, '--- SelectGroup Report ---' AS ReportGroupName
                            UNION ALL
                            SELECT ReportGroupId, ReportGroupName FROM ReportGroups ORDER BY ReportGroupId";

            SqlParameter[] param = new SqlParameter[3];
            //param[0] = new SqlParameter("@BuyId", BuyId);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public DataSet Spt_GetReport(int ReportGroupId)
        {
            string sSql = @"SELECT 0 AS ReportId, 0 AS ReportGroupId, '--- Select Report ---' AS ReportName
                            UNION ALL
                            SELECT ReportId, ReportGroupId, ReportName FROM Reports WHERE ReportGroupId = @ReportGroupId";

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@ReportGroupId", ReportGroupId);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public DataSet GetLKBuyCustomer(int CustomerTypeId)
        {
            string sSql = @"SELECT 0 AS CustomerId, '0000' AS CustomerCode, '--- แสดงทั้งหมด ---' AS CustomerName
                            UNION ALL
                            SELECT CustomerId, CustomerCode, CustomerName
                            FROM Customer A
                            LEFT OUTER JOIN CustomerTypes B ON A.CustomerTypeId = B.CustomerTypeId
                            WHERE A.Active = 1 AND A.CustomerTypeId=@CustomerTypeId";

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@CustomerTypeId", CustomerTypeId);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public DataSet Spt_GetBuyBill(int BuyId)
        {
            string sSql = @"SELECT BuyId, BuyNumber, BuyDate, CustomerId, CustomerName, 
                            CustomerAddress, LicensePlate, Phone, 
                            BeginBalance, ValueBalance, SubTotal, DownValue, SetOffValue, 
                            NetTotal, ProductTypeId, IsFinalze, Active,
                            BuyProductId, PriceId, [Percentage], WeightAmount, WeightAmount_Plate, 
                            Drc, TotalPrice_Smoke, WeightAmount_Raw,  
                            TotalPrice_Raw, CalRubber, TotalPrice, Remark, ProductUsing
                            FROM (
                            SELECT A.BuyId, BuyNumber, BuyDate, A.CustomerId, A.CustomerName, 
                            CustomerAddress, LicensePlate, Phone, 
                            BeginBalance, ValueBalance, SubTotal, DownValue, SetOffValue, 
                            NetTotal, ProductTypeId, IsFinalze, A.Active,
                            BuyProductId, PriceId, [Percentage], WeightAmount, WeightAmount_Plate, 
                            Drc, TotalPrice_Smoke, WeightAmount_Raw,  
                            TotalPrice_Raw, CalRubber, TotalPrice, Remark, A.ProductUsing
                            FROM Buy A
                            INNER JOIN Customer B ON A.CustomerId = B.CustomerId
                            INNER JOIN BuyProduct C ON A.BuyId = C.BuyId
                            ) Buy
                            WHERE BuyId = @BuyId";

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@BuyId", BuyId);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public DataSet Spt_GetSaleBill(int SaleId, int CustomerId)
        {
            string sSql = @"SELECT SaleId, SaleNumber, SaleDate, CustomerId, CustomerName, 
                            CustomerAddress, LicensePlate, Phone, 
                            BeginBalance, ValueBalance, SubTotal, DownValue, SetOffValue, NetTotal, ProductTypeId, 
                            IsFinalze, Active, SaleProductId, PriceId, [Percentage], WeightAmount, WeightAmount_Plate, 
                            Drc, TotalPrice_Smoke, WeightAmount_Raw,  
                            TotalPrice_Raw, CalRubber, TotalPrice, Remark
                            FROM (
                            SELECT A.SaleId, SaleNumber, SaleDate, A.CustomerId, A.CustomerName, CustomerAddress, 
                            LicensePlate, Phone, BeginBalance, ValueBalance, SubTotal, DownValue, SetOffValue, 
                            NetTotal, ProductTypeId, IsFinalze, A.Active,
                            SaleProductId, PriceId, [Percentage], WeightAmount, WeightAmount_Plate, Drc, 
                            TotalPrice_Smoke, WeightAmount_Raw, TotalPrice_Raw, CalRubber, TotalPrice, Remark
                            FROM Sale A
                            INNER JOIN Customer B ON A.CustomerId = B.CustomerId
                            INNER JOIN SaleProduct C ON A.SaleId = C.SaleId
                            ) Sale
                            WHERE SaleId = @SaleId";

            if (CustomerId != 0)
            {
                sSql += " AND CustomerId =  @CustomerId";
            }

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@SaleId", SaleId);
            param[1] = new SqlParameter("@CustomerId", CustomerId);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        // By Date

        public DataSet Rpt_BuyByDate_1(DateTime StartDate, DateTime EndDate, int ProductTypeId, int CustomerId, string ProductUsing)
        {
            string sSql = @"select Buy.BuyNumber,CAST(Buy.BuyDate as date) as BuyDate, Buy.BuyDate as Buytime
                            ,BuyProduct.WeightAmount
                            ,BuyProduct.Drc
                            ,BuyProduct.WeightAmount_Raw
                            ,BuyProduct.TotalPrice_Raw
                            ,BuyProduct.TotalPrice
                            ,Customer.CustomerId
                            ,Customer.CustomerName
                            ,Buy.ProductUsing

                            from Buy

                            inner join BuyProduct on BuyProduct.BuyId = Buy.BuyId
                            inner join Customer on Customer.CustomerId = buy.CustomerId

                            where Buy.Active = 1 and buy.IsFinalze = 1
                            and BuyDate BETWEEN @StartDate AND @EndDate AND ProductTypeId =  @ProductTypeId";

            if (CustomerId != 0)
            {
                sSql += " AND Customer.CustomerId =  @CustomerId";
            }

            if (!string.IsNullOrEmpty(ProductUsing))
            {
                sSql += @" AND ProductUsing = @ProductUsing";
            }

            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@StartDate", StartDate);
            param[1] = new SqlParameter("@EndDate", EndDate);
            param[2] = new SqlParameter("@ProductTypeId", ProductTypeId);
            param[3] = new SqlParameter("@CustomerId", CustomerId);
            param[4] = new SqlParameter("@ProductUsing", ProductUsing);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public DataSet Rpt_BuyByDate_2(DateTime StartDate, DateTime EndDate, int ProductTypeId, int CustomerId)
        {
            string sSql = @"select Customer.CustomerId,Customer.CustomerCode,Customer.CustomerName 
                            ,Buy.BuyId,Buy.BuyNumber,buy.BuyDate,Buy.SubTotal,Buy.DownValue,buy.NetTotal,
                            Buy.SetOffValue,Buy.ValueBalance
                            ,BuyProduct.WeightAmount,BuyProduct.WeightAmount_Plate,BuyProduct.Drc,
                            BuyProduct.TotalPrice_Smoke,BuyProduct.WeightAmount_Raw,BuyProduct.TotalPrice_Raw,
                            BuyProduct.TotalPrice,BuyProduct.Remark
                            ,BuyProduct.CalRubber  
                            ,Buy.ProductUsing
                            from Buy
                            inner join Customer on Customer.CustomerId = Buy.CustomerId
                            inner join BuyProduct on Buy.BuyId = BuyProduct.BuyId
                            where Buy.IsFinalze = 1 and Buy.Active = 1
                            and BuyDate BETWEEN @StartDate AND @EndDate AND ProductTypeId=@ProductTypeId";

            if (CustomerId != 0)
            {
                sSql += " AND Customer.CustomerId =  @CustomerId";
            }

            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@StartDate", StartDate);
            param[1] = new SqlParameter("@EndDate", EndDate);
            param[2] = new SqlParameter("@ProductTypeId", ProductTypeId);
            param[3] = new SqlParameter("@CustomerId", CustomerId);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }
        //ByDate
        public DataSet Rpt_BuyWeightBalanceBydate(DateTime StartDate, DateTime EndDate, int CustomerId)
        {
            string sSql = @"SELECT CustomerId,CustomerName,WeightBalanceAmt FROM (select Customer.CustomerId,Customer.CustomerName,SUM(WeightBalance.WeightBalanceAmt) AS WeightBalanceAmt  
                            FROM WeightBalance
                            INNER JOIN Customer ON WeightBalance.CustomerId = Customer.CustomerId AND Customer.Active != 0
                            INNER JOIN CustomerPrice ON CustomerPrice.PriceId = WeightBalance.PriceId AND CustomerPrice.IsDefault != 1 AND CustomerPrice.Active = 1
                            GROUP BY Customer.CustomerId,Customer.CustomerName)Tmp";

            if (CustomerId != 0)
            {
                sSql += " WHERE Tmp.CustomerId = @CustomerId";
            }

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@StartDate", StartDate);
            param[1] = new SqlParameter("@EndDate", EndDate);
            param[2] = new SqlParameter("@CustomerId", CustomerId);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        // By Month
        public DataSet Rpt_BuyByMonth_1(DateTime StartDate, int ProductTypeId, int CustomerId, string ProductUsing)
        {
            string sSql = @"select Buy.BuyNumber,CAST(Buy.BuyDate as date) as BuyDate, Buy.BuyDate as Buytime
                            ,BuyProduct.WeightAmount
                            ,BuyProduct.Drc
                            ,BuyProduct.WeightAmount_Raw
                            ,BuyProduct.TotalPrice_Raw
                            ,BuyProduct.TotalPrice
                            ,Customer.CustomerId
                            ,Customer.CustomerName
                            ,Buy.ProductUsing

                            from Buy

                            inner join BuyProduct on BuyProduct.BuyId = Buy.BuyId
                            inner join Customer on Customer.CustomerId = buy.CustomerId

                            where Buy.Active = 1 and buy.IsFinalze = 1
                            and YEAR(BuyDate) = YEAR(@StartDate) AND MONTH(BuyDate) = MONTH(@StartDate) 
                            AND ProductTypeId =  @ProductTypeId";

            if (CustomerId != 0)
            {
                sSql += " AND Customer.CustomerId =  @CustomerId";
            }

            if (!string.IsNullOrEmpty(ProductUsing))
            {
                sSql += @" AND ProductUsing = @ProductUsing";
            }

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@StartDate", StartDate);
            param[1] = new SqlParameter("@ProductTypeId", ProductTypeId);
            param[2] = new SqlParameter("@CustomerId", CustomerId);
            param[3] = new SqlParameter("@ProductUsing", ProductUsing);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public DataSet Rpt_BuyByMonth_2(DateTime StartDate, int ProductTypeId, int CustomerId)
        {
            string sSql = @"select Customer.CustomerId,Customer.CustomerCode,Customer.CustomerName 
                            ,Buy.BuyId,Buy.BuyNumber,buy.BuyDate,Buy.SubTotal,Buy.DownValue,
                            buy.NetTotal,Buy.SetOffValue,Buy.ValueBalance
                            ,BuyProduct.WeightAmount,BuyProduct.WeightAmount_Plate,BuyProduct.Drc,
                            BuyProduct.TotalPrice_Smoke,BuyProduct.WeightAmount_Raw,BuyProduct.TotalPrice_Raw,
                            BuyProduct.TotalPrice,BuyProduct.Remark
                            ,BuyProduct.CalRubber   
                            from Buy
                            inner join Customer on Customer.CustomerId = Buy.CustomerId
                            inner join BuyProduct on Buy.BuyId = BuyProduct.BuyId
                            where Buy.IsFinalze = 1 and Buy.Active = 1
                            AND YEAR(BuyDate) = YEAR(@StartDate) AND MONTH(BuyDate) = MONTH(@StartDate) 
                            AND ProductTypeId =  @ProductTypeId";

            if (CustomerId != 0)
            {
                sSql += " AND Customer.CustomerId =  @CustomerId";
            }

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@StartDate", StartDate);
            param[1] = new SqlParameter("@ProductTypeId", ProductTypeId);
            param[2] = new SqlParameter("@CustomerId", CustomerId);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public DataSet Rpt_BuyWeightBalanceByMonth(DateTime StartDate, int CustomerId)
        {
            string sSql = @"SELECT CustomerId,CustomerName,WeightBalanceAmt FROM (select Customer.CustomerId,Customer.CustomerName,SUM(WeightBalance.WeightBalanceAmt) AS WeightBalanceAmt  
                            FROM WeightBalance
                            INNER JOIN Customer ON WeightBalance.CustomerId = Customer.CustomerId AND Customer.Active != 0
                            INNER JOIN CustomerPrice ON CustomerPrice.PriceId = WeightBalance.PriceId AND CustomerPrice.IsDefault != 1 AND CustomerPrice.Active = 1
                            GROUP BY Customer.CustomerId,Customer.CustomerName)Tmp";

            if (CustomerId != 0)
            {
                sSql += " WHERE Tmp.CustomerId =  @CustomerId";
            }

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@StartDate", StartDate);
            param[1] = new SqlParameter("@CustomerId", CustomerId);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

  // By Year

        public DataSet Rpt_BuyByYear_1(DateTime StartDate, int ProductTypeId, int CustomerId, string ProductUsing)
        {
            string sSql = @"select Buy.BuyNumber,CAST(Buy.BuyDate as date) as BuyDate, Buy.BuyDate as Buytime
                            ,BuyProduct.WeightAmount
                            ,BuyProduct.Drc
                            ,BuyProduct.WeightAmount_Raw
                            ,BuyProduct.TotalPrice_Raw
                            ,BuyProduct.TotalPrice
                            ,Customer.CustomerId
                            ,Customer.CustomerName
                            ,Buy.ProductUsing
                            from Buy

                            inner join BuyProduct on BuyProduct.BuyId = Buy.BuyId
                            inner join Customer on Customer.CustomerId = buy.CustomerId

                            where Buy.Active = 1 and buy.IsFinalze = 1
                            and YEAR(BuyDate) = YEAR(@StartDate) AND ProductTypeId =  @ProductTypeId";

            if (CustomerId != 0)
            {
                sSql += " AND Customer.CustomerId =  @CustomerId";
            }

            if (!string.IsNullOrEmpty(ProductUsing))
            {
                sSql += " AND Buy.ProductUsing = @ProductUsing";
            }

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@StartDate", StartDate);
            param[1] = new SqlParameter("@ProductTypeId", ProductTypeId);
            param[2] = new SqlParameter("@CustomerId", CustomerId);
            param[3] = new SqlParameter("@ProductUsing", ProductUsing);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public DataSet Rpt_BuyByYear_2(DateTime StartDate, int ProductTypeId, int CustomerId)
        {
            string sSql = @"select Customer.CustomerId,Customer.CustomerCode,Customer.CustomerName 
                            ,Buy.BuyId,Buy.BuyNumber,buy.BuyDate,Buy.SubTotal,Buy.DownValue,
                            buy.NetTotal,Buy.SetOffValue,Buy.ValueBalance
                            ,BuyProduct.WeightAmount,BuyProduct.WeightAmount_Plate,BuyProduct.Drc,
                            BuyProduct.TotalPrice_Smoke,BuyProduct.WeightAmount_Raw,BuyProduct.TotalPrice_Raw,
                            BuyProduct.TotalPrice,BuyProduct.Remark
                            ,BuyProduct.CalRubber   
                            from Buy
                            inner join Customer on Customer.CustomerId = Buy.CustomerId
                            inner join BuyProduct on Buy.BuyId = BuyProduct.BuyId
                            where Buy.IsFinalze = 1 and Buy.Active = 1
                            AND YEAR(BuyDate) = YEAR(@StartDate) AND ProductTypeId =  @ProductTypeId";

            if (CustomerId != 0)
            {
                sSql += " AND Customer.CustomerId =  @CustomerId";
            }

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@StartDate", StartDate);
            param[1] = new SqlParameter("@ProductTypeId", ProductTypeId);
            param[2] = new SqlParameter("@CustomerId", CustomerId);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }        

        public DataSet Rpt_BuyWeightBalanceByYear(DateTime StartDate, int CustomerId)
        {
            string sSql = @"SELECT CustomerId,CustomerName,WeightBalanceAmt FROM (select Customer.CustomerId,Customer.CustomerName,SUM(WeightBalance.WeightBalanceAmt) AS WeightBalanceAmt  
                            FROM WeightBalance
                            INNER JOIN Customer ON WeightBalance.CustomerId = Customer.CustomerId AND Customer.Active != 0
                            INNER JOIN CustomerPrice ON CustomerPrice.PriceId = WeightBalance.PriceId AND CustomerPrice.IsDefault != 1 AND CustomerPrice.Active = 1
                            GROUP BY Customer.CustomerId,Customer.CustomerName)Tmp";

            if (CustomerId != 0)
            {
                sSql += " WHERE Tmp.CustomerId =  @CustomerId";
            }

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@StartDate", StartDate);
            param[1] = new SqlParameter("@CustomerId", CustomerId);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public DataSet Rpt_SaleByDate_1(DateTime StartDate, DateTime EndDate, int ProductTypeId, int CustomerId)
        {
            string sSql = @"select Sale.SaleNumber,CAST(Sale.SaleDate as date) as SaleDate, Sale.SaleDate as Saletime
                            ,SaleProduct.WeightAmount
                            ,SaleProduct.Drc
                            ,SaleProduct.WeightAmount_Raw
                            ,SaleProduct.TotalPrice_Raw
                            ,SaleProduct.TotalPrice
                            ,Customer.CustomerId
                            ,Customer.CustomerName

                            from Sale

                            inner join SaleProduct on SaleProduct.SaleId = Sale.SaleId
                            inner join Customer on Customer.CustomerId = Sale.CustomerId

                            where Sale.Active = 1 and Sale.IsFinalze = 1 
                            and Sale.SaleDate BETWEEN @StartDate AND @EndDate 
                            AND ProductTypeId = @ProductTypeId";

            if (CustomerId != 0)
            {
                sSql += " AND Customer.CustomerId =  @CustomerId";
            }

            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@StartDate", StartDate);
            param[1] = new SqlParameter("@EndDate", EndDate);
            param[2] = new SqlParameter("@ProductTypeId", ProductTypeId);
            param[3] = new SqlParameter("@CustomerId", CustomerId);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public DataSet Rpt_SaleByMonth_1(DateTime StartDate, int ProductTypeId, int CustomerId)
        {
            string sSql = @"select Sale.SaleNumber,CAST(Sale.SaleDate as date) as SaleDate, Sale.SaleDate as Saletime
                            ,SaleProduct.WeightAmount
                            ,SaleProduct.Drc
                            ,SaleProduct.WeightAmount_Raw
                            ,SaleProduct.TotalPrice_Raw
                            ,SaleProduct.TotalPrice
                            ,Customer.CustomerId
                            ,Customer.CustomerName

                            from Sale

                            inner join SaleProduct on SaleProduct.SaleId = Sale.SaleId
                            inner join Customer on Customer.CustomerId = Sale.CustomerId

                            where Sale.Active = 1 and Sale.IsFinalze = 1 
                            and YEAR(Sale.SaleDate) = YEAR(@StartDate) AND MONTH(Sale.SaleDate) = MONTH(@StartDate) 
                            AND ProductTypeId =  @ProductTypeId";

            if (CustomerId != 0)
            {
                sSql += " AND Customer.CustomerId =  @CustomerId";
            }

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@StartDate", StartDate);
            param[1] = new SqlParameter("@ProductTypeId", ProductTypeId);
            param[2] = new SqlParameter("@CustomerId", CustomerId);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public DataSet Rpt_SaleByYear_1(DateTime StartDate, int ProductTypeId, int CustomerId)
        {
            string sSql = @"select Sale.SaleNumber,CAST(Sale.SaleDate as date) as SaleDate, Sale.SaleDate as Saletime
                            ,SaleProduct.WeightAmount
                            ,SaleProduct.Drc
                            ,SaleProduct.WeightAmount_Raw
                            ,SaleProduct.TotalPrice_Raw
                            ,SaleProduct.TotalPrice
                            ,Customer.CustomerId
                            ,Customer.CustomerName

                            from Sale

                            inner join SaleProduct on SaleProduct.SaleId = Sale.SaleId
                            inner join Customer on Customer.CustomerId = Sale.CustomerId

                            where Sale.Active = 1 and Sale.IsFinalze = 1 
                            and YEAR(Sale.SaleDate) = YEAR(@StartDate) 
                            AND ProductTypeId =  @ProductTypeId";

            if (CustomerId != 0)
            {
                sSql += " AND Customer.CustomerId =  @CustomerId";
            }

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@StartDate", StartDate);
            param[1] = new SqlParameter("@ProductTypeId", ProductTypeId);
            param[2] = new SqlParameter("@CustomerId", CustomerId);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public DataSet Rpt_SaleByDate_2(DateTime StartDate, DateTime EndDate, int ProductTypeId, int CustomerId)
        {
            string sSql = @"select Sale.SaleId,Sale.SaleNumber,Sale.SaleDate,Customer.CustomerName,
                            Sale.SubTotal,Sale.DownValue,Sale.NetTotal,Sale.SetOffValue,Sale.ValueBalance
                            ,SaleProduct.Percentage,SaleProduct.WeightAmount,SaleProduct.WeightAmount_Plate,
                            SaleProduct.Drc,SaleProduct.TotalPrice_Smoke,SaleProduct.TotalPrice_Raw,
                            SaleProduct.CalRubber,SaleProduct.TotalPrice,SaleProduct.Remark
                            from Sale
                            inner join SaleProduct on Sale.SaleId = SaleProduct.SaleId
                            inner join Customer on Sale.CustomerId = Customer.CustomerId 
                            where Sale.Active = 1 and Sale.IsFinalze = 1 
                            and Sale.SaleDate BETWEEN @StartDate AND @EndDate 
                            AND ProductTypeId =  @ProductTypeId";

            if (CustomerId != 0)
            {
                sSql += " AND Customer.CustomerId =  @CustomerId";
            }

            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@StartDate", StartDate);
            param[1] = new SqlParameter("@EndDate", EndDate);
            param[2] = new SqlParameter("@ProductTypeId", ProductTypeId);
            param[3] = new SqlParameter("@CustomerId", CustomerId);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public DataSet Rpt_SaleByMonth_2(DateTime StartDate, int ProductTypeId, int CustomerId)
        {
            string sSql = @"select Sale.SaleNumber,Sale.SaleDate,Customer.CustomerName,Sale.SubTotal,
                            Sale.DownValue,Sale.NetTotal,Sale.SetOffValue,Sale.ValueBalance
                            ,SaleProduct.Percentage,SaleProduct.WeightAmount,SaleProduct.WeightAmount_Plate,
                            SaleProduct.Drc,SaleProduct.TotalPrice_Smoke,SaleProduct.TotalPrice_Raw,
                            SaleProduct.CalRubber,SaleProduct.TotalPrice,SaleProduct.Remark
                            from Sale
                            inner join SaleProduct on Sale.SaleId = SaleProduct.SaleId
                            inner join Customer on Sale.CustomerId = Customer.CustomerId 
                            where Sale.Active = 1 and Sale.IsFinalze = 1 
                            AND YEAR(Sale.SaleDate) = YEAR(@StartDate) AND MONTH(Sale.SaleDate) = MONTH(@StartDate) 
                            AND ProductTypeId =  @ProductTypeId";

            if (CustomerId != 0)
            {
                sSql += " AND Customer.CustomerId =  @CustomerId";
            }

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@StartDate", StartDate);
            param[1] = new SqlParameter("@ProductTypeId", ProductTypeId);
            param[2] = new SqlParameter("@CustomerId", CustomerId);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public DataSet Rpt_SaleByYear_2(DateTime StartDate, int ProductTypeId, int CustomerId)
        {
            string sSql = @"select Sale.SaleNumber,Sale.SaleDate,Customer.CustomerName,Sale.SubTotal,
                            Sale.DownValue,Sale.NetTotal,Sale.SetOffValue,Sale.ValueBalance
                            ,SaleProduct.Percentage,SaleProduct.WeightAmount,SaleProduct.WeightAmount_Plate,
                            SaleProduct.Drc,SaleProduct.TotalPrice_Smoke,SaleProduct.TotalPrice_Raw,
                            SaleProduct.CalRubber,SaleProduct.TotalPrice,SaleProduct.Remark
                            from Sale
                            inner join SaleProduct on Sale.SaleId = SaleProduct.SaleId
                            inner join Customer on Sale.CustomerId = Customer.CustomerId 
                            where Sale.Active = 1 and Sale.IsFinalze = 1 
                            AND YEAR(Sale.SaleDate) = YEAR(@StartDate) 
                            AND ProductTypeId =  @ProductTypeId";

            if (CustomerId != 0)
            {
                sSql += " AND Customer.CustomerId =  @CustomerId";
            }

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@StartDate", StartDate);
            param[1] = new SqlParameter("@ProductTypeId", ProductTypeId);
            param[2] = new SqlParameter("@CustomerId", CustomerId);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }


        public DataSet Rpt_SaleWeightBalanceBydate(DateTime StartDate, DateTime EndDate, int CustomerId)
        {
            string sSql = @"select Customer.CustomerId,Customer.CustomerCode, Customer.CustomerName
                            ,CustomerPrice.PriceId,TransactionLog.LogId
                            ,CAST(CustomerPrice.SaleDate AS date) as SaleDate
                            ,CustomerPrice.SalePriceAdvance
                            ,CustomerPrice.WeightAmount 
                            ,WeightBalance.WeightBalanceAmt as Remain
                            ,LogType.LogName
                            ,CAST(TransactionLog.InsertDate AS date) as BuyDate
                            ,TransactionLog.BeginAmt
                            ,CASE WHEN LogType.LogName = 'Sale' then 0 - TransactionLog.WeightAmount_Raw   else TransactionLog.WeightAmount_Raw end as WeightAmount_Raw
                            ,TransactionLog.WeightBalanceAmt
							,case when LogType.LogName = 'Cancel' then Users.UserName else '' end as DeleteBy

                            from TransactionLog 

                            inner join CustomerPrice on CustomerPrice.PriceId = TransactionLog.PriceId and CustomerPrice.CustomerId = TransactionLog.CustomerId and CustomerPrice.Active = 1
                            inner join Customer on TransactionLog.CustomerId = Customer.CustomerId and TransactionLog.LogTypeId <> 1
                            inner join WeightBalance on WeightBalance.CustomerId = CustomerPrice.CustomerId and WeightBalance.PriceId = CustomerPrice.PriceId
                            inner join LogType on TransactionLog.LogTypeId = LogType.LogTypeId
                            inner join CustomerTypes on CustomerTypes.CustomerTypeId = Customer.CustomerTypeId and CustomerTypes.CustomerTypeId = 2
							left join Sale on Sale.SaleId = TransactionLog.RefId 
							left join Users on Users.UserId = Sale.DeleteBy

                            WHERE CustomerPrice.SalePriceAdvance <> 0 AND TransactionLog.InsertDate BETWEEN @StartDate AND @EndDate";

            if (CustomerId != 0)
            {
                sSql += " AND Customer.CustomerId =  @CustomerId";
            }

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@StartDate", StartDate);
            param[1] = new SqlParameter("@EndDate", EndDate);
            param[2] = new SqlParameter("@CustomerId", CustomerId);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public DataSet Rpt_SaleWeightBalanceByMonth(DateTime StartDate, int CustomerId)
        {
            string sSql = @"select Customer.CustomerId,Customer.CustomerCode, Customer.CustomerName
                            ,CustomerPrice.PriceId,TransactionLog.LogId
                            ,CAST(CustomerPrice.SaleDate AS date) as SaleDate
                            ,CustomerPrice.SalePriceAdvance
                            ,CustomerPrice.WeightAmount 
                            ,WeightBalance.WeightBalanceAmt as Remain
                            ,LogType.LogName
                            ,CAST(TransactionLog.InsertDate AS date) as BuyDate
                            ,TransactionLog.BeginAmt
                            ,CASE WHEN LogType.LogName = 'Sale' then 0 - TransactionLog.WeightAmount_Raw   else TransactionLog.WeightAmount_Raw end as WeightAmount_Raw
                            ,TransactionLog.WeightBalanceAmt
							,case when LogType.LogName = 'Cancel' then Users.UserName else '' end as DeleteBy

                            from TransactionLog 

                            inner join CustomerPrice on CustomerPrice.PriceId = TransactionLog.PriceId and CustomerPrice.CustomerId = TransactionLog.CustomerId and CustomerPrice.Active = 1
                            inner join Customer on TransactionLog.CustomerId = Customer.CustomerId and TransactionLog.LogTypeId <> 1
                            inner join WeightBalance on WeightBalance.CustomerId = CustomerPrice.CustomerId and WeightBalance.PriceId = CustomerPrice.PriceId
                            inner join LogType on TransactionLog.LogTypeId = LogType.LogTypeId
                            inner join CustomerTypes on CustomerTypes.CustomerTypeId = Customer.CustomerTypeId and CustomerTypes.CustomerTypeId = 2
							left join Sale on Sale.SaleId = TransactionLog.RefId 
							left join Users on Users.UserId = Sale.DeleteBy

                            WHERE CustomerPrice.SalePriceAdvance <> 0 AND YEAR(TransactionLog.InsertDate) = YEAR(@StartDate) AND MONTH(TransactionLog.InsertDate) = MONTH(@StartDate) ";

            if (CustomerId != 0)
            {
                sSql += " AND Customer.CustomerId =  @CustomerId";
            }

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@StartDate", StartDate);
            param[1] = new SqlParameter("@CustomerId", CustomerId);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public DataSet Rpt_SaleWeightBalanceByYear(DateTime StartDate, int CustomerId)
        {
            string sSql = @"select Customer.CustomerId,Customer.CustomerCode, Customer.CustomerName
                            ,CustomerPrice.PriceId,TransactionLog.LogId
                            ,CAST(CustomerPrice.SaleDate AS date) as SaleDate
                            ,CustomerPrice.SalePriceAdvance
                            ,CustomerPrice.WeightAmount 
                            ,WeightBalance.WeightBalanceAmt as Remain
                            ,LogType.LogName
                            ,CAST(TransactionLog.InsertDate AS date) as BuyDate
                            ,TransactionLog.BeginAmt
                            ,CASE WHEN LogType.LogName = 'Sale' then 0 - TransactionLog.WeightAmount_Raw   else TransactionLog.WeightAmount_Raw end as WeightAmount_Raw
                            ,TransactionLog.WeightBalanceAmt
							,case when LogType.LogName = 'Cancel' then Users.UserName else '' end as DeleteBy

                            from TransactionLog 

                            inner join CustomerPrice on CustomerPrice.PriceId = TransactionLog.PriceId and CustomerPrice.CustomerId = TransactionLog.CustomerId and CustomerPrice.Active = 1
                            inner join Customer on TransactionLog.CustomerId = Customer.CustomerId and TransactionLog.LogTypeId <> 1
                            inner join WeightBalance on WeightBalance.CustomerId = CustomerPrice.CustomerId and WeightBalance.PriceId = CustomerPrice.PriceId
                            inner join LogType on TransactionLog.LogTypeId = LogType.LogTypeId
                            inner join CustomerTypes on CustomerTypes.CustomerTypeId = Customer.CustomerTypeId and CustomerTypes.CustomerTypeId = 2
							left join Sale on Sale.SaleId = TransactionLog.RefId 
							left join Users on Users.UserId = Sale.DeleteBy

                            WHERE CustomerPrice.SalePriceAdvance <> 0 AND YEAR(TransactionLog.InsertDate) = YEAR(@StartDate)";

            if (CustomerId != 0)
            {
                sSql += " AND Customer.CustomerId =  @CustomerId";
            }

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@StartDate", StartDate);
            param[1] = new SqlParameter("@CustomerId", CustomerId);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

    }
}
