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
    
    public partial class customer_registration
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public customer_registration()
        {
            this.contributor_details = new HashSet<contributor_details>();
            this.cust_users = new HashSet<cust_users>();
            this.event_details = new HashSet<event_details>();
            this.qr_verify_details = new HashSet<qr_verify_details>();
            this.visitor_details = new HashSet<visitor_details>();
        }
    
        public long cust_reg_sno { get; set; }
        public string cust_name { get; set; }
        public string physical_address { get; set; }
        public string mobile_no { get; set; }
        public string email_address { get; set; }
        public string status { get; set; }
        public string posted_by { get; set; }
        public System.DateTime posted_date { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<contributor_details> contributor_details { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<cust_users> cust_users { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<event_details> event_details { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<qr_verify_details> qr_verify_details { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<visitor_details> visitor_details { get; set; }
    }
}
