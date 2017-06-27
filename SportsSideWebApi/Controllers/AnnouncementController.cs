using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SportsSideBL.Services;
using SportsSide.DAL;

namespace SportsSideWebApi.Controllers
{
    public class AnnouncementController : ApiController
    {
        private ISportsSideService sportsSideService = new SportsSideService();

        [HttpGet]
        [Route("SSApi/GetAnnoun")]
        public List<ANNOUNCEMENTS> GetAnnoun()
        {
            List<ANNOUNCEMENTS> Ann = sportsSideService.GetAnn();
            return Ann;
        }



    }
}
