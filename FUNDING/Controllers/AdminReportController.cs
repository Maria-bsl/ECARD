using ECARD.DL.EDMX;
using FUNDING.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace FUNDING.Controllers
{
    public class AdminReportController : Controller
    {
       

        private readonly ECARDAPPEntities _dbContext = new ECARDAPPEntities();

   // GET: AdminReport
        
        public ActionResult Index()
        {
            if (this.Session["admin1"] == null)
                return RedirectToAction("Login", "Login");


            long event_id = Convert.ToInt64(Session["EventID"]);
           ViewBag.eventId = event_id;

            //TODO: there must be a way to view only active events

            ViewBag.EventDropDownList = _dbContext.event_details.Where(e => e.event_date >= DateTime.Today)
                .Select(b => new SelectListItem
                {
                    Value = b.event_det_sno.ToString(),
                    Text = b.event_name
                });

            return View();
        }

        private long? GetEventID() => new long?(Convert.ToInt64(this.Session["EventID"]));

        #region FETCH MESSAGE

        /*  var client = new HttpClient();
          var request = new HttpRequestMessage(HttpMethod.Get, "https://api.twilio.com/2010-04-01/Accounts/AC14b36a565bc1c9863dea07a57161bdf2/Messages/MMf4d1e0b26ffb97d62f3515eb99f36734.json");
          request.Headers.Add("Authorization", "Basic QUMxNGIzNmE1NjViYzFjOTg2M2RlYTA3YTU3MTYxYmRmMjplOWRlNjYxMTY2N2I2MzlhNDgwYTIyNzQ5ZTQ4MzI4Zg==");
          var content = new StringContent("", null, "text/plain");
          request.Content = content;
          var response = await client.SendAsync(request);
          response.EnsureSuccessStatusCode();
          Console.WriteLine(await response.Content.ReadAsStringAsync());
         */
        #endregion


        private static async Task FetchMessageStatus(string messageSid, long? eventId)
        {

                ECARDAPPEntities context = new ECARDAPPEntities();
            await Task.WhenAll(new List<Task>()
            {Task.Run(async () =>
            {

                var auth = context.whatsapp_master.Where(w => w.status.ToLower().Equals("active")).OrderByDescending(r => r.effective_date).FirstOrDefault();

                if (auth != null)
                {
                    TwilioClient.Init(auth.account_sid, auth.auth_token);
                }
                else
                TwilioClient.Init("AC14b36a565bc1c9863dea07a57161bdf2", "e9de6611667b639a480a22749e48328f");


                var message = await MessageResource.FetchAsync(pathSid: messageSid);

                Console.WriteLine(message.Body);


                try
                {
                    var messageCheck = context.twilio_send_log.Where(t => t.event_det_sno == eventId.ToString() && t.sid == messageSid).FirstOrDefault();

                    //if (messageCheck.status.ToString().ToLower().Equals("accepted")){}

                    messageCheck.status = message.Status.ToString();
                    messageCheck.error_code = message.ErrorCode.ToString();
                    messageCheck.error_messages = message.ErrorMessage ?? "";
                    messageCheck.price = message.Price ?? "";
                    messageCheck.price_unit = message.PriceUnit ?? "";
                    messageCheck.date_updated = message.DateUpdated;
                    messageCheck.misc_column2 = "StatusChecked";

                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    var errorLog = new service_error_logs { error = ex.ToString(), posted_date = DateTime.Now };
                    context.service_error_logs.Add(errorLog);
                    context.SaveChanges();


                }

                })
           });


        }


        [HttpPost]
        public ActionResult GetDetails()
        {
            var eventId = Session["EventID"].ToString();
            var statusCheck = _dbContext.twilio_send_log.Where(v => v.event_det_sno == eventId && !v.status.ToString().ToLower().Equals("read") && !v.status.ToString().ToLower().Equals("failed") && !v.status.ToString().ToLower().Equals("delivered")).ToList();

            foreach (var keyitem in statusCheck)
            {

                _ = FetchMessageStatus(keyitem.sid, long.Parse(eventId));
            }

            try
            {
                var list = (from w in _dbContext.twilio_send_log
                                //join v in _dbContext.visitor_details on w.misc_column3 equals v.visitor_det_sno.ToString()
                            where w.event_det_sno == eventId
                            select new Twilio_send_log
                            {
                                sid = w.sid,
                                status = w.status,
                                //invitee_name = v.visitor_name,
                                event_det_sno = w.event_det_sno,
                                whatsapp_to = w.whatsapp_to,
                                date_created = w.date_created,
                                date_updated = w.date_updated,
                                direction = w.direction,
                                error_code = w.error_code ?? "",
                                error_messages = w.error_messages ?? "",
                                posted_date = w.posted_date,
                                price = w.price,
                                price_unit = w.price_unit,
                                misc_column1 = w.misc_column1,
                                misc_column2 = w.misc_column2 ?? "",
                                misc_column3 = w.misc_column3 ?? ""

                            }).ToList();

                var Details = (list != null && list.Count > 0) ? list : null;
                return Details != null ? Json(Details, JsonRequestBehavior.AllowGet) : Json(0, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.ToString(), JsonRequestBehavior.AllowGet);
            }


        }



    }
}