using System;
using ATOZWEBOMS.WebSessionStore;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.DTO.HouseKeeping;
using DataAccessLayer.Utility;
using System.Data;
using DataAccessLayer.BLL;
using DataAccessLayer.DTO.SystemControl;

namespace ATOZWEBOMS.Pages
{
    public partial class A2ZOrderPositionList : System.Web.UI.Page
    {

        //public string TmpOpenDate;
        //public string TmpAccMatureDate;
        //public string TmpAccPrevRenwlDate;
        //public Int16 TmpAccPeriod;
        //public Decimal TmpAccOrgAmt;
        //public Decimal TmpAccPrincipal;
        //public Decimal TmpAccIntRate;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!IsPostBack)
                {
                    string NewAccNo = (string)Session["NewAccNo"];
                    string flag = (string)Session["flag"];
                    lblflag.Text = flag;


                    lblID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));
                    lblIDName.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_NAME));



                    OrderNoDropdown();





                    string PFlag = (string)Session["ProgFlag"];
                    CtrlProgFlag.Text = PFlag;

                    if (CtrlProgFlag.Text != "1")
                    {


                        var p = A2ZERPSYSPRMDTO.GetParameterValue();
                        lblCompanyName.Text = Converter.GetString(p.PrmUnitName);
                        lblBranchName.Text = Converter.GetString(p.PrmUnitName);


                        //var p = A2ZERPSYSPRMDTO.GetParameterValue();
                        //lblBranchNo.Text = Converter.GetString(p.PrmUnitNo);

                        A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                        DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                        string date = dt.ToString("dd/MM/yyyy");

                        txtfdate.Text = Converter.GetString(date);
                        txttdate.Text = Converter.GetString(date);

                        lblProcDate.Text = Converter.GetString(date);




                        chkAllOrderNo.Checked = true;
                        ddlOrderNo.Enabled = false;
                        ddlOrderNo.SelectedIndex = 0;


                        rbtOptDetails.Visible = false;
                        rbtOptSummary.Visible = false;


                        FunctionName();


                    }
                    else
                    {

                        string RchkAllOrderNo = (string)Session["SchkAllOrderNo"];
                        string RddlOrderNo = (string)Session["SddlOrderNo"];


                        string Rtxtfdate = (string)Session["Stxtfdate"];
                        string Rtxttdate = (string)Session["Stxttdate"];


                        if (RchkAllOrderNo == "1")
                        {
                            chkAllOrderNo.Checked = true;
                            ddlOrderNo.Enabled = false;
                            ddlOrderNo.SelectedIndex = 0;

                            rbtOptDetails.Visible = false;
                            rbtOptSummary.Visible = false;
                        }
                        else
                        {
                            chkAllOrderNo.Checked = false;
                            ddlOrderNo.SelectedValue = RddlOrderNo;
                            ddlOrderNo.Enabled = true;

                            rbtOptDetails.Visible = true;
                            rbtOptSummary.Visible = true;
                        }

                        txtfdate.Text = Rtxtfdate;
                        txttdate.Text = Rtxttdate;



                    }



                    FunctionName();

                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.Page_Load Problem');</script>");


                //throw ex;
            }
        }


        private void OrderNoDropdown()
        {
            string sqlquery = "SELECT OrderNo,OrderNo FROM A2ZORDERDETAILS";
            ddlOrderNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlOrderNo, "A2ZACOMS");
        }


        protected void FunctionName()
        {

            lblStatementFunc.Text = "Order Details List";

        }


        protected void StoreRecordsSession()
        {
            Session["ProgFlag"] = "1";


            if (chkAllOrderNo.Checked == true)
            {
                Session["SchkAllOrderNo"] = "1";
            }
            else
            {
                Session["SchkAllOrderNo"] = "0";
            }

            Session["SddlOrderNo"] = ddlOrderNo.SelectedValue;

            Session["Stxtfdate"] = txtfdate.Text;
            Session["Stxttdate"] = txttdate.Text;



        }

        protected void BtnView_Click(object sender, EventArgs e)
        {
            try
            {

                StoreRecordsSession();

                if (chkAllOrderNo.Checked == true || rbtOptDetails.Checked == true)
                {
                    var prm = new object[3];

                    if (chkAllOrderNo.Checked == true)
                    {
                        prm[0] = "0";
                    }
                    else
                    {
                        prm[0] = ddlOrderNo.SelectedValue;
                    }

                    prm[1] = Converter.GetDateToYYYYMMDD(txtfdate.Text);
                    prm[2] = Converter.GetDateToYYYYMMDD(txttdate.Text);


                    int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_rptOrderPositionReport", prm, "A2ZACOMS"));
                    if (result == 0)
                    {
                        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_FDATE, Converter.GetDateToYYYYMMDD(txtfdate.Text));
                        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_TDATE, Converter.GetDateToYYYYMMDD(txttdate.Text));

                        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZACOMS");

                        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptOrderPositionReport");
                        Response.Redirect("ReportServer.aspx", false);
                    }
                }
                else
                {
                    var prm = new object[1];

                    prm[0] = ddlOrderNo.SelectedValue;


                    int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_rptOrderPositionSingle", prm, "A2ZACOMS"));
                    if (result == 0)
                    {

                        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZACOMS");


                        SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptOrderPositionSummarySingle");
                        Response.Redirect("ReportServer.aspx", false);
                    }
                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnView_Click Problem');</script>");
                //throw ex;
            }
        }


        protected void RemoveSession()
        {


            Session["ProgFlag"] = string.Empty;

            Session["CFlag"] = string.Empty;

            Session["SchkAllOrderNo"] = string.Empty;
            Session["SddlOrderNo"] = string.Empty;





        }
        protected void BtnExit_Click(object sender, EventArgs e)
        {
            RemoveSession();
            Response.Redirect("A2ZERPModule.aspx");
        }



        protected void chkAllOrderNo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllOrderNo.Checked == true)
            {
                ddlOrderNo.SelectedIndex = 0;
                ddlOrderNo.Enabled = false;
                rbtOptDetails.Visible = false;
                rbtOptSummary.Visible = false;
            }
            else
            {
                ddlOrderNo.SelectedIndex = 0;
                ddlOrderNo.Enabled = true;

                rbtOptDetails.Visible = true;
                rbtOptSummary.Visible = true;
            }
        }

        protected void rbtOptSummary_CheckedChanged(object sender, EventArgs e)
        {
            lblfdate.Visible = false;
            txtfdate.Visible = false;
            lbltdate.Visible = false;
            txttdate.Visible = false;
        }

        protected void rbtOptDetails_CheckedChanged(object sender, EventArgs e)
        {
            lblfdate.Visible = true;
            txtfdate.Visible = true;
            lbltdate.Visible = true;
            txttdate.Visible = true;
        }









    }

}
