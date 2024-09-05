using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FUNDING.Models
{
    public class Webhook_payload
    {
        //public long sno { get; set; }
        public string AccountSid { get; set; }
        public string Sid { get; set; }
        public string ParentAccountSid { get; set; }
        public DateTime Timestamp { get; set; }
        public string Level { get; set; }
        public string PayloadType { get; set; }
        public string Payload { get; set; }
    }
}