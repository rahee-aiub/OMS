using System;
using System.Data;
using System.Web.UI.WebControls;
using ATOZWEBOMS.WebSessionStore;
using DataAccessLayer.Utility;


using DataAccessLayer.DTO.HouseKeeping;
using DataAccessLayer.DTO.CustomerServices;

namespace ATOZWEBOMS.MasterPages
{
    public partial class HKMenuMasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    GetMenuData();

                    A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                    lblProcessDate.Text = Converter.GetString(dto.ProcessDate.ToLongDateString());

                    lblUserName.Text = Converter.GetString(SessionStore.GetValue(Params.SYS_USER_NAME));

                    var p = A2ZERPSYSPRMDTO.GetParameterValue();
                    lblCompanyName.Text = p.PrmUnitName;

                    //if (Converter.GetSmallInteger(SessionStore.GetValue(Params.SYS_USER_PERMISSION)) == 10)
                    //{
                    //    lblUserPermission.Text = "Permission :" + "Input";
                    //}
                    //if (Converter.GetSmallInteger(SessionStore.GetValue(Params.SYS_USER_PERMISSION)) == 20)
                    //{
                    //    lblUserPermission.Text = "Permission :" + "Checked and Verify";
                    //}
                    //if (Converter.GetSmallInteger(SessionStore.GetValue(Params.SYS_USER_PERMISSION)) == 30)
                    //{
                    //    lblUserPermission.Text = "Permission :" + "Approved";
                    //}

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

        private void GetMenuData()
        {
            string strQuery = "SELECT MenuNo,MenuName,MenuParentNo,MenuUrl FROM A2ZERPMENU WHERE UserId = " + Converter.GetSmallInteger(SessionStore.GetValue(Params.SYS_USER_ID));
            DataTable table = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(strQuery, "A2ZHKOMS");

            DataView view = new DataView(table);
            view.RowFilter = "MenuParentNo IS NULL";
            foreach (DataRowView row in view)
            {
                MenuItem menuItem = new MenuItem(row["MenuName"].ToString(), row["MenuNo"].ToString());
                menuItem.NavigateUrl = row["MenuUrl"].ToString();
                menuBar.Items.Add(menuItem);
                AddChildItems(table, menuItem);
            }
        }
        private void AddChildItems(DataTable table, MenuItem menuItem)
        {
            DataView viewItem = new DataView(table);
            viewItem.RowFilter = "MenuParentNo = " + menuItem.Value;
            foreach (DataRowView childView in viewItem)
            {
                MenuItem childItem = new MenuItem(childView["MenuName"].ToString(), childView["MenuNo"].ToString());
                childItem.NavigateUrl = childView["MenuUrl"].ToString();
                menuItem.ChildItems.Add(childItem);
                AddChildItems(table, childItem);
            }
        } 
    }
}
