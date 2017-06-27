using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SportsSide.DAL;
using SportsSide.DAL.Models;

namespace SportsSideAdmin.Models
{
    public class DashboardModel
    {
        public int countFacility { get; set; }
        public int countApprovedReservation { get; set; }
        public int countSubscriber { get; set; }
        public int countVisitor { get; set; }
        public PagedList.IPagedList<FACILITY> Facilities { get; set; }
        public int percentFacility { get; set; }
        public int percentReservation { get; set; }
        public int percentSubscriber { get; set; }
        public int todayReservation { get; set; }
        public int todaySubscriber { get; set; }
        public IEnumerable<GroupByMonths> GroupByMonths { get; set; }
        public IEnumerable<GroupByDays> GroupByDays { get; set; }
    }
}