using SportsSide.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace sahakirala.Models
{
    public class ReservationBigModel
    {
        public FACILITY Facility { get; set; }
        public IEnumerable<RESERVATION> Reservations { get; set; }
        public IEnumerable<SUBSCRIBER> Subscribers { get; set; }
        public IEnumerable<FACILITY_PICTURES> FacilityPictures { get; set; }
        public DateTime Date { get; set; }
        public string Hour { get; set; }
        [Required(ErrorMessage = "Kişi sayısı girilmesi zorunludur")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Kişi sayısı minimum 1 olabilir.")]
        public int Count { get; set; }
        public string isProblem { get; set; }
    }
}