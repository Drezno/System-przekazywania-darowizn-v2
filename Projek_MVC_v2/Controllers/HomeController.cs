using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Asn1.Ocsp;
using Projek_MVC_v2.Models;

namespace Projek_MVC_v2.Controllers
{
    public class HomeController : Controller
    {

        private readonly IConfiguration configuration;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }



        
        public IActionResult Index()
        {/*
            var model = new List<Kampanie>();

            String connectionString = configuration.GetConnectionString("DefaultConnection");

            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand com = new SqlCommand("Select * from kampanie", connection);
            SqlDataReader rdr = com.ExecuteReader();

            while (rdr.Read())
            {
                var cam = new Kampanie();
                cam.ID_kampanii = (int)rdr["idkampanie"];
                cam.tytul_kampanii = rdr["tytul"].ToString();

                model.Add(cam);
            }


            connection.Close();
            */
            return View();
        }
        
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult SupportCampaign()
        {
            return View();
        }

        //[HttpGet]
        public IActionResult DonorForm()
        {
            return View();
        }



        

        //[HttpGet]
        public ActionResult Donation(Projek_MVC_v2.Models.DonorFormModel model, int id)
        {
            //string mm = Request["postal_code"];
            /*string imie = model.First_Name;
            string subject = "Przekazanie darowizny";

            MailMessage mm = new MailMessage();
            mm.To.Add("paxior788@gmail.com");
            mm.Subject = subject;
            mm.Body = imie;
            mm.From = new MailAddress("dobrowraca1997@gmail.com");
            mm.IsBodyHtml = false;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.Port = 587;
            smtp.UseDefaultCredentials = true;
            smtp.EnableSsl = true;
            smtp.Credentials = new System.Net.NetworkCredential("dobrowraca1997@gmail.com", "poziom1C");
            smtp.Send(mm);*/
            

            MailMessage mail = new MailMessage("dobrowraca1997@gmail.com", "paxior788@gmail.com");
            mail.Subject = "Przekazano darowiznę na kampanię "+"'"+id+"'";
            mail.Body = "DANE DARCZYŃCY\n\nEmail: "+model.Email+"\n" +
                "Imię: "+model.First_Name+"\n" +
                "Nazwisko: "+model.Last_Name+"\n" +
                "Numer telefonu: "+model.PhoneNumber+"\n" +
                "Województwo: "+model.County+"\n" +
                "Miasto: "+model.City+"\n" +
                "Ulica: "+model.Street+" "+model.Street_Number+"\n" +
                "Kod pocztowy: "+model.PostalCode+"\n" +
                "Preferowana data odbioru: "+model.Date+"\n" +
                "Przekazuje dla organizacji: ";
            mail.IsBodyHtml = false;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;

            NetworkCredential nc = new NetworkCredential("dobrowraca1997@gmail.com", "poziom1C");
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = nc;
            smtp.Send(mail);

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
