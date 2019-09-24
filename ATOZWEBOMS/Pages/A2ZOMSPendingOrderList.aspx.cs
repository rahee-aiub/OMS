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
    public partial class A2ZOMSPendingOrderList : System.Web.UI.Page
    {
        string NoImage = "0x89504E470D0A1A0A0000000D49484452000000DC000000FE0806000000513E6BBC000000097048597300002E2300002E230178A53F760000000467414D410000B18E7CFB5193000000206348524D00007A25000080830000F9FF000080E9000075300000EA6000003A980000176F925FC54600000A584944415478DA62FCFFFF3FC3281805A3803E00208098468360148C02FA0180001ACD70A36014D0110004D068861B05A3808E0020804633DC28180574040001349AE146C128A0230008A0D10C370A46011D0140008D66B851300AE80800026834C38D82514047001040A3196E148C023A0280001ACD70A36014D0110004D068861B05A3808E0020804633DC28180574040001349AE146C128A0230008A0D10C370A46011D0140008D66B851300AE80800026834C38D82514047001040A3196E148C023A0280001ACD70A36014D0110004D068861B05A3808E0020804633DC28180574040001349AE146C128A0230008A0D10C370A46011D0140008D66B851300AE80800026834C38D82514047001040A3196E148C023A0280001ACD70A36014D0110004D068861B05A3808E0020804633DC28180574040001349AE146C128A0230008A0D10C370A46011D0140008D66B851300AE80800026834C38D82514047001040A3196E148C023A0280001ACD70A36014D0110004D068861B05A3808E0020804633DC28180574040001349AE146C128A0230008A0D10C370A46011D0140008D66B851300AE80800026834C38D82514047001040A3196E148C023A0280001ACD70A36014D0110004D068861B05A3808E0020804633DC28180574040001349AE146C128A0230008A0D10C370A46011D0140008D66B851300AE80800026834C38D82514047001040A3196E148C023A0280001ACD70A36014D0110004D068861B05A3808E0020804633DC28180574040001349AE146C128A0230008A0D10C370A46011D0140008D66B851300AE80800026834C38D82514047001040A3196E148C023A0280001ACD70A36014D0110004D068861B05A3808E0020804633DC28180574040001349AE146C128A0230008A0D10C370A46011D0140008D66B851300AE80800026834C38D82514047001040A3196E148C023A0280001ACD70A36014D0110004D068861B05A3808E0020804633DC28180574040001349AE146C128A0230008A0D10C370A46011D0140008D66B851300AE80800026834C38D82514047001040A3196E148C023A0280001ACD70A36014D0110004D068861B05A3808E0020804633DC28180574040001349AE146C128A0230008A0D10C370A46011D0140008D66B851300AE80800026834C38D82514047001040A3196E148C023A0280001ACD70A36014D0110004D068861B05A3808E0020804633DC28180574040001349AE146C128A0230008A0D10C370A46011D0140008D66B851300AE80800026834C38D82514047001040A3196E148C023A0280001ACD70A36014D0110004D068861B05A3808E0020804633DC28180574040001349AE146C128A0230008A0D10C370A46011D0140008D66B851300AE80800026834C38D82514047001040A3196E148C023A0280001ACD70A36014D0110004D068861B05A3808E0020804633DC28180574040001349AE146C128A0230008A0D10C370A46011D0140008D66B851300AE80800026834C38D82514047001040A3196E148C023A0280001ACD70A36014D0110004D068861B05A3808E0020804633DC28180574040001349AE146C128A0230008A0D10C370A46011D0140008D66B851300AE80800026834C38D82514047001040A3196E148C023A0280001ACD70A36014D0110004D068861B05A3808E0020804633DC28180574040001349AE146C128A0230008A0D10C370A46011D0140008D66B851300AE80800026834C38D82514047001040A3196E148C023A0280001ACD70A36014D0110004D068861B05A3808E0020804633DC28180574040001349AE146C128A0230008A0D10C370A46011D0140008D66B851300AE80800026834C38D82514047001040A3196E148C023A0280001ACD70A36014D0110004D068861B05A3808E0020804633DC28180574040001349AE146C128A0230008A0D10C370A46011D0140008D66B851300AE80800026834C38D82514047001040A3196E148C023A0280001ACD70A36014D0110004D068861B05A3808E0020804633DC28180574040001349AE146C128A0230008A0D10C370A46011D0140008D66B851300AE80800026834C38D82514047001040A3196E148C023A0280001ACD70A36014D0110004D068861B05A3808E0020804633DC28180574040001349AE146C128A0230008A0D10C370A46011D0140008D66B851300AE80800026834C38D82514047001040A3196E148C023A0280001ACD70A36014D0110004D068861B05A3808E0020804633DC28180574040001349AE146C128A0230008A0D10C370A46011D0140008D66B851300AE80800026834C38D82514047001040A3196E148C023A0280001ACD70A36014D0110004D068861B05A3808E0020804633DC28180574040001349AE146C128A0230008A0D10C370A46011D0140008D66B851300AE80800026834C38D82514047001040A3196E148C023A0280001ACD70A36014D0110004D068861B05A3808E0020804633DC28180574040001349AE146C128A0230008A0D10C370A46011D0140008D66B851300AE80800026834C38D82514047001040A3196E148C023A0280001ACD70A36014D0110004D068861B05A3808E0020804633DC28180574040001349AE146C128A0230008A0D10C370A46011D0140008D66B851300AE80800026834C38D82514047001040A3196E148C023A0280001ACD70A36014D0110004D068861B05A3808E0020804633DC28180574040001349AE146C128A0230008A0D10C370A46011D0140008D66B851300AE80800026834C38D82514047001040A3196E148C023A0280001ACD70A36014D0110004D068861B05A3808E0020804633DC28180574040001349AE146C128A0230008A0D10C370A46011D0140008D66B851300AE80800026834C38D82514047001040A3196E148C023A0280001ACD70A36014D0110004D068861B05A3808E0020804633DC28180574040001349AE146C128A0230008A0D10C370A46011D0140008D66B851300AE80800026834C38D82514047001040A3196E148C023A0280001ACD70A36014D0110004D068861B05A3808E0020804633DC28180574040001349AE146C128A0230008A0D10C370A46011D0140008D66B851300AE80800026834C38D82514047001040A3196E148C023A0280001ACD70A36014D0110004D068861B05A3808E0020804633DC28180574040001349AE146C128A0230008A0D10C370A46011D0140008D66B851300AE80800026834C38D82514047001040A3196E148C023A0280001ACD70A36014D0110004D068861B05A3808E0020804633DC28180574040001349AE146C128A0230008A0D10C370A46011D0140008D66B851300AE80800026834C38D82514047001040A3196E148C023A0280001ACD70A36014D0110004D068861B05A3808E0020804633DC28180574040001349AE146C128A0230008A0D10C370A46011D0140008D66B851300AE80800026834C38D82514047001040A3196E148C023A0280001ACD70A36014D0110004D068861B05A3808E0020804633DC28180574040001349AE146C128A0230008A0D10C370A46011D0140008D66B851300AE80800026834C38D82514047001040A3196E148C023A0280001ACD70A36014D0110004D068861B05A3808E0020804633DC28180574040001349AE146C128A0230008300095A304F9A3D6F81E0000000049454E44AE426082";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    FactoryPartyDropdown();
                    OrderPartyDropdown();
                    TransitPartyDropdown();



                    lblID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));
                    lblIDName.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_NAME));

                    A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                    DateTime dt1 = Converter.GetDateTime(dto.ProcessDate);
                    string date = dt1.ToString("dd/MM/yyyy");
                    txtfdate.Text = Converter.GetString(date);
                    txttdate.Text = Converter.GetString(date);
                    lblProcDate.Text = Converter.GetString(date);

                    string PFlag = (string)Session["ProgFlag"];
                    CtrlProgFlag.Text = PFlag;

                    if (CtrlProgFlag.Text == "1")
                    {
                        
                        string RchkAllFactoryParty = (string)Session["SchkAllFactoryParty"];
                        string RddlFactoryParty = (string)Session["SddlFactoryParty"];

                        string RchkAllOrderParty = (string)Session["SchkAllOrderParty"];
                        string RddlOrderParty = (string)Session["SddlOrderParty"];

                        string RchkAllTransitParty = (string)Session["SchkAllTransitParty"];
                        string RddlTransitParty = (string)Session["SddlTransitParty"];

                        string RchkAllSteps = (string)Session["SchkAllSteps"];
                        string RddlSteps = (string)Session["SddlSteps"];
 
                        string Rtxtfdate = (string)Session["Stxtfdate"];
                        string Rtxttdate = (string)Session["Stxttdate"];

                        if (RchkAllFactoryParty == "1")
                        {
                            chkAllFactoryParty.Checked = true;
                            ddlFactoryParty.Enabled = false;
                            ddlFactoryParty.SelectedIndex = 0;
                        }
                        else
                        {
                            chkAllFactoryParty.Checked = false;
                            ddlFactoryParty.SelectedValue = RddlFactoryParty;
                            ddlFactoryParty.Enabled = true;
                        }

                        if (RchkAllOrderParty == "1")
                        {
                            chkAllOrderParty.Checked = true;
                            ddlOrderParty.Enabled = false;
                            ddlOrderParty.SelectedIndex = 0;
                        }
                        else
                        {
                            chkAllOrderParty.Checked = false;
                            ddlOrderParty.SelectedValue = RddlOrderParty;
                            ddlOrderParty.Enabled = true;
                        }

                        if (RchkAllTransitParty == "1")
                        {
                            chkAllTransitParty.Checked = true;
                            ddlTransitParty.Enabled = false;
                            ddlTransitParty.SelectedIndex = 0;
                        }
                        else
                        {
                            chkAllTransitParty.Checked = false;
                            ddlTransitParty.SelectedValue = RddlTransitParty;
                            ddlTransitParty.Enabled = true;
                        }

                        if (RchkAllSteps == "1")
                        {
                            chkAllSteps.Checked = true;
                            ddlSteps.Enabled = false;
                            ddlSteps.SelectedIndex = 0;
                        }
                        else
                        {
                            chkAllSteps.Checked = false;
                            ddlSteps.SelectedValue = RddlSteps;
                            ddlSteps.Enabled = true;
                        }


                        txtfdate.Text = Rtxtfdate;
                        txttdate.Text = Rtxttdate;


                        LoadGridViewImage();
                    }
                    else 
                    {
                        chkAllFactoryParty.Checked = true;
                        chkAllOrderParty.Checked = true;
                        chkAllTransitParty.Checked = true;
                        chkAllSteps.Checked = true;

                        ddlFactoryParty.Enabled = false;
                        ddlOrderParty.Enabled = false;
                        ddlTransitParty.Enabled = false;
                        ddlSteps.Enabled = false; 

                    }


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

            Session["SchkAllFactoryParty"] = string.Empty;
            Session["SddlFactoryParty"] = string.Empty;

            Session["SchkAllOrderParty"] = string.Empty;
            Session["SddlOrderParty"] = string.Empty;

            Session["SchkAllTransitParty"] = string.Empty;
            Session["SddlTransitParty"] = string.Empty;

            Session["SchkAllSteps"] = string.Empty;
            Session["SddlSteps"] = string.Empty;

            Session["Stxtfdate"] = string.Empty;
            Session["Stxttdate"] = string.Empty;

        }

        private void FactoryPartyDropdown()
        {
            string sqlquery = "SELECT PartyCode,PartyName from A2ZPARTYCODE WHERE GroupCode = 11";
            ddlFactoryParty = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlFactoryParty, "A2ZACOMS");
        }

        private void OrderPartyDropdown()
        {
            string sqlquery = "SELECT PartyCode,PartyName from A2ZPARTYCODE WHERE GroupCode = 13";
            ddlOrderParty = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlOrderParty, "A2ZACOMS");
        }

        private void TransitPartyDropdown()
        {
            string sqlquery = "SELECT PartyCode,PartyName from A2ZPARTYCODE WHERE GroupCode = 12";
            ddlTransitParty = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlTransitParty, "A2ZACOMS");
        }

        private void LoadGridViewImage()
        {
            string sqlquery = "SELECT OrderModeName,OrderNo1,OrderPartyName1,ItemInfo1,ItemName1,ItemImage1,OrderNo2,OrderPartyName2,ItemInfo2,ItemName2,ISNULL(ItemImage2," + NoImage + ") AS ItemImage2,OrderNo3,OrderPartyName3,ItemInfo3,ItemName3,ISNULL(ItemImage3," + NoImage + ") AS ItemImage3,OrderNo4,OrderPartyName4,ItemInfo4,ItemName4,ISNULL(ItemImage4," + NoImage + ") AS ItemImage4,OrderNo5,OrderPartyName5,ItemInfo5,ItemName5,ISNULL(ItemImage5," + NoImage + ") AS ItemImage5 FROM WFA2ZORDERIMAGEVIEW";
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


        protected void Image1_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Session["ProgFlag"] = "1";
                Session["CtrlFlag"] = "1";

                ImageButton b = (ImageButton)sender;
                GridViewRow r = (GridViewRow)b.NamingContainer;

                Label OrderNo = (Label)gvDetailInfo.Rows[r.RowIndex].Cells[4].FindControl("lblOrderNo1");

                Session["OrderNo"] = OrderNo.Text;
                Response.Redirect("A2ZOMSSingleOrderDetails.aspx");


            }

            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnEdit_Click Problem');</script>");
                //throw ex;
            }

        }

        protected void Image2_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Session["ProgFlag"] = "1";
                Session["CtrlFlag"] = "1";

                ImageButton b = (ImageButton)sender;
                GridViewRow r = (GridViewRow)b.NamingContainer;

                Label OrderNo = (Label)gvDetailInfo.Rows[r.RowIndex].Cells[4].FindControl("lblOrderNo2");

                if (OrderNo.Text != string.Empty)
                {
                    Session["OrderNo"] = OrderNo.Text;
                    Response.Redirect("A2ZOMSSingleOrderDetails.aspx");
                }


            }

            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnEdit_Click Problem');</script>");
                //throw ex;
            }

        }

        protected void Image3_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Session["ProgFlag"] = "1";
                Session["CtrlFlag"] = "1";

                ImageButton b = (ImageButton)sender;
                GridViewRow r = (GridViewRow)b.NamingContainer;

                Label OrderNo = (Label)gvDetailInfo.Rows[r.RowIndex].Cells[4].FindControl("lblOrderNo3");
                if (OrderNo.Text != string.Empty)
                {
                    Session["OrderNo"] = OrderNo.Text;
                    Response.Redirect("A2ZOMSSingleOrderDetails.aspx");
                }

            }

            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnEdit_Click Problem');</script>");
                //throw ex;
            }

        }

        protected void Image4_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Session["ProgFlag"] = "1";
                Session["CtrlFlag"] = "1";

                ImageButton b = (ImageButton)sender;
                GridViewRow r = (GridViewRow)b.NamingContainer;

                Label OrderNo = (Label)gvDetailInfo.Rows[r.RowIndex].Cells[4].FindControl("lblOrderNo4");
                if (OrderNo.Text != string.Empty)
                {
                    Session["OrderNo"] = OrderNo.Text;
                    Response.Redirect("A2ZOMSSingleOrderDetails.aspx");
                }

            }

            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnEdit_Click Problem');</script>");
                //throw ex;
            }

        }

        protected void Image5_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Session["ProgFlag"] = "1";
                Session["CtrlFlag"] = "1";

                ImageButton b = (ImageButton)sender;
                GridViewRow r = (GridViewRow)b.NamingContainer;

                Label OrderNo = (Label)gvDetailInfo.Rows[r.RowIndex].Cells[4].FindControl("lblOrderNo5");
                if (OrderNo.Text != string.Empty)
                {
                    Session["OrderNo"] = OrderNo.Text;
                    Response.Redirect("A2ZOMSSingleOrderDetails.aspx");
                }

            }

            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnEdit_Click Problem');</script>");
                //throw ex;
            }

        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            if (ddlTransitParty.SelectedIndex == 0)
            {
                var prm = new object[6];

                prm[0] = Converter.GetDateToYYYYMMDD(txtfdate.Text);
                prm[1] = Converter.GetDateToYYYYMMDD(txttdate.Text);

                if (chkAllFactoryParty.Checked == true)
                {
                    prm[2] = "0";
                }
                else
                {
                    prm[2] = ddlFactoryParty.SelectedValue;
                }

                if (chkAllOrderParty.Checked == true)
                {
                    prm[3] = "0";
                }
                else
                {
                    prm[3] = ddlOrderParty.SelectedValue;
                }

                if (chkAllTransitParty.Checked == true)
                {
                    prm[4] = "0";
                }
                else
                {
                    prm[4] = ddlTransitParty.SelectedValue;
                }


                if (chkAllSteps.Checked == true)
                {
                    prm[5] = "0";
                }
                else
                {
                    prm[5] = ddlSteps.SelectedValue;
                }


                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_OrderPendingImageView", prm, "A2ZACOMS"));
                if (result == 0)
                {
                    LoadGridViewImage();
                }
            }
            else 
            {
                var prm = new object[6];

                prm[0] = Converter.GetDateToYYYYMMDD(txtfdate.Text);
                prm[1] = Converter.GetDateToYYYYMMDD(txttdate.Text);

                if (chkAllFactoryParty.Checked == true)
                {
                    prm[2] = "0";
                }
                else
                {
                    prm[2] = ddlFactoryParty.SelectedValue;
                }

                if (chkAllOrderParty.Checked == true)
                {
                    prm[3] = "0";
                }
                else
                {
                    prm[3] = ddlOrderParty.SelectedValue;
                }

                if (chkAllTransitParty.Checked == true)
                {
                    prm[4] = "0";
                }
                else
                {
                    prm[4] = ddlTransitParty.SelectedValue;
                }


                if (chkAllSteps.Checked == true)
                {
                    prm[5] = "0";
                }
                else
                {
                    prm[5] = ddlSteps.SelectedValue;
                }


                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_OrderPendingTransitImageView", prm, "A2ZACOMS"));
                if (result == 0)
                {
                    LoadGridViewImage();
                }
            }
        }


        protected void StoreRecordsSession()
        {
            Session["ProgFlag"] = "1";


            if (chkAllFactoryParty.Checked == true)
            {
                Session["SchkAllFactoryParty"] = "1";
            }
            else
            {
                Session["SchkAllFactoryParty"] = "0";
            }

            Session["SddlFactoryParty"] = ddlFactoryParty.SelectedValue;


            if (chkAllOrderParty.Checked == true)
            {
                Session["SchkAllOrderParty"] = "1";
            }
            else
            {
                Session["SchkAllOrderParty"] = "0";
            }

            Session["SddlOrderParty"] = ddlOrderParty.SelectedValue;


            if (chkAllTransitParty.Checked == true)
            {
                Session["SchkAllTransitParty"] = "1";
            }
            else
            {
                Session["SchkAllTransitParty"] = "0";
            }

            Session["SddlTransitParty"] = ddlTransitParty.SelectedValue;


            if (chkAllSteps.Checked == true)
            {
                Session["SchkAllSteps"] = "1";
            }
            else
            {
                Session["SchkAllSteps"] = "0";
            }

            Session["SddlSteps"] = ddlSteps.SelectedValue;

            Session["Stxtfdate"] = txtfdate.Text;
            Session["Stxttdate"] = txttdate.Text;



        }


        protected void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlTransitParty.SelectedIndex == 0)
                {
                    var prm = new object[6];

                    prm[0] = Converter.GetDateToYYYYMMDD(txtfdate.Text);
                    prm[1] = Converter.GetDateToYYYYMMDD(txttdate.Text);

                    if (chkAllFactoryParty.Checked == true)
                    {
                        prm[2] = "0";
                    }
                    else
                    {
                        prm[2] = ddlFactoryParty.SelectedValue;
                    }

                    if (chkAllOrderParty.Checked == true)
                    {
                        prm[3] = "0";
                    }
                    else
                    {
                        prm[3] = ddlOrderParty.SelectedValue;
                    }

                    if (chkAllTransitParty.Checked == true)
                    {
                        prm[4] = "0";
                    }
                    else
                    {
                        prm[4] = ddlTransitParty.SelectedValue;
                    }

                    if (chkAllSteps.Checked == true)
                    {
                        prm[5] = "0";
                    }
                    else
                    {
                        prm[5] = ddlSteps.SelectedValue;
                    }

                    int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_OrderPendingImageView", prm, "A2ZACOMS"));
                    if (result == 0)
                    {
                    }
                }
                else 
                {
                    var prm = new object[6];

                    prm[0] = Converter.GetDateToYYYYMMDD(txtfdate.Text);
                    prm[1] = Converter.GetDateToYYYYMMDD(txttdate.Text);

                    if (chkAllFactoryParty.Checked == true)
                    {
                        prm[2] = "0";
                    }
                    else
                    {
                        prm[2] = ddlFactoryParty.SelectedValue;
                    }

                    if (chkAllOrderParty.Checked == true)
                    {
                        prm[3] = "0";
                    }
                    else
                    {
                        prm[3] = ddlOrderParty.SelectedValue;
                    }

                    if (chkAllTransitParty.Checked == true)
                    {
                        prm[4] = "0";
                    }
                    else
                    {
                        prm[4] = ddlTransitParty.SelectedValue;
                    }

                    if (chkAllSteps.Checked == true)
                    {
                        prm[5] = "0";
                    }
                    else
                    {
                        prm[5] = ddlSteps.SelectedValue;
                    }

                    int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_OrderPendingTransitImageView", prm, "A2ZACOMS"));
                    if (result == 0)
                    {
                    }
                }

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_FDATE, Converter.GetDateToYYYYMMDD(txtfdate.Text));
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_TDATE, Converter.GetDateToYYYYMMDD(txttdate.Text));


                if (chkAllFactoryParty.Checked == true)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME1, "All");
                }
                else 
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME1, ddlFactoryParty.SelectedItem.Text);
                }

                if (chkAllOrderParty.Checked == true)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME2, "All");
                }
                else
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME2, ddlOrderParty.SelectedItem.Text);
                }

                if (chkAllTransitParty.Checked == true)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME3, "All");
                }
                else
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME3, ddlTransitParty.SelectedItem.Text);
                }

                if (chkAllSteps.Checked == true)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME4, "All");
                }
                else
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NAME4, ddlSteps.SelectedItem.Text);
                }


                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZACOMS");

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptPendingOrderImageReport");

                Response.Redirect("ReportServer.aspx", false);



            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnView_Click Problem');</script>");
                //throw ex;
            }
        }

        protected void chkAllFactoryParty_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllFactoryParty.Checked == true)
            {
                ddlFactoryParty.SelectedIndex = 0;
                ddlFactoryParty.Enabled = false;
            }
            else
            {
                ddlFactoryParty.SelectedIndex = 0;
                ddlFactoryParty.Enabled = true;
            }
        }

        protected void chkAllOrderParty_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllOrderParty.Checked == true)
            {
                ddlOrderParty.SelectedIndex = 0;
                ddlOrderParty.Enabled = false;
            }
            else
            {
                ddlOrderParty.SelectedIndex = 0;
                ddlOrderParty.Enabled = true;
            }
        }

        protected void chkAllTransitParty_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllTransitParty.Checked == true)
            {
                ddlTransitParty.SelectedIndex = 0;
                ddlTransitParty.Enabled = false;

                chkAllSteps.Checked = true;
                chkAllSteps.Enabled = true;
                ddlSteps.SelectedIndex = 0;
                ddlSteps.Enabled = false;
            }
            else
            {
                ddlTransitParty.SelectedIndex = 0;
                ddlTransitParty.Enabled = true;

                chkAllSteps.Checked = false;
                ddlSteps.SelectedValue = "3";
                ddlSteps.Enabled = false;
                chkAllSteps.Enabled = false;
            }
        }

        protected void chkAllSteps_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllSteps.Checked == true)
            {
                ddlSteps.SelectedIndex = 0;
                ddlSteps.Enabled = false;
            }
            else
            {
                ddlSteps.SelectedIndex = 0;
                ddlSteps.Enabled = true;
            }
        }

    }
}