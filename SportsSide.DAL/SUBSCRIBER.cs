//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SportsSide.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class SUBSCRIBER
    {
        public int SUBSCRIBER_ID { get; set; }
        public int USER_ID { get; set; }
        public int FACILITY_ID { get; set; }
        public string WEEK_DAY { get; set; }
        public System.TimeSpan S_TIME { get; set; }
        public string STATUS { get; set; }
        public System.DateTime CREATED_DATE { get; set; }
        public string R_HISTORY { get; set; }
    
        public virtual FACILITY FACILITY { get; set; }
        public virtual USERS USERS { get; set; }
    }
}
