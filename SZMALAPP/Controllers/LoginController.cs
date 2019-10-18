using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SZMALAPP.Models;

namespace SZMALAPP.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View("~/Views/Index.cshtml");
        }

        [HttpPost]
        public ActionResult LoginUser(uzytkownik objUser)
        {
            if (ModelState.IsValid)
            {
                using (szmaldbEntities db = new szmaldbEntities())
                {
                    var obj = db.uzytkowniks.Where(a => a.imie.Equals(objUser.login) && a.haslo.Equals(objUser.haslo)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["UserID"] = obj.login.ToString();
                        return RedirectToAction("UserDashBoard");
                    }
                }
            }
            return View(objUser);
        }


        [HttpPost]
        public ActionResult RegisterUser(uzytkownik objUser)
        {
            if (ModelState.IsValid)
            {
                using (szmaldbEntities db = new szmaldbEntities())
                {
                    var obj = db.uzytkowniks.Where(a => a.imie.Equals(objUser.login) && a.haslo.Equals(objUser.haslo)).FirstOrDefault();
                    if (obj == null)
                    {
                        db.uzytkowniks.Add(objUser);
                        db.SaveChanges();
                    }
                }
            }
            return View(objUser);
        }


        public ActionResult UserDashBoard()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
    }
}