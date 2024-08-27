// Decompiled with JetBrains decompiler
// Type: FUNDING.Controllers.Api_Old.VisitorApiController
// Assembly: FUNDING, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5DE1C1E8-8D80-43BD-915D-F2502219BD58
// Assembly location: D:\ECardtest - 22082024\bin\FUNDING.dll

using ECARD.DL.EDMX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

 
namespace FUNDING.Controllers.Api_Old
{
  public class VisitorApiController : Controller
  {
    private readonly ECARDAPPEntities _context = new ECARDAPPEntities();
    private const int MAX_VISITOR_COUNT_REACHED = 0;

    [HttpPost]
    public JsonResult ReadQRCode(string qr_code)
    {
      if (string.IsNullOrEmpty(qr_code))
        return this.Json((object) new
        {
          status = 0,
          message = "Failed! Invitee does not exist."
        });
      var visitor = this._context.visitor_details.Where<visitor_details>((Expression<Func<visitor_details, bool>>) (v => v.qrcode == qr_code)).Select(v => new
      {
        visitor_id = v.visitor_det_sno,
        visitorName = v.visitor_name,
        numberOfVisitorPerCard = v.no_of_persons
      }).ToList();
      if (visitor.Count() == 0)
        return this.Json((object) new
        {
          status = 0,
          message = "Failed! Invitee does not exist."
        });
      int checkedInVisitor = Queryable.Sum(this._context.qr_verify_details.Where<qr_verify_details>((Expression<Func<qr_verify_details, bool>>) (v => v.qrcode == qr_code)).Select<qr_verify_details, int?>((Expression<Func<qr_verify_details, int?>>) (iv => iv.no_of_persons)).DefaultIfEmpty<int?>(new int?(0))).Value;
      return this.Json((object) visitor.Select(fv => new
      {
        visitor_id = fv.visitor_id,
        visitorName = fv.visitorName,
        numberOfVisitorPerCard = fv.numberOfVisitorPerCard.Value - checkedInVisitor
      }));
    }

        [HttpPost]
        public JsonResult VerifyInvitees(long? visitor_id, string qr_code, int? number_of_visitors, string customer_id)
        {
            #region Paramater validations
            /*
             * The following condition statements verify if the parameter required for this method are valid.
             * 1.Check if visitor_id is not null.
             * 2.Check if qr_code is not null or empty.
             * 3.Check if number_of_visitors is not null
             * 4.Check if number_of_visitors is at least one.
             * 5.Check if customer_id is not null
             */
            if (visitor_id == null)
            {
                return Json(new { status = 0, message = "Failed! Visitor id is required." });
            }

            if (string.IsNullOrEmpty(qr_code))
            {
                return Json(new { status = 0, message = "Failed! QR code identity is required." });
            }

            if (number_of_visitors == null)
            {
                return Json(new { status = 0, message = "Failed! Number of visitor is required." });
            }
            if (number_of_visitors <= 0)
            {
                return Json(new { status = 0, message = "Failed! Number of visitors should be atleast one." });
            }

            if (string.IsNullOrEmpty(customer_id))
            {
                return Json(new { status = 0, message = "Failed! Event Admin Id is required." });
            }

            #endregion;

            var visitor = _context.visitor_details
                .Where(v => v.visitor_det_sno == visitor_id && v.qrcode == qr_code);

            //Card verification process should only take place at the date of event.
            var todayDate = DateTime.UtcNow.Date;
            var eventDate = Convert.ToDateTime(visitor.ToList()[0].event_details.event_start_time).Date;

            if (todayDate == eventDate)
            {
                if (visitor != null)
                {
                    //2. Check if either number_of_visitors or customer_id not null
                    var invitees = _context.qr_verify_details.Where(iv => iv.qrcode == qr_code);

                    if (invitees != null)
                    {
                        int checked_In_Inivitees = (int)invitees
                            .Select(iv => iv.no_of_persons)
                            .DefaultIfEmpty(0)
                            .Sum();

                        int invitees_per_card = (int)visitor.Select(v => v.no_of_persons).DefaultIfEmpty(0).Sum();

                        var numberRemainToCheckIn = invitees_per_card - checked_In_Inivitees;

                        var numberOfVisitors = Convert.ToInt32(number_of_visitors);

                        if (numberRemainToCheckIn == MAX_VISITOR_COUNT_REACHED)
                        {
                            return Json(new { status = 0, message = "Failed! All guests have checked-in already." });
                        }

                        if (numberOfVisitors <= numberRemainToCheckIn)
                        {
                            var inviteeDetails = visitor.FirstOrDefault();

                            var verifyGuest = new qr_verify_details
                            {
                                visitor_det_sno = inviteeDetails.visitor_det_sno,
                                event_det_sno = inviteeDetails.event_det_sno,
                                cust_reg_sno = inviteeDetails.cust_reg_sno,
                                qrcode = inviteeDetails.qrcode,
                                event_start_time = inviteeDetails.event_details.event_start_time,
                                card_state_mas_sno = inviteeDetails.card_state_mas_sno,
                                no_of_persons = number_of_visitors,
                                scan_status = GetScanStatus(),
                                posted_date = DateTime.Now,
                                posted_by = customer_id

                            };

                            _context.qr_verify_details.Add(verifyGuest);
                            _context.SaveChanges();

                            return Json(new { status = 1, message = "Guest has been successfully checked-in." });
                        }

                        return Json(new { status = 0, message = "Failed! Number of visitor should not exceed limit." });
                    }
                }

                return Json(new { status = 0, message = "Failed! Invitee does not exist." });
            }

            return Json(new { status = 0, message = "Failed! Card verification must be on date of event." });
        }


        [HttpGet]
    public JsonResult CheckedInVisitors(long event_id)
    {
      var qr = this._context.qr_verify_details.Where<qr_verify_details>((Expression<Func<qr_verify_details, bool>>) (iv => iv.event_det_sno == (long?) event_id)).Select(iv => new
      {
        qr_ver_det_sno = iv.qr_ver_det_sno,
        visitor_name = iv.visitor_details.visitor_name,
        card_state = iv.card_state_master.card_state,
        no_of_persons = iv.no_of_persons,
        scan_status = iv.scan_status
      }).ToList();
      int index = 1;
      var visitor = qr.Select(vd => new
      {
        id = index++,
        visitor_name = vd.visitor_name,
        card_state = vd.card_state,
        no_of_persons = vd.no_of_persons,
        scan_status = this.GetScanStatus(vd.qr_ver_det_sno)
      });
      return this.Json((object) new
      {
        verified_visitor_count = visitor.Select(iv => iv.no_of_persons).DefaultIfEmpty<int?>(new int?(0)).Sum(),
        verified_visitors = visitor
      }, JsonRequestBehavior.AllowGet);
    }

    [HttpGet]
    public JsonResult AllVisitors(long event_id)
    {
            var invitees = _context.visitor_details
                      .Where(iv => iv.event_det_sno == event_id).Select(
                      iv => new
                      {
                          iv.visitor_name,
                          iv.card_state_master.card_state,
                          iv.no_of_persons,
                      });

            return Json(new
            {
                invitees_count = invitees.Select(iv => iv.no_of_persons).DefaultIfEmpty(0).Sum(),
                invitees = invitees.ToList(),
            }, JsonRequestBehavior.AllowGet);
        }

    private string GetScanStatus(long qr_ver_det_sno)
    {
      IQueryable<qr_verify_details> source = this._context.qr_verify_details.Include("visitor_details").Where<qr_verify_details>((Expression<Func<qr_verify_details, bool>>) (c => c.qr_ver_det_sno == qr_ver_det_sno));
      if (source == null)
        return (string) null;
      qr_verify_details qrVerifyDetails = source.ToList<qr_verify_details>()[0];
      int int32_1 = Convert.ToInt32((object) qrVerifyDetails.no_of_persons);
      string qrCode = qrVerifyDetails.qrcode;
      int int32_2 = Convert.ToInt32((object) qrVerifyDetails.visitor_details.no_of_persons);
      int num = Queryable.Sum(this._context.qr_verify_details.Where<qr_verify_details>((Expression<Func<qr_verify_details, bool>>) (iv => iv.qrcode == qrCode && iv.qr_ver_det_sno < qr_ver_det_sno)).Select<qr_verify_details, int?>((Expression<Func<qr_verify_details, int?>>) (iv => iv.no_of_persons)).DefaultIfEmpty<int?>(new int?(0))).Value;
      return int32_1 < int32_2 - num ? string.Format("{0}/{1} Scanned", (object) (num + int32_1), (object) int32_2) : string.Format("{0}/{1} Scanned", (object) int32_2, (object) int32_2);
    }

    private string GetScanStatus() => "Success";

    protected override void Dispose(bool disposing)
    {
      if (disposing)
        this._context.Dispose();
      base.Dispose(disposing);
    }
  }
}
