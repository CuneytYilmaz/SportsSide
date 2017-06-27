using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SportsSideAdmin.Models;
using PagedList;

namespace SportsSideAdmin.Controllers
{
    [Authorize]
    public class FacilitiesController : Controller
    {
        SportsSideWebApi.Controllers.SportsSideApiController webApi = new SportsSideWebApi.Controllers.SportsSideApiController();
        public static List<string> picturesPath = new List<string>();

        // GET: Facilities
        public ActionResult Index(int? page)
        {
            string role = new Business.AccountBusiness().currentMember();
            if (role == "U")
            {
                return RedirectToAction("Login", "Account");
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            picturesPath.Clear();
            string cookieName = FormsAuthentication.FormsCookieName; //Find cookie name
            HttpCookie authCookie = HttpContext.Request.Cookies[cookieName]; //Get the cookie by it's name
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value); //Decrypt it
            string UserName = ticket.Name; //You have the UserName!
            var user = webApi.GetUser(UserName);
            var facilities = webApi.GetFacilitiesByUserId(user.USER_ID).ToPagedList(pageNumber, pageSize);
            return View(facilities);
        }

        public ActionResult GetPictures()
        {
            if (Request.Files.Count > 0)
            {
                foreach (string file in Request.Files)
                {
                    string saveAsString;
                    HttpPostedFileBase httpPostedFileBase = Request.Files[file];
                    string fullPath = "/img/FacilityPictures/" + httpPostedFileBase.FileName;
                    picturesPath.Add(fullPath);
                    saveAsString = Path.Combine(Server.MapPath("~/img/FacilityPictures"), httpPostedFileBase.FileName);
                    httpPostedFileBase.SaveAs(saveAsString);
                    saveAsString = saveAsString.Replace("SportsSideAdmin", "sahakirala");
                    httpPostedFileBase.SaveAs(saveAsString);
                }
            }
            if (TempData["pageName"].ToString() == "Create")
            {
                return RedirectToAction("Create", "Facilities");
            }
            else if (TempData["pageName"].ToString() == "Edit")
            {
                return RedirectToAction("Edit", "Facilities", new { @id = int.Parse(TempData["facilityId"].ToString()), @isRedirected = true });
            }
            return RedirectToAction("Index", "Facilities");
        }

        public ActionResult DeletePicture(string imgPath)
        {
            string fullPath = "/img/FacilityPictures/" + imgPath;
            picturesPath.Remove(fullPath);

            if (TempData["pageName"].ToString() == "Create")
            {
                return RedirectToAction("Create", "Facilities");
            }
            else if (TempData["pageName"].ToString() == "Edit")
            {
                return RedirectToAction("Edit", "Facilities", new { @id = int.Parse(TempData["facilityId"].ToString()), @isRedirected = true });
            }
            return RedirectToAction("Index", "Facilities");
        }

        public ActionResult Create()
        {
            string role = new Business.AccountBusiness().currentMember();
            if (role == "U")
            {
                return RedirectToAction("Login", "Account");
            }
            TempData["pageName"] = "Create";
            ViewBag.Areas = new SelectList(webApi.GetAreas(), "AREA_ID", "AREA_NAME");
            ViewBag.FacilityTypes = new SelectList(webApi.GetFacilityTypes(), "FT_ID", "FT_NAME");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SportsSide.DAL.FACILITY model)
        {
            string cookieName = FormsAuthentication.FormsCookieName; //Find cookie name
            HttpCookie authCookie = HttpContext.Request.Cookies[cookieName]; //Get the cookie by it's name
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value); //Decrypt it
            string UserName = ticket.Name; //You have the UserName!
            var user = webApi.GetUser(UserName);
            model.USER_ID = user.USER_ID;
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = webApi.PostFacility(model);
            if (result == false)
            {
                TempData["Hata"] = "Bir hata oluştu.";
                return View(model);
            }
            SportsSide.DAL.FACILITY_PICTURES pictureModel = new SportsSide.DAL.FACILITY_PICTURES();
            pictureModel.F_ID = model.FACILITY_ID;
            foreach (var path in picturesPath)
            {
                pictureModel.FP_PICTURE = path;
                result = webApi.PostFacilityPicture(pictureModel);
                if (result == false)
                {
                    TempData["Hata"] = "Bir hata oluştu.";
                    return View(model);
                }
            }
            if (picturesPath.Count == 0)
            {
                pictureModel.FP_PICTURE = "/img/FacilityPictures/no-photo.gif";
                result = webApi.PostFacilityPicture(pictureModel);
                if (result == false)
                {
                    TempData["Hata"] = "Bir hata oluştu";
                    return View(model);
                }
            }
            return RedirectToAction("Index", "Facilities");
        }

        public ActionResult Edit(int id, bool? isRedirected)
        {
            string role = new Business.AccountBusiness().currentMember();
            if (role == "U")
            {
                return RedirectToAction("Login", "Account");
            }
            TempData["pageName"] = "Edit";
            TempData["facilityId"] = id;
            var facility = webApi.GetFacility(id);
            ViewBag.Areas = new SelectList(webApi.GetAreas(), "AREA_ID", "AREA_NAME", facility.DISTRICT.CITY.AREA.AREA_ID);
            ViewBag.Cities = new SelectList(webApi.GetCitiesByAreas(facility.DISTRICT.CITY.AREA.AREA_ID), "CITY_ID", "CITY_NAME", facility.DISTRICT.CITY.CITY_ID);
            ViewBag.Districts = new SelectList(webApi.GetDistrictsByCities(facility.DISTRICT.CITY.CITY_ID), "DISTRICT_ID", "DISTRICT_NAME");
            ViewBag.FacilityTypes = new SelectList(webApi.GetFacilityTypes(), "FT_ID", "FT_NAME", facility.FACILITY_TYPE);

            if (isRedirected != true)
            {
                var facilityPictures = webApi.GetPicturesFromFacility(id);
                foreach (var item in facilityPictures)
                {
                    if (item.FP_PICTURE == "/img/FacilityPictures/no-photo.gif")
                    {
                        continue;
                    }
                    picturesPath.Add(item.FP_PICTURE);
                }
            }
            return View(facility);
        }

        [HttpPost]
        public ActionResult Edit(SportsSide.DAL.FACILITY model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = webApi.PutFacility(model);
            if (result == false)
            {
                TempData["Hata"] = "Bir hata oluştu.";
                var facility = webApi.GetFacility(model.FACILITY_ID);
                ViewBag.Areas = new SelectList(webApi.GetAreas(), "AREA_ID", "AREA_NAME", facility.DISTRICT.CITY.AREA.AREA_ID);
                ViewBag.Cities = new SelectList(webApi.GetCitiesByAreas(facility.DISTRICT.CITY.AREA.AREA_ID), "CITY_ID", "CITY_NAME");
                ViewBag.Districts = new SelectList(webApi.GetDistrictsByCities(facility.DISTRICT.CITY.CITY_ID), "DISTRICT_ID", "DISTRICT_NAME");
                ViewBag.FacilityTypes = new SelectList(webApi.GetFacilityTypes(), "FT_ID", "FT_NAME", facility.FACILITY_TYPE);
                return View(model);
            }

            SportsSide.DAL.FACILITY_PICTURES pictureModel = new SportsSide.DAL.FACILITY_PICTURES();
            pictureModel.F_ID = model.FACILITY_ID;
            webApi.DeleteAllFacilityPicturesFromId(model.FACILITY_ID);
            foreach (var path in picturesPath)
            {
                pictureModel.FP_PICTURE = path;
                result = webApi.PostFacilityPicture(pictureModel);
                if (result == false)
                {
                    TempData["Hata"] = "Bir hata oluştu.";
                    return View(model);
                }
            }
            if (picturesPath.Count == 0)
            {
                pictureModel.FP_PICTURE = "/img/FacilityPictures/no-photo.gif";
                result = webApi.PostFacilityPicture(pictureModel);
                if (result == false)
                {
                    TempData["Hata"] = "Bir hata oluştu";
                    return View(model);
                }
            }
            return RedirectToAction("Index", "Facilities");
        }

        public ActionResult GetFacilityPictures()
        {
            List<FacilityPictures> picturesList = new List<FacilityPictures>();
            foreach (var item in picturesPath)
            {
                FacilityPictures model = new FacilityPictures();
                string[] split = item.Split('/');
                model.name = split[split.Length - 1];
                model.path = item;
                FileInfo file = new FileInfo(Path.Combine(Server.MapPath("~/img/FacilityPictures"), model.name));
                model.size = file.Length;
                picturesList.Add(model);
            }
            return Json(new { Data = picturesList }, JsonRequestBehavior.AllowGet); ;
        }

        public ActionResult Details(int id)
        {
            string role = new Business.AccountBusiness().currentMember();
            if (role == "U")
            {
                return RedirectToAction("Login", "Account");
            }
            var facility = webApi.GetFacility(id);
            return View(facility);
        }

        public ActionResult Delete(int id)
        {
            string role = new Business.AccountBusiness().currentMember();
            if (role == "U")
            {
                return RedirectToAction("Login", "Account");
            }
            var facility = webApi.GetFacility(id);
            return View(facility);
        }


        [HttpPost]
        public ActionResult Delete(SportsSide.DAL.FACILITY facility)
        {
            webApi.DeleteAllFacilityPicturesFromId(facility.FACILITY_ID);
            var result = webApi.DeleteFacility(facility);
            if (result == false)
            {
                var oldfacility = webApi.GetFacility(facility.FACILITY_ID);
                TempData["Hata"] = "Bir hata oluştu.";
                return View(oldfacility);
            }
            return RedirectToAction("Index", "Facilities");
        }
    }
}