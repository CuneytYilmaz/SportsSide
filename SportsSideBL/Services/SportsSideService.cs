using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SportsSide.DAL;
using System.Data.Entity;
using System.Data.Entity.Validation;
using SportsSide.DAL.Models;


namespace SportsSideBL.Services
{
    public class SportsSideService : ISportsSideService
    {
        private SportsSideEntities db = new SportsSideEntities();

        public bool DeleteArea(AREA area)
        {
            try
            {
                db.Entry(area).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public AREA GetArea(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.AREA.Where(x => x.AREA_ID == id).FirstOrDefault();
        }

        public List<AREA> GetAreas()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.AREA.Include(x => x.CITY).ToList();
        }

        public bool PostArea(AREA area)
        {
            try
            {
                db.AREA.Add(area);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool PutArea(AREA area)
        {
            try
            {
                db.Entry(area).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public List<ANNOUNCEMENTS> GetAnn()
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<ANNOUNCEMENTS> ann = db.ANNOUNCEMENTS.OrderByDescending(x => x.A_ID).Take(4).ToList();

            return ann;
        }
        public FACILITY_TYPE GetFacilityType(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.FACILITY_TYPE.Where(x => x.FT_ID == id).FirstOrDefault();
        }

        public List<FACILITY_TYPE> GetFacilityTypes()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.FACILITY_TYPE.ToList();
        }

        public bool PostFacilityType(FACILITY_TYPE facilityType)
        {
            try
            {
                db.FACILITY_TYPE.Add(facilityType);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool PutFacilityType(FACILITY_TYPE facilityType)
        {
            try
            {
                db.Entry(facilityType).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteFacilityType(FACILITY_TYPE facilityType)
        {
            try
            {
                db.Entry(facilityType).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public CITY GetCity(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.CITY.Include(x => x.AREA).Where(x => x.CITY_ID == id).FirstOrDefault();
        }

        public List<CITY> GetCities()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.CITY.Include(x => x.AREA).Include(x => x.DISTRICT).ToList();
        }

        public List<CITY> GetCitiesByAreas(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.CITY.Include(x => x.AREA).Where(x => x.AREA_ID == id).ToList();
        }

        public List<DISTRICT> GetDistrictsByCities(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.DISTRICT.Include(x => x.CITY).Include(x => x.CITY.AREA).Where(x => x.CITY_ID == id).ToList();
        }

        public bool PostCity(CITY city)
        {
            try
            {
                db.CITY.Add(city);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool PutCity(CITY city)
        {
            try
            {
                db.Entry(city).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteCity(CITY city)
        {
            try
            {
                db.Entry(city).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public DISTRICT GetDistrict(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.DISTRICT.Include(x => x.CITY).Include(x => x.CITY.AREA).Where(x => x.DISTRICT_ID == id).FirstOrDefault();
        }

        public List<DISTRICT> GetDistricts()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.DISTRICT.Include(x => x.CITY).ToList();
        }

        public bool PostDistrict(DISTRICT district)
        {
            try
            {
                db.DISTRICT.Add(district);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool PutDistrict(DISTRICT district)
        {
            try
            {
                db.Entry(district).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteDistrict(DISTRICT district)
        {
            try
            {
                db.Entry(district).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<SLIDER> GetSliders()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.SLIDER.ToList();
        }

        public SLIDER GetSlider(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.SLIDER.Where(x => x.SLIDER_ID == id).FirstOrDefault();
        }

        public bool PostSlider(SLIDER slider)
        {
            try
            {
                db.SLIDER.Add(slider);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool PutSlider(SLIDER slider)
        {
            try
            {
                db.Entry(slider).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteSlider(SLIDER slider)
        {
            try
            {
                db.Entry(slider).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<FACILITY> GetAllFacilities()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.FACILITY.Include(x => x.DISTRICT).Include(x => x.FACILITY_PICTURES).Include(x => x.FACILITY_TYPE1).Include(x => x.USERS).Include(x => x.DISTRICT).Include(x => x.FACILITY_PICTURES).Include(x => x.FACILITY_TYPE1).Include(x => x.DISTRICT.CITY).Include(x => x.DISTRICT.CITY.AREA).ToList();
        }

        public List<FACILITY> GetFacilitiesByUserId(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.FACILITY.Include(x => x.DISTRICT).Include(x => x.FACILITY_PICTURES).Include(x => x.FACILITY_TYPE1).Include(x => x.USERS).Include(x => x.DISTRICT).Include(x => x.FACILITY_PICTURES).Include(x => x.FACILITY_TYPE1).Include(x => x.DISTRICT.CITY).Include(x => x.DISTRICT.CITY.AREA).Where(x => x.USER_ID == id).ToList();
        }

        public FACILITY GetFacility(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.FACILITY.Include(x => x.DISTRICT).Include(x => x.FACILITY_PICTURES).Include(x => x.FACILITY_TYPE1).Include(x => x.USERS).Include(x => x.DISTRICT).Include(x => x.FACILITY_PICTURES).Include(x => x.FACILITY_TYPE1).Include(x => x.DISTRICT.CITY).Include(x => x.DISTRICT.CITY.AREA).Where(x => x.FACILITY_ID == id).FirstOrDefault();
        }

        public bool PostFacility(FACILITY facility)
        {
            try
            {
                db.FACILITY.Add(facility);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool PutFacility(FACILITY facility)
        {
            try
            {
                db.Entry(facility).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteFacility(FACILITY facility)
        {
            try
            {
                db.Entry(facility).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<FACILITY_PICTURES> GetPicturesFromFacility(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.FACILITY_PICTURES.Include(x => x.FACILITY).Where(x => x.F_ID == id).ToList();
        }

        public USERS GetUser(string userName)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.USERS.Include(x => x.DISTRICT).Include(x => x.DISTRICT.CITY).Include(x => x.DISTRICT.CITY.AREA).Where(x => x.USER_NAME == userName).FirstOrDefault();
        }

        public USERS GetUser(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.USERS.Include(x => x.DISTRICT).Include(x => x.DISTRICT.CITY).Include(x => x.DISTRICT.CITY.AREA).Where(x => x.USER_ID == id).FirstOrDefault();
        }
        public bool PostFacilityPicture(FACILITY_PICTURES facilityPicture)
        {
            try
            {
                db.FACILITY_PICTURES.Add(facilityPicture);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool DeleteAllFacilityPicturesFromId(int id)
        {
            try
            {
                var items = db.FACILITY_PICTURES.AsNoTracking().Where(item => item.F_ID == id).ToList();
                foreach (var item in items)
                {
                    db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool PutUser(USERS user)
        {
            try
            {
                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public List<RESERVATION> GetFacilityReservations(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.RESERVATION.Include(x => x.FACILITY).Include(x => x.USERS).Where(x => x.FACILITY_ID == id && (x.STATUS == "A" || x.STATUS == "P")).ToList();
        }

        public List<SUBSCRIBER> GetFacilitySubscribers(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.SUBSCRIBER.Include(x => x.FACILITY).Include(x => x.USERS).Where(x => x.FACILITY_ID == id && x.STATUS == "A").ToList();
        }

        public bool PostReservation(RESERVATION reservation)
        {
            try
            {
                db.RESERVATION.Add(reservation);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool PostSubscriber(SUBSCRIBER subscribe)
        {
            try
            {
                db.SUBSCRIBER.Add(subscribe);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public List<RESERVATION> GetReservationsByUserId(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.RESERVATION.Include(x => x.USERS).Include(x => x.FACILITY).Where(x => x.USER_ID == id).ToList();
        }

        public List<SUBSCRIBER> GetFacilitySubscribersForSubscribe(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.SUBSCRIBER.Include(x => x.FACILITY).Include(x => x.USERS).Where(x => x.FACILITY_ID == id && (x.STATUS == "A" || x.STATUS == "P")).ToList();
        }

        public List<RESERVATION> GetReservationsForApprove(int userId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.RESERVATION.Include(x => x.FACILITY).Include(x => x.USERS).Where(x => x.FACILITY.USER_ID == userId && x.STATUS == "P").OrderByDescending(x => x.RESERVATION_ID).ToList();
        }

        public List<SUBSCRIBER> GetSubscribersForApprove(int userId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.SUBSCRIBER.Include(x => x.FACILITY).Include(x => x.USERS).Where(x => x.FACILITY.USER_ID == userId && x.STATUS == "P").OrderByDescending(x => x.SUBSCRIBER_ID).ToList();
        }

        public List<RESERVATION> GetAllReservationsForFacilityOwner(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.RESERVATION.Include(x => x.FACILITY).Include(x => x.USERS).Where(x => x.FACILITY.USER_ID == id).OrderByDescending(x => x.RESERVATION_ID).ToList();
        }

        public List<SUBSCRIBER> GetAllSubscribersForFacilityOwner(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.SUBSCRIBER.Include(x => x.FACILITY).Include(x => x.USERS).Where(x => x.FACILITY.USER_ID == id).OrderByDescending(x => x.SUBSCRIBER_ID).ToList();
        }

        public RESERVATION GetReservationFromReservationId(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.RESERVATION.Include(x => x.FACILITY).Include(x => x.USERS).Where(x => x.RESERVATION_ID == id).FirstOrDefault();
        }
        public bool PutReservation(RESERVATION reservation)
        {
            try
            {
                db.Entry(reservation).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex){
                return false;
            }
        }
        public List<SUBSCRIBER> GetSubscribersByUserId(int userId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.SUBSCRIBER.Include(x => x.FACILITY).Include(x => x.USERS).Where(x => x.USERS.USER_ID == userId).ToList();
        }

        public bool DeleteSubscriberByUserId(int id)
        {
            try
            {
                var items = db.SUBSCRIBER.AsNoTracking().Where(item => item.SUBSCRIBER_ID == id).ToList();
                foreach (var item in items)
                {
                    db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        
        public bool KontrolMail(string email)
        {
            bool cnrl;

            var model = db.USERS.FirstOrDefault(x => x.USER_MAIL == email);
            if (model == null) { cnrl = false;}
            else { cnrl = true;}  

            return cnrl;
        }
        public void SifreDegistirMail(string mail,int sifre)
        {
            var model = db.USERS.FirstOrDefault(x => x.USER_MAIL == mail);
            model.USER_PASSWORD = sifre.ToString();
            db.SaveChanges(); 
        }


        public SUBSCRIBER GetSubscriberFromSubscriberId(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.SUBSCRIBER.Include(x => x.FACILITY).Include(x => x.USERS).Where(x => x.SUBSCRIBER_ID == id).FirstOrDefault();
        }

        public bool PutSubscriber(SUBSCRIBER subscriber)
        {
            try
            {
                db.Entry(subscriber).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<USERS> GetAllUsers()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.USERS.Include(x => x.DISTRICT).Include(x => x.DISTRICT.CITY).ToList();
        }
        public List<ANNOUNCEMENTS> GetAllAnnouncements()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.ANNOUNCEMENTS.OrderByDescending(x => x.A_ID).ToList();
        }

        public bool PostAnnouncements(ANNOUNCEMENTS announcement)
        {
            try
            {
                db.ANNOUNCEMENTS.Add(announcement);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool PutAnnouncements(ANNOUNCEMENTS announcement)
        {
            try
            {
                db.Entry(announcement).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteAnnouncements(ANNOUNCEMENTS announcement)
        {
            try
            {
                db.Entry(announcement).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public ANNOUNCEMENTS GetAnnouncementById(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.ANNOUNCEMENTS.Where(x => x.A_ID == id).FirstOrDefault();
        }

        public List<USERS> GetUsersForPermission(string userName, string userFirstName, string userSurname)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.USERS.Include(x => x.DISTRICT).Include(x => x.DISTRICT.CITY).Where(x => x.USER_NAME.Contains(userName) && x.USER_FIRST_NAME.Contains(userFirstName) && x.USER_SURNAME.Contains(userSurname)).ToList();
        }

        public int CountFacility(string userName)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.FACILITY.Include(x => x.DISTRICT).Include(x => x.FACILITY_TYPE1).Include(x => x.USERS).Count(x => x.USERS.USER_NAME == userName);
        }

        public int CountApprovedReservation(string userName)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.RESERVATION.Include(x => x.FACILITY).Include(x => x.USERS).Count(x => x.FACILITY.USERS.USER_NAME == userName && x.STATUS == "A");
        }

        public int CountSubscriber(string userName)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.SUBSCRIBER.Include(x => x.FACILITY).Include(x => x.USERS).Count(x => x.FACILITY.USERS.USER_NAME == userName && x.STATUS == "A");
        }

        public int CountVisitor(string userName)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return (db.RESERVATION.Where(x => x.FACILITY.USERS.USER_NAME == userName && x.STATUS == "A" && x.DT_BEGIN <= DateTime.Now).Sum(x => x.COUNT) ?? 0);
        }

        public List<FACILITY> GetFacilitiesByUserName(string userName)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.FACILITY.Include(x => x.DISTRICT).Include(x => x.DISTRICT.CITY).Include(x => x.DISTRICT.CITY.AREA).Include(x => x.FACILITY_TYPE1).Include(x => x.USERS).Where(x => x.USERS.USER_NAME == userName).ToList();
        }

        public int PercentFacility(string userName)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var list = db.RESERVATION.Include(x => x.FACILITY).Include(x => x.USERS).Where(x => x.STATUS == "A" && x.FACILITY.USERS.USER_NAME == userName).ToList();
            var newList = list.Where(x => x.DT_BEGIN.Date == DateTime.Now.Date).ToList();
            double todayCapacity = newList.Count();
            double totalCapacity = 24 * (db.FACILITY.Include(x => x.USERS).Where(x => x.USERS.USER_NAME == userName).Count());
            double percent = 0;
            if (todayCapacity != 0 && totalCapacity != 0)
            {
                percent = todayCapacity / totalCapacity;
            }
            percent = percent * 100;
            return int.Parse(Math.Round(percent).ToString());
        }
        public int PercentReservation(string userName)
        {
            db.Configuration.ProxyCreationEnabled = false;
            double approvedReservation = db.RESERVATION.Include(x => x.FACILITY).Include(x => x.FACILITY.USERS).Where(x => x.STATUS == "A" && x.FACILITY.USERS.USER_NAME == userName).Count();
            double totalReservation = db.RESERVATION.Include(x => x.FACILITY).Include(x => x.FACILITY.USERS).Where(x => x.FACILITY.USERS.USER_NAME == userName).Count();
            double percent = 0;
            if (approvedReservation != 0 && totalReservation != 0)
            {
                percent = approvedReservation / totalReservation;
            }
            percent = percent * 100;
            return int.Parse(Math.Round(percent).ToString());
        }
        public int PercentSubscriber(string userName)
        {
            db.Configuration.ProxyCreationEnabled = false;
            double approvedSubscriber = db.SUBSCRIBER.Include(x => x.FACILITY).Include(x => x.FACILITY.USERS).Where(x => x.STATUS == "A" && x.FACILITY.USERS.USER_NAME == userName).Count();
            double totalSubscriber = db.SUBSCRIBER.Include(x => x.FACILITY).Include(x => x.FACILITY.USERS).Where(x => x.FACILITY.USERS.USER_NAME == userName).Count();
            double percent = 0;
            if (approvedSubscriber != 0 && totalSubscriber != 0)
            {
                percent = approvedSubscriber / totalSubscriber;
            }
            percent = percent * 100;
            return int.Parse(Math.Round(percent).ToString());
        }
        public int TodayReservation(string userName)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var list = db.RESERVATION.Include(x => x.FACILITY).Include(x => x.FACILITY.USERS).Where(x => x.FACILITY.USERS.USER_NAME == userName).ToList();
            var newList = list.Where(x => x.CREATED_DATE.Date == DateTime.Now.Date).ToList();
            return newList.Count();
        }
        public int TodaySubscriber(string userName)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var list = db.SUBSCRIBER.Include(x => x.FACILITY).Include(x => x.FACILITY.USERS).Where(x => x.FACILITY.USERS.USER_NAME == userName).ToList();
            var newList = list.Where(x => x.CREATED_DATE.Date == DateTime.Now.Date).ToList();
            return newList.Count();
        }
        public List<GroupByMonths> GroupByMonths(string userName)
            {
            db.Configuration.ProxyCreationEnabled = false;
            var reservationList = db.RESERVATION.Include(x => x.FACILITY).Include(x => x.FACILITY.USERS).Where(x => x.FACILITY.USERS.USER_NAME == userName).GroupBy(x => new { x.CREATED_DATE.Month}).ToList();
            var subscriberList = db.SUBSCRIBER.Include(x => x.FACILITY).Include(x => x.FACILITY.USERS).Where(x => x.FACILITY.USERS.USER_NAME == userName).GroupBy(x => new { x.CREATED_DATE.Month }).ToList();

            List<GroupByMonths> newList = new List<GroupByMonths>();
            string[] Months = { "Ocak", "Şubat", "Mart", "Nisan", "Mayıs", "Haziran", "Temmuz", "Ağustos", "Eylül", "Ekim", "Kasım", "Aralık" };

            for (int i = 1; i <= 12; i++)
            {
                GroupByMonths myItem = new GroupByMonths();
                myItem.MonthName = Months[i - 1];
                myItem.CountReservation = 0;
                myItem.CountSubscriber = 0;

                foreach (var item in reservationList)
                {
                    if (item.Key.Month == i)
                    {
                        myItem.CountReservation = item.Count();
                        break;
                    }
                }

                foreach (var item in subscriberList)
                {
                    if (item.Key.Month == i)
                    {
                        myItem.CountSubscriber = item.Count();
                        break;
                    }
                }
                newList.Add(myItem);
            }
            return newList;
        }

        public List<GroupByDays> GroupByDays(string userName)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var oldReservationList = db.RESERVATION.Include(x => x.FACILITY).Include(x => x.FACILITY.USERS).Where(x => x.FACILITY.USERS.USER_NAME == userName).ToList();
            var reservationList = oldReservationList.GroupBy(x => new { x.DT_BEGIN.DayOfWeek }).Select(x => new { DayOfWeek = x.Key, Count = x.Count()}).ToList();
            var subscriberList = db.SUBSCRIBER.Include(x => x.FACILITY).Include(x => x.FACILITY.USERS).Where(x => x.FACILITY.USERS.USER_NAME == userName).GroupBy(x => new { x.WEEK_DAY }).ToList();

            List<GroupByDays> newList = new List<GroupByDays>();
            string[] Days = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            string[] turkishDays = { "Pazartesi", "Salı", "Car.", "Perşembe", "Cuma", "Cmt.", "Pazar" };

            for (int i = 1; i <= 7; i++)
            {
                GroupByDays myItem = new GroupByDays();
                myItem.DayName = turkishDays[i - 1];
                myItem.CountReservation = 0;
                myItem.CountSubscriber = 0;

                foreach (var item in reservationList)
                {
                    if (item.DayOfWeek.DayOfWeek.ToString() == Days[i - 1])
                    {
                        myItem.CountReservation = item.Count;
                        break;
                    }
                }

                foreach (var item in subscriberList)
                {
                    if (item.Key.WEEK_DAY == Days[i-1])
                    {
                        myItem.CountSubscriber = item.Count();
                        break;
                    }
                }
                newList.Add(myItem);
            }
            return newList;
        }
        public List<RESERVATION> liveReservations(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            return db.RESERVATION.Include(x => x.USERS).Include(x => x.FACILITY).Where(x => x.USER_ID == id).ToList();
        }

        public List<RESERVATION> oldReservations(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            return db.RESERVATION.Include(x => x.USERS).Include(x => x.FACILITY).Where(x => x.USER_ID == id).ToList();
        }
        public string UserRole(string isim)
        {
            var model = db.USERS.FirstOrDefault(x => x.USER_NAME == isim);

            return model.USER_TYPE;
        }

    }
}