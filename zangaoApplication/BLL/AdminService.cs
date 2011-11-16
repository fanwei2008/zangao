using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using zangaoApplication.Models;
using zangaoApplication.DAL;
using System.Data.SqlClient;
using System.Data;

namespace zangaoApplication.BLL
{
    public class AdminService
    {
        public Administrator Verify(String name, String pwd)
        {
            AdminDao dao = new AdminDao();
            String query = dao.getVerifyQuery(name,pwd);
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlParameter nameParam = new SqlParameter("@name",SqlDbType.NVarChar);
            nameParam.Value = name;
            parameters.Add(nameParam);
            SqlParameter pwdParam = new SqlParameter("@pwd", SqlDbType.VarChar);
            pwdParam.Value = pwd;
            parameters.Add(pwdParam);
            List<Administrator> administrators = dao.findAdministrator(query, parameters);
            if (administrators.Count > 0)
            {
                return administrators[0];
            }
            else
            {
                return null;
            }
        }
    }
}