using System;
using System.Web;
using System.Web.UI;
using ATOZWEBOMS.WebSessionStore;
using DataAccessLayer.DTO;
using DataAccessLayer.Utility;
using DataAccessLayer.BLL;
using DataAccessLayer.DTO.CustomerServices;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace ATOZWEBOMS.Pages
{
    public partial class A2ZOMSSingleOrderDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                string Cflag = (string)Session["CtrlFlag"];
                lblCtrlFlag.Text = Cflag;

                lblID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));

                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                string date = dt.ToString("dd/MM/yyyy");
                CtrlProcDate.Text = date;
            
                ItemsDropdown();

                LoadOrderInformation();

            }
        }

        protected void LoadOrderInformation()
        {
          

                var prm = new object[1];

                prm[0] = Session["OrderNo"].ToString();

                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableBySpWithParams("Sp_GetOrderInformation", prm, "A2ZACOMS");

                var p = new A2ZPARTYDTO();
                if (dt.Rows.Count > 0)
                {
                    DateTime Odate = Converter.GetDateTime(dt.Rows[0]["OrderDate"]);
                    txtOrderDate.Text = Odate.ToString("dd/MM/yyyy");
                    txtOrderNo.Text = Converter.GetString(dt.Rows[0]["OrderNo"]);
                    txtOrderParty.Text = Converter.GetString(dt.Rows[0]["OrderParty"]);

                    ddlWayToOrder.SelectedValue = Converter.GetString(dt.Rows[0]["WayToOrder"]);


                    DateTime Pdate = Converter.GetDateTime(dt.Rows[0]["DeliveryPossibleDate"]);
                    txtDeliveryPossibleDate.Text = Pdate.ToString("dd/MM/yyyy");

                    ddlItemName.SelectedValue = Converter.GetString(dt.Rows[0]["ItemCode"]);

                    txtKarat.Text = Converter.GetString(dt.Rows[0]["ItemKarat"]);
                    txtSize.Text = Converter.GetString(dt.Rows[0]["ItemSize"]);
                    txtColor.Text = Converter.GetString(dt.Rows[0]["ItemColor"]);
                    txtLength.Text = Converter.GetString(dt.Rows[0]["ItemLength"]);
                    txtPiece.Text = Converter.GetString(dt.Rows[0]["ItemPiece"]);
                    txtWeight.Text = Converter.GetString(dt.Rows[0]["ItemWeight"]);
                    txtTotalWeight.Text = Converter.GetString(dt.Rows[0]["ItemTotalWeight"]);
                    txtWide.Text = Converter.GetString(dt.Rows[0]["ItemWide"]);
                    txtPiece.Text = Converter.GetString(dt.Rows[0]["ItemPiece"]);
                    txtWhoseOrder.Text = Converter.GetString(dt.Rows[0]["WhoseOrder"]);
                    txtPhoneNo.Text = Converter.GetString(dt.Rows[0]["WhoseOrderPhone"]);
                    showimage();
                }
           
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Session["OrderNo"] = string.Empty;
            Session["CtrlFlag"] = string.Empty;

            if (lblCtrlFlag.Text == "1")
            {
                Response.Redirect("A2ZOMSPendingOrderList.aspx");
            }
            else 
            {
                Response.Redirect("A2ZOMSOrderByFactoryList.aspx");
            }
            
        }

    

     

     

        private void ItemsDropdown()
        {
            string sqlquery = "SELECT ItemsCode,ItemsName from A2ZITEMSCODE";
            ddlItemName = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlItemName, "A2ZACOMS");
        }
    
    
   
        protected void showimage()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(DataAccessLayer.Constants.DBConstants.GetConnectionString("A2ZACOMS")))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand("SELECT ItemImage  FROM A2ZORDERDETAILS WHERE OrderNo='" + Session["OrderNo"].ToString() + "'", conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            byte[] picData = reader["ItemImage"] as byte[] ?? null;

                            if (picData != null)
                            {
                                using (MemoryStream ms = new MemoryStream(picData))
                                {
                                    string base64String = Convert.ToBase64String(picData, 0, picData.Length);
                                    ImgPicture.ImageUrl = "data:image/png;base64," + base64String;
                                }
                            }
                        }
                        else
                        {

                            ImgPicture.ImageUrl = "~/Images/BlankImage.jpg";

                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('No Image');", true);
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    
    }
}
