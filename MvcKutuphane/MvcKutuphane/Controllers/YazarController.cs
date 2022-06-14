using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;

namespace MvcKutuphane.Controllers
{
    public class YazarController : Controller
    {
        // GET: Yazar
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        public ActionResult Index()
        {
            var degerler = db.TBLYAZAR.ToList();
            return View(degerler); // Yazarları Listeledik.
        }
        [HttpGet]
        public ActionResult YazarEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YazarEkle(TBLYAZAR p)
        {
            if (!ModelState.IsValid) //buradan dönecek olan değer false ise yani işlem geçerli ise. Kontrol sağlıyoruz.
            {
                return View("YazarEkle");
            }
            db.TBLYAZAR.Add(p);
            db.SaveChanges();
            return View();
        }
        public ActionResult YazarSil(int id)
        {
            var yazar = db.TBLYAZAR.Find(id); //kategori içindeki id bul dedik.
            db.TBLYAZAR.Remove(yazar); //yazarlardan kaldır yani çıkart, neyi? yazar isimli değerimi.
            db.SaveChanges();
            return RedirectToAction("Index"); // Beni index aksiyonuna yönlendir.
        }
        public ActionResult YazarGetir(int id)
        {
            var yzr = db.TBLYAZAR.Find(id);
            return View("YazarGetir",yzr); //YazarGetir içerisinde yzr değerime göre verileri bana getir.
        }
        public ActionResult YazarGuncelle(TBLYAZAR p)
        {
            var yzr = db.TBLYAZAR.Find(p.ID);
            yzr.AD = p.AD;
            yzr.SOYAD = p.SOYAD; //Atamaları yaptık burada.
            yzr.DETAY = p.DETAY;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult YazarKitaplar(int id)
        {
            var yazarkitap = db.TBLKİTAP.Where(x=>x.YAZAR == id).ToList(); //yazarın kitaplarını listeledik. Yazarın kitapları butonuna tıklayınca.
            var yzrad = db.TBLYAZAR.Where(y=>y.ID == id).Select(z=>z.AD + " " + z.SOYAD).FirstOrDefault(); // yazarın adı ve soyadını getir dedik yazarın kitapları butonuna tıklayınca.
            ViewBag.y1 = yzrad; // view'a taşıdık yazarın adını ve soyadını.
            return View(yazarkitap);
        }
    }
}