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
    public partial class FrmAddUser : XtraForm
    {
        public FrmAddUser()
        {
            InitializeComponent();
        }

        readonly SQLData SQLData = new SQLData();
        readonly SQLAuthorized SQLAuthorized = new SQLAuthorized();

        private int userId, roleId;
        private string strUserName, userName, passWord, firstName, lastName;

        private void FrmAddUser_Load(object sender, EventArgs e)
        {
            RefreshForm();
        }

        private void RefreshForm()
        {
            try
            {
                ClearValues();

                SplashScreenManager.ShowForm(this, typeof(FrmSplashScreen), true, true, false);

                if (GetRoles() == true)
                {
                    if (GetGridData(TxtSearch.Text) == true)
                    {
                        SplashScreenManager.CloseForm(false);
                    }
                    else
                    {
                        SplashScreenManager.CloseForm(false);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            GetGridData(TxtSearch.Text);
        }

        private void ClearValues()
        {
            try
            {
                userId = 0;
                roleId = 0;
                SLRole.EditValue = 0;
                TxtUserName.Text = "";
                TxtPassword.Text = "";
                TxtFirstName.Text = "";
                TxtLastName.Text = "";
                TxtUserName.Select();

                strUserName = "";
                userName = "";
                strUserName = "";
                passWord = "";
                firstName = "";
                lastName = "";
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            ClearValues();
        }

        private void BtnShowPassWord_Click(object sender, EventArgs e)
        {
            if (TxtPassword.Properties.UseSystemPasswordChar == true)
            {
                TxtPassword.Properties.UseSystemPasswordChar = false;
            }
            else
            {
                TxtPassword.Properties.UseSystemPasswordChar = true;
            }
        }

        private bool GetRoles()
        {
            try
            {
                using (var context = new RubberSoftEntities())
                {
                    var query = context.spt_GetRoles().Where(o => o.Active == 1).ToList();
                    SLRole.Properties.DataSource = query;
                    SLRole.Properties.DisplayMember = "RoleFullName";
                    SLRole.Properties.ValueMember = "RoleId";
                }

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool GetGridData(string tex)
        {
            try
            {
                using (var context = new RubberSoftEntities())
                {
                    if (tex.Trim() == "" || tex.Length <= 0)
                    {
                        var query = context.spt_GetUsers().Where(o => o.Active == true).ToList();
                        GridUsers.DataSource = query;
                    }
                    else
                    {
                        var query = context.spt_GetUsers().Where(o => o.Active == true && o.UserName == tex
                                                                 || o.FirstName == tex || o.LastName == tex).ToList();
                        GridUsers.DataSource = query;
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

        private bool CheckData(int id)
        {
            try
            {
                using (var context = new RubberSoftEntities())
                {
                    var query = context.spt_GetUsers().Where(o => o.UserId == id).ToList();
                    if (query.Count > 0)
                    {
                        foreach (spt_GetUsers_Result dt in query)
                        {
                            userId = dt.UserId;
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

        private void GridViewUsers_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (!(sender is GridView view)) return;
            userId = Convert.ToInt32(view.GetFocusedRowCellValue("UserId"));

            if (CheckData(userId) == true)
            {
                GetData(userId);
            }
        }

        private bool GetData(int id)
        {
            try
            {
                using (var context = new RubberSoftEntities())
                {
                    var query = context.spt_GetUsers().Where(o => o.UserId == id).ToList();
                    if (query.Count > 0)
                    {
                        foreach (spt_GetUsers_Result dt in query)
                        {
                            userName = dt.UserName;
                            strUserName = dt.UserName;
                            roleId = dt.UserTypeId.Value;
                            TxtUserName.Text = dt.UserName;
                            TxtPassword.Text = SQLData.DecryptString(dt.Password);
                            TxtFirstName.Text = dt.FirstName;
                            TxtLastName.Text = dt.LastName;
                            SLRole.EditValue = roleId;
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

        private void BtnSave_Click(object sender, EventArgs e)
        {
            // 3	2 : สิทธิ์ใช้งานหน้าผู้ใช้งาน 	บันทึกข้อมูลผู้ใช้งาน
            int AuthorizeId = 3;

            if (SQLAuthorized.CheckAuthorized(ClassProperty.permisRoleId, AuthorizeId) == false)
            {
                SQLAuthorized.AlertAuthorized("สิทธิ์ใช้งานหน้าผู้ใช้งาน", "ไม่มีสิทธิ์บันทึกข้อมูลผู้ใช้งาน");
                return;
            }

            if (SaveData() == true)
            {
                GetGridData("");
                GridViewUsers.FocusedRowHandle = GridViewUsers.LocateByValue("UserName", TxtUserName.Text);
                userId = Convert.ToInt32(GridViewUsers.GetFocusedRowCellValue("UserId"));
                GetData(userId);
            }
        }

        private bool SaveData()
        {
            try
            {
                if (CheckValues() == true)
                {
                    if (CheckData(userId) == true)
                    {
                        if (userName != strUserName)
                        {
                            if (CheckDuplicate() == true)
                            {
                                XtraMessageBox.Show("ชื่อผู้ใช้งานนี้ถูกใช้งานแล้ว !", "เตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return false;
                            }
                            else
                            {
                                if (UpdateData(true) == true)
                                {
                                    XtraMessageBox.Show("บันทึกข้อมูลสำเร็จ", "สถานะ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }
                        else
                        {
                            if (UpdateData(true) == true)
                            {
                                XtraMessageBox.Show("บันทึกข้อมูลสำเร็จ", "สถานะ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                    else
                    {
                        if (CheckDuplicate() == true)
                        {
                            XtraMessageBox.Show("ชื่อผู้ใช้งานนี้ถูกใช้งานแล้ว !", "เตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                        else
                        {
                            if (AddData() == true)
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

        private bool CheckDuplicate()
        {
            try
            {
                using (var context = new RubberSoftEntities())
                {
                    var query = context.spt_GetUsers().Where(o => o.UserName == userName && o.Active == true).ToList();
                    if (query.Count > 0)
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

        private bool CheckValues()
        {
            try
            {
                if (roleId == 0)
                {
                    XtraMessageBox.Show("กรุณาระบุ สิทธิ์การใช้งาน !", "เตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                if (TxtUserName.Text.Trim() == string.Empty)
                {
                    XtraMessageBox.Show("กรุณาระบุชื่อผู้ใช้งาน !", "เตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TxtUserName.Focus();
                    return false;
                }

                if (TxtPassword.Text.Trim() == string.Empty)
                {
                    XtraMessageBox.Show("กรุณาระบุรหัสผ่าน !", "เตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TxtPassword.Focus();
                    return false;
                }

                if (TxtFirstName.Text.Trim() == string.Empty)
                {
                    XtraMessageBox.Show("กรุณาระบุชื่อ !", "เตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TxtFirstName.Focus();
                    return false;
                }

                userName = TxtUserName.Text;
                passWord = SQLData.EncryptString(TxtPassword.Text);
                firstName = TxtFirstName.Text;
                lastName = TxtLastName.Text;

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool AddData()
        {
            try
            {
                using (var context = new RubberSoftEntities())
                {
                    var query = context.spt_AddUsers(roleId, userName, passWord, firstName, lastName, true);

                    return true;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private void BtnAdd_Click_1(object sender, EventArgs e)
        {
            ClearValues();
        }

        private bool UpdateData(bool active)
        {
            try
            {
                using (var context = new RubberSoftEntities())
                {
                    var query = context.spt_UpdateUsers(userId, roleId, userName, passWord, firstName, lastName, active);

                    return true;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            userId = Convert.ToInt32(GridViewUsers.GetFocusedRowCellValue("UserId"));

            // 4	2 : สิทธิ์ใช้งานหน้าผู้ใช้งาน	 ลบข้อมูลผู้ใช้งาน
            int AuthorizeId = 4;

            if (SQLAuthorized.CheckAuthorized(ClassProperty.permisRoleId, AuthorizeId) == false)
            {
                SQLAuthorized.AlertAuthorized("สิทธิ์ใช้งานหน้าผู้ใช้งาน", "ไม่มีสิทธิ์ลบข้อมูลผู้ใช้งาน");
                return;
            }

            if (CheckData(userId) == true)
            {
                if (XtraMessageBox.Show(" คุณยืนยันที่จะลบข้อมูล ใช่หรือไม่?", "ยืนยัน", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    if (UpdateData(false) == true)
                    {
                        XtraMessageBox.Show("ลบข้อมูลสำเร็จ", "สถานะ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearValues();
                        GetGridData("");
                    }
                }
            }
        }

        private void SLRole_EditValueChanged(object sender, EventArgs e)
        {
            SLRoleSelect(GridViewRoles);
        }

        private bool SLRoleSelect(GridView _view)
        {
            try
            {
                if (_view.SelectedRowsCount > 0)
                {
                    roleId = Convert.ToInt32(_view.GetFocusedRowCellValue("RoleId"));
                }

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.FindForm().DialogResult = DialogResult.OK;
        }
    }
}
