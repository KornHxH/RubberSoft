using DevExpress.DataAccess.Excel;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RubberSoft.Data
{
    class SQLData
    {
        public string ConnectionApp = ConfigurationManager.ConnectionStrings["ConnectionApp"].ConnectionString;

        public string ConnectionOdb = ConfigurationManager.ConnectionStrings["ConnectionOLEDB"].ConnectionString;

        public SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionApp"].ConnectionString);

        internal CultureInfo _cultureEnInfo = new CultureInfo("en-US");
        internal CultureInfo _cultureThInfo = new CultureInfo("th-TH");

        const int TimeOut = 9000;

        public string Token = "b14ca5898a4e4133bbce2ea2315a1916";
        public OpenFileDialog OpenFile = new OpenFileDialog();
        public SaveFileDialog SaveFile = new SaveFileDialog();
        public string Excelpath = Application.StartupPath + @"\Templet\Template.xlsx";

        public string ID = System.Configuration.ConfigurationManager.AppSettings["ID"];
        public string KEY = System.Configuration.ConfigurationManager.AppSettings["KEY"];

        public bool CheckConnection()
        {
            try
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();
                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        public bool CheckRubberSoftConnection()
        {
            try
            {
                using (RubberSoftEntities db = new RubberSoftEntities())
                {
                    bool check = db.Database.Exists();
                    if (check == false)
                    {
                        db.Database.Connection.Close();
                        return false;
                    }
                }

                return true;
            }
            catch (SqlException __SqlException)
            {
                XtraMessageBox.Show(__SqlException.Message);
                return false;
            }
        }

        #region "Sql Excecute"

        //---------------------------------------- connect   Access databases
        public DataSet OleDbExcecuteDataSet(string connectionStr, CommandType sqlComType, string sQry, OleDbParameter[] _params)
        {
            DataSet ds = new DataSet();
            using (OleDbConnection connection = new OleDbConnection(connectionStr))
            {
                OleDbDataAdapter da = new OleDbDataAdapter();
                if (_params != null)
                {
                    OleDbCommand command = new OleDbCommand(sQry, connection)
                    {
                        CommandType = sqlComType
                    };

                    foreach (OleDbParameter param in _params)
                    {
                        if (param != null)
                            command.Parameters.Add(param);
                    }

                    command.CommandTimeout = TimeOut;
                    da.SelectCommand = command;
                    command.Connection.Open();
                    da.Fill(ds);
                    command.Connection.Close();
                    command.Dispose();
                }

                return ds;
            }
        }

        public DataSet SQLExcecuteDataSet(string connectionStr, CommandType sqlComType, string sQry, SqlParameter[] _params)
        {
            DataSet ds = new DataSet();
            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                SqlCommand command = new SqlCommand(sQry, connection)
                {
                    CommandType = sqlComType
                };

                SqlDataAdapter da = new SqlDataAdapter();
                if (_params != null)
                {
                    foreach (SqlParameter param in _params)
                    {
                        if (param != null)
                            command.Parameters.Add(param);
                    }
                }

                command.CommandTimeout = TimeOut;
                da.SelectCommand = command;
                command.Connection.Open();
                da.Fill(ds);
                command.Connection.Close();
                command.Dispose();
            }
            return ds;
        }
        public Object SQLExcecuteScalar(string connectionStr, CommandType sqlComType, string sQry, SqlParameter[] _params)
        {
            Object _result = new Object();
            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                SqlCommand command = new SqlCommand(sQry, connection)
                {
                    CommandType = sqlComType
                };

                SqlDataAdapter da = new SqlDataAdapter();
                if (_params != null)
                {
                    foreach (SqlParameter param in _params)
                    {
                        if (param != null)
                            command.Parameters.Add(param);
                    }
                }
                command.CommandTimeout = TimeOut;
                da.SelectCommand = command;
                command.Connection.Open();
                _result = command.ExecuteScalar();
                command.Connection.Close();
                command.Dispose();
            }
            return _result;
        }

        public Object SQLExcecuteNonQuery(string connectionStr, CommandType sqlComType, string sQry, SqlParameter[] _params)
        {
            Object _result = new Object();
            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                SqlCommand command = new SqlCommand(sQry, connection)
                {
                    CommandType = sqlComType
                };

                SqlDataAdapter da = new SqlDataAdapter();
                if (_params != null)
                {
                    foreach (SqlParameter param in _params)
                    {
                        if (param != null)
                            command.Parameters.Add(param);
                    }
                }
                command.CommandTimeout = TimeOut;
                da.SelectCommand = command;
                command.Connection.Open();
                _result = command.ExecuteNonQuery();
                command.Connection.Close();
                command.Dispose();
            }
            return _result;
        }

        #endregion

        public string GetIP()
        {
            string strHostName = Dns.GetHostName();
            IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(strHostName);
            IPAddress[] addr = ipEntry.AddressList;
            return addr[addr.Length - 1].ToString();
        }

        public DataTable ToDataTable(ExcelDataSource excelDataSource)
        {
            IList list = ((IListSource)excelDataSource).GetList();
            DevExpress.DataAccess.Native.Excel.DataView dataView = (DevExpress.DataAccess.Native.Excel.DataView)list;
            List<DevExpress.DataAccess.Native.Excel.ViewColumn> props = dataView.Columns;

            DataTable table = new DataTable();
            for (int i = 0; i <= props.Count - 1; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            object[] values = new object[props.Count - 1 + 1];
            foreach (DevExpress.DataAccess.Native.Excel.ViewRow item in list)
            {
                for (int i = 0; i <= values.Length - 1; i++)
                    values[i] = props[i].GetValue(item);
                table.Rows.Add(values);
            }
            return table;
        }

        public void DeleteSelectedRows(GridView view)

        {
            if (view == null || view.SelectedRowsCount == 0) return;
            DataRow[] rows = new DataRow[view.SelectedRowsCount];
            for (int i = 0; i < view.SelectedRowsCount; i++)
                rows[i] = view.GetDataRow(view.GetSelectedRows()[i]);
            view.BeginSort();
            try
            {
                foreach (DataRow row in rows)
                    row.Delete();
            }
            finally
            {
                view.EndSort();
            }
        }


        public bool CloseFunc(DialogResult parameter)
        {
            return parameter != DialogResult.Cancel;
        }

        public void ShowGridPreview(GridControl grid)
        {
            // Check whether the GridControl can be previewed. 
            if (!grid.IsPrintingAvailable)
            {
                MessageBox.Show("The 'DevExpress.XtraPrinting' library is not found", "Error");
                return;
            }

            // Opens the Preview window. 
            grid.ShowPrintPreview();
        }

        public void PrintGrid(GridControl grid)
        {
            // Check whether the GridControl can be printed. 
            if (!grid.IsPrintingAvailable)
            {
                MessageBox.Show("The 'DevExpress.XtraPrinting' library is not found", "Error");
                return;
            }

            // Print. 
            grid.Print();
        }

        public void OpenExCel(GridControl _Grid, string Sheet, string path)
        {
            try
            {
                // Dim path As String = "rptPricelist.xlsx"
                ExcelDataSource myExcelSource = new ExcelDataSource
                {
                    FileName = path
                };

                ExcelWorksheetSettings worksheetSettings = new ExcelWorksheetSettings(Sheet);
                myExcelSource.SourceOptions = new ExcelSourceOptions(worksheetSettings)
                {
                    // or 
                    // myExcelSource.SourceOptions = New CsvSourceOptions() With {.CellRange = "A1:L100"}
                    SkipEmptyRows = false,
                    UseFirstRowAsHeader = true
                };

                myExcelSource.Fill();

                _Grid.DataSource = ToDataTable(myExcelSource);
                myExcelSource.Dispose();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        public void ExportExCel(GridControl _GridControl, GridView _Gridview, string Sheet)
        {
            try
            {
                // SaveFile.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
                SaveFile.Filter = "Excel files (*.xlsx)|*.xlsx|CSV Files (*.csv)|*.csv|XLS Files (*.xls)|*xls";
                string path = "";
                if (SaveFile.ShowDialog() == DialogResult.OK)
                {
                    FileInfo fi = new FileInfo(SaveFile.FileName);
                    string FileName = SaveFile.FileName;
                    path = fi.FullName;

                    // Customize export options 
                    (_GridControl.MainView as GridView).OptionsPrint.PrintHeader = true;
                    XlsxExportOptionsEx advOptions = new XlsxExportOptionsEx
                    {
                        AllowGrouping = DevExpress.Utils.DefaultBoolean.False,
                        ShowTotalSummaries = DevExpress.Utils.DefaultBoolean.False,
                        SheetName = Sheet
                    };

                    _Gridview.ExportToXlsx(path, advOptions);
                    // Open the created XLSX file with the default application. 
                    Process.Start(path).Dispose();
                }
                else
                    return;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        public void ExpandAllRows(GridView View)
        {
            View.BeginUpdate();
            try
            {
                int dataRowCount = View.DataRowCount;
                for (int rHandle = 0; rHandle < dataRowCount; rHandle++)
                    View.SetMasterRowExpanded(rHandle, true);
            }
            finally
            {
                View.EndUpdate();
            }

        }

        #region "Encrypt and Decrypt"

        public string Encrypt(string password)
        {
            byte[] encode = new byte[password.Length - 1 + 1];
            encode = Encoding.UTF8.GetBytes(password);
            string strmsg = Convert.ToBase64String(encode);

            return strmsg;
        }

        public string Decrypt(string encryptpwd)
        {
            UTF8Encoding encodepwd = new UTF8Encoding();
            Decoder Decode = encodepwd.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encryptpwd);

            int charCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount - 1 + 1];
            Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string decryptpwd = new string(decoded_char);

            return decryptpwd;
        }

        public string EncryptString(string plainText)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(Token);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }

        public string DecryptString(string cipherText)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(cipherText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(Token);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }

        #endregion

    }
}
