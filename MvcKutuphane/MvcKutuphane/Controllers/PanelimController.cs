using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;
using System.Web.Security;
namespace MvcKutuphane.Controllers
{
    [Authorize]  // Artık panelimde de bir authorize ait çalışacak Herhangi bir sayfaya loginden giriş yapmadan erişilmeyecek.
    public class PanelimController : Controller
    {
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        // GET: Panelim
        [HttpGet]
          
        public ActionResult Index()
        {
            var uyemail = (string)Session["Mail"];
            //var degerler = db.TBLUYELER.FirstOrDefault(z => z.MAİL == uyemail);
            var degerler = db.TBLDUYURULAR.ToList();


            var d1 = db.TBLUYELER.Where(x => x.MAİL == uyemail).Select(y => y.AD).FirstOrDefault(); //mail adresi uyemail'E eşit olanın adını getirsin.
            ViewBag.d1 = d1;
            var d2 = db.TBLUYELER.Where(x => x.MAİL == uyemail).Select(y => y.SOYAD).FirstOrDefault(); //mail adresi uyemail'E eşit olanın adını getirsin.
            ViewBag.d2 = d2;

            var d3 = db.TBLUYELER.Where(x => x.MAİL == uyemail).Select(y => y.FOTOGRAF).FirstOrDefault(); //mail adresi uyemail'E eşit olanın adını getirsin.
            ViewBag.d3 = d3;

            var d4 = db.TBLUYELER.Where(x => x.MAİL == uyemail).Select(y => y.KULLANICIADI).FirstOrDefault(); //mail adresi uyemail'E eşit olanın adını getirsin.
            ViewBag.d4 = d4;

            var d5 = db.TBLUYELER.Where(x => x.MAİL == uyemail).Select(y => y.OKUL).FirstOrDefault(); //mail adresi uyemail'E eşit olanın adını getirsin.
            ViewBag.d5 = d5;

            var d6 = db.TBLUYELER.Where(x => x.MAİL == uyemail).Select(y => y.TELEFON).FirstOrDefault(); //mail adresi uyemail'E eşit olanın adını getirsin.
            ViewBag.d6 = d6;

            var d7 = db.TBLUYELER.Where(x => x.MAİL == uyemail).Select(y => y.MAİL).FirstOrDefault(); //mail adresi uyemail'E eşit olanın adını getirsin.
            ViewBag.d7 = d7;

            var uyeid = db.TBLUYELER.Where(x => x.MAİL == uyemail).Select(y => y.ID).FirstOrDefault(); //mail adresi uyemail'E eşit olanın adını getirsin.
            var d8 = db.TBLHAREKET.Where(x => x.UYE == uyeid).Count(); //üye toplamda kaç kitap almış onu yazdırdık.
            ViewBag.d8 = d8;

            var d9 = db.TBLMESAJLAR.Where(x => x.ALICI == uyemail).Count(); //mesajlar tablonun içerisinde sisteme authontice olan uyemail kaç defa geçiyorsa onu count olarak döndürecek.
            ViewBag.d9 = d9;

            var d10 = db.TBLDUYURULAR.Count();
            ViewBag.d10 = d10;

            return View(degerler);
        }
        [HttpPost]
        public ActionResult Index2(TBLUYELER p)
        {
            var kullanici = (string)Session["Mail"];
            var uye = db.TBLUYELER.FirstOrDefault(x => x.MAİL == kullanici);
            uye.SİFRE = p.SİFRE;
            uye.AD = p.AD;             ///Üye bilgileri güncelleme işlemleri yaptık.
            uye.SOYAD = p.SOYAD;
            uye.FOTOGRAF = p.FOTOGRAF;
            uye.OKUL = p.OKUL;
            uye.KULLANICIADI = p.KULLANICIADI;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Kitaplarim()
        {
            var kullanici = (string)Session["Mail"];
            var id = db.TBLUYELER.Where(x => x.MAİL == kullanici.ToString()).Select(z => z.ID).FirstOrDefault(); // giriş yapılan üyenin kitap geçmişini listeledik.
            var degerler = db.TBLHAREKET.Where(x => x.UYE == id).ToList(); //üyenin id durumuna göre çekecek.

            var uyeisim = (string)Session["Mail"];
            var ide = db.TBLUYELER.Where(k => k.AD == uyeisim).Select(y => y.ID).FirstOrDefault();
            return View(degerler);
        }
      
        public ActionResult Duyurular()
        {
            var duyurulistesi = db.TBLDUYURULAR.ToList();
            return View(duyurulistesi);
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("GirisYap", "Login");
        }
        public PartialViewResult PartialActivity()
        {
            return PartialView();
        }
        public PartialViewResult PartialSettings()
        {
            var kullanici = (string)Session["Mail"];
            var id = db.TBLUYELER.Where(x => x.MAİL == kullanici).Select(y => y.ID).FirstOrDefault();
            var uyegetir = db.TBLUYELER.Find(id);
            return PartialView("PartialSettings",uyegetir);
        }
    }
}