using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace SportsSideAdmin.Controllers
{
    [Authorize]
    public class PermissionController : Controller
    {
        SportsSideWebApi.Controllers.SportsSideApiController webApi = new SportsSideWebApi.Controllers.SportsSideApiController();
        // GET: Permission
        public ActionResult Index(int? page, string userName, string userFirstName, string userSurname)
        {
            string role = new Business.AccountBusiness().currentMember();
            if (role == "U" || role == "A")
            {
                return RedirectToAction("Login", "Account");
            }
            userName = userName ?? "";
            userFirstName = userFirstName ?? "";
            userSurname = userSurname ?? "";
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            var users = webApi.GetUsersForPermission(userName, userFirstName, userSurname).ToPagedList(pageNumber, pageSize);
            return View(users);
        }

        [HttpGet]
        public bool GivePermission(int id)
        {
            SportsSide.DAL.USERS model = new SportsSide.DAL.USERS();
            model = webApi.GetUser(id);
            model.USER_TYPE = "A";
            var result = webApi.PutUser(model);
            return result;
        }

        [HttpGet]
        public bool TakePermission(int id)
        {
            SportsSide.DAL.USERS model = new SportsSide.DAL.USERS();
            model = webApi.GetUser(id);
            model.USER_TYPE = "U";
            var result = webApi.PutUser(model);
            return result;
        }
    }
}