using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WypozyczalniaSamochodow.DAL;
using WypozyczalniaSamochodow.Infrastructure;
using WypozyczalniaSamochodow.Models;

namespace WypozyczalniaSamochodow.Controllers
{
    public class AutaController : Controller
    {
        private WypozyczeniaContext db = new WypozyczeniaContext();
        
        public ActionResult Kategorie()
        {
            ICacheProvider cache = new DefaultCacheProvider();
            List<Kategoria> kategorie;
            if(cache.IsSet(Consts.KategorieCacheKey))
            {
                kategorie = cache.Get(Consts.KategorieCacheKey) as List<Kategoria>;
            }
            else
            {
                kategorie = db.Kategorie.ToList();
                cache.Set(Consts.KategorieCacheKey, kategorie, 60);
            }
            return View(kategorie);
        }
        public ActionResult KatalogAuta(string kategoria)
        {
            var auta = db.Auta.Where(a => a.Kategoria.NazwaKategorii == kategoria).ToList();
            return View(auta);
        }
        public ActionResult SzczegolyAuta(int autoid)
        {
            var auto = db.Auta.Where(a => a.AutoId == autoid).Single();
            return View(auto);
        }
    }
}