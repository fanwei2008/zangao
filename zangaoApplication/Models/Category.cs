using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using zangaoApplication.Utils;

namespace zangaoApplication.Models
{
    public class Category:IModel
    {
        private String tableMap;
        public String Table
        {
            get
            {
                return this.tableMap;
            }
            set
            {
                this.tableMap = value;
            }
        }

        public Category()
        {
            this.tableMap = DataTableMapUtil.getTableMap("category");
            AdminUtil admin = new AdminUtil();
            this.AddUser=admin.getAdministrator();
        }

        public int AddUser
        {
            get;
            set;
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

        public String Description
        {
            get;
            set;
        }

        public int Sort
        {
            get;
            set;
        }

        public Category Parent
        {
            get;
            set;
        }

        public int Pid
        {
            get;
            set;
        }

        public State Status
        {
            get;
            set;
        }

        public bool IsFirstPage
        {
            get;
            set;
        }

        public DateTime AddTime
        {
            get;
            set;
        }
    }
}