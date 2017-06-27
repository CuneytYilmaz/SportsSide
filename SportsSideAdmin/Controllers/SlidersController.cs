using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsSideAdmin.Controllers
{
    [Authorize]
    public class SlidersController : Controller
    {
        SportsSideWebApi.Controllers.SportsSideApiController webApi = new SportsSideWebApi.Controllers.SportsSideApiController();
        // GET: Sliders
        public ActionResult Index()
        {
            string role = new Business.AccountBusiness().currentMember();
            if (role == "U" || role == "A")
            {
                return RedirectToAction("Login", "Account");
            }
            var sliders = webApi.GetSliders();
            return View(sliders);
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
        public ActionResult Create(HttpPostedFileBase file, string sliderName)
        {
            var path = "";
            var sliderPicture = "";
            SportsSide.DAL.SLIDER model = new SportsSide.DAL.SLIDER();
            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    if ((Path.GetExtension(file.FileName).ToLower() == ".jpg") || (Path.GetExtension(file.FileName).ToLower() == ".png") ||
                        (Path.GetExtension(file.FileName).ToLower() == ".jpeg"))
                    {
                        path = Path.Combine(Server.MapPath("~/img/Slider"), file.FileName);
                        sliderPicture = "/img/Slider/" + file.FileName;
                        model.SLIDER_DESCRIPTION = sliderName;
                        model.SLIDER_PICTURE = sliderPicture;
                        var result = webApi.PostSlider(model);
                        if (result == false)
                        {
                            TempData["Hata"] = "Bir hata oluştu.";
                            return View();
                        }
                        file.SaveAs(path);
                        path = path.Replace("SportSideAdmin", "sahakirala");
                        file.SaveAs(path);
                        return RedirectToAction("Index", "Sliders");
                    }
                }
            }
            else
            {
                model.SLIDER_DESCRIPTION = sliderName;
                model.SLIDER_PICTURE = sliderPicture;
                var result = webApi.PostSlider(model);
                if (result == false)
                {
                    TempData["Hata"] = "Bir hata oluştu.";
                    return View();
                }
                return RedirectToAction("Index", "Sliders");
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
            var slider = webApi.GetSlider(id);
            return View(slider);
        }

        [HttpPost]
        public ActionResult Edit(HttpPostedFileBase file, SportsSide.DAL.SLIDER model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    if ((Path.GetExtension(file.FileName).ToLower() == ".jpg") || (Path.GetExtension(file.FileName).ToLower() == ".png") ||
                        (Path.GetExtension(file.FileName).ToLower() == ".jpeg"))
                    {
                        string oldPath = model.SLIDER_PICTURE;
                        string oldFullPath = "";

                        string[] oldPathArray = oldPath.Split('/');
                        string newOldPath = oldPathArray[oldPathArray.Length - 1];
                        oldFullPath = Path.Combine(Server.MapPath("~/img/Slider"), newOldPath);
                        //if (System.IO.File.Exists(oldFullPath))
                        //{
                        //    System.IO.File.Delete(oldFullPath);
                        //}

                        string newPath = Path.Combine(Server.MapPath("~/img/Slider"), file.FileName);
                        string sliderPicture = "/img/Slider/" + file.FileName;
                        model.SLIDER_PICTURE = sliderPicture;
                        var result = webApi.PutSlider(model);
                        if (result == false)
                        {
                            TempData["Hata"] = "Bir hata oluştu.";
                            return View();
                        }
                        file.SaveAs(newPath);
                        newPath = newPath.Replace("SportsSideAdmin", "sahakirala");
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
                var result = webApi.PutSlider(model);
                if (result == false)
                {
                    TempData["Hata"] = "Bir hata oluştu.";
                    return View();
                }
            }
            return RedirectToAction("Index", "Sliders");
        }

        public ActionResult Delete(int id)
        {
            string role = new Business.AccountBusiness().currentMember();
            if (role == "U" || role == "A")
            {
                return RedirectToAction("Login", "Account");
            }
            var slider = webApi.GetSlider(id);
            return View(slider);
        }

        [HttpPost]
        public ActionResult Delete(SportsSide.DAL.SLIDER slider)
        {
            var result = webApi.DeleteSlider(slider);
            if (result == false)
            {
                TempData["Hata"] = "Bir hata oluştu.";
                return View(slider);
            }
            return RedirectToAction("Index", "Sliders");
        }
    }
}