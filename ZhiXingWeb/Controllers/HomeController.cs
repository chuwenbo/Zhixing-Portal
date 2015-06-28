using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZhiXingWeb.Models;

namespace ZhiXingWeb.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Index/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Design()
        {
            ViewBag.CID = Request.Params["id"] ?? string.Empty;

            return View();
        }  

        public ActionResult Service()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}
