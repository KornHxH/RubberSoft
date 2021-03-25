using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using RubberSoft.Data;

namespace RubberSoft.Main
{
    public partial class UcSelectProduct : XtraUserControl
    {
        public UcSelectProduct()
        {
            InitializeComponent();
        }

        private void UcSelectProduct_Load(object sender, EventArgs e)
        {

        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            ClassProperty.sProductType = 0;
            this.FindForm().DialogResult = DialogResult.Cancel;           
        }

        private void Btnรายการรับซื้อน้ำยาง_Click(object sender, EventArgs e)
        {
            ClassProperty.sProductType = 1;
            this.FindForm().DialogResult = DialogResult.OK;
        }

        private void Btnรายการรับซื้อยางแผ่นดิบ_Click(object sender, EventArgs e)
        {
            ClassProperty.sProductType = 2;
            this.FindForm().DialogResult = DialogResult.OK;
        }
    }
}
