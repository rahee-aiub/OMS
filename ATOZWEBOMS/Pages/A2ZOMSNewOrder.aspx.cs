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
using System.Drawing;


namespace ATOZWEBOMS.Pages
{
    public partial class A2ZOMSNewOrder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                DivUpdateMSG.Visible = false;

                lblID.Text = DataAccessLayer.Utility.Converter.GetString(SessionStore.GetValue(Params.SYS_USER_ID));

                A2ZCSPARAMETERDTO dto = A2ZCSPARAMETERDTO.GetParameterValue();
                DateTime dt = Converter.GetDateTime(dto.ProcessDate);
                string date = dt.ToString("dd/MM/yyyy");
                CtrlProcDate.Text = date;

                OrderPartyDropdown();
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

        private void OrderPartyDropdown()
        {
            string sqlquery = "SELECT PartyCode,PartyName from A2ZPARTYCODE WHERE GroupCode = 13 ";
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
                
                if (ddlOrderParty.SelectedIndex == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Order Party');", true);
                    return;
                }


                if (ddlItemName.SelectedIndex == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Item Name');", true);
                    return;
                }

                if (ddlKarat.SelectedIndex == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Karat');", true);
                    return;
                }

                if (txtPiece.Text == string.Empty || txtPiece.Text == "0")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Piece');", true);
                    return;
                }

                if (txtWeight.Text == string.Empty || txtWeight.Text == "0")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Weight Per Piece');", true);
                    return;
                }

                if (txtDeliveryPossibleDate.Text == string.Empty)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Delivery Possible Date');", true);
                    return;
                }

                if (ImgPicture.ImageUrl != "" && ImgPicture.ImageUrl != null)
                {

                    Int16 code = Converter.GetSmallInteger(55);
                    A2ZRECCTRLDTO getDTO = (A2ZRECCTRLDTO.GetLastRecords(code));
                    lblNewOrderNo.Text = "55" + getDTO.CtrlRecLastNo.ToString("000000");

                    var prm = new object[22];

                    prm[0] = lblNewOrderNo.Text;//OrderNo;
                    prm[1] = Converter.GetDateToYYYYMMDD(CtrlProcDate.Text);//OrderDate;
                    prm[2] = ddlOrderParty.SelectedValue;//OrderParty;              
                    prm[3] = ddlWayToOrder.SelectedValue;//WayToOrder;
                    prm[4] = txtWayToOrderNo.Text;//WayToOrderNo;

                    prm[5] = txtWhoseOrder.Text;//WhoseOrder;

                    
                    prm[6] = Converter.GetDateToYYYYMMDD(txtDeliveryPossibleDate.Text);//DeliveryPossibleDate;
                    prm[7] = "11";//OrderStatus
                    prm[8] = "New Order";//OrderStatusDesc;
                    prm[9] = Converter.GetDateToYYYYMMDD(CtrlProcDate.Text);//OrderStatusDate;
                    prm[10] = ddlItemName.SelectedValue; //ItemCode;
                    prm[11] = ddlItemName.SelectedItem.Text; //ItemName;

                    prm[12] = ddlKarat.SelectedValue; //ItemKarat;
                    prm[13] = txtSize.Text; //ItemSize;
                    prm[14] = txtLength.Text; //ItemLength;
                    prm[15] = txtPiece.Text; //ItemPiece;
                    prm[16] = txtWide.Text; //ItemWide;
                    prm[17] = txtColor.Text; //ItemColor;
                    prm[18] = txtWeight.Text; //ItemWeight; 
                    prm[19] = txtTotalWeight.Text; //ItemTotalWeight;

                    prm[20] = lblID.Text;
                    prm[21] = txtPhoneNo.Text;

                    int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_NewOrder", prm, "A2ZACOMS"));
                    if (result == 0)
                    {
                        UpdatedMSG();
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        protected void UpdatedMSG()
        {
            DivUpdateMSG.Visible = true;

            DivMain.Attributes.CssStyle.Add("opacity", "0.7");

            DivUpdateMSG.Style.Add("Top", "220px");
            DivUpdateMSG.Style.Add("left", "530px");
            DivUpdateMSG.Style.Add("position", "fixed");
            DivUpdateMSG.Attributes.CssStyle.Add("opacity", "200");
            DivUpdateMSG.Attributes.CssStyle.Add("z-index", "200");

            btnUpdMsg.Focus();
            btnUpdMsg.ForeColor = Color.Red;

            LockAllButton();

            lblMsg1.Text = string.Empty;
            lblMsg2.Text = string.Empty;

            lblMsg1.Text = "    NEW ORDER SUCESSFULLY DONE";

            string b = "Generated Order No. ";
            string c = string.Format(lblNewOrderNo.Text);


            lblMsg2.Text = b + c;

        }

        protected void btnUpdMsg_Click(object sender, EventArgs e)
        {
            DivUpdateMSG.Visible = false;

            DivMain.Attributes.CssStyle.Add("opacity", "1");

            UnlockAllButton();

            Response.Redirect(Request.RawUrl);
        }

        protected void LockAllButton()
        {
            btnDeleteImage.Enabled = false;
            ibtnUpload.Enabled = false;
            ddlOrderParty.Enabled = false;
            ddlItemName.Enabled = false;
            txtDeliveryPossibleDate.Enabled = false;

            BtnExit.Enabled = false;
            btnUpdate.Enabled = false;
            btnCancel.Enabled = false;
        }

        protected void UnlockAllButton()
        {
            btnDeleteImage.Enabled = true;
            ibtnUpload.Enabled = true;
            ddlOrderParty.Enabled = true;
            ddlItemName.Enabled = true;
            txtDeliveryPossibleDate.Enabled = true;

            BtnExit.Enabled = true;
            btnUpdate.Enabled = true;
            btnCancel.Enabled = true;
        }
        protected void btnDeleteImage_Click(object sender, EventArgs e)
        {
            string strQuery = "DELETE FROM WFITEMIMAGE WHERE UserId = '" + lblID.Text + "'";
            int rowEffiect = Converter.GetSmallInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZACOMS"));
            if (rowEffiect > 0)
            {
                showimage();
            }
        }

     
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
