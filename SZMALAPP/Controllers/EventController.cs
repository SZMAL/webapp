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
    public class EventController : Controller
    {
        // GET: Logins
        public ActionResult Index()
        {
            return View("Index");
        }
        
        [HttpPost]
        public ActionResult EventShow(BigModel bObj)
        {
            using (szmalDBEvents db = new szmalDBEvents())
            {
                var obj = db.zgloszenies;

                if (obj == null)
                {

                    
                    ViewResult result= View("~/Views/Index.cshtml", bObj);
                    return result;
                }

               else if (ModelState.IsValid)
                {
                   return View("~/Views/Home/Yours.cshtml", bObj);
                }
            
            return View(this);
             }
        }


    }
}