using System;
using System.Web.UI.WebControls;
using DataAccessLayer.DTO.HouseKeeping;
using DataAccessLayer.Utility;
using System.Drawing;

using ATOZWEBOMS.WebSessionStore;
using DataAccessLayer.DTO;
using System.Web.UI;
using System.Data;
using DataAccessLayer.DTO.CustomerServices;



namespace ATOZWEBOMS.Pages
{
    public partial class A2ZERPUserAccessibility : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DivMessage.Visible = false;
            try
            {
                if (IsPostBack)
                {

                }
                else
                {
                    //ddlModule = A2ZERPMODULEDTO.GetDropDownList(ddlModule);

                    IdsDropdown();
                    txtIdsNo.Focus();

                   
                    btnMark1.Visible = false;
                    btnUnMark1.Visible = false;


                    A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                    

                    //lblMemApp1.Text = Converter.GetString(dto.PrmMemApplication1);
                    //lblMemApp2.Text = Converter.GetString(dto.PrmMemApplication2);
                   


                }
            }
            catch (Exception ex)
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Err.Load Problem');</script>");
                //throw ex;
            }

        }

        private void IdsDropdown()
        {
            string sqlquery = "SELECT IdsNo,IdsName from A2ZSYSIDS ORDER BY IdsName ASC";
            ddlIdsNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlIdsNo, "A2ZHKOMS");
        }

        private void IDsNotFoundMsg()
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Ids Does not exist');", true);
            return;

        }
        protected void txtIdsNo_TextChanged(object sender, EventArgs e)
        {
            int idno = Converter.GetInteger(txtIdsNo.Text);
            A2ZSYSIDSDTO dto = new A2ZSYSIDSDTO();
            dto = A2ZSYSIDSDTO.GetUserInformation(idno, "A2ZHKOMS");
            if (dto.IdsNo > 0)
            {
                ddlIdsNo.SelectedValue = Converter.GetString(dto.IdsNo);

                string sqlquery = "SELECT ModuleNo,ModuleName from A2ZSYSMODULECTRL WHERE IDSNO = '" + ddlIdsNo.SelectedValue + "' ";
                ddlModule = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlModule, "A2ZHKOMS");

            }
            else
            {
                IDsNotFoundMsg();
                ddlIdsNo.SelectedValue = "-Select-";
                txtIdsNo.Text = string.Empty;
                txtIdsNo.Focus();
                return;
            }


            ddlIdsNo.SelectedValue = Converter.GetString(txtIdsNo.Text);
        }

        protected void ddlIdsNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtIdsNo.Text = Converter.GetString(ddlIdsNo.SelectedValue);

            string sqlquery = "SELECT ModuleNo,ModuleName from A2ZSYSMODULECTRL WHERE IDSNO = '" + ddlIdsNo.SelectedValue + "' ";
            ddlModule = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlModule, "A2ZHKOMS");

        }


        protected void ddlModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlIdsNo.SelectedValue == "-Select-")
                {
                    ddlModule.SelectedValue = "-Select-";
                    ddlIdsNo.Focus();
                    return;
                }


                
                DivGridTitleView.Visible = true;

                int result = 0;
                string strQuery = string.Empty;


                strQuery = "UPDATE A2ZHKOMS.DBO.A2ZMASTERMENU SET UserId = null, SelectUserId = null";
                result = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHKOMS"));

                if (ddlModule.SelectedValue == "1")
                {
                    strQuery =
                        "UPDATE A2ZHKOMS.DBO.A2ZMASTERMENU SET UserId = (SELECT UserId FROM A2ZACOMS.DBO.A2ZERPMENU WHERE A2ZACOMS.DBO.A2ZERPMENU.UserId = " +
                          ddlIdsNo.SelectedValue + " AND A2ZHKOMS.DBO.A2ZMASTERMENU.MenuNo = A2ZACOMS.DBO.A2ZERPMENU.MenuNo AND " +
                          "A2ZHKOMS.DBO.A2ZMASTERMENU.ModuleNo = " + ddlModule.SelectedValue + " AND MenuUrl IS NOT NULL )";
                    result = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHKOMS"));

                    //strQuery =
                    //    "UPDATE A2ZHKCUBS.DBO.A2ZMASTERMENU SET SelectUserId = (SELECT UserId FROM A2ZCSCUBS.DBO.A2ZERPMENU WHERE A2ZCSCUBS.DBO.A2ZERPMENU.UserId = " +
                    //      ddlIdsNo.SelectedValue + " AND A2ZHKCUBS.DBO.A2ZMASTERMENU.MenuNo = A2ZCSCUBS.DBO.A2ZERPMENU.MenuNo AND " +
                    //      "A2ZHKCUBS.DBO.A2ZMASTERMENU.ModuleNo = " + ddlModule.SelectedValue + " AND MenuParentNo IS NULL AND MenuUrl IS NULL )";
                    //result = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHKCUBS"));

                }


                if (ddlModule.SelectedValue == "2")
                {
                    strQuery =
                        "UPDATE A2ZHKOMS.DBO.A2ZMASTERMENU SET UserId = (SELECT UserId FROM A2ZHKOMS.DBO.A2ZERPMENU WHERE A2ZHKOMS.DBO.A2ZERPMENU.UserId = " +
                          ddlIdsNo.SelectedValue + " AND A2ZHKOMS.DBO.A2ZMASTERMENU.MenuNo = A2ZHKOMS.DBO.A2ZERPMENU.MenuNo AND " +
                          "A2ZHKOMS.DBO.A2ZMASTERMENU.ModuleNo = " + ddlModule.SelectedValue + " AND MenuUrl IS NOT NULL )";
                    result = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHKOMS"));

                    //strQuery =
                    //    "UPDATE A2ZHKCUBS.DBO.A2ZMASTERMENU SET SelectUserId = (SELECT UserId FROM A2ZHKCUBS.DBO.A2ZERPMENU WHERE A2ZHKCUBS.DBO.A2ZERPMENU.UserId = " +
                    //      ddlIdsNo.SelectedValue + " AND A2ZHKCUBS.DBO.A2ZMASTERMENU.MenuNo = A2ZHKCUBS.DBO.A2ZERPMENU.MenuNo AND " +
                    //      "A2ZHKCUBS.DBO.A2ZMASTERMENU.ModuleNo = " + ddlModule.SelectedValue + " AND MenuParentNo IS NULL AND MenuUrl IS NULL )";
                    //result = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHKCUBS"));
                }


                //if (ddlModule.SelectedValue == "3")
                //{
                //    strQuery =
                //        "UPDATE A2ZHKCUBS.DBO.A2ZMASTERMENU SET UserId = (SELECT UserId FROM A2ZBTCUBS.DBO.A2ZERPMENU WHERE A2ZBTCUBS.DBO.A2ZERPMENU.UserId = " +
                //          ddlIdsNo.SelectedValue + " AND A2ZHKCUBS.DBO.A2ZMASTERMENU.MenuNo = A2ZBTCUBS.DBO.A2ZERPMENU.MenuNo AND " +
                //          "A2ZHKCUBS.DBO.A2ZMASTERMENU.ModuleNo = " + ddlModule.SelectedValue + " AND MenuUrl IS NOT NULL )";
                //    result = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHKCUBS"));

                //    //strQuery =
                //    //    "UPDATE A2ZHKCUBS.DBO.A2ZMASTERMENU SET SelectUserId = (SELECT UserId FROM A2ZBTCUBS.DBO.A2ZERPMENU WHERE A2ZBTCUBS.DBO.A2ZERPMENU.UserId = " +
                //    //      ddlIdsNo.SelectedValue + " AND A2ZHKCUBS.DBO.A2ZMASTERMENU.MenuNo = A2ZBTCUBS.DBO.A2ZERPMENU.MenuNo AND " +
                //    //      "A2ZHKCUBS.DBO.A2ZMASTERMENU.ModuleNo = " + ddlModule.SelectedValue + " AND MenuParentNo IS NULL AND MenuUrl IS NULL )";
                //    //result = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHKCUBS"));
                //}



                //if (ddlModule.SelectedValue == "4")
                //{
                //    strQuery =
                //        "UPDATE A2ZHKCUBS.DBO.A2ZMASTERMENU SET UserId = (SELECT UserId FROM A2ZHRCUBS.DBO.A2ZERPMENU WHERE A2ZHRCUBS.DBO.A2ZERPMENU.UserId = " +
                //          ddlIdsNo.SelectedValue + " AND A2ZHKCUBS.DBO.A2ZMASTERMENU.MenuNo = A2ZHRCUBS.DBO.A2ZERPMENU.MenuNo AND " +
                //          "A2ZHKCUBS.DBO.A2ZMASTERMENU.ModuleNo = " + ddlModule.SelectedValue + " AND MenuUrl IS NOT NULL )";
                //    result = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHKCUBS"));

                //    //strQuery =
                //    //    "UPDATE A2ZHKCUBS.DBO.A2ZMASTERMENU SET SelectUserId = (SELECT UserId FROM A2ZHRCUBS.DBO.A2ZERPMENU WHERE A2ZHRCUBS.DBO.A2ZERPMENU.UserId = " +
                //    //      ddlIdsNo.SelectedValue + " AND A2ZHKCUBS.DBO.A2ZMASTERMENU.MenuNo = A2ZHRCUBS.DBO.A2ZERPMENU.MenuNo AND " +
                //    //      "A2ZHKCUBS.DBO.A2ZMASTERMENU.ModuleNo = " + ddlModule.SelectedValue + " AND MenuParentNo IS NULL AND MenuUrl IS NULL )";
                //    //result = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHKCUBS"));

                //}


                //if (ddlModule.SelectedValue == "7")
                //{
                //    strQuery =
                //        "UPDATE A2ZHKMCUS.DBO.A2ZMASTERMENU SET UserId = (SELECT UserId FROM A2ZOBTMCUS.DBO.A2ZERPMENU WHERE A2ZOBTMCUS.DBO.A2ZERPMENU.UserId = " +
                //          ddlIdsNo.SelectedValue + " AND A2ZHKMCUS.DBO.A2ZMASTERMENU.MenuNo = A2ZOBTMCUS.DBO.A2ZERPMENU.MenuNo AND " +
                //          "A2ZHKMCUS.DBO.A2ZMASTERMENU.ModuleNo = " + ddlModule.SelectedValue + ")";
                //    result = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHKMCUS"));
                //}
                //strQuery = " SELECT MenuName,MenuNo,UserId FROM A2ZMASTERMENU WHERE ModuleNo = '" + ddlModule.SelectedValue + "' AND MenuParentNo IS NOT NULL AND MenuUrl IS NOT NULL";


                if (ddlModule.SelectedValue == "1")
                {
                    strQuery = " SELECT MenuName,MenuNo,MenuParentNo,UserId FROM A2ZMASTERMENU WHERE ModuleNo = '" + ddlModule.SelectedValue + "' AND MenuUrl IS NOT NULL ";
                    gvModuleTitle = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(strQuery, gvModuleTitle, "A2ZHKOMS");
                }
                else if (ddlModule.SelectedValue == "2")
                {
                    strQuery = " SELECT MenuName,MenuNo,MenuParentNo,UserId FROM A2ZMASTERMENU WHERE ModuleNo = '" + ddlModule.SelectedValue + "' AND MenuUrl IS NOT NULL ";
                    gvModuleTitle = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(strQuery, gvModuleTitle, "A2ZHKOMS");
                }


                //---------------------------------------------------------------------------------------------------

                //if (lblMemApp1.Text == "0" && lblMemApp2.Text == "0" && ddlModule.SelectedValue == "1")
                //{
                //    strQuery = " SELECT MenuName,MenuNo,MenuParentNo,UserId FROM A2ZMASTERMENU WHERE ModuleNo = '" + ddlModule.SelectedValue + "' AND MenuNo !=112 AND MenuNo !=113 AND MenuNo !=114 AND MenuNo !=115 AND MenuUrl IS NOT NULL ";
                //    gvModuleTitle = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(strQuery, gvModuleTitle, "A2ZHKCUBS");
                //}
                //else if (lblMemApp1.Text == "0" && lblMemApp2.Text == "1" && ddlModule.SelectedValue == "1")
                //{
                // strQuery = " SELECT MenuName,MenuNo,MenuParentNo,UserId FROM A2ZMASTERMENU WHERE ModuleNo = '" + ddlModule.SelectedValue + "' AND MenuNo !=112 AND MenuUrl IS NOT NULL ";
                //    gvModuleTitle = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(strQuery, gvModuleTitle, "A2ZHKCUBS");
                //}
                //else if (lblMemApp1.Text == "1" && ddlModule.SelectedValue == "1")
                //{
                //    strQuery = " SELECT MenuName,MenuNo,MenuParentNo,UserId FROM A2ZMASTERMENU WHERE ModuleNo = '" + ddlModule.SelectedValue + "' AND MenuNo !=111 AND MenuUrl IS NOT NULL ";
                //    gvModuleTitle = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(strQuery, gvModuleTitle, "A2ZHKCUBS");
                //}
                //else if (lblMemApp2.Text == "0" && ddlModule.SelectedValue == "3")
                //{
                //    strQuery = " SELECT MenuName,MenuNo,MenuParentNo,UserId FROM A2ZMASTERMENU WHERE ModuleNo = '" + ddlModule.SelectedValue + "' AND MenuNo !=11 AND MenuNo !=12 AND MenuUrl IS NOT NULL ";
                //    gvModuleTitle = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(strQuery, gvModuleTitle, "A2ZHKCUBS");
                //}
                //else if (lblMemApp2.Text == "1" && ddlModule.SelectedValue == "3")
                //{
                //    strQuery = " SELECT MenuName,MenuNo,MenuParentNo,UserId FROM A2ZMASTERMENU WHERE ModuleNo = '" + ddlModule.SelectedValue + "' AND MenuUrl IS NOT NULL ";
                //    gvModuleTitle = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(strQuery, gvModuleTitle, "A2ZHKCUBS");
                //}
                //else if (ddlModule.SelectedValue == "2")
                //{
                //    strQuery = " SELECT MenuName,MenuNo,MenuParentNo,UserId FROM A2ZMASTERMENU WHERE ModuleNo = '" + ddlModule.SelectedValue + "' AND MenuUrl IS NOT NULL ";
                //    gvModuleTitle = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(strQuery, gvModuleTitle, "A2ZHKCUBS");
                //}

                ShowGridViewWithValue();

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Err.Select Problem');</script>");
                throw ex;
            }
        }




        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                int rowEffiect = 0;
                string strQuery = string.Empty;


                string strQuery1 = "UPDATE A2ZMASTERMENU SET SelectUserId = NULL WHERE ModuleNo = " + ddlModule.SelectedValue;
                int rowEffiect1 = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery1, "A2ZHKOMS"));

                int i = 0;

                foreach (GridViewRow gv in gvModuleTitle.Rows)
                {
                    Boolean m = ((CheckBox)gvModuleTitle.Rows[i].FindControl("chkSelect")).Checked;

                    if (m)
                    {
                        lblMenuNo.Text = gvModuleTitle.Rows[i].Cells[2].Text;
                        if (gvModuleTitle.Rows[i].Cells[3].Text == "&nbsp;")
                        {
                            lblMenuParentNo.Text = "0";
                        }
                        else
                        {
                            lblMenuParentNo.Text = gvModuleTitle.Rows[i].Cells[3].Text;
                        }


                        strQuery = "UPDATE A2ZMASTERMENU SET SelectUserId = '" + ddlIdsNo.SelectedValue + "' WHERE MenuNo = " + lblMenuNo.Text + " AND ModuleNo = " + ddlModule.SelectedValue;
                        rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHKOMS"));


                        strQuery = "UPDATE A2ZMASTERMENU SET SelectUserId = '" + ddlIdsNo.SelectedValue + "' WHERE MenuParentNo = " + lblMenuParentNo.Text + " AND MenuUrl = NULL AND ModuleNo = " + ddlModule.SelectedValue;
                        rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHKOMS"));

                        lblMenuParentNo1.Text = "0";

                        DataTable dt;
                        string sqlquery3 = "SELECT MenuParentNo FROM A2ZMASTERMENU WHERE MenuNo = " + lblMenuParentNo.Text + " AND ModuleNo = " + ddlModule.SelectedValue;
                        dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(sqlquery3, "A2ZHKOMS");
                        if (dt.Rows.Count > 0)
                        {
                            lblMenuParentNo1.Text = Converter.GetString(dt.Rows[0]["MenuParentNo"]);
                            if (lblMenuParentNo1.Text == string.Empty)
                            {
                                lblMenuParentNo1.Text = "0";
                            }
                        }


                        strQuery = "UPDATE A2ZMASTERMENU SET SelectUserId = '" + ddlIdsNo.SelectedValue + "' WHERE MenuNo = " + lblMenuParentNo.Text + " AND ModuleNo = " + ddlModule.SelectedValue;
                        rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHKOMS"));

                        strQuery = "UPDATE A2ZMASTERMENU SET SelectUserId = '" + ddlIdsNo.SelectedValue + "' WHERE MenuNo = " + lblMenuParentNo1.Text + " AND ModuleNo = " + ddlModule.SelectedValue;
                        rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHKOMS"));

                    }
                    i++;

                }
                if (ddlModule.SelectedValue == "1") //FOR CUSTOMER SERVICE
                {

                    strQuery = "DELETE FROM A2ZACOMS.DBO.A2ZERPMENU WHERE UserId = '" + ddlIdsNo.SelectedValue + "' AND ModuleNo = " + ddlModule.SelectedValue;
                    rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZACOMS"));


                    
                    strQuery = "INSERT INTO A2ZACOMS.DBO.A2ZERPMENU (UserId,ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl) SELECT SelectUserId,ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl FROM A2ZHKOMS.DBO.A2ZMASTERMENU WHERE SelectUserId = '" + ddlIdsNo.SelectedValue + "'";
                    rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZACOMS"));
                   
                    

                    //strQuery = "INSERT INTO A2ZCSCUBS.DBO.A2ZERPMENU (UserId,ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl) SELECT " + ddlIdsNo.SelectedValue + ",ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl FROM A2ZHKCUBS.DBO.A2ZMASTERMENU WHERE ModuleNo = " + ddlModule.SelectedValue + " AND (MenuParentNo IS NULL OR MenuUrl IS NULL)";
                    //rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZCSCUBS"));

                    if (rowEffiect > 0)
                    {
                        ShowMessage("Data saved successfully.", Color.Green);
                        ClearInformation();
                    }
                }
                //if (ddlModule.SelectedValue == "2") //FOR GENERAL
                //{

                //    strQuery = "DELETE FROM A2ZGLCUBS.DBO.A2ZERPMENU WHERE UserId = '" + ddlIdsNo.SelectedValue + "' AND ModuleNo = " + ddlModule.SelectedValue;
                //    rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZGLCUBS"));

                //    strQuery = "INSERT INTO A2ZGLCUBS.DBO.A2ZERPMENU (UserId,ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl) SELECT UserId,ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl FROM A2ZHKMCUS.DBO.A2ZMASTERMENU WHERE UserId = '" + ddlIdsNo.SelectedValue + "'";
                //    rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZGLCUBS"));

                //    strQuery = "INSERT INTO A2ZGLCUBS.DBO.A2ZERPMENU (UserId,ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl) SELECT " + ddlIdsNo.SelectedValue + ",ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl FROM A2ZHKMCUS.DBO.A2ZMASTERMENU WHERE ModuleNo = " + ddlModule.SelectedValue + " AND (MenuParentNo IS NULL OR MenuUrl IS NULL)";
                //    rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZGLCUBS"));

                //    if (rowEffiect > 0)
                //    {
                //        ShowMessage("Data saved successfully.", Color.Green);
                //        ClearInformation();
                //    }
                //}

                if (ddlModule.SelectedValue == "2") //HOUSE KEEPING
                {
                    strQuery = "DELETE FROM A2ZHKOMS.DBO.A2ZERPMENU WHERE UserId = '" + ddlIdsNo.SelectedValue + "' AND ModuleNo = " + ddlModule.SelectedValue;
                    rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHKOMS"));

                    strQuery = "INSERT INTO A2ZHKOMS.DBO.A2ZERPMENU (UserId,ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl) SELECT SelectUserId,ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl FROM A2ZHKOMS.DBO.A2ZMASTERMENU WHERE SelectUserId = '" + ddlIdsNo.SelectedValue + "'";
                    rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHKOMS"));



                    //strQuery = "DELETE FROM A2ZHKCUBS.DBO.A2ZERPMENU WHERE UserId = '" + ddlIdsNo.SelectedValue + "' AND ModuleNo = " + ddlModule.SelectedValue;
                    //rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHKCUBS"));

                    //strQuery = "INSERT INTO A2ZHKCUBS.DBO.A2ZERPMENU (UserId,ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl) SELECT UserId,ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl FROM A2ZHKCUBS.DBO.A2ZMASTERMENU WHERE UserId = '" + ddlIdsNo.SelectedValue + "'";
                    //rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHKCUBS"));

                    //strQuery = "INSERT INTO A2ZHKCUBS.DBO.A2ZERPMENU (UserId,ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl) SELECT " + ddlIdsNo.SelectedValue + ",ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl FROM A2ZHKCUBS.DBO.A2ZMASTERMENU WHERE ModuleNo = " + ddlModule.SelectedValue + " AND (MenuParentNo IS NULL OR MenuUrl IS NULL)";
                    //rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHKCUBS"));

                    if (rowEffiect > 0)
                    {
                        ShowMessage("Data saved successfully.", Color.Green);
                        ClearInformation();
                    }
                }


                //if (ddlModule.SelectedValue == "3") // BT 
                //{

                //    strQuery = "DELETE FROM A2ZBTCUBS.DBO.A2ZERPMENU WHERE UserId = '" + ddlIdsNo.SelectedValue + "' AND ModuleNo = " + ddlModule.SelectedValue;
                //    rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZBTCUBS"));

                //    if (lblMemApp2.Text == "0")
                //    {
                //        strQuery = "INSERT INTO A2ZBTCUBS.DBO.A2ZERPMENU (UserId,ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl) SELECT SelectUserId,ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl FROM A2ZHKCUBS.DBO.A2ZMASTERMENU WHERE SelectUserId = '" + ddlIdsNo.SelectedValue + "' AND MenuNo !=11 AND MenuNo !=12";
                //        rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZBTCUBS"));
                //    }
                //    else 
                //    {
                //        strQuery = "INSERT INTO A2ZBTCUBS.DBO.A2ZERPMENU (UserId,ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl) SELECT SelectUserId,ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl FROM A2ZHKCUBS.DBO.A2ZMASTERMENU WHERE SelectUserId = '" + ddlIdsNo.SelectedValue + "'";
                //        rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZBTCUBS"));
                //    }



                   

                //    if (rowEffiect > 0)
                //    {
                //        ShowMessage("Data saved successfully.", Color.Green);
                //        ClearInformation();
                //    }
                //}



                //if (ddlModule.SelectedValue == "4") // HR 
                //{

                //    strQuery = "DELETE FROM A2ZHRCUBS.DBO.A2ZERPMENU WHERE UserId = '" + ddlIdsNo.SelectedValue + "' AND ModuleNo = " + ddlModule.SelectedValue;
                //    rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHRCUBS"));

                //    strQuery = "INSERT INTO A2ZHRCUBS.DBO.A2ZERPMENU (UserId,ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl) SELECT UserId,ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl FROM A2ZHKCUBS.DBO.A2ZMASTERMENU WHERE UserId = '" + ddlIdsNo.SelectedValue + "'";
                //    rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHRCUBS"));

                //    strQuery = "INSERT INTO A2ZHRCUBS.DBO.A2ZERPMENU (UserId,ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl) SELECT " + ddlIdsNo.SelectedValue + ",ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl FROM A2ZHKCUBS.DBO.A2ZMASTERMENU WHERE ModuleNo = " + ddlModule.SelectedValue + " AND (MenuParentNo IS NULL OR MenuUrl IS NULL)";
                //    rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHRCUBS"));

                //    if (rowEffiect > 0)
                //    {
                //        ShowMessage("Data saved successfully.", Color.Green);
                //        ClearInformation();
                //    }
                //}

                //if (ddlModule.SelectedValue == "6") // BT 
                //{

                //    strQuery = "DELETE FROM A2ZBTMCUS.DBO.A2ZERPMENU WHERE UserId = '" + ddlIdsNo.SelectedValue + "' AND ModuleNo = " + ddlModule.SelectedValue;
                //    rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZBTMCUS"));

                //    strQuery = "INSERT INTO A2ZBTMCUS.DBO.A2ZERPMENU (UserId,ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl) SELECT UserId,ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl FROM A2ZHKMCUS.DBO.A2ZMASTERMENU WHERE UserId = '" + ddlIdsNo.SelectedValue + "'";
                //    rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZBTMCUS"));

                //    strQuery = "INSERT INTO A2ZBTMCUS.DBO.A2ZERPMENU (UserId,ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl) SELECT " + ddlIdsNo.SelectedValue + ",ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl FROM A2ZHKMCUS.DBO.A2ZMASTERMENU WHERE ModuleNo = " + ddlModule.SelectedValue + " AND (MenuParentNo IS NULL OR MenuUrl IS NULL)";
                //    rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZBTMCUS"));

                //    if (rowEffiect > 0)
                //    {
                //        ShowMessage("Data saved successfully.", Color.Green);
                //        ClearInformation();
                //    }
                //}

                //if (ddlModule.SelectedValue == "7") // BT 
                //{

                //    strQuery = "DELETE FROM A2ZOBTMCUS.DBO.A2ZERPMENU WHERE UserId = '" + ddlIdsNo.SelectedValue + "' AND ModuleNo = " + ddlModule.SelectedValue;
                //    rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZOBTMCUS"));

                //    strQuery = "INSERT INTO A2ZOBTMCUS.DBO.A2ZERPMENU (UserId,ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl) SELECT UserId,ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl FROM A2ZHKMCUS.DBO.A2ZMASTERMENU WHERE UserId = '" + ddlIdsNo.SelectedValue + "'";
                //    rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZOBTMCUS"));

                //    strQuery = "INSERT INTO A2ZOBTMCUS.DBO.A2ZERPMENU (UserId,ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl) SELECT " + ddlIdsNo.SelectedValue + ",ModuleNo,MenuNo,MenuName,MenuParentNo,MenuUrl FROM A2ZHKMCUS.DBO.A2ZMASTERMENU WHERE ModuleNo = " + ddlModule.SelectedValue + " AND (MenuParentNo IS NULL OR MenuUrl IS NULL)";
                //    rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZOBTMCUS"));

                //    if (rowEffiect > 0)
                //    {
                //        ShowMessage("Data saved successfully.", Color.Green);
                //        ClearInformation();
                //        ddlModule.Focus();
                //    }
                //}


            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.Add Problem');</script>");
                //throw ex;
            }

        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("A2ZERPModule.aspx", false);
            }
            catch (Exception ex)
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.Exit Problem');</script>");
                //throw ex;
            }
        }

        protected void ShowMessage(string message, Color clr)
        {
            lblMessage.Text = message;
            lblMessage.ForeColor = clr;
            lblMessage.Visible = true;
            DivMessage.Visible = true;
            DivMain.Attributes.CssStyle.Add("opacity", "0.1");
            DivGridView.Attributes.CssStyle.Add("opacity", "0.1");
            DivButton.Attributes.CssStyle.Add("opacity", "0.1");


            DivMessage.Style.Add("Top", "250px");
            DivMessage.Style.Add("Right", "500px");
            DivMessage.Style.Add("position", "absolute");
            DivMessage.Attributes.CssStyle.Add("opacity", "100");
        }

        protected void btnHideMessageDiv_Click(object sender, EventArgs e)
        {
            DivMain.Attributes.CssStyle.Add("opacity", "100");
            DivGridView.Attributes.CssStyle.Add("opacity", "100");
            DivButton.Attributes.CssStyle.Add("opacity", "100");
            DivMessage.Visible = false;
            DivMain.Visible = true;
        }

       
        private void ClearInformation()
        {

            try
            {
                //ddlUserId.SelectedIndex = 0;
                ddlModule.SelectedIndex = 0;

                DivGridTitleView.Visible = false;


              
                
                btnMark1.Visible = false;
                btnUnMark1.Visible = false;
                btnMark.Visible = true;
                btnUnMark.Visible = true;


            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.Clear Problem');</script>");

                //throw ex;
            }
        }

        private void ShowGridViewWithValue()
        {
            int i = 0;

            foreach (GridViewRow gv in gvModuleTitle.Rows)
            {
                int userId = Converter.GetSmallInteger(gvModuleTitle.Rows[i].Cells[4].Text);

                if (userId > 0)
                {
                    ((CheckBox)gvModuleTitle.Rows[i].FindControl("chkSelect")).Checked = true;
                }

                i++;

            }

        }

        private void ShowGridViewWithValue1()
        {
            int i = 0;

            foreach (GridViewRow gv in gvModule.Rows)
            {
                int userId = Converter.GetSmallInteger(gvModule.Rows[i].Cells[3].Text);

                if (userId > 0)
                {
                    ((CheckBox)gvModule.Rows[i].FindControl("chkSelect")).Checked = true;
                }

                i++;

            }

        }

        protected void btnMark_Click(object sender, EventArgs e)
        {
            int i = 0;

            foreach (GridViewRow gv in gvModuleTitle.Rows)
            {
                ((CheckBox)gvModuleTitle.Rows[i].FindControl("chkSelect")).Checked = true;

                i++;
            }
        }

        protected void btnUnMark_Click(object sender, EventArgs e)
        {
            int i = 0;

            foreach (GridViewRow gv in gvModuleTitle.Rows)
            {
                ((CheckBox)gvModuleTitle.Rows[i].FindControl("chkSelect")).Checked = false;

                i++;
            }
        }


        protected void btnMark1_Click(object sender, EventArgs e)
        {
            int i = 0;

            foreach (GridViewRow gv in gvModule.Rows)
            {
                ((CheckBox)gvModule.Rows[i].FindControl("chkSelect")).Checked = true;

                i++;
            }
        }

        protected void btnUnMark1_Click(object sender, EventArgs e)
        {
            int i = 0;

            foreach (GridViewRow gv in gvModule.Rows)
            {
                ((CheckBox)gvModule.Rows[i].FindControl("chkSelect")).Checked = false;

                i++;
            }
        }

       


    }
}
