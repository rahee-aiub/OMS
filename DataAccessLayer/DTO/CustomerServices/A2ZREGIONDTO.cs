using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Utility;
using System.Web.UI.WebControls;
using System.Data;

namespace DataAccessLayer.DTO.CustomerServices
{
   public class A2ZREGIONDTO
    {
        #region Propertise
        public int BranchNo { set; get; }
        public int RegNo { set; get; }
        public String RegName { set; get; }
        
        public String AddressL1 { set; get; }
        public String AddressL2 { set; get; }
        public String AddressL3 { set; get; }
       
        public int Division { set; get; }
        public int District { set; get; }

        public int UpZila { set; get; }
        public int Thana { set; get; }
        public int CollNo { set; get; }

                
        #endregion


        public static int InsertInformation(A2ZREGIONDTO dto)
        {
            dto.RegName = (dto.RegName != null) ? dto.RegName.Trim().Replace("'", "''") : "";
            int rowEffect = 0;
            string strQuery = @"INSERT into A2ZREGION(BranchNo,REGNO,REGNAME,REGADDL1,REGADDL2,REGADDL3,
                       REGDIVI,REGDIST,REGUPZILA,REGTHANA,ColNo)values('" + dto.BranchNo + "','" + dto.RegNo + "','" + dto.RegName 
                      + "','" + dto.AddressL1 + "','" + dto.AddressL2 + "','" + dto.AddressL3
                      + "','" + dto.Division + "','" + dto.District + "','" + dto.UpZila + "','" + dto.Thana
                      + "','" + dto.CollNo + "')";
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

        public static A2ZREGIONDTO GetInformation(int BranchNo,int RegNo)
        {
           
            DataTable dt = new DataTable();
            string strQuery = "SELECT * FROM A2ZREGION WHERE  BranchNo='" + BranchNo + "' AND  RegNo='" + RegNo + "'";
            dt = BLL.CommonManager.Instance.GetDataTableByQuery(strQuery, "A2ZACOMS");



            var p = new A2ZREGIONDTO();
            if (dt.Rows.Count > 0)
            {

                p.RegNo = Converter.GetInteger(dt.Rows[0]["RegNo"]);
                p.RegName = Converter.GetString(dt.Rows[0]["RegName"]);
               
                p.AddressL1 = Converter.GetString(dt.Rows[0]["RegAddL1"]);
                p.AddressL2 = Converter.GetString(dt.Rows[0]["RegAddL2"]);
                p.AddressL3 = Converter.GetString(dt.Rows[0]["RegAddL3"]);
     
                p.Division = Converter.GetInteger(dt.Rows[0]["RegDivi"]);
                p.District = Converter.GetInteger(dt.Rows[0]["RegDist"]);
                p.Thana = Converter.GetInteger(dt.Rows[0]["RegThana"]);
                p.CollNo = Converter.GetInteger(dt.Rows[0]["ColNo"]);
                
                return p;
            }
            p.RegNo = 0;

            return p;

        }


        public static A2ZREGIONDTO GetInfoCollWise(int BranchNo, int CollNum, int RegNo)
        {

            DataTable dt = new DataTable();
            string strQuery = "SELECT * FROM A2ZREGION WHERE  BranchNo='" + BranchNo + "' AND  RegNo='" + RegNo + "' AND  ColNo='" + CollNum + "'";
            dt = BLL.CommonManager.Instance.GetDataTableByQuery(strQuery, "A2ZACOMS");



            var p = new A2ZREGIONDTO();
            if (dt.Rows.Count > 0)
            {

                p.RegNo = Converter.GetInteger(dt.Rows[0]["RegNo"]);
                p.RegName = Converter.GetString(dt.Rows[0]["RegName"]);

                p.AddressL1 = Converter.GetString(dt.Rows[0]["RegAddL1"]);
                p.AddressL2 = Converter.GetString(dt.Rows[0]["RegAddL2"]);
                p.AddressL3 = Converter.GetString(dt.Rows[0]["RegAddL3"]);

                p.Division = Converter.GetInteger(dt.Rows[0]["RegDivi"]);
                p.District = Converter.GetInteger(dt.Rows[0]["RegDist"]);
                p.Thana = Converter.GetInteger(dt.Rows[0]["RegThana"]);
                p.CollNo = Converter.GetInteger(dt.Rows[0]["ColNo"]);

                return p;
            }
            p.RegNo = 0;

            return p;

        }

        public static int UpdateInformation(A2ZREGIONDTO dto)
        {
            dto.RegName = (dto.RegName != null) ? dto.RegName.Trim().Replace("'", "''") : "";
            int rowEffect = 0;
            string strQuery = "UPDATE A2ZREGION set BranchNo='" + dto.BranchNo + "',RegNo='" + dto.RegNo + "',RegName='" + dto.RegName
                + "',RegAddL1='" + dto.AddressL1 + "',RegAddL2='" + dto.AddressL2 + "',RegAddL3='" + dto.AddressL3
                + "',RegDivi='" + dto.Division
                + "',RegDist='" + dto.District + "',RegThana='" + dto.Thana + "',ColNo='" + dto.CollNo + "'  where  BranchNo='" + dto.BranchNo + "' AND RegNo='" + dto.RegNo + "'"; 
                
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
