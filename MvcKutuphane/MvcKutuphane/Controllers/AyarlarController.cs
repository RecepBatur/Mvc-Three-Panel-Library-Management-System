using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;
namespace MvcKutuphane.Controllers
{
    public class AyarlarController : Controller
    {
        // GET: Ayarlar
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        public ActionResult Index()
        {
            var kullanicilar = db.TBLADMİN.ToList();
            return View(kullanicilar);
        }
        public ActionResult IndexSettings()
        {
            var kullanicilar = db.TBLADMİN.ToList();
            return View(kullanicilar);
        }
        [HttpGet]
        public ActionResult YeniAdmin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniAdmin(TBLADMİN a)
        {
            db.TBLADMİN.Add(a);
            db.SaveChanges();

            return RedirectToAction("IndexSettings");
        }
        public ActionResult AdminDelete(int id)
        { 
            var admin = db.TBLADMİN.Find(id);
            db.TBLADMİN.Remove(admin);
            db.SaveChanges();
            return RedirectToAction("IndexSettings");
        }
        [HttpGet]
        public ActionResult AdminUpdate(int id)
        {
            var adm = db.TBLADMİN.Find(id); // id göre değerimizi bul dedik.
            return View("AdminUpdate", adm); // Bana adm'den gelen değerle AdminUpdate sayfasına döndür.
        }
        [HttpPost]
        public ActionResult AdminUpdate(TBLADMİN p)
        {
            var adm = db.TBLADMİN.Find(p.ID); // Güncelleme işleminde aynı eklemede olduğu gibi birden fazla değer vardır. O yüzden parametre atayıp yaparız.
            adm.Kullanici = p.Kullanici;
            adm.Sifre = p.Sifre;
            adm.Yetki = p.Yetki;
            db.SaveChanges();
            return RedirectToAction("IndexSettings");
        }
    }
}