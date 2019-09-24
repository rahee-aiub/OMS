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
    public partial class A2ZOrderPositionReport : System.Web.UI.Page
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



                    OrderPartyDropdown();





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




                        chkAllOrderParty.Checked = true;
                        ddlOrderParty.Enabled = false;
                        ddlOrderParty.SelectedIndex = 0;




                        FunctionName();


                    }
                    else
                    {

                        string RchkAllOrderParty = (string)Session["SchkAllOrderParty"];
                        string RddlOrderParty = (string)Session["SddlOrderParty"];


                        string Rtxtfdate = (string)Session["Stxtfdate"];
                        string Rtxttdate = (string)Session["Stxttdate"];


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


        private void OrderPartyDropdown()
        {
            string sqlquery = "SELECT PartyCode,PartyName FROM A2ZPARTYCODE where GroupCode = 13";
            ddlOrderParty = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlOrderParty, "A2ZACOMS");
        }


        protected void FunctionName()
        {

            lblStatementFunc.Text = "Order Position List";

        }


        protected void StoreRecordsSession()
        {
            Session["ProgFlag"] = "1";


            if (chkAllOrderParty.Checked == true)
            {
                Session["SchkAllOrderParty"] = "1";
            }
            else
            {
                Session["SchkAllOrderParty"] = "0";
            }

            Session["SddlOrderParty"] = ddlOrderParty.SelectedValue;

            Session["Stxtfdate"] = txtfdate.Text;
            Session["Stxttdate"] = txttdate.Text;



        }

        protected void BtnView_Click(object sender, EventArgs e)
        {
            try
            {

                StoreRecordsSession();


                var prm = new object[3];

                if (chkAllOrderParty.Checked == true)
                {
                    prm[0] = "0";
                }
                else
                {
                    prm[0] = ddlOrderParty.SelectedValue;
                }

                prm[1] = Converter.GetDateToYYYYMMDD(txtfdate.Text);
                prm[2] = Converter.GetDateToYYYYMMDD(txttdate.Text);


                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_rptOrderPositionSummary", prm, "A2ZACOMS"));

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_FDATE, Converter.GetDateToYYYYMMDD(txtfdate.Text));
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_TDATE, Converter.GetDateToYYYYMMDD(txttdate.Text));

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZACOMS");

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptOrderPositionReportSummary");
                Response.Redirect("ReportServer.aspx", false);




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

            Session["SchkAllOrderParty"] = string.Empty;
            Session["SddlOrderParty"] = string.Empty;





        }
        protected void BtnExit_Click(object sender, EventArgs e)
        {
            RemoveSession();
            Response.Redirect("A2ZERPModule.aspx");
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











    }

}
