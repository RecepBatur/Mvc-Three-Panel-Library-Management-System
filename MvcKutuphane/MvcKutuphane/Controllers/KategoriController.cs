using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;
namespace MvcKutuphane.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        public ActionResult Index()
        {
            var degerler = db.TBL_KATEGORİ.Where(X => X.DURUM == true).ToList(); //Kategorileri listeledik. Durumu true olanları listeledik.
            return View(degerler);
        }
        [HttpGet] //Sayfa yüklendiğinde herhangi bir işlem yapma sadece sayfayı bana geri döndür.
        public ActionResult KategoriEkle()
        {
            return View();
        }
        [HttpPost] // Bir şeye tıklanınca yani sayfaya herhangi bir gönderme işlemi olunca burası çalışacak.
        public ActionResult KategoriEkle(TBL_KATEGORİ p)
        {
            p.DURUM = true;
            db.TBL_KATEGORİ.Add(p);
            db.SaveChanges(); //değişikleri kaydet anlamında.
            return RedirectToAction("Index");
        }
        public ActionResult KategoriSil(int id)
        {
            var kategori = db.TBL_KATEGORİ.Find(id); //kategori içindeki id bul dedik.
            /*db.TBL_KATEGORİ.Remove(kategori);*/ //kategorilerden kaldır yani çıkart, neyi? kategori isimli değerimi.
            kategori.DURUM = false;
            db.SaveChanges();
            return RedirectToAction("Index"); // Beni index aksiyonuna yönlendir.
        }
        public ActionResult KategoriGetir(int id)
        {
            var ktg = db.TBL_KATEGORİ.Find(id); // id göre değerimizi bul dedik.
            return View("KategoriGetir", ktg); // Bana ktg'den gelen değerle KategoriGetir sayfasına döndür.
        }

        public ActionResult KategoriGuncelle(TBL_KATEGORİ p)
        {
            var ktg = db.TBL_KATEGORİ.Find(p.ID); // Güncelleme işleminde aynı eklemede olduğu gibi birden fazla değer vardır. O yüzden parametre atayıp yaparız.
            ktg.AD = p.AD;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}