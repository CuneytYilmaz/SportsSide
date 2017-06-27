using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace SportsSideAdmin.Controllers
{
    [Authorize]
    public class FacilityTypesController : Controller
    {
        SportsSideWebApi.Controllers.SportsSideApiController webApi = new SportsSideWebApi.Controllers.SportsSideApiController();
        // GET: FacilityTypes
        public ActionResult Index(int? page)
        {
            string role = new Business.AccountBusiness().currentMember();
            if (role == "U" || role == "A")
            {
                return RedirectToAction("Login", "Account");
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            var facilityTypes = webApi.GetFacilityTypes().ToPagedList(pageNumber, pageSize);
            return View(facilityTypes);
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
        public ActionResult Create(HttpPostedFileBase file, string ftName)
        {
            var path = "";
            var ftPicture = "";
            SportsSide.DAL.FACILITY_TYPE model = new SportsSide.DAL.FACILITY_TYPE();
            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    if ((Path.GetExtension(file.FileName).ToLower() == ".jpg") || (Path.GetExtension(file.FileName).ToLower() == ".png") ||
                        (Path.GetExtension(file.FileName).ToLower() == ".jpeg"))
                    {
                        path = Path.Combine(Server.MapPath("~/img/FacilityTypes"), file.FileName);
                        ftPicture = "/img/FacilityTypes/" + file.FileName;
                        model.FT_NAME = ftName;
                        model.FT_PICTURE = ftPicture;
                        var result = webApi.PostFacilityType(model);
                        if (result == false)
                        {
                            TempData["Hata"] = "Bir hata oluştu.";
                            return View();
                        }
                        file.SaveAs(path);
                        path = path.Replace("SportSideAdmin", "sahakirala");
                        file.SaveAs(path);
                        return RedirectToAction("Index", "FacilityTypes");
                    }
                }
            }
            else
            {
                model.FT_NAME = ftName;
                model.FT_PICTURE = ftPicture;
                var result = webApi.PostFacilityType(model);
                if (result == false)
                {
                    TempData["Hata"] = "Bir hata oluştu.";
                    return View();
                }
                return RedirectToAction("Index", "FacilityTypes");
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            string role = new Business.AccountBusiness().currentMember();
            if (role == "U" || role == "A")
            {
                return RedirectToAction("Login", "Account");
            }
            var facilityType = webApi.GetFacilityType(id);
            return View(facilityType);
        }

        [HttpPost]
        public ActionResult Edit(HttpPostedFileBase file, SportsSide.DAL.FACILITY_TYPE model, bool isDelete)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (isDelete)
            {
                string oldPath = model.FT_PICTURE;
                string oldFullPath = "";
                if (oldPath == null)
                {
                    oldPath = "";
                    oldFullPath = Path.Combine(Server.MapPath("~"), oldPath);
                }
                else
                {
                    string[] oldPathArray = oldPath.Split('/');
                    string newOldPath = oldPathArray[oldPathArray.Length - 1];
                    oldFullPath = Path.Combine(Server.MapPath("~/img/FacilityTypes"), newOldPath);
                }
                model.FT_PICTURE = "";
                //if (System.IO.File.Exists(oldFullPath))
                //{
                //    System.IO.File.Delete(oldFullPath);
                //}
                var result = webApi.PutFacilityType(model);
                if (result == false)
                {
                    TempData["Hata"] = "Bir hata oluştu.";
                    return View();
                }
            }
            else
            {
                if (file != null)
                {
                    if (file.ContentLength > 0)
                    {
                        if ((Path.GetExtension(file.FileName).ToLower() == ".jpg") || (Path.GetExtension(file.FileName).ToLower() == ".png") ||
                            (Path.GetExtension(file.FileName).ToLower() == ".jpeg"))
                        {
                            string oldPath = model.FT_PICTURE;
                            string oldFullPath = "";
                            if (oldPath == null) {
                                oldPath = "";
                                oldFullPath = Path.Combine(Server.MapPath("~"), oldPath);
                            }
                            else {
                                string[] oldPathArray = oldPath.Split('/');
                                string newOldPath = oldPathArray[oldPathArray.Length - 1];
                                oldFullPath = Path.Combine(Server.MapPath("~/img/FacilityTypes"), newOldPath);
                            }
                            model.FT_PICTURE = "";
                            //if (System.IO.File.Exists(oldFullPath))
                            //{
                            //    System.IO.File.Delete(oldFullPath);
                            //}

                            string newPath = Path.Combine(Server.MapPath("~/img/FacilityTypes"), file.FileName);
                            string ftPicture = "/img/FacilityTypes/" + file.FileName;
                            model.FT_PICTURE = ftPicture;
                            var result = webApi.PutFacilityType(model);
                            if (result == false)
                            {
                                TempData["Hata"] = "Bir hata oluştu.";
                                return View();
                            }
                            file.SaveAs(newPath);
                            newPath = newPath.Replace("SportSideAdmin", "sahakirala");
                            file.SaveAs(newPath);
                        }
                        else
                        {
                            TempData["Hata"] = "Bir hata oluştu.";
                            return View();
                        }
                    }
                    else
                    {
                        TempData["Hata"] = "Bir hata oluştu.";
                        return View();
                    }
                }
                else
                {
                    var result = webApi.PutFacilityType(model);
                    if (result == false)
                    {
                        TempData["Hata"] = "Bir hata oluştu.";
                        return View();
                    }
                }
            }
            return RedirectToAction("Index", "FacilityTypes");
        }

        public ActionResult Delete(int id)
        {
            string role = new Business.AccountBusiness().currentMember();
            if (role == "U" || role == "A")
            {
                return RedirectToAction("Login", "Account");
            }
            var facilityType = webApi.GetFacilityType(id);
            return View(facilityType);
        }

        [HttpPost]
        public ActionResult Delete(SportsSide.DAL.FACILITY_TYPE facilityType)
        {
            var result = webApi.DeleteFacilityType(facilityType);
            if (result == false)
            {
                TempData["Hata"] = "Bir hata oluştu.";
                return View(facilityType);
            }
            return RedirectToAction("Index", "FacilityTypes");
        }
    }
}