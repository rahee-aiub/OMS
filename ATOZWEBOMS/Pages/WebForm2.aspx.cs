using DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ATOZWEBGMS.Pages
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {

                SqlConnection strCon = new SqlConnection(DataAccessLayer.Constants.DBConstants.GetConnectionString("A2ZACOMS"));
                string ImageName = string.Empty;
                byte[] Image = null;


                if (FileUpload1.PostedFile != null && FileUpload1.FileName != "")
                {
                    ImageName = Path.GetFileName(FileUpload1.FileName);
                    Image = new byte[FileUpload1.PostedFile.ContentLength];
                    HttpPostedFile UploadedImage = FileUpload1.PostedFile;
                    UploadedImage.InputStream.Read(Image, 0, (int)FileUpload1.PostedFile.ContentLength);
                }



                using (SqlCommand cmd = new SqlCommand())
                {
                    strCon.Open();
                    cmd.Connection = strCon;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_UploadItemImage";
                    cmd.Parameters.Add(new SqlParameter("@pvchAction", SqlDbType.VarChar, 50));
                    cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@ItemImage", SqlDbType.Image));
                    cmd.Parameters.Add("@pIntErrDescOut", SqlDbType.Int).Direction = ParameterDirection.Output;

                    cmd.Parameters["@pvchAction"].Value = "insert";
                    cmd.Parameters["@UserID"].Value = Converter.GetInteger(lblID.Text);
                    cmd.Parameters["@ItemImage"].Value = Image;

                    cmd.ExecuteNonQuery();
                    int retVal = (int)cmd.Parameters["@pIntErrDescOut"].Value;



                }
            }

            catch (Exception ex)
            {

            }
        }

    }
}