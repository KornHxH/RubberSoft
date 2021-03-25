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
    class SQLCustomer
    {
        readonly SQLData SQLData = new SQLData();

        public DataSet Spt_GenCustomerCode()
        {
            string sSql = @"SELECT SUBSTRING(CONVERT(varchar, CustomerId + 100000), 0, 8) CustomerCode FROM (
                            SELECT MAX(ISNULL(CustomerId +1, 0)) CustomerId FROM Customer) Customer";

            SqlParameter[] param = new SqlParameter[3];
            //param[0] = new SqlParameter("@UnitId", id);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public DataSet GetLkCustomerTypes()
        {
            string sSql = @"SELECT CAST(0 AS int) AS CustomerTypeId, '--- ระบุประเภทลูกค้า ---' AS CustomerTypeName 
                            UNION ALL
                            SELECT CustomerTypeId, CustomerTypeName FROM CustomerTypes "; 
            sSql +=        "ORDER BY CustomerTypeId";

            SqlParameter[] param = new SqlParameter[3];
            //param[0] = new SqlParameter("@UnitId", id);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public DataSet Spt_GetCustomers(string tex, int typeid, int customerGroupId)
        {
            string sSql = @"SELECT CustomerId, A.CustomerTypeId, CustomerGroupId, CustomerTypeName, 
                            CustomerCode, CustomerName, CustomerAddress, LicensePlate, Phone
                            FROM Customer A
                            LEFT OUTER JOIN CustomerTypes B ON A.CustomerTypeId = B.CustomerTypeId
                            WHERE A.Active = 1 AND A.IsDefault = 0 ";

            if (typeid != 0)
            {
                sSql += @"AND (A.CustomerTypeId = @typeid) ";
            }

            if (customerGroupId != 0)
            {
                sSql += @"AND (CustomerGroupId = @CustomerGroupId) ";
            }

            if (!string.IsNullOrEmpty(tex))
            {
                sSql += @"AND (CustomerCode LIKE '%' + @tex + '%' OR CustomerName LIKE '%' + @tex + '%' 
                              OR LicensePlate LIKE '%' + @tex + '%')";
            }

            sSql += @"ORDER BY A.CustomerTypeId, CustomerId";

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@tex", tex);
            param[1] = new SqlParameter("@typeid", typeid);
            param[2] = new SqlParameter("@CustomerGroupId", customerGroupId);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public DataSet GetCustomer(string tex, int typeid)
        {
            string sSql = @"SELECT CustomerId, A.CustomerTypeId, CustomerGroupId, CustomerTypeName, 
                            CustomerCode, CustomerName, CustomerAddress, LicensePlate, Phone
                            FROM Customer A
                            LEFT OUTER JOIN CustomerTypes B ON A.CustomerTypeId = B.CustomerTypeId
                            WHERE A.Active = 1 ";

            if (typeid != 0)
            {
                sSql += @"AND (A.CustomerTypeId = @typeid) ";
            }

            if (!string.IsNullOrEmpty(tex))
            {
                sSql += @"AND (CustomerCode LIKE '%' + @tex + '%' OR CustomerName LIKE '%' + @tex + '%' 
                              OR LicensePlate LIKE '%' + @tex + '%')";
            }

            sSql += @"ORDER BY A.CustomerTypeId, CustomerId";

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@tex", tex);
            param[1] = new SqlParameter("@typeid", typeid);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public DataSet Spt_GetMaxCustomerPriceID()
        {
            string sSql = @"SELECT MAX(ISNULL(PriceId, 0)) PriceId FROM CustomerPrice";

            SqlParameter[] param = new SqlParameter[3];
            //param[0] = new SqlParameter("@CustomerId", cud_id);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public int GetMaxCustomerPriceID()
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = Spt_GetMaxCustomerPriceID();
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow drv in dt.Rows)
                    {
                        ClassProperty.MaxCustomerPriceId = Convert.ToInt32(drv["PriceId"]);
                    }
                }

                return ClassProperty.MaxCustomerPriceId;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return 0;
            }
        }

        public DataSet Spt_GetDefaultCustomerPrice(int id)
        {
            string sSql = @"SELECT PriceId, PriceName, CustomerId, IsDefault FROM CustomerPrice WHERE CustomerId=@CustomerId AND IsDefault=1";

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@CustomerId", id);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }


        public DataSet Spt_GetCustomerPrices(int id)
        {
            string sSql = @"SELECT PriceId, CustomerId, CustomerName, sName, PriceName, SaleDate, SalePriceAdvance, WeightAmount, BeginAmt, DeliveryPrice,  
                            BeginBalanceAmt, WeightBalanceAmt, LastLogId, Active, ItemPriceId, UnitPrice, IsDefault
                            FROM (

                            SELECT A.PriceId, A.CustomerId, CustomerName, A.PriceId AS sName, SaleDate, SalePriceAdvance, ISNULL(ItemPriceId, 0) ItemPriceId, 
                            ISNULL(UnitPrice, 0) UnitPrice, WeightAmount, DeliveryPrice, A.Active, ISNULL(A.IsDefault, 0) IsDefault,
							ISNULL(WeightBalanceAmt, 0) AS BeginAmt, ISNULL(BeginBalanceAmt, 0) AS BeginBalanceAmt, 
                            ISNULL(WeightBalanceAmt, 0) AS WeightBalanceAmt, ISNULL(LastLogId, 0) AS LastLogId, 
                            CASE 
                            WHEN PriceName IS NULL THEN CONVERT(VARCHAR, FORMAT(SaleDate, 'dd-MMMM-yyyy', 'Th-th'))
                            ELSE PriceName END AS PriceName
                            FROM CustomerPrice A
                            INNER JOIN Customer B ON A.CustomerId = B.CustomerId
							LEFT OUTER JOIN (

							SELECT DD.WeightBalanceId, DD.PriceId, BeginBalanceAmt, WeightBalanceAmt, LastLogId 
							FROM (

							SELECT MAX(WeightBalanceId) AS WeightBalanceId, PriceId FROM WeightBalance 
							WHERE CustomerId=@CustomerId
							GROUP BY WeightBalanceId, PriceId) DT
							INNER JOIN WeightBalance DD ON Dt.WeightBalanceId = DD.WeightBalanceId

							) C ON A.PriceId = C.PriceId

							LEFT OUTER JOIN (		

							SELECT ItemPriceId, PriceId, UnitPrice 
							FROM ItemPrice WHERE Active = 1 AND IsDefault = 1

							) D ON A.PriceId = D.PriceId
                            WHERE A.Active = 1 AND A.CustomerId=@CustomerId
                            AND (WeightBalanceAmt > 0 OR A.IsDefault = 1)

                            ) WeightBalance
                            ORDER BY PriceId";

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@CustomerId", id);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public DataSet Spt_GetCustomerGroup(int CustomerGroupId)
        {
            string sSql = @"SELECT CustomerGroupId, CustomerGroupName 
                            FROM (
                            SELECT 0 CustomerGroupId, '-- ระบุกลุ่มลูกค้า --' CustomerGroupName
                            UNION ALL
                            SELECT CustomerGroupId, CustomerGroupName FROM CustomerGroup ) CustomerGroup ";

            if (CustomerGroupId != 0)
            {
                sSql += @"WHERE CustomerGroupId = @CustomerGroupId";
            }

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@CustomerGroupId", CustomerGroupId);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public DataSet GetLkProducts(int ProductTypeId)
        {
            string sSql = @"SELECT ProductId, ProductTypeId, ProductName
                            FROM (
                            SELECT 0 ProductId, 0 ProductTypeId, '-- ระบุกลุ่มลูกค้า --' ProductName
                            UNION ALL
                            SELECT ProductId, ProductTypeId, ProductName FROM Products 
                            WHERE Active = 1 AND ProductTypeId = @ProductTypeId) Products";

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@ProductTypeId", ProductTypeId);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public DataSet Spt_GetLkItemPrice(int PriceId)
        {
            string sSql = @"SELECT ItemPriceId, FORMAT(UnitPrice, 'N2') UnitPrice
                            FROM ItemPrice
                            WHERE PriceId = @PriceId AND Active = 1
                            ORDER BY IsDefault Desc";

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@PriceId", PriceId);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public DataSet Spt_GetItemPrice(int PriceId)
        {
            string sSql = @"SELECT ItemPriceId, PriceId, UnitPrice, FORMAT(UnitPrice, 'N2') sUnitPrice, 
                            IsDefault, Active, CAST(0 AS BIT) IsNew 
                            FROM ItemPrice
                            WHERE PriceId = @PriceId AND Active = 1";

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@PriceId", PriceId);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public DataSet Spt_AddItemPrice(int PriceId, decimal UnitPrice, bool IsDefault)
        {
            string sSql = @"INSERT INTO ItemPrice (PriceId, UnitPrice, IsDefault, Active)
                            VALUES (@PriceId, @UnitPrice, @IsDefault, 1)";

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@PriceId", PriceId);
            param[1] = new SqlParameter("@UnitPrice", UnitPrice);
            param[2] = new SqlParameter("@IsDefault", IsDefault);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public DataSet Spt_UpdateItemPrice(int ItemPriceId, decimal UnitPrice, bool IsDefault, bool Active)
        {
            string sSql = @"UPDATE ItemPrice SET UnitPrice=@UnitPrice, IsDefault=@IsDefault, Active=@Active
                            WHERE ItemPriceId=@ItemPriceId";

            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@ItemPriceId", ItemPriceId);
            param[1] = new SqlParameter("@UnitPrice", UnitPrice);
            param[2] = new SqlParameter("@IsDefault", IsDefault);
            param[3] = new SqlParameter("@Active", Active);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public DataSet Spt_CheckCustomerPrice(int CustomerId)
        {
            string sSql = @"SELECT PriceId, CustomerId, SaleDate, SalePriceAdvance, FORMAT(WeightAmount, 'N2') WeightAmount, DeliveryPrice, 
                            Active, InsertDate, UpdateDate
                            FROM CustomerPrice
                            WHERE Active = 1 AND IsDefault = 1 AND CustomerId=@CustomerId";

            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@CustomerId", CustomerId);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public DataSet Spt_GetUsePrice(int PriceId)
        {
            string sSql = @"SELECT PriceId, CustomerId, CustomerName, sName, PriceName, SaleDate, SalePriceAdvance, WeightAmount, BeginAmt, DeliveryPrice,  
                            BeginBalanceAmt, WeightBalanceAmt, LastLogId, Active, ItemPriceId, UnitPrice, IsDefault
                            FROM (

                            SELECT A.PriceId, A.CustomerId, CustomerName, A.PriceId AS sName, SaleDate, SalePriceAdvance, ISNULL(ItemPriceId, 0) ItemPriceId, 
                            ISNULL(UnitPrice, 0) UnitPrice, WeightAmount, DeliveryPrice, A.Active, ISNULL(A.IsDefault, 0) IsDefault,
							ISNULL(WeightBalanceAmt, 0) AS BeginAmt, ISNULL(BeginBalanceAmt, 0) AS BeginBalanceAmt, 
                            ISNULL(WeightBalanceAmt, 0) AS WeightBalanceAmt, ISNULL(LastLogId, 0) AS LastLogId, 
                            CASE 
                            WHEN PriceName IS NULL THEN CONVERT(VARCHAR, FORMAT(SaleDate, 'dd-MMMM-yyyy', 'Th-th'))
                            ELSE PriceName END AS PriceName
                            FROM CustomerPrice A
                            INNER JOIN Customer B ON A.CustomerId = B.CustomerId
							LEFT OUTER JOIN (

							SELECT DD.WeightBalanceId, DD.PriceId, BeginBalanceAmt, WeightBalanceAmt, LastLogId 
							FROM (

							SELECT MAX(WeightBalanceId) AS WeightBalanceId, PriceId FROM WeightBalance 
							--WHERE CustomerId=@CustomerId
							GROUP BY WeightBalanceId, PriceId) DT
							INNER JOIN WeightBalance DD ON Dt.WeightBalanceId = DD.WeightBalanceId

							) C ON A.PriceId = C.PriceId

							LEFT OUTER JOIN (		

							SELECT ItemPriceId, PriceId, UnitPrice 
							FROM ItemPrice WHERE Active = 1 AND IsDefault = 1

							) D ON A.PriceId = D.PriceId
                            WHERE A.Active = 1 --AND A.CustomerId=@CustomerId
                            AND (WeightBalanceAmt > 0 OR A.IsDefault = 1)

                            ) WeightBalance
							WHERE PriceId = @PriceId
                            ORDER BY CustomerId";

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@PriceId", PriceId);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public DataSet Spt_GetDefaultCustomer()
        {
            string sSql = @"SELECT CustomerId, CustomerTypeId, CustomerCode, CustomerName FROM Customer WHERE IsDefault = 1";

            SqlParameter[] param = new SqlParameter[5];
            //param[0] = new SqlParameter("@CustomerId", CustomerId);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public DataSet Spt_GetDefaultItemPrice(int PriceId)
        {
            string sSql = @"SELECT ItemPriceId, PriceId, UnitPrice, IsDefault FROM ItemPrice 
                            WHERE IsDefault=1 AND Active=1 AND PriceId=@PriceId";

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@PriceId", PriceId);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public DataSet Spt_GetUseItemPrice(int PriceId)
        {
            string sSql = @"SELECT ItemPriceId, PriceId, UnitPrice, IsDefault FROM ItemPrice 
                            WHERE IsDefault=1 AND Active=1 AND PriceId=@PriceId";

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@PriceId", PriceId);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public int GetDefaultItemPrice(int id)
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = Spt_GetDefaultItemPrice(id);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow drv in dt.Rows)
                    {
                        ClassProperty.DefaultItemPrice = Convert.ToInt32(drv["ItemPriceId"]);
                    }
                }

                return ClassProperty.DefaultItemPrice;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return 0;
            }
        }

        public int GetDefaultCustomer()
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = Spt_GetDefaultCustomer();
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow drv in dt.Rows)
                    {
                        ClassProperty.DefaultCustomer = Convert.ToInt32(drv["CustomerId"]);
                    }
                }

                return ClassProperty.DefaultCustomer;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return 0;
            }
        }

        public int GetDefaultCustomerPrice(int id)
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = Spt_GetDefaultCustomerPrice(id);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow drv in dt.Rows)
                    {
                        ClassProperty.DefaultCustomerPrice = Convert.ToInt32(drv["PriceId"]);
                    }
                }

                return ClassProperty.DefaultCustomerPrice;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return 0;
            }
        }

        public decimal GetUsePrice(int id)
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = Spt_GetUsePrice(id);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow drv in dt.Rows)
                    {
                        ClassProperty.UsePrice = Convert.ToInt32(drv["BeginAmt"]);
                    }
                }

                return ClassProperty.UsePrice;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return 0;
            }
        }

        public bool AddCustomerPrice(int CustomerId, string PriceName, DateTime sSaleDate, decimal sSalePriceAdvance,
                                       double sWeightAmount, decimal sDeliveryPrice, bool IsDefault)
        {
            try
            {
                using (var context = new RubberSoftEntities())
                {
                    var query = context.spt_AddCustomerPrice(PriceName, CustomerId, sSaleDate, sSalePriceAdvance,
                                        sWeightAmount, sDeliveryPrice, IsDefault);

                    return true;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        public bool UpdateCustomerPrice(int CustomerId, int sPriceId, string PriceName, DateTime sSaleDate, decimal sSalePriceAdvance,
                                       double sWeightAmount, decimal sDeliveryPrice, bool IsDefault)
        {
            try
            {
                using (var context = new RubberSoftEntities())
                {
                    var query = context.spt_UpdateCustomerPrice(sPriceId, PriceName, CustomerId, sSaleDate, sSalePriceAdvance,
                                        sWeightAmount, sDeliveryPrice, IsDefault);

                    return true;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

    }
}
