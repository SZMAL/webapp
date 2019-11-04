using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SZMALAPP.Controllers
{
    public class UserController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View("~/Views/Home/Actual.cshtml");
        }
        
        // GET: O nas
        public ActionResult About()
        {
            return View("~/Views/Home/About.cshtml");
        }
        // GET: Przegladaj Aktualne
        public ActionResult Actual()
        {
            return View("~/Views/Home/Actual.cshtml");
        }
        // GET: Dodaj
        public ActionResult Add()
        {
            return View("~/Views/Home/Add.cshtml");
        }

        // GET: Przegladaj poczekalnia
        public ActionResult Pending()
        {
            return View("~/Views/Home/Pending.cshtml");
        }
        // GET: Statystyki serwisu
        public ActionResult ServiceStats()
        {
            return View("~/Views/Home/ServiceStats.cshtml");
        }
        // GET: Ustawienia
        public ActionResult Settings()
        {
            return View("~/Views/Home/Settings.cshtml");
        }
        // GET: Przegladaj twoje
        public ActionResult Yours()
        {
            return View("~/Views/Home/Yours.cshtml");
        }

        // GET: Statystyki twoje
        public ActionResult YoursStats()
        {
            return View("~/Views/Home/YoursStats.cshtml");
        }
    }
}