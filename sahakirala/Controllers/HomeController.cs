using Database;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using sahakirala.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace sahakirala.Controllers
{
    public class HomeController : Controller
    {
       

        // GET: Home
        public ActionResult Index(string enlem, string boylam)
       {


            //DatabaseEntities db = new DatabaseEntities();
            //string cookieName = FormsAuthentication.FormsCookieName; //Find cookie name
            //HttpCookie authCookie = HttpContext.Request.Cookies[cookieName]; //Get the cookie by it's name
            //FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value); //Decrypt it
            //string UserName = ticket.Name; //You have the UserName!
            //var rowFind = db.USERS.FirstOrDefault(x => x.USER_NAME == UserName);

            //if (enlem != null && boylam != null)
            //{
            //    string url = "http://maps.googleapis.com/maps/api/geocode/json?latlng=" + enlem + "," + boylam + "&sensor=true";
            //    HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            //    string jsonVerisi = "";
            //    using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            //    {
            //        StreamReader reader = new StreamReader(response.GetResponseStream());
            //        jsonVerisi = reader.ReadToEnd();
            //    }
            //    var obj = JObject.Parse(jsonVerisi);

            //    var user = JsonConvert.DeserializeObject<GoogleGeoCodeResponse>(jsonVerisi);
            //    var yer = user.results[0].address_components[0].long_name;
            //    var mevki = user.results[0].address_components[1].long_name;
            //    var mahalle = user.results[0].address_components[2].long_name;
            //    var ilce = user.results[0].address_components[3].long_name;
            //    var il = user.results[0].address_components[4].long_name;

            //    //var distList = db.DISTRICTs.FirstOrDefault(a => a.DISTRICT_NAME == ilce);

            //    //var facility = db.FACILITies.Where(a => a.DISTRICT_ID == distList.DISTRICT_ID).ToList();
            //    //IEnumerable<FACILITY> facilityIE = facility;


            //    //rowFind.USER_DISTRICT = distList.DISTRICT_ID;

            //    //db.Entry(rowFind).State = EntityState.Modified;
            //    //db.SaveChanges();
            //    return View();
            //}

                return View();
        }
        [HttpPost]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult iletisim()
        {


            return View();
        }

        [HttpPost]
        public JsonResult iletisimPost(string isim,string soyad,string email,string tel,string mesaj, MemberBigModel model)
        {
            if (ModelState.IsValid)
            {
                
                var getJson = "";
                if (email == "" || mesaj == "")
                {
                    getJson = "0";

                }
                else
                {
                    getJson = "1";
                    MailMessage mesajim = new MailMessage();
                    SmtpClient istemci = new SmtpClient();


                    istemci.Credentials = new System.Net.NetworkCredential("mustafatrkn93@outlook.com", "haskovo35");
                    istemci.Port = 587;
                    istemci.Host = "smtp.live.com";
                    istemci.EnableSsl = true;
                    mesajim.To.Add(email);
                    mesajim.From = new MailAddress("mustafatrkn93@outlook.com");
                    mesajim.Subject = "SportSide Alan Tahsili";
                    mesajim.Body = "Gönderenin Adı ve Soyadı: " + isim.ToString() + " " + soyad.ToString() + "\n" + "Telefon Numarası: " + tel.ToString() + "\n" + "Gönderilen Mesaj\n" + mesaj.ToString() + "";
                    //istemci.Send(mesajim);
                    object userState = mesajim;
                    istemci.Send(mesajim);
                }
                return Json(getJson);

            }
            else return Json("0");

        }
    }
}