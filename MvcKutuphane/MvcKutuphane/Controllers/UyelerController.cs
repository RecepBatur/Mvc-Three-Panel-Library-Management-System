using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;
using PagedList;
using PagedList.Mvc;
namespace MvcKutuphane.Controllers
{
    public class UyelerController : Controller
    {
        // GET: Uyeler
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        public ActionResult Index(int sayfa = 1) //üyeleri sayfalama formatında listeledik. Sayfa değeri 1'den başlasın.
        {
            //var degerler = db.TBLUYELER.ToList();
            var degerler = db.TBLUYELER.ToList().ToPagedList(sayfa, 3); //Her sayfada 3 tane üye listelensin.
            return View(degerler);
        }
        [HttpGet] //Sayfa yüklendiğinde herhangi bir işlem yapma sadece sayfayı bana geri döndür.
        public ActionResult UyeEkle()
        {
            return View();
        }
        [HttpPost] // Bir şeye tıklanınca yani sayfaya herhangi bir gönderme işlemi olunca burası çalışacak.
        public ActionResult UyeEkle(TBLUYELER p)
        {
            if (!ModelState.IsValid)//Arkaplanda ki DataAnnotations bağlı olan geçerlilik değeri sağlamadıysa personel ekle'ye geri döndür.
            {
                return View("UyeEkle");
            }
            db.TBLUYELER.Add(p); // sağlarsa bu işlemi yapsın.
            db.SaveChanges(); //değişikleri kaydet anlamında.
            return View();
        }
        public ActionResult UyeSil(int id)
        {
            var uye = db.TBLUYELER.Find(id); //personel içindeki id bul dedik.
            db.TBLUYELER.Remove(uye); //personellerden kaldır yani çıkart, neyi? personel isimli değerimi.
            db.SaveChanges();
            return RedirectToAction("Index"); // Beni index aksiyonuna yönlendir.
        }
        public ActionResult UyeGetir(int id)
        {
            var uye = db.TBLUYELER.Find(id); // id göre değerimizi bul dedik.
            return View("UyeGetir", uye); // Bana prs'den gelen değerle PersonelGetir sayfasına döndür.
        }

        public ActionResult UyeGuncelle(TBLUYELER p)
        {
            var uye = db.TBLUYELER.Find(p.ID); // Güncelleme işleminde aynı eklemede olduğu gibi birden fazla değer vardır. O yüzden parametre atayıp yaparız.
            uye.AD = p.AD;
            uye.SOYAD = p.SOYAD;
            uye.MAİL = p.MAİL;
            uye.KULLANICIADI = p.KULLANICIADI;
            uye.SİFRE = p.SİFRE;
            uye.OKUL = p.OKUL;
            uye.TELEFON = p.TELEFON;
            uye.FOTOGRAF = p.FOTOGRAF;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UyeKitapGecmis(int id)
        {
            var ktpgecmis = db.TBLHAREKET.Where(x => x.UYE == id).ToList();
            var uyeisim = db.TBLUYELER.Where(u=>u.ID == id).Select(k=>k.AD + " " + k.SOYAD).FirstOrDefault();
            ViewBag.uye1 = uyeisim;
            return View(ktpgecmis);
        }
    }
}