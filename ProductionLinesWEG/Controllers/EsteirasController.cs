using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductionLinesWEG.Controllers
{
    public class EsteirasController : Controller
    {

        public ActionResult Index()
        {
            // requisita o cookie do cliente
            HttpCookie cookie = Request.Cookies.Get("AuthId");
            if (cookie != null)
            {
                if (!cookie.Value.Equals(""))
                {
                    // se estiver logado retorna a tela que esta sendo chamada
                    return View();
                }
            }

            // senão retorna a tela de login para se logar
            return RedirectToAction("Index", "Login");
        }
    }
}