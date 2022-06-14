using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;
namespace MvcKutuphane.Controllers
{
    public class DuyurularController : Controller
    {
        // GET: Duyurular
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        public ActionResult Index()
        {
            var degerler = db.TBLDUYURULAR.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult DuyuruEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DuyuruEkle(TBLDUYURULAR t)
        {
            db.TBLDUYURULAR.Add(t);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DuyuruSil(int id)
        {
            var duyuru = db.TBLDUYURULAR.Find(id);
            db.TBLDUYURULAR.Remove(duyuru);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DuyuruDetay(TBLDUYURULAR p)
        {
            var duyuru = db.TBLDUYURULAR.Find(p.ID);
            return View("DuyuruDetay",duyuru);
        }
        public ActionResult DuyuruGuncelle(TBLDUYURULAR g)
        {
            var duyurular = db.TBLDUYURULAR.Find(g.ID);
            duyurular.KATEGORİ = g.KATEGORİ;
            duyurular.İCERİK = g.İCERİK;
            duyurular.TARİH = g.TARİH;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}