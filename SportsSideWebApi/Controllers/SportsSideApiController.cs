using SportsSide.DAL;
using SportsSideBL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SportsSide.DAL.Models;

namespace SportsSideWebApi.Controllers
{
    public class SportsSideApiController : ApiController
    {
        private ISportsSideService sportsSideService = new SportsSideService();

        [HttpGet]
        //[ActionName("GetArea")]
        [Route("SSApi/GetArea")]
        public IEnumerable<AREA> GetAreas()
        {
            return sportsSideService.GetAreas();
        }

        [HttpGet]
        //[ActionName("GetArea")]
        [Route("SSApi/GetArea/{id}")]
        public AREA GetArea(int id)
        {
            var area = sportsSideService.GetArea(id);
            return area;
        }

        //[AcceptVerbs("PUT", "POST")]
        [HttpPost]
        public bool PostArea(AREA area)
        {
            var isSaved = sportsSideService.PostArea(area);
            if (isSaved)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpPut]
        public bool PutArea(AREA area)
        {
            var isUpdated = sportsSideService.PutArea(area);
            if (isUpdated)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpDelete]
        public bool DeleteArea(AREA area)
        {
            var isDeleted = sportsSideService.DeleteArea(area);
            if (isDeleted)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpGet]
        //[ActionName("GetFacilityType")]
        [Route("SSApi/GetFacilityType")]
        public IEnumerable<FACILITY_TYPE> GetFacilityTypes()
        {
            return sportsSideService.GetFacilityTypes();
        }

        [HttpGet]
        //[ActionName("GetFacilityType")]
        [Route("SSApi/GetFacilityType/{id}")]
        public FACILITY_TYPE GetFacilityType(int id)
        {
            var facilityType = sportsSideService.GetFacilityType(id);
            return facilityType;
        }

        [HttpPost]
        public bool PostFacilityType(FACILITY_TYPE facilityType)
        {
            var isSaved = sportsSideService.PostFacilityType(facilityType);
            if (isSaved)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpPut]
        public bool PutFacilityType(FACILITY_TYPE facilityType)
        {
            var isUpdated = sportsSideService.PutFacilityType(facilityType);
            if (isUpdated)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpDelete]
        public bool DeleteFacilityType(FACILITY_TYPE facilityType)
        {
            var isDeleted = sportsSideService.DeleteFacilityType(facilityType);
            if (isDeleted)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpGet]
        //[ActionName("GetCity")]
        [Route("SSApi/GetCity")]
        public IEnumerable<CITY> GetCities()
        {
            return sportsSideService.GetCities();
        }

        [HttpGet]
        //[ActionName("GetCity")]
        [Route("SSApi/GetCity/{id}")]
        public CITY GetCity(int id)
        {
            var city = sportsSideService.GetCity(id);
            return city;
        }

        [HttpGet, HttpPost]
        [Route("SSApi/GetCitiesByAreas")]
        public IEnumerable<CITY> GetCitiesByAreas(int areaId)
        {
            return sportsSideService.GetCitiesByAreas(areaId);
        }

        [HttpGet, HttpPost]
        [Route("SSApi/GetDistrictsByCities")]
        public IEnumerable<DISTRICT> GetDistrictsByCities(int cityId)
        {
            return sportsSideService.GetDistrictsByCities(cityId);
        }

        [HttpPost]
        public bool PostCity(CITY city)
        {
            var isSaved = sportsSideService.PostCity(city);
            if (isSaved)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpPut]
        public bool PutCity(CITY city)
        {
            var isUpdated = sportsSideService.PutCity(city);
            if (isUpdated)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpDelete]
        public bool DeleteCity(CITY city)
        {
            var isDeleted = sportsSideService.DeleteCity(city);
            if (isDeleted)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpGet]
        //[ActionName("GetDistrict")]
        [Route("SSApi/GetDistrict")]
        public IEnumerable<DISTRICT> GetDistricts()
        {
            return sportsSideService.GetDistricts();
        }

        [HttpGet]
        //[ActionName("GetDistrict")]
        [Route("SSApi/GetDistrict/{id}")]
        public DISTRICT GetDistrict(int id)
        {
            var district = sportsSideService.GetDistrict(id);
            return district;
        }

        [HttpPost]
        public bool PostDistrict(DISTRICT district)
        {
            var isSaved = sportsSideService.PostDistrict(district);
            if (isSaved)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpPut]
        public bool PutDistrict(DISTRICT district)
        {
            var isUpdated = sportsSideService.PutDistrict(district);
            if (isUpdated)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpDelete]
        public bool DeleteDistrict(DISTRICT district)
        {
            var isDeleted = sportsSideService.DeleteDistrict(district);
            if (isDeleted)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpGet]
        //[ActionName("GetFacilityType")]
        [Route("SSApi/GetSlider")]
        public IEnumerable<SLIDER> GetSliders()
        {
            return sportsSideService.GetSliders();
        }

        [HttpGet]
        //[ActionName("GetFacilityType")]
        [Route("SSApi/GetSlider/{id}")]
        public SLIDER GetSlider(int id)
        {
            var slider = sportsSideService.GetSlider(id);
            return slider;
        }

        [HttpPost]
        public bool PostSlider(SLIDER slider)
        {
            var isSaved = sportsSideService.PostSlider(slider);
            if (isSaved)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpPut]
        public bool PutSlider(SLIDER slider)
        {
            var isUpdated = sportsSideService.PutSlider(slider);
            if (isUpdated)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpDelete]
        public bool DeleteSlider(SLIDER slider)
        {
            var isDeleted = sportsSideService.DeleteSlider(slider);
            if (isDeleted)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpGet]
        public IEnumerable<FACILITY> GetAllFacilities()
        {
            return sportsSideService.GetAllFacilities();
        }

        [HttpGet]
        [Route("SSApi/GetFacility/{id}")]
        public FACILITY GetFacility(int id)
        {
            var facility = sportsSideService.GetFacility(id);
            return facility;
        }

        [HttpGet]
        public IEnumerable<FACILITY> GetFacilitiesByUserId(int id)
        {
            return sportsSideService.GetFacilitiesByUserId(id);
        }

        [HttpPost]
        public bool PostFacility(FACILITY facility)
        {
            var isSaved = sportsSideService.PostFacility(facility);
            if (isSaved)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpPut]
        public bool PutFacility(FACILITY facility)
        {
            var isUpdated = sportsSideService.PutFacility(facility);
            if (isUpdated)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpDelete]
        public bool DeleteFacility(FACILITY facility)
        {
            var isDeleted = sportsSideService.DeleteFacility(facility);
            if (isDeleted)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpGet, HttpPost]
        [Route("SSApi/GetPicturesFromFacility")]
        public IEnumerable<FACILITY_PICTURES> GetPicturesFromFacility(int facilityId)
        {
            return sportsSideService.GetPicturesFromFacility(facilityId);
        }

        [HttpGet]
        [Route("SSApi/GetUser/{id}")]
        public USERS GetUser(string userName)
        {
            var user = sportsSideService.GetUser(userName);
            return user;
        }

        [HttpGet]
        [Route("SSApi/GetUser/{id}")]
        public USERS GetUser(int id)
        {
            var user = sportsSideService.GetUser(id);
            return user;
        }

        [HttpPost]
        public bool PostFacilityPicture(FACILITY_PICTURES facilityPicture)
        {
            var isSaved = sportsSideService.PostFacilityPicture(facilityPicture);
            if (isSaved)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpDelete]
        public bool DeleteAllFacilityPicturesFromId(int id)
        {
            var isDeleted = sportsSideService.DeleteAllFacilityPicturesFromId(id);
            if (isDeleted)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        [HttpPut]
        public bool PutUser(USERS user)
        {
            var isUpdated = sportsSideService.PutUser(user);
            if (isUpdated)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpGet, HttpPost]
        [Route("SSApi/GetPicturesFromFacility/{id}")]
        public IEnumerable<RESERVATION> GetFacilityReservations(int facilityId)
        {
            return sportsSideService.GetFacilityReservations(facilityId);
        }

        [HttpGet, HttpPost]
        [Route("SSApi/GetPicturesFromFacility/{id}")]
        public IEnumerable<SUBSCRIBER> GetFacilitySubscribers(int facilityId)
        {
            return sportsSideService.GetFacilitySubscribers(facilityId);
        }

        [HttpGet]
        public IEnumerable<RESERVATION> GetReservationsByUserId(int id)
        {
            return sportsSideService.GetReservationsByUserId(id);
        }

        [HttpPost]
        public bool PostReservation(RESERVATION reservation)
        {
            var isSaved = sportsSideService.PostReservation(reservation);
            if (isSaved)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpPost]
        public bool PostSubscriber(SUBSCRIBER subscribe)
        {
            var isSaved = sportsSideService.PostSubscriber(subscribe);
            if (isSaved)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpGet, HttpPost]
        [Route("SSApi/GetFacilitySubscribersForSubscribe/{id}")]
        public IEnumerable<SUBSCRIBER> GetFacilitySubscribersForSubscribe(int facilityId)
        {
            return sportsSideService.GetFacilitySubscribersForSubscribe(facilityId);
        }

        [HttpGet, HttpPost]
        [Route("SSApi/GetReservationsForApprove/{id}")]
        public IEnumerable<RESERVATION> GetReservationsForApprove(int userId)
        {
            return sportsSideService.GetReservationsForApprove(userId);
        }

        [HttpGet, HttpPost]
        [Route("SSApi/GetSubscribersForApprove/{id}")]
        public IEnumerable<SUBSCRIBER> GetSubscribersForApprove(int userId)
        {
            return sportsSideService.GetSubscribersForApprove(userId);
        }

        [HttpGet, HttpPost]
        public IEnumerable<SUBSCRIBER> GetSubscribersByUserId(int id)
        {
            return sportsSideService.GetSubscribersByUserId(id);
        }

        [HttpDelete]
        public bool DeleteSubscriberByUserId(int userId)
        {
            var isDeleted = sportsSideService.DeleteSubscriberByUserId(userId);
            if (isDeleted)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [Route("SSApi/GetAllReservationsForFacilityOwner/{id}")]
        public IEnumerable<RESERVATION> GetAllReservationsForFacilityOwner(int id)
        {
            return sportsSideService.GetAllReservationsForFacilityOwner(id);
        }

        [HttpGet, HttpPost]
        [Route("SSApi/GetAllSubscribersForFacilityOwner/{id}")]
        public IEnumerable<SUBSCRIBER> GetAllSubscribersForFacilityOwner(int id)
        {
            return sportsSideService.GetAllSubscribersForFacilityOwner(id);
        }

        public bool ControlMail(string email)
        {
            bool control= sportsSideService.KontrolMail(email);


            return control;
        }

        public void MailSifreDegistir(string mail,int sifre)
        {
            sportsSideService.SifreDegistirMail(mail,sifre);
        }

        [HttpGet]
        [Route("SSApi/GetReservationFromReservationId/{id}")]
        public RESERVATION GetReservationFromReservationId(int id)
        {
            var reservation = sportsSideService.GetReservationFromReservationId(id);
            return reservation;
        }

        [HttpPut]
        public bool PutReservation(RESERVATION reservation)
        {
            var isUpdated = sportsSideService.PutReservation(reservation);
            if (isUpdated)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpGet]
        [Route("SSApi/GetSubscriberFromSubscriberId/{id}")]
        public SUBSCRIBER GetSubscriberFromSubscriberId(int id)
        {
            var subscriber = sportsSideService.GetSubscriberFromSubscriberId(id);
            return subscriber;
        }

        [HttpPut]
        public bool PutSubscriber(SUBSCRIBER subscriber)
        {
            var isUpdated = sportsSideService.PutSubscriber(subscriber);
            if (isUpdated)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpGet]
        [Route("SSApi/GetAllUsers")]
        public IEnumerable<USERS> GetAllUsers()
        {
            return sportsSideService.GetAllUsers();
        }

        [HttpGet]
        [Route("SSApi/GetAllAnnouncements")]
        public IEnumerable<ANNOUNCEMENTS> GetAllAnnouncements()
        {
            return sportsSideService.GetAllAnnouncements();
        }

        [HttpPost]
        public bool PostAnnouncements(ANNOUNCEMENTS announcement)
        {
            var isSaved = sportsSideService.PostAnnouncements(announcement);
            if (isSaved)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpPut]
        public bool PutAnnouncements(ANNOUNCEMENTS announcement)
        {
            var isUpdated = sportsSideService.PutAnnouncements(announcement);
            if (isUpdated)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpDelete]
        public bool DeleteAnnouncements(ANNOUNCEMENTS announcement)
        {
            var isDeleted = sportsSideService.DeleteAnnouncements(announcement);
            if (isDeleted)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpGet]
        [Route("SSApi/GetAnnouncementById/{id}")]
        public ANNOUNCEMENTS GetAnnouncementById(int id)
        {
            var announcement = sportsSideService.GetAnnouncementById(id);
            return announcement;
        }

        [HttpGet]
        [Route("SSApi/GetUsersForPermission")]
        public IEnumerable<USERS> GetUsersForPermission(string userName, string userFirstName, string userSurname)
        {
            return sportsSideService.GetUsersForPermission(userName, userFirstName, userSurname);
        }

        [HttpGet]
        [Route("SSApi/CountFacility")]
        public int CountFacility(string userName)
        {
            return sportsSideService.CountFacility(userName);
        }

        [HttpGet]
        [Route("SSApi/CountApprovedReservation")]
        public int CountApprovedReservation(string userName)
        {
            return sportsSideService.CountApprovedReservation(userName);
        }

        [HttpGet]
        [Route("SSApi/CountSubscriber")]
        public int CountSubscriber(string userName)
        {
            return sportsSideService.CountSubscriber(userName);
        }

        [HttpGet]
        [Route("SSApi/CountVisitor")]
        public int CountVisitor(string userName)
        {
            return sportsSideService.CountVisitor(userName);
        }

        [HttpGet]
        [Route("SSApi/GetFacilitiesByUserName")]
        public IEnumerable<FACILITY> GetFacilitiesByUserName(string userName)
        {
            return sportsSideService.GetFacilitiesByUserName(userName);
        }

        [HttpGet]
        [Route("SSApi/percentFacility")]
        public int PercentFacility(string userName)
        {
            return sportsSideService.PercentFacility(userName);
        }

        [HttpGet]
        [Route("SSApi/PercentReservation")]
        public int PercentReservation(string userName)
        {
            return sportsSideService.PercentReservation(userName);
        }

        [HttpGet]
        [Route("SSApi/PercentSubscriber")]
        public int PercentSubscriber(string userName)
        {
            return sportsSideService.PercentSubscriber(userName);
        }

        [HttpGet]
        [Route("SSApi/TodayReservation")]
        public int TodayReservation(string userName)
        {
            return sportsSideService.TodayReservation(userName);
        }

        [HttpGet]
        [Route("SSApi/TodaySubscriber")]
        public int TodaySubscriber(string userName)
        {
            return sportsSideService.TodaySubscriber(userName);
        }

        [HttpGet]
        [Route("SSApi/GroupByMonths")]
        public IEnumerable<GroupByMonths> GroupByMonths(string userName)
        {
            return sportsSideService.GroupByMonths(userName);
        }

        [HttpGet]
        [Route("SSApi/GroupByDays")]
        public IEnumerable<GroupByDays> GroupByDays(string userName)
        {
            return sportsSideService.GroupByDays(userName);
        }

        [HttpGet]
        public IEnumerable<RESERVATION> liveReservations(int id)
        {
            return sportsSideService.liveReservations(id);
        }

        [HttpGet]
        public IEnumerable<RESERVATION> oldReservations(int id)
        {
            return sportsSideService.oldReservations(id);
        }

        public string getRole(string isim)
        {


            return sportsSideService.UserRole(isim);
        }
    }
}
