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
            szmalDBEvents db = new szmalDBEvents();
            UserLoginModel userLoginModel = new UserLoginModel();
            var bigmodel = new BigModel() { EventToShowModel = db.zgloszenies.Select(s => s), UserLoginModel = null };
            
            return View("Index",bigmodel);
        }
        
        [HttpPost]
        public ActionResult LoginUser(BigModel objUser)
        {
            using (szmaldbEntities db = new szmaldbEntities())
            {
                var obj = db.uzytkowniks.Where(a => a.login.Equals(objUser.UserLoginModel.Login) && a.haslo.Equals(objUser.UserLoginModel.Password)).FirstOrDefault();

                if (obj == null)
                {

                    ModelState.AddModelError("Login", "Niepoprawna nazwa użytkownika lub hasło");
                    ModelState.AddModelError("Password", "");
                    ViewResult result= View("~/Views/Index.cshtml", objUser);

                    return result;
                }

               else if (ModelState.IsValid)
                {
                    Session["UserID"] = obj;
                   return View("~/Views/Home/Yours.cshtml");
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
                    return Json(false);
               else return Json(true);
            }
        }


        [HttpPost]
        public async Task<ActionResult> TryLogin(BigModel user)
        {
            {
                using (szmaldbEntities db = new szmaldbEntities())
                {
                    if (db.uzytkowniks.Any(u => u.login == user.UserLoginModel.Login && u.haslo == user.UserLoginModel.Password))
                    {
                        Session["UserID"] = user.UserLoginModel.Login.ToString();
                        szmalDBEvents ev = new szmalDBEvents();
                        var lista = ev.zgloszenies.Select(s => s).ToList();
                        ev.Dispose();
                        return View("~/Views/Home/Yours.cshtml", lista);
                        //return View("~/Views/Home/Yours.cshtml", db.uzytkowniks.FirstOrDefault(u => u.login == user.UserLoginModel.Login));
                    }
                    else
                    {
                        ModelState.AddModelError("Login", "Niepoprawny login lub hasło");

                        var view = View("~/Views/Index.cshtml", user);
                        view.ViewData.ModelState.AddModelError("Login", "Niepoprawny login lub hasło");
                        return PartialView("Index", user);
                    }
                }

            }
        }


        [HttpPost]
        public ActionResult RegisterUser(BigModel objUser)
        {
            if (ModelState.IsValid)
            {
                using (szmaldbEntities db = new szmaldbEntities())
                {
                    var obj = db.uzytkowniks.Where(a => a.imie.Equals(objUser.UserLoginModel.Login) && a.haslo.Equals(objUser.UserLoginModel.Password)).FirstOrDefault();
                    if (obj == null)
                    {
                        uzytkownik u = new uzytkownik();
                        u.login = objUser.UserLoginModel.Login;
                        u.haslo = objUser.UserLoginModel.Password;
                        u.email = objUser.UserLoginModel.Email;
                        db.uzytkowniks.Add(u);
                        db.SaveChanges();
                    }
                }
            }
            else return PartialView("Index", objUser);
            return View("~/Views/Index.cshtml");
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