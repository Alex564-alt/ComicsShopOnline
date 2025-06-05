using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ComicsShopOnline.Web.Filters
{
    public class AdminModeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var authCookie = filterContext.HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                var ticket = FormsAuthentication.Decrypt(authCookie.Value);
                if (ticket != null && ticket.UserData == "Admin")
                {
                    // Передаем флаг в ViewBag
                    filterContext.Controller.ViewBag.IsAdmin = true;
                    return;
                }
            }
            filterContext.Controller.ViewBag.IsAdmin = false;
        }
    }
}