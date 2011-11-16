using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using zangaoApplication.Models;
using System.Text;
using System.Data.SqlClient;
using zangaoApplication.Utils;
using System.Data;

namespace zangaoApplication.DAL
{
    public class CategoryDao
    {
        public void save(Category category)
        {
            String insertQuery = "insert into " + category.Table;
            int id;
            StringBuilder columns = new StringBuilder("(");
            StringBuilder values = new StringBuilder("(");
            if(category.Name!=null&&!category.Name.Equals(""))
            {
                columns.Append("category_name,");
                values.Append("'").Append(category.Name).Append("'").Append(",");
            }
            if(category.Pid>=0)
            {
                columns.Append("category_pid,");
                values.Append(category.Pid.ToString()).Append(",");
            }
            if (category.Sort != null)
            {
                columns.Append("category_sort,");
                values.Append(category.Sort.ToString()).Append(",");
            }
            if (category.Description != null && !category.Description.Equals(""))
            {
                columns.Append("category_desc,");
                values.Append("'").Append(category.Description).Append("'").Append(",");
            }
            if (category.Status >= 0)
            {
                switch (category.Status)
                {
                    case State.Delete:
                        values.Append("0").Append(",");
                        break;
                    case State.Normal:
                        values.Append("1").Append(",");
                        break;
                    case State.Expiration:
                        values.Append("4").Append(",");
                        break;
                    case State.Forbidden:
                        values.Append("3").Append(",");
                        break;
                    case State.Verify:
                        values.Append("2").Append(",");
                        break;
                    case State.Unknow:
                        values.Append("5").Append(",");
                        break;
                    default:
                        values.Append("5").Append(",");
                        break;
                }
                columns.Append("category_status,");
                values.Append(Convert.ToInt32(category.Status).ToString()).Append(",");
            }
            if (category.AddUser >= 0)
            {
                columns.Append("add_user");
                values.Append(category.AddUser.ToString()).Append(",");
            }
            columns.Append("category_weight").Append(",");
            values.Append(category.Weight.ToString()).Append(",");
            columns.Append("category_page").Append(",");
            if (category.IsFirstPage)
            {
                values.Append("1").Append(",");
            }
            else
            {
                values.Append("0").Append(",");
            }

            columns.Append("add_date").Append(")");
            values.Append("GETDATE()").Append(")") ;
            insertQuery = insertQuery + columns + " values" + values;
            String insertResult = "select max(category_id) from category";
            SqlConnection conn = new SqlConnection(WebApplicationUtil.getConnectionString());
            try
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();

                //tran.IsolationLevel = System.Data.IsolationLevel.Serializable;
                SqlCommand command = new SqlCommand();
                command.Connection = conn;
                command.Transaction = tran;
                command.CommandText = insertQuery;
                try
                {
                    command.ExecuteNonQuery();
                    SqlCommand queryCommand = new SqlCommand(insertResult, conn);
                    queryCommand.Transaction = tran;
                    id = (int)queryCommand.ExecuteScalar();
                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                    throw new Exception("execute query has erro!");
                }
                conn.Close();
                category.Id = id;
            }
            catch
            {
                if(conn!=null)
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }      
        }



        public List<Category> findCategorys(String query,List<SqlParameter> parameters)
        {
            List<Category> categorys = new List<Category>();
            SqlConnection conn = new SqlConnection(WebApplicationUtil.getConnectionString());
            try
            {
                conn.Open();
                SqlCommand command = new SqlCommand(query, conn);
                if (parameters != null)
                {
                    foreach (SqlParameter param in parameters)
                    {
                        command.Parameters.Add(param);
                    }
                }
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Category category = new Category();
                    category.Id = reader.GetInt32(reader.GetOrdinal("category_id"));
                    category.Name = reader.GetString(reader.GetOrdinal("category_name"));
                    category.Pid = reader.GetInt32(reader.GetOrdinal("category_pid"));
                    category.Sort = reader.GetInt32(reader.GetOrdinal("category_sort"));
                    category.Weight = reader.GetInt32(reader.GetOrdinal("category_weight"));
                    category.AddUser = reader.GetInt32(reader.GetOrdinal("add_user"));
                    category.AddTime = reader.GetDateTime(reader.GetOrdinal("add_date"));
                    object desc = reader.GetValue(reader.GetOrdinal("category_desc"));
                    if (desc != null)
                    {
                        category.Description = (String)desc;
                    }
                    int stats = reader.GetInt32(reader.GetOrdinal("category_status"));
                    switch (stats)
                    {
                        case 0:
                            category.Status = State.Delete;
                            break;
                        case 1:
                            category.Status = State.Normal;
                            break;
                        case 2:
                            category.Status = State.Verify;
                            break;
                        case 3:
                            category.Status = State.Forbidden;
                            break;
                        case 4:
                            category.Status = State.Expiration;
                            break;
                        default:
                            category.Status = State.Unknow;
                            break;
                    }
                    int isFirstPage =(int)reader.GetValue(reader.GetOrdinal("category_page"));
                    if (isFirstPage == 1)
                    {
                        category.IsFirstPage = true;
                    }
                    else
                    {
                        category.IsFirstPage = false;
                    }
                    categorys.Add(category);
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
            return categorys;
        }

        public List<Category> getAllCategory()
        {
            return findCategorys("select * from category", null);
        }

        

    }
}