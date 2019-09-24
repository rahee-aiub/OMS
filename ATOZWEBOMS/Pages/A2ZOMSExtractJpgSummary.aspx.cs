using ATOZWEBOMS.WebSessionStore;
using DataAccessLayer.DTO.CustomerServices;
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

namespace ATOZWEBOMS.Pages
{
    public partial class A2ZOMSExtractJpgSummary : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));
                OrderNoDropdown();
                dvJpgTable.Visible = false;
            }
        }

        private void OrderNoDropdown()
        {
            string sqlquery = "SELECT OrderNo,OrderNo FROM A2ZORDERDETAILS WHERE OrderStatus != 99";
            ddlOrderNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlOrderNo, "A2ZACOMS");
        }

        protected void showimage()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(DataAccessLayer.Constants.DBConstants.GetConnectionString("A2ZACOMS")))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand("SELECT ItemImage  FROM A2ZORDERDETAILS WHERE OrderNo='" + ddlOrderNo.SelectedValue + "'", conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            byte[] picData = reader["ItemImage"] as byte[] ?? null;

                            if (picData != null)
                            {
                                using (MemoryStream ms = new MemoryStream(picData))
                                {
                                    string base64String = Convert.ToBase64String(picData, 0, picData.Length);
                                    ImgPicture.ImageUrl = "data:image/png;base64," + base64String;
                                }
                            }
                        }
                        else
                        {

                            ImgPicture.ImageUrl = "~/Images/BlankImage.jpg";

                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('No Image');", true);
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnSaveImage_Click(object sender, EventArgs e)
        {

            string base64 = Request.Form[hfImageData.UniqueID].Split(',')[1];
            byte[] bytes = Convert.FromBase64String(base64);
            Response.Clear();
            Response.ContentType = "image/jpg";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + ddlOrderNo.SelectedValue + ".jpg");
            // Response.TransmitFile(Server.MapPath("./Pic/" + ddlOrderNo.SelectedValue + ".jpg"));

            Response.Buffer = true;
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.BinaryWrite(bytes);
            Response.End();
        }

        protected void ddlOrderNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlOrderNo.SelectedIndex != 0)
            {

                var prm = new object[1];

                prm[0] = ddlOrderNo.SelectedValue;

                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableBySpWithParams("Sp_GetOrderInformationForJPG", prm, "A2ZACOMS");

                var p = new A2ZPARTYDTO();
                if (dt.Rows.Count > 0)
                {
                    dvJpgTable.Visible = true;

                    lblOrderNo.Text = Converter.GetString(dt.Rows[0]["OrderNo"]);

                    DateTime Odate = Converter.GetDateTime(dt.Rows[0]["OrderDate"]);
                    lblOrderDate.Text = Odate.ToString("dd/MM/yyyy");

                    lblOrderPartyJPG.Text = Converter.GetString(dt.Rows[0]["OrderParty"]);

                   // lblItemNameJPG.Text = Converter.GetString(dt.Rows[0]["ItemName"]);
                    lblSizeJPG.Text = Converter.GetString(dt.Rows[0]["ItemSize"]) + " (" + Converter.GetString(dt.Rows[0]["ItemPiece"]) + " PCS)";
                    lblColorJPG.Text = Converter.GetString(dt.Rows[0]["ItemColor"]);
                   // lblWideJPG.Text = Converter.GetString(dt.Rows[0]["ItemWide"]);
                    lblPerPcsWeight.Text = Converter.GetString(dt.Rows[0]["ItemWeight"]) + " gm";
                    lblTotalWeight.Text = Converter.GetString(dt.Rows[0]["ItemTotalWeight"]) + " gm";

                    int FPartyCode = Converter.GetInteger(dt.Rows[0]["FactoryParty"]);

                    A2ZPARTYDTO getDTO2 = (A2ZPARTYDTO.GetPartyInformation(FPartyCode));                 
                    DateTime Pdate = Converter.GetDateTime(dt.Rows[0]["DeliveryPossibleDate"]);
                 
                    Pdate = Converter.GetDateTime(dt.Rows[0]["FactoryReceiveDate"]);

                    showimage();
                }
                else
                {
                    dvJpgTable.Visible = false;

                }
            }
            else
            {
                dvJpgTable.Visible = false;

            }
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }

       
     
    }
}