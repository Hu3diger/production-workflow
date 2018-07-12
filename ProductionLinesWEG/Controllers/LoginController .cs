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
            // requisita o cookie do cliente
            HttpCookie cookie = Request.Cookies.Get("AuthId");
            if (cookie == null || cookie.Value.Equals(""))
            {
                // se estiver não logado retorna a tela que esta sendo chamada
                return View();
            }

            // senão retorna a tela de Dashboard
            return RedirectToAction("Index", "Dashboard");
        }
    }
}