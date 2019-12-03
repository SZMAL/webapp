//SZMAL Company 
//
//Radoslaw Kister 2019
//
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SZMALAPP.Models;

namespace SZMALAPP.Controllers
{
    public class RaportController : Controller
    {
        // GET: Logins
        public ActionResult Raport()
        {
            return View("raport");
        }

        public ActionResult Details()
        {
            var tuple = new Tuple<SZMALAPP.Models.zgloszenie, SZMALAPP.Models.instytucja>(new zgloszenie(), new instytucja());
            return View(tuple);
        }


    }
}