
using ECARD.DL.EDMX;
using FUNDING.BL.BusinessEntities.Masters;
using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Web.Mvc;

 
namespace FUNDING.Controllers
{
  public class EventsController : Controller
  {
    private readonly Events _event = new Events();
    private readonly ECARDAPPEntities _dbContext = new ECARDAPPEntities();

    [Route("Event-Details")]
    public ActionResult Index()
    {
      if (this.Session["admin1"] as string == CustomerUsers.EventAdminUserType)
        return (ActionResult) this.View((object) this.GetCustomerEventDetails());
      return this.Session["admin1"] as string == CustomerUsers.NormalUserType ? (ActionResult) this.View("Index_readonly") : (ActionResult) this.RedirectToAction("CustomerLogin", "Login");
    }

    public ActionResult IndexDataTable()
    {
      long eventAdmin = Convert.ToInt64(this.Session["EventAdminID"]);
      long eventID = Convert.ToInt64(this.Session["EventID"]);
      var eventData = this._dbContext.event_details.Where(e => e.cust_reg_sno == eventAdmin && e.event_det_sno == eventID).Select(e => new
      {
        e.event_det_sno,
        e.inviter_name,
        e.event_name,
        e.event_date,
        e.event_start_time,
        e.venue,
        e.reservation,
        e.event_status,
        e.remarks
      }).ToList();
      int index = 1;
      return Json(new
      {
        data = eventData.Select(e =>
        {
          int num = index++;
          long eventDetSno = e.event_det_sno;
          string inviterName = e.inviter_name;
          string eventName = e.event_name;
          DateTime dateTime = Convert.ToDateTime((object) e.event_date);
          string str1 = dateTime.ToString("dddd, dd MMMM yyyy");
          dateTime = Convert.ToDateTime((object) e.event_start_time);
          string str2 = dateTime.ToString("hh:mm tt");
          string venue = e.venue;
          string reservation = e.reservation;
          string str3 = this.DynamicEventStatus(Convert.ToDateTime((object) e.event_start_time));
          string remarks = e.remarks;
          return new
          {
            id = num,
            event_det_sno = eventDetSno,
            inviter_name = inviterName,
            event_name = eventName,
            event_date = str1,
            event_start_time = str2,
            venue = venue,
            reservation = reservation,
            event_status = str3,
            remarks = remarks
          };
        })
      });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult AjaxHelperCreateForm([Bind(Include = "event_det_sno,inviter_name,event_name,venue,reservation,remarks")] Events eventDetails)
    {
      if (this.ModelState.IsValid)
      {
        DateTime? nullable = eventDetails.event_date;
        DateTime dateTime1 = nullable.Value;
        nullable = eventDetails.event_start_time;
        DateTime dateTime2 = nullable.Value;
        string shortDateString = dateTime1.ToShortDateString();
        string shortTimeString = dateTime2.ToShortTimeString();
        eventDetails.event_start_time = new DateTime?(Convert.ToDateTime(shortDateString + " " + shortTimeString));
        this._dbContext.event_details.Add(new event_details()
        {
          cust_reg_sno = new long?(this.GetEventStatusCustomerID()),
          inviter_name = eventDetails.inviter_name,
          event_name = eventDetails.event_name,
          event_date = eventDetails.event_date,
          event_start_time = eventDetails.event_start_time,
          venue = eventDetails.venue,
          reservation = eventDetails.reservation,
          remarks = eventDetails.remarks,
          event_status = this.GetEventStatus(),
          posted_date = DateTime.Now,
          posted_by = this.GetPostedBy()
        });
        this._dbContext.SaveChanges();
        return (ActionResult) this.Json((object) new
        {
          createStatus = true,
          response = "Record successful created."
        });
            }
            ViewBag.cust_reg_sno = new SelectList(_dbContext.customer_registration, "cust_reg_sno", "cust_name");

            return (ActionResult) this.PartialView("_AjaxHelperCreateForm", (object) eventDetails);
    }

    [ChildActionOnly]
    public ActionResult AjaxHelperUpdateForm()
    {
      return (ActionResult) this.PartialView("_AjaxHelperUpdateFormView", (object) this.GetCustomerEventDetails());
    }

    private Events GetCustomerEventDetails()
    {
      event_details eventDetails = this._dbContext.event_details.Find(new object[1]
      {
        (object) Convert.ToInt64(this.Session["EventID"])
      });
      return new Events()
      {
        event_det_sno = eventDetails.event_det_sno,
        inviter_name = eventDetails.inviter_name,
        event_name = eventDetails.event_name,
        Event_Date_String = Convert.ToDateTime((object) eventDetails.event_date).ToString("dddd, dd MMMM yyyy"),
        Event_Start_Time_String = Convert.ToDateTime((object) eventDetails.event_start_time).ToString("hh:mm tt"),
        venue = eventDetails.venue,
        reservation = eventDetails.reservation,
        remarks = eventDetails.remarks
      };
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult AjaxHelperUpdateForm([Bind(Include = "event_det_sno,inviter_name,event_name,venue,reservation,remarks")] Events eventDetails)
    {
      long int64 = Convert.ToInt64(this.Session["EventID"]);
      this.ModelState.Remove("event_date");
      this.ModelState.Remove("event_start_time");
      if (this.ModelState.IsValid)
      {
        event_details entity = this._dbContext.event_details.Find(new object[1]
        {
          (object) eventDetails.event_det_sno
        });
        if (entity == null && eventDetails.event_det_sno != int64)
          return (ActionResult) this.Json((object) new
          {
            updateStatus = false,
            response = "Failed! Item does not exist."
          });
        entity.inviter_name = eventDetails.inviter_name;
        entity.event_name = eventDetails.event_name;
        entity.venue = eventDetails.venue;
        entity.reservation = eventDetails.reservation;
        entity.remarks = eventDetails.remarks;
        entity.event_status = this.GetEventStatus();
        entity.posted_date = DateTime.Now;
        entity.posted_by = this.GetPostedBy();
        this._dbContext.Entry<event_details>(entity).State = EntityState.Modified;
        this._dbContext.Entry<event_details>(entity).Property("cust_reg_sno").IsModified = false;
        this._dbContext.Entry<event_details>(entity).Property("event_date").IsModified = false;
        this._dbContext.Entry<event_details>(entity).Property("event_start_time").IsModified = false;
        this._dbContext.SaveChanges();
        return (ActionResult) this.Json((object) new
        {
          updateStatus = true,
          response = "Record successful updated.",
          eventID = int64
        });
            }
            ViewBag.cust_reg_sno = new SelectList(_dbContext.customer_registration, "cust_reg_sno", "cust_name", eventDetails.cust_reg_sno);
            return (ActionResult) this.PartialView("_AjaxHelperUpdateForm", (object) eventDetails);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult AjaxHelperDeleteForm(long? id)
    {
      if (!id.HasValue)
        return (ActionResult) this.Json((object) new
        {
          deleteStatus = false,
          response = "Failed! ID not supplied"
        });
      event_details entity = this._dbContext.event_details.Find(new object[1]
      {
        (object) id
      });
      if (entity == null)
        return (ActionResult) this.Json((object) new
        {
          deleteStatus = false,
          response = "Failed! Item does not exist"
        });
      try
      {
        this._dbContext.event_details.Remove(entity);
        this._dbContext.SaveChanges();
      }
      catch (Exception ex)
      {
        return (ActionResult) this.Json((object) new
        {
          deleteStatus = false,
          response = "Record cannot be delete, it is being used by other records."
        });
      }
      return (ActionResult) this.Json((object) new
      {
        deleteStatus = true,
        response = "Record successful deleted."
      });
    }

    [HttpPost]
    public ActionResult AjaxHelperEventUpdateForm([Bind(Include = "event_det_sno,inviter_name,event_name,event_date,event_start_time,venue,reservation,remarks")] Events eventDetails)
    {
      if (this.ModelState.IsValid)
      {
        long int64 = Convert.ToInt64(this.Session["EventID"]);
        event_details entity = this._dbContext.event_details.Find(new object[1]
        {
          (object) eventDetails.event_det_sno
        });
        if (entity == null && eventDetails.event_det_sno != int64)
          return (ActionResult) this.Json((object) new
          {
            updateStatus = false,
            response = "Failed! Item does not exist."
          });
        DateTime? nullable = eventDetails.event_date;
        DateTime dateTime1 = nullable.Value;
        nullable = eventDetails.event_start_time;
        DateTime dateTime2 = nullable.Value;
        string shortDateString = dateTime1.ToShortDateString();
        string shortTimeString = dateTime2.ToShortTimeString();
        eventDetails.event_start_time = new DateTime?(Convert.ToDateTime(shortDateString + " " + shortTimeString));
        entity.inviter_name = eventDetails.inviter_name;
        entity.event_name = eventDetails.event_name;
        entity.event_date = eventDetails.event_date;
        entity.event_start_time = eventDetails.event_start_time;
        entity.venue = eventDetails.venue;
        entity.reservation = eventDetails.reservation;
        entity.remarks = eventDetails.remarks;
        entity.event_status = this.GetEventStatus();
        entity.posted_date = DateTime.Now;
        entity.posted_by = this.GetPostedBy();
        this._dbContext.Entry<event_details>(entity).State = EntityState.Modified;
        this._dbContext.Entry<event_details>(entity).Property("cust_reg_sno").IsModified = false;
        this._dbContext.SaveChanges();
        return (ActionResult) this.Json((object) new
        {
          updateStatus = true,
          response = "Record successful updated.",
          eventID = int64
        });
      }
            ViewBag.cust_reg_sno = new SelectList(_dbContext.customer_registration, "cust_reg_sno", "cust_name", eventDetails.cust_reg_sno);


            return (ActionResult) this.PartialView("_AjaxHelperEventUpdateForm", (object) eventDetails);
    }

    private long GetEventStatusCustomerID() => Convert.ToInt64(this.Session["EventAdminID"]);

    private string GetPostedBy() => this.Session["UfullName"].ToString();

    private string GetEventStatus() => "Pending";

    private string DynamicEventStatus(DateTime customerWithEvent)
    {
      DateTime date = DateTime.Now.Date;
      double totalDays = Convert.ToDateTime(customerWithEvent).Date.Subtract(date).TotalDays;
      if (totalDays > 0.0)
        return totalDays.ToString() + " days to event";
      return totalDays == 0.0 ? "Active" : "Closed";
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
        this._dbContext.Dispose();
      base.Dispose(disposing);
    }
  }
}
