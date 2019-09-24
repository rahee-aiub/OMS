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
    public partial class A2ZOMSExtractJpg : System.Web.UI.Page
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


                    if (rbtWithParty.Checked == true)
                    {
                        int PartyCode = Converter.GetInteger(lblOrderPartyJPG.Text);

                        A2ZPARTYDTO getDTO = (A2ZPARTYDTO.GetPartyInformation(PartyCode));
                        if (getDTO.PartyCode > 0)
                        {
                            lblOrderPartyName.Text = Converter.GetString(getDTO.PartyName);
                        }
                    }
                    else
                    {
                        lblOrderPartyName.Text = string.Empty;
                    }


                    int FPartyCode = Converter.GetInteger(dt.Rows[0]["FactoryParty"]);

                    A2ZPARTYDTO getDTO2 = (A2ZPARTYDTO.GetPartyInformation(FPartyCode));
                    if (getDTO2.PartyCode > 0)
                    {
                        lblFactoryName.Text = Converter.GetString(getDTO2.PartyName);
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
                    lblSizeJPG.Text = Converter.GetString(dt.Rows[0]["ItemSize"]);
                    lblColorJPG.Text = Converter.GetString(dt.Rows[0]["ItemColor"]);
                    lblPieceJPG.Text = Converter.GetString(dt.Rows[0]["ItemPiece"]);
                    lblWideJPG.Text = Converter.GetString(dt.Rows[0]["ItemWide"]);
                    lblWayToOrderPhoneJPG.Text = Converter.GetString(dt.Rows[0]["WayToOrderNo"]);
                    lblPendingDays.Text = Converter.GetString(dt.Rows[0]["PendingDay"] + " Day");

                    lblDeliverypossibleDateJPG.Text = Pdate.ToString("dd/MM/yyyy");


                    Pdate = Converter.GetDateTime(dt.Rows[0]["FactoryReceiveDate"]);


                    if (Pdate == DateTime.MinValue)
                    {
                        lblFactoryReceiveStatus.Text = "  Not Received  ";
                        lblFactoryReceiveStatusDate.Text = "N/A";
                    }
                    else
                    {
                        lblFactoryReceiveStatus.Text = "  Received  ";
                        lblFactoryReceiveStatusDate.Text = Pdate.ToString("dd/MM/yyyy");
                    }



                    int FactoryReceivePendingDay = Converter.GetInteger(dt.Rows[0]["FactoryReceivePendingDay"]);
                    if (FactoryReceivePendingDay < 0)
                    {

                        lblFactoryReceiveStatusMode.Text = "Delay : " + Math.Abs(FactoryReceivePendingDay) + " Days";
                    }
                    else
                    {
                        lblFactoryReceiveStatusMode.Text = "Pending : " + Math.Abs(FactoryReceivePendingDay) + " Days";

                    }
                    Pdate = Converter.GetDateTime(dt.Rows[0]["ReadyInFactoryDate"]);


                    if (Pdate == DateTime.MinValue)
                    {
                        lblFactoryStatus.Text = "  Not Ready ";
                        lblFactoryStatusDate.Text = "N/A";
                    }
                    else
                    {
                        lblFactoryStatus.Text = "  Ready ";
                        lblFactoryStatusDate.Text = Pdate.ToString("dd/MM/yyyy");
                    }

                    int FactoryPendingDay = Converter.GetInteger(dt.Rows[0]["FactoryPendingDay"]);
                    if (FactoryPendingDay < 0)
                    {

                        lblFactoryStatusMode.Text = "Delay : " + Math.Abs(FactoryPendingDay) + " Days";
                    }
                    else
                    {
                        lblFactoryStatusMode.Text = "Pending : " + Math.Abs(FactoryPendingDay) + " Days";

                    }


                    Pdate = Converter.GetDateTime(dt.Rows[0]["SendToFactoryDate"]);


                    Pdate = Converter.GetDateTime(dt.Rows[0]["SendToTransitDate"]);
                    if (Pdate == DateTime.MinValue)
                    {
                        lblTransitStatus.Text = "  Not in Transit  ";
                        lblTransitStatusDate.Text = "N/A";
                    }
                    else
                    {
                        lblTransitStatus.Text = "  Send to Transit  ";
                        lblTransitStatusDate.Text = Pdate.ToString("dd/MM/yyyy");
                    }

                    int TransitPendingDay = Converter.GetInteger(dt.Rows[0]["TransitPendingDay"]);

                    if (TransitPendingDay < 0)
                    {

                        lblTransitStatusMode.Text = "Delay : " + Math.Abs(TransitPendingDay) + " Days";
                    }
                    else
                    {
                        lblTransitStatusMode.Text = "Pending : " + Math.Abs(TransitPendingDay) + " Days";
                    }


                    Pdate = Converter.GetDateTime(dt.Rows[0]["ReceiveFromTransitDate"]);
                    if (Pdate == DateTime.MinValue)
                    {
                        lblReceiveStatus.Text = "  Not Received  ";
                        lblReceiveStatusDate.Text = "N/A";
                    }
                    else
                    {
                        lblReceiveStatus.Text = "  Received  ";
                        lblReceiveStatusDate.Text = Pdate.ToString("dd/MM/yyyy");
                    }

                    int ReceiveDay = Converter.GetInteger(dt.Rows[0]["ReceivePendingDay"]);

                    if (TransitPendingDay < 0)
                    {

                        lblReceiveStatusMode.Text = "Delay : " + Math.Abs(ReceiveDay) + " Days";
                    }
                    else
                    {
                        lblReceiveStatusMode.Text = "Pending : " + Math.Abs(ReceiveDay) + " Days";
                    }

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

        protected void rbtWithParty_CheckedChanged(object sender, EventArgs e)
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


                if (rbtWithParty.Checked == true)
                {
                    int PartyCode = Converter.GetInteger(lblOrderPartyJPG.Text);

                    A2ZPARTYDTO getDTO = (A2ZPARTYDTO.GetPartyInformation(PartyCode));
                    if (getDTO.PartyCode > 0)
                    {
                        lblOrderPartyName.Text = Converter.GetString(getDTO.PartyName);
                    }
                }
                else
                {
                    lblOrderPartyName.Text = string.Empty;
                }


                int FPartyCode = Converter.GetInteger(dt.Rows[0]["FactoryParty"]);

                A2ZPARTYDTO getDTO2 = (A2ZPARTYDTO.GetPartyInformation(FPartyCode));
                if (getDTO2.PartyCode > 0)
                {
                    lblFactoryName.Text = Converter.GetString(getDTO2.PartyName);
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
                lblSizeJPG.Text = Converter.GetString(dt.Rows[0]["ItemSize"]);
                lblColorJPG.Text = Converter.GetString(dt.Rows[0]["ItemColor"]);
                lblPieceJPG.Text = Converter.GetString(dt.Rows[0]["ItemPiece"]);
                lblWideJPG.Text = Converter.GetString(dt.Rows[0]["ItemWide"]);
                lblWayToOrderPhoneJPG.Text = Converter.GetString(dt.Rows[0]["WayToOrderNo"]);
                lblPendingDays.Text = Converter.GetString(dt.Rows[0]["PendingDay"] + " Day");

                lblDeliverypossibleDateJPG.Text = Pdate.ToString("dd/MM/yyyy");


                Pdate = Converter.GetDateTime(dt.Rows[0]["FactoryReceiveDate"]);


                if (Pdate == DateTime.MinValue)
                {
                    lblFactoryReceiveStatus.Text = "  Not Received  ";
                    lblFactoryReceiveStatusDate.Text = "N/A";
                }
                else
                {
                    lblFactoryReceiveStatus.Text = "  Received  ";
                    lblFactoryReceiveStatusDate.Text = Pdate.ToString("dd/MM/yyyy");
                }



                int FactoryReceivePendingDay = Converter.GetInteger(dt.Rows[0]["FactoryReceivePendingDay"]);
                if (FactoryReceivePendingDay < 0)
                {

                    lblFactoryReceiveStatusMode.Text = "Delay : " + Math.Abs(FactoryReceivePendingDay) + " Days";
                }
                else
                {
                    lblFactoryReceiveStatusMode.Text = "Pending : " + Math.Abs(FactoryReceivePendingDay) + " Days";

                }
                Pdate = Converter.GetDateTime(dt.Rows[0]["ReadyInFactoryDate"]);


                if (Pdate == DateTime.MinValue)
                {
                    lblFactoryStatus.Text = "  Not Ready ";
                    lblFactoryStatusDate.Text = "N/A";
                }
                else
                {
                    lblFactoryStatus.Text = "  Ready ";
                    lblFactoryStatusDate.Text = Pdate.ToString("dd/MM/yyyy");
                }

                int FactoryPendingDay = Converter.GetInteger(dt.Rows[0]["FactoryPendingDay"]);
                if (FactoryPendingDay < 0)
                {

                    lblFactoryStatusMode.Text = "Delay : " + Math.Abs(FactoryPendingDay) + " Days";
                }
                else
                {
                    lblFactoryStatusMode.Text = "Pending : " + Math.Abs(FactoryPendingDay) + " Days";

                }


                Pdate = Converter.GetDateTime(dt.Rows[0]["SendToFactoryDate"]);


                Pdate = Converter.GetDateTime(dt.Rows[0]["SendToTransitDate"]);
                if (Pdate == DateTime.MinValue)
                {
                    lblTransitStatus.Text = "  Not in Transit  ";
                    lblTransitStatusDate.Text = "N/A";
                }
                else
                {
                    lblTransitStatus.Text = "  Send to Transit  ";
                    lblTransitStatusDate.Text = Pdate.ToString("dd/MM/yyyy");
                }

                int TransitPendingDay = Converter.GetInteger(dt.Rows[0]["TransitPendingDay"]);

                if (TransitPendingDay < 0)
                {

                    lblTransitStatusMode.Text = "Delay : " + Math.Abs(TransitPendingDay) + " Days";
                }
                else
                {
                    lblTransitStatusMode.Text = "Pending : " + Math.Abs(TransitPendingDay) + " Days";
                }


                Pdate = Converter.GetDateTime(dt.Rows[0]["ReceiveFromTransitDate"]);
                if (Pdate == DateTime.MinValue)
                {
                    lblReceiveStatus.Text = "  Not Received  ";
                    lblReceiveStatusDate.Text = "N/A";
                }
                else
                {
                    lblReceiveStatus.Text = "  Received  ";
                    lblReceiveStatusDate.Text = Pdate.ToString("dd/MM/yyyy");
                }

                int ReceiveDay = Converter.GetInteger(dt.Rows[0]["ReceivePendingDay"]);

                if (TransitPendingDay < 0)
                {

                    lblReceiveStatusMode.Text = "Delay : " + Math.Abs(ReceiveDay) + " Days";
                }
                else
                {
                    lblReceiveStatusMode.Text = "Pending : " + Math.Abs(ReceiveDay) + " Days";
                }

                showimage();
            }
            else
            {
                dvJpgTable.Visible = false;

            }

        }

        protected void rbtWithoutParty_CheckedChanged(object sender, EventArgs e)
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


                if (rbtWithParty.Checked == true)
                {
                    int PartyCode = Converter.GetInteger(lblOrderPartyJPG.Text);

                    A2ZPARTYDTO getDTO = (A2ZPARTYDTO.GetPartyInformation(PartyCode));
                    if (getDTO.PartyCode > 0)
                    {
                        lblOrderPartyName.Text = Converter.GetString(getDTO.PartyName);
                    }
                }
                else
                {
                    lblOrderPartyName.Text = string.Empty;
                }


                int FPartyCode = Converter.GetInteger(dt.Rows[0]["FactoryParty"]);

                A2ZPARTYDTO getDTO2 = (A2ZPARTYDTO.GetPartyInformation(FPartyCode));
                if (getDTO2.PartyCode > 0)
                {
                    lblFactoryName.Text = Converter.GetString(getDTO2.PartyName);
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
                lblSizeJPG.Text = Converter.GetString(dt.Rows[0]["ItemSize"]);
                lblColorJPG.Text = Converter.GetString(dt.Rows[0]["ItemColor"]);
                lblPieceJPG.Text = Converter.GetString(dt.Rows[0]["ItemPiece"]);
                lblWideJPG.Text = Converter.GetString(dt.Rows[0]["ItemWide"]);
                lblWayToOrderPhoneJPG.Text = Converter.GetString(dt.Rows[0]["WayToOrderNo"]);
                lblPendingDays.Text = Converter.GetString(dt.Rows[0]["PendingDay"] + " Day");

                lblDeliverypossibleDateJPG.Text = Pdate.ToString("dd/MM/yyyy");


                Pdate = Converter.GetDateTime(dt.Rows[0]["FactoryReceiveDate"]);


                if (Pdate == DateTime.MinValue)
                {
                    lblFactoryReceiveStatus.Text = "  Not Received  ";
                    lblFactoryReceiveStatusDate.Text = "N/A";
                }
                else
                {
                    lblFactoryReceiveStatus.Text = "  Received  ";
                    lblFactoryReceiveStatusDate.Text = Pdate.ToString("dd/MM/yyyy");
                }



                int FactoryReceivePendingDay = Converter.GetInteger(dt.Rows[0]["FactoryReceivePendingDay"]);
                if (FactoryReceivePendingDay < 0)
                {

                    lblFactoryReceiveStatusMode.Text = "Delay : " + Math.Abs(FactoryReceivePendingDay) + " Days";
                }
                else
                {
                    lblFactoryReceiveStatusMode.Text = "Pending : " + Math.Abs(FactoryReceivePendingDay) + " Days";

                }
                Pdate = Converter.GetDateTime(dt.Rows[0]["ReadyInFactoryDate"]);


                if (Pdate == DateTime.MinValue)
                {
                    lblFactoryStatus.Text = "  Not Ready ";
                    lblFactoryStatusDate.Text = "N/A";
                }
                else
                {
                    lblFactoryStatus.Text = "  Ready ";
                    lblFactoryStatusDate.Text = Pdate.ToString("dd/MM/yyyy");
                }

                int FactoryPendingDay = Converter.GetInteger(dt.Rows[0]["FactoryPendingDay"]);
                if (FactoryPendingDay < 0)
                {

                    lblFactoryStatusMode.Text = "Delay : " + Math.Abs(FactoryPendingDay) + " Days";
                }
                else
                {
                    lblFactoryStatusMode.Text = "Pending : " + Math.Abs(FactoryPendingDay) + " Days";

                }


                Pdate = Converter.GetDateTime(dt.Rows[0]["SendToFactoryDate"]);


                Pdate = Converter.GetDateTime(dt.Rows[0]["SendToTransitDate"]);
                if (Pdate == DateTime.MinValue)
                {
                    lblTransitStatus.Text = "  Not in Transit  ";
                    lblTransitStatusDate.Text = "N/A";
                }
                else
                {
                    lblTransitStatus.Text = "  Send to Transit  ";
                    lblTransitStatusDate.Text = Pdate.ToString("dd/MM/yyyy");
                }

                int TransitPendingDay = Converter.GetInteger(dt.Rows[0]["TransitPendingDay"]);

                if (TransitPendingDay < 0)
                {

                    lblTransitStatusMode.Text = "Delay : " + Math.Abs(TransitPendingDay) + " Days";
                }
                else
                {
                    lblTransitStatusMode.Text = "Pending : " + Math.Abs(TransitPendingDay) + " Days";
                }


                Pdate = Converter.GetDateTime(dt.Rows[0]["ReceiveFromTransitDate"]);
                if (Pdate == DateTime.MinValue)
                {
                    lblReceiveStatus.Text = "  Not Received  ";
                    lblReceiveStatusDate.Text = "N/A";
                }
                else
                {
                    lblReceiveStatus.Text = "  Received  ";
                    lblReceiveStatusDate.Text = Pdate.ToString("dd/MM/yyyy");
                }

                int ReceiveDay = Converter.GetInteger(dt.Rows[0]["ReceivePendingDay"]);

                if (TransitPendingDay < 0)
                {

                    lblReceiveStatusMode.Text = "Delay : " + Math.Abs(ReceiveDay) + " Days";
                }
                else
                {
                    lblReceiveStatusMode.Text = "Pending : " + Math.Abs(ReceiveDay) + " Days";
                }

                showimage();
            }
            else
            {
                dvJpgTable.Visible = false;

            }

        }
    }
}