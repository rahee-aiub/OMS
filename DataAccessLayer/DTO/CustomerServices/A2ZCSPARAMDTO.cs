using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Utility;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;

namespace DataAccessLayer.DTO.CustomerServices
{
  public  class A2ZCSPARAMDTO
  {
      #region Propertise

        public int AccType { set; get; }
        public int AccTypeClass { set; get; }
        public Int16 CalculationPeriod { set; get; }
        public Int16 CalculationMethod { set; get; }
        public Int16 LoanCalculationMethod { set; get; }
        public decimal InterestRate { set; get; }
        public decimal FundRate { set; get; }
        public Int16 ProductCondition { set; get; }
        public Int16 ProductInterestType { set; get; }
        public decimal MinDepositAmt { set; get; }
        public Int16 IntWithdrDays { set; get; }
        public Int16 RoundFlag { set; get; }
        public decimal AccProcFees { set; get; }
        public decimal AccClosingFees { set; get; }
        public decimal PenalAmount { set; get; }
        public DateTime ProvBegDate { set; get; }
        public String ProvBegNullDate { set; get; }
        public Int16 BenefitBy { set; get; }
        public Int16 PeriodSlab { set; get; }
        public Int16 MandatoryDepFlag { set; get; }
        public Int16 AutoDepositFlag { set; get; }
        public decimal AutoDepositFees { set; get; }
        public decimal MinBalanceAmt { set; get; }
        public Int16 ProvisionFlag { set; get; }
        public Int16 RenewalFlag { set; get; }
        public Int16 AnniversaryFlag { set; get; }
        public Int16 BenefitWBy { set; get; }
        public Int16 SurityPictureFrom { set; get; }
      #endregion

        public static int InsertInformation(A2ZCSPARAMDTO dto)
        {
            SqlDateTime sqldatenull;
            sqldatenull = SqlDateTime.Null;

            SqlParameter param1 = new SqlParameter("@ProvBegDate", DBNull.Value);


            if (dto.ProvBegNullDate == "")
            {

                param1 = new SqlParameter("@ProvBegDate", DBNull.Value);
            }
            else
            {
                param1 = new SqlParameter("@ProvBegDate", dto.ProvBegDate);
            }



            int result = Helper.SqlHelper.ExecuteNonQuery(DataAccessLayer.Constants.DBConstants.GetConnectionString("A2ZACOMS"), "Sp_CSParamDataInsert", new object[] { dto.AccType, dto.AccTypeClass, dto.CalculationPeriod, dto.CalculationMethod, dto.LoanCalculationMethod, dto.InterestRate, dto.FundRate, dto.ProductCondition, dto.ProductInterestType, dto.IntWithdrDays, dto.MinDepositAmt, dto.RoundFlag, dto.AccProcFees, dto.AccClosingFees, dto.PenalAmount, param1, dto.BenefitBy, dto.BenefitWBy, dto.SurityPictureFrom, dto.MandatoryDepFlag, dto.AutoDepositFlag, dto.AutoDepositFees, dto.MinBalanceAmt, dto.ProvisionFlag, dto.RenewalFlag, dto.AnniversaryFlag, dto.PeriodSlab });

            if (result == 0)
            {
                return 0;
            }
            else
            {
                return 1;
            }

        }


        public static A2ZCSPARAMDTO GetInformation(int AccType)
        {
            var prm = new object[1];

            prm[0] = AccType;
            
            DataTable dt = BLL.CommonManager.Instance.GetDataTableBySpWithParams("Sp_CSGetInfoParam", prm, "A2ZACOMS");
            
            
            
            //DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZCSPARAM WHERE AccType = '" + AccType + "'", "A2ZCSMCUS");


            var p = new A2ZCSPARAMDTO();
            if (dt.Rows.Count > 0)
            {
                p.AccType = Converter.GetInteger(dt.Rows[0]["AccType"]);
                p.AccTypeClass = Converter.GetInteger(dt.Rows[0]["AccTypeClass"]);
                p.CalculationPeriod = Converter.GetSmallInteger(dt.Rows[0]["PrmCalPeriod"]);
                p.CalculationMethod = Converter.GetSmallInteger(dt.Rows[0]["PrmCalMethod"]);
                p.LoanCalculationMethod = Converter.GetSmallInteger(dt.Rows[0]["PrmLoanCalMethod"]);
                p.InterestRate= Converter.GetDecimal(dt.Rows[0]["PrmIntRate"]);
                p.FundRate = Converter.GetDecimal(dt.Rows[0]["PrmFundRate"]);
                p.ProductCondition = Converter.GetSmallInteger(dt.Rows[0]["PrmProdCon"]);
                p.ProductInterestType = Converter.GetSmallInteger(dt.Rows[0]["PrmProdIntType"]);
                p.MinDepositAmt = Converter.GetDecimal(dt.Rows[0]["PrmMinDeposit"]);
                p.RoundFlag = Converter.GetSmallInteger(dt.Rows[0]["PrmRoundFlag"]);
                p.IntWithdrDays = Converter.GetSmallInteger(dt.Rows[0]["PrmIntWithdrDays"]);
                p.AccProcFees = Converter.GetDecimal(dt.Rows[0]["PrmAccProcFees"]);
                p.AccClosingFees = Converter.GetDecimal(dt.Rows[0]["PrmAccClosingFees"]);
                p.PenalAmount = Converter.GetDecimal(dt.Rows[0]["PrmPenalAmt"]);
                p.ProvBegDate = Converter.GetDateTime(dt.Rows[0]["PrmProvBegDate"]);
                p.BenefitBy = Converter.GetSmallInteger(dt.Rows[0]["PrmBenefitBy"]);
                p.BenefitWBy = Converter.GetSmallInteger(dt.Rows[0]["PrmBenefitWBy"]);
                p.SurityPictureFrom = Converter.GetSmallInteger(dt.Rows[0]["PrmSurityPictureFrom"]);

                p.MandatoryDepFlag = Converter.GetSmallInteger(dt.Rows[0]["PrmMandatoryDepFlag"]);
                p.AutoDepositFlag = Converter.GetSmallInteger(dt.Rows[0]["PrmAutoDepositFlag"]);
                p.AutoDepositFees = Converter.GetDecimal(dt.Rows[0]["PrmAutoDepositFees"]);
                p.MinBalanceAmt = Converter.GetDecimal(dt.Rows[0]["PrmMinBalanceAmt"]);
                p.ProvisionFlag = Converter.GetSmallInteger(dt.Rows[0]["PrmProvisionFlag"]);
                p.RenewalFlag = Converter.GetSmallInteger(dt.Rows[0]["PrmRenewalFlag"]);
                p.AnniversaryFlag = Converter.GetSmallInteger(dt.Rows[0]["PrmAnniversaryFlag"]);

                return p;

            }
            else
            {
                p.AccType = 0;
            
            }


            return p;



        }


        public static int UpdateInformation(A2ZCSPARAMDTO dto)
        {

            SqlDateTime sqldatenull;
            sqldatenull = SqlDateTime.Null;

            SqlParameter param1 = new SqlParameter("@ProvBegDate", DBNull.Value);


            if (dto.ProvBegNullDate == "")
            {

                param1 = new SqlParameter("@ProvBegDate", DBNull.Value);
            }
            else
            {
                param1 = new SqlParameter("@ProvBegDate", dto.ProvBegDate);
            }



            int result = Helper.SqlHelper.ExecuteNonQuery(DataAccessLayer.Constants.DBConstants.GetConnectionString("A2ZACOMS"), "Sp_CSParamDataUpdate", new object[] { dto.AccType, dto.AccTypeClass, dto.CalculationPeriod, dto.CalculationMethod, dto.LoanCalculationMethod, dto.InterestRate, dto.FundRate, dto.ProductCondition, dto.ProductInterestType, dto.IntWithdrDays, dto.MinDepositAmt, dto.RoundFlag, dto.AccProcFees, dto.AccClosingFees, dto.PenalAmount, param1, dto.BenefitBy, dto.BenefitWBy, dto.SurityPictureFrom, dto.MandatoryDepFlag, dto.AutoDepositFlag, dto.AutoDepositFees, dto.MinBalanceAmt, dto.ProvisionFlag, dto.RenewalFlag, dto.AnniversaryFlag, dto.PeriodSlab });

            if (result == 0)
            {
                return 0;
            }
            else
            {
                return 1;
            }

        }

       

  }



}
