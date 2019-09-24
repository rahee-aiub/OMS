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
using System.Web.UI.WebControls;

namespace ATOZWEBOMS.Pages
{
    public partial class A2ZOMSSendToTransit : System.Web.UI.Page
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

                TransitPartyDropdown();
               

                CalculateBalance();

                OrderNoDropdown();

                TruncateWF();

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


        private void CalculateBalance()
        {
            int result1 = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("[SpM_GenerateAccountBalance]", "A2ZACOMS"));
        }


        protected void TruncateWF()
        {
            string depositQry = "DELETE dbo.WFA2ZTRANSACTION WHERE UserId='" + lblID.Text + "'";
            int rowEffect1 = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(depositQry, "A2ZACOMS"));
        }

        private void TransitPartyDropdown()
        {
            string sqlquery = "SELECT PartyCode,PartyName from A2ZPARTYCODE WHERE GroupCode = 12 ";
            ddlTransitParty = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlTransitParty, "A2ZACOMS");
        }

        private void OrderNoDropdown()
        {
            string sqlquery = "SELECT AccNo,CONCAT(AccNo,' - ',A2ZPARTYCODE.PartyName,' - ',A2ZPARTYCODE_1.PartyName, ' - ',AccBalance) AS AccInfo FROM A2ZACCOUNT LEFT OUTER JOIN A2ZPARTYCODE on A2ZPARTYCODE.PartyCode=A2ZACCOUNT.OrderParty LEFT OUTER JOIN dbo.A2ZPARTYCODE AS A2ZPARTYCODE_1 ON dbo.A2ZACCOUNT.FactoryParty = A2ZPARTYCODE_1.PartyCode WHERE AccType = 55 AND AccLocation = 1 AND AccBalance > 0";

            //string sqlquery = "SELECT AccNo,CONCAT(AccNo,' - ',AccBalance) AS AccInfo FROM A2ZACCOUNT WHERE AccType = 55 AND AccLocation = 1 AND AccBalance > 0";
            ddlOrderNo = DataAccessLayer.BLL.CommonManager.Instance.FillDropDownList(sqlquery, ddlOrderNo, "A2ZACOMS");
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlTransitParty.SelectedIndex == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Transit Party');", true);
                    return;
                }

                if (txtTrnNote.Text == string.Empty)
                {
                    txtTrnNote.Focus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Note');", true);
                    return;
                }

                A2ZVCHNOCTRLDTO getDTO = new A2ZVCHNOCTRLDTO();

                getDTO = (A2ZVCHNOCTRLDTO.GetLastDefaultVchNo());
                CtrlVoucherNo.Text = "ORD" + getDTO.RecLastNo.ToString("000000");

                var prm = new object[4];

                prm[0] = ddlTransitParty.SelectedValue;
                prm[1] = CtrlVoucherNo.Text;
                prm[2] = txtTrnNote.Text;

              
                prm[3] = lblID.Text;

                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("Sp_UpdateTransitTransaction", prm, "A2ZACOMS"));
                if (result == 0)
                {
                    Response.Redirect(Request.RawUrl);
                }
            }
            catch (Exception ex)
            {

                //throw ex;
            }
        }

       
        //protected void BtnAddItem_Click(object sender, EventArgs e)
        //{
        //    if (ddlTransitParty.SelectedIndex == 0)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Transit Code');", true);
        //        return;
        //    }

        //    if (ddlOrderNo.SelectedIndex == 0)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Order No.');", true);
        //        return;
        //    }

           

        //    if (txtGrossWt.Text == string.Empty || Converter.GetDecimal(txtGrossWt.Text) == 0)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Weight');", true);
        //        return;
        //    }


        //    try
        //    {
        //        var prm = new object[7];
        //        prm[0] = Converter.GetDateToYYYYMMDD(CtrlProcDate.Text); // TrnDate
        //        prm[1] = ddlTransitParty.SelectedValue; // TrnKeyNo
        //        prm[2] = ddlOrderNo.SelectedValue; // RefTrnKeyNo
        //        prm[3] = "2"; // FuncOpt
        //        prm[4] = "Send To Transit"; //FuncOptDesc
        //        prm[5] = txtGrossWt.Text; // TrnAmount 
        //        prm[6] = lblID.Text; // UserID

        //        int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("[Sp_InsertWfSendTransit]", prm, "A2ZACOMS"));

        //        if (result == 0)
        //        {
        //            ClearInfo();
        //            gvDetailsInfo();

        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        throw Ex;
        //    }
        //}


        private void ClearInfo()
        {
            ddlOrderNo.SelectedIndex = 0;
            txtGrossWt.Text = string.Empty;
        }

        private void gvDetailsInfo()
        {

            gvDetails.Visible = true;

            string sqlquery = @"SELECT WFA2ZTRANSACTION.Id,WFA2ZTRANSACTION.RefAccNo,WFA2ZTRANSACTION.TrnAmount FROM WFA2ZTRANSACTION WHERE WFA2ZTRANSACTION.UserId = " + lblID.Text + " AND WFA2ZTRANSACTION.TrnFlag = 0 ORDER BY WFA2ZTRANSACTION.Id asc";
            gvDetails = DataAccessLayer.BLL.CommonManager.Instance.FillGridViewList(sqlquery, gvDetails, "A2ZACOMS");
        }
        protected void gvItemDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                Label IdNo = (Label)gvDetails.Rows[e.RowIndex].Cells[0].FindControl("lblId");
                int Id = Converter.GetInteger(IdNo.Text);

                int NId1 = Id + 1;

                int NId2 = NId1 + 1;

                string sqlQuery = string.Empty;
                int rowEffect;
                sqlQuery = @"DELETE  FROM WFA2ZTRANSACTION WHERE  Id = '" + Id + "'";
                rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQuery, "A2ZACOMS"));

                sqlQuery = @"DELETE  FROM WFA2ZTRANSACTION WHERE  Id = '" + NId1 + "'";
                rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQuery, "A2ZACOMS"));

                sqlQuery = @"DELETE  FROM WFA2ZTRANSACTION WHERE  Id = '" + NId2 + "'";
                rowEffect = Converter.GetInteger(DataAccessLayer.BLL.CommonManager.Instance.ExecuteNonQuery(sqlQuery, "A2ZACOMS"));


                gvDetailsInfo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void BalanceWeight()
        {
            string qry = "SELECT AccBalance FROM A2ZACCOUNT WHERE AccLocation = 1 AND AccNo = " + ddlOrderNo.SelectedValue + "";
            DataTable dt = DataAccessLayer.BLL.CommonManager.Instance.GetDataTableByQuery(qry, "A2ZACOMS");
            if (dt.Rows.Count > 0)
            {
                lblAccBalance.Text = Converter.GetString(dt.Rows[0]["AccBalance"]);
            }
        }


        protected void TotalWeight()
        {
            BalanceWeight();

            Decimal sumAmt = 0;


            for (int i = 0; i < gvDetails.Rows.Count; ++i)
            {
                Label keyno = (Label)gvDetails.Rows[i].Cells[1].FindControl("lblTrnKeyNo");
                Label amount = (Label)gvDetails.Rows[i].Cells[2].FindControl("lblTrnAmount");

                if (amount.Text != string.Empty && keyno.Text == ddlOrderNo.SelectedValue)
                {
                    sumAmt += Convert.ToDecimal(String.Format("{0:0,0.00}", amount.Text));
                }
            }


            decimal balance = Converter.GetDecimal(lblAccBalance.Text);

            decimal netbalance = (balance - sumAmt);

            lblAccBalance.Text = Converter.GetString(String.Format("{0:0,0.00}", netbalance));
        }


        protected void txtGrossWt_TextChanged(object sender, EventArgs e)
        {
            if (ddlTransitParty.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Transit Code');", true);
                return;
            }

            if (ddlOrderNo.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Order No.');", true);
                return;
            }



            if (txtGrossWt.Text == string.Empty || Converter.GetDecimal(txtGrossWt.Text) == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Input Weight');", true);
                return;
            }



            TotalWeight();

            decimal InputWeight = Converter.GetDecimal(txtGrossWt.Text);
            decimal BalanceWeight = Converter.GetDecimal(lblAccBalance.Text);

            if (InputWeight > BalanceWeight)
            {
                txtGrossWt.Text = string.Empty;
                txtGrossWt.Focus();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Weight Not Avaiable');", true);
                return;
            }

            try
            {
                var prm = new object[7];
                prm[0] = Converter.GetDateToYYYYMMDD(CtrlProcDate.Text); // TrnDate
                prm[1] = ddlTransitParty.SelectedValue; // TrnKeyNo
                prm[2] = ddlOrderNo.SelectedValue; // RefTrnKeyNo
                prm[3] = "2"; // FuncOpt
                prm[4] = "Send To Transit"; //FuncOptDesc
                prm[5] = txtGrossWt.Text; // TrnAmount 
                prm[6] = lblID.Text; // UserID

                int result = Converter.GetInteger(CommonManager.Instance.GetScalarValueBySp("[Sp_InsertWfSendTransit]", prm, "A2ZACOMS"));

                if (result == 0)
                {

                    ddlTransitParty.Enabled = false;

                    ClearInfo();
                    gvDetailsInfo();

                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        protected void ddlOrderNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtGrossWt.Focus();
        }


    }
}
