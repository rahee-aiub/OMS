using System;
using System.Linq;
using System.Web;
using System.Web.Services;

using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Web.Script.Services;

namespace ATOZWEBOMS.Pages
{
    /// <summary>
    /// Summary description for GMSWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class GMSWebService : System.Web.Services.WebService
    {

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

        public string[] GetLoanPartyName(string prefix,string GroupCode)
        {
            List<string> party = new List<string>();

            string conStrig = "Data Source =  (local) ; Initial Catalog = A2ZACOMS; User Id =; Password =; Integrated Security = SSPI";

            using (SqlConnection con = new SqlConnection(conStrig))
            {
                SqlCommand cmd = new SqlCommand("Sp_GetLoanPartyName", con);

                cmd.Parameters.AddWithValue("@strValue", prefix);
                cmd.Parameters.AddWithValue("@GroupCode", GroupCode);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    party.Add(string.Format("{0}-{1}", rdr["PartyName"].ToString(), rdr["PartyCode"].ToString()));
                }
            }

            return party.ToArray();
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string[] GetCarrPartyName(string prefix)
        {
            List<string> Cparty = new List<string>();

            string conStrig = "Data Source =  (local) ; Initial Catalog = A2ZACOMS; User Id =; Password =; Integrated Security = SSPI";

            using (SqlConnection con = new SqlConnection(conStrig))
            {
                SqlCommand cmd = new SqlCommand("Sp_GetCarrPartyName", con);

                cmd.Parameters.AddWithValue("@strValue", prefix);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Cparty.Add(string.Format("{0}-{1}", rdr["CPartyName"].ToString(), rdr["CPartyCode"].ToString()));
                }
            }

            return Cparty.ToArray();
        }

    }
}
