using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Utility;
using System.Web.UI.WebControls;
using System.Data;
using DataAccessLayer.BLL;

namespace DataAccessLayer.DTO.CustomerServices
{
  public  class A2ZITEMSDTO
    {
      #region Propertise
      public int ItemsCode { set; get; }
      public String ItemsName { set; get; }
     
      #endregion


        public static int InsertInformation(A2ZITEMSDTO dto)
        {
            dto.ItemsName = (dto.ItemsName != null) ? dto.ItemsName.Trim().Replace("'", "''") : "";
            int rowEffect = 0;
            string strQuery = @"INSERT into A2ZITEMSCODE(ItemsCode,ItemsName)values('" + dto.ItemsCode + "','" + dto.ItemsName + "')";
            rowEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZACOMS"));

            if (rowEffect == 0)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        public static A2ZITEMSDTO GetInformation(int ItemsCode)
        {
            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZITEMSCODE WHERE ItemsCode = " + ItemsCode, "A2ZACOMS");


            var p = new A2ZITEMSDTO();
            if (dt.Rows.Count > 0)
            {

                p.ItemsCode = Converter.GetInteger(dt.Rows[0]["ItemsCode"]);
                p.ItemsName = Converter.GetString(dt.Rows[0]["ItemsName"]);
                return p;
            }
            p.ItemsCode = 0;

            return p;

        }
        public static int UpdateInformation(A2ZITEMSDTO dto)
        {
            dto.ItemsName = (dto.ItemsName != null) ? dto.ItemsName.Trim().Replace("'", "''") : "";
            int rowEffect = 0;
            string strQuery = "UPDATE A2ZITEMSCODE set ItemsCode='" + dto.ItemsCode + "',ItemsName='" + dto.ItemsName + "' where ItemsCode='" + dto.ItemsCode + "'";
            rowEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZACOMS"));
            if (rowEffect == 0)
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
