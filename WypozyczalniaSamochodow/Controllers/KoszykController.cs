using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WypozyczalniaSamochodow.DAL;
using WypozyczalniaSamochodow.Infrastructure;
using WypozyczalniaSamochodow.ViewModels;

namespace WypozyczalniaSamochodow.Controllers
{
    public class KoszykController : Controller
    {
        private KoszykManager koszykManager;
        private ISessionManager sessionManager { get; set; }
        private WypozyczeniaContext db;

        public KoszykController()
        {
            db = new WypozyczeniaContext();
            sessionManager = new SessionManager();
            koszykManager = new KoszykManager(sessionManager, db);
        }

        // GET: Koszyk
        public ActionResult Index()
        {
            var pozycjaKoszyka = koszykManager.PobierzKoszyk();
            var WartoscCalkowita = koszykManager.WartoscKoszyka();
            KoszykViewModel koszykVM = new KoszykViewModel()
            {
                PozycjeKoszyka = pozycjaKoszyka,
                WartoscCalkowita = WartoscCalkowita
            };
            return View(koszykVM);
        }

        public ActionResult DodajDoKoszyka(int id)
        {
            koszykManager.DodajDoKoszyka(id);
            return RedirectToAction("Index");
        }

        public int IloscPozycjiKoszyka()
        {
            return koszykManager.IloscPozycjiKoszyka();
        }

        public ActionResult UsunZKoszyka(int autoId)
        {
            int iloscPozycji = koszykManager.UsunZKoszyka(autoId);
            int iloscPozycjiKoszyka = koszykManager.IloscPozycjiKoszyka();
            decimal WartoscCalkowita = koszykManager.WartoscKoszyka();
            var result = new KoszykUsuwanieViewModel()
            {
                IdPozycjiUsuwanej = autoId,
                KoszykCenaCalkowita = WartoscCalkowita,
                KoszykIloscPozycji = iloscPozycjiKoszyka
            };
            return RedirectToAction("Index", result);
        }
    }
}