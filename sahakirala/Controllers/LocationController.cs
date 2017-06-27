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
using System.Data;

namespace sahakirala.Controllers
{
    public class LocationController : Controller
    {
        // GET: Location
        public ActionResult Index(USER model, string enlem, string boylam)
        {
            if (enlem != null && boylam != null)
            {
                DatabaseEntities db = new DatabaseEntities();
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



                model.DISTRICT.CITY.CITY_NAME = il;
                model.DISTRICT.DISTRICT_NAME = ilce;
                //model.DISTRICT.CITY.AREA.AREA_NAME = bolge;
                //model.ConfirmPassword = model.Password;
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return View(model);
            }

            return View(model);
        }
    }
}