using ATOZWEBOMS.WebSessionStore;
using DataAccessLayer.BLL;
using DataAccessLayer.DTO;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.DTO.HouseKeeping;
using DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace ATOZWEBOMS.Pages
{
    public partial class A2ZPeriodEnd : System.Web.UI.Page
    {

        SqlConnection con = new SqlConnection(DataAccessLayer.Constants.DBConstants.GetConnectionString("master"));
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    int userPermission = Converter.GetInteger(SessionStore.GetValue(Params.SYS_USER_PERMISSION));

                    hdnID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));


                    DivGridView.Visible = false;
                    Div1.Visible = false;

                    //if (userPermission != 30)
                    //{
                    //    string notifyMsg = "?txtOne=" + Converter.GetString(SessionStore.GetValue(Params.SYS_USER_NAME)) + "&txtTwo=" + "You Don't Have Permission for Approve" +
                    //                       "&txtThree=" + "Contact Your Super User" + "&PreviousMenu=A2ZERPModule.aspx";
                    //    Server.Transfer("Notify.aspx" + notifyMsg);
                    //}

                    int checkAllUser = DataAccessLayer.DTO.A2ZSYSIDSDTO.CountForSingleUserPurpose(Converter.GetSmallInteger(SessionStore.GetValue(Params.SYS_USER_ID)), "A2ZACOMS");

                    if (checkAllUser > 0)
                    {
                        string strQuery = "SELECT IdsNo, IdsName, EmpCode, IdsFlag, IdsLogInFlag FROM A2ZSYSIDS WHERE IdsLogInFlag <> 0";
                        gvUserInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(strQuery, gvUserInfo, "A2ZACOMS");

                        //btnProcess.BackColor = Color.Black;
                        btnProcess.Enabled = false;

                        


                        //string msg = "Can not Process Period End - System Not In Single User";

                        //String csname1 = "PopupScript";
                        //Type cstype = GetType();
                        //ClientScriptManager cs = Page.ClientScript;

                        //if (!cs.IsStartupScriptRegistered(cstype, csname1))
                        //{
                        //    String cstext1 = "alert('" + msg + "');";
                        //    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                        //}
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Can not Process Period End - System Not In Single User');", true);
                        return;
                    }


                    //string strQry = "SELECT BranchNo, BranchName, ProcStatusDesc, ProcStatusDate, UserId FROM A2ZERPBRANCH";
                    //gvBranchInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(strQry, gvBranchInfo, "A2ZHKCUBS");
   
                    var dt = A2ZCSPARAMETERDTO.GetParameterValue();
                    DateTime processDate = dt.ProcessDate;
                    string date1 = processDate.ToString("dd/MM/yyyy");
                    lblProcDate.Text = date1;

                    lblNewYear.Text = Converter.GetString(dt.CurrentYear);

                    txtDayEnd.Focus();


                    txtToDaysDate.Text = Converter.GetString(String.Format("{0:D}", processDate));


                    A2ZERPSYSPRMDTO dto = A2ZERPSYSPRMDTO.GetParameterValue();
                    hdndatapath.Text = Converter.GetString(dto.PrmDataPath);

                    lblEndOfMonth.Visible = false;
                    //lblYearEnd.Visible = false;

                    lblYearClose.Text = "0";

                    int lastDay = DateTime.DaysInMonth(processDate.Year, processDate.Month);

                    if (processDate.Day == lastDay)
                    {
                        lblEndOfMonth.Visible = true;

                        if (dt.ProcessDate.Month == 12)
                        {
                            lblYearEnd.Visible = true;
                        }
                        if (dt.ProcessDate.Month == 6)
                        {
                            lblYearClose.Text = "1";
                        }
                    }

                    A2ZCSPARAMETERDTO.UpdateSingleUserFlag(1);
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.Page_Load Problem');</script>");
                //throw ex;
            }
        }

        protected void CheckUnPostTransaction()
        {
            try
            {

                string qry1 = "SELECT TrnProcStat FROM A2ZTRANSACTION WHERE TrnProcStat = 1";
                DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZACOMS");
                if (dt1.Rows.Count > 0)
                {
                    UnpostMSG();
                    CtrlTranStat.Text = "1";
                }
                else
                {
                    CtrlTranStat.Text = "0";
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.CheckUnPostTransaction Problem');</script>");
                //throw ex;
            }
        }


        //protected void VerifyDayConfirmtion()
        //{
        //    try
        //    {
        //        CtrlProcDoneFlag.Text = "0";

        //        string qry1 = "SELECT ProcStatus FROM A2ZERPBRANCH WHERE ProcStatus = 0";
        //        DataTable dt1 = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry1, "A2ZHKGMS");
        //        if (dt1.Rows.Count > 0)
        //        {
        //            CtrlProcDoneFlag.Text = "1";
        //            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Process Done Confirmation Not Done');", true);
        //            return;    
        //        }
                
        //    }
        //    catch (Exception ex)
        //    {
        //        Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.CheckUnPostTransaction Problem');</script>");
        //        //throw ex;
        //    }
        //}

        protected void btnExit_Click(object sender, EventArgs e)
        {
            A2ZCSPARAMETERDTO.UpdateSingleUserFlag(0);
            if (CtrlProcFlag.Text == "0" || CtrlProcFlag.Text == "")
            {
                Response.Redirect("A2ZERPModule.aspx");
            }
            else
            {
                Response.Redirect("A2ZLogin.aspx");
            }
        }

        protected void btnHideMessageDiv_Click(object sender, EventArgs e)
        {

        }

        protected void BackUpMSG()
        {


            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Database Backup Not Done');", true);
            return;

        }

        protected void UpdateMSG()
        {


            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Day End Process Sucessfully Done');", true);
            return;

        }
        protected void UnpostMSG()
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Have a Un-Post Transaction');", true);
            return;

        }

        protected void UnpostSalMSG()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Current Month Salary Not Posted');", true);
            return;

        }
        protected void btnProcess_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtDayEnd.Text == string.Empty)
                {
                    txtDayEnd.Focus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input END OF DAY');", true);
                    return;

                }

                if (txtDayEnd.Text == "END OF DAY")
                {
                    CtrlProcFlag.Text = "0";

                    CheckUnPostTransaction();
                    if (CtrlTranStat.Text == "1")
                    {
                        return;
                    }


                    //VerifyDayConfirmtion();
                    //if (CtrlProcDoneFlag.Text == "1")
                    //{
                    //    return;
                    //}



                    ProcessEOD();
                }
                else
                {
                    txtDayEnd.Text = string.Empty;
                    txtDayEnd.Focus();

                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input END OF DAY');", true);
                    return;
                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.btnProcess_Click Problem');</script>");
                //throw ex;
            }
        }

        protected void ProcessEOD()
        {
            A2ZERPSYSPRMDTO dto = A2ZERPSYSPRMDTO.GetParameterValue();

            CtrlBackUpStat.Text = Converter.GetString(dto.PrmBackUpStat);
            CtrlSalPostStat.Text = Converter.GetString(dto.PrmSalPostStat);

            CtrlLoanIntStat.Text = Converter.GetString(dto.PrmLoanChargeStat);


            

            if (CtrlBackUpStat.Text == "0")
            {
                BackUpMSG();
                return;
            }
            else
            {
                int periodFlag = 1;

                if (lblEndOfMonth.Visible)
                {
                    periodFlag = 2;
                }
                if (lblYearEnd.Visible)
                {
                    periodFlag = 3;
                }

                //if (CtrlSalPostStat.Text == "0" && periodFlag != 1)
                //{
                //    UnpostSalMSG();
                //    return;
                //}

                var prm = new object[2];
                prm[0] = hdnID.Text;
                prm[1] = periodFlag;

                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_PeriodEnd", prm, "A2ZACOMS"));

                if (result == 0)
                {
                    
                    //if (periodFlag == 3)
                    //{
                    //    CreateNewYearDatabase();
                    //}
                    //btnProcess.BackColor = Color.Black;
                    btnProcess.Enabled = false;
                    UpdateBackUpStat();
                   
                    UpdateEODStat();

                    if (lblYearClose.Text == "1")
                    {
                        UpdateYearEndStat();
                    }



                    //hdnID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));

                    string sql = "UPDATE A2ZERPSYSPRM SET PrmNoOfUser = 0";
                    int rowEff = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sql, "A2ZHKOMS"));
                    
                    string sqlQuery = "UPDATE A2ZSYSIDS SET IdsLogInFlag = 0 WHERE IdsNo = " + hdnID.Text;
                    int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQuery, "A2ZACOMS"));

                    if (rowEffect > 0)
                    {
                        CtrlProcFlag.Text = "1";
                        txtDayEnd.Text = string.Empty;
                        UpdateMSG();

                        //Response.Redirect("A2ZERPModule.aspx");
                        //Response.Redirect("A2ZStartUp.aspx");
                    }


                }

            }
        }


        protected void InitialSalPostStat()
        {
            try
            {
                Int16 BStat = 0;

                int roweffect = A2ZERPSYSPRMDTO.UpdateSalPostStat(BStat);
                if (roweffect > 0)
                {

                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.UpdateSalPostStat Problem');</script>");
                //throw ex;
            }

        }

        protected void UpdateBackUpStat()
        {
            try
            {
                Int16 BStat = 0;

                int roweffect = A2ZERPSYSPRMDTO.UpdateBackUpStat(BStat);
                if (roweffect > 0)
                {

                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.UpdateBackUpStat Problem');</script>");
                //throw ex;
            }

        }

       

        protected void UpdateEODStat()
        {
            try
            {
                Int16 BStat = 1;

                int roweffect = A2ZERPSYSPRMDTO.UpdateEODStat(BStat);
                if (roweffect > 0)
                {

                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.UpdateEODStat Problem');</script>");
                //throw ex;
            }

        }

        protected void UpdateYearEndStat()
        {
            try
            {
                Int16 BStat = 1;

                int roweffect = A2ZERPSYSPRMDTO.UpdateYearEndStat(BStat);
                if (roweffect > 0)
                {

                }

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.UpdateEODStat Problem');</script>");
                //throw ex;
            }

        }
        protected void UpdateDatabseFile()
        {
            try
            {
                A2ZDATABASEDTO objDTO = new A2ZDATABASEDTO();

                objDTO.DatabaseName = Converter.GetString(hdndbname.Text);

                int roweffect = A2ZDATABASEDTO.InsertInformation(objDTO);
                if (roweffect > 0)
                {

                }



            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.UpdateEODStat Problem');</script>");
                //throw ex;
            }

        }

        public void CreateNewYearDatabase()
        //protected void btnProcess_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string DatabaseName = "";
                int year = Converter.GetInteger(lblNewYear.Text);
                int yearr = year + 1;
                string financialYr = Converter.GetString(yearr);
                DatabaseName = "A2ZACOMST" + financialYr;
                hdndbname.Text = DatabaseName;
                //string datapath = "E:/";
                string str = "CREATE DATABASE " + DatabaseName + " ON PRIMARY " +
                         "(NAME = MyDataBaseName, " +
                         "FILENAME = '" + hdndatapath.Text + "" + DatabaseName + ".mdf', " +
                         "SIZE = 3MB, MAXSIZE = UNLIMITED, FILEGROWTH = 10%) " +
                         "LOG ON (NAME = MyDataBaseName_log, " +
                        "FILENAME = '" + hdndatapath.Text + "" + DatabaseName + "_log.ldf', " +
                        "SIZE = 1MB, " +
                        "MAXSIZE = UNLIMITED, " +
                        "FILEGROWTH = 10%)";

                //string str = "CREATE DATABASE " + DatabaseName + "";
                SqlCommand cmd = new SqlCommand(str, con);
                cmd.ExecuteNonQuery();
                SqlConnection.ClearAllPools();


                //var prm = new object[1];
                //prm[0] = DatabaseName;
                //int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_GLYearEnd",prm, "A2ZGLMCUS"));
                con.Dispose();
                con.Close();
                string CSOpBaltblename = "A2ZACOPBALANCE";
                ACOPCreate(CSOpBaltblename);

                string Trntblename = "A2ZTRANSACTION";
                TrnCreate(Trntblename);

                UpdateDatabseFile();


            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.Process End Problem');</script>");
                //throw ex;
            }



        }

        public void ACOPCreate(string TName)
        {
            SqlConnection conn = new SqlConnection(DataAccessLayer.Constants.DBConstants.GetConnectionString(hdndbname.Text));

            try
            {
                using (SqlCommand cmd = new SqlCommand("CREATE TABLE [dbo].[" + TName + "]("
                                + "[Id] [int] IDENTITY(1,1) NOT NULL,"
                                + "[TrnDate] [smalldatetime] NOT NULL,"
                                + "[KeyNo] [int] NULL,"
                                + "[AccType] [int] NULL,"
                                + "[AccNo] [bigint] NULL,"
                                + "[TrnPrincAmount] [money] NULL,"
                                + "[TrnIntAmount] [money] NULL,"
                                + "[TrnAmount] [money] NULL,"
                                + "CONSTRAINT [PK_" + TName + "] PRIMARY KEY CLUSTERED "
                                + "("
                                + "[ID] ASC"
                                + ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]"
                                + ") ON [PRIMARY]", conn))
                {
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

       

        public void TrnCreate(string TName)
        {
            SqlConnection conn = new SqlConnection(DataAccessLayer.Constants.DBConstants.GetConnectionString(hdndbname.Text));

            try
            {
                using (SqlCommand cmd = new SqlCommand("CREATE TABLE [dbo].[" + TName + "]("
                                + "[Id] [int] IDENTITY(1,1) NOT NULL,"
                                + "[TrnDate] [smalldatetime] NULL,"
                                + "[VchNo] [nvarchar](20) NULL,"
                                + "[VoucherNo]  [nvarchar](20) NULL,"
                                + "[TrnKeyNo][int] NULL,"
                                + "[CurrencyCode][int] NULL,"
                                + "[AccType] [int] NULL,"
                                + "[AccNo] [bigint] NULL,"
                                + "[FuncOpt]  [smallint] NULL,"
                                + "[FuncOptDesc] [nvarchar](100) NULL,"
                                + "[PayType] [int] NULL,"
                                + "[TrnType] [tinyint] NULL,"
                                + "[TrnDrCr] [tinyint] NULL,"
                                + "[TrnPrincDebit] [money] NULL ,"
                                + "[TrnPrincCredit] [money] NULL ,"
                                + "[TrnIntDebit] [money] NULL ,"
                                + "[TrnIntCredit] [money] NULL ,"
                                + "[TrnDebitAmt] [money] NULL ,"
                                + "[TrnCreditAmt] [money] NULL,"
                                + "[TrnDebitWeight] [money] NULL,"
                                + "[TrnCreditWeight] [money] NULL,"
                                + "[TrnIntRate] [money] NULL,"
                                + "[ExchRate] [decimal(18, 8)] NULL,"
                                + "[TrnDesc] [nvarchar](100) NULL,"
                                + "[TrnVchType] [nvarchar](1) NULL,"
                                + "[TrnChqPrx] [nvarchar](7) NULL,"
                                + "[TrnChqNo] [nvarchar](15) NULL,"
                                + "[TrnBankChqNo] [nvarchar](50) NULL,"
                                + "[TrnCSGL] [tinyint] NULL,"
                                + "[AccKeyNo] [int] NULL,"
                                + "[AccDebitAmt] [money] NULL,"
                                + "[AccCreditAmt] [money] NULL,"
                                + "[AccDebitWeight] [money] NULL,"
                                + "[AccCreditWeight] [money] NULL,"
                                + "[TrnDiscountAmt] [money] NULL,"
                                + "[TrnFlag] [tinyint] NULL,"
                                + "[TrnProcStat] [tinyint] NULL,"
                                + "[TrnSysUser] [tinyint] NULL,"
                                + "[UserID] [int] NULL,"
                                + "[VerifyUserID] [int] NULL,"  
                                + "[CreateDate] [datetime] NULL,"
                                + "[TrnSwitch] [tinyint] NULL,"
                                + "CONSTRAINT [PK_" + TName + "] PRIMARY KEY CLUSTERED "
                                + "("
                                + "[ID] ASC"
                                + ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]"
                                + ") ON [PRIMARY]", conn))
                {
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                }

            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}