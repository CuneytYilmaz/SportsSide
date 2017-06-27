using SportsSideAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PagedList;
using System.Net.Mail;

namespace SportsSideAdmin.Controllers
{
    [Authorize]
    public class ApproveController : Controller
    {
        SportsSideWebApi.Controllers.SportsSideApiController webApi = new SportsSideWebApi.Controllers.SportsSideApiController();
        // GET: ReservationApprove
        public ActionResult Index(int? pageRes, int? pageSub, int? pageAllRes, int? pageAllSub, int? selectedTab)
        {
            string role = new Business.AccountBusiness().currentMember();
            if (role == "U")
            {
                return RedirectToAction("Login", "Account");
            }
            if (selectedTab != null)
            {
                TempData["selectedTab"] = selectedTab;
            }

            int pageSize = 10;
            int pageNumberRes = (pageRes ?? 1);
            int pageNumberSub = (pageSub ?? 1);
            int pageNumberAllRes = (pageAllRes ?? 1);
            int pageNumberAllSub = (pageAllSub ?? 1);
            TempData["selectedTab"] = TempData["selectedTab"] == null ? 1 : TempData["selectedTab"];
            string cookieName = FormsAuthentication.FormsCookieName; //Find cookie name
            HttpCookie authCookie = HttpContext.Request.Cookies[cookieName]; //Get the cookie by it's name
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value); //Decrypt it
            string UserName = ticket.Name; //You have the UserName!
            var user = webApi.GetUser(UserName);

            ApproveModel model = new ApproveModel();
            model.Reservations = webApi.GetReservationsForApprove(user.USER_ID).ToPagedList(pageNumberRes, pageSize);
            model.Subscribers = webApi.GetSubscribersForApprove(user.USER_ID).ToPagedList(pageNumberSub, pageSize);
            model.AllReservations = webApi.GetAllReservationsForFacilityOwner(user.USER_ID).ToPagedList(pageNumberAllRes, pageSize);
            model.AllSubscibers = webApi.GetAllSubscribersForFacilityOwner(user.USER_ID).ToPagedList(pageNumberAllSub, pageSize);
            model.selectedTab = TempData["selectedTab"].ToString();

            return View(model);
        }

        [HttpGet]
        public bool RejectReservation(int id)
        {
            SportsSide.DAL.RESERVATION model = new SportsSide.DAL.RESERVATION();
            model = webApi.GetReservationFromReservationId(id);
            model.STATUS = "R";
            var result = webApi.PutReservation(model);
            TempData["selectedTab"] = 1;
            //Bilgilendirme Maili
            MailMessage mesajim = new MailMessage();
            SmtpClient istemci = new SmtpClient();

            istemci.Credentials = new System.Net.NetworkCredential("info@sportsside.net", "CunMerMus123");
            istemci.Port = 587;
            istemci.Host = "mail.sportsside.net";
            istemci.EnableSsl = false;
            mesajim.IsBodyHtml = true;
            mesajim.To.Add(model.USERS.USER_MAIL);
            mesajim.From = new MailAddress("info@sportsside.net", "SportsSide");
            mesajim.Subject = "Rezervasyonunuz Reddedilmiştir";
            mesajim.Body = "<table style='background-color: #f6f6f6;width: 100%;'><tr><td></td><td style='display: block !important;max-width: 600px !important;margin: 0 auto !important;clear: both !important;' width='600'><div style='max-width: 600px;margin: 0 auto;display: block;padding: 20px;'><table style='background: #fff;border: 1px solid #e9e9e9;border-radius: 3px;' width='100%' cellpadding='0' cellspacing='0'><tr><td style='background: #d0021b;font-size: 16px;color: #fff;font-weight: 500;padding: 20px;text-align: center;border-radius: 3px 3px 0 0;'>Rezervasyonunuz Reddedilmiştir!</td></tr><tr><td style='padding: 20px;'><table width = '100%' cellpadding='0' cellspacing='0'><tr><td style='padding: 0 0 20px; color:#000;'>Merhaba<strong> " + model.USERS.USER_FIRST_NAME + " " + model.USERS.USER_SURNAME + "</strong> ,</td></tr><tr><td style='padding: 0 0 20px; color:#000;'>Rezervasyonunuz tesis sahibi tarafından reddedilmiştir.</td></tr><tr><td style='padding: 0 0 20px; color:#000;'><strong>Tesis Adı :</strong> " + model.FACILITY.FACILITY_NAME + " <br><strong>Başlangıç Tarihi :</strong> " + model.DT_BEGIN.ToString("dd/MM/yyyy H:mm") + " <br><strong>Bitiş Tarihi :</strong> " + model.DT_END.ToString("dd/MM/yyyy H:mm") + " <br><strong>Oluşturulma Tarihi :</strong> " + model.CREATED_DATE.ToString("dd/MM/yyyy H:mm") + " <br><strong>Kişi Sayısı :</strong> " + model.COUNT + " <br><strong>Tesis Adresi :</strong> " + model.FACILITY.FACILITY_ADDRESS + "<br></td></tr><tr><td style='padding: 0 0 20px;'><a href = 'http://localhost:30471/Member/Index' style='text-decoration: none;color: #FFF;background-color: #348eda;border: solid #348eda;border-width: 10px 20px;line-height: 2;font-weight: bold;text-align: center;cursor: pointer;display: inline-block;border-radius: 5px;text-transform: capitalize;'>Rezervasyonları Görüntüle</a></td></tr><tr><td style='padding: 0 0 20px; color:#000;'>SportsSide'ı seçtiğiniz için teşekkür ederiz.</td></tr></table></td></tr></table></td><td></td></tr></table>";
            //mesajim.Body = "Merhaba <b>" + user.USER_FIRST_NAME + "</b> " + user.USER_SURNAME + ", rezervasyonunuz tesis sahibine iletilmiştir. Tesis sahibinden geri dönüş aldığımız an size tekrar mail yoluyla bilgilendirme yapılacaktır. Bizi seçtiğiniz için teşekkür ederiz!";
            object userState = mesajim;
            istemci.Send(mesajim);
            //
            return result;
        }

        [HttpGet]
        public bool ApproveReservation(int id)
        {
            SportsSide.DAL.RESERVATION model = new SportsSide.DAL.RESERVATION();
            model = webApi.GetReservationFromReservationId(id);
            model.STATUS = "A";
            var result = webApi.PutReservation(model);
            TempData["selectedTab"] = 1;
            //Bilgilendirme Maili
            MailMessage mesajim = new MailMessage();
            SmtpClient istemci = new SmtpClient();

            istemci.Credentials = new System.Net.NetworkCredential("info@sportsside.net", "CunMerMus123");
            istemci.Port = 587;
            istemci.Host = "mail.sportsside.net";
            istemci.EnableSsl = false;
            mesajim.IsBodyHtml = true;
            mesajim.To.Add(model.USERS.USER_MAIL);
            mesajim.From = new MailAddress("info@sportsside.net", "SportsSide");
            mesajim.Subject = "Rezervasyonunuz Onaylanmıştır";
            mesajim.Body = "<table style='background-color: #f6f6f6;width: 100%;'><tr><td></td><td style='display: block !important;max-width: 600px !important;margin: 0 auto !important;clear: both !important;' width='600'><div style='max-width: 600px;margin: 0 auto;display: block;padding: 20px;'><table style='background: #fff;border: 1px solid #e9e9e9;border-radius: 3px;' width='100%' cellpadding='0' cellspacing='0'><tr><td style='background: #68b90f;font-size: 16px;color: #fff;font-weight: 500;padding: 20px;text-align: center;border-radius: 3px 3px 0 0;'>Rezervasyonunuz Onaylanmıştır!</td></tr><tr><td style='padding: 20px;'><table width = '100%' cellpadding='0' cellspacing='0'><tr><td style='padding: 0 0 20px; color:#000;'>Merhaba<strong> " + model.USERS.USER_FIRST_NAME + " " + model.USERS.USER_SURNAME + "</strong> ,</td></tr><tr><td style='padding: 0 0 20px; color:#000;'>Rezervasyonunuz tesis sahibi tarafından onaylanmıştır. Keyifli zaman geçirmenizi dileriz :)</td></tr><tr><td style='padding: 0 0 20px; color:#000;'><strong>Tesis Adı :</strong> " + model.FACILITY.FACILITY_NAME + " <br><strong>Başlangıç Tarihi :</strong> " + model.DT_BEGIN.ToString("dd/MM/yyyy H:mm") + " <br><strong>Bitiş Tarihi :</strong> " + model.DT_END.ToString("dd/MM/yyyy H:mm") + " <br><strong>Oluşturulma Tarihi :</strong> " + model.CREATED_DATE.ToString("dd/MM/yyyy H:mm") + " <br><strong>Kişi Sayısı :</strong> " + model.COUNT + " <br><strong>Tesis Adresi :</strong> " + model.FACILITY.FACILITY_ADDRESS + "<br></td></tr><tr><td style='padding: 0 0 20px;'><a href = 'http://localhost:30471/Member/Index' style='text-decoration: none;color: #FFF;background-color: #348eda;border: solid #348eda;border-width: 10px 20px;line-height: 2;font-weight: bold;text-align: center;cursor: pointer;display: inline-block;border-radius: 5px;text-transform: capitalize;'>Rezervasyonları Görüntüle</a></td></tr><tr><td style='padding: 0 0 20px; color:#000;'>SportsSide'ı seçtiğiniz için teşekkür ederiz.</td></tr></table></td></tr></table></td><td></td></tr></table>";
            //mesajim.Body = "Merhaba <b>" + user.USER_FIRST_NAME + "</b> " + user.USER_SURNAME + ", rezervasyonunuz tesis sahibine iletilmiştir. Tesis sahibinden geri dönüş aldığımız an size tekrar mail yoluyla bilgilendirme yapılacaktır. Bizi seçtiğiniz için teşekkür ederiz!";
            object userState = mesajim;
            istemci.Send(mesajim);
            //
            return result;
        }

        [HttpGet]
        public bool ApproveSubscriber(int id)
        {
            SportsSide.DAL.SUBSCRIBER model = new SportsSide.DAL.SUBSCRIBER();
            model = webApi.GetSubscriberFromSubscriberId(id);
            model.STATUS = "A";
            var result = webApi.PutSubscriber(model);
            TempData["selectedTab"] = 2;
            //Bilgilendirme Maili
            string day = "";
            switch (model.WEEK_DAY)
            {
                case "Monday":
                    day = "Pazartesi";
                    break;
                case "Tuesday":
                    day = "Salı";
                    break;
                case "Wednesday":
                    day = "Çarşamba";
                    break;
                case "Thursday":
                    day = "Perşembe";
                    break;
                case "Friday":
                    day = "Cuma";
                    break;
                case "Saturday":
                    day = "Cumartesi";
                    break;
                case "Sunday":
                    day = "Pazar";
                    break;
            }

            MailMessage mesajim = new MailMessage();
            SmtpClient istemci = new SmtpClient();

            istemci.Credentials = new System.Net.NetworkCredential("info@sportsside.net", "CunMerMus123");
            istemci.Port = 587;
            istemci.Host = "mail.sportsside.net";
            istemci.EnableSsl = false;
            mesajim.IsBodyHtml = true;
            mesajim.To.Add(model.USERS.USER_MAIL);
            mesajim.From = new MailAddress("info@sportsside.net", "SportsSide");
            mesajim.Subject = "Aboneliğiniz Onaylanmıştır";
            mesajim.Body = "<table style='background-color: #f6f6f6;width: 100%;'><tr><td></td><td style='display: block !important;max-width: 600px !important;margin: 0 auto !important;clear: both !important;' width='600'><div style='max-width: 600px;margin: 0 auto;display: block;padding: 20px;'><table style='background: #fff;border: 1px solid #e9e9e9;border-radius: 3px;' width='100%' cellpadding='0' cellspacing='0'><tr><td style='background: #68b90f;font-size: 16px;color: #fff;font-weight: 500;padding: 20px;text-align: center;border-radius: 3px 3px 0 0;'>Aboneliğiniz Onaylanmıştır!</td></tr><tr><td style='padding: 20px;'><table width = '100%' cellpadding='0' cellspacing='0'><tr><td style='padding: 0 0 20px; color:#000;'>Merhaba<strong> " + model.USERS.USER_FIRST_NAME + " " + model.USERS.USER_SURNAME + "</strong> ,</td></tr><tr><td style='padding: 0 0 20px; color:#000;'>Aboneliğiniz tesis sahibi tarafından onaylanmıştır. Keyifli zaman geçirmenizi dileriz :)</td></tr><tr><td style='padding: 0 0 20px; color:#000;'><strong>Tesis Adı :</strong> " + model.FACILITY.FACILITY_NAME + " <br><strong>Abonelik Günü :</strong> " + day + " <br><strong>Abonelik Saati :</strong> " + DateTime.Parse(model.S_TIME.ToString()).ToShortTimeString() + " - " + (int.Parse(model.S_TIME.Hours.ToString()) + 1).ToString() + ":00 <br><strong>Oluşturulma Tarihi :</strong> " + model.CREATED_DATE.ToString("dd/MM/yyyy H:mm") + " <br><strong>Tesis Adresi :</strong> " + model.FACILITY.FACILITY_ADDRESS + "<br></td></tr><tr><td style='padding: 0 0 20px;'><a href = 'http://localhost:30471/Member/Index' style='text-decoration: none;color: #FFF;background-color: #348eda;border: solid #348eda;border-width: 10px 20px;line-height: 2;font-weight: bold;text-align: center;cursor: pointer;display: inline-block;border-radius: 5px;text-transform: capitalize;'>Abonelikleri Görüntüle</a></td></tr><tr><td style='padding: 0 0 20px; color:#000;'>SportsSide'ı seçtiğiniz için teşekkür ederiz.</td></tr></table></td></tr></table></td><td></td></tr></table>";
            //mesajim.Body = "Merhaba <b>" + user.USER_FIRST_NAME + "</b> " + user.USER_SURNAME + ", rezervasyonunuz tesis sahibine iletilmiştir. Tesis sahibinden geri dönüş aldığımız an size tekrar mail yoluyla bilgilendirme yapılacaktır. Bizi seçtiğiniz için teşekkür ederiz!";
            object userState = mesajim;
            istemci.Send(mesajim);
            //
            return result;
        }

        [HttpGet]
        public bool RejectSubscriber(int id)
        {
            SportsSide.DAL.SUBSCRIBER model = new SportsSide.DAL.SUBSCRIBER();
            model = webApi.GetSubscriberFromSubscriberId(id);
            model.STATUS = "R";
            var result = webApi.PutSubscriber(model);
            TempData["selectedTab"] = 2;
            //Bilgilendirme Maili
            string day = "";
            switch (model.WEEK_DAY)
            {
                case "Monday":
                    day = "Pazartesi";
                    break;
                case "Tuesday":
                    day = "Salı";
                    break;
                case "Wednesday":
                    day = "Çarşamba";
                    break;
                case "Thursday":
                    day = "Perşembe";
                    break;
                case "Friday":
                    day = "Cuma";
                    break;
                case "Saturday":
                    day = "Cumartesi";
                    break;
                case "Sunday":
                    day = "Pazar";
                    break;
            }
            MailMessage mesajim = new MailMessage();
            SmtpClient istemci = new SmtpClient();

            istemci.Credentials = new System.Net.NetworkCredential("info@sportsside.net", "CunMerMus123");
            istemci.Port = 587;
            istemci.Host = "mail.sportsside.net";
            istemci.EnableSsl = false;
            mesajim.IsBodyHtml = true;
            mesajim.To.Add(model.USERS.USER_MAIL);
            mesajim.From = new MailAddress("info@sportsside.net", "SportsSide");
            mesajim.Subject = "Aboneliğiniz Reddedilmiştir";
            mesajim.Body = "<table style='background-color: #f6f6f6;width: 100%;'><tr><td></td><td style='display: block !important;max-width: 600px !important;margin: 0 auto !important;clear: both !important;' width='600'><div style='max-width: 600px;margin: 0 auto;display: block;padding: 20px;'><table style='background: #fff;border: 1px solid #e9e9e9;border-radius: 3px;' width='100%' cellpadding='0' cellspacing='0'><tr><td style='background: #d0021b;font-size: 16px;color: #fff;font-weight: 500;padding: 20px;text-align: center;border-radius: 3px 3px 0 0;'>Aboneliğiniz Reddedilmiştir!</td></tr><tr><td style='padding: 20px;'><table width = '100%' cellpadding='0' cellspacing='0'><tr><td style='padding: 0 0 20px; color:#000;'>Merhaba<strong> " + model.USERS.USER_FIRST_NAME + " " + model.USERS.USER_SURNAME + "</strong> ,</td></tr><tr><td style='padding: 0 0 20px; color:#000;'>Aboneliğiniz tesis sahibi tarafından reddedilmiştir.</td></tr><tr><td style='padding: 0 0 20px; color:#000;'><strong>Tesis Adı :</strong> " + model.FACILITY.FACILITY_NAME + " <br><strong>Abonelik Günü :</strong> " + day + " <br><strong>Abonelik Saati :</strong> " + DateTime.Parse(model.S_TIME.ToString()).ToShortTimeString() + " - " + (int.Parse(model.S_TIME.Hours.ToString()) + 1).ToString() + ":00 <br><strong>Oluşturulma Tarihi :</strong> " + model.CREATED_DATE.ToString("dd/MM/yyyy H:mm") + " <br><strong>Tesis Adresi :</strong> " + model.FACILITY.FACILITY_ADDRESS + "<br></td></tr><tr><td style='padding: 0 0 20px;'><a href = 'http://localhost:30471/Member/Index' style='text-decoration: none;color: #FFF;background-color: #348eda;border: solid #348eda;border-width: 10px 20px;line-height: 2;font-weight: bold;text-align: center;cursor: pointer;display: inline-block;border-radius: 5px;text-transform: capitalize;'>Abonelikleri Görüntüle</a></td></tr><tr><td style='padding: 0 0 20px; color:#000;'>SportsSide'ı seçtiğiniz için teşekkür ederiz.</td></tr></table></td></tr></table></td><td></td></tr></table>";
            //mesajim.Body = "Merhaba <b>" + user.USER_FIRST_NAME + "</b> " + user.USER_SURNAME + ", rezervasyonunuz tesis sahibine iletilmiştir. Tesis sahibinden geri dönüş aldığımız an size tekrar mail yoluyla bilgilendirme yapılacaktır. Bizi seçtiğiniz için teşekkür ederiz!";
            object userState = mesajim;
            istemci.Send(mesajim);
            //
            return result;
        }

        [HttpPost]
        public JsonResult UserPos(int id, string type)
        {
            int myId = 0;
            if (type == "R")
            {
                var reservation = webApi.GetReservationFromReservationId(id);
                reservation.R_HISTORY = "1";
                webApi.PutReservation(reservation);
                myId = reservation.USERS.USER_ID;
            }
            else if (type == "S")
            {
                var subscriber = webApi.GetSubscriberFromSubscriberId(id);
                subscriber.R_HISTORY = "1";
                webApi.PutSubscriber(subscriber);
                myId = subscriber.USERS.USER_ID;
            }
            var user = webApi.GetUser(myId);
            user.USER_POS = user.USER_POS + 1;
            var result = webApi.PutUser(user);
            return Json(result);
        }
        [HttpPost]
        public JsonResult UserNeg(int id, string type)
        {
            int myId = 0;
            if (type == "R")
            {
                var reservation = webApi.GetReservationFromReservationId(id);
                reservation.R_HISTORY = "1";
                webApi.PutReservation(reservation);
                myId = reservation.USERS.USER_ID;
            }
            else if (type == "S")
            {
                var subscriber = webApi.GetSubscriberFromSubscriberId(id);
                subscriber.R_HISTORY = "1";
                webApi.PutSubscriber(subscriber);
                myId = subscriber.USERS.USER_ID;
            }
            var user = webApi.GetUser(myId);
            user.USER_NEG = user.USER_NEG + 1;
            var result = webApi.PutUser(user);
            return Json(result);
        }
    }
}