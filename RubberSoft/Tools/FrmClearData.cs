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
    public partial class FrmClearData : XtraForm
    {
        public FrmClearData()
        {
            InitializeComponent();
        }

        readonly SQLClearData SQLClearData = new SQLClearData();

        private void FrmClearData_Load(object sender, EventArgs e)
        {

        }

        private void LinkClearLogDetails_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("คุณยืนยันที่จะเคลียร์ Log การใช้งาน ใช่หรือไม่?", "ยืนยัน", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                // เคลียร์ Log การใช้งาน
                if (SQLClearData.ClearLogDetails())
                {
                    XtraMessageBox.Show("เคลียร์ Log การใช้งาน สำเร็จ", "สถานะ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void LinkClearTerminal_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("คุณยืนยันที่จะเคลียร์ เครื่องใช้งาน ใช่หรือไม่?", "ยืนยัน", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                // เคลียร์ เครื่องใช้งาน
                if (SQLClearData.ClearTerminal())
                {
                    XtraMessageBox.Show("เคลียร์ เครื่องใช้งาน สำเร็จ", "สถานะ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void LinkClearUserRole_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("คุณยืนยันที่จะเคลียร์ ประเภทสิทธิ์การใช้งาน ใช่หรือไม่?", "ยืนยัน", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                // เคลียร์ ประเภทสิทธิ์การใช้งาน
                if (SQLClearData.ClearUserRole())
                {
                    XtraMessageBox.Show("เคลียร์ ประเภทสิทธิ์การใช้งาน สำเร็จ", "สถานะ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void LinkClearUsers_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("คุณยืนยันที่จะเคลียร์ ข้อมูลผู้ใช้งาน ใช่หรือไม่?", "ยืนยัน", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                // เคลียร์ ข้อมูลผู้ใช้งาน
                if (SQLClearData.ClearUsers())
                {
                    XtraMessageBox.Show("เคลียร์ ข้อมูลผู้ใช้งาน สำเร็จ", "สถานะ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void LinkClearAuthorizedUser_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("คุณยืนยันที่จะเคลียร์ สิทธิ์การใช้งาน ใช่หรือไม่?", "ยืนยัน", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                // เคลียร์ สิทธิ์การใช้งาน
                if (SQLClearData.ClearAuthorizedUser())
                {
                    XtraMessageBox.Show("เคลียร์ สิทธิ์การใช้งาน สำเร็จ", "สถานะ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void LinkClearCustomer_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("คุณยืนยันที่จะเคลียร์ ข้อมูลลูกค้า ใช่หรือไม่?", "ยืนยัน", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                // เคลียร์ ข้อมูลลูกค้า
                if (SQLClearData.ClearCustomer())
                {
                    XtraMessageBox.Show("เคลียร์ ข้อมูลลูกค้า สำเร็จ", "สถานะ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void LinkClearCustomerPrice_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("คุณยืนยันที่จะเคลียร์ ข้อมูลราคาล่วงหน้า ใช่หรือไม่?", "ยืนยัน", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                // เคลียร์ ข้อมูลราคาล่วงหน้า
                if (SQLClearData.ClearCustomerPrice())
                {
                    XtraMessageBox.Show("เคลียร์ ข้อมูลราคาล่วงหน้า สำเร็จ", "สถานะ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void LinkClearBuy_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("คุณยืนยันที่จะเคลียร์ ข้อมูลการซื้อ ใช่หรือไม่?", "ยืนยัน", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                // เคลียร์ ข้อมูลการซื้อ
                if (SQLClearData.ClearBuy())
                {
                    if (SQLClearData.ClearBuyProduct())
                    {
                        if (SQLClearData.ClearSaveBuy())
                        {
                            if (SQLClearData.ClearSaveBuyProduct())
                            {
                                XtraMessageBox.Show("เคลียร์ ข้อมูลการซื้อ สำเร็จ", "สถานะ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
            }
        }

        private void LinkClearSale_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("คุณยืนยันที่จะเคลียร์ ข้อมูลการขาย ใช่หรือไม่?", "ยืนยัน", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                // เคลียร์ ข้อมูลการขาย
                if (SQLClearData.ClearSale())
                {
                    if (SQLClearData.ClearSaleProduct())
                    {
                        if (SQLClearData.ClearSaveSale())
                        {
                            if (SQLClearData.ClearSaveSaleProduct())
                            {
                                XtraMessageBox.Show("เคลียร์ ข้อมูลการขาย สำเร็จ", "สถานะ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
            }
        }

        private void LinkClearWeightBalance_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("คุณยืนยันที่จะเคลียร์ ข้อมูลน้ำหนักยาง ใช่หรือไม่?", "ยืนยัน", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                // เคลียร์ ข้อมูลน้ำหนักยาง
                if (SQLClearData.ClearTransactionLog())
                {
                    if (SQLClearData.ClearWeightBalance())
                    {
                        XtraMessageBox.Show("เคลียร์ ข้อมูลน้ำหนักยาง สำเร็จ", "สถานะ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void LinkClearOutStandingBalance_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("คุณยืนยันที่จะเคลียร์ ข้อมูลยอดคงเหลือลูกค้า ใช่หรือไม่?", "ยืนยัน", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                // เคลียร์ ข้อมูลยอดคงเหลือลูกค้า
                if (SQLClearData.ClearOutStandingLog())
                {
                    if (SQLClearData.ClearOutStandingBalance())
                    {
                        XtraMessageBox.Show("เคลียร์ ข้อมูลยอดคงเหลือลูกค้า สำเร็จ", "สถานะ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void LinkClearBuySaleData_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("คุณยืนยันที่จะเคลียร์ข้อมูล ส่วนข้อมูลการซื้อ-ขาย ใช่หรือไม่?", "ยืนยัน", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                if (SQLClearData.ClearBuySaleData())
                {
                    XtraMessageBox.Show("เคลียร์ข้อมูล ส่วนข้อมูลการซื้อ-ขาย สำเร็จ", "สถานะ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void LinkClearDetails_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("คุณยืนยันที่จะเคลียร์ข้อมูล ส่วนข้อมูลทั่วไป ใช่หรือไม่?", "ยืนยัน", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                if (SQLClearData.ClearDetailsData())
                {
                    XtraMessageBox.Show("เคลียร์ข้อมูล ส่วนข้อมูลทั่วไป สำเร็จ", "สถานะ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
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
    }
}
