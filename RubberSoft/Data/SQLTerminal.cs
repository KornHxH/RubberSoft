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
    class SQLTerminal
    {
        readonly SQLData SQLData = new SQLData();

        public DataSet Spt_GetTerminal()
        {
            string sSql = @"SELECT TerminalId, TerminalName, IPMachine, MachineName, CreateDate, Active 
                            FROM Terminal";

            SqlParameter[] param = new SqlParameter[3];

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public DataSet Spt_UpdateTerminal(int TerminalId, bool Active)
        {
            string sSql = @"UPDATE Terminal SET Active = @Active WHERE TerminalId = @TerminalId";

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@TerminalId", TerminalId);
            param[1] = new SqlParameter("@Active", Active);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public bool UpdateTerminal(int TerminalId, bool Active)
        {
            try
            {
                DataSet ds = Spt_UpdateTerminal(TerminalId, Active);

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        public bool CheckTerminal(string name)
        {
            try
            {
                using (var context = new RubberSoftEntities())
                {
                    var query = context.spt_GetTerminal().Where(o => o.MachineName == name).ToList();
                    if (query.Count > 0)
                    {
                        foreach (spt_GetTerminal_Result dt in query)
                        {
                            ClassProperty.StrTerminalId = dt.TerminalId;
                        }

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        public bool GetTerminal(int id)
        {
            try
            {
                using (var context = new RubberSoftEntities())
                {
                    var query = context.spt_GetTerminal().Where(o => o.TerminalId == id).ToList();
                    if (query.Count > 0)
                    {
                        foreach (spt_GetTerminal_Result dt in query)
                        {
                            ClassProperty.StrTerminalId = dt.TerminalId;
                            ClassProperty.MachineName = dt.TerminalName;
                            ClassProperty.EnableTerminal = dt.Active.Value;
                        }
                    }
                    else
                    {
                        ClassProperty.StrTerminalId = 0;
                        ClassProperty.MachineName = "";
                        ClassProperty.EnableTerminal = false;
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

        public bool AddTerminal()
        {
            try
            {
                using (var context = new RubberSoftEntities())
                {
                    context.spt_AddTerminal(ClassProperty.GetCurrentName, ClassProperty.IPMachine, ClassProperty.MachineName, false);
                }

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        public DataSet Spt_GetPrinter(int TerminalId, string OptionName)
        {
            string sSql = @"SELECT OptionID, TerminalId, TerminalName, OptionName, OptionValue, DateValue, TimeValue, Active, ISNULL(IsTrue, 1) IsTrue
                            FROM Options WHERE TerminalId=@TerminalId AND OptionName=@OptionName";

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@TerminalId", TerminalId);
            param[1] = new SqlParameter("@OptionName", OptionName);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public DataSet Spt_AddPrinter(int TerminalId, string TerminalName, string OptionName, string OptionValue, bool IsTrue, bool Active)
        {
            string sSql = @"INSERT INTO Options (TerminalId, TerminalName, OptionName, OptionValue, DateValue, TimeValue, IsTrue, Active) 
                            VALUES (@TerminalId, @TerminalName, @OptionName, @OptionValue, NULL, NULL, @IsTrue, @Active)";

            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@TerminalId", TerminalId);
            param[1] = new SqlParameter("@TerminalName", TerminalName);
            param[2] = new SqlParameter("@OptionName", OptionName);
            param[3] = new SqlParameter("@OptionValue", OptionValue);
            param[4] = new SqlParameter("@IsTrue", IsTrue);
            param[5] = new SqlParameter("@Active", Active);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public bool AddPrinter(string OptionValue, bool IsTrue, bool Active)
        {
            try
            {
                Spt_AddPrinter(ClassProperty.StrTerminalId, ClassProperty.MachineName, "SetPrinter", OptionValue, IsTrue, Active);
             
                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        public DataSet Spt_UpdatePrinter(int OptionID, string OptionValue, bool IsTrue, bool Active)
        {
            string sSql = @"UPDATE Options SET OptionValue = @OptionValue, IsTrue=@IsTrue, Active=@Active WHERE OptionID = @OptionID";

            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@OptionID", OptionID);
            param[1] = new SqlParameter("@OptionValue", OptionValue);
            param[2] = new SqlParameter("@IsTrue", IsTrue);
            param[3] = new SqlParameter("@Active", Active);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public bool UpdatePrinter(int OptionID, string OptionValue, bool IsTrue, bool Active)
        {
            try
            {
                Spt_UpdatePrinter(OptionID, OptionValue, IsTrue, Active);

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
