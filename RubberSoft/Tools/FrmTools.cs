using DevExpress.XtraBars.Docking2010.Customization;
using DevExpress.XtraBars.Docking2010.Views.WindowsUI;
using DevExpress.XtraEditors;
using RubberSoft.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RubberSoft.Tools
{
    public partial class FrmTools : XtraForm
    {
        public FrmTools()
        {
            InitializeComponent();
        }

        private void FrmTools_Load(object sender, EventArgs e)
        {

        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LinkGetBuyData_Click(object sender, EventArgs e)
        {
            FrmGetBuyData frm = new FrmGetBuyData();
            {
                this.Hide();
                frm.Show();
            }
        }

        private void LinkAdjustOutstandingDebt_Click(object sender, EventArgs e)
        {
            FrmAdjustOutStandingBalance frm = new FrmAdjustOutStandingBalance();
            {
                this.Hide();
                frm.Show();
            }
        }

        private void LinkClearData_Click(object sender, EventArgs e)
        {
            FrmClearData frm = new FrmClearData();
            {
                this.Hide();
                frm.Show();
            }
        }

        private void LinkTerminal_Click(object sender, EventArgs e)
        {
            FrmTerminal frm = new FrmTerminal();
            {
                this.Hide();
                frm.Show();
            }
        }
    }
}
