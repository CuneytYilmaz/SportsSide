using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportsSide.DAL.Models
{
    public class GroupByMonths
    {
        public string MonthName { get; set; }
        public int CountReservation { get; set; }
        public int CountSubscriber { get; set; }
    }
}