using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubberSoft.Data
{
    class SQLLog
    {
        public bool Addlog(int logtype, string logname, string logdetail, int userid, string ipmachhine, string machinename)
        {
            try
            {
                using (var context = new RubberSoftEntities())
                {
                    context.spt_AddLog(logtype, logname, logdetail, userid, ipmachhine, machinename);
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
