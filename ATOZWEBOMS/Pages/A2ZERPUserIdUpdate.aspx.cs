using DataAccessLayer.DTO;
using DataAccessLayer.DTO.CustomerServices;

using DataAccessLayer.DTO.HouseKeeping;

using DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ATOZWEBOMS.Pages
{
    public partial class A2ZERPUserIDUpdate : System.Web.UI.Page
    {
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
                    lblPrmUnitFlag.Text = Converter.GetString(p.PrmUnitFlag);
                    lblPrmUnitNo.Text = Converter.GetString(p.PrmUnitNo);


                    //PerNoDropdown();
                    //GLCashCodeDropdown();

                    //UserBranchDropdown();



                    ChkSODflag.Checked = false;
                    ChkVPrintflag.Checked = false;
                    ChkMobileflag.Checked = false;
                    //ChkGenAutoVch.Checked = false;

                    

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

        //    string sqlquery = "SELECT GLAccNo,GLAccDesc from A2ZCGLMST where GLRecType = 2 and GLSubHead = 10101000";
        //    ddlGLCashCode = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlGLCashCode, "A2ZGLCUBS");

        //}

        
        protected void txtIdNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                A2ZSYSIDSDTO dto = A2ZSYSIDSDTO.GetUserInformation(Converter.GetInteger(txtIdNo.Text), "A2ZHKOMS");

                if (dto.IdsNo != 0)
                {
                    txtIdNo.Text = Converter.GetString(dto.IdsNo);
                    ddlIdLevel.SelectedValue = Converter.GetString(dto.IdsLevel);

                    if (ddlIdLevel.SelectedValue == "40")
                    {
                        txtUserName.Text = Converter.GetString(dto.IdsName);

                        
                       

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
                        txtUserName.Text = Converter.GetString(dto.IdsName);

                        //txtGlCashCode.Text = Converter.GetString(dto.GLCashCode);

                       

                        //if (txtGlCashCode.Text != string.Empty && txtGlCashCode.Text != "0")
                        //{
                        //    ddlGLCashCode.SelectedValue = Converter.GetString(dto.GLCashCode);
                        //}

                        ChkSODflag.Checked = Converter.GetBoolean(dto.SODflag);
                        ChkVPrintflag.Checked = Converter.GetBoolean(dto.VPrintflag);
                        ChkMobileflag.Checked = Converter.GetBoolean(dto.IdsMobile);
                        //ChkGenAutoVch.Checked = Converter.GetBoolean(dto.AutoVchflag);



                        

                        lblSODfag.Visible = true;
                        ChkSODflag.Visible = true;
                        lblVchPrintfag.Visible = true;
                        ChkVPrintflag.Visible = true;
                        ChkMobileflag.Visible = true;
                        //ChkGenAutoVch.Visible = true;

                        lblUserName.Visible = true;
                        txtUserName.Visible = true;

                        lblWriteFlag.Text = "0";

                        

                    }


                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.txtIdNo_TextChanged Problem');</script>");

                //throw ex;
            }
        }

        private void InvalidEmpCode()
        {
            String csname1 = "PopupScript";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;

            if (!cs.IsStartupScriptRegistered(cstype, csname1))
            {
                String cstext1 = "alert('Employee deos not exist for this ID.');";
                cs.RegisterStartupScript(cstype, csname1, cstext1, true);
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


        //protected void Info()
        //{
        //    String strQuery = "SELECT EmpName FROM A2ZEMPLOYEE WHERE EmpCode ='" + Convert.ToInt16(ddlPerNo.SelectedValue) + "'";
        //    lblIdsName.Text = Converter.GetString(DataAccessLayer.BLL.CommonManager.Instance.GetScalarValueByQuery(strQuery, "A2ZHRCUBS"));
        //    //strQuery = "SELECT EmpFatherName FROM A2ZEMPLOYEE WHERE EmpCode ='" + Convert.ToInt16(ddlIdName.SelectedValue) + "'";
        //    //txtFatherName.Text = Converter.GetString(DataAccessLayer.BLL.CommonManager.Instance.GetScalarValueByQuery(strQuery, "A2ZHRMCUS"));

        //    ////Employee Depertment

        //    //strQuery = "SELECT  DepartmentCode FROM A2ZEMPLOYEE where EmpCode='" + Convert.ToInt16(ddlIdName.SelectedValue) + "'";
        //    //txtDp.Text = Converter.GetString(DataAccessLayer.BLL.CommonManager.Instance.GetScalarValueByQuery(strQuery, "A2ZHRMCUS"));

        //    //strQuery = "SELECT DepartmentName FROM A2ZDEPARTMENT where DepartmentCode= '" + txtDp.Text + "'";
        //    //txtDepartMent.Text = Converter.GetString(DataAccessLayer.BLL.CommonManager.Instance.GetScalarValueByQuery(strQuery, "A2ZHRMCUS"));

        //    //// Employee Designation

        //    //strQuery = "SELECT  DesigCode FROM A2ZEMPLOYEE where EmpCode='" + Convert.ToInt16(ddlIdName.SelectedValue) + "'";
        //    //txtDn.Text = Converter.GetString(DataAccessLayer.BLL.CommonManager.Instance.GetScalarValueByQuery(strQuery, "A2ZHRMCUS"));

        //    //strQuery = "SELECT DesigDescription FROM A2ZDESIGNATION where DesigCode= '" + txtDn.Text + "'";
        //    //txtDesignation.Text = Converter.GetString(DataAccessLayer.BLL.CommonManager.Instance.GetScalarValueByQuery(strQuery, "A2ZHKMCUS"));

        //    //// Employee Location

        //    //strQuery = "SELECT  LocationCode FROM A2ZEMPLOYEE where EmpCode='" + Convert.ToInt16(ddlIdName.SelectedValue) + "'";
        //    //txtloc.Text = DataAccessLayer.Utility.Converter.GetString(DataAccessLayer.BLL.CommonManager.Instance.GetScalarValueByQuery(strQuery, "A2ZHRMCUS"));

        //    //strQuery = "SELECT LocationName FROM A2ZLOCATION where LocationCode= '" + txtloc.Text + "'";
        //    //txtLocation.Text = DataAccessLayer.Utility.Converter.GetString(DataAccessLayer.BLL.CommonManager.Instance.GetScalarValueByQuery(strQuery, "A2ZHRMCUS"));

        //    // Employee Section

        //    //strQuery = "SELECT  SectionCode FROM A2ZEMPLOYEE where EmpCode='" + Convert.ToInt16(ddlIdName.SelectedValue) + "'";
        //    //txtsec.Text = DataAccessLayer.Utility.Converter.GetString(DataAccessLayer.BLL.CommonManager.Instance.GetScalarValueByQuery(strQuery, "A2ZHRMCUS"));

        //    //strQuery = "SELECT SectionName FROM A2ZSECTION where LocationCode= '" + txtsec.Text + "'";
        //    //txtSection.Text = DataAccessLayer.Utility.Converter.GetString(DataAccessLayer.BLL.CommonManager.Instance.GetScalarValueByQuery(strQuery, "A2ZHRMCUS"));
        //}


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

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                //if (txtGlCashCode.Text == string.Empty)
                //{
                //    txtGlCashCode.Focus();
                //    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input GL Cash Code');", true);
                //    return;
                //}


                

                A2ZSYSIDSDTO dto = new A2ZSYSIDSDTO();
                dto.IdsNo = Converter.GetInteger(txtIdNo.Text);
                dto.IdsLevel = Converter.GetSmallInteger(ddlIdLevel.SelectedValue);
                //dto.EmpCode = Converter.GetSmallInteger(ddlPerNo.SelectedValue);
                //dto.IdsName = Converter.GetString(lblIdsName.Text);
                //dto.IdsFlag = Converter.GetSmallInteger(ddlPermission.SelectedValue);
                //dto.GLCashCode = Converter.GetInteger(txtGlCashCode.Text);

                


                dto.SODflag = Converter.GetBoolean(ChkSODflag.Checked);
                dto.VPrintflag = Converter.GetBoolean(ChkVPrintflag.Checked);
                dto.IdsMobile = Converter.GetBoolean(ChkMobileflag.Checked);

                //dto.AutoVchflag = Converter.GetBoolean(ChkGenAutoVch.Checked);

                dto.IdsName = Converter.GetString(txtUserName.Text);

              


                int noOfRowEffected = A2ZSYSIDSDTO.Update(dto);


                //string sqlQuery = "INSERT INTO A2ZUSERCASHCODE (IdsNo,FromCashCode,FromCashCodeDesc) VALUES ('" + txtIdNo.Text + "','" + txtGlCashCode.Text + "','" + ddlGLCashCode.SelectedItem.Text + "')";

                //string strQuery = "UPDATE A2ZUSERCASHCODE SET  FromCashCode = '" + txtGlCashCode.Text + "',FromCashCodeDesc = '" + ddlGLCashCode.SelectedItem.Text + "' WHERE IdsNo='" + txtIdNo.Text + "'";
                //int rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSCUBS"));


                
                //ShowMessage("Data has been Update successfully.", Color.Green);
                ClearInformation();
                UpdateMSG();

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.btnUpdate_Click Problem');</script>");

                //throw ex;
            }
        }


        protected void UpdateMSG()
        {
            String csname1 = "PopupScript";
            Type cstype = GetType();
            ClientScriptManager cs = Page.ClientScript;

            if (!cs.IsStartupScriptRegistered(cstype, csname1))
            {
                String cstext1 = "alert('Data has been Update successfully');";
                cs.RegisterStartupScript(cstype, csname1, cstext1, true);
            }

            return;

        }
        private void ClearInformation()
        {
            txtIdNo.Text = string.Empty;
            //txtPerNo.Text = string.Empty;
            //ddlPerNo.SelectedIndex = 0;
            ddlIdLevel.SelectedIndex = 0;



            //ddlUserBranch.SelectedIndex = 0;
            //ddlGLCashCode.SelectedIndex = 0;
            //ddlPermission.SelectedIndex = 0;
            //txtGlCashCode.Text = string.Empty;

            ChkSODflag.Checked = false;
            ChkVPrintflag.Checked = false;
            ChkMobileflag.Checked = false;
            //ChkGenAutoVch.Checked = false;

            txtUserName.Text = string.Empty;

           


            txtIdNo.Focus();

            //txtLocation.Text = string.Empty;
            //txtSection.Text = string.Empty;
            //txtDesignation.Text = string.Empty;
            //txtDepartMent.Text = string.Empty;
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
        //                ddlPerNo.SelectedValue = Converter.GetString(getDTO.EmployeeID);
        //                lblIdsName.Text = Converter.GetString(getDTO.EmployeeName);
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

        protected void ddlIdLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlIdLevel.SelectedValue == "40")
            {
                //lblPerNo.Visible = false;
                //txtPerNo.Visible = false;
                //ddlPerNo.Visible = false;
                //lblUserBranch.Visible = false;
                //ddlUserBranch.Visible = false;

                lblSODfag.Visible = false;
                ChkSODflag.Visible = false;
                lblVchPrintfag.Visible = false;
                ChkVPrintflag.Visible = false;
                ChkMobileflag.Visible = false;
                //ChkGenAutoVch.Visible = false;

                lblUserName.Visible = true;
                txtUserName.Visible = true;

                //    lblManagment.Visible = true;
                //    txtManagment.Visible = true;
            }
            else
            {
                //lblPerNo.Visible = true;
                //txtPerNo.Visible = true;
                //ddlPerNo.Visible = true;

                


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


    }
}