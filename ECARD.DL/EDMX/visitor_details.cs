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
    
    public partial class visitor_details
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public visitor_details()
        {
            this.qr_verify_details = new HashSet<qr_verify_details>();
        }
    
        public long visitor_det_sno { get; set; }
        public Nullable<long> event_det_sno { get; set; }
        public Nullable<long> cust_reg_sno { get; set; }
        public string visitor_name { get; set; }
        public string qrcode { get; set; }
        public Nullable<long> card_state_mas_sno { get; set; }
        public Nullable<int> no_of_persons { get; set; }
        public string mobile_no { get; set; }
        public string email_address { get; set; }
        public string posted_by { get; set; }
        public System.DateTime posted_date { get; set; }
        public string table_number { get; set; }
        public string att_status { get; set; }
        public string designation { get; set; }
        public string company_name { get; set; }
    
        public virtual card_state_master card_state_master { get; set; }
        public virtual customer_registration customer_registration { get; set; }
        public virtual event_details event_details { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<qr_verify_details> qr_verify_details { get; set; }
    }
}