using DevExpress.XtraBars.Docking2010.Customization;
using DevExpress.XtraBars.Docking2010.Views.WindowsUI;
using DevExpress.XtraEditors;
using RubberSoft.Data;
using RubberSoft.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RubberSoft.Main
{
    public partial class UcLogin : XtraUserControl
    {
        public UcLogin()
        {
            InitializeComponent();
        }

        readonly SQLTerminal SQLTerminal = new SQLTerminal();
        readonly SQLAuthorized SQLAuthorized = new SQLAuthorized();
        readonly SQLData SQLData = new SQLData();
        readonly SQLLog SQLLog = new SQLLog();

        private TimeSpan TimeNow;

        readonly SQLAddImage SQLImage = new SQLAddImage();

        private void UcLogin_Load(object sender, EventArgs e)
        {
            TimeLogin.Enabled = true;
            GetImg(PicProduct);
            ClearLogin();
        }

        private void ClearLogin()
        {
            try
            {
                TxtPassword.Text = "";
                TxtUserName.Text = "";
                TxtUserName.Select();

                string Logo = System.Configuration.ConfigurationManager.AppSettings["Logo"].ToString();
                if (Logo == "True")
                {
                    PicProduct.Properties.ShowMenu = true;
                }
                else if (Logo == "False")
                {
                    PicProduct.Properties.ShowMenu = false;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void GetImg(PictureEdit Img)
        {
            try
            {
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                ds = SQLImage.Spt_GetBranchImg(1);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow drv in dt.Rows)
                    {
                        Img.Image = SQLImage.LoadPic(drv);
                        SQLImage.UpdateBranchImg(1, Img);
                    }
                }
                else
                {
                    Img.Image = Resources.NoImage;
                    SQLImage.AddBranchImg(Img);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            Login();
        }

        private bool Login()
        {
            try
            {
                if (SQLData.CheckRubberSoftConnection() == false || SQLData.CheckConnection() == false)
                {
                    XtraMessageBox.Show("ไม่สามารถเชื่อมต่อระบบได้", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (ClassProperty.EnableTerminal == false)
                {
                    XtraMessageBox.Show("เครื่องใช้งานไม่มีสิทธิ์เข้าใช้งานระบบ", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (TxtUserName.Text.Trim() == "" || TxtPassword.Text == "")
                {
                    Alert();
                    return false;
                }

                if (TxtUserName.Text.Trim() == SQLData.ID &&
                    TxtPassword.Text.Trim() == SQLData.DecryptString(SQLData.KEY))
                {
                    ClassProperty.permisUserID = -1;
                    ClassProperty.permisUserNm = SQLData.ID;
                    ClassProperty.permisRole = "IT";
                    ClassProperty.permisRoleId = -1;
                    ClassProperty.permisUserPass = TxtPassword.Text.Trim();

                    CheckLogin();
                }
                else
                {
                    using (var context = new RubberSoftEntities())
                    {

                        var query = context.spt_GetUsers().Where(o => o.UserName == TxtUserName.Text
                                               && SQLData.DecryptString(o.Password) == TxtPassword.Text.Trim()).ToList();
                        if (query.Count > 0)
                        {
                            foreach (spt_GetUsers_Result dt in query)
                            {
                                ClassProperty.permisUserID = dt.UserId;
                                ClassProperty.permisUserNm = dt.UserName;
                                ClassProperty.permisRole = dt.RoleName;
                                ClassProperty.permisRoleId = dt.UserTypeId.Value;
                                ClassProperty.permisUserPass = TxtPassword.Text.Trim();

                                // 1-001	1 : สิทธิ์ใช้งานหน้าเข้าสู่ระบบ	เข้าสู่ระบบ
                                int AuthorizeId = 1;

                                if (SQLAuthorized.CheckAuthorized(ClassProperty.permisRoleId, AuthorizeId) == false)
                                {
                                    SQLAuthorized.AlertAuthorized("สิทธิ์ใช้งานหน้าเข้าสู่ระบบ", "ไม่มีสิทธิ์การใช้งานระบบ");
                                    return false;
                                }

                                CheckLogin();
                            }
                        }
                        else
                        {
                            //XtraMessageBox.Show("ข้อมูลผู้ใช้งานมีปัญหา กรุณาติดต่อ IT !", "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            Alert();
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

        private bool CheckLogin()
        {
            try
            {
                SQLLog.Addlog(0, "Login", "Login By " + ClassProperty.GetCurrentName, ClassProperty.permisUserID,
                                        ClassProperty.IPMachine, ClassProperty.MachineName);

                SQLImage.UpdateBranchImg(1, PicProduct);

                this.FindForm().DialogResult = DialogResult.OK;

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private void Alert()
        {
            try
            {
                FlyoutAction action = new FlyoutAction()
                {
                    Caption = "คำเตือน!",
                    Description = "คุณระบุชื่อผู้ใช้งาน (username) หรือรหัสผ่าน (password) ไม่ถูกต้อง"
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
                properties.Appearance.BackColor = Color.Orange;
                properties.Appearance.ForeColor = Color.White;
                properties.Appearance.BorderColor = Color.Red;
                properties.Style = FlyoutStyle.Popup;

                if (FlyoutDialog.Show(null, action, properties, predicate) == DialogResult.OK)
                    return;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Close_Form();
        }

        private void Close_Form()
        {
            try
            {
                FlyoutAction action = new FlyoutAction() { Caption = "ยืนยัน", Description = "คุณต้องการปิดโปรแกรม ใช่หรือไม่?" };
                Predicate<DialogResult> predicate = SQLData.CloseFunc;
                FlyoutCommand command1 = new FlyoutCommand() { Text = "ใช่", Result = DialogResult.Yes };
                FlyoutCommand command2 = new FlyoutCommand() { Text = "ไม่ใช่", Result = DialogResult.No };
                action.Commands.Add(command1);
                action.Commands.Add(command2);
                FlyoutProperties properties = new FlyoutProperties
                {
                    ButtonSize = new Size(100, 40),
                    // properties.Appearance.BackColor = Color.WhiteSmoke
                    Style = FlyoutStyle.Popup
                };

                if (FlyoutDialog.Show(null, action, properties, predicate) == DialogResult.Yes)
                {
                    System.Environment.Exit(0);
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void TxtUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.Handled = true;
                Login();
            }
            else if (e.KeyData == Keys.Escape)
            {
                e.Handled = true;
                Close_Form();
            }
        }

        private void TxtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.Handled = true;
                Login();
            }
            else if (e.KeyData == Keys.Escape)
            {
                e.Handled = true;
                Close_Form();
            }
        }

        private void TimeLogin_Tick(object sender, EventArgs e)
        {
            TimeNow = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            lblDay.Text = DateTime.Now.ToString("วันdddd ที่ dd MMMM yyyy", SQLData._cultureThInfo);
            lblTime.Text = "เวลา: " + TimeNow;
            lblFullDate.Text = lblDay.Text + " " + lblTime.Text;
        }

        private void UcLogin_Leave(object sender, EventArgs e)
        {
            
        }
    }
}
