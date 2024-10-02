using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FUNDING.Models
{
    public class Webhook_error_log
    {

        public long sno { get; set; }
        public string account_sid { get; set; }
        public string sid { get; set; }
        public string parent_account_sid { get; set; }
        public DateTime? received_at { get; set; }
        public string level { get; set; }
        public string payload_type { get; set; }
        public string payload { get; set; }
        public DateTime? posted_date { get; set; }
        public string posted_by { get; set; }
        public string misc_column1 { get; set; }
        public string misc_column2 { get; set; }
        public DateTime? misc_column3 { get; set; }
    }
}