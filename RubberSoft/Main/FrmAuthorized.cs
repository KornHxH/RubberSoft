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

namespace RubberSoft.Main
{
    public partial class FrmAuthorized : XtraForm
    {
        public FrmAuthorized()
        {
            InitializeComponent();
        }

        readonly SQLData SQLData = new SQLData();
        readonly SQLAuthorized SQLAuthorized = new SQLAuthorized();
        DataTable dtUserRole = new DataTable();
        DataTable dtAuthorized = new DataTable();

        private int RoleId, MaxRoleId;
        private string strRoleName, stRoleName, strRoleFullName, stRoleFullName;

        private void FrmAuthorized_Load(object sender, EventArgs e)
        {
            GetRoles();
            ClearValues();
        }

        private bool GetRoles()
        {
            try
            {
                DataSet ds = SQLAuthorized.Spt_GetUserRole(TxtSearch.Text.Trim());
                dtUserRole = ds.Tables[0];
                GridRoles.DataSource = dtUserRole;

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private int GetMaxUserRole()
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = SQLAuthorized.Spt_GetMaxUserRole();
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow drv in dt.Rows)
                    {
                        MaxRoleId = Convert.ToInt32(drv["RoleId"]);
                    }
                }

                return MaxRoleId;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return 0;
            }
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            FrmMain frm = new FrmMain();
            {
                this.Close();
                frm.Show();
            }
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            GetRoles();
        }

        private void BtnNew_Click(object sender, EventArgs e)
        {
            ClearValues();
        }

        private void ClearValues()
        {
            try
            {
                RoleId = 0;
                TxtRoleName.Text = "";
                TxtRoleFullName.Text = "";
                TxtRoleName.Select();

                strRoleName = "";
                stRoleName = "";
                strRoleFullName = "";
                stRoleFullName = "";

                GetAuthorized(RoleId);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void GridViewRole_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (!(sender is GridView view)) return;
            RoleId = Convert.ToInt32(view.GetFocusedRowCellValue("RoleId"));

            if (CheckData(RoleId) == true)
            {
                GetData(RoleId);
            }
        }

        private bool GetData(int sRoleId)
        {
            try
            {
                using (var context = new RubberSoftEntities())
                {
                    var query = context.spt_GetRoles().Where(o => o.RoleId == sRoleId).ToList();
                    if (query.Count > 0)
                    {
                        foreach (spt_GetRoles_Result dt in query)
                        {
                            strRoleName = dt.RoleName;
                            TxtRoleName.Text = dt.RoleName;
                            strRoleFullName = dt.RoleFullName;
                            TxtRoleFullName.Text = dt.RoleFullName;
                        }

                        GetAuthorized(sRoleId);
                    }
                    else
                    {
                        ClearValues();
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

        private bool GetAuthorized(int RoleId)
        {
            try
            {

                DataSet ds = SQLAuthorized.Spt_GetAuthorized(RoleId);
                dtAuthorized = ds.Tables[0];
                GridAuthorized.DataSource = dtAuthorized;

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        public bool CheckData(int RoleId)
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = SQLAuthorized.Spt_CheckUserRole(RoleId);
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
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            // 33	9 : สิทธิ์ใช้งานหน้าตั้งค่าสิทธิ์การใช้งาน	 บันทึกข้อมูลสิทธิ์การใช้งาน
            int AuthorizeId = 33;

            if (SQLAuthorized.CheckAuthorized(ClassProperty.permisRoleId, AuthorizeId) == false)
            {
                SQLAuthorized.AlertAuthorized("สิทธิ์ใช้งานหน้าตั้งค่าสิทธิ์การใช้งาน", "ไม่มีสิทธิ์บันทึกข้อมูลสิทธิ์การใช้งาน");
                return;
            }

            if (SaveData() == true)
            {
                GetRoles();
                GridViewRole.FocusedRowHandle = GridViewRole.LocateByValue("RoleName", TxtRoleName.Text);
                RoleId = Convert.ToInt32(GridViewRole.GetFocusedRowCellValue("RoleId"));
                GetData(RoleId);
            }
        }

        private bool SaveData()
        {
            try
            {
                if (CheckValues() == true)
                {
                    if (CheckData(RoleId) == true)
                    {
                        if (stRoleName != strRoleName)
                        {
                            if (CheckDuplicate(1) == false)
                            {
                                XtraMessageBox.Show("ชื่อสิทธิ์การใช้งานนี้ถูกใช้งานแล้ว !", "เตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                TxtRoleName.Focus();
                                return false;
                            }
                        }

                        if (stRoleFullName != strRoleFullName)
                        {
                            if (CheckDuplicate(2) == false)
                            {
                                XtraMessageBox.Show("ชื่อเต็มสิทธิ์การใช้งานนี้ถูกใช้งานแล้ว !", "เตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                TxtRoleFullName.Focus();
                                return false;
                            }
                        }

                        if (SQLAuthorized.UpdateUserRole(RoleId, stRoleName, stRoleFullName))
                        {
                            if (SaveAuthorized())
                            {
                                XtraMessageBox.Show("บันทึกข้อมูลสำเร็จ", "สถานะ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                    else
                    {
                        if (CheckDuplicate(1) == false)
                        {
                            XtraMessageBox.Show("ชื่อสิทธิ์การใช้งานนี้ถูกใช้งานแล้ว !", "เตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            TxtRoleName.Focus();
                            return false;
                        }

                        if (CheckDuplicate(2) == false)
                        {
                            XtraMessageBox.Show("ชื่อเต็มสิทธิ์การใช้งานนี้ถูกใช้งานแล้ว !", "เตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            TxtRoleFullName.Focus();
                            return false;
                        }

                        if (SQLAuthorized.AddUserRole(stRoleName, stRoleFullName))
                        {
                            RoleId = GetMaxUserRole();
                            if (SaveAuthorized())
                            {
                                XtraMessageBox.Show("เพิ่มข้อมูลสำเร็จ", "สถานะ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
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

        private bool CheckValues()
        {
            try
            {
                if (TxtRoleName.Text.Trim() == string.Empty)
                {
                    XtraMessageBox.Show("กรุณาระบุชื่อสิทธิ์การใช้งาน !", "เตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TxtRoleName.Focus();
                    return false;
                }

                if (TxtRoleFullName.Text.Trim() == string.Empty)
                {
                    XtraMessageBox.Show("กรุณาระบุชื่อเต็มสิทธิ์การใช้งาน !", "เตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TxtRoleFullName.Focus();
                    return false;
                }

                stRoleName = TxtRoleName.Text;
                stRoleFullName = TxtRoleFullName.Text;

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool CheckDuplicate(int Type)
        {
            try
            {
                using (var context = new RubberSoftEntities())
                {
                    if (Type == 1)
                    {
                        var ckRoleName = context.spt_GetRoles().Where(o => o.RoleName == stRoleName && 
                        o.Active == 1).ToList();

                        if (ckRoleName.Count > 0)
                        {
                            return false;
                        }
                    }

                    if (Type == 2)
                    {
                        var ckRoleFullName = context.spt_GetRoles().Where(o => o.RoleFullName == stRoleFullName &&
                        o.Active == 1).ToList();

                        if (ckRoleFullName.Count > 0)
                        {
                            return false;
                        }
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

        private bool SaveAuthorized()
        {
            try
            {
                if (dtAuthorized.Rows.Count > 0)
                {
                    foreach (DataRow drv in dtAuthorized.Rows)
                    {
                        if (SQLAuthorized.CheckAuthorized(RoleId, Convert.ToInt32(drv["AuthorizeId"])) == true)
                        {
                            if (Convert.ToBoolean(drv["Selected"]) == false)
                            {
                                SQLAuthorized.RomoveAuthorizedUser(RoleId, Convert.ToInt32(drv["AuthorizeId"]));
                            }
                        }
                        else
                        {
                            if (Convert.ToBoolean(drv["Selected"]) == true)
                            {
                                SQLAuthorized.AddAuthorizedUser(RoleId, Convert.ToInt32(drv["AuthorizeId"]));
                            }
                        }
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

        private void LinkSelected_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SelectedAutorized();
            if (LinkSelected.Text == "เลือกทั้งหมด")
            {
                LinkSelected.Text = "ไม่เลือกทั้งหมด";
            }
            else
            {
                LinkSelected.Text = "เลือกทั้งหมด";
            }
        }

        private bool SelectedAutorized()
        {
            try
            {
                if (dtAuthorized.Rows.Count > 0)
                {
                    foreach (DataRow drv in dtAuthorized.Rows)
                    {
                        if (LinkSelected.Text == "เลือกทั้งหมด")
                        {
                            drv["Selected"] = true;
                        }
                        else
                        {
                            drv["Selected"] = false;
                        }
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

    }
}
