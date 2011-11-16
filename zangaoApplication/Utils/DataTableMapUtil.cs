using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace zangaoApplication.Utils
{
    public static class DataTableMapUtil
    {
        public static String getTableMap(String model)
        {
            if (model.Equals("category"))
            {
                return "category";
            }
            else if(model.Equals("aministrator"))
            {
                return "admin";
            }
            else
            {
                return null;
            }
        }
    }
}