using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using zangaoApplication.Models;
using zangaoApplication.DAL;
using System.Text;

namespace zangaoApplication.BLL
{
    public class CategoryService
    {
        private CategoryDao dao=null;
        public CategoryService()
        {
            this.dao = new CategoryDao();
        }

        public void save(Category category)
        {
            //thisdao = new CategoryDao();
            dao.save(category);
        }

        public List<Category> getAllCategory()
        {
            return dao.getAllCategory();
        }

        public String generateJSON(List<Category> categorys)
        {
            String JSON = "[";
            StringBuilder value=new StringBuilder();
            int count = categorys.Count;
            int i=1;
            foreach (Category category in categorys)
            {
                value.Append("{").Append("id:").Append(category.Id.ToString()).Append(",")
                    .Append("pid:").Append(category.Pid.ToString()).Append(",").Append("name:")
                    .Append("\"").Append(category.Name).Append("\"").Append("}");
                if (i < count)
                {
                    value.Append(",");
                }
                i++;
            }
            JSON = JSON + value.ToString() + "]";
            return JSON;
        }
    }
}