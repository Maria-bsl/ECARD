﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ECARDAPPEntities : DbContext
    {
        public ECARDAPPEntities()
            : base("name=ECARDAPPEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<activity_master> activity_master { get; set; }
        public virtual DbSet<arights_master> arights_master { get; set; }
        public virtual DbSet<audit_log> audit_log { get; set; }
        public virtual DbSet<budget_master> budget_master { get; set; }
        public virtual DbSet<card_state_master> card_state_master { get; set; }
        public virtual DbSet<cash_collection> cash_collection { get; set; }
        public virtual DbSet<con_notificaiton_master> con_notificaiton_master { get; set; }
        public virtual DbSet<contributor_details> contributor_details { get; set; }
        public virtual DbSet<cust_users> cust_users { get; set; }
        public virtual DbSet<customer_registration> customer_registration { get; set; }
        public virtual DbSet<designation_list> designation_list { get; set; }
        public virtual DbSet<email_event_text> email_event_text { get; set; }
        public virtual DbSet<email_text> email_text { get; set; }
        public virtual DbSet<emp_detail> emp_detail { get; set; }
        public virtual DbSet<ErrorLog> ErrorLogs { get; set; }
        public virtual DbSet<event_details> event_details { get; set; }
        public virtual DbSet<expenditure_details> expenditure_details { get; set; }
        public virtual DbSet<localization_master> localization_master { get; set; }
        public virtual DbSet<modules_master> modules_master { get; set; }
        public virtual DbSet<payment_details> payment_details { get; set; }
        public virtual DbSet<qr_verify_details> qr_verify_details { get; set; }
        public virtual DbSet<roles_master> roles_master { get; set; }
        public virtual DbSet<s_e_w_event_send_details> s_e_w_event_send_details { get; set; }
        public virtual DbSet<service_error_logs> service_error_logs { get; set; }
        public virtual DbSet<sms_email_instances> sms_email_instances { get; set; }
        public virtual DbSet<sms_event_text> sms_event_text { get; set; }
        public virtual DbSet<sms_settings> sms_settings { get; set; }
        public virtual DbSet<sms_text> sms_text { get; set; }
        public virtual DbSet<smtp_settings> smtp_settings { get; set; }
        public virtual DbSet<table_details> table_details { get; set; }
        public virtual DbSet<temp_localization> temp_localization { get; set; }
        public virtual DbSet<track_details> track_details { get; set; }
        public virtual DbSet<visitor_details> visitor_details { get; set; }
        public virtual DbSet<wup_event_text> wup_event_text { get; set; }
        public virtual DbSet<card_measurement> card_measurement { get; set; }
        public virtual DbSet<country> countries { get; set; }
        public virtual DbSet<district_master> district_master { get; set; }
        public virtual DbSet<package_details> package_details { get; set; }
        public virtual DbSet<participant> participants { get; set; }
        public virtual DbSet<region_master> region_master { get; set; }
        public virtual DbSet<sms_contribution> sms_contribution { get; set; }
        public virtual DbSet<sms_invitation> sms_invitation { get; set; }
        public virtual DbSet<ward_master> ward_master { get; set; }
        public virtual DbSet<twilio_notification_log> twilio_notification_log { get; set; }
        public virtual DbSet<twilio_send_log> twilio_send_log { get; set; }
        public virtual DbSet<webhook_error_log> webhook_error_log { get; set; }
        public virtual DbSet<exception_error_logs> exception_error_logs { get; set; }
        public virtual DbSet<contents_template_master> contents_template_master { get; set; }
        public virtual DbSet<whatsapp_master> whatsapp_master { get; set; }
    }
}
