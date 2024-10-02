using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FUNDING.Models.WhatsApp
{
    public class WhatsAppMaster
    {
        public long Whats_mas_sno { get; set; }
        public string Account_sid { get; set; }
        public string Auth_token { get; set; }
        public string Whatsapp_sender { get; set; }
        public string Status { get; set; }
        public string Callback_url { get; set; }
        public string Webhook_incoming { get; set; }
        public string Webhook_error_log { get; set; }
        public string Misc_column3 { get; set; }
        public DateTime? Posted_date { get; set; }
        public string Posted_by { get; set; }
        public string Misc_column1 { get; set; }
        public string Misc_column2 { get; set; }
        public int Sno { get;  set; }
        public string Effective_date { get; set; }
    }
}