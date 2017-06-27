using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SportsSideAdmin.Models;
using PagedList;

namespace SportsSideAdmin.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        SportsSideWebApi.Controllers.SportsSideApiController webApi = new SportsSideWebApi.Controllers.SportsSideApiController();
        // GET: Dashboard
        public ActionResult Index(int? page)
        {
            string role = new Business.AccountBusiness().currentMember();
            if (role == "U")
            {
                return RedirectToAction("Login", "Account");
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            string cookieName = FormsAuthentication.FormsCookieName; //Find cookie name
            HttpCookie authCookie = HttpContext.Request.Cookies[cookieName]; //Get the cookie by it's name
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value); //Decrypt it
            string UserName = ticket.Name; //You have the UserName!

            DashboardModel model = new DashboardModel();
            model.countFacility = webApi.CountFacility(UserName);
            model.countApprovedReservation = webApi.CountApprovedReservation(UserName);
            model.countSubscriber = webApi.CountSubscriber(UserName);
            model.countVisitor = webApi.CountVisitor(UserName);
            model.Facilities = webApi.GetFacilitiesByUserName(UserName).ToPagedList(pageNumber, pageSize);
            model.percentFacility = webApi.PercentFacility(UserName);
            model.percentReservation = webApi.PercentReservation(UserName);
            model.percentSubscriber = webApi.PercentSubscriber(UserName);
            model.todayReservation = webApi.TodayReservation(UserName);
            model.todaySubscriber = webApi.TodaySubscriber(UserName);
            model.GroupByMonths = webApi.GroupByMonths(UserName);
            model.GroupByDays = webApi.GroupByDays(UserName);

            return View(model);
        }
    }
}