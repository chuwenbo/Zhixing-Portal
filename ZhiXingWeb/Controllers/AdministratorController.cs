using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZhiXing.Core.Utility;
using ZhiXingWeb.Attribute;

namespace ZhiXingWeb.Controllers
{
    public class AdministratorController : Controller
    {
        //
        // GET: /Administrator/
        [AuthAttribute]
        public ActionResult Index()
        {
            return View();
        }

        [AuthAttribute]
        public ActionResult Category()
        {
            return View();
        }

        [AuthAttribute]
        public ActionResult Image()
        {
            return View();
        }

        public ActionResult Login()
        {
            HttpCookie cookie = HttpContext.Request.Cookies["psw"];

            if (cookie != null && !string.IsNullOrEmpty(cookie.Value) && cookie.Value == MD5Provider.GetMD5String("123456"))
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public JsonResult doLogin(string name, string password)
        {
            bool successed = true;

            if (!name.Equals("zhixing", StringComparison.OrdinalIgnoreCase))
            {
                successed = false;
            }

            if (!password.Equals("123456"))
            {
                successed = false;
            }

            if(successed)
            {
                // set cookie
                HttpCookie cookie = new HttpCookie("psw");
                cookie.Value = MD5Provider.GetMD5String("123456");
                cookie.Expires = DateTime.Now.AddDays(30);

                HttpContext.Response.Cookies.Add(cookie);
            } 

            return Json(successed);
        }
    }
}
