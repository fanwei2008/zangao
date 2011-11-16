using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace zangaoApplication.Utils
{
    public class WebApplicationUtil
    {
        public static String getConnectionString()
        {
            return System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
        }
    }
}