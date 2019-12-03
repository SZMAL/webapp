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

        public ActionResult Raport()
        {
            return View("~/Views/Raport/raport.cshtml");
        }
        // GET: Statystyki twoje
        public ActionResult YoursStats(uzytkownik u)
        {
            return View("~/Views/Home/YoursStats.cshtml", u);
        }
        public void Email(string pdf, string email)
        {
            try
            {
                var apiKey = "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";
                var client = new SendGridClient(apiKey);
                var msg = new SendGridMessage()
                {
                    From = new EmailAddress("szmal.wcy@gmail.com", "SZMAL"),
                    Subject = "Raport o zdarzeniu",
                    PlainTextContent = "Przesłano raport o zdarzeniu, pozdrawiamy Zespół SZMAL",
                    HtmlContent = "<strong>Przesłano raport o zdarzeniu, <br>pozdrawiamy Zespół SZMAL<strong>"
                };
                var bytes = System.IO.File.ReadAllBytes(pdf);
                var file = Convert.ToBase64String(bytes);
                msg.AddAttachment("raport.pdf", file);

                msg.AddTo(new EmailAddress(email, "Organizacja"));
                var response = client.SendEmailAsync(msg);

                /*
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("szmal.wcy@gmail.com");
                message.To.Add(new MailAddress("chelseaman96@gmail.com"));
                message.Subject = "Raport o zdarzeniu";
                System.Net.Mail.Attachment attachment;
                attachment = new System.Net.Mail.Attachment(pdf);
                message.Attachments.Add(attachment);
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; 
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("szmal.wcy@gmail.com", "ZbyszekStonoga1");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
                 */

            }
            catch (Exception) { }
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
            var htmlToPdf = new HtmlToPdf();
            var pdf = htmlToPdf.RenderHtmlAsPdf(html);
            
            var OutputPath =Environment.GetFolderPath( Environment.SpecialFolder.LocalApplicationData)+"/raport.pdf";

            pdf.SaveAs(OutputPath);
            Email(OutputPath, sendto);
            System.Diagnostics.Process.Start(OutputPath);
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
           

            return View("~/Views/Home/Yours.cshtml", true);
        }
    }
}