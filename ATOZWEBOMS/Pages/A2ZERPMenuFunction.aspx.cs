using ATOZWEBOMS.WebSessionStore;
using DataAccessLayer.BLL;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.DTO.HouseKeeping;
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
using System.Drawing;

namespace ATOZWEBOMS.Pages
{
    public partial class A2ZERPMenuFunction : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    //A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                    //lblItemNoControl.Text = Converter.GetString(dto.PrmItemNoControl);
                    divMain.Visible = true;
                    MainGridviewInfo();
                    //  
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        protected void gvMainInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == 0)
                    e.Row.Style.Add("height", "40px");
                //e.Row.Style.Add("height", "110px");
                //e.Row.Style.Add("top", "-1600px");
                e.Row.Style.Add("top", "-1600px");


                //Label lblHeadCode = (Label)e.Row.FindControl("lblHeadCode");
                //Label lblSubHeadCode = (Label)e.Row.FindControl("lblSubHeadCode");
                //Label lblDetailCode = (Label)e.Row.FindControl("lblDetailCode");
                //Button MainInformation = (Button)e.Row.FindControl("MainInformation");

                //if (lblSubHeadCode.Text == "0" && lblDetailCode.Text == "0")
                //{
                //    MainInformation.ForeColor = Color.Red;
                //}
                //if (lblSubHeadCode.Text != "0" && lblDetailCode.Text == "0")
                //{
                //    MainInformation.ForeColor = Color.Blue;
                //}
                //if (lblSubHeadCode.Text != "0" && lblDetailCode.Text != "0")
                //{
                //    MainInformation.ForeColor = Color.White;
                //}
            }
        }
        protected void MainGridviewInfo()
        {
            try
            {
                //string strQuery = @"SELECT HeadFuncCode,SubHeadFuncCode,DetailFuncCode,FuncDescription FROM A2ZERPFUNCMENU ORDER BY  HeadFuncCode, SubHeadFuncCode, DetailFuncCode, FuncDescription";

                string strquery = @"SELECT DetailFuncCode,FuncDescription FROM A2ZERPFUNCMENU  WHERE  DetailFuncCode  = 0";
                gvMainInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(strquery, gvMainInfo, "A2ZACOMS");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    
        protected void MainInformation_Click(object sender, EventArgs e)
        {
            try
            {
                Button b = (Button)sender;
                GridViewRow r = (GridViewRow)b.NamingContainer;
                Label code = (Label)gvMainInfo.Rows[r.RowIndex].Cells[0].FindControl("lblFuncCode");
                lblFunc.Text = Converter.GetString(code.Text);
                divMain.Visible = false;
            }

            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.BtnEdit_Click Problem');</script>");
                //throw ex;
            }

        }
        protected void MainBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx", false);
        }
       
        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx", false);
        }

    }
}