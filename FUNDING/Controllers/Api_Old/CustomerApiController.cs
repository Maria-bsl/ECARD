// Decompiled with JetBrains decompiler
// Type: FUNDING.Controllers.Api_Old.CustomerApiController
// Assembly: FUNDING, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5DE1C1E8-8D80-43BD-915D-F2502219BD58
// Assembly location: D:\ECardtest - 22082024\bin\FUNDING.dll

using ECARD.DL.EDMX;
using FUNDING.BL.BusinessEntities.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;

 
namespace FUNDING.Controllers.Api_Old
{
  public class CustomerApiController : Controller
  {
    private readonly ECARDAPPEntities _context = new ECARDAPPEntities();
    private string _mobileNumber;

    [HttpPost]
    public JsonResult CustomerLogin(string mobileNumber, string password)
    {
      string encryptedData = this.GetEncryptedData(password);
      List<event_details> eventDetailsList = CustomerUsers.EventsEntitled(mobileNumber, encryptedData);
      if (eventDetailsList == null)
        return this.Json((object) new
        {
          status = 0,
          message = "Invalid mobile number or password"
        });
      List<object> objectList = new List<object>();
      foreach (event_details eventDetails in eventDetailsList)
        objectList.Add((object) new
        {
          event_det_sno = eventDetails.event_det_sno,
          event_name = eventDetails.event_name
        });
      this._mobileNumber = mobileNumber;
      return this.Json((object) new
      {
        status = 1,
        eventDetails = objectList,
        mobileNumber = mobileNumber
      });
    }

    [HttpPost]
    public JsonResult EventOfChoice(string eventOfChoice, string mobileNumber)
    {
      if (string.IsNullOrEmpty(mobileNumber))
        return this.Json((object) new
        {
          status = 0,
          message = "Mobile number is required."
        });
      using (ECARDAPPEntities ecardappEntities = new ECARDAPPEntities())
      {
        List<cust_users> list = ecardappEntities.cust_users.Where<cust_users>((Expression<Func<cust_users, bool>>) (e => e.mobile_no == mobileNumber && e.event_det_sno.Contains(eventOfChoice))).ToList<cust_users>();
        if (list.Count == 0)
          return this.Json((object) new
          {
            status = 0,
            message = "Failed! Event does not exist."
          });
        cust_users custUsers = list[0];
        return this.Json((object) new
        {
          status = 1,
          customerAdminID = custUsers.cust_reg_sno,
          fullName = custUsers.user_fullname,
          userID = custUsers.cust_users_sno
        });
      }
    }

    public JsonResult Invitees(long? event_id)
    {
      if (!event_id.HasValue)
        return this.Json((object) new
        {
          status = 0,
          message = "Failed! Item does not exist."
        }, JsonRequestBehavior.AllowGet);
      var visitor_details = this._context.visitor_details.Where(v => v.event_det_sno == event_id);
      int totalVisitors = visitor_details.Count();
      var visitors = visitor_details.Select(vd => new
      {
        visitor_det_sno = vd.visitor_det_sno,
        visitor_name = vd.visitor_name,
        card_state = vd.card_state_master.card_state,
        no_of_persons = vd.no_of_persons,
        mobile_no = vd.mobile_no,
        email_address = vd.email_address
      });
      return this.Json((object) new
      {
        status = 1,
        totalVisitor = totalVisitors,
        visitorDetails = visitors
      }, JsonRequestBehavior.AllowGet);
    }

    public string GetEncryptedData(string value)
    {
      return Convert.ToBase64String(Encoding.UTF8.GetBytes(value));
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
        this._context.Dispose();
      base.Dispose(disposing);
    }
  }
}
