using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WypozyczalniaSamochodow.DAL;
using WypozyczalniaSamochodow.Models;
using WypozyczalniaSamochodow.ViewModels;

namespace WypozyczalniaSamochodow.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private WypozyczeniaContext db = new WypozyczeniaContext();

        public ManageController()
        {
        }

        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

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

        // GET: Manage
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            var name = User.Identity.Name;

            if (TempData["ViewData"] != null)
            {
                ViewData = (ViewDataDictionary)TempData["ViewData"];
            }

            if (User.IsInRole("Admin"))
                ViewBag.UserIsAdmin = true;
            else
                ViewBag.UserIsAdmin = false;

            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return View("Error");
            }

            var model = new ManageCredentialsViewModel
            {
                Message = message,
                DaneKlienta = user.DaneKlienta
            };

            return View(model);
        }

        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangeProfile([Bind(Prefix = "DaneKlienta")]DaneKlienta daneKlienta)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                user.DaneKlienta = daneKlienta;
                var result = await UserManager.UpdateAsync(user);

                AddErrors(result);
            }

            if (!ModelState.IsValid)
            {
                TempData["ViewData"] = ViewData;
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult DodajAuto(int? autoId, bool? potwierdzenie)
        {
            Auto auto;
            if(autoId.HasValue)
            {
                ViewBag.EditMode = true;
                auto = db.Auta.Find(autoId);
            }
            else
            {
                ViewBag.EditMode = false;
                auto = new Auto();
            }
            var result = new EdytujAutoViewModel();
            result.Kategorie = db.Kategorie.ToList();
            result.Auto = auto;
            result.Potwierdzenie = potwierdzenie;
            return View(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult DodajAuto(EdytujAutoViewModel model)
        {
            if (model.Auto.AutoId > 0)
            {
                db.Entry(model.Auto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DodajAuto", new { potwierdzenie = true });
            }
            else
            {
                if (ModelState.IsValid)
                {
                    model.Auto.DataDodania = DateTime.Now;
                    model.Auto.Wypozyczony = false;
                    db.Entry(model.Auto).State = EntityState.Added;
                    db.SaveChanges();
                    return RedirectToAction("DodajAuto", new { potwierdzenie = true });
                }
                else
                {
                    var kategorie = db.Kategorie.ToList();
                    model.Kategorie = kategorie;
                    return View(model);
                }
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult UsunAuto(int autoId)
        {
            var auto = db.Auta.Find(autoId);
            db.Auta.Remove(auto);
            db.SaveChanges();
            return RedirectToAction("DodajAuto", new { potwierdzenie = true });
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            Error
        }
    }
}