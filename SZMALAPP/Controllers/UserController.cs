using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SZMALAPP.Models;

namespace SZMALAPP.Controllers
{
    public class UserController : Controller
    {
        // GET: Home
        public ActionResult Index(uzytkownik u)
        {
            return View("~/Views/Home/Actual.cshtml", u);
        }
        
        // GET: O nas
        public ActionResult About(uzytkownik u)
        {
            return View("~/Views/Home/About.cshtml", u);
        }
        // GET: Przegladaj Aktualne
        public ActionResult Actual(uzytkownik u)
        {
            return View("~/Views/Home/Actual.cshtml", u);
        }
        // GET: Dodaj
        public ActionResult Add(uzytkownik u)
        {
            return View("~/Views/Home/Add.cshtml", u);
        }

        // GET: Przegladaj poczekalnia
        public ActionResult Pending(uzytkownik u)
        {
            return View("~/Views/Home/Pending.cshtml", u);
        }
        // GET: Statystyki serwisu
        public ActionResult ServiceStats(uzytkownik u)
        {
            return View("~/Views/Home/ServiceStats.cshtml", u);
        }
        // GET: Ustawienia
        public ActionResult Settings(uzytkownik u)
        {
            return View("~/Views/Home/Settings.cshtml", u);
        }
        // GET: Przegladaj twoje

        public ActionResult Yours(uzytkownik u)
        {
            return View("~/Views/Home/Yours.cshtml",u);
        }


        // GET: Statystyki twoje
        public ActionResult YoursStats(uzytkownik u)
        {
            return View("~/Views/Home/YoursStats.cshtml", u);
        }
    }
}