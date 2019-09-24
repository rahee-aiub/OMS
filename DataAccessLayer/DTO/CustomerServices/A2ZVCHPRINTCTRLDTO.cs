using System;
using System.Data;
using System.Web.UI.WebControls;
using DataAccessLayer.Utility;


namespace DataAccessLayer.DTO.CustomerServices
{
    public class A2ZVCHPRINTCTRLDTO
    {
        #region Propertise
        public int FuncOpt { set; get; }
       
        public Int16 VchPrintFlag { set; get; }
        
        #endregion

        public static A2ZVCHPRINTCTRLDTO GetVprintInformation(int funcopt, string dbName)
        {
            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZVCHCTRL WHERE FuncOpt = " + funcopt, dbName);

            var p = new A2ZVCHPRINTCTRLDTO();

            if (dt.Rows.Count > 0)
            {
                p.FuncOpt = Converter.GetInteger(dt.Rows[0]["FuncOpt"]);
                p.VchPrintFlag = Converter.GetSmallInteger(dt.Rows[0]["VchPrintFlag"]);
                        
                return p;
            }

            p.FuncOpt = 0;
            return p;

        }

      

    }
}
