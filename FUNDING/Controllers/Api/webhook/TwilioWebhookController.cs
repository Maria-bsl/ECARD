using ECARD.DL.EDMX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;    
using Twilio.TwiML;

namespace FUNDING.Controllers.Api.webhook
{
    public class TwilioWebhookController : ApiController
    {
        // This is the method that Twilio will call
        [HttpPost]
        public IHttpActionResult HandleNotification()
        {
            // Retrieve data from the incoming request (Twilio sends data via POST)
            var form = HttpContext.Current.Request.Form;

            // Get the necessary data from Twilio's POST request
            string messageStatus = form["MessageStatus"];
            string messageSid = form["MessageSid"];
            string from = form["From"];
            string to = form["To"];

            try
            {

                using (ECARDAPPEntities _context = new ECARDAPPEntities())
                {

                    var webhookData = new twilio_notification_log
                    {
                        channel_prefix = form["ChannelPrefix"],
                        messaging_service_sid = form["MessagingServiceSid"],
                        api_version = form["ApiVersion"],
                        message_status = form["MessageStatus"],
                        sms_sid = form["SmsSid"],
                        sms_status = form["SmsStatus"],
                        channel_install_sid = form["ChannelInstallSid"],
                        whatsapp_to = form["To"],
                        whatsapp_from = form["From"],
                        messaging_sid = form["MessageSid"],
                        account_sid = form["AccountSid"],
                        channel_to_address = form["ChannelToAddress"]
                    };

                    // Save the data to the database
                    _context.twilio_notification_log.Add(webhookData);
                    _context.SaveChangesAsync();


                    return Ok();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Exception on DB: {ex}");
                System.Diagnostics.Debug.WriteLine($"Message : {ex.Message}");

                ECARDAPPEntities context = new ECARDAPPEntities();
                var errorLogs = new exception_error_logs
                {
                    exception = ex.InnerException.ToString(),
                    class_name = "Status Handler Webhook",
                    message = ex.Message,
                    method = ex.TargetSite.ToString(),
                    status = "Failed",
                    triggered = "Twilio GET",
                    trycatch = "Yes",
                    posted_by = "Twilio",
                    misc_column1 = ex.ToString(),
                    posted_date = DateTime.Now
                };
                context.exception_error_logs.Add(errorLogs);
                context.SaveChanges();

                var errorLog = new ErrorLog
                {
                    Message = ex.Message,
                    Source = ex.Source,
                    StackTrace = ex.StackTrace,
                    TargetSite = ex.TargetSite.ToString(),
                    InnerException = ex.InnerException.ToString(),
                    AuditDate = DateTime.Now,
                    RequestURL = "Status Handler Db",
                    BrowserType = "From Twilio"


                };
                context.ErrorLogs.Add(errorLog);
                context.SaveChanges();
                // ex.ToString();
                return Ok(ex.ToString());
                //return TaskContinuationOptions

            }

            // Log the received data or process it as per your need
            // You can store it in the database or handle it in your application

            // Return an acknowledgment response
           // return Ok();
        }

     


    }
}
