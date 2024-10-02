using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FUNDING.Models.WhatsApp
{
    public class WhatsAppConfig
    {
        public long Cont_mas_sno { get; set; }
        public string Messaging_service_sid { get; set; }
        public string Content_sid { get; set; }
        public string Content_name { get; set; }
        public string Content_code { get; set; }
        public string Content_variables { get; set; }
        public string Status { get; set; }
        public DateTime? Posted_date { get; set; }
        public string Posted_by { get; set; }
        public string Misc_column1 { get; set; }
        public string Misc_column2 { get; set; }
        public long Sno { get; set; }
    }
}