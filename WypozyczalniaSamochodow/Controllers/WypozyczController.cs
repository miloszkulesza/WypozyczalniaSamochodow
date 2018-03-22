using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WypozyczalniaSamochodow.DAL;
using WypozyczalniaSamochodow.Infrastructure;
using WypozyczalniaSamochodow.Models;

namespace WypozyczalniaSamochodow.Controllers
{
    public class WypozyczController : Controller
    {
        private WypozyczeniaContext db;
        private ISessionManager sessionManager { get; set; }
        private KoszykManager koszykManager;
        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public WypozyczController()
        {
            db = new WypozyczeniaContext();
            sessionManager = new SessionManager();
            koszykManager = new KoszykManager(sessionManager, db);
        }

        // GET: Wypozycz
        public ActionResult WyswietlKategorie()
        {
            var kategorie = db.Kategorie.ToList();
            return View(kategorie);
        }

        public ActionResult WyswietlAuta(string kategoria)
        {
            var auta = db.Auta.Where(a => (a.Kategoria.NazwaKategorii == kategoria) && !a.Wypozyczony).ToList();
            return View(auta);
        }

        public ActionResult KartaPojazdu(int autoid)
        {
            var auto = db.Auta.Where(a => a.AutoId == autoid).Single();
            return View(auto);
        }

        public ActionResult Wypozyczenia()
        {
            bool isAdmin = User.IsInRole("Admin");
            ViewBag.UserIsAdmin = isAdmin;
            IEnumerable<Wypozyczenie> wypozyczenia;
            if(isAdmin)
            {
                wypozyczenia = db.Wypozyczenia.Include("PozycjeWypozyczenia").OrderByDescending(o => o.DataZlozenia).ToArray();
            }
            else
            {
                var userEmail = User.Identity.GetUserName();
                wypozyczenia = db.Wypozyczenia.Where(o => o.Email == userEmail).Include("PozycjeWypozyczenia").OrderByDescending(o => o.DataZlozenia).ToArray();
            }
            return View(wypozyczenia);
        }

        public async Task<ActionResult> Wypozycz()
        {
            if (Request.IsAuthenticated)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                var wypozyczenie = new Wypozyczenie()
                {
                    Imie = user.DaneKlienta.Imie,
                    Nazwisko = user.DaneKlienta.Nazwisko,
                    Adres = user.DaneKlienta.Adres,
                    Miasto = user.DaneKlienta.Miasto,
                    Telefon = user.DaneKlienta.Telefon,
                    Email = user.Email,
                };
                return View(wypozyczenie);
            }
            else
            {
                return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("Wypozycz", "Wypozycz") });
            }
        }

        [HttpPost]
        public async Task<ActionResult> Wypozycz(Wypozyczenie szczegoly)
        {
            if(ModelState.IsValid)
            {
                var userEmail = User.Identity.GetUserName();
                var noweWypozyczenie = koszykManager.UtworzWypozyczenie(szczegoly, userEmail);
                var user = await UserManager.FindByEmailAsync(userEmail);
                TryUpdateModel(user.DaneKlienta);
                await UserManager.UpdateAsync(user);
                koszykManager.PustyKoszyk();
                return RedirectToAction("PotwierdzenieWypozyczenia");
            }
            else
            {
                return View(szczegoly);
            }
        }

        public ActionResult PotwierdzenieWypozyczenia()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ZwrotSamochodu(int autoid)
        {
            var autoDoZwrotu = db.Auta.Find(autoid);
            autoDoZwrotu.Wypozyczony = false;
            db.SaveChanges();
            return RedirectToAction("Wypozyczenia");
        }
    }
}