using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Utility;
using System.Web.UI.WebControls;
using System.Data;


namespace DataAccessLayer.DTO.CustomerServices
{
    public class A2ZRECCTRLDTO
    {
        #region Propertise

        public Int16 CtrlRecType { set; get; }
        public int CtrlRecLastNo { set; get; }

        public int GLCashCode { set; get; }

        public int RecLastVchNo { set; get; }

        #endregion

        public static A2ZRECCTRLDTO UpdateLastRecords(Int16 RecType,int Code)
        {

            DataTable dt = new DataTable();
            string strQuery = "SELECT * FROM A2ZRECCTRL WHERE CtrlRecType='" + RecType + "'";
            dt = BLL.CommonManager.Instance.GetDataTableByQuery(strQuery, "A2ZACOMS");

            var p = new A2ZRECCTRLDTO();
            if (dt.Rows.Count > 0)
            {

                p.CtrlRecType = Converter.GetSmallInteger(dt.Rows[0]["CtrlRecType"]);
                p.CtrlRecLastNo = Converter.GetInteger(dt.Rows[0]["CtrlRecLastNo"]);
                p.CtrlRecLastNo = p.CtrlRecLastNo + 1;

                int rowEffect = 0;
                string str1Query = "UPDATE A2ZRECCTRL set CtrlRecLastNo='" + Code + "' where CtrlRecType='" + p.CtrlRecType + "'";
                rowEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(str1Query, "A2ZACOMS"));

                return p;
            }
            p.CtrlRecType = 0;
            return p;

        }

        public static A2ZRECCTRLDTO GetLastRecords(Int16 RecType)
        {

            DataTable dt = new DataTable();
            string strQuery = "SELECT * FROM A2ZRECCTRL WHERE  CtrlRecType='" + RecType + "'";
            dt = BLL.CommonManager.Instance.GetDataTableByQuery(strQuery, "A2ZACOMS");

            var p = new A2ZRECCTRLDTO();
            if (dt.Rows.Count > 0)
            {

                p.CtrlRecType = Converter.GetSmallInteger(dt.Rows[0]["CtrlRecType"]);
                p.CtrlRecLastNo = Converter.GetInteger(dt.Rows[0]["CtrlRecLastNo"]);
                p.CtrlRecLastNo = p.CtrlRecLastNo + 1;

                int rowEffect = 0;
                string str1Query = "UPDATE A2ZRECCTRL set CtrlRecLastNo='" + p.CtrlRecLastNo + "' where CtrlRecType='" + p.CtrlRecType + "'";
                rowEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(str1Query, "A2ZACOMS"));

                return p;
            }
            p.CtrlRecType = 0;
            return p;

        }
        public static A2ZRECCTRLDTO ReadLastRecords(Int16 RecType)
        {

            DataTable dt = new DataTable();
            string strQuery = "SELECT * FROM A2ZRECCTRL WHERE  CtrlRecType='" + RecType + "'";
            dt = BLL.CommonManager.Instance.GetDataTableByQuery(strQuery, "A2ZACOMS");

            var p = new A2ZRECCTRLDTO();
            if (dt.Rows.Count > 0)
            {

                p.CtrlRecType = Converter.GetSmallInteger(dt.Rows[0]["CtrlRecType"]);
                p.CtrlRecLastNo = Converter.GetInteger(dt.Rows[0]["CtrlRecLastNo"]);


                return p;
            }
            p.CtrlRecType = 0;
            return p;

        }
       
    }
}
