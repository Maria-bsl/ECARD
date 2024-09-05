// Decompiled with JetBrains decompiler
// Type: FUNDING.Controllers.Api.InviteesController
// Assembly: FUNDING, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5DE1C1E8-8D80-43BD-915D-F2502219BD58
// Assembly location: D:\ECardtest - 22082024\bin\FUNDING.dll

using ECARD.DL.EDMX;
using FUNDING.BL.BusinessEntities.Masters;
using FUNDING.Models.ApiModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Cors;

 
namespace FUNDING.Controllers.Api
{
  [EnableCors("*", "*", "*")]
  public class InviteesController : ApiBaseController
  {
    private readonly ECARDAPPEntities dbContext = new ECARDAPPEntities();
    private const int MAX_VISITOR_COUNT_REACHED = 0;

    [HttpPost]
    [Route("api/invitees/register")]
    public HttpResponseMessage RegisterInvitee([FromBody] Visitors visitor)
    {
      this.ModelState.Remove("mobile_no");
      if (!this.ModelState.IsValid)
        return this.Request.CreateResponse(HttpStatusCode.NotFound);
      visitor_details entity = new visitor_details()
      {
        event_det_sno = visitor.event_det_sno,
        cust_reg_sno = visitor.cust_reg_sno,
        visitor_name = visitor.visitor_name,
        card_state_mas_sno = visitor.card_state_mas_sno,
        no_of_persons = visitor.no_of_persons,
        table_number = visitor.Table_Number,
        mobile_no = visitor.mobile_no,
        email_address = visitor.email_address,
        posted_date = DateTime.Now,
        posted_by = visitor.posted_by
      };
      try
      {
        this.dbContext.visitor_details.Add(entity);
        this.dbContext.SaveChanges();
        entity.qrcode = Visitors.GetGeneratedQRCodeWithEventId(dbContext, entity.event_det_sno);
        this.dbContext.Entry<visitor_details>(entity).State = EntityState.Modified;
        this.dbContext.SaveChanges();
        return this.Request.CreateResponse(new
        {
          createStatus = true,
          response = "Invitee successful created."
        });
      }
      catch (Exception ex)
      {
                return this.Request.CreateResponse(new
                {
                    createStatus = false,
                    response = "Invitee creation failed." + ex.Message
                });
      }
    }

    [HttpGet]
    [Route("api/invitees/{event_id:long:min(1)}")]
    public HttpResponseMessage GetInvitees(long event_id)
    {
      if (!this.ModelState.IsValid)
        return this.Request.CreateResponse(HttpStatusCode.NotFound);
      var visitorsList = this.dbContext.visitor_details.Where<visitor_details>((Expression<Func<visitor_details, bool>>) (v => v.event_det_sno == (long?) event_id)).Select(vd => new
      {
        vd.visitor_det_sno,
        vd.visitor_name,
        vd.card_state_master.card_state,
        vd.no_of_persons,
        vd.mobile_no,
        vd.email_address,
        vd.table_number,
        qr_code = vd.qrcode.Substring(vd.qrcode.Length - 5)
      });
      var visitorsDetails = visitorsList;
      return this.Request.CreateResponse(HttpStatusCode.OK, new
      {
        status = 1,
        totalVisitor = Queryable.Sum(visitorsDetails.Select(iv => iv.no_of_persons).DefaultIfEmpty(new int?(0))),
        visitors = visitorsList
      });
    }

    [HttpPost]
    [Route("api/read-qr-code")]
    public HttpResponseMessage ReadQRCode([FromBody] InviteeQRCode invitee)
    {
      if (invitee == null)
        return this.Request.CreateResponse(HttpStatusCode.BadRequest, new
        {
          status = 0,
          message = "Failed! Invitee does not exist."
        });
      if (!this.ModelState.IsValid)
        return this.Request.CreateResponse(HttpStatusCode.BadRequest, new
        {
          status = 0,
          errorList = this.GetModelStateErrorList()
        });
      List<visitor_details> inviteesByQrCode = this.GetInviteesByQRCode(this.GetQrCodeWithEventId(invitee.Event_Id, invitee.QR_Code));
      if (inviteesByQrCode.Count<visitor_details>() <= 0)
        return this.Request.CreateResponse(HttpStatusCode.NotFound, new
        {
          status = 0,
          message = "Failed! Invitee does not exist."
        });
      int checkedInVisitor = this.GetCountOfAlreadyCheckedInInvitees(invitee.QR_Code);
      return this.Request.CreateResponse(HttpStatusCode.OK, inviteesByQrCode.Select(fv => new
      {
        event_id = fv.event_det_sno,
        visitor_name = fv.visitor_name,
        unchecked_invitee = fv.no_of_persons.Value - checkedInVisitor,
        table_number = fv.table_number
      }).Single());
    }

    [HttpPost]
    [Route("api/invitees-verification")]
    public HttpResponseMessage VerifyInvitees(InviteeCheckInDetails inviteeCheckinDetails)
    {
      if (inviteeCheckinDetails == null)
        return this.Request.CreateResponse(HttpStatusCode.BadRequest, new
        {
          status = 0,
          message = "Failed! Invitee does not exist."
        });
      if (this.ModelState.IsValid)
      {
        visitor_details usingTheSameCard1 = this.GetInviteesUsingTheSameCard(inviteeCheckinDetails);
        if (usingTheSameCard1 == null)
          return this.Request.CreateResponse(HttpStatusCode.NotFound, new
          {
            status = 0,
            message = "Failed! Invitee does not exist."
          });
        if (!this.IsTodaysEventDate(usingTheSameCard1))
          return this.Request.CreateResponse(HttpStatusCode.OK, new
          {
            status = 0,
            message = "Failed! Card verification must be on date of event."
          });
        IQueryable<qr_verify_details> usingTheSameCard2 = this.GetCheckedInInviteesUsingTheSameCard(inviteeCheckinDetails);
        if (usingTheSameCard2 != null)
        {
          int inInviteesPerCard = InviteesController.GetNumberOfCheckedInInviteesPerCard(usingTheSameCard2);
          int num1 = usingTheSameCard1.no_of_persons.Value - inInviteesPerCard;
          int num2 = inviteeCheckinDetails.Number_Of_CheckingIn_Invitees.Value;
          if (num1 == 0)
            return this.Request.CreateResponse(HttpStatusCode.OK, new
            {
              status = 0,
              message = "Failed! All invitees have checked-in already."
            });
          if (num2 > num1)
            return this.Request.CreateResponse(HttpStatusCode.OK, new
            {
              status = 0,
              message = "Failed! Number of visitor should not exceed limit."
            });
          this.SaveCheckedInviteeRecord(inviteeCheckinDetails, usingTheSameCard1);
          return this.Request.CreateResponse(HttpStatusCode.OK, new
          {
            status = 1,
            message = "Invitee has been successfully checked-in."
          });
        }
      }
      return this.Request.CreateResponse(HttpStatusCode.BadRequest, new
      {
        status = 0,
        errorList = this.GetModelStateErrorList()
      });
    }

    [HttpGet]
    [Route("api/checked-in-invitees/{event_id:long:min(1)}")]
    public HttpResponseMessage GetCheckedInInvitees(long event_id)
    {
      if (!this.ModelState.IsValid)
        return this.Request.CreateResponse(HttpStatusCode.NotFound);
      var qr_verify = this.dbContext.qr_verify_details.Where(iv => iv.event_det_sno == (long?) event_id).Select(iv => new
      {
        qr_ver_det_sno = iv.qr_ver_det_sno,
        visitor_name = iv.visitor_details.visitor_name,
        card_state = iv.card_state_master.card_state,
        no_of_persons = iv.no_of_persons,
        scan_status = iv.scan_status,
        posted_by = iv.posted_by,
        table_number = iv.visitor_details.table_number
      }).ToList();
      int index = 1;
      return this.Request.CreateResponse(HttpStatusCode.OK, new
      {
        number_of_verified_invitees = qr_verify.Select(iv => iv.no_of_persons).DefaultIfEmpty<int?>(new int?(0)).Sum(),
        verified_invitees = qr_verify.Select(vd => new
        {
          id = index++,
          visitor_name = vd.visitor_name,
          card_state = vd.card_state,
          no_of_persons = vd.no_of_persons,
          scan_status = this.GetScanStatus(vd.qr_ver_det_sno),
          scanned_by = this.dbContext.cust_users.Find(new object[1]
          {
            (object) Convert.ToInt64(vd.posted_by)
          }).user_fullname
        })
      });
    }

    private void SaveCheckedInviteeRecord(
      InviteeCheckInDetails inviteeCheckinDetails,
      visitor_details invitee)
    {
      this.dbContext.qr_verify_details.Add(new qr_verify_details()
      {
        visitor_det_sno = new long?(invitee.visitor_det_sno),
        event_det_sno = invitee.event_det_sno,
        cust_reg_sno = invitee.cust_reg_sno,
        qrcode = invitee.qrcode,
        event_start_time = invitee.event_details.event_start_time,
        card_state_mas_sno = invitee.card_state_mas_sno,
        no_of_persons = inviteeCheckinDetails.Number_Of_CheckingIn_Invitees,
        scan_status = this.GetScanStatus(),
        posted_date = DateTime.Now,
        posted_by = inviteeCheckinDetails.User_Id
      });
      this.dbContext.SaveChanges();
    }

    private bool IsTodaysEventDate(visitor_details visitor)
    {
      return DateTime.UtcNow.Date == Convert.ToDateTime((object) visitor.event_details.event_start_time).Date;
    }

    private visitor_details GetInviteesUsingTheSameCard(InviteeCheckInDetails invitee)
    {
      string qr_code = this.GetQrCodeWithEventId(invitee.Event_Id, invitee.QR_Code);
      return this.dbContext.visitor_details.Where<visitor_details>((Expression<Func<visitor_details, bool>>) (v => v.event_det_sno == invitee.Event_Id && v.qrcode == qr_code)).SingleOrDefault<visitor_details>();
    }

    private static int GetNumberOfCheckedInInviteesPerCard(IQueryable<qr_verify_details> invitees)
    {
      return Queryable.Sum(invitees.Select<qr_verify_details, int?>((Expression<Func<qr_verify_details, int?>>) (iv => iv.no_of_persons)).DefaultIfEmpty<int?>(new int?(0))).Value;
    }

    private IQueryable<qr_verify_details> GetCheckedInInviteesUsingTheSameCard(
      InviteeCheckInDetails invitee)
    {
      string qr_code = this.GetQrCodeWithEventId(invitee.Event_Id, invitee.QR_Code);
      return this.dbContext.qr_verify_details.Where<qr_verify_details>((Expression<Func<qr_verify_details, bool>>) (iv => iv.qrcode == qr_code));
    }

    private List<visitor_details> GetInviteesByQRCode(string qr_code)
    {
      return this.dbContext.visitor_details.Where(v => v.qrcode == qr_code.Trim()).ToList();
    }

    private string GetQrCodeWithEventId(long? event_id, string qr_code)
    {
      return "BIZ" + event_id.ToString().PadLeft(6, '0') + qr_code;
    }

    private visitor_details GetInviteeByIdAndQrCode(long? visitor_id, string qr_code)
    {
      return this.dbContext.visitor_details.Where(v => v.visitor_det_sno == visitor_id && v.qrcode == qr_code).FirstOrDefault();
    }

    private int GetCountOfAlreadyCheckedInInvitees(string qr_code)
    {
      return Queryable.Sum(this.dbContext.qr_verify_details.Where(v => v.qrcode == qr_code).Select(iv => iv.no_of_persons).DefaultIfEmpty(new int?(0))).Value;
    }

    private string GetScanStatus() => "Success";

        private string GetScanStatus(long qr_ver_det_sno)
        {
            //1. find the specific record
            var checkedInData = dbContext.qr_verify_details
                .Include("visitor_details").Where(c => c.qr_ver_det_sno == qr_ver_det_sno);

            if (checkedInData != null)
            {
                var qrVerified = checkedInData.ToList()[0];
                //.2 Get the number of people scanned
                int numberOfScannedPeople = Convert.ToInt32(qrVerified.no_of_persons);
                var qrCode = qrVerified.qrcode;
                int numberOfPeoplePerCard = Convert.ToInt32(qrVerified.visitor_details.no_of_persons);

                int TotalPeopleScanned = (int)dbContext.qr_verify_details
                    .Where(iv => iv.qrcode == qrCode && iv.qr_ver_det_sno < qr_ver_det_sno)
                    .Select(iv => iv.no_of_persons)
                    .DefaultIfEmpty(0)
                    .Sum();

                //3. Give informative status

                if (numberOfScannedPeople < (numberOfPeoplePerCard - TotalPeopleScanned))
                {
                    return string.Format("{0}/{1} Scanned", (TotalPeopleScanned + numberOfScannedPeople), numberOfPeoplePerCard);
                }

                return string.Format("{0}/{1} Scanned", numberOfPeoplePerCard, numberOfPeoplePerCard);
            }
            //4. No record is there

            return null;
        }

        protected override void Dispose(bool disposing)
    {
      if (disposing)
        this.dbContext.Dispose();
      base.Dispose(disposing);
    }
  }
}
