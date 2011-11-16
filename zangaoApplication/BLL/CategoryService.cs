using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using zangaoApplication.Models;
using zangaoApplication.DAL;

namespace zangaoApplication.BLL
{
    public class CategoryService
    {
        public void save(Category category)
        {
            CategoryDao dao = new CategoryDao();
            dao.save(category);
        }
    }
}