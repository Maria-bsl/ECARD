// Decompiled with JetBrains decompiler
// Type: FUNDING.Controllers.Api.webhook.StatusHandlerWebHookController
// Assembly: FUNDING, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5DE1C1E8-8D80-43BD-915D-F2502219BD58
// Assembly location: D:\ECardtest - 22082024\bin\FUNDING.dll

using ECARD.DL.EDMX;
using FUNDING.Models;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Twilio.TwiML;


namespace FUNDING.Controllers.Api.webhook
{
  public class StatusHandlerWebHookController : ApiBaseController
  {
       /* var client = new HttpClient();
        var url = "https://example.com/api/WhatsAppConfig";

        var content = new StringContent(JsonConvert.SerializeObject(whatsAppConfig), Encoding.UTF8, "application/json");

        var response = await client.PostAsync(url, content);*/

    [HttpPost]
    public IHttpActionResult HandleWebhook([FromBody] Twilio_notify_post payload)
    {
            if (payload == null)
            {
                return BadRequest();
            }
            try
            {

                using (ECARDAPPEntities _context = new ECARDAPPEntities())
                {

                    var webhookData = new twilio_notification_log
                    {
                        channel_prefix = payload.ChannelPrefix,
                        messaging_service_sid = payload.MessagingServiceSid,
                        api_version = payload.ApiVersion,
                        message_status = payload.MessageStatus,
                        sms_sid = payload.SmsSid,
                        sms_status = payload.SmsStatus,
                        channel_install_sid = payload.ChannelInstallSid,
                        whatsapp_to = payload.To,
                        whatsapp_from = payload.From,
                        messaging_sid = payload.MessageSid,
                        account_sid = payload.AccountSid,
                        channel_to_address = payload.ChannelToAddress
                    };

                    // Save the data to the database
                    _context.twilio_notification_log.Add(webhookData);
                     _context.SaveChangesAsync();


                    return Ok();
                }
            }
            catch(Exception ex)
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

    }

  

   

  }
}





