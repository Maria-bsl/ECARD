// Decompiled with JetBrains decompiler
// Type: FUNDING.Controllers.Api.TwilioWebhookHandler
// Assembly: FUNDING, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5DE1C1E8-8D80-43BD-915D-F2502219BD58
// Assembly location: D:\ECardtest - 22082024\bin\FUNDING.dll

using ECARD.DL.EDMX;
using FUNDING.Models;
using Microsoft.AspNet.WebHooks;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;


namespace FUNDING.Controllers.Api
{
    public class TwilioWebhookHandler : ApiBaseController
    {
        public static string ReceiverName { get; set; }


        [HttpGet]
        public async Task<IHttpActionResult> TwilioWebhookHandlerAsync([FromBody] Webhook_payload payload)
        {
            if (payload == null)
            {
                return BadRequest("Invalid payload.");
            }
            try
            {
                using (ECARDAPPEntities _context = new ECARDAPPEntities())
                {

                    var webhookData = new webhook_error_log
                    {
                        parent_account_sid = payload.ParentAccountSid,
                        received_at = payload.Timestamp,
                        payload = payload.Payload,
                        payload_type = payload.PayloadType,
                        sid = payload.Sid,
                        level = payload.Level,
                        posted_by = "Webhook Twilio",
                        posted_date = DateTime.Now,
                        account_sid = payload.AccountSid,
                    };

                    // Save the data to the database
                    _context.webhook_error_log.Add(webhookData);
                    await _context.SaveChangesAsync();


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
                return Ok();

            }

        }

       /* public TwilioWebhookHandler() => this.Receiver = TwilioWebhookHandler.ReceiverName;

        [HttpGet]
        public override Task ExecuteAsync(string receiver, WebHookHandlerContext context)
        {
            context.GetDataOrDefault<JObject>();
            context.Actions.FirstOrDefault<string>();
            return (Task)Task.FromResult<bool>(true);
        }*/
    }
}
