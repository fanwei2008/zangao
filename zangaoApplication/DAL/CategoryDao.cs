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
            if(category.Pid>0)
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
                columns.Append("category_status,");
                values.Append(Convert.ToInt32(category.Status).ToString()).Append(",");
            }
            if (category.AddUser > 0)
            {
                columns.Append("add_user");
                values.Append(category.AddUser.ToString()).Append(",");
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
    }
}