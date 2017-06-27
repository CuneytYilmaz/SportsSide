using Database;
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
using System.Web.UI.WebControls;
using PagedList;

namespace sahakirala.Controllers
{
    public class AramaController : Controller
    {
        SportsSideWebApi.Controllers.SportsSideApiController webApi = new SportsSideWebApi.Controllers.SportsSideApiController();
        // GET: Arama
        public ActionResult Index(int? page, string SearchString, string FacilityType, string ddlAreas, string ddlCities, string ddlDistricts, string Price1, string Price2, string enlem, string boylam)
        {
            string role = new Business.AccountBusiness().currentMember();
            if (role != "U" && role != "A" && role != "O")
            {
                return RedirectToAction("Login", "Account");
            }
            DatabaseEntities db = new DatabaseEntities();
            string cookieName = FormsAuthentication.FormsCookieName; //Find cookie name
            HttpCookie authCookie = HttpContext.Request.Cookies[cookieName]; //Get the cookie by it's name
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value); //Decrypt it
            string UserName = ticket.Name; //You have the UserName!
            var rowFind = db.USERS.FirstOrDefault(x => x.USER_NAME == UserName);
            var district = rowFind.USER_DISTRICT;
            List<FACILITY> facilityList = db.FACILITies.Where(a => a.DISTRICT_ID == district).ToList();
            IEnumerable<FACILITY> facilityIE = facilityList;
            var facilities = facilityIE;
            ViewBag.Areas = new SelectList(webApi.GetAreas(), "AREA_ID", "AREA_NAME");

            if (enlem != null && boylam != null)
            {
                string url = "http://maps.googleapis.com/maps/api/geocode/json?latlng=" + enlem + "," + boylam + "&sensor=true";
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                string jsonVerisi = "";
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    jsonVerisi = reader.ReadToEnd();
                }
                var obj = JObject.Parse(jsonVerisi);

                var user = JsonConvert.DeserializeObject<GoogleGeoCodeResponse>(jsonVerisi);
                var yer = user.results[0].address_components[0].long_name;
                var mevki = user.results[0].address_components[1].long_name;
                var mahalle = user.results[0].address_components[2].long_name;
                var ilce = user.results[0].address_components[3].long_name;
                var il = user.results[0].address_components[4].long_name;

                var distList = db.DISTRICTs.FirstOrDefault(a => a.DISTRICT_NAME == ilce);

                var facility = db.FACILITies.Where(a => a.DISTRICT_ID == distList.DISTRICT_ID).ToList();
                //IEnumerable<FACILITY> facilityIE = facility;


                //rowFind.USER_DISTRICT = distList.DISTRICT_ID;

                //db.Entry(rowFind).State = EntityState.Modified;
                //db.SaveChanges();
                return View(facility);
            }



            if (!String.IsNullOrEmpty(ddlAreas))
            {
                facilities = db.FACILITies.Where(a => a.DISTRICT.CITY.AREA_ID.ToString() == ddlAreas).ToList();
            }
            if (!String.IsNullOrEmpty(ddlCities))
            {
                facilities = db.FACILITies.Where(x => x.DISTRICT.CITY_ID.ToString() == ddlCities).ToList();
            }
            if (!String.IsNullOrEmpty(ddlDistricts))
            {
                facilities = db.FACILITies.Where(x => x.DISTRICT_ID.ToString() == ddlDistricts).ToList();
            }
            if (String.IsNullOrEmpty(ddlAreas) && String.IsNullOrEmpty(ddlCities) && String.IsNullOrEmpty(ddlDistricts) && !String.IsNullOrEmpty(SearchString))
            {
                facilities = db.FACILITies.Where(x => x.FACILITY_NAME.StartsWith(SearchString)).ToList();
            }
            if (Price1 != null && Price1 != "")
            {
                int PriceMin = Int32.Parse(Price1);
                facilities = facilities.Where(a => a.FACILITY_PRICE >= PriceMin);
            }
            if (Price2 != null && Price2 != "")
            {
                int PriceMax = Int32.Parse(Price2);
                facilities = facilities.Where(a => a.FACILITY_PRICE <= PriceMax).ToList();
            }
            if (!String.IsNullOrEmpty(FacilityType) && FacilityType != "Tesis Türü Seç")
            {
                facilities = facilities.Where(x => x.FACILITY_TYPE1.FT_NAME == FacilityType).ToList();
            }
            if ((!String.IsNullOrEmpty(ddlAreas) || !String.IsNullOrEmpty(ddlCities) || !String.IsNullOrEmpty(ddlDistricts)) && !String.IsNullOrEmpty(SearchString))
            {
                var newFacilities = db.FACILITies.Where(x => x.FACILITY_NAME.StartsWith(SearchString));
                facilities = newFacilities.Where(x => x.DISTRICT_ID.ToString() == ddlDistricts).ToList();
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(facilities.ToPagedList(pageNumber, pageSize));
        }

        public string DistrictByCityId(int cityId)
        {
            string sonuc = "";
            DatabaseEntities db = new DatabaseEntities();
            var ilceler = db.DISTRICTs.Where(a => a.CITY_ID == cityId).ToList();
            return sonuc;
        }
    }
}