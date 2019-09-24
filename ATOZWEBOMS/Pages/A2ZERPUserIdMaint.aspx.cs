using System;
using System.Drawing;

using DataAccessLayer.Utility;
using DataAccessLayer.DTO;
using ATOZWEBOMS.WebSessionStore;
using System.Web.UI;

using System.Data;
using DataAccessLayer.DTO.HouseKeeping;

namespace ATOZWEBOMS.Pages
{
    public partial class A2ZERPUserIdMaint : System.Web.UI.Page
    {
        /* © 2013 AtoZ Computer Services */
        //Oni: September 7, 2013
        // Last modified by Musleh for Call Function using DTO
        /// <summary>
        /// Thsi page is for User ID.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            DivMessage.Visible = false;
            try
            {
                if (IsPostBack)
                {
                    //todo

                }
                else
                {
                    string strQuery;
                    

                    strQuery = "SELECT IdsLevel,LevelDesc FROM A2ZIDLEVEL";
                    ddlIdLevel = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(strQuery, ddlIdLevel, "A2ZHKOMS");


                    var p = A2ZERPSYSPRMDTO.GetParameterValue();
                    lblPrmUnitFlag.Text =Converter.GetString(p.PrmUnitFlag);
                    lblPrmUnitNo.Text = Converter.GetString(p.PrmUnitNo);
                    


                    //PerNoDropdown();
                    //GLCashCodeDropdown();

                    //UserBranchDropdown();

                    ChkSODflag.Checked = false;
                    ChkVPrintflag.Checked = false;
                    ChkMobileflag.Checked = false;
                    //ChkGenAutoVch.Checked = false;

                    //lblManagment.Visible = false;
                    //txtManagment.Visible = false;

                    ddlIdLevel.SelectedValue = "10";

                    

                    //lblManagment.Visible = false;
                    //txtManagment.Visible = false;

                }
                txtIdNo.Focus();
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.Load Problem');</script>");
                
                //throw ex;
            }
        }

        //protected void PerNoDropdown()
        //{
        //    string strQuery1 = "SELECT EmpCode,EmpName FROM A2ZEMPLOYEE";
        //    ddlPerNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(strQuery1, ddlPerNo, "A2ZHRCUBS");
           

        //}

        
        //protected void GLCashCodeDropdown()
        //{

        //    string sqlquery = "SELECT GLAccNo,GLAccDesc from A2ZCGLMST where GLRecType = 2 and GLSubHead = 10101000 ORDER BY GLAccDesc ASC";
        //    ddlGLCashCode = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlGLCashCode, "A2ZGLCUBS");

        //}
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {

                //if (txtGlCashCode.Text == string.Empty)
                //{
                //    txtGlCashCode.Focus();
                //    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input GL Cash Code');", true);
                //    return;
                //}

                
                


            string idName = string.Empty;
            Int32 EmpCode = 0;
            int noOfRowEffected = 0;

            
                A2ZSYSIDSDTO dto = new A2ZSYSIDSDTO();

                dto.IdsNo = Converter.GetInteger(txtIdNo.Text);
                
                
                dto.IdsLevel = Converter.GetSmallInteger(ddlIdLevel.SelectedValue);
                
                dto.UserId = Converter.GetInteger(SessionStore.GetValue(Params.SYS_USER_ID));
                //dto.GLCashCode = Converter.GetInteger(txtGlCashCode.Text);

                
                dto.SODflag = Converter.GetBoolean(ChkSODflag.Checked);
                dto.VPrintflag = Converter.GetBoolean(ChkVPrintflag.Checked);
                dto.IdsMobile = Converter.GetBoolean(ChkMobileflag.Checked);

                //dto.AutoVchflag = Converter.GetBoolean(ChkGenAutoVch.Checked);

                
                    dto.IdsName = Converter.GetString(txtUserName.Text);
               

                noOfRowEffected = A2ZSYSIDSDTO.InsertNewIdInformation(dto);

                if (noOfRowEffected > 0)
                {

                    //string sqlQuery = "INSERT INTO A2ZUSERCASHCODE (IdsNo,FromCashCode,FromCashCodeDesc) VALUES ('" + txtIdNo.Text + "','" + txtGlCashCode.Text + "','" + ddlGLCashCode.SelectedItem.Text + "')";
                    //int rowEffiect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQuery, "A2ZACOMS"));

                                       
                    UpdateMSG();
                    //ShowMessage("Data has been saved successfully.", Color.Green);
                    ClearInformation();
                }
                else
                {
                    NotUpdateMSG();
                    //ShowMessage("Sorry!!Data has not been saved.", Color.Red);
                }
            }

            catch (Exception)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.Add Problem');</script>");
                
                //throw;
            }
        }

        protected void UpdateMSG()
        {
            String csname1 = "PopupScript";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;

            if (!cs.IsStartupScriptRegistered(cstype, csname1))
            {
                String cstext1 = "alert('Data Saved Successfully');";
                cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            }

            return;

        }

        protected void NotUpdateMSG()
        {
            String csname1 = "PopupScript";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;

            if (!cs.IsStartupScriptRegistered(cstype, csname1))
            {
                String cstext1 = "alert('Sorry!!Data has not been Saved');";
                cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            }

            return;

        }

        protected void UsedMSG()
        {
            String csname1 = "PopupScript";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;

            if (!cs.IsStartupScriptRegistered(cstype, csname1))
            {
                String cstext1 = "alert('Id Already Use');";
                cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            }

            return;

        }
        private void ClearInformation()
        {
            txtIdNo.Text = string.Empty;
            //txtGlCashCode.Text = string.Empty;
            //txtPerNo.Text = string.Empty;
            //ddlPerNo.SelectedIndex = 0;
            //ddlUserBranch.SelectedIndex = 0;
            //ddlGLCashCode.SelectedIndex = 0;
            ChkSODflag.Checked = false;
            ChkVPrintflag.Checked = false;
            ChkMobileflag.Checked = false;
            //ChkGenAutoVch.Checked = false;

            txtUserName.Text = string.Empty;
            //txtManagment.Text = string.Empty;

           

        }

        protected void ddlIdLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlIdLevel.SelectedValue == "40")
                {
                    //lblPerNo.Visible = false;
                    //txtPerNo.Visible = false;
                    //ddlPerNo.Visible = false;
                    //lblUserBranch.Visible = false;
                    //ddlUserBranch.Visible = false;

                    lblSODfag.Visible = true;
                    ChkSODflag.Visible = true;
                    lblVchPrintfag.Visible = true;
                    ChkVPrintflag.Visible = true;
                    ChkMobileflag.Visible = true;
                    //ChkGenAutoVch.Visible = true;

                    lblUserName.Visible = true;
                    txtUserName.Visible = true;

                }
                else
                {
                    //lblPerNo.Visible = true;
                    //txtPerNo.Visible = true;
                    //ddlPerNo.Visible = true;

                    //if (lblPrmUnitFlag.Text != "1")
                    //{
                    //    lblUserBranch.Visible = true;
                    //    ddlUserBranch.Visible = true;
                    //}
                    //else
                    //{
                    //    lblUserBranch.Visible = false;
                    //    ddlUserBranch.Visible = false;
                    //}
                    
                    lblSODfag.Visible = true;
                    ChkSODflag.Visible = true;
                    lblVchPrintfag.Visible = true;
                    ChkVPrintflag.Visible = true;
                    ChkMobileflag.Visible = true;
                    //ChkGenAutoVch.Visible = true;


                    lblUserName.Visible = true;
                    txtUserName.Visible = true;


                    //lblManagment.Visible = false;
                    //txtManagment.Visible = false;
                }
               
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.Select Problem');</script>");
                
                //throw ex;
            }
        }


        protected void txtIdNo_TextChanged(object sender, EventArgs e)
        {
            A2ZSYSIDSDTO dto = A2ZSYSIDSDTO.GetUserInformation(Converter.GetInteger(txtIdNo.Text), "A2ZHKOMS");

            if (dto.IdsNo != 0)
            {
                UsedMSG();
                //ShowMessage("Id Already Use.", Color.Red);
                txtIdNo.Text = string.Empty;

                return;
            }
            else 
            {
                txtUserName.Focus();
            }
        }

        protected void ShowMessage(string message, Color clr)
        {
            DivMessage.Visible = true;

            lblMessage.Text = message;
            lblMessage.ForeColor = clr;
            lblMessage.Visible = true;

            DivMain.Attributes.CssStyle.Add("opacity", "0.1");
            DivMessage.Style.Add("Top", "250px");
            DivMessage.Style.Add("Right", "500px");
            DivMessage.Style.Add("position", "absolute");
            DivMessage.Attributes.CssStyle.Add("opacity", "100");

        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx", false);
        }

       
        protected void btnHideMessageDiv_Click(object sender, EventArgs e)
        {
            DivMain.Attributes.CssStyle.Add("opacity", "100");
            DivMessage.Visible = false;
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            DivGridView.Visible = true;

            string strQuery = @"SELECT dbo.A2ZSYSIDS.IdsNo, dbo.A2ZSYSIDS.IdsName,
                             dbo.A2ZIDLEVEL.LevelDesc, dbo.A2ZSYSIDS.EmpCode,dbo.A2ZSYSIDS.GLCashCode          
                             FROM dbo.A2ZSYSIDS INNER JOIN
                              dbo.A2ZIDLEVEL ON dbo.A2ZSYSIDS.IdsLevel = dbo.A2ZIDLEVEL.IdsLevel";
            gvUserIdInfromation = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(strQuery, gvUserIdInfromation, "A2ZHKOMS");
        }

        //protected void txtGlCashCode_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {

        //        if (txtGlCashCode.Text != string.Empty)
        //        {
        //            int GLCode = Converter.GetInteger(txtGlCashCode.Text);
        //            A2ZCGLMSTDTO getDTO = (A2ZCGLMSTDTO.GetInformation(GLCode));

        //            if (getDTO.GLAccNo > 0)
        //            {
        //                txtGlCashCode.Text = Converter.GetString(getDTO.GLAccNo);
        //                ddlGLCashCode.SelectedValue = Converter.GetString(getDTO.GLAccNo);
        //            }
        //            else
        //            {
        //                txtGlCashCode.Text = string.Empty;
        //                txtGlCashCode.Focus();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.txtGlCashCode_TextChanged Problem');</script>");
        //        //throw ex;
        //    }
        //}

        //protected void ddlGLCashCode_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {

        //        if (ddlGLCashCode.SelectedValue != "-Select-")
        //        {

        //            int GLCode = Converter.GetInteger(ddlGLCashCode.SelectedValue);
        //            A2ZCGLMSTDTO getDTO = (A2ZCGLMSTDTO.GetInformation(GLCode));

        //            if (getDTO.GLAccNo > 0)
        //            {
        //                txtGlCashCode.Text = Converter.GetString(getDTO.GLAccNo);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.ddlGLCashCode_SelectedIndexChanged Problem');</script>");
        //        //throw ex;
        //    }
        //}

        private void DeplicatePerNoMSG()
        {
            String csname1 = "PopupScript";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;

            if (!cs.IsStartupScriptRegistered(cstype, csname1))
            {
                String cstext1 = "alert('Per No. Already selected in Other User Ids.');";
                cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            }
            return;
        }

        //protected void txtPerNo_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {

        //        if (txtPerNo.Text != string.Empty)
        //        {
        //            int PerNo = Converter.GetInteger(txtPerNo.Text);
        //            A2ZEMPLOYEEDTO getDTO = (A2ZEMPLOYEEDTO.GetInformation(PerNo));

        //            if (getDTO.EmployeeID > 0)
        //            {               
        //              string qry = "SELECT IdsNo FROM A2ZSYSIDS where EmpCode='" + PerNo + "'";
        //              DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZHKCUBS");
        //              if (dt.Rows.Count > 0)
        //              {
        //                  DeplicatePerNoMSG();
        //                  txtPerNo.Text = string.Empty;
        //                  txtPerNo.Focus();
        //                  return;
        //              }    
        //                txtPerNo.Text = Converter.GetString(getDTO.EmployeeID);
        //                ddlPerNo.SelectedValue = Converter.GetString(getDTO.EmployeeID);
        //                lblIdsName.Text = Converter.GetString(getDTO.EmployeeName);
        //                txtGlCashCode.Focus();
        //            }
        //            else
        //            {
        //                txtPerNo.Text = string.Empty;
        //                txtPerNo.Focus();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.txtPerNo_TextChanged Problem');</script>");
        //        //throw ex;
        //    }
        //}

        //protected void ddlPerNo_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {

        //        if (ddlPerNo.SelectedValue != "-Select-")
        //        {

        //            int PerNo = Converter.GetInteger(ddlPerNo.SelectedValue);
        //            A2ZEMPLOYEEDTO getDTO = (A2ZEMPLOYEEDTO.GetInformation(PerNo));

        //            if (getDTO.EmployeeID > 0)
        //            {
        //                string qry = "SELECT IdsNo FROM A2ZSYSIDS where EmpCode='" + PerNo + "'";
        //                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZHKCUBS");
        //                if (dt.Rows.Count > 0)
        //                {
        //                    DeplicatePerNoMSG();
        //                    txtPerNo.Text = string.Empty;
        //                    txtPerNo.Focus();
        //                    return;
        //                }    
                        
        //                txtPerNo.Text = Converter.GetString(getDTO.EmployeeID);
        //                lblIdsName.Text = Converter.GetString(getDTO.EmployeeName);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.ddlPerNo_SelectedIndexChanged Problem');</script>");
        //        //throw ex;
        //    }
        //}

        


    }
}
