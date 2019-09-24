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
    public partial class A2ZTransactionList : System.Web.UI.Page
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


                  
                    



                    UserIdDropdown();




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




                        chkAllFuncOpt.Checked = true;
                        ddlFuncOpt.Enabled = false;
                        ddlFuncOpt.SelectedIndex = 0;

                        chkAllUser.Checked = true;
                        ddlUser.Enabled = false;
                        ddlUser.SelectedIndex = 0;


                        FunctionName();


                    }
                    else
                    {

                        string RchkAllFuncOpt = (string)Session["SchkAllFuncOpt"];
                        string RddlFuncOpt = (string)Session["SddlFuncOpt"];

                        string RchkAllUser = (string)Session["SchkAllUser"];
                        string RddlUser = (string)Session["SddlUser"];


                        string Rtxtfdate = (string)Session["Stxtfdate"];
                        string Rtxttdate = (string)Session["Stxttdate"];


                    
                        txtfdate.Text = Rtxtfdate;
                        txttdate.Text = Rtxttdate;


                        if (RchkAllFuncOpt == "1")
                        {
                            chkAllFuncOpt.Checked = true;
                            ddlFuncOpt.SelectedIndex = 0;
                            ddlFuncOpt.Enabled = false;
                        }
                        else
                        {
                            chkAllFuncOpt.Checked = false;
                            ddlFuncOpt.SelectedValue = RddlFuncOpt;
                            ddlFuncOpt.Enabled = true;
                        }


                        if (RchkAllUser == "1")
                        {
                            chkAllUser.Checked = true;
                            ddlUser.SelectedIndex = 0;
                            ddlUser.Enabled = false;
                        }
                        else
                        {
                            chkAllUser.Checked = false;
                            ddlUser.SelectedValue = RddlUser;
                            ddlUser.Enabled = true;
                        }



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

        private void UserIdDropdown()
        {
            string sqlquery = "SELECT IdsNo,IdsName from A2ZSYSIDS";
            ddlUser = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlUser, "A2ZACOMS");
        }


        protected void FunctionName()
        {

            lblStatementFunc.Text = "Transaction List";

        }


        protected void StoreRecordsSession()
        {
            Session["ProgFlag"] = "1";


            if (chkAllFuncOpt.Checked == true)
            {
                Session["SchkAllFuncOpt"] = "1";
            }
            else
            {
                Session["SchkAllFuncOpt"] = "0";
            }

            Session["SddlFuncOpt"] = ddlFuncOpt.SelectedValue;


            if (chkAllUser.Checked == true)
            {
                Session["SchkAllUser"] = "1";
            }
            else
            {
                Session["SchkAllUser"] = "0";
            }

            Session["ddlUser"] = ddlFuncOpt.SelectedValue;


            Session["Stxtfdate"] = txtfdate.Text;
            Session["Stxttdate"] = txttdate.Text;



        }

        protected void BtnView_Click(object sender, EventArgs e)
        {
            try
            {



                StoreRecordsSession();

                //var p = A2ZERPSYSPRMDTO.GetParameterValue();
                //string comName = p.PrmUnitName;
                //string comAddress1 = p.PrmUnitAdd1;
                //SessionStore.SaveToCustomStore(Params.COMPANY_NAME, comName);
                //SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, comAddress1);

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_FDATE, Converter.GetDateToYYYYMMDD(txtfdate.Text));
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_TDATE, Converter.GetDateToYYYYMMDD(txttdate.Text));

                //  For Account Type and Name Desc. 




                if (chkAllFuncOpt.Checked == true)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, 0);
                }
                else
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO1, ddlFuncOpt.SelectedValue);
                }


                if (chkAllUser.Checked == true)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO2, 0);
                }
                else
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_NO2, ddlUser.SelectedValue);
                }


                //============== End For Multi User Parameter =========================
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZACOMS");

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.NFLAG, 0);
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptMTransactionList");

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
            Session["flag"] = string.Empty;
            Session["NewAccNo"] = string.Empty;
            Session["RTranDate"] = string.Empty;
            Session["SFuncOpt"] = string.Empty;
            Session["SModule"] = string.Empty;
            Session["SControlFlag"] = string.Empty;

            Session["ProgFlag"] = string.Empty;

            Session["CFlag"] = string.Empty;

            Session["SchkAllFuncOpt"] = string.Empty;
            Session["SddlFuncOpt"] = string.Empty;

            Session["SchkAllUser"] = string.Empty;
            Session["SddlUser"] = string.Empty;




            Session["Stxtfdate"] = string.Empty;
            Session["Stxttdate"] = string.Empty;




        }
        protected void BtnExit_Click(object sender, EventArgs e)
        {
            RemoveSession();
            Response.Redirect("A2ZERPModule.aspx");
        }






        protected void AccTransferedMSG()
        {
            //String csname1 = "PopupScript";
            //Type cstype = GetType();
            //ClientScriptManager cs = Page.ClientScript;

            //if (!cs.IsStartupScriptRegistered(cstype, csname1))
            //{
            //    String cstext1 = "alert('Account not Active');";
            //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            //}

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Account Transfered');", true);
            return;
        }





        private void InvalidMemMSG()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Member No.');", true);
            return;
        }

        protected void chkAllFuncOpt_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllFuncOpt.Checked == true)
            {
                ddlFuncOpt.SelectedIndex = 0;
                ddlFuncOpt.Enabled = false;
            }
            else
            {
                ddlFuncOpt.SelectedIndex = 0;
                ddlFuncOpt.Enabled = true;
            }
        }

        protected void chkAllUser_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllUser.Checked == true)
            {
                ddlUser.SelectedIndex = 0;
                ddlUser.Enabled = false;
            }
            else
            {
                ddlUser.SelectedIndex = 0;
                ddlUser.Enabled = true;
            }
        }








    }

}
