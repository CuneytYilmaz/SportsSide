using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace SportsSideAdmin.Controllers
{
    [Authorize]
    public class CitiesController : Controller
    {
        SportsSideWebApi.Controllers.SportsSideApiController webApi = new SportsSideWebApi.Controllers.SportsSideApiController();
        // GET: Cities
        public ActionResult Index(int? page)
        {
            string role = new Business.AccountBusiness().currentMember();
            if (role == "U" || role == "A")
            {
                return RedirectToAction("Login", "Account");
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            var cities = webApi.GetCities().ToPagedList(pageNumber, pageSize);
            return View(cities);
        }

        public ActionResult Create() {
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
        public ActionResult Create(SportsSide.DAL.CITY model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = webApi.PostCity(model);
            if (result == false)
            {
                TempData["Hata"] = "Bir hata oluştu.";
                return View(model);
            }
            return RedirectToAction("Index", "Cities");
        }

        public ActionResult Edit(int id)
        {
            string role = new Business.AccountBusiness().currentMember();
            if (role == "U" || role == "A")
            {
                return RedirectToAction("Login", "Account");
            }
            var city = webApi.GetCity(id);
            ViewBag.Areas = new SelectList(webApi.GetAreas(), "AREA_ID", "AREA_NAME");
            return View(city);
        }

        [HttpPost]
        public ActionResult Edit(SportsSide.DAL.CITY model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = webApi.PutCity(model);
            if (result == false)
            {
                TempData["Hata"] = "Bir hata oluştu.";
                ViewBag.Areas = new SelectList(webApi.GetAreas(), "AREA_ID", "AREA_NAME");
                return View(model);
            }
            return RedirectToAction("Index", "Cities");
        }

        public ActionResult Delete(int id)
        {
            string role = new Business.AccountBusiness().currentMember();
            if (role == "U" || role == "A")
            {
                return RedirectToAction("Login", "Account");
            }
            var city = webApi.GetCity(id);
            return View(city);
        }

        [HttpPost]
        public ActionResult Delete(SportsSide.DAL.CITY city)
        {
            var result = webApi.DeleteCity(city);
            if (result == false)
            {
                TempData["Hata"] = "Bir hata oluştu.";
                return View(city);
            }
            return RedirectToAction("Index", "Cities");
        }
    }
}