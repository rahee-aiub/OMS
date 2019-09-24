
using System;
using System.Data;
using System.Web.UI.WebControls;
using ATOZWEBOMS.WebSessionStore;
using DataAccessLayer.DTO.CustomerServices;
using DataAccessLayer.Utility;
using DataAccessLayer.DTO.HouseKeeping;



namespace ATOZWEBOMS.MasterPages
{
    public partial class CustomerServices : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    lblUserName.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_NAME));
                    lblUserBranchNo.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_BRNO));
                    lblUserLabel.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_LEVEL));

                    string UnitAddress1 = string.Empty;
                    string UnitAddress2 = string.Empty;
                    string UnitAddress3 = string.Empty;

                    string UnitNameB = string.Empty;
                    string UnitAddress1B = string.Empty;

                    var p = A2ZERPSYSPRMDTO.GetParameterValue();

                    if (p.PrmUnitFlag == 1 || lblUserLabel.Text == "40")
                    {
                        lblUserBranchNo.Text = Converter.GetString(p.PrmUnitNo);
                        lblCompanyName.Text = p.PrmUnitName;
                        UnitAddress1 = p.PrmUnitAdd1;
                        UnitAddress2 = p.PrmUnitAdd2;
                        UnitAddress3 = p.PrmUnitAdd3;

                        UnitNameB = p.PrmUnitNameB;
                        UnitAddress1B = p.PrmUnitAdd1B;
                    }
                    else
                    {
                        A2ZERPBRANCHDTO getDTO = new A2ZERPBRANCHDTO();
                        int userbranch = Converter.GetInteger(lblUserBranchNo.Text);
                        getDTO = (A2ZERPBRANCHDTO.GetInformation(userbranch));

                        if (getDTO.BranchNo > 0)
                        {
                            lblCompanyName.Text = Converter.GetString(getDTO.BranchName);
                            UnitAddress1 = Converter.GetString(getDTO.BranchAdd1);

                            UnitNameB = Converter.GetString(getDTO.BranchNameB);
                            UnitAddress1B = Converter.GetString(getDTO.BranchAdd1B);
                           
                        }
                    }


                    SessionStore.SaveToCustomStore(Params.BRNO, lblUserBranchNo.Text);
                    SessionStore.SaveToCustomStore(Params.COMPANY_NAME, lblCompanyName.Text);
                    SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS, UnitAddress1);

                    SessionStore.SaveToCustomStore(Params.COMPANY_NAME_B, UnitNameB);
                    SessionStore.SaveToCustomStore(Params.BRANCH_ADDRESS_B, UnitAddress1B);
                   
                

                    //    GetMenuData();


                    string Rdate = (string)Session["date"];

                    if (Rdate == null || Rdate == "")
                    {
                        A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                        lblProcessDate.Text = Converter.GetString(dto.ProcessDate.ToLongDateString());
                    }
                    else
                    {
                        lblProcessDate.Text = Rdate;
                    }


                    //if (DataAccessLayer.Utility.Converter.GetSmallInteger(SessionStore.GetValue(Params.SYS_USER_PERMISSION)) == 10)
                    //{
                    //    lblUserPermission.Text = "Permission :" + "Input";
                    //}
                    //if (DataAccessLayer.Utility.Converter.GetSmallInteger(SessionStore.GetValue(Params.SYS_USER_PERMISSION)) == 20)
                    //{
                    //    lblUserPermission.Text = "Permission :" + "Checked and Verify";
                    //}
                    //if (DataAccessLayer.Utility.Converter.GetSmallInteger(SessionStore.GetValue(Params.SYS_USER_PERMISSION)) == 30)
                    //{
                    //    lblUserPermission.Text = "Permission :" + "Approved";
                    //}
                }
                else
                {
                    //Response.Cache.SetNoStore();
                    //Response.Cache.AppendCacheExtension("no-cache");
                    //Response.Expires = 0;

                   
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
