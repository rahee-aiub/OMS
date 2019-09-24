using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;

namespace A2ZERPWEB.WebLogger
{
    public static class SimpleLogger
    {
        private static readonly string FILENAME = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SimpleLogger.log");

        public static void Log(string message)
        {
            try
            {
                File.AppendAllText(FILENAME, message + "\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }

        public static void Log(string template, params object[] items)
        {
            Log(string.Format(template, items));
        }
    }
}
