using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using zangaoApplication.Models;
using System.Data.SqlClient;
using zangaoApplication.Utils;
using System.Data;

namespace zangaoApplication.DAL
{
    public class AdminDao
    {
        private String baseQuery = "select * from admin where 1=1 ";
        public String getVerifyQuery(String name, String pwd)
        {
            String condition = "and admin_name=@name and admin_pwd=@pwd";
            String query = this.baseQuery + condition;
            return query;
        }


        public List<Administrator> findAdministrator(String query)
        {
            return findAdministrator(query, null);
        }


        public List<Administrator> findAdministrator(String query,List<SqlParameter> paramters)
        {
            List<Administrator> administrators =new List<Administrator>();
            SqlConnection conn = new SqlConnection(WebApplicationUtil.getConnectionString());
            try
            {
                
                conn.Open();
                SqlCommand command = new SqlCommand(query, conn);
                if (paramters != null)
                {
                    foreach (SqlParameter param in paramters)
                    {
                        command.Parameters.Add(param);
                    }
                }
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Administrator administrator = new Administrator();
                    administrator.Id = (int)reader.GetInt32(reader.GetOrdinal("admin_id"));
                    administrator.Name = (String)reader.GetValue(reader.GetOrdinal("admin_name"));
                    administrator.Pwd = (String)reader.GetValue(reader.GetOrdinal("admin_pwd"));
                    object email = reader.GetValue(reader.GetOrdinal("admin_email"));
                    if (!(email is DBNull))
                    {
                        administrator.Email = (String)email;
                    }
                    object lastlogintime = reader.GetValue(reader.GetOrdinal("admin_login_date"));
                    if (!(lastlogintime is DBNull))
                    {
                        administrator.LastLoginTime = (DateTime)lastlogintime;
                    }
                   // administrator.LastLoginTime = (DateTime)reader.GetDateTime(reader.GetOrdinal("admin_login_date"));
                    int status = reader.GetInt32(reader.GetOrdinal("admin_status"));
                    switch (status)
                    {
                        case 0:
                            administrator.Status = State.Delete;
                            break;
                        case 1:
                            administrator.Status = State.Normal;
                            break;
                        case 2:
                            administrator.Status = State.Verify;
                            break;
                        case 3:
                            administrator.Status = State.Forbidden;
                            break;
                        case 4:
                            administrator.Status = State.Expiration;
                            break;
                        default:
                            administrator.Status = State.Unknow;
                            break;
                    }
                    object sort = reader.GetValue(reader.GetOrdinal("admin_sort"));
                    if (!(sort is DBNull))
                    {
                        administrator.Sort = (int)sort;
                    }
                  //  administrator.Sort = reader.GetInt32(reader.GetOrdinal("admin_sort"));
                    administrator.Level = reader.GetInt32(reader.GetOrdinal("admin_level"));
                    administrator.Super = reader.GetInt32(reader.GetOrdinal("is_super"));
                    administrators.Add(administrator);
                }
                conn.Close();
            }
            catch
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return administrators;
        }

        

    }
}