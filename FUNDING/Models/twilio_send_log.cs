using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FUNDING.Models
{
    public class twilio_send_log
    {

        public string misc_column1 { get; set; }
        public string posted_by { get; set; }
        public DateTime? posted_date { get; set; }
        public string uri { get; set; }
        public string subrecource_uris { get; set; }
        public string status { get; set; }
        public string price_unit { get; set; }
        public string price { get; set; }
        public string num_segment { get; set; }
        public string num_media { get; set; }
        public string messaging_service_sid { get; set; }
        public string whatsapp_to { get; set; }
        public string whatsapp_from { get; set; }
        public string error_messages { get; set; }
        public string error_code { get; set; }
        public string direction { get; set; }
        public DateTime? date_updated { get; set; }
        public DateTime? date_Sent { get; set; }
        public DateTime? date_created { get; set; }
        public string body { get; set; }
        public string apiversion { get; set; }
        public string sid { get; set; }
        public string accountsid { get; set; }
        public string event_det_sno { get; set; }
        public long sno { get; set; }
        public string misc_column2 { get; set; }
        public string misc_column3 { get; set; }
    }
}