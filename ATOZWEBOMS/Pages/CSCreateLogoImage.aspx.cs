using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer.Utility;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.DTO.HouseKeeping;
using ATOZWEBOMS.WebSessionStore;

namespace ATOZWEBOMS.Pages
{
    public partial class CSCreateLogoImage : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(DataAccessLayer.Constants.DBConstants.GetConnectionString("A2ZACOMS"));
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                ScriptManager.GetCurrent(this).RegisterPostBackControl(this.ibtnUpload);
                ScriptManager.GetCurrent(this).RegisterPostBackControl(this.BtnUpdate);
                if (!IsPostBack)
                {
                    
                    //lblBranchNo.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.BRNO));
                    //var p = A2ZERPSYSPRMDTO.GetParameterValue();
                    //lblBranchNo.Text = Converter.GetString(p.PrmUnitNo);


                    string Ctrflag = (string)Session["flag"];
                    string Nflag = (string)Session["NFlag"];
                    //lblFlag.Text = Ctrflag;

                  
                    BtnUpdate.Visible = false;






                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.Page_Load Problem');</script>");
                //throw ex;
            }
        }

        protected void SessionRemove1()
        {
            Session["RCreditUNo"] = string.Empty;
            Session["RMemNo"] = string.Empty;
            Session["flag"] = string.Empty;
            Session["NFlag"] = string.Empty;

        }
        protected void ibtnUpload_Click(object sender, ImageClickEventArgs e)
        {
            try
            {

                if (FileUpload1.HasFile)
                {
                    if (IsImageFile((HttpPostedFile)FileUpload1.PostedFile))
                    {

                        SqlCommand cmd = new SqlCommand("UpdateLogoimage", con);
                        cmd.Parameters.AddWithValue("@Image", FileUpload1.FileBytes);

                       
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        int result = cmd.ExecuteNonQuery();
                        if (result == 1)
                        {
                            string filename = FileUpload1.FileName;
                            FileUpload1.SaveAs(Server.MapPath("~/Profile Pic/") + filename);
                            ImgPicture.ImageUrl = "~/Profile Pic/" + filename;
                            //txtCuNo.Text = string.Empty;
                            //txtMemNo.Text = string.Empty;
                            //ddlCreditUNo.SelectedValue = "-Select-";
                            //ddlMemNo.SelectedValue = "-Select-";
                           
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.ibtnUpload_Click Problem');</script>");
                //throw ex;
            }
        }

        private void CheckCU()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('This Credit Union already upload image..');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);

            //}
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('This Credit Union already upload image..');", true);
            return;


        }

        protected bool IsImageFile(HttpPostedFile httpPostedFile)
        {
            bool isImage = false;
            string fullPath = Server.MapPath("~/Profile Pic/" + FileUpload1.FileName);
            FileUpload1.SaveAs(fullPath);
            ImgPicture.ImageUrl = "~/Profile Pic/@" + FileUpload1.FileName;
            System.IO.FileStream fs = new System.IO.FileStream(fullPath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            System.IO.BinaryReader br = new System.IO.BinaryReader(fs);
            string fileclass = "";
            byte buffer = br.ReadByte();
            fileclass = buffer.ToString();
            buffer = br.ReadByte();
            fileclass += buffer.ToString();
            br.Close();
            fs.Close();

            // only allow images    jpg       gif     bmp     png      
            String[] fileType = { "255216", "7173", "6677", "13780" };
            for (int i = 0; i < fileType.Length; i++)
            {
                if (fileclass == fileType[i])
                {
                    isImage = true;
                    break;
                }
            }
            return isImage;

        }

        protected void ibtnDelete_Click(object sender, ImageClickEventArgs e)
        {
            ImgPicture.ImageUrl = "~/Images/index.jpg";
        }


       

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }

        protected void BtnUpdate_Click(object sender, ImageClickEventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                if (IsImageFile((HttpPostedFile)FileUpload1.PostedFile))
                {

                    SqlCommand cmd = new SqlCommand("UpdateLogoimage", con);
                    cmd.Parameters.AddWithValue("@Image", FileUpload1.FileBytes);
                    
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    int result = cmd.ExecuteNonQuery();
                    if (result == 1)
                    {
                        string filename = FileUpload1.FileName;
                        FileUpload1.SaveAs(Server.MapPath("~/Profile Pic") + filename);
                        ImgPicture.ImageUrl = "~/Profile Pic" + filename;

                        
                        ibtnUpload.Visible = true;
                        BtnUpdate.Visible = false;

                    }

                }

            }
        }


    }
}