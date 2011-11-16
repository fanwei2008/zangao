using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using zangaoApplication.BLL;
using zangaoApplication.Models;

namespace zangaoApplication.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult Login()
        {
            return View("Login");
        }

        public ActionResult Verify()
        {
            String name=this.HttpContext.Request.Form["name"];
            String pwd = this.HttpContext.Request.Form["pwd"];
            AdminService service = new AdminService();
            Administrator administrator = service.Verify(name, pwd);
            if (administrator!= null)
            { 
                if(administrator.Status==State.Normal)
                {
                    Session["name"] = name;
                    Session["id"] = administrator.Id.ToString();
                    Session["super"] = administrator.Super.ToString();
                    Session["level"] = administrator.Level.ToString();
                    return View("Manage");
                }
                else
                {
                    ViewData["message"] = "此管理员账号非正常状态，无法登陆请联系超级管理员";
                    return View("Login");
                }
                
            }
            ViewData["message"] = "用户名密码错误，请重新输入";
            return View("Login");
        }

    }
}
