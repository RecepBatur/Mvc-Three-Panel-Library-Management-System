using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;
using System.Web.Security;
namespace MvcKutuphane.Controllers
{
    [AllowAnonymous] // GirisYap sayfasını authorize işleminden muaf tuttuk. AllowAnonymous demesek hiçbir yere erişim sağlamamıza izin verilmez.
    public class LoginController : Controller
    {
        // GET: Login
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();

        public ActionResult GirisYap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GirisYap(TBLUYELER p)
        {
            var bilgiler = db.TBLUYELER.FirstOrDefault(x => x.MAİL == p.MAİL && x.SİFRE == p.SİFRE);
            if (bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.MAİL, false);
                Session["Mail"] = bilgiler.MAİL.ToString(); // oturum kısmında sadece maili tutacak.
                Session["Ad"] = bilgiler.AD.ToString();
                Session["Soyad"] = bilgiler.SOYAD.ToString();
                //TempData["id"] = bilgiler.ID.ToString();
                //TempData["Ad"] = bilgiler.AD.ToString();
                //TempData["Soyad"] = bilgiler.SOYAD.ToString();
                //TempData["Kullanıcı Adı"] = bilgiler.KULLANICIADI.ToString();
                //TempData["Şifre"] = bilgiler.SİFRE.ToString();
                //TempData["Okul"] = bilgiler.OKUL.ToString();

                return RedirectToAction("Index", "Panelim");
            }
            else
            {
                return View();
            }
        }
    }
}