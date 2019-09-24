using ATOZWEBOMS.WebSessionStore;
using DataAccessLayer.BLL;
using DataAccessLayer.DTO.CustomerServices;
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

namespace ATOZWEBGMS.Pages
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                lblID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));

                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                string date = dt.ToString("dd/MM/yyyy");
                CtrlProcDate.Text = date;

                OrderPartyDropdown();

            }
        }

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("A2ZERPModule.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        private void OrderPartyDropdown()
        {
            string sqlquery = "SELECT PartyCode,PartyName from A2ZPARTYCODE WHERE GroupCode = 11 ";
            ddlOrderParty = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlOrderParty, "A2ZACOMS");
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                A2ZVCHNOCTRLDTO getDTO = new A2ZVCHNOCTRLDTO();

                getDTO = (A2ZVCHNOCTRLDTO.GetLastDefaultVchNo());
                lblCtrlOrderNo.Text = "OR" + getDTO.RecLastNo.ToString("000000");

                var prm = new object[21];


                prm[0] = lblCtrlOrderNo.Text;//OrderNo;
                prm[1] = Converter.GetDateToYYYYMMDD(CtrlProcDate.Text);//OrderDate;
                prm[2] = ddlOrderParty.SelectedValue;//OrderParty;
                prm[3] = ddlItemName.SelectedValue;//OrderItemName;
                prm[4] = ddlWayToOrder.SelectedValue;//WayToOrder;
                prm[5] = txtWhoseOrder.Text;//WhoseOrder;
                prm[6] = Converter.GetDateToYYYYMMDD(txtDeliveryPossibleDate.Text);//DeliveryPossibleDate;
                prm[7] = "1";//OrderStatus (Received = 1 || Send to Factory = 2 || Ready in Factory = 3 || Transit = 4 || Received = 5 || Delevered = 6 || Cancelled = 99)
                prm[8] = "Order Received";//OrderStatusDesc;
                prm[9] = Converter.GetDateToYYYYMMDD(CtrlProcDate.Text);//OrderStatusDate;
                prm[10] = ddlItemName.SelectedValue; //ItemCode;
                prm[11] = ddlItemName.SelectedItem.Text; //ItemName;
                // prm[12] = "0"; //ItemImage;
                prm[12] = txtSize.Text; //ItemSize;
                prm[13] = txtLength.Text; //ItemLength;
                prm[14] = txtWide.Text; //ItemWide;
                prm[15] = "0"; //ItemWidth;
                prm[16] = txtWeight.Text; //ItemWeight;
                prm[17] = txtColor.Text; //ItemColor;
                prm[18] = txtquantity.Text; //ItemQuantity;
                prm[19] = txtTotalWeight.Text; //ItemTotalWeight;
                prm[20] = txtWhoseOrder.Text; //OrderSubmitPerson

                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_OrderReceive", prm, "A2ZACOMS"));
                if (result == 0)
                {
                    Response.Redirect(Request.RawUrl);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void btnDeleteImage_Click(object sender, EventArgs e)
        {

        }

        //protected void btnUploadImage_Click(object sender, EventArgs e)
        protected void ibtnUpload_Click(object sender, ImageClickEventArgs e)
        {
            try
            {

                SqlConnection strCon = new SqlConnection(DataAccessLayer.Constants.DBConstants.GetConnectionString("A2ZACOMS"));
                string ImageName = string.Empty;
                byte[] Image = null;


                if (FileUpload1.PostedFile != null && FileUpload1.FileName != "")
                {
                    ImageName = Path.GetFileName(FileUpload1.FileName);
                    Image = new byte[FileUpload1.PostedFile.ContentLength];
                    HttpPostedFile UploadedImage = FileUpload1.PostedFile;
                    UploadedImage.InputStream.Read(Image, 0, (int)FileUpload1.PostedFile.ContentLength);
                }



                using (SqlCommand cmd = new SqlCommand())
                {
                    strCon.Open();
                    cmd.Connection = strCon;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_UploadItemImage";
                    cmd.Parameters.Add(new SqlParameter("@pvchAction", SqlDbType.VarChar, 50));
                    cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@ItemImage", SqlDbType.Image));
                    cmd.Parameters.Add("@pIntErrDescOut", SqlDbType.Int).Direction = ParameterDirection.Output;

                    cmd.Parameters["@pvchAction"].Value = "insert";
                    cmd.Parameters["@UserID"].Value = Converter.GetInteger(lblID.Text);
                    cmd.Parameters["@ItemImage"].Value = Image;

                    cmd.ExecuteNonQuery();
                    int retVal = (int)cmd.Parameters["@pIntErrDescOut"].Value;

                    //btnSearch_Click(this, new EventArgs());
                    //ItmImageUploadMSG();
                    showimage();
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.ibtnUpload_Click Problem');</script>");
                //throw ex;
            }
        }


        protected void showimage()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(DataAccessLayer.Constants.DBConstants.GetConnectionString("A2ZACOMS")))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand("SELECT ItemImage  FROM WFITEMIMAGE WHERE UserId='" + lblID.Text + "'", conn))
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

                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Item Image Not Found');", true);
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
