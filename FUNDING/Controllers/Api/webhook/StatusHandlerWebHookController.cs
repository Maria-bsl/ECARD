// Decompiled with JetBrains decompiler
// Type: FUNDING.Controllers.Api.webhook.StatusHandlerWebHookController
// Assembly: FUNDING, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5DE1C1E8-8D80-43BD-915D-F2502219BD58
// Assembly location: D:\ECardtest - 22082024\bin\FUNDING.dll

using ECARD.DL.EDMX;
using FUNDING.Models;
using System;
using System.Threading.Tasks;
using System.Web.Http;

 
namespace FUNDING.Controllers.Api.webhook
{
  public class StatusHandlerWebHookController : ApiBaseController
  {

    [HttpPost]
    public async Task<IHttpActionResult> HandleWebhookAsync([FromBody] Twilio_notify_post payload)
    {
            if (payload == null)
            {
                return BadRequest("Invalid payload.");
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
                    await _context.SaveChangesAsync();


                    return Ok("Webhook data saved successfully.");
                }
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Exception on DB: {ex}");
                System.Diagnostics.Debug.WriteLine($"Message : {ex.Message}");

                ECARDAPPEntities context = new ECARDAPPEntities();
                /*var errorLogs = new service_error_logs
                {
                    error = ex.ToString(),
                    posted_date = DateTime.Now
                };
                context.service_error_logs.Add(errorLogs);
                context.SaveChanges();*/

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

    private void LogWebhookPayload(object payload)
    {

    }
  }
}
