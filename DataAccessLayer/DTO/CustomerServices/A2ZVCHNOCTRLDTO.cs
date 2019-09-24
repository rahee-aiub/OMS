using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Utility;
using System.Web.UI.WebControls;
using System.Data;


namespace DataAccessLayer.DTO.CustomerServices
{
    public class A2ZVCHNOCTRLDTO
    {
        #region Propertise

        public int RecCode { set; get; }
        public int RecLastNo { set; get; }
        public int Record { set; get; }

        #endregion


        
        public static A2ZVCHNOCTRLDTO GetLastDefaultVchNo()
        {
            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZCSPARAMETER", "A2ZACOMS");

            var p = new A2ZVCHNOCTRLDTO();
            if (dt.Rows.Count > 0)
            {
                p.RecLastNo = Converter.GetInteger(dt.Rows[0]["LastVoucherNo"]);
                p.RecLastNo = p.RecLastNo + 1;

                p.RecCode = 1;
                int rowEffect = 0;
                string strQuery = "UPDATE A2ZCSPARAMETER set LastVoucherNo='" + p.RecLastNo + "'";
                rowEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZACOMS"));

                return p;
            }
            p.RecCode = 0;
            return p;

        }

        
    }
}
