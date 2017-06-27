using Database;
using sahakirala.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PagedList;

namespace sahakirala.Controllers
{
    public class MemberController : Controller
    {
        SportsSideWebApi.Controllers.SportsSideApiController webApi = new SportsSideWebApi.Controllers.SportsSideApiController();

        // GET: Member
        public ActionResult Index(int? pageActive, int? pageHistory, int? pageSub, int? selectedTab)
        {
            string role = new Business.AccountBusiness().currentMember();
            if (role != "U" && role != "A" && role != "O")
            {
                return RedirectToAction("Login", "Account");
            }
            if (selectedTab != null)
            {
                TempData["selectedTab"] = selectedTab;
            }
            int pageSize = 10;
            int pageNumberActive = (pageActive ?? 1);
            int pageNumberHistory = (pageHistory ?? 1);
            int pageNumberSub = (pageSub ?? 1);
            TempData["selectedTab"] = TempData["selectedTab"] == null ? 0 : TempData["selectedTab"];

            DatabaseEntities db = new DatabaseEntities();

            string cookieName = FormsAuthentication.FormsCookieName;
            HttpCookie authCookie = HttpContext.Request.Cookies[cookieName];
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
            string userName = ticket.Name;
            var user = webApi.GetUser(userName);
            var liveReservations = webApi.liveReservations(user.USER_ID);
            var subscribers = webApi.GetSubscribersByUserId(user.USER_ID);
            var oldReservations = webApi.oldReservations(user.USER_ID);
            List<SportsSide.DAL.RESERVATION> newLiveReservations = new List<SportsSide.DAL.RESERVATION>();
            List<SportsSide.DAL.RESERVATION> newOldReservations = new List<SportsSide.DAL.RESERVATION>();
            foreach (var item in liveReservations)
            {
                if (item.DT_BEGIN.Date > DateTime.Now.Date || (item.DT_BEGIN.Date == DateTime.Now.Date && item.DT_BEGIN.Hour > DateTime.Now.Hour))
                {
                    newLiveReservations.Add(item);
                }
            }
            foreach (var item in oldReservations)
            {
                if (item.DT_BEGIN.Date < DateTime.Now.Date || (item.DT_BEGIN.Date == DateTime.Now.Date && item.DT_BEGIN.Hour < DateTime.Now.Hour))
                {
                    newOldReservations.Add(item);
                }
            }
            MemberBigModel model = new MemberBigModel();
            model.User = user;
            model.LiveReservations = newLiveReservations.OrderByDescending(x => x.CREATED_DATE).ToPagedList(pageNumberActive, pageSize);
            model.oldReservations = newOldReservations.OrderByDescending(x => x.CREATED_DATE).ToPagedList(pageNumberHistory, pageSize);
            model.Subscribers = subscribers.OrderByDescending(x => x.CREATED_DATE).ToPagedList(pageNumberSub, pageSize);
            model.selectedTab = TempData["selectedTab"].ToString();

            model.Date = System.DateTime.Now.Date;


            ViewBag.Areas = new SelectList(webApi.GetAreas(), "AREA_ID", "AREA_NAME", user.DISTRICT.CITY.AREA.AREA_ID);
            ViewBag.Cities = new SelectList(webApi.GetCitiesByAreas(user.DISTRICT.CITY.AREA.AREA_ID), "CITY_ID", "CITY_NAME");
            ViewBag.Districts = new SelectList(webApi.GetDistrictsByCities(user.DISTRICT.CITY.CITY_ID), "DISTRICT_ID", "DISTRICT_NAME");

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(MemberBigModel model)
        {
            var result = false;
            int pageSize = 10;
            int pageNumberActive = 1;
            int pageNumberHistory = 1;
            int pageNumberSub = 1;
            if (!ModelState.IsValid)
            {
                result = webApi.PutUser(model.User);
                if (result == false)
                {
                    TempData["Hata"] = "Bir hata oluştu.";
                    var user1 = webApi.GetUser(model.User.USER_ID);
                    ViewBag.Areas = new SelectList(webApi.GetAreas(), "AREA_ID", "AREA_NAME", user1.DISTRICT.CITY.AREA.AREA_ID);
                    ViewBag.Cities = new SelectList(webApi.GetCitiesByAreas(user1.DISTRICT.CITY.AREA.AREA_ID), "CITY_ID", "CITY_NAME");
                    ViewBag.Districts = new SelectList(webApi.GetDistrictsByCities(user1.DISTRICT.CITY.CITY_ID), "DISTRICT_ID", "DISTRICT_NAME");

                    var liveReservations = webApi.liveReservations(user1.USER_ID);
                    var subscribers = webApi.GetSubscribersByUserId(user1.USER_ID);
                    var oldReservations = webApi.oldReservations(user1.USER_ID);
                    List<SportsSide.DAL.RESERVATION> newLiveReservations = new List<SportsSide.DAL.RESERVATION>();
                    List<SportsSide.DAL.RESERVATION> newOldReservations = new List<SportsSide.DAL.RESERVATION>();
                    foreach (var item in liveReservations)
                    {
                        if (item.DT_BEGIN.Date > DateTime.Now.Date || (item.DT_BEGIN.Date == DateTime.Now.Date && item.DT_BEGIN.Hour > DateTime.Now.Hour))
                        {
                            newLiveReservations.Add(item);
                        }
                    }
                    foreach (var item in oldReservations)
                    {
                        if (item.DT_BEGIN.Date < DateTime.Now.Date || (item.DT_BEGIN.Date == DateTime.Now.Date && item.DT_BEGIN.Hour < DateTime.Now.Hour))
                        {
                            newOldReservations.Add(item);
                        }
                    }
                    MemberBigModel bigModel = new MemberBigModel();
                    model.User = user1;
                    model.LiveReservations = newLiveReservations.OrderByDescending(x => x.CREATED_DATE).ToPagedList(pageNumberActive, pageSize);
                    model.oldReservations = newOldReservations.OrderByDescending(x => x.CREATED_DATE).ToPagedList(pageNumberHistory, pageSize);
                    model.Subscribers = subscribers.OrderByDescending(x => x.CREATED_DATE).ToPagedList(pageNumberSub, pageSize);
                    model.selectedTab = "1";

                    model.Date = System.DateTime.Now.Date;

                    ViewBag.Areas = new SelectList(webApi.GetAreas(), "AREA_ID", "AREA_NAME", user1.DISTRICT.CITY.AREA.AREA_ID);
                    ViewBag.Cities = new SelectList(webApi.GetCitiesByAreas(user1.DISTRICT.CITY.AREA.AREA_ID), "CITY_ID", "CITY_NAME");
                    ViewBag.Districts = new SelectList(webApi.GetDistrictsByCities(user1.DISTRICT.CITY.CITY_ID), "DISTRICT_ID", "DISTRICT_NAME");

                    return View(model);
                }
                return RedirectToAction("Index", "Arama");
            }
            else
            {
                TempData["Hata"] = "Bir hata oluştu.";
                var user1 = webApi.GetUser(model.User.USER_ID);
                ViewBag.Areas = new SelectList(webApi.GetAreas(), "AREA_ID", "AREA_NAME", user1.DISTRICT.CITY.AREA.AREA_ID);
                ViewBag.Cities = new SelectList(webApi.GetCitiesByAreas(user1.DISTRICT.CITY.AREA.AREA_ID), "CITY_ID", "CITY_NAME");
                ViewBag.Districts = new SelectList(webApi.GetDistrictsByCities(user1.DISTRICT.CITY.CITY_ID), "DISTRICT_ID", "DISTRICT_NAME");

                var liveReservations = webApi.liveReservations(user1.USER_ID);
                var subscribers = webApi.GetSubscribersByUserId(user1.USER_ID);
                var oldReservations = webApi.oldReservations(user1.USER_ID);
                List<SportsSide.DAL.RESERVATION> newLiveReservations = new List<SportsSide.DAL.RESERVATION>();
                List<SportsSide.DAL.RESERVATION> newOldReservations = new List<SportsSide.DAL.RESERVATION>();
                foreach (var item in liveReservations)
                {
                    if (item.DT_BEGIN.Date > DateTime.Now.Date || (item.DT_BEGIN.Date == DateTime.Now.Date && item.DT_BEGIN.Hour > DateTime.Now.Hour))
                    {
                        newLiveReservations.Add(item);
                    }
                }
                foreach (var item in oldReservations)
                {
                    if (item.DT_BEGIN.Date < DateTime.Now.Date || (item.DT_BEGIN.Date == DateTime.Now.Date && item.DT_BEGIN.Hour < DateTime.Now.Hour))
                    {
                        newOldReservations.Add(item);
                    }
                }
                MemberBigModel bigModel = new MemberBigModel();
                model.User = user1;
                model.LiveReservations = newLiveReservations.OrderByDescending(x => x.CREATED_DATE).ToPagedList(pageNumberActive, pageSize);
                model.oldReservations = newOldReservations.OrderByDescending(x => x.CREATED_DATE).ToPagedList(pageNumberHistory, pageSize);
                model.Subscribers = subscribers.OrderByDescending(x => x.CREATED_DATE).ToPagedList(pageNumberSub, pageSize);
                model.selectedTab = "1";

                model.Date = System.DateTime.Now.Date;

                ViewBag.Areas = new SelectList(webApi.GetAreas(), "AREA_ID", "AREA_NAME", user1.DISTRICT.CITY.AREA.AREA_ID);
                ViewBag.Cities = new SelectList(webApi.GetCitiesByAreas(user1.DISTRICT.CITY.AREA.AREA_ID), "CITY_ID", "CITY_NAME");
                ViewBag.Districts = new SelectList(webApi.GetDistrictsByCities(user1.DISTRICT.CITY.CITY_ID), "DISTRICT_ID", "DISTRICT_NAME");

                return View(model);
            }
        }
        [HttpGet]
        public bool RejectSubscriber(int id)
        {
            SportsSide.DAL.SUBSCRIBER model = new SportsSide.DAL.SUBSCRIBER();
            model = webApi.GetSubscriberFromSubscriberId(id);
            model.STATUS = "R";
            var result = webApi.PutSubscriber(model);
            return result;
        }

        [HttpGet]
        public bool RejectReservation(int id)
        {
            SportsSide.DAL.RESERVATION model = new SportsSide.DAL.RESERVATION();
            model = webApi.GetReservationFromReservationId(id);
            model.STATUS = "R";
            var result = webApi.PutReservation(model);
            return result;
        }
    }
}