using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FUNDING.Models
{
    public class Twilio_notify_post
    {
        public string ChannelPrefix { get; set; }
        public string MessagingServiceSid { get; set; }
        public string ApiVersion { get; set; }
        public string MessageStatus { get; set; }
        public string SmsSid { get; set; }
        public string SmsStatus { get; set; }
        public string ChannelInstallSid { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public string MessageSid { get; set; }
        public string AccountSid { get; set; }
        public string ChannelToAddress { get; set; }
    }
}