using System;
using System.Web;
using System.Web.UI;
using ATOZWEBOMS.WebSessionStore;
using DataAccessLayer.DTO;
using DataAccessLayer.Utility;
using DataAccessLayer.BLL;
using DataAccessLayer.DTO.CustomerServices;
using System.Data;

namespace ATOZWEBOMS.Pages
{
    public partial class A2ZEditPartyCode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }


        private void PartyDropdown()
        {
            string sqlquery = "SELECT PartyCode,PartyName from A2ZPARTYCODE  where GroupCode='" + ddlGroup.Text + "' GROUP BY PartyCode,PartyName";
            ddlPartyName = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlPartyName, "A2ZACOMS");
        }


        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }




        protected void InitializedRecords()
        {

            txtPartyName.Text = string.Empty;
            txtPartyAddressL1.Text = string.Empty;
            txtPartyAddressL2.Text = string.Empty;
            txtPartyAddressL3.Text = string.Empty;
            txtMobileNo.Text = string.Empty;
            txtPartyEmail.Text = string.Empty;


            ddlGroup.Enabled = true;
            txtPartyName.Enabled = true;
            txtPartyAddressL1.Enabled = true;
            txtPartyAddressL2.Enabled = true;
            txtPartyAddressL3.Enabled = true;
            txtMobileNo.Enabled = true;
            txtPartyEmail.Enabled = true;

            txtPartyCode.Text = string.Empty;
            if (ddlPartyName.SelectedIndex > 0)
            {
                ddlPartyName.SelectedIndex = 0;
            }




        }


        protected void ClearRecords()
        {
            ddlGroup.SelectedIndex = 0;
            txtPartyName.Text = string.Empty;
            txtPartyAddressL1.Text = string.Empty;
            txtPartyAddressL2.Text = string.Empty;
            txtPartyAddressL3.Text = string.Empty;
            txtMobileNo.Text = string.Empty;
            txtPartyEmail.Text = string.Empty;



            ddlGroup.Enabled = true;
            txtPartyName.Enabled = true;
            txtPartyAddressL1.Enabled = true;
            txtPartyAddressL2.Enabled = true;
            txtPartyAddressL3.Enabled = true;
            txtMobileNo.Enabled = true;
            txtPartyEmail.Enabled = true;

            txtPartyCode.Text = string.Empty;
            ddlPartyName.SelectedIndex = 0;



        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (ddlGroup.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Select Group Code');", true);
                return;
            }


            if (txtPartyName.Text == string.Empty)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Input Party Name');", true);
                return;
            }


            try
            {

                A2ZPARTYDTO objDTO = new A2ZPARTYDTO();

                objDTO.GroupCode = Converter.GetInteger(ddlGroup.SelectedValue);
                objDTO.GroupName = Converter.GetString(ddlGroup.SelectedItem.Text);
                objDTO.PartyCode = Converter.GetInteger(txtPartyCode.Text);

                objDTO.PartyName = Converter.GetString(txtPartyName.Text);
                objDTO.PartyAddresssLine1 = Converter.GetString(txtPartyAddressL1.Text);
                objDTO.PartyAddresssLine2 = Converter.GetString(txtPartyAddressL2.Text);
                objDTO.PartyAddresssLine3 = Converter.GetString(txtPartyAddressL3.Text);
                objDTO.PartyMobileNo = Converter.GetString(txtMobileNo.Text);
                objDTO.PartyEmail = Converter.GetString(txtPartyEmail.Text);


                int roweffect = A2ZPARTYDTO.UpdateParty(objDTO);
                if (roweffect > 0)
                {


                    ClearRecords();

                    ddlGroup.Focus();
                }



            }
            catch (Exception Ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data not inserted');", true);
                return;
            }

        }

        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {

            InitializedRecords();


            PartyDropdown();



            txtPartyName.Focus();


        }


        protected void txtPartyCode_TextChanged(object sender, EventArgs e)
        {
            if (txtPartyCode.Text != string.Empty)
            {

                string input = Converter.GetString(txtPartyCode.Text);
                string GroupCode = input.Substring(0, 2);

                if (GroupCode != ddlGroup.SelectedValue)
                {
                    txtPartyCode.Text = string.Empty;
                    txtPartyCode.Focus();

                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Party Code');", true);
                    return;
                }


                int PartyCode = Converter.GetInteger(txtPartyCode.Text);
                A2ZPARTYDTO getDTO = (A2ZPARTYDTO.GetPartyInformation(PartyCode));

                if (getDTO.PartyCode > 0)
                {
                    txtPartyName.Text = Converter.GetString(getDTO.PartyName);

                    txtPartyAddressL1.Text = Converter.GetString(getDTO.PartyAddresssLine1);
                    txtPartyAddressL2.Text = Converter.GetString(getDTO.PartyAddresssLine2);
                    txtPartyAddressL3.Text = Converter.GetString(getDTO.PartyAddresssLine3);
                    txtMobileNo.Text = Converter.GetString(getDTO.PartyMobileNo);
                    txtPartyEmail.Text = Converter.GetString(getDTO.PartyEmail);
                }
                else
                {
                    txtPartyCode.Text = string.Empty;
                    txtPartyCode.Focus();

                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Party Code');", true);
                    return;
                }
            }
        }

        protected void ddlPartyName_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtPartyCode.Text = ddlPartyName.SelectedValue;


            int PartyCode = Converter.GetInteger(txtPartyCode.Text);
            A2ZPARTYDTO getDTO = (A2ZPARTYDTO.GetPartyInformation(PartyCode));

            if (getDTO.PartyName == null)
            {
                txtPartyName.Text = string.Empty;
                txtPartyCode.Text = string.Empty;
                txtPartyCode.Focus();
            }

            else
            {
                txtPartyName.Text = Converter.GetString(getDTO.PartyName);

                txtPartyAddressL1.Text = Converter.GetString(getDTO.PartyAddresssLine1);
                txtPartyAddressL2.Text = Converter.GetString(getDTO.PartyAddresssLine2);
                txtPartyAddressL3.Text = Converter.GetString(getDTO.PartyAddresssLine3);
                txtMobileNo.Text = Converter.GetString(getDTO.PartyMobileNo);
                txtPartyEmail.Text = Converter.GetString(getDTO.PartyEmail);
            }



        }

       


    }
}
