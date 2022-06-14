using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;
namespace MvcKutuphane.Controllers
{
    public class KitapController : Controller
    {
        // GET: Kitap
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        public ActionResult Index(string c)
        {
            //kitap listeleme.
            var kitaplar = from k in db.TBLKİTAP select k; //kitaplar adlı değişkeni tblkitap tablosundan alacaksın.
            if (!string.IsNullOrEmpty(c)) // parametreden gelen string değer null ya da boş değilse
            {
                kitaplar = kitaplar.Where(m => m.AD.Contains(c));
            }
            //var kitaplar = db.TBLKİTAP.ToList();
            return View(kitaplar.ToList());
        }
        [HttpGet]
        public ActionResult KitapEkle()
        {
            //Linq sorgusu yazdık.Ve kategorileri liste şeklinde listeledik.
            List<SelectListItem> ktp = (from i in db.TBL_KATEGORİ.ToList() //listeden bir değer isminde öğe seçiyoruz. 
                                           select new SelectListItem //yeni bir liste öğesi seç dedik.
                                           {
                                               Text = i.AD, //seçilmiş olan değeri yani adı text'e atadık. ön tarafta çalışacak değer.
                                               Value = i.ID.ToString() //arkplanda çalışacak olan değer. 
                                           }).ToList(); // bu değeri bana liste halinde getir.
            ViewBag.ktp1 = ktp; // ktp1 olarak taşı.

            //Yazarları liste şeklinde listeledik.
            List<SelectListItem> yzr = (from y in db.TBLYAZAR.ToList()
                                        select new SelectListItem
                                        {
                                            Text = y.AD + ' ' + y.SOYAD,
                                            Value = y.ID.ToString()

                                        }).ToList();
            ViewBag.yzr1 = yzr;
            
            return View();
        }
        [HttpPost]
        public ActionResult KitapEkle(TBLKİTAP p)
        {
            var ktg = db.TBL_KATEGORİ.Where(k => k.ID == p.TBL_KATEGORİ.ID).FirstOrDefault(); // parametreden gelen tblkategori içerisindeki ID değerimi ilk veya varsayılmış olan değerini ktg adlı değişkene ata.
            var yzr = db.TBLYAZAR.Where(y => y.ID == p.TBLYAZAR.ID).FirstOrDefault(); // parametreden gelen tblyazar içerisindeki ID değerimi ilk veya varsayılmış olan değerini yzr adlı değişkene ata.
            p.TBL_KATEGORİ = ktg; //kategoriden gelen değeri ktg'ye ata.
            p.TBLYAZAR = yzr; // yazardan gelen değeri yzr'ye ata.
            db.TBLKİTAP.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
           
        }
        public ActionResult KitapSil(int id)
        {
            var book = db.TBLKİTAP.Find(id);
            db.TBLKİTAP.Remove(book); 
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KitapGetir(int id)
        {
            var kitap = db.TBLKİTAP.Find(id);
            List<SelectListItem> ktp = (from i in db.TBL_KATEGORİ.ToList() //listeden bir değer isminde öğe seçiyoruz. 
                                        select new SelectListItem //yeni bir liste öğesi seç dedik.
                                        {
                                            Text = i.AD, //seçilmiş olan değeri yani adı text'e atadık. ön tarafta çalışacak değer.
                                            Value = i.ID.ToString() //arkplanda çalışacak olan değer. 
                                        }).ToList(); // bu değeri bana liste halinde getir.
            ViewBag.ktp1 = ktp; // ktp1 olarak taşı.
            List<SelectListItem> yzr = (from y in db.TBLYAZAR.ToList()
                                        select new SelectListItem
                                        {
                                            Text = y.AD + ' ' + y.SOYAD,
                                            Value = y.ID.ToString()

                                        }).ToList();
            ViewBag.yzr1 = yzr;
            return View("KitapGetir", kitap); //YazarGetir içerisinde yzr değerime göre verileri bana getir.
        }
        public ActionResult KitapGuncelle(TBLKİTAP p)
        {
            var BookUpdate = db.TBLKİTAP.Find(p.ID);
            BookUpdate.AD = p.AD;
            BookUpdate.BASIMYIL = p.BASIMYIL; //Atamaları yaptık burada.
            BookUpdate.YAYINEVİ = p.YAYINEVİ;
            BookUpdate.SAYFA = p.SAYFA;
            BookUpdate.DURUM = true;
            var ktg = db.TBL_KATEGORİ.Where(k => k.ID == p.TBL_KATEGORİ.ID).FirstOrDefault();
            var yzr = db.TBLYAZAR.Where(y => y.ID == p.TBLYAZAR.ID).FirstOrDefault();
            BookUpdate.KATEGORİ = ktg.ID;
            BookUpdate.YAZAR = yzr.ID;
            BookUpdate.KİTAPRESİM = p.KİTAPRESİM;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}