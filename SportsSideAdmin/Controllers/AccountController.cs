using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;


namespace SportsSideAdmin.Controllers
{
    public class AccountController : Controller
    {
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
                if ((role == "A" || role == "O") && FormsAuthentication.Decrypt(authCookie.Value) != null)
                {
                    return RedirectToAction("Index", "Dashboard");
                }
                else if (role == "U")
                {
                    TempData["Message"] = "Yönetici rolüne sahip değilsiniz";
                }

                return View();
            }
            catch (Exception)
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            
            string userName = form["username"].ToString();
            string pass = form["password"].ToString();
            var result = new Business.AccountBusiness().Login(userName, pass);
          
            if (result == "Basarili")
            {
                FormsAuthentication.SetAuthCookie(userName, false);
                return RedirectToAction("Index", "Dashboard");
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

        public ActionResult Logout()
        {
            new Business.AccountBusiness().Logout();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

        public ActionResult Forgot_Password()
        {
            return View();
        }
        public bool SentEmail(string email, int sifre)
        {
            //Bilgilendirme Maili
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
            mesajim.Body = "<table style='background-color: #f6f6f6;width: 100%;'><tr><td></td><td style='display: block !important;max-width: 600px !important;margin: 0 auto !important;clear: both !important;' width='600'><div style='max-width: 600px;margin: 0 auto;display: block;padding: 20px;'><table style='background: #fff;border: 1px solid #e9e9e9;border-radius: 3px;' width='100%' cellpadding='0' cellspacing='0'><tr><td style='background: #68b90f;font-size: 16px;color: #fff;font-weight: 500;padding: 20px;text-align: center;border-radius: 3px 3px 0 0;'>Yeni Şifre</td></tr><tr><td style='padding: 20px;'><table width = '100%' cellpadding='0' cellspacing='0'><tr><td style='padding: 0 0 20px; color:#000;'>Merhaba sitemize giriş yapmanız için gereken şifre <strong>" + sifre.ToString() + "  </strong> ,</td></tr><tr><td style='padding: 0 0 20px; color:#000;'></td></tr><tr><td style='padding: 0 0 20px;'><a href = 'http://localhost:10988/Account/' style='text-decoration: none;color: #FFF;background-color: #348eda;border: solid #348eda;border-width: 10px 20px;line-height: 2;font-weight: bold;text-align: center;cursor: pointer;display: inline-block;border-radius: 5px;text-transform: capitalize;'>Yönetici Girişi</a></td></tr><tr><td style='padding: 0 0 20px; color:#000;'>SportsSide'ı seçtiğiniz için teşekkür ederiz.</td></tr></table></td></tr></table></td><td></td></tr></table>";
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
    }


}
