using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace zangaoApplication.Models
{
    public class Article:IModel
    {
        private String tableMap;
        public String Table
        {
            get
            {
                return tableMap;
            }
            set
            {
                this.tableMap = value;
            }
        }
        //wogaideoodfaofafa

    }
}