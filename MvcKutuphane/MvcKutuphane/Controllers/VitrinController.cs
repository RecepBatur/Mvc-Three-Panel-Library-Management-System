using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;
using MvcKutuphane.Models.Sınıflarım;
namespace MvcKutuphane.Controllers
{
    [AllowAnonymous]
    public class VitrinController : Controller
    {
        // GET: Vitrin
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        [HttpGet]
        public ActionResult Index()
        {
            Class1 cs = new Class1();
            cs.Deger1 = db.TBLKİTAP.ToList(); // TBLKİTAP'ların listesini deger1 propu'nun üzerine atadık. 
            cs.Deger2 = db.TBLHAKKİMİZDA.ToList();
            //var degerler = db.TBLKİTAP.ToList();
            return View(cs);
        }
        [HttpPost]
        public ActionResult Index(TBLİLETİSİM t)
        {
            db.TBLİLETİSİM.Add(t);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}