using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SportsSide.DAL;

namespace sahakirala.Models
{
    public class MemberBigModel
    {
        public USERS User { get; set; }
        public PagedList.IPagedList<RESERVATION> LiveReservations { get; set; }
        public DateTime Date { get; set; }
        public int Hour { get; set; }
        public PagedList.IPagedList<RESERVATION> oldReservations { get; set; }
        public PagedList.IPagedList<SUBSCRIBER> Subscribers { get; set; }
        public string selectedTab { get; set; }
    }
}