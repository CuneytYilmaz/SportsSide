using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace SportsSideAdmin.Controllers
{
    [Authorize]
    public class AreasController : Controller
    {
        SportsSideWebApi.Controllers.SportsSideApiController webApi = new SportsSideWebApi.Controllers.SportsSideApiController();
        // GET: Areas
        public ActionResult Index(int? page)
        {
            string role = new Business.AccountBusiness().currentMember();
            if (role == "U" || role == "A")
            {
                return RedirectToAction("Login", "Account");
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            var areas = webApi.GetAreas().ToPagedList(pageNumber,pageSize);
            return View(areas);
        }

        public ActionResult Create() {
            string role = new Business.AccountBusiness().currentMember();
            if (role == "U" || role == "A")
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SportsSide.DAL.AREA model) {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = webApi.PostArea(model);
            if (result == false)
            {
                TempData["Hata"] = "Bir hata oluştu.";
                return View(model);
            }
            return RedirectToAction("Index", "Areas");
        }

        public ActionResult Edit(int id) {
            string role = new Business.AccountBusiness().currentMember();
            if (role == "U" || role == "A")
            {
                return RedirectToAction("Login", "Account");
            }
            var area = webApi.GetArea(id);
            return View(area);
        }

        [HttpPost]
        public ActionResult Edit(SportsSide.DAL.AREA model) {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = webApi.PutArea(model);
            if (result == false)
            {
                TempData["Hata"] = "Bir hata oluştu.";
                return View(model);
            }
            return RedirectToAction("Index", "Areas");
        }

        public ActionResult Delete(int id) {
            string role = new Business.AccountBusiness().currentMember();
            if (role == "U" || role == "A")
            {
                return RedirectToAction("Login", "Account");
            }
            var area = webApi.GetArea(id);
            return View(area);
        }

        [HttpPost]
        public ActionResult Delete(SportsSide.DAL.AREA area) {
            var result = webApi.DeleteArea(area);
            if (result == false)
            {
                TempData["Hata"] = "Bir hata oluştu.";
                return View(area);
            }
            return RedirectToAction("Index", "Areas");
        }
    }
}