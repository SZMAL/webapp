//SZMAL Company 
//
//Bartosz Konecki 2019
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
    public class LoginController : Controller
    {
        // GET: Logins
        public ActionResult Index()
        {
            return View("Index");
        }
        
        [HttpPost]
        public ActionResult LoginUser(UserLoginModel objUser)
        {
            using (szmaldbEntities db = new szmaldbEntities())
            {
                var obj = db.uzytkowniks.Where(a => a.imie.Equals(objUser.Login) && a.haslo.Equals(objUser.Password)).FirstOrDefault();

                if (obj == null)
                {

                    ModelState.AddModelError("Login", "Niepoprawna nazwa użytkownika lub hasło");
                    ModelState.AddModelError("Password", "");
                    ViewResult result= View("~/Views/Index.cshtml", objUser);
                    
                    return Json(false);
                }

                if (ModelState.IsValid)
                {
                   Session["UserID"] = obj.login.ToString();
                   return RedirectToAction("UserDashBoard");
                }
            
            return View(this);
             }
        }


        [HttpPost]
        public JsonResult UserExists(string Login)
        {
            
            using (szmaldbEntities db = new szmaldbEntities())
            {
               if( db.uzytkowniks.Any(u=>u.login==Login))
                    return Json(true);
               else return Json(false);
            }
                

        }


        [HttpPost]
        public async Task<ActionResult> TryLogin(UserLoginModel user)
        {

            using (szmaldbEntities db = new szmaldbEntities())
            {
                if (db.uzytkowniks.Any(u => u.login == user.Login && u.haslo == user.Password))
                    return Json(true);
                else
                {
                    ModelState.AddModelError("Login", "Niepoprawny login lub hasło");
                   
                    var view= View("~/Views/Index.cshtml", user);
                    view.ViewData.ModelState.AddModelError("Login", "Niepoprawny login lub hasło");
                    return PartialView("Index",user);
                }
            }


        }


        [HttpPost]
        public ActionResult RegisterUser(UserLoginModel objUser)
        {
            if (ModelState.IsValid)
            {
                using (szmaldbEntities db = new szmaldbEntities())
                {
                    var obj = db.uzytkowniks.Where(a => a.imie.Equals(objUser.Login) && a.haslo.Equals(objUser.Password)).FirstOrDefault();
                    if (obj == null)
                    {
                        db.uzytkowniks.Add(new uzytkownik());
                        db.SaveChanges();
                    }
                }
            }
            else return PartialView("Index", objUser);
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