using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace SportsSideAdmin.Controllers
{
    [Authorize]
    public class AnnouncementsController : Controller
    {
        SportsSideWebApi.Controllers.SportsSideApiController webApi = new SportsSideWebApi.Controllers.SportsSideApiController();
        // GET: Announcements
        public ActionResult Index(int? page)
        {
            string role = new Business.AccountBusiness().currentMember();
            if (role == "U" || role == "A")
            {
                return RedirectToAction("Login", "Account");
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            var announcements = webApi.GetAllAnnouncements().ToPagedList(pageNumber, pageSize);
            return View(announcements);
        }

        public ActionResult Create()
        {
            string role = new Business.AccountBusiness().currentMember();
            if (role == "U" || role == "A")
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SportsSide.DAL.ANNOUNCEMENTS model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = webApi.PostAnnouncements(model);
            if (result == false)
            {
                TempData["Hata"] = "Bir hata oluştu.";
                return View(model);
            }
            return RedirectToAction("Index", "Announcements");
        }

        public ActionResult Edit(int id)
        {
            string role = new Business.AccountBusiness().currentMember();
            if (role == "U" || role == "A")
            {
                return RedirectToAction("Login", "Account");
            }
            var announcement = webApi.GetAnnouncementById(id);
            return View(announcement);
        }

        [HttpPost]
        public ActionResult Edit(SportsSide.DAL.ANNOUNCEMENTS model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = webApi.PutAnnouncements(model);
            if (result == false)
            {
                TempData["Hata"] = "Bir hata oluştu.";
                return View(model);
            }
            return RedirectToAction("Index", "Announcements");
        }

        public ActionResult Delete(int id)
        {
            string role = new Business.AccountBusiness().currentMember();
            if (role == "U" || role == "A")
            {
                return RedirectToAction("Login", "Account");
            }
            var announcement = webApi.GetAnnouncementById(id);
            return View(announcement);
        }

        [HttpPost]
        public ActionResult Delete(SportsSide.DAL.ANNOUNCEMENTS announcement)
        {
            var result = webApi.DeleteAnnouncements(announcement);
            if (result == false)
            {
                TempData["Hata"] = "Bir hata oluştu.";
                return View(announcement);
            }
            return RedirectToAction("Index", "Announcements");
        }
    }
}