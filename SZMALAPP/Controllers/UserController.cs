using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SZMALAPP.Models;
using IronPdf;


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

        public ActionResult Yours(bool success)
        {
            return View();
        }
        // GET: Przegladaj Aktualne
        public ActionResult Actual(uzytkownik u)
        {
            return View("~/Views/Home/Actual.cshtml", u);
        }
        // GET: Dodaj
        public ActionResult Add()
        {
            return View("~/Views/Home/Add.cshtml");
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

        public  string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }

         void CreatePdf()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Views", "Raport", "raport.cshtml");
            string html = "";
            using (szmalDBEvents db = new szmalDBEvents())
            {
                var obj = db.zgloszenies.First(a => a.id_zgloszenia.Equals(1));
                if(obj!=null)
                {
                    html = RenderRazorViewToString(path, obj);
                }
                else
                {

                }

            }
          

            //var html = System.IO.File.ReadAllText(path);

            var htmlToPdf = new HtmlToPdf();
            var pdf = htmlToPdf.RenderHtmlAsPdf(html);

            var OutputPath =Environment.GetFolderPath( Environment.SpecialFolder.LocalApplicationData)+"/raport.pdf";
            pdf.SaveAs(OutputPath);
           
            System.Diagnostics.Process.Start(OutputPath);

        }


        //POST: Add
        [HttpPost]
        public ActionResult AddEvent(zgloszenie ev)
        {
            chuj chuj 
            using (szmalDBEvents events = new szmalDBEvents())
            {
                try
                {
                    CreatePdf();
                    ev.fk_login = ((uzytkownik)Session["UserID"]).login;
                    
                    events.zgloszenies.Add(ev);
                    events.SaveChanges();
                }
                catch (Exception e)
                {
                    return View("~/Views/Home/Error.cshtml", e.Message);
                }
            }

            return View("~/Views/Home/Yours.cshtml", true);
        }
    }
}