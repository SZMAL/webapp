using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SZMALAPP.Models;
using System.Net.Mail;
using IronPdf;
using System.Net;
using System.Diagnostics;
using System.Globalization;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace SZMALAPP.Controllers
{
    public class UserController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            szmalDBEvents ev = new szmalDBEvents();
            var lista = ev.zgloszenies.Select(s => s).ToList();
            ev.Dispose();
            return View("~/Views/Home/Actual.cshtml", lista);
        }
        
        // GET: O nas
        public ActionResult About(uzytkownik u)
        {
            return View("~/Views/Home/About.cshtml", u);
        }

        // GET: Przegladaj Aktualne
        public ActionResult Actual()
        {
            szmalDBEvents ev = new szmalDBEvents();
            var lista = ev.zgloszenies.Select(s => s).ToList();
            ev.Dispose();
            return View("~/Views/Home/Actual.cshtml", lista);
            
        }
        // GET: Dodaj
        public ActionResult Add()
        {
            return View("~/Views/Home/Add.cshtml");
        }

        // GET: Przegladaj poczekalnia
        public ActionResult Pending()
        {
            szmalDBEvents ev = new szmalDBEvents();
            var lista = ev.zgloszenies.Select(s => s).ToList();
            ev.Dispose();
            return View("~/Views/Home/Pending.cshtml", lista);
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

        public ActionResult Yours()
        {
            szmalDBEvents ev = new szmalDBEvents();
            var lista = ev.zgloszenies.Select(s => s).ToList();
            ev.Dispose();
            return View("~/Views/Home/Yours.cshtml", lista);
        }

        public ActionResult Raport()
        {
            return View("~/Views/Raport/raport.cshtml");
        }
        // GET: Statystyki twoje
        public ActionResult YoursStats(uzytkownik u)
        {
            return View("~/Views/Home/YoursStats.cshtml", u);
        }
        public static async Task Email(string html, string email)
        {
            

            email = email.Replace(" ", string.Empty);
            //StreamReader sr = new StreamReader("D:\\home\\klucz.txt");
            //var apiKey = sr.ReadLine();
            var apiKey = System.Environment.GetEnvironmentVariable("SENDGRID_APIKEY");
            
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("szmal.wcy@gmail.com", "SZMAL");
            var subject = "Raport o zdarzeniu";
            var to = new EmailAddress(email, "Organizacja");
            var plainTextContent = html;
            var htmlContent = html;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
            //sr.Dispose();
            /*
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("szmal.wcy@gmail.com", "SZMAL"),
                Subject = "Raport o zdarzeniu",
                PlainTextContent = html,
                HtmlContent = html
            };
                
            msg.AddTo(new EmailAddress(email, "Organizacja"));
            var response = await client.SendEmailAsync(msg);
            */


            
            
        }
        public string RenderRazorViewToString(string viewName, object model)
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
        /*
         0 - czeka do zatwierdzenia
         1 - zatwierdzone
         
         */
         public double degreesToRadians(double degrees)
        {
            return degrees * (Math.PI / 180d);
        }
        public double getDistance(double dlugoscZgloszenie , double szerokoscZgloszenie , double dlugoscFirma, double szerokoscFirma)
        {
            double distance;
            double R = 6371d;
            double dSzerokosc = degreesToRadians(szerokoscFirma - szerokoscZgloszenie );
            double dDlugosc  = degreesToRadians( dlugoscFirma - dlugoscZgloszenie);
            dSzerokosc = Math.Abs(dSzerokosc);
            dDlugosc = Math.Abs(dDlugosc);
            double a = Math.Sin(dSzerokosc / 2d) * Math.Sin(dSzerokosc / 2d) + Math.Cos(degreesToRadians(szerokoscZgloszenie)) *
                Math.Cos(degreesToRadians(szerokoscFirma)) * Math.Sin(dDlugosc / 2d) * Math.Sin(dDlugosc / 2d);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1d - a));
            distance = R * c;
            
            return distance;
        }
         void CreatePdf(zgloszenie ev)
        {
            string path = "~/Views/Raport/raport.cshtml";
            string html = "";
            string typ_zgloszenia;
            double dlugoscZgloszenie = 0, szerokoscZgloszenie = 0;
            NumberFormatInfo provider = new NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";
            zgloszenie objZgloszenie = null;
            szmalDBEvents db = new szmalDBEvents();
            {
                objZgloszenie = db.zgloszenies.First(a => a.dlugosc.Equals(ev.dlugosc));
           
                typ_zgloszenia = ev.typ_zgloszenia;
                try
                {
                    dlugoscZgloszenie = Convert.ToDouble(ev.dlugosc,provider);
                    szerokoscZgloszenie = Convert.ToDouble(ev.szerokosc,provider);
                }
                catch(Exception e)
                {
                    Debug.Write(e.Message);
                }
            }
            IEnumerable<instytucja> obj=null;
            szmalDBOrganizations db1 = new szmalDBOrganizations();
            {
                try
                {
                     obj = (db1.instytucjas.Select(b => b).Where(c => c.działalnosc.Equals(typ_zgloszenia))).
                        AsEnumerable().OrderBy(a => getDistance(dlugoscZgloszenie, szerokoscZgloszenie, Convert.ToDouble(a.dlugosc, provider), Convert.ToDouble(a.szerokosc, provider)));
                    
                }
                catch (Exception e)
                {
                    Debug.Write(e.Message);
                }
            }
            if(obj!=null)
            html = RenderRazorViewToString(path, new BigModelRaport() { instytucja= obj.First(), zgloszenie=objZgloszenie });
            string sendto = obj.First().email;
            
            Email(html, sendto);
            
            db.Dispose();
            db1.Dispose();
        }


        //POST: Add
        [HttpPost]
        public ActionResult AddEvent(zgloszenie ev)
        {
            
            using (szmalDBEvents events = new szmalDBEvents())
            {
                try
                {

                    if (Request.Files.Count > 0)
                    {
                        var binaryReader = new BinaryReader(Request.Files[0].InputStream);
                        ev.image = binaryReader.ReadBytes((int)Request.Files[0].InputStream.Length );
                        binaryReader.Dispose();
                    }
                    ev.fk_login = ((string)Session["UserID"]);
                    ev.status = 0;
                   
                    events.zgloszenies.Add(ev);
                    events.SaveChanges();
                    CreatePdf(ev);
                }
                catch (Exception e)
                {
                    return View("~/Views/Home/Error.cshtml", e.Message);
                }
            }

            szmalDBEvents eve = new szmalDBEvents();
            var lista = eve.zgloszenies.Select(s => s).ToList();
            eve.Dispose();
            return View("~/Views/Home/Yours.cshtml", lista);
        }
    }
}