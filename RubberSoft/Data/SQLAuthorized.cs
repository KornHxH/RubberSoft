using DevExpress.XtraBars.Docking2010.Customization;
using DevExpress.XtraBars.Docking2010.Views.WindowsUI;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RubberSoft.Data
{
    class SQLAuthorized
    {
        readonly SQLData SQLData = new SQLData();

        public DataSet Spt_CheckUserRole(int RoleId)
        {
            string sSql = @"SELECT RoleId, RoleName, RoleFullName, Active FROM UserRole
                            WHERE RoleId = @RoleId";

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@RoleId", RoleId);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public DataSet Spt_GetMaxUserRole()
        {
            string sSql = @"SELECT MAX(ISNULL(RoleId, 0)) RoleId FROM UserRole";

            SqlParameter[] param = new SqlParameter[3];
            //param[0] = new SqlParameter("@RoleId", RoleId);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public DataSet Spt_GetUserRole(string text)
        {
            string sSql = @"SELECT RoleId, RoleName, RoleFullName, Active FROM UserRole
                            WHERE Active = 1 ";
            if (text != "")
            {
                sSql += "AND (RoleName LIKE '%' + @text + '%' OR RoleFullName LIKE '%' + @text + '%' ) ";
            }
               
            sSql += "ORDER BY RoleId";


            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@text", text);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public DataSet Spt_AddUserRole(string RoleName, string RoleFullName)
        {
            string sSql = @"INSERT INTO UserRole (RoleName, RoleFullName, Active, CreateDate, CreateUser) 
                            VALUES (@RoleName, @RoleFullName, 1, GETDATE(), @CreateUser)";

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@RoleName", RoleName);
            param[1] = new SqlParameter("@RoleFullName", RoleFullName);
            param[2] = new SqlParameter("@CreateUser", ClassProperty.permisUserID);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public bool AddUserRole(string RoleName, string RoleFullName)
        {
            try
            {
                DataSet ds = Spt_AddUserRole(RoleName, RoleFullName);

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        public DataSet Spt_UpdateUserRole(int RoleId, string RoleName, string RoleFullName)
        {
            string sSql = @"UPDATE UserRole SET RoleName=@RoleName, RoleFullName=@RoleFullName, UpdateDate=GETDATE(),
                            UpdateUser=@UpdateUser
                            WHERE RoleId=@RoleId";

            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@RoleId", RoleId);
            param[1] = new SqlParameter("@RoleName", RoleName);
            param[2] = new SqlParameter("@RoleFullName", RoleFullName);
            param[3] = new SqlParameter("@UpdateUser", ClassProperty.permisUserID);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public bool UpdateUserRole(int RoleId, string RoleName, string RoleFullName)
        {
            try
            {
                DataSet ds = Spt_UpdateUserRole(RoleId, RoleName, RoleFullName);

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        public DataSet Spt_GetAuthorized(int RoleId)
        {
            string sSql = @"SELECT AuthorizeId, AuthorizeGroupId, AuthorizeGroupName, AuthorizeCode, AuthorizedGroupName, 
                            AuthorizeName, Active, Selected 
                            FROM (
                            SELECT A.AuthorizeId, A.AuthorizeGroupId, AuthorizeGroupName, AuthorizeCode, AuthorizeName, 
                            (CONVERT(VARCHAR, A.AuthorizeGroupId) + ' : ' + AuthorizeGroupName) AuthorizedGroupName, Active, 
                            CASE WHEN C.AuthorizeId IS NULL THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS Selected 
                            FROM Authorized A
                            INNER JOIN AuthorizedGroup B ON A.AuthorizeGroupId = B.AuthorizeGroupId
                            LEFT OUTER JOIN (
                            SELECT AuthorizeId, RoleId FROM AuthorizedUser WHERE RoleId = @RoleId
                            ) C ON A.AuthorizeId = C.AuthorizeId
                            WHERE Active = 1 
                            ) Authorized
                            ORDER BY AuthorizeId";

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@RoleId", RoleId);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public DataSet Spt_CheckAuthorizedUser(int RoleId, int AuthorizeId)
        {
            string sSql = @"SELECT AuthorizeId, RoleId FROM AuthorizedUser 
                            WHERE RoleId = @RoleId AND AuthorizeId = @AuthorizeId";

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@RoleId", RoleId);
            param[1] = new SqlParameter("@AuthorizeId", AuthorizeId);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public bool CheckAuthorized(int RoleId, int AuthorizeId)
        {
            try
            {
                if (RoleId == -1)
                {
                    return true;
                }
                else
                {
                    DataTable dt = new DataTable();
                    DataSet ds = Spt_CheckAuthorizedUser(RoleId, AuthorizeId);
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
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

        public DataSet Spt_AddAuthorizedUser(int RoleId, int AuthorizeId)
        {
            string sSql = @"INSERT INTO AuthorizedUser (RoleId, AuthorizeId)   
                            VALUES (@RoleId, @AuthorizeId)";

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@RoleId", RoleId);
            param[1] = new SqlParameter("@AuthorizeId", AuthorizeId);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public bool AddAuthorizedUser(int RoleId, int AuthorizeId)
        {
            try
            {
                DataSet ds = Spt_AddAuthorizedUser(RoleId, AuthorizeId);

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        public DataSet Spt_RemoveAuthorizedUser(int RoleId, int AuthorizeId)
        {
            string sSql = @"DELETE AuthorizedUser WHERE RoleId = @RoleId AND AuthorizeId = @AuthorizeId";

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@RoleId", RoleId);
            param[1] = new SqlParameter("@AuthorizeId", AuthorizeId);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public bool RomoveAuthorizedUser(int RoleId, int AuthorizeId)
        {
            try
            {
                DataSet ds = Spt_RemoveAuthorizedUser(RoleId, AuthorizeId);

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        public void AlertAuthorized(string caption, string alert)
        {
            try
            {
                FlyoutAction action = new FlyoutAction()
                {
                    Caption = caption,
                    Description = alert
                };

                Predicate<DialogResult> predicate = SQLData.CloseFunc;

                FlyoutCommand command1 = new FlyoutCommand()
                {
                    Text = "OK",
                    Result = DialogResult.OK
                };

                action.Commands.Add(command1);

                FlyoutProperties properties = new FlyoutProperties
                {
                    ButtonSize = new Size(100, 40)
                };

                properties.AppearanceButtons.BackColor = Color.White;
                properties.AppearanceButtons.ForeColor = Color.Blue;
                properties.Appearance.BackColor = Color.DarkSeaGreen;
                properties.Appearance.ForeColor = Color.White;
                properties.Appearance.BorderColor = Color.Gray;
                properties.Style = FlyoutStyle.Popup;

                if (FlyoutDialog.Show(null, action, properties, predicate) == DialogResult.OK)
                    return;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

    }
}
