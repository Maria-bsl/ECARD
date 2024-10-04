using ECARD.DL.EDMX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FUNDING.Controllers
{
    public class TwilioWebhookController : Controller
    {
        [HttpPost]
        public ActionResult ReceiveNotification(FormCollection form)
        {
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


                    return  new HttpStatusCodeResult(200);
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
                    triggered = "Twilio POST",
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
                return new HttpStatusCodeResult(500);
                //return TaskContinuationOptions

            }
        }


        [HttpPost]
        public ActionResult HandleNotification(FormCollection form)
        {
            // Retrieve data from the incoming request (Twilio sends data via POST)
          
            // Get the necessary data from Twilio's POST request
         
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


                    return new HttpStatusCodeResult(200);
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
                return new HttpStatusCodeResult(500);
                //return TaskContinuationOptions

            }

            // Log the received data or process it as per your need
            // You can store it in the database or handle it in your application

            // Return an acknowledgment response
            // return Ok();
        }


        [HttpPost]
        public ActionResult WebhookHandler(FormCollection payload)
        {
            if (payload == null)
            {
                return new HttpStatusCodeResult(400);
            }
            try
            {
                using (ECARDAPPEntities _context = new ECARDAPPEntities())
                {

                    var webhookData = new webhook_error_log
                    {
                        parent_account_sid = payload["ParentAccountSid"],
                        misc_column1 = payload["Timestamp"],
                        payload = payload["Payload"],
                        payload_type = payload["PayloadType"],
                        sid = payload["Sid"],
                        level = payload["Level"],
                        posted_by = "Webhook Twilio",
                        posted_date = DateTime.Now,
                        account_sid = payload["AccountSid"],
                    };

                    // Save the data to the database
                    _context.webhook_error_log.Add(webhookData);
                    _context.SaveChangesAsync();


                    return new HttpStatusCodeResult(200);
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
                    triggered = "Twilio POST",
                    trycatch = "Yes",
                    posted_by = "Twilio",
                    misc_column1 = ex.ToString(),
                    posted_date = DateTime.Now
                };
                context.exception_error_logs.Add(errorLogs);
                context.SaveChanges();
                return new HttpStatusCodeResult(500);

            }

        }



    }
}