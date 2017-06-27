using SportsSide.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportsSideAdmin.Models
{
    public class ApproveModel
    {
        public PagedList.IPagedList<RESERVATION> Reservations { get; set; }
        public PagedList.IPagedList<SUBSCRIBER> Subscribers { get; set; }
        public PagedList.IPagedList<RESERVATION> AllReservations { get; set; }
        public PagedList.IPagedList<SUBSCRIBER> AllSubscibers { get; set; }
        public string selectedTab { get; set; }
    }
}