

using ECARD.DL.EDMX;
using FUNDING.BL.BusinessEntities.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

 
namespace FUNDING.Controllers
{
  public class SMSEventsController : BaseController
  {
    private readonly ECARDAPPEntities _dbContext = new ECARDAPPEntities();
    private readonly CustomerUsers _users = new CustomerUsers();
    private SMS_INVITATION cy = new SMS_INVITATION();
    private Arights act = new Arights();

    public ActionResult SMSEvent()
    {
      if (this.Session["admin1"] == null)
        return (ActionResult) this.RedirectToAction("Login", "Login");
     
        #region Event dropdownlist

            //TODO: there must be a way to view only active events

            ViewBag.EventDropDownList = _dbContext.event_details.Where(e => e.event_date >= DateTime.Today)
                .Select(b => new SelectListItem
                {
                    Value = b.event_det_sno.ToString(),
                    Text = b.event_name
                });

            #endregion;

            return this.act.checkact("SMS Event Text", long.Parse(this.Session["Desg"].ToString()), this.Session["role"].ToString(), this.Session["Rcode"].ToString()) == 0L ? (ActionResult) this.RedirectToAction("Setup", "Setup") : (ActionResult) this.View();
    }

    [HttpPost]
    public ActionResult OnEventChangeAction(string Event_Id)
    {
      long event_id = Convert.ToInt64(Event_Id);
      List<visitor_details> list = this._dbContext.visitor_details.Where(v => v.event_det_sno == event_id && v.event_details.event_date >= (DateTime?) DateTime.Today).ToList();
      return list.Count() > 0 ? this.Json(new
      {
        status = true,
        data_value = list.Select(v => new
        {
          visitor_det_sno = v.visitor_det_sno,
          visitor_name = v.visitor_name
        })
      }) : (ActionResult) this.Json((object) new
      {
        status = false,
        response = "Event details does not exist"
      });
    }

    [HttpPost]
    public ActionResult AddSMSEvent(long Event_Id, string Message, long sno)
    {
      sms_invitation smsInvitation = this._dbContext.sms_invitation.Where(v => v.event_det_sno == Event_Id).FirstOrDefault();
      this.cy.sms_text = Message;
      this.cy.event_sno = new long?(Event_Id);
      this.cy.sno = sno;
      this.cy.posted_by = this.Session["UserID"].ToString();
      if (sno == 0L)
      {
        if (smsInvitation == null)
          return Json(cy.AddSMSEvents(cy), JsonRequestBehavior.AllowGet);
      }
      else if (sno > 0L && smsInvitation != null)
      {
        this.cy.UpdateSMSEvent(this.cy);
        return (ActionResult) this.Json((object) sno, JsonRequestBehavior.AllowGet);
      }
      return (ActionResult) null;
    }

    [HttpPost]
    public ActionResult GetSMSEventDetails()
    {
      try
      {
        var smsEvent = this.cy.GetSMSEvent();
        return smsEvent != null ? (ActionResult) this.Json((object) smsEvent, JsonRequestBehavior.AllowGet) : (ActionResult) this.Json((object) 0, JsonRequestBehavior.AllowGet);
      }
      catch (Exception ex)
      {
        return this.Json(ex.ToString(), JsonRequestBehavior.AllowGet);
      }
      
    }

    public ActionResult DeleteSMSEvent(int Sno)
    {
      try
      {
        if (Sno > 0)
        {
          this.cy.DeleteSMS((long) Sno);
          return (ActionResult) this.Json((object) Sno, JsonRequestBehavior.AllowGet);
        }
      }
      catch (Exception ex)
      {
        ex.Message.ToString();
      }
      return (ActionResult) null;
    }
  }
}
