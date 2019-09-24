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
    public partial class A2ZRDPDetailsReport : System.Web.UI.Page
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




                        FunctionName();


                    }
                    else
                    {



                        string Rtxtfdate = (string)Session["Stxtfdate"];
                        string Rtxttdate = (string)Session["Stxttdate"];



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




        protected void FunctionName()
        {

            lblStatementFunc.Text = "RDP Details Report";

        }


        protected void StoreRecordsSession()
        {
            Session["ProgFlag"] = "1";



            Session["Stxtfdate"] = txtfdate.Text;
            Session["Stxttdate"] = txttdate.Text;



        }

        protected void BtnView_Click(object sender, EventArgs e)
        {
            try
            {

                StoreRecordsSession();

                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_FDATE, Converter.GetDateToYYYYMMDD(txtfdate.Text));
                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.COMMON_TDATE, Converter.GetDateToYYYYMMDD(txttdate.Text));


                SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_DATABASE_NAME_KEY, "A2ZACOMS");

                if (rbtOptDetails.Checked == true)
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptOMSRDPDetailsReport");
                }
                else 
                {
                    SessionStore.SaveToCustomStore(DataAccessLayer.Utility.Params.REPORT_FILE_NAME_KEY, "rptOMSRDPSummaryReport");
                }
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

           




        }
        protected void BtnExit_Click(object sender, EventArgs e)
        {
            RemoveSession();
            Response.Redirect("A2ZERPModule.aspx");
        }









    }

}
