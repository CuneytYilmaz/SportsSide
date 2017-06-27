using sahakirala.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Net.Mail;
using System.Net;
using Database;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;


namespace sahakirala.Controllers
{
    public class AccountController : Controller
    {
        SportsSideWebApi.Controllers.SportsSideApiController webApi = new SportsSideWebApi.Controllers.SportsSideApiController();
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            string role = new Business.AccountBusiness().currentMember();
            string cookieName = FormsAuthentication.FormsCookieName; //Find cookie name
            HttpCookie authCookie = HttpContext.Request.Cookies[cookieName]; //Get the cookie by it's name
            try
            {
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
                if ((role == "U" || role == "A" || role == "O") && FormsAuthentication.Decrypt(authCookie.Value) != null)
                {
                    return RedirectToAction("Index", "Arama");
                }
            }
            catch (Exception)
            {
                return View();
            }
            //TempData["Message"] = "Lütfen kullanıcı adı ve şifrenizle giriş yapınız.";
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            DatabaseEntities db = new DatabaseEntities();

            


            //var result = new Business.AccountBusiness().Login(userName, pass);
            string userName = form["username"].ToString();
            string pass = form["password"].ToString();
            var result = new Business.AccountBusiness().Login(userName, pass);
            if (result == "Basarili")
            {
                FormsAuthentication.SetAuthCookie(userName, false);
                return RedirectToAction("Index", "Arama");
            }
            else if (result == null)
            {
                TempData["Message"] = "Kullanıcı adı ve şifrenizi giriniz.";
            }
            else
            {
                TempData["Message"] = result;
            }



            return View();
        }
        public ActionResult Register()
        {
            DatabaseEntities db = new DatabaseEntities();

            ViewBag.Areas = new SelectList(webApi.GetAreas(), "AREA_ID", "AREA_NAME");
            ViewBag.USER_DISTRICT = new SelectList(db.DISTRICTs, "DISTRICT_ID", "DISTRICT_NAME");

            return View();
        }
        [HttpPost]
        public ActionResult Register(USER Users)
        {
            DatabaseEntities db = new DatabaseEntities();
            ViewBag.USER_DISTRICT = new SelectList(db.DISTRICTs, "DISTRICT_ID", "DISTRICT_NAME", Users.USER_DISTRICT);

            string userName = Users.USER_NAME.ToString();
            string pass = Users.USER_PASSWORD.ToString();
            string firstName = Users.USER_FIRST_NAME.ToString();
            int district = Int32.Parse(Users.USER_DISTRICT.ToString());
            string surname = Users.USER_SURNAME.ToString();
            string gsm = Users.USER_GSM.ToString();
            string email = Users.USER_MAIL.ToString();
            var result = new Business.AccountBusiness().Register(userName, pass, firstName, district, surname, gsm, email);
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Forgot_Password()
        {
            return View();
        }


        public bool SentEmail(string email,int sifre)
        {
            MailMessage mesajim = new MailMessage();
            SmtpClient istemci = new SmtpClient();

            istemci.Credentials = new System.Net.NetworkCredential("info@sportsside.net", "CunMerMus123");
            istemci.Port = 587;
            istemci.Host = "mail.sportsside.net";
            istemci.EnableSsl = false;
            mesajim.IsBodyHtml = true;
            mesajim.To.Add(email);
            mesajim.From = new MailAddress("info@sportsside.net", "SportsSide");
            mesajim.Subject = "Yeni Şifreniz";
            mesajim.Body = "<table style='background-color: #f6f6f6;width: 100%;'><tr><td></td><td style='display: block !important;max-width: 600px !important;margin: 0 auto !important;clear: both !important;' width='600'><div style='max-width: 600px;margin: 0 auto;display: block;padding: 20px;'><table style='background: #fff;border: 1px solid #e9e9e9;border-radius: 3px;' width='100%' cellpadding='0' cellspacing='0'><tr><td style='background: #68b90f;font-size: 16px;color: #fff;font-weight: 500;padding: 20px;text-align: center;border-radius: 3px 3px 0 0;'>Yeni Şifre</td></tr><tr><td style='padding: 20px;'><table width = '100%' cellpadding='0' cellspacing='0'><tr><td style='padding: 0 0 20px; color:#000;'>Merhaba sitemize giriş yapmanız için gereken şifre <strong> " + sifre.ToString() + "  </strong> ,</td></tr><tr><td style='padding: 0 0 20px; color:#000;'></td></tr><tr><td style='padding: 0 0 20px;'><a href = 'http://localhost:30471/Account/login' style='text-decoration: none;color: #FFF;background-color: #348eda;border: solid #348eda;border-width: 10px 20px;line-height: 2;font-weight: bold;text-align: center;cursor: pointer;display: inline-block;border-radius: 5px;text-transform: capitalize;'>Kullanıcı Girişi</a></td></tr><tr><td style='padding: 0 0 20px; color:#000;'>SportsSide'ı seçtiğiniz için teşekkür ederiz.</td></tr></table></td></tr></table></td><td></td></tr></table>";
            //mesajim.Body = "Merhaba <b>" + user.USER_FIRST_NAME + "</b> " + user.USER_SURNAME + ", rezervasyonunuz tesis sahibine iletilmiştir. Tesis sahibinden geri dönüş aldığımız an size tekrar mail yoluyla bilgilendirme yapılacaktır. Bizi seçtiğiniz için teşekkür ederiz!";
            object userState = mesajim;
            istemci.Send(mesajim);
            bool kontrol = true;
            

            return kontrol;
        }


        [HttpPost]
        public JsonResult SentMail(string email)
        {
 
                int sayi;
                Random rastgele = new Random();
                sayi = rastgele.Next(100000, 999999);

                SportsSideWebApi.Controllers.SportsSideApiController webApi = new SportsSideWebApi.Controllers.SportsSideApiController();
                string getJson = "";
                bool model2 = webApi.ControlMail(email);

                if (email == "")
                {   // mail girilmeden post edildi
                    getJson = "0";
                }
                else if (model2 == true)
                {
                    //Mail sistem datanasinde var
                    bool kontrol = SentEmail(email, sayi);
                    if (kontrol == true)
                    {   // emalin sahibi kullanıcının sifresini mailde gönderilen yapar
                        webApi.MailSifreDegistir(email, sayi);
                        //Mail gönderildi
                        getJson = "1";
                    }
                    else
                    {
                        // Mail gonderme methodunda hata
                        //SentMailde güncelleme yapılınca aktifleşecek
                        getJson = "4";
                    }
                }
                else if (model2 == false)
                {
                    //Mail sistem databasende yok
                    getJson = "3";
                };

                return Json(getJson);
        }
        [HttpGet]
        public ActionResult MailLogin()
        {

            return RedirectToAction("Login", "Account");
        }
        public ActionResult MailTekrar()
        {

            return RedirectToAction("Forgot_Password", "Account");
        }
        public ActionResult MailKayit()
        {

            return RedirectToAction("Register", "Account");
        }

        public ActionResult Logout() {
            new Business.AccountBusiness().Logout();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
    }
}