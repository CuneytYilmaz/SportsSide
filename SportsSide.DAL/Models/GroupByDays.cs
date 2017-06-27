using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportsSide.DAL.Models
{
    public class GroupByDays
    {
        public string DayName { get; set; }
        public int CountReservation { get; set; }
        public int CountSubscriber { get; set; }
    }
}