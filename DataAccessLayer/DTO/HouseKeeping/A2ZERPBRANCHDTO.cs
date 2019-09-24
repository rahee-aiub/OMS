using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Utility;
using System.Web.UI.WebControls;
using System.Data;
namespace DataAccessLayer.DTO.HouseKeeping
{
    public class A2ZERPBRANCHDTO
    {
        #region Propertise

        public int BranchNo { set; get; }
        public String BranchName { set; get; }
        public String BranchNameB { set; get; }
        public String BranchAdd1 { set; get; }
        public String BranchAdd1B { set; get; }
        public String BranchAdd2 { set; get; }
        public String BranchAdd3 { set; get; }
        public int ProcStatus { set; get; }
        public String ProcStatusDesc { set; get; }
        public DateTime ProcStatusDate { set; get; }
        public int UserId { set; get; }
        #endregion


        //public static int InsertInformation(A2ZERPBRANCHDTO dto)
        //{
        //    dto.BankName = (dto.BankName != null) ? dto.BankName.Trim().Replace("'", "''") : "";
        //    int rowEffect = 0;
        //    string strQuery = @"INSERT into A2ZERPBRANCH(BankCode,BankName)values('" + dto.BankCode + "','" + dto.BankName + "')";
        //    rowEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHKOMS"));

        //    if (rowEffect == 0)
        //    {
        //        return 0;
        //    }
        //    else
        //    {
        //        return 1;
        //    }
        //}

        public static A2ZERPBRANCHDTO GetInformation(int branchno)
        {
            //var prm = new object[1];

            //prm[0] = branchno;



            //DataTable dt = BLL.CommonManager.Instance.GetDataTableBySpWithParams("Sp_HRGetInfoBank", prm, "A2ZHRCUBS");

            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZERPBRANCH WHERE BranchNo = " + branchno, "A2ZHKOMS");


            var p = new A2ZERPBRANCHDTO();
            if (dt.Rows.Count > 0)
            {

                p.BranchNo = Converter.GetInteger(dt.Rows[0]["BranchNo"]);
                p.BranchName = Converter.GetString(dt.Rows[0]["BranchName"]);
                p.BranchNameB = Converter.GetString(dt.Rows[0]["BranchNameB"]);
                p.BranchAdd1 = Converter.GetString(dt.Rows[0]["BranchAdd1"]);
                p.BranchAdd1B = Converter.GetString(dt.Rows[0]["BranchAdd1B"]);
                p.BranchAdd2 = Converter.GetString(dt.Rows[0]["BranchAdd2"]);
                p.BranchAdd3 = Converter.GetString(dt.Rows[0]["BranchAdd3"]);
                p.ProcStatus = Converter.GetInteger(dt.Rows[0]["ProcStatus"]);
                p.ProcStatusDesc = Converter.GetString(dt.Rows[0]["ProcStatusDesc"]);
                p.ProcStatusDate = Converter.GetDateTime(dt.Rows[0]["ProcStatusDate"]);
                p.UserId = Converter.GetInteger(dt.Rows[0]["UserId"]);
                return p;
            }
            p.BranchNo = 0;

            return p;

        }
        //public static int UpdateInformation(A2ZERPBRANCHDTO dto)
        //{
        //    dto.BankName = (dto.BankName != null) ? dto.BankName.Trim().Replace("'", "''") : "";
        //    int rowEffect = 0;
        //    string strQuery = "UPDATE A2ZBANK set BankCode='" + dto.BankCode + "',BankName='" + dto.BankName + "' where BankCode='" + dto.BankCode + "'";
        //    rowEffect = Converter.GetInteger(BLL.CommonManager.Instance.ExecuteNonQuery(strQuery, "A2ZHRCUBS"));
        //    if (rowEffect == 0)
        //    {
        //        return 0;
        //    }
        //    else
        //    {
        //        return 1;
        //    }
        //}
    }
}

