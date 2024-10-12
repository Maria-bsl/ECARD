using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FUNDING.Models
{
    public class Twilio_notification_log
    {

        public string whatsapp_from { get; set; }
        public string misc_column3 { get; set; }
        public string misc_column2 { get; set; }
        public string misc_column1 { get; set; }
        public string channel_prefix { get; set; }
        public string posted_by { get; set; }
        public DateTime? posted_date { get; set; }
        public string message_status { get; set; }
        public string messaging_service_sid { get; set; }
        public string channel_to_address { get; set; }
        public string channel_install_sid { get; set; }
        public string sms_status { get; set; }
        public DateTime? received_at { get; set; }
        public string api_version { get; set; }
        public string sms_sid { get; set; }
        public string account_sid { get; set; }
        public long sno { get; set; }
        public string whatsapp_to { get; set; }
        public string messaging_sid { get; set; }

    }
}