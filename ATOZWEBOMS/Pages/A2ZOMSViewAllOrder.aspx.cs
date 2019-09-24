using ATOZWEBOMS.WebSessionStore;
using DataAccessLayer.BLL;
using DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.DTO.SystemControl;
using DataAccessLayer.DTO.HouseKeeping;
using System.Drawing;
using System.Data.SqlClient;
using System.IO;

namespace ATOZWEBOMS.Pages
{
    public partial class A2ZOMSViewAllOrder : System.Web.UI.Page
    {
      
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                   
                    lblID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));
                    lblIDName.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_NAME));

                    A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                    DateTime dt1 = Converter.GetDateTime(dto.ProcessDate);
                    string date = dt1.ToString("dd/MM/yyyy");
                    lblProcDate.Text = Converter.GetString(date);

                    LoadGridViewImage();
                  
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.Page_Load Problem');</script>");
            }
        }

        protected void RemoveSession()
        {
            Session["ProgFlag"] = string.Empty;
            Session["SlblModule"] = string.Empty;
            Session["SlblBranchNo"] = string.Empty;
        }

      

        private void LoadGridViewImage2()
        {
            string sqlquery = "SELECT OrderNo1,ItemName1,ItemImage1,OrderNo2,ItemName2,ItemImage2,OrderNo3,ItemName3,ItemImage3,OrderNo4,ItemName4,ItemImage4,OrderNo5,ItemName5,ItemImage5 FROM WFA2ZORDERIMAGEVIEW";
            gvDetailInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery, gvDetailInfo, "A2ZACOMS");
        }

        private void LoadGridViewImage()
        {
            string sqlquery = "SELECT A2ZPARTYCODE.PartyName,OrderNo,ItemName,ItemSize,ItemTotalWeight,ItemImage FROM A2ZORDERDETAILS LEFT OUTER JOIN A2ZPARTYCODE ON A2ZORDERDETAILS.OrderParty = A2ZPARTYCODE.PartyCode";
            gvDetailInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery, gvDetailInfo, "A2ZACOMS");
        }
     

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            RemoveSession();
            Response.Redirect("A2ZERPModule.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

      
      


        //protected void showimage()
        //{
        //    try
        //    {
        //        using (SqlConnection conn = new SqlConnection(DataAccessLayer.Constants.DBConstants.GetConnectionString("A2ZACOMS")))
        //        {
        //            conn.Open();

        //            using (SqlCommand cmd = new SqlCommand("SELECT ItemImage  FROM A2ZORDERDETAILS WHERE OrderNo='" + lblOrderNo.Text + "'", conn))
        //            using (SqlDataReader reader = cmd.ExecuteReader())
        //            {
        //                if (reader.Read())
        //                {
        //                    byte[] picData = reader["ItemImage"] as byte[] ?? null;

        //                    if (picData != null)
        //                    {
        //                        using (MemoryStream ms = new MemoryStream(picData))
        //                        {
        //                            string base64String = Convert.ToBase64String(picData, 0, picData.Length);
        //                            ImgPicture.ImageUrl = "data:image/png;base64," + base64String;
        //                        }
        //                    }
        //                }
        //                else
        //                {

        //                    ImgPicture.ImageUrl = "~/Images/BlankImage.jpg";

        //                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('No Image');", true);
        //                    return;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        protected void BtnDetails_Click(object sender, EventArgs e)
        {
            try
            {

                Button b = (Button)sender;
                GridViewRow r = (GridViewRow)b.NamingContainer;

                Label OrderNo = (Label)gvDetailInfo.Rows[r.RowIndex].Cells[2].FindControl("lblOrderNo");

                Session["OrderNo"] = OrderNo.Text;
                Response.Redirect("A2ZOMSSingleOrderDetails.aspx");
            

            }

            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnEdit_Click Problem');</script>");
                //throw ex;
            }

        }

      
        protected void Image1_Click(object sender, ImageClickEventArgs e)
        {
            try
            {

                ImageButton b = (ImageButton)sender;
                GridViewRow r = (GridViewRow)b.NamingContainer;

                Label OrderNo = (Label)gvDetailInfo.Rows[r.RowIndex].Cells[2].FindControl("lblOrderNo");

                Session["OrderNo"] = OrderNo.Text;
                Response.Redirect("A2ZOMSSingleOrderDetails.aspx");


            }

            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnEdit_Click Problem');</script>");
                //throw ex;
            }

        }

        

    }
}