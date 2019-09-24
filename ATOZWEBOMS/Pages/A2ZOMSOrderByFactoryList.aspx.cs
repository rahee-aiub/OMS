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
    public partial class A2ZOMSOrderByFactoryList : System.Web.UI.Page
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
                    txtfdate.Text = Converter.GetString(date);
                    txttdate.Text = Converter.GetString(date);

                    //LoadReceiveGridSpooler();

                 
                    gvOrderInfo1.Visible = false;

                    BtnBack.Visible = false;

                    string PFlag = (string)Session["ProgFlag"];
                    CtrlProgFlag.Text = PFlag;

                    if (CtrlProgFlag.Text == "1" || CtrlProgFlag.Text == "2")
                    {
                        string RddlSteps = (string)Session["SddlSteps"];
                        string Rtxtfdate = (string)Session["Stxtfdate"];
                        string Rtxttdate = (string)Session["Stxttdate"];

                        string ROrderParty = (string)Session["SOrderParty"];
                        string RFactoryParty = (string)Session["SFactoryParty"];

                        CtrlOrderParty.Text = ROrderParty;
                        CtrlFactoryParty.Text = RFactoryParty;

                       
                        ddlSteps.SelectedValue = RddlSteps;
                        txtfdate.Text = Rtxtfdate;
                        txttdate.Text = Rtxttdate;
                    }

                    if (CtrlProgFlag.Text == "2")
                    {
                        lblFromDate.Text = Converter.GetDateToYYYYMMDD(txtfdate.Text);
                        lblTillDate.Text = Converter.GetDateToYYYYMMDD(txttdate.Text);

                        gvOrderInfo1.Visible = true;
                        BtnExit.Visible = false;
                        BtnBack.Visible = true;

                        if (ddlSteps.Text == "1")
                        {
                            string sqlquery = "SELECT OrderNo,OrderDate,ItemName,ItemKarat,ItemPiece,ItemTotalWeight FROM A2ZORDERDETAILS WHERE OrderParty='" + CtrlOrderParty.Text + "' AND FactoryParty='" + CtrlFactoryParty.Text + "' AND FactoryReceiveDate IS NOT NULL AND ReadyInFactoryDate IS NULL AND OrderDate BETWEEN '" + lblFromDate.Text + "' AND '" + lblTillDate.Text + "'";
                            gvOrderInfo1 = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery, gvOrderInfo1, "A2ZACOMS");
                        }
                        else
                        {
                            string sqlquery = "SELECT OrderNo,OrderDate,ItemName,ItemKarat,ItemPiece,ItemTotalWeight FROM A2ZORDERDETAILS WHERE OrderParty='" + CtrlOrderParty.Text + "' AND FactoryParty='" + CtrlFactoryParty.Text + "' AND FactoryReceiveDate IS NOT NULL AND ReadyInFactoryDate IS NOT NULL AND OrderDate BETWEEN '" + lblFromDate.Text + "' AND '" + lblTillDate.Text + "'";
                            gvOrderInfo1 = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery, gvOrderInfo1, "A2ZACOMS");
                        }
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
            Session["SddlSteps"] = string.Empty;
            Session["Stxtfdate"] = string.Empty;
            Session["Stxttdate"] = string.Empty;
        }




        private void LoadReceiveGridSpooler()
        {
            gvOrderInfo.Visible = true;

            string sqlquery = "SELECT OrderParty,OrderPartyName,FactoryParty1,FactoryPartyName1,FactoryNoOrder1,FactoryParty2,FactoryPartyName2,FactoryNoOrder2,FactoryPartyName3,FactoryNoOrder3,FactoryPartyName4,FactoryNoOrder4,FactoryPartyName5,FactoryNoOrder5,FactoryPartyName6,FactoryNoOrder6,FactoryPartyName7,FactoryNoOrder7,FactoryPartyName8,FactoryNoOrder8,FactoryPartyName9,FactoryNoOrder9,FactoryPartyName10,FactoryNoOrder10 FROM WFA2ZORDERPOSITION";
            gvOrderInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery, gvOrderInfo, "A2ZACOMS");
        }




        protected void BtnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                Session["ProgFlag"] = "2";
                Session["CtrlFlag"] = "2";

                Session["SddlSteps"] = ddlSteps.SelectedValue;
                Session["Stxtfdate"] = txtfdate.Text;
                Session["Stxttdate"] = txttdate.Text;

                Button b = (Button)sender;
                GridViewRow r = (GridViewRow)b.NamingContainer;

                Label OrderNo = (Label)gvOrderInfo1.Rows[r.RowIndex].Cells[1].FindControl("lblOrderNo");
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

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            RemoveSession();
            Response.Redirect("A2ZERPModule.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }



       
        protected void BtnBack_Click(object sender, EventArgs e)
        {
            BtnBack.Visible = false;
            BtnExit.Visible = true;
            gvOrderInfo1.Visible = false;
            gvOrderInfo.Visible = true;
            LoadReceiveGridSpooler();
        }
        protected void BtnFactoryNoOrder1_Click(object sender, EventArgs e)
        {
            lblFromDate.Text = Converter.GetDateToYYYYMMDD(txtfdate.Text);
            lblTillDate.Text = Converter.GetDateToYYYYMMDD(txttdate.Text);


            Button b = (Button)sender;
            GridViewRow r = (GridViewRow)b.NamingContainer;

            Label OrderParty = (Label)gvOrderInfo.Rows[r.RowIndex].Cells[0].FindControl("lblOrderParty");
            Label FactoryParty = (Label)gvOrderInfo.Rows[r.RowIndex].Cells[2].FindControl("lblFactoryParty1");

            if (FactoryParty != null)
            {

                Session["SOrderParty"] = OrderParty.Text;
                Session["SFactoryParty"] = FactoryParty.Text;


                gvOrderInfo.Visible = false;
                gvOrderInfo1.Visible = true;

                BtnExit.Visible = false;
                BtnBack.Visible = true;

                if (ddlSteps.Text == "1")
                {
                    string sqlquery = "SELECT OrderNo,OrderDate,ItemName,ItemKarat,ItemPiece,ItemTotalWeight FROM A2ZORDERDETAILS WHERE OrderParty='" + OrderParty.Text + "' AND FactoryParty='" + FactoryParty.Text + "' AND FactoryReceiveDate IS NOT NULL AND ReadyInFactoryDate IS NULL AND OrderDate BETWEEN '" + lblFromDate.Text + "' AND '" + lblTillDate.Text + "'";
                    gvOrderInfo1 = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery, gvOrderInfo1, "A2ZACOMS");
                }
                else 
                {
                    string sqlquery = "SELECT OrderNo,OrderDate,ItemName,ItemKarat,ItemPiece,ItemTotalWeight FROM A2ZORDERDETAILS WHERE OrderParty='" + OrderParty.Text + "' AND FactoryParty='" + FactoryParty.Text + "' AND FactoryReceiveDate IS NOT NULL AND ReadyInFactoryDate IS NOT NULL AND OrderDate BETWEEN '" + lblFromDate.Text + "' AND '" + lblTillDate.Text + "'";
                    gvOrderInfo1 = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery, gvOrderInfo1, "A2ZACOMS");
                }
            }


        }
        protected void BtnFactoryNoOrder2_Click(object sender, EventArgs e)
        {
            lblFromDate.Text = Converter.GetDateToYYYYMMDD(txtfdate.Text);
            lblTillDate.Text = Converter.GetDateToYYYYMMDD(txttdate.Text);

            Button b = (Button)sender;
            GridViewRow r = (GridViewRow)b.NamingContainer;

            Label OrderParty = (Label)gvOrderInfo.Rows[r.RowIndex].Cells[0].FindControl("lblOrderParty");
            Label FactoryParty = (Label)gvOrderInfo.Rows[r.RowIndex].Cells[3].FindControl("lblFactoryParty2");


            if (FactoryParty != null)
            {
                Session["SOrderParty"] = OrderParty.Text;
                Session["SFactoryParty"] = FactoryParty.Text;

                gvOrderInfo.Visible = false;
                gvOrderInfo1.Visible = true;

                BtnExit.Visible = false;
                BtnBack.Visible = true;

                if (ddlSteps.Text == "1")
                {
                    string sqlquery = "SELECT OrderNo,OrderDate,ItemName,ItemKarat,ItemPiece,ItemTotalWeight FROM A2ZORDERDETAILS WHERE OrderParty='" + OrderParty.Text + "' AND FactoryParty='" + FactoryParty.Text + "' AND FactoryReceiveDate IS NOT NULL AND ReadyInFactoryDate IS NULL AND OrderDate BETWEEN '" + lblFromDate.Text + "' AND '" + lblTillDate.Text + "'";
                    gvOrderInfo1 = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery, gvOrderInfo1, "A2ZACOMS");
                }
                else
                {
                    string sqlquery = "SELECT OrderNo,OrderDate,ItemName,ItemKarat,ItemPiece,ItemTotalWeight FROM A2ZORDERDETAILS WHERE OrderParty='" + OrderParty.Text + "' AND FactoryParty='" + FactoryParty.Text + "' AND FactoryReceiveDate IS NOT NULL AND ReadyInFactoryDate IS NOT NULL AND OrderDate BETWEEN '" + lblFromDate.Text + "' AND '" + lblTillDate.Text + "'";
                    gvOrderInfo1 = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery, gvOrderInfo1, "A2ZACOMS");
                }
            }

        }
        protected void BtnFactoryNoOrder3_Click(object sender, EventArgs e)
        {
            lblFromDate.Text = Converter.GetDateToYYYYMMDD(txtfdate.Text);
            lblTillDate.Text = Converter.GetDateToYYYYMMDD(txttdate.Text);

            Button b = (Button)sender;
            GridViewRow r = (GridViewRow)b.NamingContainer;

            Label OrderParty = (Label)gvOrderInfo.Rows[r.RowIndex].Cells[0].FindControl("lblOrderParty");
            Label FactoryParty = (Label)gvOrderInfo.Rows[r.RowIndex].Cells[4].FindControl("lblFactoryParty3");

            if (FactoryParty != null)
            {
                Session["SOrderParty"] = OrderParty.Text;
                Session["SFactoryParty"] = FactoryParty.Text;

                gvOrderInfo.Visible = false;
                gvOrderInfo1.Visible = true;

                BtnExit.Visible = false;
                BtnBack.Visible = true;

                if (ddlSteps.Text == "1")
                {
                    string sqlquery = "SELECT OrderNo,OrderDate,ItemName,ItemKarat,ItemPiece,ItemTotalWeight FROM A2ZORDERDETAILS WHERE OrderParty='" + OrderParty.Text + "' AND FactoryParty='" + FactoryParty.Text + "' AND FactoryReceiveDate IS NOT NULL AND ReadyInFactoryDate IS NULL AND OrderDate BETWEEN '" + lblFromDate.Text + "' AND '" + lblTillDate.Text + "'";
                    gvOrderInfo1 = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery, gvOrderInfo1, "A2ZACOMS");
                }
                else
                {
                    string sqlquery = "SELECT OrderNo,OrderDate,ItemName,ItemKarat,ItemPiece,ItemTotalWeight FROM A2ZORDERDETAILS WHERE OrderParty='" + OrderParty.Text + "' AND FactoryParty='" + FactoryParty.Text + "' AND FactoryReceiveDate IS NOT NULL AND ReadyInFactoryDate IS NOT NULL AND OrderDate BETWEEN '" + lblFromDate.Text + "' AND '" + lblTillDate.Text + "'";
                    gvOrderInfo1 = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery, gvOrderInfo1, "A2ZACOMS");
                }
            }

        }
        protected void BtnFactoryNoOrder4_Click(object sender, EventArgs e)
        {
            lblFromDate.Text = Converter.GetDateToYYYYMMDD(txtfdate.Text);
            lblTillDate.Text = Converter.GetDateToYYYYMMDD(txttdate.Text);

            Button b = (Button)sender;
            GridViewRow r = (GridViewRow)b.NamingContainer;

            Label OrderParty = (Label)gvOrderInfo.Rows[r.RowIndex].Cells[0].FindControl("lblOrderParty");
            Label FactoryParty = (Label)gvOrderInfo.Rows[r.RowIndex].Cells[5].FindControl("lblFactoryParty4");

            if (FactoryParty != null)
            {

                Session["SOrderParty"] = OrderParty.Text;
                Session["SFactoryParty"] = FactoryParty.Text;

                gvOrderInfo.Visible = false;
                gvOrderInfo1.Visible = true;

                BtnExit.Visible = false;
                BtnBack.Visible = true;

                if (ddlSteps.Text == "1")
                {
                    string sqlquery = "SELECT OrderNo,OrderDate,ItemName,ItemKarat,ItemPiece,ItemTotalWeight FROM A2ZORDERDETAILS WHERE OrderParty='" + OrderParty.Text + "' AND FactoryParty='" + FactoryParty.Text + "' AND FactoryReceiveDate IS NOT NULL AND ReadyInFactoryDate IS NULL AND OrderDate BETWEEN '" + lblFromDate.Text + "' AND '" + lblTillDate.Text + "'";
                    gvOrderInfo1 = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery, gvOrderInfo1, "A2ZACOMS");
                }
                else
                {
                    string sqlquery = "SELECT OrderNo,OrderDate,ItemName,ItemKarat,ItemPiece,ItemTotalWeight FROM A2ZORDERDETAILS WHERE OrderParty='" + OrderParty.Text + "' AND FactoryParty='" + FactoryParty.Text + "' AND FactoryReceiveDate IS NOT NULL AND ReadyInFactoryDate IS NOT NULL AND OrderDate BETWEEN '" + lblFromDate.Text + "' AND '" + lblTillDate.Text + "'";
                    gvOrderInfo1 = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery, gvOrderInfo1, "A2ZACOMS");
                }
            }

        }
        protected void BtnFactoryNoOrder5_Click(object sender, EventArgs e)
        {
            lblFromDate.Text = Converter.GetDateToYYYYMMDD(txtfdate.Text);
            lblTillDate.Text = Converter.GetDateToYYYYMMDD(txttdate.Text);

            Button b = (Button)sender;
            GridViewRow r = (GridViewRow)b.NamingContainer;

            Label OrderParty = (Label)gvOrderInfo.Rows[r.RowIndex].Cells[0].FindControl("lblOrderParty");
            Label FactoryParty = (Label)gvOrderInfo.Rows[r.RowIndex].Cells[6].FindControl("lblFactoryParty5");

            if (FactoryParty != null)
            {
                Session["SOrderParty"] = OrderParty.Text;
                Session["SFactoryParty"] = FactoryParty.Text;

                gvOrderInfo.Visible = false;
                gvOrderInfo1.Visible = true;

                BtnExit.Visible = false;
                BtnBack.Visible = true;

                if (ddlSteps.Text == "1")
                {
                    string sqlquery = "SELECT OrderNo,OrderDate,ItemName,ItemKarat,ItemPiece,ItemTotalWeight FROM A2ZORDERDETAILS WHERE OrderParty='" + OrderParty.Text + "' AND FactoryParty='" + FactoryParty.Text + "' AND FactoryReceiveDate IS NOT NULL AND ReadyInFactoryDate IS NULL AND OrderDate BETWEEN '" + lblFromDate.Text + "' AND '" + lblTillDate.Text + "'";
                    gvOrderInfo1 = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery, gvOrderInfo1, "A2ZACOMS");
                }
                else
                {
                    string sqlquery = "SELECT OrderNo,OrderDate,ItemName,ItemKarat,ItemPiece,ItemTotalWeight FROM A2ZORDERDETAILS WHERE OrderParty='" + OrderParty.Text + "' AND FactoryParty='" + FactoryParty.Text + "' AND FactoryReceiveDate IS NOT NULL AND ReadyInFactoryDate IS NOT NULL AND OrderDate BETWEEN '" + lblFromDate.Text + "' AND '" + lblTillDate.Text + "'";
                    gvOrderInfo1 = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery, gvOrderInfo1, "A2ZACOMS");
                }
            }

        }
        protected void BtnFactoryNoOrder6_Click(object sender, EventArgs e)
        {
            lblFromDate.Text = Converter.GetDateToYYYYMMDD(txtfdate.Text);
            lblTillDate.Text = Converter.GetDateToYYYYMMDD(txttdate.Text);

            Button b = (Button)sender;
            GridViewRow r = (GridViewRow)b.NamingContainer;

            Label OrderParty = (Label)gvOrderInfo.Rows[r.RowIndex].Cells[0].FindControl("lblOrderParty");
            Label FactoryParty = (Label)gvOrderInfo.Rows[r.RowIndex].Cells[7].FindControl("lblFactoryParty6");

            if (FactoryParty != null)
            {
                Session["SOrderParty"] = OrderParty.Text;
                Session["SFactoryParty"] = FactoryParty.Text;

                gvOrderInfo.Visible = false;
                gvOrderInfo1.Visible = true;

                BtnExit.Visible = false;
                BtnBack.Visible = true;

                if (ddlSteps.Text == "1")
                {
                    string sqlquery = "SELECT OrderNo,OrderDate,ItemName,ItemKarat,ItemPiece,ItemTotalWeight FROM A2ZORDERDETAILS WHERE OrderParty='" + OrderParty.Text + "' AND FactoryParty='" + FactoryParty.Text + "' AND FactoryReceiveDate IS NOT NULL AND ReadyInFactoryDate IS NULL AND OrderDate BETWEEN '" + lblFromDate.Text + "' AND '" + lblTillDate.Text + "'";
                    gvOrderInfo1 = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery, gvOrderInfo1, "A2ZACOMS");
                }
                else
                {
                    string sqlquery = "SELECT OrderNo,OrderDate,ItemName,ItemKarat,ItemPiece,ItemTotalWeight FROM A2ZORDERDETAILS WHERE OrderParty='" + OrderParty.Text + "' AND FactoryParty='" + FactoryParty.Text + "' AND FactoryReceiveDate IS NOT NULL AND ReadyInFactoryDate IS NOT NULL AND OrderDate BETWEEN '" + lblFromDate.Text + "' AND '" + lblTillDate.Text + "'";
                    gvOrderInfo1 = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery, gvOrderInfo1, "A2ZACOMS");
                }
            }

        }
        protected void BtnFactoryNoOrder7_Click(object sender, EventArgs e)
        {
            lblFromDate.Text = Converter.GetDateToYYYYMMDD(txtfdate.Text);
            lblTillDate.Text = Converter.GetDateToYYYYMMDD(txttdate.Text);

            Button b = (Button)sender;
            GridViewRow r = (GridViewRow)b.NamingContainer;

            Label OrderParty = (Label)gvOrderInfo.Rows[r.RowIndex].Cells[0].FindControl("lblOrderParty");
            Label FactoryParty = (Label)gvOrderInfo.Rows[r.RowIndex].Cells[8].FindControl("lblFactoryParty7");

            if (FactoryParty != null)
            {
                Session["SOrderParty"] = OrderParty.Text;
                Session["SFactoryParty"] = FactoryParty.Text;

                gvOrderInfo.Visible = false;
                gvOrderInfo1.Visible = true;

                BtnExit.Visible = false;
                BtnBack.Visible = true;

                if (ddlSteps.Text == "1")
                {
                    string sqlquery = "SELECT OrderNo,OrderDate,ItemName,ItemKarat,ItemPiece,ItemTotalWeight FROM A2ZORDERDETAILS WHERE OrderParty='" + OrderParty.Text + "' AND FactoryParty='" + FactoryParty.Text + "' AND FactoryReceiveDate IS NOT NULL AND ReadyInFactoryDate IS NULL AND OrderDate BETWEEN '" + lblFromDate.Text + "' AND '" + lblTillDate.Text + "'";
                    gvOrderInfo1 = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery, gvOrderInfo1, "A2ZACOMS");
                }
                else
                {
                    string sqlquery = "SELECT OrderNo,OrderDate,ItemName,ItemKarat,ItemPiece,ItemTotalWeight FROM A2ZORDERDETAILS WHERE OrderParty='" + OrderParty.Text + "' AND FactoryParty='" + FactoryParty.Text + "' AND FactoryReceiveDate IS NOT NULL AND ReadyInFactoryDate IS NOT NULL AND OrderDate BETWEEN '" + lblFromDate.Text + "' AND '" + lblTillDate.Text + "'";
                    gvOrderInfo1 = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery, gvOrderInfo1, "A2ZACOMS");
                }
            }

        }
        protected void BtnFactoryNoOrder8_Click(object sender, EventArgs e)
        {
            lblFromDate.Text = Converter.GetDateToYYYYMMDD(txtfdate.Text);
            lblTillDate.Text = Converter.GetDateToYYYYMMDD(txttdate.Text);

            Button b = (Button)sender;
            GridViewRow r = (GridViewRow)b.NamingContainer;

            Label OrderParty = (Label)gvOrderInfo.Rows[r.RowIndex].Cells[0].FindControl("lblOrderParty");
            Label FactoryParty = (Label)gvOrderInfo.Rows[r.RowIndex].Cells[9].FindControl("lblFactoryParty8");

            if (FactoryParty != null)
            {
                Session["SOrderParty"] = OrderParty.Text;
                Session["SFactoryParty"] = FactoryParty.Text;

                gvOrderInfo.Visible = false;
                gvOrderInfo1.Visible = true;

                BtnExit.Visible = false;
                BtnBack.Visible = true;

                if (ddlSteps.Text == "1")
                {
                    string sqlquery = "SELECT OrderNo,OrderDate,ItemName,ItemKarat,ItemPiece,ItemTotalWeight FROM A2ZORDERDETAILS WHERE OrderParty='" + OrderParty.Text + "' AND FactoryParty='" + FactoryParty.Text + "' AND FactoryReceiveDate IS NOT NULL AND ReadyInFactoryDate IS NULL AND OrderDate BETWEEN '" + lblFromDate.Text + "' AND '" + lblTillDate.Text + "'";
                    gvOrderInfo1 = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery, gvOrderInfo1, "A2ZACOMS");
                }
                else
                {
                    string sqlquery = "SELECT OrderNo,OrderDate,ItemName,ItemKarat,ItemPiece,ItemTotalWeight FROM A2ZORDERDETAILS WHERE OrderParty='" + OrderParty.Text + "' AND FactoryParty='" + FactoryParty.Text + "' AND FactoryReceiveDate IS NOT NULL AND ReadyInFactoryDate IS NOT NULL AND OrderDate BETWEEN '" + lblFromDate.Text + "' AND '" + lblTillDate.Text + "'";
                    gvOrderInfo1 = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery, gvOrderInfo1, "A2ZACOMS");
                }
            }

        }
        protected void BtnFactoryNoOrder9_Click(object sender, EventArgs e)
        {
            lblFromDate.Text = Converter.GetDateToYYYYMMDD(txtfdate.Text);
            lblTillDate.Text = Converter.GetDateToYYYYMMDD(txttdate.Text);

            Button b = (Button)sender;
            GridViewRow r = (GridViewRow)b.NamingContainer;

            Label OrderParty = (Label)gvOrderInfo.Rows[r.RowIndex].Cells[0].FindControl("lblOrderParty");
            Label FactoryParty = (Label)gvOrderInfo.Rows[r.RowIndex].Cells[10].FindControl("lblFactoryParty9");

            if (FactoryParty != null)
            {
                Session["SOrderParty"] = OrderParty.Text;
                Session["SFactoryParty"] = FactoryParty.Text;

                gvOrderInfo.Visible = false;
                gvOrderInfo1.Visible = true;

                BtnExit.Visible = false;
                BtnBack.Visible = true;

                if (ddlSteps.Text == "1")
                {
                    string sqlquery = "SELECT OrderNo,OrderDate,ItemName,ItemKarat,ItemPiece,ItemTotalWeight FROM A2ZORDERDETAILS WHERE OrderParty='" + OrderParty.Text + "' AND FactoryParty='" + FactoryParty.Text + "' AND FactoryReceiveDate IS NOT NULL AND ReadyInFactoryDate IS NULL AND OrderDate BETWEEN '" + lblFromDate.Text + "' AND '" + lblTillDate.Text + "'";
                    gvOrderInfo1 = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery, gvOrderInfo1, "A2ZACOMS");
                }
                else
                {
                    string sqlquery = "SELECT OrderNo,OrderDate,ItemName,ItemKarat,ItemPiece,ItemTotalWeight FROM A2ZORDERDETAILS WHERE OrderParty='" + OrderParty.Text + "' AND FactoryParty='" + FactoryParty.Text + "' AND FactoryReceiveDate IS NOT NULL AND ReadyInFactoryDate IS NOT NULL AND OrderDate BETWEEN '" + lblFromDate.Text + "' AND '" + lblTillDate.Text + "'";
                    gvOrderInfo1 = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery, gvOrderInfo1, "A2ZACOMS");
                }
            }

        }
        protected void BtnFactoryNoOrder10_Click(object sender, EventArgs e)
        {
            lblFromDate.Text = Converter.GetDateToYYYYMMDD(txtfdate.Text);
            lblTillDate.Text = Converter.GetDateToYYYYMMDD(txttdate.Text);

            Button b = (Button)sender;
            GridViewRow r = (GridViewRow)b.NamingContainer;

            Label OrderParty = (Label)gvOrderInfo.Rows[r.RowIndex].Cells[0].FindControl("lblOrderParty");
            Label FactoryParty = (Label)gvOrderInfo.Rows[r.RowIndex].Cells[11].FindControl("lblFactoryParty10");

            if (FactoryParty != null)
            {
                Session["SOrderParty"] = OrderParty.Text;
                Session["SFactoryParty"] = FactoryParty.Text;

                gvOrderInfo.Visible = false;
                gvOrderInfo1.Visible = true;

                BtnExit.Visible = false;
                BtnBack.Visible = true;

                if (ddlSteps.Text == "1")
                {
                    string sqlquery = "SELECT OrderNo,OrderDate,ItemName,ItemKarat,ItemPiece,ItemTotalWeight FROM A2ZORDERDETAILS WHERE OrderParty='" + OrderParty.Text + "' AND FactoryParty='" + FactoryParty.Text + "' AND FactoryReceiveDate IS NOT NULL AND ReadyInFactoryDate IS NULL AND OrderDate BETWEEN '" + lblFromDate.Text + "' AND '" + lblTillDate.Text + "'";
                    gvOrderInfo1 = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery, gvOrderInfo1, "A2ZACOMS");
                }
                else
                {
                    string sqlquery = "SELECT OrderNo,OrderDate,ItemName,ItemKarat,ItemPiece,ItemTotalWeight FROM A2ZORDERDETAILS WHERE OrderParty='" + OrderParty.Text + "' AND FactoryParty='" + FactoryParty.Text + "' AND FactoryReceiveDate IS NOT NULL AND ReadyInFactoryDate IS NOT NULL AND OrderDate BETWEEN '" + lblFromDate.Text + "' AND '" + lblTillDate.Text + "'";
                    gvOrderInfo1 = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery, gvOrderInfo1, "A2ZACOMS");
                }
            }

        }


        protected void StoreRecordsSession()
        {
            Session["ProgFlag"] = "1";
           
            Session["SddlSteps"] = ddlSteps.SelectedValue;

            Session["Stxtfdate"] = txtfdate.Text;
            Session["Stxttdate"] = txttdate.Text;
        }

        protected void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlSteps.SelectedIndex == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Steps');", true);
                    return;
                }


                StoreRecordsSession();


                var prm = new object[3];

                prm[0] = Converter.GetDateToYYYYMMDD(txtfdate.Text);
                prm[1] = Converter.GetDateToYYYYMMDD(txttdate.Text);
                prm[2] = ddlSteps.SelectedValue;

                if (ddlSteps.SelectedValue == "1")
                {
                    int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_GenerateOrderQtyByFactory01", prm, "A2ZACOMS"));
                    if (result == 0)
                    {
                    }
                }
                else
                {
                    int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_GenerateOrderQtyByFactory02", prm, "A2ZACOMS"));
                    if (result == 0)
                    {
                    }
                }


                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_FDATE, Converter.GetDateToYYYYMMDD(txtfdate.Text));
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_TDATE, Converter.GetDateToYYYYMMDD(txttdate.Text));
                
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZACOMS");

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptOrderNumbersByFactoryReport");

                Response.Redirect("ReportServer.aspx", false);


            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnView_Click Problem');</script>");
                //throw ex;
            }
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            if (ddlSteps.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Steps');", true);
                return;
            }



            var prm = new object[3];

            prm[0] = Converter.GetDateToYYYYMMDD(txtfdate.Text);
            prm[1] = Converter.GetDateToYYYYMMDD(txttdate.Text);
            prm[2] = ddlSteps.SelectedValue;

            if (ddlSteps.SelectedValue == "1")
            {
                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_GenerateOrderQtyByFactory01", prm, "A2ZACOMS"));
                if (result == 0)
                {
                    LoadReceiveGridSpooler();
                }
            }
            else
            {
                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_GenerateOrderQtyByFactory02", prm, "A2ZACOMS"));
                if (result == 0)
                {
                    LoadReceiveGridSpooler();
                }
            }

        }


    }
}