using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using zangaoApplication.Utils;

namespace zangaoApplication.Models
{
    public class Administrator:IModel
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

        public Administrator()
        {
            this.tableMap = DataTableMapUtil.getTableMap("");
        }
        public int Id
        {
            get;
            set;
        }

        public String Name
        {
            get;
            set;
        }

        public String Pwd
        {
            get;
            set;
        }

        public String Email
        {
            get;
            set;
        }

        public DateTime LastLoginTime
        {
            get;
            set;
        }

        public int Sort
        {
            get;
            set;
        }

        public State Status
        {
            get;
            set;
        }

        public int Super
        {
            get;
            set;
        }

        public int Level
        {
            get;
            set;
        }
    }
}