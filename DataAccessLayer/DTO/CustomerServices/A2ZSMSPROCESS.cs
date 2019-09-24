using DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;

namespace DataAccessLayer.DTO
{
    public class A2ZSMSPROCESS
    {

        #region Propertise
        public string SMSPort { set; get; }
        public string SMSController { set; get; }
        public string SMSTxtMsg { set; get; }
        public short NoRecord { set; get; }
        
        #endregion

        public static A2ZSMSPROCESS SendSMSInformation(int BranchNo, string VchNo, string MobileNo)
        {
            var p = new A2ZSMSPROCESS();

            p.NoRecord = 0;

            string mNo = MobileNo.Substring(0, 11);

            string FilName = "" + mNo + "-" + BranchNo + "-" + VchNo + "";

            p.SMSTxtMsg = System.IO.File.ReadAllText(@"D:\PrimarySMS\"+FilName+".TXT");


            DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZCSPARAMETER", "A2ZACOMS");

            if (dt.Rows.Count > 0)
            {
                p.SMSPort = Converter.GetString(dt.Rows[0]["PrmSMSPort"]);
            }

            SerialPort SP = new SerialPort();

            string input = p.SMSPort;
            string ProtNm = input.Substring(0, 4);

            SP.PortName = ProtNm;


            SP.Open();
            Thread.Sleep(1000);

            string ph_no;
            ph_no = Char.ConvertFromUtf32(34) + mNo + Char.ConvertFromUtf32(34);

            SP.Write("AT+CMGF=1" + Char.ConvertFromUtf32(13));
            Thread.Sleep(1000);

            SP.Write("AT+CMGS=" + ph_no + Char.ConvertFromUtf32(13));
            Thread.Sleep(1000);

            SP.Write(p.SMSTxtMsg + Char.ConvertFromUtf32(26) + Char.ConvertFromUtf32(13));
            Thread.Sleep(1000);

            SP.Write("AT+CMGS=\"" + mNo + "\"\r\n");

            Thread.Sleep(1000);

            SP.Write(p.SMSTxtMsg + "\x1A");

            Thread.Sleep(1000);

            SP.Close();

            p.NoRecord = 1;

            return p;

           
        }
              
    }
}
