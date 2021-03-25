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
using System.IO;
using RubberSoft.Data;
using System.Diagnostics;
using RubberSoft.Main;
using DevExpress.XtraBars.Docking2010.Customization;
using DevExpress.XtraBars.Docking2010.Views.WindowsUI;
using RubberSoft.Report;
using RubberSoft.Tools;

namespace RubberSoft
{
    public partial class FrmMain : XtraForm
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        readonly SQLData SQLData = new SQLData();
        readonly SQLAuthorized SQLAuthorized = new SQLAuthorized();
        readonly SQLTerminal SQLTerminal = new SQLTerminal();
        readonly SQLLog SQLLog = new SQLLog();

        private TimeSpan TimeNow;
        readonly string fileName = Application.StartupPath + @"\SplitBuy.xml";

        private void FrmMain_Load(object sender, EventArgs e)
        {
            if (SQLData.CheckRubberSoftConnection() == false)
            {
                XtraMessageBox.Show("Not connect database", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                if (SQLData.CheckConnection() == false)
                {
                    XtraMessageBox.Show("Not Connection database", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Load_Form();
            }

            lblUserName.Text = "ยินดีต้อนรับ : " + ClassProperty.permisUserNm;
        }

        private void Load_Form()
        {
            try
            {
                TimerMain.Enabled = true;

                if (ClassProperty.permisUserNm == SQLData.ID && ClassProperty.permisUserPass == SQLData.DecryptString(SQLData.KEY))
                {
                    BtnTools.Visible = true;
                }
                else
                {
                    BtnTools.Visible = false;
                }

                SaveLayOut();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void TimerMain_Tick(object sender, EventArgs e)
        {
            TimeNow = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            lblDay.Text = DateTime.Now.ToString("วันdddd ที่ dd MMMM yyyy", SQLData._cultureThInfo) + " เวลา : " + TimeNow;
        }

        private void BtnLogOut_Click(object sender, EventArgs e)
        {
            CheckLogOut();
        }

        private bool CheckLogOut()
        {
            try
            {
                //LogOut();
                Load_UcLogin();

                SQLLog.Addlog(0, "LogOut", "LogOut By " + ClassProperty.GetCurrentName, ClassProperty.permisUserID,
                                             ClassProperty.IPMachine, ClassProperty.MachineName);

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        //private void LogOut()
        //{
        //    try
        //    {
        //        FrmLogin frm = new FrmLogin();
        //        {
        //            this.Hide();
        //            if (frm.ShowDialog(this) == DialogResult.OK)
        //            {
        //                lblUserName.Text = "ยินดีต้อนรับ : " + ClassProperty.permisUserNm;

        //                if (ClassProperty.permisUserNm == SQLData.ID && ClassProperty.permisUserPass == SQLData.DecryptString(SQLData.KEY))
        //                {
        //                    BtnTools.Visible = true;
        //                }
        //            }
        //            else
        //            {
        //                return;
        //            }

        //            //frm.Dispose();
        //        }
        //        this.Show();
        //    }
        //    catch (Exception ex)
        //    {
        //        XtraMessageBox.Show(ex.Message);
        //    }
        //}

        private void Load_UcLogin()
        {
            try
            {
                ClassProperty.IsCusType = 0;
                ClassProperty.IsOpenBuy = true;

                FlyoutDialog flyout_Dialog = new FlyoutDialog(FindForm(), new UcLogin());
                flyout_Dialog.Properties.Style = FlyoutStyle.Popup;
                {
                    var frm = flyout_Dialog;
                    if (frm.ShowDialog(this) == DialogResult.OK)
                    {
                        lblUserName.Text = "ยินดีต้อนรับ : " + ClassProperty.permisUserNm;

                        if (ClassProperty.permisUserNm == SQLData.ID && ClassProperty.permisUserPass == SQLData.DecryptString(SQLData.KEY))
                        {
                            BtnTools.Visible = true;
                        }
                        else
                        {
                            BtnTools.Visible = false;
                        }
                    }
                    else
                    {
                        Load_UcLogin();
                    }

                    frm.Dispose();
                }

                this.Show();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void TBuyProducts_ItemClick(object sender, TileItemEventArgs e)
        {
            Load_FrmBuyLatex();
        }

        private void Load_FrmBuyLatex()
        {
            try
            {
                // 5	3 : สิทธิ์ใช้งานหน้าการซื้อ	เข้าใช้งานหน้าการซื้อ
                int AuthorizeId = 5;

                if (SQLAuthorized.CheckAuthorized(ClassProperty.permisRoleId, AuthorizeId) == false)
                {
                    SQLAuthorized.AlertAuthorized("สิทธิ์ใช้งานหน้าการซื้อ", "ไม่มีเข้าใช้งานหน้าการซื้อ");
                    return;
                }

                ClassProperty.IsBuy = true;
                ClassProperty.IsCusType = 1;
                ClassProperty.CustomerGroupId = 1;
                ClassProperty.IsOpenBuy = false;

                FrmBuyLatex frm = new FrmBuyLatex();
                {
                    //this.Hide();
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void BtnSetUsers_Click(object sender, EventArgs e)
        {
            Load_FrmAddUser();
        }

        private void Load_FrmAddUser()
        {
            try
            {
                // 2-001	2 : สิทธิ์ใช้งานหน้าผู้ใช้งาน 	เข้าใช้งานหน้าผู้ใช้งาน
                int AuthorizeId = 2;

                if (SQLAuthorized.CheckAuthorized(ClassProperty.permisRoleId, AuthorizeId) == false)
                {
                    SQLAuthorized.AlertAuthorized("สิทธิ์ใช้งานหน้าผู้ใช้งาน", "ไม่มีสิทธิ์เข้าใช้งานหน้าผู้ใช้งาน");
                    return;
                }

                FrmAddUser frm = new FrmAddUser();
                {
                    //this.Hide();
                    if (frm.ShowDialog(this) == DialogResult.OK)
                    {
                        GetUserData(ClassProperty.permisUserID);
                    }
                    else
                    {
                        return;
                    }

                    frm.Dispose();
                }
                this.Show();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private bool GetUserData(int UserId)
        {
            try
            {
                using (var context = new RubberSoftEntities())
                {
                    var query = context.spt_GetUsers().Where(o => o.UserId == UserId).ToList();
                    if (query.Count > 0)
                    {
                        foreach (spt_GetUsers_Result dt in query)
                        {
                            ClassProperty.permisUserNm = dt.UserName;
                            ClassProperty.permisRole = dt.RoleName;
                            ClassProperty.permisRoleId = dt.UserTypeId.Value;

                            lblUserName.Text = "ยินดีต้อนรับ : " + ClassProperty.permisUserNm;
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

        private void TProducts_ItemClick(object sender, TileItemEventArgs e)
        {
            Load_UcCustomerType();
        }

        private void Load_UcCustomerType()
        {
            try
            {
                // 15	5 : สิทธิ์ใช้งานหน้าสมาชิก	เข้าใช้งานหน้าสมาชิก
                int AuthorizeId = 15;

                if (SQLAuthorized.CheckAuthorized(ClassProperty.permisRoleId, AuthorizeId) == false)
                {
                    SQLAuthorized.AlertAuthorized("สิทธิ์ใช้งานหน้าผู้ใช้งาน", "ไม่มีสิทธิ์เข้าใช้งานหน้าสมาชิก");
                    return;
                }

                ClassProperty.IsCusType = 0;
                ClassProperty.CustomerGroupId = 0;
                ClassProperty.IsOpenBuy = true;

                FlyoutDialog flyout_Dialog = new FlyoutDialog(FindForm(), new UcCustomerType());
                flyout_Dialog.Properties.Style = FlyoutStyle.Popup;
                {
                    var frm = flyout_Dialog;
                    if (frm.ShowDialog(this) == DialogResult.OK)
                    {

                    }

                    frm.Dispose();
                }
                this.Show();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private bool SaveLayOut()
        {
            try
            {
                if (!File.Exists(fileName))
                {
                    using (FileStream fs = File.Create(fileName))
                    {
                        for (byte i = 0; i < 100; i++)
                        {
                            fs.WriteByte(i);
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

        private void TCustomers_ItemClick(object sender, TileItemEventArgs e)
        {
            Load_FrmReport();
        }

        private void Load_FrmReport()
        {
            try
            {
                // 23	6 : สิทธิ์ใช้งานหน้ารายงาน	เข้าใช้งานหน้ารายงาน
                int AuthorizeId = 23;

                if (SQLAuthorized.CheckAuthorized(ClassProperty.permisRoleId, AuthorizeId) == false)
                {
                    SQLAuthorized.AlertAuthorized("สิทธิ์ใช้งานหน้ารายงาน", "ไม่มีสิทธิ์เข้าใช้งานหน้ารายงาน");
                    return;
                }

                FrmReport frm = new FrmReport();
                {
                    //this.Hide();
                    frm.Show();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void TSaleProducts_ItemClick(object sender, TileItemEventArgs e)
        {
            Load_FrmSale();
        }

        private void Load_FrmSale()
        {
            try
            {
                // 10	4 : สิทธิ์ใช้งานหน้าการขาย	 เข้าใช้งานหน้าการขาย
                int AuthorizeId = 10;

                if (SQLAuthorized.CheckAuthorized(ClassProperty.permisRoleId, AuthorizeId) == false)
                {
                    SQLAuthorized.AlertAuthorized("สิทธิ์ใช้งานหน้าการขาย", "ไม่มีเข้าใช้งานหน้าการขาย");
                    return;
                }

                ClassProperty.IsBuy = false;
                ClassProperty.IsCusType = 2;
                ClassProperty.CustomerGroupId = 3;
                ClassProperty.IsOpenBuy = false;

                FrmSaleLatex frm = new FrmSaleLatex();
                {
                    //this.Hide();
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void Load_FrmTools()
        {
            try
            {
                FrmTools frm = new FrmTools();
                {
                    //this.Hide();
                    frm.Show();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void BtnTools_Click(object sender, EventArgs e)
        {
            Load_FrmTools();
        }
        private void TileBuyData_ItemClick(object sender, TileItemEventArgs e)
        {
            Load_UcLoadBuy();
        }

        private void Load_UcLoadBuy()
        {
            try
            {
                // 30	7 : สิทธิ์ใช้งานหน้าค้นหาการซื้อ	เข้าใช้งานหน้าค้นหาการซื้อ
                int AuthorizeId = 30;

                if (SQLAuthorized.CheckAuthorized(ClassProperty.permisRoleId, AuthorizeId) == false)
                {
                    SQLAuthorized.AlertAuthorized("สิทธิ์ใช้งานหน้าค้นหาการซื้อ", "ไม่มีสิทธิ์เข้าใช้งานหน้าค้นหาการซื้อ");
                    return;
                }

                ClassProperty.IsOpenBuy = true;

                FlyoutDialog flyout_Dialog = new FlyoutDialog(FindForm(), new UcLoadBuy());
                flyout_Dialog.Properties.Style = FlyoutStyle.Popup;
                {
                    var frm = flyout_Dialog;
                    if (frm.ShowDialog(this) == DialogResult.OK)
                    {

                    }

                    frm.Dispose();
                }
                this.Show();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void TileSaleData_ItemClick(object sender, TileItemEventArgs e)
        {
            Load_UcLoadSale();
        }

        private void Load_UcLoadSale()
        {
            try
            {
                // 31	8 : สิทธิ์ใช้งานหน้าค้นหาการขาย 	เข้าใช้งานหน้าค้นหาการขาย
                int AuthorizeId = 31;

                if (SQLAuthorized.CheckAuthorized(ClassProperty.permisRoleId, AuthorizeId) == false)
                {
                    SQLAuthorized.AlertAuthorized("สิทธิ์ใช้งานหน้าค้นหาการขาย", "ไม่มีสิทธิ์เข้าใช้งานหน้าค้นหาการขาย");
                    return;
                }

                ClassProperty.IsOpenBuy = true;

                FlyoutDialog flyout_Dialog = new FlyoutDialog(FindForm(), new UcLoadSale());
                flyout_Dialog.Properties.Style = FlyoutStyle.Popup;
                {
                    var frm = flyout_Dialog;
                    if (frm.ShowDialog(this) == DialogResult.OK)
                    {

                    }

                    frm.Dispose();
                }
                this.Show();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void TileMain_Click(object sender, EventArgs e)
        {

        }

        private void TileAutorize_ItemClick(object sender, TileItemEventArgs e)
        {
            Load_FrmAuthorized();
        }

        private void Load_FrmAuthorized()
        {
            try
            {
                // 32	9 : สิทธิ์ใช้งานหน้าตั้งค่าสิทธิ์การใช้งาน 	เข้าใช้งานหน้าตั้งค่าสิทธิ์การใช้งาน
                int AuthorizeId = 32;

                if (SQLAuthorized.CheckAuthorized(ClassProperty.permisRoleId, AuthorizeId) == false)
                {
                    SQLAuthorized.AlertAuthorized("สิทธิ์ใช้งานหน้าตั้งค่าสิทธิ์การใช้งาน", "ไม่มีสิทธิ์เข้าใช้งานหน้าตั้งค่าสิทธิ์การใช้งาน");
                    return;
                }

                FrmAuthorized frm = new FrmAuthorized();
                {
                    //this.Hide();
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

    }
}
