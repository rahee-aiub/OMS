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
    public partial class A2ZOpenPartyCode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          
        }

     
        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
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
                Int16 code = Converter.GetSmallInteger(ddlGroup.SelectedValue);
                A2ZRECCTRLDTO getDTO = (A2ZRECCTRLDTO.GetLastRecords(code));
                lblLastLPartyNo.Text = Converter.GetString(getDTO.CtrlRecLastNo);

                lblnewPartyNo.Text = code.ToString() + getDTO.CtrlRecLastNo.ToString("000000");

                
                UpdateRecords();                           
            }

            catch (Exception Ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Data not inserted');", true);
                return;
            }
        }
       
        private void UpdateRecords()
        {
            
            A2ZPARTYDTO objDTO = new A2ZPARTYDTO();

            objDTO.GroupCode = Converter.GetInteger(ddlGroup.SelectedValue);
            objDTO.GroupName = Converter.GetString(ddlGroup.SelectedItem.Text);
            objDTO.PartyCode = Converter.GetInteger(lblnewPartyNo.Text);
            objDTO.PartyName = Converter.GetString(txtPartyName.Text);
            objDTO.PartyAddresssLine1 = Converter.GetString(txtPartyAddress.Text);
            objDTO.PartyMobileNo = Converter.GetString(txtMobileNo.Text);
            objDTO.PartyEmail = Converter.GetString(txtPartyEmail.Text);

            int roweffect = A2ZPARTYDTO.InsertInformation(objDTO);
            if (roweffect > 0)
            {

                Response.Redirect(Request.RawUrl);
            }
        }

        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtPartyName.Focus();
        }

        
    }
}
