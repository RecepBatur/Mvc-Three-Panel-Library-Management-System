using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcKutuphane.Controllers
{
    [AllowAnonymous]
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult PageError400()
        {
            Response.StatusCode = 400;
            Response.TrySkipIisCustomErrors = true; //hata sayfasının gelmesi için kullandık.
            return View();
        }
        public ActionResult PageError403()
        {
            Response.StatusCode = 403;
            Response.TrySkipIisCustomErrors = true; //hata sayfasının gelmesi için kullandık.
            return View();
        }
        public ActionResult PageError404()
        {
            Response.StatusCode = 404;
            Response.TrySkipIisCustomErrors = true; //hata sayfasının gelmesi için kullandık.
            return View();
        }
    }
}