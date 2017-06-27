using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sahakirala.Models;
using System.Web.Security;
using System.Net.Mail;

namespace sahakirala.Controllers
{
    public class ReservationController : Controller
    {
        SportsSideWebApi.Controllers.SportsSideApiController webApi = new SportsSideWebApi.Controllers.SportsSideApiController();
        public SelectList listDays { get; set; }
        public SelectList listHours { get; set; }
        // GET: Reservation
        public ActionResult Index(int id)
            {
            ReservationBigModel model = new ReservationBigModel();
            model.Date = System.DateTime.Now.Date;
            //model.Date = DateTime.Parse("23.04.2017");
            var facility = webApi.GetFacility(id);
            var reservations = webApi.GetFacilityReservations(id);
            var subscribers = webApi.GetFacilitySubscribers(id);
            var facilityPictures = webApi.GetPicturesFromFacility(id);
            model.Facility = facility;
            model.Reservations = reservations;
            model.Subscribers = subscribers;
            model.FacilityPictures = facilityPictures;
            #region FillDays

            List<SelectListItem> weekDays = new List<SelectListItem>();
            SelectListItem _dList = new SelectListItem();
            _dList = new SelectListItem() { Text = "Pazartesi", Value = "Monday" };
            weekDays.Add(_dList);
            _dList = new SelectListItem() { Text = "Salı", Value = "Tuesday" };
            weekDays.Add(_dList);
            _dList = new SelectListItem() { Text = "Çarşamba", Value = "Wednesday" };
            weekDays.Add(_dList);
            _dList = new SelectListItem() { Text = "Perşembe", Value = "Thursday" };
            weekDays.Add(_dList);
            _dList = new SelectListItem() { Text = "Cuma", Value = "Friday" };
            weekDays.Add(_dList);
            _dList = new SelectListItem() { Text = "Cumartesi", Value = "Saturday" };
            weekDays.Add(_dList);
            _dList = new SelectListItem() { Text = "Pazar", Value = "Sunday" };
            weekDays.Add(_dList);

            #endregion

            #region FillHours

            List<SelectListItem> Hours = new List<SelectListItem>();
            SelectListItem _hList = new SelectListItem();
            for (int i = 0; i < 24; i++)
            {
                string Hour = "";
                string nextHour = "";
                if (i < 10)
                {
                    Hour = "0" + i.ToString();
                    nextHour = (i == 9) ? "10" : "0" + (i + 1).ToString();
                }
                else
                {
                    Hour = i.ToString();
                    nextHour = (i + 1).ToString();
                }

                _hList = new SelectListItem() { Text = Hour + ":00 - " + nextHour + ":00", Value = i.ToString() };
                Hours.Add(_hList);
            }

            #endregion

            ViewBag.Days = new SelectList(weekDays, "Value", "Text");
            ViewBag.Hours = new SelectList(Hours, "Value", "Text");
            //ViewBag.FacilityTypes = new SelectList(webApi.GetFacilityTypes(), "FT_ID", "FT_NAME");

            return View(model);
        }

        [HttpPost]
        public JsonResult Create(int facilityId, DateTime date, string hour, int count)
        {
            string cookieName = FormsAuthentication.FormsCookieName; //Find cookie name
            HttpCookie authCookie = HttpContext.Request.Cookies[cookieName]; //Get the cookie by it's name
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value); //Decrypt it
            string UserName = ticket.Name; //You have the UserName!
            var user = webApi.GetUser(UserName);
            //var facility = webApi.GetFacility(model.Facility.FACILITY_ID);
            var newBeginDate = new DateTime(date.Year, date.Month, date.Day, int.Parse(hour), 0, 0);
            if (hour == "23")
            {
                date = date.AddDays(1);
                hour = "-1";
            }
            var newEndDate = new DateTime(date.Year, date.Month, date.Day, int.Parse(hour) + 1, 0, 0);

            SportsSide.DAL.RESERVATION rsvModel = new SportsSide.DAL.RESERVATION();
            rsvModel.USER_ID = user.USER_ID;
            rsvModel.FACILITY_ID = facilityId;
            rsvModel.DT_BEGIN = newBeginDate;
            rsvModel.DT_END = newEndDate;
            rsvModel.CREATED_DATE = System.DateTime.Now;
            rsvModel.STATUS = "P";
            rsvModel.COUNT = count;
            //rsvModel.USERS = user;
            //rsvModel.FACILITY = facility;

            var result = webApi.PostReservation(rsvModel);
            var facility = webApi.GetFacility(facilityId);
            //Bilgilendirme Maili
            MailMessage mesajim = new MailMessage();
            SmtpClient istemci = new SmtpClient();

            istemci.Credentials = new System.Net.NetworkCredential("info@sportsside.net", "CunMerMus123");
            istemci.Port = 587;
            istemci.Host = "mail.sportsside.net";
            istemci.EnableSsl = false;
            mesajim.IsBodyHtml = true;
            mesajim.To.Add(user.USER_MAIL);
            mesajim.From = new MailAddress("info@sportsside.net","SportsSide");
            mesajim.Subject = "Rezervasyonunuz İletilmiştir";
            mesajim.Body = "<table style='background-color: #f6f6f6;width: 100%;'><tr><td></td><td style='display: block !important;max-width: 600px !important;margin: 0 auto !important;clear: both !important;' width='600'><div style='max-width: 600px;margin: 0 auto;display: block;padding: 20px;'><table style='background: #fff;border: 1px solid #e9e9e9;border-radius: 3px;' width='100%' cellpadding='0' cellspacing='0'><tr><td style='background: #68b90f;font-size: 16px;color: #fff;font-weight: 500;padding: 20px;text-align: center;border-radius: 3px 3px 0 0;'>Rezervasyonunuz iletilmiştir!</td></tr><tr><td style='padding: 20px;'><table width = '100%' cellpadding='0' cellspacing='0'><tr><td style='padding: 0 0 20px; color:#000;'>Merhaba<strong> " + user.USER_FIRST_NAME + " " + user.USER_SURNAME + "</strong> ,</td></tr><tr><td style='padding: 0 0 20px; color:#000;'>Rezervasyonunuz tesis sahibine iletilmiştir.Tesis sahibinden geri dönüş aldığımız an size tekrar mail yoluyla bilgilendirme yapılacaktır.</td></tr><tr><td style='padding: 0 0 20px; color:#000;'><strong>Tesis Adı :</strong> " + facility.FACILITY_NAME + " <br><strong>Başlangıç Tarihi :</strong> " + newBeginDate.ToString("dd/MM/yyyy H:mm") + " <br><strong>Bitiş Tarihi :</strong> " + newEndDate.ToString("dd/MM/yyyy H:mm") + " <br><strong>Oluşturulma Tarihi :</strong> " + System.DateTime.Now.ToString("dd/MM/yyyy H:mm") + " <br><strong>Kişi Sayısı :</strong> " + count + " <br><strong>Tesis Adresi :</strong> " + facility.FACILITY_ADDRESS + "<br></td></tr><tr><td style='padding: 0 0 20px;'><a href = 'http://localhost:30471/Member/Index' style='text-decoration: none;color: #FFF;background-color: #348eda;border: solid #348eda;border-width: 10px 20px;line-height: 2;font-weight: bold;text-align: center;cursor: pointer;display: inline-block;border-radius: 5px;text-transform: capitalize;'>Rezervasyonları Görüntüle</a></td></tr><tr><td style='padding: 0 0 20px; color:#000;'>SportsSide'ı seçtiğiniz için teşekkür ederiz.</td></tr></table></td></tr></table></td><td></td></tr></table>";
            //mesajim.Body = "Merhaba <b>" + user.USER_FIRST_NAME + "</b> " + user.USER_SURNAME + ", rezervasyonunuz tesis sahibine iletilmiştir. Tesis sahibinden geri dönüş aldığımız an size tekrar mail yoluyla bilgilendirme yapılacaktır. Bizi seçtiğiniz için teşekkür ederiz!";
            object userState = mesajim;
            istemci.Send(mesajim);
            //

            return Json(result);
            //return RedirectToAction("Index", "Member");
        }

        public ActionResult SearchSummary(int id, string date, string hour)
        {
            string nextHour = "";
            if (int.Parse(hour) < 10)
            {
                nextHour = (int.Parse(hour) == 9) ? "10" : "0" + (int.Parse(hour) + 1).ToString();
            }
            else
            {
                nextHour = (int.Parse(hour) + 1).ToString();
            }

            ReservationBigModel model = new ReservationBigModel();
            model.Date = DateTime.Parse(date);
            model.Hour = hour + ":00 - " + nextHour + ":00";
            var facility = webApi.GetFacility(id);
            var reservations = webApi.GetFacilityReservations(id);
            var subscribers = webApi.GetFacilitySubscribers(id);
            var facilityPictures = webApi.GetPicturesFromFacility(id);
            model.Facility = facility;
            model.Reservations = reservations;
            model.Subscribers = subscribers;
            model.FacilityPictures = facilityPictures;
            return PartialView("_PartialReservationSummary", model);
        }

        [HttpGet]
        public ActionResult Search(int id, string date)
        {
            ReservationBigModel model = new ReservationBigModel();
            model.Date = DateTime.Parse(date);

            //model.Date = DateTime.Parse("23.04.2017");
            var facility = webApi.GetFacility(id);
            var reservations = webApi.GetFacilityReservations(id);
            var subscribers = webApi.GetFacilitySubscribers(id);
            var facilityPictures = webApi.GetPicturesFromFacility(id);
            model.Facility = facility;
            model.Reservations = reservations;
            model.Subscribers = subscribers;
            model.FacilityPictures = facilityPictures;

            return PartialView("_PartialReservation", model);
        }

        [HttpGet]
        public ActionResult CheckSub(int id, string day, string hour, string hourText)
        {
            ReservationBigModel model = new ReservationBigModel();
            List<SportsSide.DAL.RESERVATION> crossedResList = new List<SportsSide.DAL.RESERVATION>();

            var facility = webApi.GetFacility(id);
            var reservations = webApi.GetFacilityReservations(id);
            var subscribers = webApi.GetFacilitySubscribersForSubscribe(id);
            var facilityPictures = webApi.GetPicturesFromFacility(id);
            model.Facility = facility;
            model.isProblem = "N";
            //model.Reservations = reservations;
            model.Subscribers = subscribers;
            model.FacilityPictures = facilityPictures;

            reservations = reservations.Where(x => x.DT_BEGIN >= DateTime.Now).ToList();
            foreach (var item in subscribers)
            {
                if (day == item.WEEK_DAY && hour == item.S_TIME.Hours.ToString())
                {
                    model.isProblem = "E";
                }
            }

            foreach (var item in reservations)
            {
                string modelDay = item.DT_BEGIN.DayOfWeek.ToString();
                if (day == modelDay && item.DT_BEGIN.Hour.ToString() == hour)
                {
                    model.isProblem = "Y";
                    crossedResList.Add(item);
                }
            }
            model.Reservations = crossedResList;

            #region FillDays

            List<SelectListItem> weekDays = new List<SelectListItem>();
            SelectListItem _dList = new SelectListItem();
            _dList = new SelectListItem() { Text = "Pazartesi", Value = "Monday" };
            weekDays.Add(_dList);
            _dList = new SelectListItem() { Text = "Salı", Value = "Tuesday" };
            weekDays.Add(_dList);
            _dList = new SelectListItem() { Text = "Çarşamba", Value = "Wednesday" };
            weekDays.Add(_dList);
            _dList = new SelectListItem() { Text = "Perşembe", Value = "Thursday" };
            weekDays.Add(_dList);
            _dList = new SelectListItem() { Text = "Cuma", Value = "Friday" };
            weekDays.Add(_dList);
            _dList = new SelectListItem() { Text = "Cumartesi", Value = "Saturday" };
            weekDays.Add(_dList);
            _dList = new SelectListItem() { Text = "Pazar", Value = "Sunday" };
            weekDays.Add(_dList);

            #endregion

            #region FillHours

            List<SelectListItem> Hours = new List<SelectListItem>();
            SelectListItem _hList = new SelectListItem();
            for (int i = 0; i < 24; i++)
            {
                string Hour = "";
                string nextHour = "";
                if (i < 10)
                {
                    Hour = "0" + i.ToString();
                    nextHour = (i == 9) ? "10" : "0" + (i + 1).ToString();
                }
                else
                {
                    Hour = i.ToString();
                    nextHour = (i + 1).ToString();
                }

                _hList = new SelectListItem() { Text = Hour + ":00 - " + nextHour + ":00", Value = i.ToString() };
                Hours.Add(_hList);
            }

            #endregion

            ViewBag.Days = new SelectList(weekDays, "Value", "Text");
            ViewBag.Hours = new SelectList(Hours, "Value", "Text");

            //Subscriber inserted
            if (crossedResList.Count == 0 && model.isProblem == "N")
            {
                SportsSide.DAL.SUBSCRIBER subModel = new SportsSide.DAL.SUBSCRIBER();
                string cookieName = FormsAuthentication.FormsCookieName; //Find cookie name
                HttpCookie authCookie = HttpContext.Request.Cookies[cookieName]; //Get the cookie by it's name
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value); //Decrypt it
                string UserName = ticket.Name; //You have the UserName!
                var user = webApi.GetUser(UserName);
                subModel.USER_ID = user.USER_ID;
                subModel.FACILITY_ID = id;
                subModel.WEEK_DAY = day;
                TimeSpan ts = new TimeSpan(int.Parse(hour), 0, 0);
                subModel.S_TIME = ts;
                subModel.STATUS = "P";
                subModel.CREATED_DATE = DateTime.Now;
                var result = webApi.PostSubscriber(subModel);
                //Bilgilendirme Maili
                switch (day)
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
                mesajim.To.Add(user.USER_MAIL);
                mesajim.From = new MailAddress("info@sportsside.net", "SportsSide");
                mesajim.Subject = "Aboneliğiniz İletilmiştir";
                mesajim.Body = "<table style='background-color: #f6f6f6;width: 100%;'><tr><td></td><td style='display: block !important;max-width: 600px !important;margin: 0 auto !important;clear: both !important;' width='600'><div style='max-width: 600px;margin: 0 auto;display: block;padding: 20px;'><table style='background: #fff;border: 1px solid #e9e9e9;border-radius: 3px;' width='100%' cellpadding='0' cellspacing='0'><tr><td style='background: #68b90f;font-size: 16px;color: #fff;font-weight: 500;padding: 20px;text-align: center;border-radius: 3px 3px 0 0;'>Aboneliğiniz iletilmiştir!</td></tr><tr><td style='padding: 20px;'><table width = '100%' cellpadding='0' cellspacing='0'><tr><td style='padding: 0 0 20px; color:#000;'>Merhaba<strong> " + user.USER_FIRST_NAME + " " + user.USER_SURNAME + "</strong> ,</td></tr><tr><td style='padding: 0 0 20px; color:#000;'>Aboneliğiniz tesis sahibine iletilmiştir.Tesis sahibinden geri dönüş aldığımız an size tekrar mail yoluyla bilgilendirme yapılacaktır.</td></tr><tr><td style='padding: 0 0 20px; color:#000;'><strong>Tesis Adı :</strong> " + facility.FACILITY_NAME + " <br><strong>Abonelik Günü :</strong> " + day.ToString() + " <br><strong>Abonelik Saati :</strong> " + hourText.ToString() + " <br><strong>Oluşturulma Tarihi :</strong> " + System.DateTime.Now.ToString("dd/MM/yyyy H:mm") + " <br><strong>Tesis Adresi :</strong> " + facility.FACILITY_ADDRESS + "<br></td></tr><tr><td style='padding: 0 0 20px;'><a href = 'http://localhost:30471/Member/Index' style='text-decoration: none;color: #FFF;background-color: #348eda;border: solid #348eda;border-width: 10px 20px;line-height: 2;font-weight: bold;text-align: center;cursor: pointer;display: inline-block;border-radius: 5px;text-transform: capitalize;'>Rezervasyonları Görüntüle</a></td></tr><tr><td style='padding: 0 0 20px; color:#000;'>SportsSide'ı seçtiğiniz için teşekkür ederiz.</td></tr></table></td></tr></table></td><td></td></tr></table>";
                //mesajim.Body = "Merhaba <b>" + user.USER_FIRST_NAME + "</b> " + user.USER_SURNAME + ", rezervasyonunuz tesis sahibine iletilmiştir. Tesis sahibinden geri dönüş aldığımız an size tekrar mail yoluyla bilgilendirme yapılacaktır. Bizi seçtiğiniz için teşekkür ederiz!";
                object userState = mesajim;
                istemci.Send(mesajim);
                //
            }

            return PartialView("_PartialSubscribe", model);
        }

        [HttpPost]
        public ActionResult CreateSubscribe(int id, string day, string hour, string hourText) {
            string cookieName = FormsAuthentication.FormsCookieName; //Find cookie name
            HttpCookie authCookie = HttpContext.Request.Cookies[cookieName]; //Get the cookie by it's name
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value); //Decrypt it
            string UserName = ticket.Name; //You have the UserName!
            var user = webApi.GetUser(UserName);
            SportsSide.DAL.SUBSCRIBER model = new SportsSide.DAL.SUBSCRIBER();
            model.USER_ID = user.USER_ID;
            model.FACILITY_ID = id;
            model.WEEK_DAY = day;
            TimeSpan ts = new TimeSpan(int.Parse(hour), 0, 0);
            model.S_TIME = ts;
            model.STATUS = "P";
            model.CREATED_DATE = DateTime.Now;
            var result = webApi.PostSubscriber(model);
            //Bilgilendirme Maili
            switch (day)
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
            var facility = webApi.GetFacility(id);
            MailMessage mesajim = new MailMessage();
            SmtpClient istemci = new SmtpClient();

            istemci.Credentials = new System.Net.NetworkCredential("info@sportsside.net", "CunMerMus123");
            istemci.Port = 587;
            istemci.Host = "mail.sportsside.net";
            istemci.EnableSsl = false;
            mesajim.IsBodyHtml = true;
            mesajim.To.Add(user.USER_MAIL);
            mesajim.From = new MailAddress("info@sportsside.net", "SportsSide");
            mesajim.Subject = "Aboneliğiniz İletilmiştir";
            mesajim.Body = "<table style='background-color: #f6f6f6;width: 100%;'><tr><td></td><td style='display: block !important;max-width: 600px !important;margin: 0 auto !important;clear: both !important;' width='600'><div style='max-width: 600px;margin: 0 auto;display: block;padding: 20px;'><table style='background: #fff;border: 1px solid #e9e9e9;border-radius: 3px;' width='100%' cellpadding='0' cellspacing='0'><tr><td style='background: #68b90f;font-size: 16px;color: #fff;font-weight: 500;padding: 20px;text-align: center;border-radius: 3px 3px 0 0;'>Aboneliğiniz iletilmiştir!</td></tr><tr><td style='padding: 20px;'><table width = '100%' cellpadding='0' cellspacing='0'><tr><td style='padding: 0 0 20px; color:#000;'>Merhaba<strong> " + user.USER_FIRST_NAME + " " + user.USER_SURNAME + "</strong> ,</td></tr><tr><td style='padding: 0 0 20px; color:#000;'>Aboneliğiniz tesis sahibine iletilmiştir.Tesis sahibinden geri dönüş aldığımız an size tekrar mail yoluyla bilgilendirme yapılacaktır.</td></tr><tr><td style='padding: 0 0 20px; color:#000;'><strong>Tesis Adı :</strong> " + facility.FACILITY_NAME + " <br><strong>Abonelik Günü :</strong> " + day.ToString() + " <br><strong>Abonelik Saati :</strong> " + hourText.ToString() + " <br><strong>Oluşturulma Tarihi :</strong> " + System.DateTime.Now.ToString("dd/MM/yyyy H:mm") + " <br><strong>Tesis Adresi :</strong> " + facility.FACILITY_ADDRESS + "<br></td></tr><tr><td style='padding: 0 0 20px;'><a href = 'http://localhost:30471/Member/Index' style='text-decoration: none;color: #FFF;background-color: #348eda;border: solid #348eda;border-width: 10px 20px;line-height: 2;font-weight: bold;text-align: center;cursor: pointer;display: inline-block;border-radius: 5px;text-transform: capitalize;'>Rezervasyonları Görüntüle</a></td></tr><tr><td style='padding: 0 0 20px; color:#000;'>SportsSide'ı seçtiğiniz için teşekkür ederiz.</td></tr></table></td></tr></table></td><td></td></tr></table>";
            //mesajim.Body = "Merhaba <b>" + user.USER_FIRST_NAME + "</b> " + user.USER_SURNAME + ", rezervasyonunuz tesis sahibine iletilmiştir. Tesis sahibinden geri dönüş aldığımız an size tekrar mail yoluyla bilgilendirme yapılacaktır. Bizi seçtiğiniz için teşekkür ederiz!";
            object userState = mesajim;
            istemci.Send(mesajim);
            //
            //if (result == false)
            //{
            //    TempData["Hata"] = "Bir hata oluştu.";
            //    return RedirectToAction("Index", "Reservation", new { @id = model.Facility.FACILITY_ID });
            //}
            ReservationBigModel bigModel = new ReservationBigModel();
            bigModel.isProblem = "N";

            #region FillDays

            List<SelectListItem> weekDays = new List<SelectListItem>();
            SelectListItem _dList = new SelectListItem();
            _dList = new SelectListItem() { Text = "Pazartesi", Value = "Monday" };
            weekDays.Add(_dList);
            _dList = new SelectListItem() { Text = "Salı", Value = "Tuesday" };
            weekDays.Add(_dList);
            _dList = new SelectListItem() { Text = "Çarşamba", Value = "Wednesday" };
            weekDays.Add(_dList);
            _dList = new SelectListItem() { Text = "Perşembe", Value = "Thursday" };
            weekDays.Add(_dList);
            _dList = new SelectListItem() { Text = "Cuma", Value = "Friday" };
            weekDays.Add(_dList);
            _dList = new SelectListItem() { Text = "Cumartesi", Value = "Saturday" };
            weekDays.Add(_dList);
            _dList = new SelectListItem() { Text = "Pazar", Value = "Sunday" };
            weekDays.Add(_dList);

            #endregion

            #region FillHours

            List<SelectListItem> Hours = new List<SelectListItem>();
            SelectListItem _hList = new SelectListItem();
            for (int i = 0; i < 24; i++)
            {
                string Hour = "";
                string nextHour = "";
                if (i < 10)
                {
                    Hour = "0" + i.ToString();
                    nextHour = (i == 9) ? "10" : "0" + (i + 1).ToString();
                }
                else
                {
                    Hour = i.ToString();
                    nextHour = (i + 1).ToString();
                }

                _hList = new SelectListItem() { Text = Hour + ":00 - " + nextHour + ":00", Value = i.ToString() };
                Hours.Add(_hList);
            }

            #endregion

            ViewBag.Days = new SelectList(weekDays, "Value", "Text");
            ViewBag.Hours = new SelectList(Hours, "Value", "Text");

            return PartialView("_PartialSubscribe", bigModel);
        }

        [HttpGet]
        public ActionResult RefreshSubscribe(int id) {
            ReservationBigModel model = new ReservationBigModel();
            var facility = webApi.GetFacility(id);
            var reservations = webApi.GetFacilityReservations(id);
            var subscribers = webApi.GetFacilitySubscribersForSubscribe(id);
            var facilityPictures = webApi.GetPicturesFromFacility(id);
            model.Facility = facility;
            model.isProblem = "";
            //model.Reservations = reservations;
            model.Subscribers = subscribers;
            model.FacilityPictures = facilityPictures;
            #region FillDays

            List<SelectListItem> weekDays = new List<SelectListItem>();
            SelectListItem _dList = new SelectListItem();
            _dList = new SelectListItem() { Text = "Pazartesi", Value = "Monday" };
            weekDays.Add(_dList);
            _dList = new SelectListItem() { Text = "Salı", Value = "Tuesday" };
            weekDays.Add(_dList);
            _dList = new SelectListItem() { Text = "Çarşamba", Value = "Wednesday" };
            weekDays.Add(_dList);
            _dList = new SelectListItem() { Text = "Perşembe", Value = "Thursday" };
            weekDays.Add(_dList);
            _dList = new SelectListItem() { Text = "Cuma", Value = "Friday" };
            weekDays.Add(_dList);
            _dList = new SelectListItem() { Text = "Cumartesi", Value = "Saturday" };
            weekDays.Add(_dList);
            _dList = new SelectListItem() { Text = "Pazar", Value = "Sunday" };
            weekDays.Add(_dList);

            #endregion

            #region FillHours

            List<SelectListItem> Hours = new List<SelectListItem>();
            SelectListItem _hList = new SelectListItem();
            for (int i = 0; i < 24; i++)
            {
                string Hour = "";
                string nextHour = "";
                if (i < 10)
                {
                    Hour = "0" + i.ToString();
                    nextHour = (i == 9) ? "10" : "0" + (i + 1).ToString();
                }
                else
                {
                    Hour = i.ToString();
                    nextHour = (i + 1).ToString();
                }

                _hList = new SelectListItem() { Text = Hour + ":00 - " + nextHour + ":00", Value = i.ToString() };
                Hours.Add(_hList);
            }

            #endregion

            ViewBag.Days = new SelectList(weekDays, "Value", "Text");
            ViewBag.Hours = new SelectList(Hours, "Value", "Text");

            return PartialView("_PartialSubscribe", model);
        }


    }
}