//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ECARD.DL.EDMX
{
    using System;
    using System.Collections.Generic;
    
    public partial class s_e_w_event_send_details
    {
        public long s_e_s_det_sno { get; set; }
        public Nullable<long> event_det_sno { get; set; }
        public Nullable<long> visitor_det_sno { get; set; }
        public string sms_text { get; set; }
        public string email_text { get; set; }
        public string wup_text { get; set; }
        public Nullable<int> sms_sent { get; set; }
        public Nullable<int> email_sent { get; set; }
        public Nullable<int> wup_sent { get; set; }
        public string posted_by { get; set; }
        public Nullable<System.DateTime> posted_date { get; set; }
    
        public virtual event_details event_details { get; set; }
    }
}
