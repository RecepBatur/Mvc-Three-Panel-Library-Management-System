using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;
namespace MvcKutuphane.Controllers
{
    public class OduncController : Controller
    {
        // GET: Odunc
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        [Authorize(Roles = "A")] //Sadece rolü A olan kişiler bu listeyi yani ödünç sayfasını görebilir. Admin için yaptık bunu.
        public ActionResult Index()
        {
            var degerler = db.TBLHAREKET.Where(x => x.ISLEMDURUM == false).ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult OduncVer()
        {
            List<SelectListItem> deger1 = (from x in db.TBLUYELER.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.AD + " " + x.SOYAD,
                                               Value = x.ID.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;

            List<SelectListItem> deger2 = (from k in db.TBLKİTAP.Where(x => x.DURUM == true).ToList()
                                           select new SelectListItem
                                           {
                                               Text = k.AD,
                                               Value = k.ID.ToString()
                                           }).ToList();
            ViewBag.dgr2 = deger2;

            List<SelectListItem> deger3 = (from y in db.TBLPERSONEL.ToList()
                                           select new SelectListItem
                                           {
                                               Text = y.PERSONEL,
                                               Value = y.ID.ToString()
                                           }).ToList();
            ViewBag.dgr3 = deger3;
            return View();
        }
        [HttpPost] // Bir şeye tıklanınca yani sayfaya herhangi bir gönderme işlemi olunca burası çalışacak.
        public ActionResult OduncVer(TBLHAREKET p)
        {
            var d1 = db.TBLUYELER.Where(x => x.ID == p.TBLUYELER.ID).FirstOrDefault();
            var d2 = db.TBLKİTAP.Where(y => y.ID == p.TBLKİTAP.ID).FirstOrDefault(); //dropdown'dan gelen değerlerle eşleştir daha sonra veritabanına dahil et.
            var d3 = db.TBLPERSONEL.Where(z => z.ID == p.TBLPERSONEL.ID).FirstOrDefault();
            p.TBLUYELER = d1;
            p.TBLKİTAP = d2; //navigation properties'e atamaları yaptık. bunlar onun karşılığı. 
            p.TBLPERSONEL = d3;
            db.TBLHAREKET.Add(p);
            db.SaveChanges(); //değişikleri kaydet anlamında.
            return RedirectToAction("Index");
        }
        public ActionResult Oduncİade(TBLHAREKET p)
        {
            var odn = db.TBLHAREKET.Find(p.ID); // id göre değerimizi bul dedik.
            DateTime d1 = DateTime.Parse(odn.İADETARİH.ToString());
            DateTime d2 = Convert.ToDateTime(DateTime.Now.ToShortDateString()); //bugünün tarihini string olarak alsın. 
            TimeSpan d3 = d2 - d1; //Timespan iki tarih arasındaki farkı alan komut.
            ViewBag.dgr = d3.TotalDays; //TotalDays yani toplam gün formatında bana d3 yazdır dedik.
            return View("Oduncİade", odn);
        }
        public ActionResult OduncGuncelle(TBLHAREKET p)
        {
            var hrk = db.TBLHAREKET.Find(p.ID); // Güncelleme işleminde aynı eklemede olduğu gibi birden fazla değer vardır. O yüzden parametre atayıp yaparız.
            hrk.UYEGETİRTARİH = p.UYEGETİRTARİH;
            hrk.ISLEMDURUM = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}