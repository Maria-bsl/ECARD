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
    
    public partial class event_details
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public event_details()
        {
            this.budget_master = new HashSet<budget_master>();
            this.cash_collection = new HashSet<cash_collection>();
            this.con_notificaiton_master = new HashSet<con_notificaiton_master>();
            this.contributor_details = new HashSet<contributor_details>();
            this.email_event_text = new HashSet<email_event_text>();
            this.expenditure_details = new HashSet<expenditure_details>();
            this.qr_verify_details = new HashSet<qr_verify_details>();
            this.s_e_w_event_send_details = new HashSet<s_e_w_event_send_details>();
            this.sms_event_text = new HashSet<sms_event_text>();
            this.visitor_details = new HashSet<visitor_details>();
            this.wup_event_text = new HashSet<wup_event_text>();
            this.card_measurement = new HashSet<card_measurement>();
            this.sms_contribution = new HashSet<sms_contribution>();
            this.sms_invitation = new HashSet<sms_invitation>();
        }
    
        public long event_det_sno { get; set; }
        public Nullable<long> cust_reg_sno { get; set; }
        public string inviter_name { get; set; }
        public string event_name { get; set; }
        public Nullable<System.DateTime> event_date { get; set; }
        public Nullable<System.DateTime> event_start_time { get; set; }
        public string venue { get; set; }
        public string reservation { get; set; }
        public string event_status { get; set; }
        public string remarks { get; set; }
        public string posted_by { get; set; }
        public System.DateTime posted_date { get; set; }
        public string visitor_confirmation { get; set; }
        public Nullable<int> no_of_sms { get; set; }
        public Nullable<int> no_of_email { get; set; }
        public Nullable<int> no_of_wup { get; set; }
        public Nullable<long> pack_det_sno { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<budget_master> budget_master { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<cash_collection> cash_collection { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<con_notificaiton_master> con_notificaiton_master { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<contributor_details> contributor_details { get; set; }
        public virtual customer_registration customer_registration { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<email_event_text> email_event_text { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<expenditure_details> expenditure_details { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<qr_verify_details> qr_verify_details { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<s_e_w_event_send_details> s_e_w_event_send_details { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<sms_event_text> sms_event_text { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<visitor_details> visitor_details { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<wup_event_text> wup_event_text { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<card_measurement> card_measurement { get; set; }
        public virtual package_details package_details { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<sms_contribution> sms_contribution { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<sms_invitation> sms_invitation { get; set; }
    }
}