using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZhiXing.Core.Utility; 

namespace ZhiXingWeb.Attribute
{
    public class AuthAttribute : ActionFilterAttribute   
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            HttpCookie cookie = filterContext.RequestContext.HttpContext.Request.Cookies["psw"];

            if (cookie == null || string.IsNullOrEmpty(cookie.Value) || cookie.Value != MD5Provider.GetMD5String("123456"))
            {
                filterContext.Result = new RedirectResult("/Administrator/login");
            }
        }
    }
}