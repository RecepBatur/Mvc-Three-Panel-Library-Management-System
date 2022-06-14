using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;
namespace MvcKutuphane.Controllers
{
    public class PersonelController : Controller
    {
        // GET: Personel
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        public ActionResult Index()
        {
            var degerler = db.TBLPERSONEL.ToList();
            return View(degerler);
        }
        [HttpGet] //Sayfa yüklendiğinde herhangi bir işlem yapma sadece sayfayı bana geri döndür.
        public ActionResult PersonelEkle()
        {
            return View();
        }
        [HttpPost] // Bir şeye tıklanınca yani sayfaya herhangi bir gönderme işlemi olunca burası çalışacak.
        public ActionResult PersonelEkle(TBLPERSONEL p)
        {
            if (!ModelState.IsValid)//Arkaplanda ki DataAnnotations bağlı olan geçerlilik değeri sağlamadıysa personel ekle'ye geri döndür.
            {
                return View("PersonelEkle");
            }
            db.TBLPERSONEL.Add(p); // sağlarsa bu işlemi yapsın.
            db.SaveChanges(); //değişikleri kaydet anlamında.
            return View();
            //return RedirectToAction("Index","Personel");
        }
        public ActionResult PersonelSil(int id)
        {
            var person = db.TBLPERSONEL.Find(id); //personel içindeki id bul dedik.
            db.TBLPERSONEL.Remove(person); //personellerden kaldır yani çıkart, neyi? personel isimli değerimi.
            db.SaveChanges();
            return RedirectToAction("Index"); // Beni index aksiyonuna yönlendir.
        }
        public ActionResult PersonelGetir(int id)
        {
            var prs = db.TBLPERSONEL.Find(id); // id göre değerimizi bul dedik.
            return View("PersonelGetir", prs); // Bana prs'den gelen değerle PersonelGetir sayfasına döndür.
        }

        public ActionResult PersonelGuncelle(TBLPERSONEL p)
        {
            var prs = db.TBLPERSONEL.Find(p.ID); // Güncelleme işleminde aynı eklemede olduğu gibi birden fazla değer vardır. O yüzden parametre atayıp yaparız.
            prs.PERSONEL = p.PERSONEL;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}