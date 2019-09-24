using System;

namespace ATOZWEBCUBS.MasterPages
{
    public partial class HKMasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (IsPostBack)
                {
                }
                else
                {
                    this.Page.MaintainScrollPositionOnPostBack = true;
                }
            }
            catch (Exception ex)
            {
                //throw;
            }
        }
    }
}
