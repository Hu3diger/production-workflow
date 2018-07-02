using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductionLinesWEG.Controllers
{
    public class LoginController : Controller
    {

        public ActionResult Index()
        {
            HttpCookie cookie = Request.Cookies.Get("AuthId");
            if (cookie == null || cookie.Value.Equals(""))
            {
                return View();
            }
            return RedirectToAction("Index", "Dashboard");
        }
    }
}