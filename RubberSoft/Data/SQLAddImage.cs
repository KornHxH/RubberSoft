using DevExpress.XtraEditors;
using RubberSoft.Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RubberSoft.Data
{
    class SQLAddImage
    {
        public string ConnectionApp = ConfigurationManager.ConnectionStrings["ConnectionApp"].ConnectionString;

        public SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionApp"].ConnectionString);
        public const int TimeOut = 9000;
        private SqlCommand cmd = new SqlCommand();
        private readonly SqlDataAdapter DA = new SqlDataAdapter();
        //private DataTable dt = new DataTable();
        //private DataSet ds = new DataSet();
        //private SqlDataReader dr = null;
        private string sql = "";

        readonly SQLData SQLData = new SQLData();

        public DataSet Spt_GetBranchImg(int BranchId)
        {
            string sSql = @"SELECT BranchId, BranchImg FROM Branch WHERE BranchId=@BranchId";

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@BranchId", BranchId);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public Image LoadPic(DataRow dRow)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                Bitmap bmpImage = new Bitmap(Resources.NoImage);
                bmpImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] data;
                if (dRow.Table.Columns.Contains("BranchImg"))
                {
                    data = (byte[])dRow["BranchImg"];
                }
                else
                {
                    data = ms.GetBuffer();
                }

                return Image.FromStream(new MemoryStream((byte[])data));
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return null;
            }
        }

        //public byte[] loadImg(DataRow dRow)
        //{
        //    try
        //    {
        //        MemoryStream ms = new MemoryStream();
        //        Bitmap bmpImage = new Bitmap(Resources.NoImage);
        //        bmpImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
        //        byte[] data = ms.GetBuffer();

        //        if (dRow.Table.Columns.Contains("BranchImg"))
        //        {
        //            BranchImg = (byte[])dRow["BranchImg"];
        //        }
        //        else
        //        {
        //            BranchImg = data;
        //        }    

        //        return BranchImg;
        //    }
        //    catch (Exception ex)
        //    {
        //        XtraMessageBox.Show(ex.Message);
        //        return BranchImg;
        //    }
        //}

        public DataSet Spt_AddBranchImg(byte[] BranchImg)
        {
            string sSql = @"INSERT INTO Branch (BranchImg) VALUES (@BranchImg)";

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@BranchImg", BranchImg);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public DataSet Spt_UpdateBranchImg(int BranchId, byte[] BranchImg)
        {
            string sSql = @"UPDATE Branch SET BranchImg=@BranchImg WHERE BranchId=@BranchId";

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@BranchId", BranchId);
            param[1] = new SqlParameter("@BranchImg", BranchImg);

            return SQLData.SQLExcecuteDataSet(SQLData.ConnectionApp, CommandType.Text, sSql, param);
        }

        public bool AddBranchImg(PictureEdit img)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                Bitmap bmpImage = new Bitmap(img.Image);
                bmpImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] data = ms.GetBuffer();
                Spt_AddBranchImg(data);

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        public bool UpdateBranchImg(int BranchId, PictureEdit img)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                Bitmap bmpImage = new Bitmap(img.Image);
                bmpImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] data = ms.GetBuffer();
                Spt_UpdateBranchImg(BranchId, data);

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        public bool Add_BranchImg(PictureEdit img, int id)
        {
            try
            {
                sql = @"INSERT INTO ProductPic(ProductId, ProductImg) VALUES (@ProductId, @ProductImg)";

                cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ProductId", id);
                MemoryStream ms = new MemoryStream();
                Bitmap bmpImage = new Bitmap(img.Image);
                bmpImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] data = ms.GetBuffer();
                SqlParameter p = new SqlParameter("@ProductImg", SqlDbType.Image);
                p.Value = data;
                cmd.Parameters.Add(p);
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        public bool Update_Branch(int id, PictureEdit img)
        {
            try
            {
                sql = @"UPDATE ProductPic SET ProductImg=@ProductImg WHERE ProductId=@ProductId";

                cmd = new SqlCommand(sql, cn);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ProductId", id);
                MemoryStream ms = new MemoryStream();
                Bitmap bmpImage = new Bitmap(img.Image);
                bmpImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] data = ms.GetBuffer();
                SqlParameter p = new SqlParameter("@ProductImg", SqlDbType.Image);
                p.Value = data;
                cmd.Parameters.Add(p);
                cmd.ExecuteNonQuery();

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
