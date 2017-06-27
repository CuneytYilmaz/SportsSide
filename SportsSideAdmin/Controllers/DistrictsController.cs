using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace SportsSideAdmin.Controllers
{
    [Authorize]
    public class DistrictsController : Controller
    {
        SportsSideWebApi.Controllers.SportsSideApiController webApi = new SportsSideWebApi.Controllers.SportsSideApiController();
        // GET: Districts
        public ActionResult Index(int? page)
        {
            string role = new Business.AccountBusiness().currentMember();
            if (role == "U" || role == "A")
            {
                return RedirectToAction("Login", "Account");
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            var districts = webApi.GetDistricts().ToPagedList(pageNumber, pageSize);
            return View(districts);
        }

        public ActionResult Create()
        {
            string role = new Business.AccountBusiness().currentMember();
            if (role == "U" || role == "A")
            {
                return RedirectToAction("Login", "Account");
            }
            ViewBag.Areas = new SelectList(webApi.GetAreas(), "AREA_ID", "AREA_NAME");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SportsSide.DAL.DISTRICT model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = webApi.PostDistrict(model);
            if (result == false)
            {
                TempData["Hata"] = "Bir hata oluştu.";
                return View(model);
            }
            return RedirectToAction("Index", "Districts");
        }

        public ActionResult Edit(int id)
        {
            string role = new Business.AccountBusiness().currentMember();
            if (role == "U" || role == "A")
            {
                return RedirectToAction("Login", "Account");
            }
            var district = webApi.GetDistrict(id);
            ViewBag.Areas = new SelectList(webApi.GetAreas(), "AREA_ID", "AREA_NAME", district.CITY.AREA.AREA_ID);
            ViewBag.Cities = new SelectList(webApi.GetCitiesByAreas(district.CITY.AREA.AREA_ID), "CITY_ID", "CITY_NAME");
            return View(district);
        }

        [HttpPost]
        public ActionResult Edit(SportsSide.DAL.DISTRICT model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = webApi.PutDistrict(model);
            if (result == false)
            {
                TempData["Hata"] = "Bir hata oluştu.";
                ViewBag.Areas = new SelectList(webApi.GetAreas(), "AREA_ID", "AREA_NAME");
                ViewBag.Cities = new SelectList(webApi.GetCitiesByAreas(model.CITY.AREA.AREA_ID), "CITY_ID", "CITY_NAME");
                return View(model);
            }
            return RedirectToAction("Index", "Districts");
        }

        public ActionResult Delete(int id)
        {
            string role = new Business.AccountBusiness().currentMember();
            if (role == "U" || role == "A")
            {
                return RedirectToAction("Login", "Account");
            }
            var district = webApi.GetDistrict(id);
            return View(district);
        }

        [HttpPost]
        public ActionResult Delete(SportsSide.DAL.DISTRICT district)
        {
            var result = webApi.DeleteDistrict(district);
            if (result == false)
            {
                var oldDistrict = webApi.GetDistrict(district.DISTRICT_ID);
                TempData["Hata"] = "Bir hata oluştu.";
                return View(oldDistrict);
            }
            return RedirectToAction("Index", "Districts");
        }

        public ActionResult Details(int id) {
            string role = new Business.AccountBusiness().currentMember();
            if (role == "U" || role == "A")
            {
                return RedirectToAction("Login", "Account");
            }
            var district = webApi.GetDistrict(id);
            return View(district);
        }
    }
}