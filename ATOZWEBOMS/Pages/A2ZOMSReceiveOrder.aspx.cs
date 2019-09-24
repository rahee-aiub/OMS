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
    public partial class A2ZOMSReceiveOrder : System.Web.UI.Page
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

                    LoadReceiveGridSpooler();

                    dvJpgTable.Visible = false;

                    FactoryPartyDropdown();
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


        private void FactoryPartyDropdown()
        {
            string sqlquery = "SELECT PartyCode,PartyName from A2ZPARTYCODE WHERE GroupCode = 11";
            ddlFactoryParty = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlFactoryParty, "A2ZACOMS");
        }

        private void LoadReceiveGridSpooler()
        {
            gvOrderInfo.Visible = true;


            //string sqlquery = "SELECT OrderNo,OrderDate,ItemName,ItemKarat,ItemPiece,ItemTotalWeight,A2ZPARTYCODE.PartyName,FactoryParty,OrderStatus,OrderStatusDesc FROM A2ZORDERDETAILS inner join A2ZPARTYCODE on A2ZPARTYCODE.PartyCode=A2ZORDERDETAILS.OrderParty WHERE OrderStatus = 12 OR OrderStatus = 14";
            string sqlquery = "SELECT OrderNo,OrderDate,ItemName,ItemKarat,ItemPiece,ItemTotalWeight,A2ZPARTYCODE.PartyName,A2ZPARTYCODE_1.PartyName as FactoryName,OrderStatus,OrderStatusDesc FROM A2ZORDERDETAILS LEFT OUTER JOIN A2ZPARTYCODE on A2ZPARTYCODE.PartyCode=A2ZORDERDETAILS.OrderParty LEFT OUTER JOIN dbo.A2ZPARTYCODE AS A2ZPARTYCODE_1 ON dbo.A2ZORDERDETAILS.FactoryParty = A2ZPARTYCODE_1.PartyCode WHERE OrderStatus = 12 OR OrderStatus = 14";
            gvOrderInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery, gvOrderInfo, "A2ZACOMS");
        }
     
        

   
        protected void BtnSelect_Click(object sender, EventArgs e)
        {
            try
            {
           
                    Button b = (Button)sender;
                    GridViewRow r = (GridViewRow)b.NamingContainer;

                    Label OrderNo = (Label)gvOrderInfo.Rows[r.RowIndex].Cells[2].FindControl("lblOrderNo");
                    



                    //string qry1 = "SELECT * FROM A2ZORDERDETAILS WHERE OrderNo = '"+OrderNo.Text+"'";
                    //DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZACOMS");

                    //if (dt1.Rows.Count > 0)
                    //{
                        var prm = new object[1];

                        prm[0] = OrderNo.Text;

                        DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableBySpWithParams("Sp_GetOrderInformation", prm, "A2ZACOMS");

                        var p = new A2ZPARTYDTO();
                        if (dt.Rows.Count > 0)
                        {
                            dvJpgTable.Visible = true;

                            lblOrderNo.Text = Converter.GetString(dt.Rows[0]["OrderNo"]);

                            DateTime Odate = Converter.GetDateTime(dt.Rows[0]["OrderDate"]);
                            lblOrderDate.Text = Odate.ToString("dd/MM/yyyy");

                            lblOrderPartyJPG.Text = Converter.GetString(dt.Rows[0]["OrderParty"]);

                            int PartyCode = Converter.GetInteger(lblOrderPartyJPG.Text);
                            A2ZPARTYDTO getDTO = (A2ZPARTYDTO.GetPartyInformation(PartyCode));

                            if (getDTO.PartyCode > 0)
                            {
                                lblOrderPartyName.Text = Converter.GetString(getDTO.PartyName);
                            }


                            if (Converter.GetString(dt.Rows[0]["WayToOrder"]) == "1")
                            {
                                lblWayToOrderJPG.Text = "Whatsapp";
                            }
                            if (Converter.GetString(dt.Rows[0]["WayToOrder"]) == "2")
                            {
                                lblWayToOrderJPG.Text = "Email";
                            }

                            DateTime Pdate = Converter.GetDateTime(dt.Rows[0]["DeliveryPossibleDate"]);
                            lblDeliverypossibleDateJPG.Text = Pdate.ToString("dd/MM/yyyy");

                            lblItemNameJPG.Text = Converter.GetString(dt.Rows[0]["ItemName"]);
                            lblItemKaratJPG.Text = Converter.GetString(dt.Rows[0]["ItemKarat"]);
                            lblSizeJPG.Text = Converter.GetString(dt.Rows[0]["ItemSize"]);
                            lblColorJPG.Text = Converter.GetString(dt.Rows[0]["ItemColor"]);
                            lblLengthJPG.Text = Converter.GetString(dt.Rows[0]["ItemLength"]);
                            lblPieceJPG.Text = Converter.GetString(dt.Rows[0]["ItemPiece"]);
                            lblWeightPerPieceJPG.Text = Converter.GetString(dt.Rows[0]["ItemWeight"]);
                            lblTotalWeightJPG.Text = Converter.GetString(dt.Rows[0]["ItemTotalWeight"]);
                            lblWideJPG.Text = Converter.GetString(dt.Rows[0]["ItemWide"]);
                            lblWhoseOrderJPG.Text = Converter.GetString(dt.Rows[0]["WhoseOrder"]);
                            lblWhoseOrderPhoneNoJPG.Text = Converter.GetString(dt.Rows[0]["WhoseOrderPhone"]);

                            ddlFactoryParty.SelectedValue = Converter.GetString(dt.Rows[0]["FactoryParty"]);

                            showimage();
                        }
                        else
                        {
                            dvJpgTable.Visible = false;

                        }
                    //}

                }
            
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnEdit_Click Problem');</script>");
                //throw ex;
            }

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

        protected void btnReceive_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtWhomReceive.Text == string.Empty)
                {
                    txtWhomReceive.Focus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Whom Receive');", true);
                    return;
                }

                //if (txtWhomPhone.Text == string.Empty)
                //{
                //    txtWhomPhone.Focus();
                //    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Whom Phone No.');", true);
                //    return;
                //}

                //if (txtRcvOrderDesc.Text == string.Empty)
                //{
                //    txtRcvOrderDesc.Focus();
                //    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Note');", true);
                //    return;
                //}


                var prm = new object[6];

                prm[0] = lblOrderNo.Text;
                prm[1] = txtRcvOrderDesc.Text;
                prm[2] = "15";//OrderStatus
                prm[3] = "Receive Order";//OrderStatusDesc;
                prm[4] = txtWhomReceive.Text;
                prm[5] = txtWhomPhone.Text;

                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_OrderReceive", prm, "A2ZACOMS"));
                if (result == 0)
                {
                    Response.Redirect(Request.RawUrl);
                }

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

       

        protected void showimage()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(DataAccessLayer.Constants.DBConstants.GetConnectionString("A2ZACOMS")))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand("SELECT ItemImage  FROM A2ZORDERDETAILS WHERE OrderNo='" + lblOrderNo.Text + "'", conn))
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


    }
}