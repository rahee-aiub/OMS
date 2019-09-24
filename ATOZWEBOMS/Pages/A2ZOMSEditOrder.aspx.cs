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
    public partial class A2ZOMSEditOrder : System.Web.UI.Page
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

                OrderNoDropdown();

                ItemsDropdown();

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

        private void OrderNoDropdown()
        {
            string sqlquery = "SELECT OrderNo,OrderNo FROM A2ZORDERDETAILS WHERE OrderStatus = 11";
            ddlOrderNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlOrderNo, "A2ZACOMS");
        }

        private void OrderPartyDropdown()
        {
            string sqlquery = "SELECT PartyCode,PartyName from A2ZPARTYCODE WHERE GroupCode = 13";
            ddlOrderParty = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlOrderParty, "A2ZACOMS");
        }

        private void ItemsDropdown()
        {
            string sqlquery = "SELECT ItemsCode,ItemsName from A2ZITEMSCODE";
            ddlItemName = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlItemName, "A2ZACOMS");
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
               

                var prm = new object[19];


                prm[0] = ddlOrderNo.SelectedValue;//OrderNo;
                prm[1] = Converter.GetDateToYYYYMMDD(txtOrderDate.Text);//OrderDate;
                prm[2] = ddlOrderParty.SelectedValue;//OrderParty;   
                prm[3] = ddlWayToOrder.SelectedValue;//WayToOrder;
                prm[4] = txtWayToOrderNo.Text;//WayToOrderNo;

                prm[5] = txtWhoseOrder.Text;//WhoseOrder;
                prm[6] = Converter.GetDateToYYYYMMDD(txtDeliveryPossibleDate.Text);//DeliveryPossibleDate;
                
                prm[7] = ddlItemName.SelectedValue; //ItemCode;
                prm[8] = ddlItemName.SelectedItem.Text; //ItemName;
                // prm[12] = "0"; //ItemImage;
                prm[9] = ddlKarat.SelectedValue; //ItemSize;
                prm[10] = txtSize.Text; //ItemSize;
                prm[11] = txtLength.Text; //ItemLength;
                prm[12] = txtPiece.Text; //ItemQuantity;
                prm[13] = txtWide.Text; //ItemWide;    
                prm[14] = txtWeight.Text; //ItemWeight;
                prm[15] = txtColor.Text; //ItemColor;     
                prm[16] = txtTotalWeight.Text; //ItemTotalWeight;
               
                prm[17] = lblID.Text;
                prm[18] = txtPhoneNo.Text;

                

                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_EditOrder", prm, "A2ZACOMS"));
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
            string strQuery = "DELETE FROM WFITEMIMAGE WHERE UserId = '" + lblID.Text + "'";
            int rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZACOMS"));
            if(rowEffiect > 0)
            {
                //ImgPicture = "data:image/png;base64," + base64String;
                showimage();
            }
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
                    showimageWF();
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "scriptkey", "<script>alert('System Error.ibtnUpload_Click Problem');</script>");
                //throw ex;
            }
        }


        protected void showimageWF()
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


        protected void showimage()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(DataAccessLayer.Constants.DBConstants.GetConnectionString("A2ZACOMS")))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand("SELECT ItemImage  FROM A2ZORDERDETAILS WHERE OrderNo='" + ddlOrderNo.SelectedValue + "'", conn))

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
        protected void ddlOrderNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlOrderNo.SelectedIndex != 0)
            {

                var prm = new object[1];

                prm[0] = ddlOrderNo.SelectedValue;

                DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableBySpWithParams("Sp_GetOrderInformation", prm, "A2ZACOMS");

                var p = new A2ZPARTYDTO();
                if (dt.Rows.Count > 0)
                {
                    DateTime Odate = Converter.GetDateTime(dt.Rows[0]["OrderDate"]);
                    txtOrderDate.Text = Odate.ToString("dd/MM/yyyy");
                    
                    ddlOrderParty.SelectedValue = Converter.GetString(dt.Rows[0]["OrderParty"]);

                    ddlWayToOrder.SelectedValue = Converter.GetString(dt.Rows[0]["WayToOrder"]);

                    txtWayToOrderNo.Text = Converter.GetString(dt.Rows[0]["WayToOrderNo"]);


                    DateTime Pdate = Converter.GetDateTime(dt.Rows[0]["DeliveryPossibleDate"]);
                    txtDeliveryPossibleDate.Text = Pdate.ToString("dd/MM/yyyy");

                    ddlItemName.SelectedValue = Converter.GetString(dt.Rows[0]["ItemCode"]);

                    ddlKarat.SelectedValue = Converter.GetString(dt.Rows[0]["ItemKarat"]);
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
        }

        protected void txtPiece_TextChanged(object sender, EventArgs e)
        {
            decimal TotalWeight = 0;
            decimal Piece = Converter.GetDecimal(txtPiece.Text);
            decimal Weight = Converter.GetDecimal(txtWeight.Text);

            if (Piece != 0 && Weight != 0)
            {
                TotalWeight = (Weight * Piece);
                txtTotalWeight.Text = Converter.GetString(String.Format("{0:0,0.0000}", TotalWeight));
            }

            txtWide.Focus();
        }

        protected void txtWeight_TextChanged(object sender, EventArgs e)
        {
            decimal TotalWeight = 0;
            decimal Piece = Converter.GetDecimal(txtPiece.Text);
            decimal Weight = Converter.GetDecimal(txtWeight.Text);

            if (Piece != 0 && Weight != 0)
            {
                TotalWeight = (Weight * Piece);
                txtTotalWeight.Text = Converter.GetString(String.Format("{0:0,0.0000}", TotalWeight));
            }
        }
    }
}
