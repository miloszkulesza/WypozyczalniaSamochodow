using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WypozyczalniaSamochodow.DAL;
using WypozyczalniaSamochodow.Infrastructure;
using WypozyczalniaSamochodow.Models;
using WypozyczalniaSamochodow.ViewModels;

namespace WypozyczalniaSamochodow.Controllers
{
    public class HomeController : Controller
    {
        private WypozyczeniaContext db = new WypozyczeniaContext();
        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult Nowosci()
        {
            ICacheProvider cache = new DefaultCacheProvider();
            List<Auto> nowosci;
            if(cache.IsSet(Consts.NowosciCacheKey))
            {
                nowosci = cache.Get(Consts.NowosciCacheKey) as List<Auto>;
            }
            else
            {
                nowosci = db.Auta.Where(a => !a.Wypozyczony).OrderByDescending(a => a.DataDodania).Take(3).ToList();
                cache.Set(Consts.NowosciCacheKey, nowosci, 1);
            }
            var vm = new HomeViewModel()
            {
                Nowosci = nowosci
            };
            return PartialView("_NowosciPartial", vm);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Offer()
        {
            return View();
        }
    }
}