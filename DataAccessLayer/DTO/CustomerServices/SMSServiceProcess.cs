using DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace DataAccessLayer.DTO.CustomerServices
{
    public class SMSSERVICEPROCESS
    {

        //public static SMSSERVICEPROCESS Deposit(int Typecode)
        //{
        //    string MemMobileNo;
        //    double BalanceAmt;
        //    string TrnDate;
        //    double CurrDeposit;


        //    DataTable dt = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT * FROM A2ZTRANSACTION WHERE SMSSwitch = '0' AND FuncNo = '1'", "A2ZACOMS");

        //    if (dt.Rows.Count > 0)
        //    {
        //        TrnDate = Converter.GetString(dt.Rows[0]["TrnDate"]);

        //        BalanceAmt = Converter.GetDouble(dt.Rows[0]["AccBalance"]);
                
        //        CurrDeposit = Converter.GetSmallInteger(dt.Rows[0]["TrnDebit"]);

        //    }



        //    DataTable dt1 = BLL.CommonManager.Instance.GetDataTableByQuery("SELECT MemPreMobile FROM A2ZMEMBER WHERE ", "A2ZACOMS");

        //    if (dt1.Rows.Count > 0)
        //    {
        //        MemMobileNo = Converter.GetString(dt.Rows[0]["MemPreMobile"]);
        //    }


        //    string path = @"c:\Primary\sms.txt";
        //    if (!File.Exists(path))
        //    {
        //        // Create a file to write to.
        //        using (StreamWriter sw = File.CreateText(path))
        //        {
        //            sw.WriteLine("Hello");
        //            sw.WriteLine("And");
        //            sw.WriteLine("Welcome");
        //        }
        //    }

        //    // Open the file to read from.
        //    using (StreamReader sr = File.OpenText(path))
        //    {
        //        string s = "";
        //        while ((s = sr.ReadLine()) != null)
        //        {
        //            Console.WriteLine(s);
        //        }
        //    }


        //    return 0;

        //}


    }


}
