using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Utility;
using System.Web.UI.WebControls;
using System.Data;

namespace DataAccessLayer.DTO.CustomerServices
{
   public class A2ZSUBREGIONDTO
    {
        #region Propertise

        public int BranchNo { set; get; }
        public int RegNo { set; get; }
        public int SubRegNo { set; get; }
        public String SubRegName { set; get; }
        
        public String AddressL1 { set; get; }
        public String AddressL2 { set; get; }
        public String AddressL3 { set; get; }
       
        public int Division { set; get; }
        public int District { set; get; }

        public int UpZila { set; get; }
        public int Thana { set; get; }

                
        #endregion


        public static int InsertInformation(A2ZSUBREGIONDTO dto)
        {
            dto.SubRegName = (dto.SubRegName != null) ? dto.SubRegName.Trim().Replace("'", "''") : "";
            int rowEffect = 0;
            string strQuery = @"INSERT into A2ZSUBREGION(BranchNo,REGNO,SUBREGNO,SUBREGNAME,SUBREGADDL1,SUBREGADDL2,SUBREGADDL3,
                       SUBREGDIVI,SUBREGDIST,SUBREGUPZILA,SUBREGTHANA)values('" + dto.BranchNo + "','" + dto.RegNo + "','" + dto.SubRegNo + "','" + dto.SubRegName 
                      + "','" + dto.AddressL1 + "','" + dto.AddressL2 + "','" + dto.AddressL3
                      + "','" + dto.Division + "','" + dto.District + "','" + dto.UpZila + "','" + dto.Thana
                      + "')";
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

        public static A2ZSUBREGIONDTO GetInformation(int BranchNo,int RegNo, int SubRegNo)
        {
            DataTable dt = new DataTable();
            string strQuery = "SELECT * FROM A2ZSUBREGION WHERE  BranchNo='" + BranchNo + "' AND  RegNo='" + RegNo + "' AND  SubRegNo='" + SubRegNo + "'";
            dt = BLL.CommonManager.Instance.GetDataTableByQuery(strQuery, "A2ZACOMS");


            var p = new A2ZSUBREGIONDTO();
            if (dt.Rows.Count > 0)
            {

                p.RegNo = Converter.GetInteger(dt.Rows[0]["RegNo"]);
                p.SubRegNo = Converter.GetInteger(dt.Rows[0]["SubRegNo"]);
                p.SubRegName = Converter.GetString(dt.Rows[0]["SubRegName"]);
               
                p.AddressL1 = Converter.GetString(dt.Rows[0]["SubRegAddL1"]);
                p.AddressL2 = Converter.GetString(dt.Rows[0]["SubRegAddL2"]);
                p.AddressL3 = Converter.GetString(dt.Rows[0]["SubRegAddL3"]);
     
                p.Division = Converter.GetInteger(dt.Rows[0]["SubRegDivi"]);
                p.District = Converter.GetInteger(dt.Rows[0]["SubRegDist"]);
                p.Thana = Converter.GetInteger(dt.Rows[0]["SubRegThana"]);
                
                return p;
            }
            p.RegNo = 0;

            return p;

        }

        public static int UpdateInformation(A2ZSUBREGIONDTO dto)
        {
            dto.SubRegName = (dto.SubRegName != null) ? dto.SubRegName.Trim().Replace("'", "''") : "";
            int rowEffect = 0;
            string strQuery = "UPDATE A2ZSUBREGION set BranchNo='" + dto.BranchNo + "',RegNo='" + dto.RegNo + "',SubRegNo='" + dto.SubRegNo + "',SubRegName='" + dto.SubRegName
                + "',SubRegAddL1='" + dto.AddressL1 + "',SubRegAddL2='" + dto.AddressL2 + "',SubRegAddL3='" + dto.AddressL3
                + "',SubRegDivi='" + dto.Division
                + "',SubRegDist='" + dto.District + "',SubRegThana='" + dto.Thana + "'"; 
                
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
