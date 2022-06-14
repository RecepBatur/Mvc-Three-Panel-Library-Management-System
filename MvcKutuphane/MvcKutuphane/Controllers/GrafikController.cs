using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models;
namespace MvcKutuphane.Controllers
{
    public class GrafikController : Controller
    {
        // GET: Grafik
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult VisualizeBookResult()
        {
            return Json(liste());
        }
        public List<Class1> liste()
        {
            List<Class1> cs = new List<Class1>();
            cs.Add(new Class1()
            {
                yayinevi = "Can Yayınları",
                sayi = 7
            });
            cs.Add(new Class1()
            {
                yayinevi = "Türkiye İş Bankası Kültür Yayınları",
                sayi = 4
            });
            cs.Add(new Class1()
            {
                yayinevi = "Yapı Kredi Kültür Yayınları",
                sayi = 6
            });
            return cs;
        }
    }
}