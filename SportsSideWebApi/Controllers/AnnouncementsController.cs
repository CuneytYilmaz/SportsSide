using SportsSide.DAL;
using SportsSideBL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace SportsSideWebApi.Controllers
{
    public class AnnouncementsController : ApiController
    {
        private ISportsSideService sportsSideService = new SportsSideService();

        [HttpGet]
        [Route("SSApi/Announcements")]
        public List<ANNOUNCEMENTS> Announcements()
        {
            List<ANNOUNCEMENTS> Anno = sportsSideService.Announcementss();
            return Anno;
        }




    }
}
