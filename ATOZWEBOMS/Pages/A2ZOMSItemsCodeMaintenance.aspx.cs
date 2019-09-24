using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ATOZWEBOMS.WebSessionStore;
using System.Xml.Linq;
//using A2Z.Web.Constants;
using DataAccessLayer.DTO;
using DataAccessLayer.Utility;
using DataAccessLayer.DTO.CustomerServices;

namespace ATOZWEBOMS.Pages
{
    public partial class A2ZOMSItemsCodeMaintenance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtItemsName.Focus();
                BtnUpdate.Visible = false;
                dropdown();
            }
        }

        private void dropdown()
        {
            string sqlquery = "SELECT ItemsCode,ItemsName from A2ZITEMSCODE";
            ddlItems = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlItems, "A2ZACOMS");
        }

        protected void txtcode_TextChanged(object sender, EventArgs e)
        {
            if (ddlItems.SelectedValue == "-Select-")
            {
                txtItemsName.Focus();

            }
            try
            {

                if (txtcode.Text != string.Empty)
                {
                    Int16 MainCode = Converter.GetSmallInteger(txtcode.Text);
                    A2ZITEMSDTO getDTO = (A2ZITEMSDTO.GetInformation(MainCode));

                    if (getDTO.ItemsCode > 0)
                    {
                        txtcode.Text = Converter.GetString(getDTO.ItemsCode);
                        txtItemsName.Text = Converter.GetString(getDTO.ItemsName);
                        BtnSubmit.Visible = false;
                        BtnUpdate.Visible = true;
                        ddlItems.SelectedValue = Converter.GetString(getDTO.ItemsCode);
                        txtItemsName.Focus();
                    }
                    else
                    {                       
                        txtcode.Text = string.Empty;
                        BtnSubmit.Visible = true;
                        BtnUpdate.Visible = false;
                        txtItemsName.Text = string.Empty;
                        txtcode.Focus();

                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ddlItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlItems.SelectedValue == "-Select-")
            {
                txtcode.Focus();
                txtcode.Text = string.Empty;
                txtItemsName.Text = string.Empty;
                BtnSubmit.Visible = true;
                BtnUpdate.Visible = false;
            }

            try
            {


                if (ddlItems.SelectedValue != "-Select-")
                {

                    int MainCode = Converter.GetInteger(ddlItems.SelectedValue);
                    A2ZITEMSDTO getDTO = (A2ZITEMSDTO.GetInformation(MainCode));
                    if (getDTO.ItemsCode > 0)
                    {
                        txtcode.Text = Converter.GetString(getDTO.ItemsCode);
                        txtItemsName.Text = Converter.GetString(getDTO.ItemsName);
                        BtnSubmit.Visible = false;
                        BtnUpdate.Visible = true;
                        txtItemsName.Focus();


                    }
                    else
                    {
                        txtcode.Focus();
                        txtcode.Text = string.Empty;
                        txtItemsName.Text = string.Empty;
                        BtnSubmit.Visible = true;
                        BtnUpdate.Visible = false;
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void clearinfo()
        {
            txtcode.Text = string.Empty;
            txtItemsName.Text = string.Empty;
        }
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtItemsName.Text != string.Empty)
                {
                    gvDetail();
                    gvDetailInfo.Visible = false;

                    int totrec = gvDetailInfo.Rows.Count;

                    int lastCode = (totrec + 1);
                    txtcode.Text = Converter.GetString(lastCode);

                    A2ZITEMSDTO objDTO = new A2ZITEMSDTO();

                    objDTO.ItemsCode = Converter.GetInteger(txtcode.Text);
                    objDTO.ItemsName = Converter.GetString(txtItemsName.Text);

                    int roweffect = A2ZITEMSDTO.InsertInformation(objDTO);
                    if (roweffect > 0)
                    {
                        txtcode.Focus();
                        clearinfo();
                        dropdown();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (ddlItems.SelectedValue != "-Select-")
            {

                A2ZITEMSDTO UpDTO = new A2ZITEMSDTO();
                UpDTO.ItemsCode = Converter.GetInteger(txtcode.Text);
                UpDTO.ItemsName = Converter.GetString(txtItemsName.Text);

                int roweffect = A2ZITEMSDTO.UpdateInformation(UpDTO);
                if (roweffect > 0)
                {

                    dropdown();
                    clearinfo();
                    //    ddlProfession.SelectedValue = "-Select-";
                    BtnSubmit.Visible = true;
                    BtnUpdate.Visible = false;
                    txtcode.Focus();

                }
            }
        }

        protected void gvDetail()
        {
            string sqlquery3 = "SELECT ItemsCode,ItemsName FROM A2ZITEMSCODE";
            gvDetailInfo = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery3, gvDetailInfo, "A2ZACOMS");
        }
        protected void BtnView_Click(object sender, EventArgs e)
        {
            gvDetailInfo.Visible = true;
            gvDetail();
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }

    }
}
