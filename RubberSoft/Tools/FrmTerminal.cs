using RubberSoft.Data;
using DevExpress.XtraEditors;
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
    public partial class FrmTerminal : XtraForm
    {
        public FrmTerminal()
        {
            InitializeComponent();
        }

        readonly SQLTerminal SQLTerminal = new SQLTerminal();
        DataTable dtTerminal = new DataTable();

        private void FrmTerminal_Load(object sender, EventArgs e)
        {
            GetTerminal();
        }

        private bool GetTerminal()
        {
            try
            {
                DataSet ds = SQLTerminal.Spt_GetTerminal();
                dtTerminal = ds.Tables[0];

                GridTerminal.DataSource = dtTerminal;

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
            FrmTools frm = new FrmTools();
            {
                this.Close();
                frm.Show();
            }
        }

        private bool SaveTerminal()
        {
            try
            {
                foreach (DataRow drv in dtTerminal.Rows)
                {
                    SQLTerminal.UpdateTerminal(Convert.ToInt32(drv["TerminalId"]), 
                        Convert.ToBoolean(drv["Active"]));
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
            if (SaveTerminal())
            {
                XtraMessageBox.Show("บันทึกข้อมูลสำเร็จ", "สถานะ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetTerminal();
            }
        }
    }
}
