

using ECARD.DL.EDMX;
using FUNDING.BL.BusinessEntities.Masters;
using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web;
using System.Web.Mvc;

 
namespace FUNDING.Controllers
{
  public class CustomersController : Controller
  {
    private readonly CustomerRegistrationViewModel _customer = new CustomerRegistrationViewModel();
    private readonly ECARDAPPEntities _dbContext = new ECARDAPPEntities();

    public ActionResult Index()
    {
      if (this.Session["admin1"] == null)
        return (ActionResult) this.RedirectToAction("Login", "Login");
      List<SelectListItem> selectListItemList = new List<SelectListItem>();
      selectListItemList.Add(new SelectListItem()
      {
        Text = "Active",
        Value = "Active"
      });
      selectListItemList.Add(new SelectListItem()
      {
        Text = "Inactive",
        Value = "Inactive"
      });
      selectListItemList.Add(new SelectListItem()
      {
        Text = "Testing",
        Value = "Testing"
      });

      ViewBag.statusList = selectListItemList;

            return (ActionResult) this.View();
    }

    public ActionResult CustomerEvent()
    {
      if (this.Session["admin1"] == null)
        return (ActionResult) this.RedirectToAction("Login", "Login");

     
      ViewBag.Customer = new SelectList(this._dbContext.customer_registration, "cust_reg_sno", "cust_name");

      ViewBag.PackageName = new SelectList(this._dbContext.package_details.Where(e => e.pack_status == "Active"), "pack_det_sno", "pack_name");

            return (ActionResult) this.View();
    }

    public JsonResult CustomerEventDataTable()
    {
      DateTime date = DateTime.Now.Date;
      
     var customerEvents = this._dbContext.event_details.Include("customer_registration")
      .Select(e => new CustomerEventViewModel {
          EventID = e.event_det_sno,
          CustomerName = e.customer_registration.cust_name,
          EventName = e.event_name,
          PackageName = e.package_details.pack_name,
          EventDate = e.event_date,
          Event_Start_Time = e.event_start_time,
          EventStatus = e.event_status

      }).ToList(); 
      int index = 1;
    var formatedCustomerEvents = customerEvents.Select(e => new
    {
        id = index++,
        e.EventID,
        e.CustomerName,
        e.EventName,
        e.PackageName,
        EventDate = Convert.ToDateTime(e.EventDate).ToString("dddd, dd MMMM yyyy"),
        Event_Start_Time = Convert.ToDateTime(e.Event_Start_Time).ToString("hh:mm tt"),
        EventStatus = DynamicEventStatus(Convert.ToDateTime(e.Event_Start_Time))
    });

            return Json(new { data = formatedCustomerEvents
});
     /* return this.Json( new
      {
        data = customerEvents.Select(e => new
        {
          int num = index++,
          long eventId = e.EventID;
          string customerName = e.CustomerName;
          string eventName = e.EventName;
          string packageName = e.PackageName;
          DateTime dateTime = Convert.ToDateTime((object) e.EventDate);
          string str1 = dateTime.ToString("dddd, dd MMMM yyyy");
          dateTime = Convert.ToDateTime((object) e.Event_Start_Time);
          string str2 = dateTime.ToString("hh:mm tt");
          string str3 = this.DynamicEventStatus(Convert.ToDateTime((object) e.Event_Start_Time));
          return new
          {
            id = num,
            EventID = eventId,
            CustomerName = customerName,
            EventName = eventName,
            PackageName = packageName,
            EventDate = str1,
            Event_Start_Time = str2,
            EventStatus = str3
          };
        })
      });*/
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult AjaxHelperCreateCustomerEvent([Bind(Include = "EventID,CustomerID,EventName,PackageName,EventDate, Event_Start_Time")] CustomerEventViewModel customerEventDetails)
    {
      if (this.Session["admin1"] == null)
        return (ActionResult) this.RedirectToAction("Login", "Login");
      if (this.ModelState.IsValid)
      {
        cust_users customerAdminDetails = this.GetCustomerAdminDetails(customerEventDetails);
        if (customerAdminDetails == null)
          return (ActionResult) this.Json((object) new
          {
            createStatus = false,
            response = "Failed! Item does not exist."
          });
        DateTime formatedEventStartTime = CustomersController.GetFormatedEventStartTime(customerEventDetails);
        event_details eventDetails = this.SaveEventDetails(customerEventDetails, formatedEventStartTime);
        this.UpdateCustomerAndEventDetails(customerAdminDetails, eventDetails);
        return (ActionResult) this.Json((object) new
        {
          createStatus = true,
          response = "Record successful created."
        });
      }


            ViewBag.Customer =  new SelectList(this._dbContext.customer_registration, "cust_reg_sno", "cust_name");

            return (ActionResult) this.PartialView("_AjaxHelperCreateCustomerEventForm", (object) customerEventDetails);
    }

    private void UpdateCustomerAndEventDetails(cust_users customerAdmin, event_details eventDetails)
    {
      string mappedToCustomer = CustomersController.GetEventsMappedToCustomer(customerAdmin.event_det_sno, Convert.ToString(eventDetails.event_det_sno));
      customerAdmin.event_det_sno = mappedToCustomer;
      this._dbContext.Entry<cust_users>(customerAdmin).State = EntityState.Modified;
      string[] strArray = new string[12]
      {
        "cust_reg_sno",
        "user_fullname",
        "username",
        "password",
        "user_type",
        "created_date",
        "expiry_date",
        "email_address",
        "mobile_no",
        "created_date",
        "posted_by",
        "posted_date"
      };
      foreach (string propertyName in strArray)
        this._dbContext.Entry<cust_users>(customerAdmin).Property(propertyName).IsModified = false;
      this.SendWelcomeSmsToNewUser(customerAdmin);
      this._dbContext.SaveChanges();
    }

    private static string GetEventsMappedToCustomer(string customerEventID, string dbEventID)
    {
      customerEventID = customerEventID != null ? customerEventID + "," + dbEventID : dbEventID;
      return customerEventID;
    }

    private event_details SaveEventDetails(
      CustomerEventViewModel customerEventDetails,
      DateTime eventStartTime)
    {
      this._dbContext.package_details.Where(p => p.pack_name == customerEventDetails.PackageName).FirstOrDefault<package_details>();
      string packageName = customerEventDetails.PackageName;
      event_details entity = new event_details()
      {
        cust_reg_sno = customerEventDetails.CustomerID,
        event_name = customerEventDetails.EventName,
        event_date = customerEventDetails.EventDate,
        event_start_time = new DateTime?(eventStartTime),
        event_status = this.GetEventStatus(),
        posted_date = DateTime.Now,
        posted_by = this.GetPostedBy(),
        pack_det_sno = new long?((long) Convert.ToInt32(packageName))
      };
      this._dbContext.event_details.Add(entity);
      this._dbContext.SaveChanges();
      return entity;
    }

    private static DateTime GetFormatedEventStartTime(CustomerEventViewModel customerEventDetails)
    {
      DateTime dateTime1 = customerEventDetails.EventDate.Value;
      DateTime dateTime2 = customerEventDetails.Event_Start_Time.Value;
      return DateTime.Parse(dateTime1.ToShortDateString() + " " + dateTime2.ToShortTimeString());
    }

    private cust_users GetCustomerAdminDetails(CustomerEventViewModel customerEventDetails)
    {
      return this._dbContext.cust_users.Include("customer_registration").Where<cust_users>((Expression<Func<cust_users, bool>>) (c => c.cust_reg_sno == (long?) customerEventDetails.CustomerID)).FirstOrDefault<cust_users>();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult AjaxHelperUpdateCustomerEvent([Bind(Include = "EventID,CustomerID,EventName,PackageName,EventDate,Event_Start_Time")] CustomerEventViewModel customerEventDetails)
    {
      if (this.Session["admin1"] == null)
        return (ActionResult) this.RedirectToAction("Login", "Login");
      if (this.ModelState.IsValid)
      {
        event_details entity = this._dbContext.event_details.Find(new object[1]
        {
          (object) customerEventDetails.EventID
        });
        if (entity == null)
          return (ActionResult) this.Json((object) new
          {
            updateStatus = false,
            response = "Failed! Item does not exist."
          });
        DateTime? nullable = customerEventDetails.EventDate;
        DateTime dateTime1 = nullable.Value;
        nullable = customerEventDetails.Event_Start_Time;
        DateTime dateTime2 = nullable.Value;
        DateTime dateTime3 = DateTime.Parse(dateTime1.ToShortDateString() + " " + dateTime2.ToShortTimeString());
        entity.pack_det_sno = new long?((long) Convert.ToInt32(customerEventDetails.PackageName));
        entity.event_name = customerEventDetails.EventName;
        entity.event_date = customerEventDetails.EventDate;
        entity.event_start_time = new DateTime?(dateTime3);
        entity.event_status = this.GetEventStatus();
        entity.posted_by = this.GetPostedBy();
        this._dbContext.Entry<event_details>(entity).State = EntityState.Modified;
        this._dbContext.Entry<event_details>(entity).Property("cust_reg_sno").IsModified = false;
        this._dbContext.Entry<event_details>(entity).Property("inviter_name").IsModified = false;
        this._dbContext.Entry<event_details>(entity).Property("venue").IsModified = false;
        this._dbContext.Entry<event_details>(entity).Property("reservation").IsModified = false;
        this._dbContext.Entry<event_details>(entity).Property("posted_date").IsModified = false;
        this._dbContext.SaveChanges();
        this._dbContext.cust_users.Include("customer_registration").Where<cust_users>((Expression<Func<cust_users, bool>>) (c => c.cust_reg_sno == (long?) customerEventDetails.CustomerID)).FirstOrDefault<cust_users>();
        return (ActionResult) this.Json((object) new
        {
          updateStatus = true,
          response = "Record successful updated."
        });
      }

            ViewBag.Customer = new SelectList((IEnumerable)this._dbContext.customer_registration, "cust_reg_sno", "cust_name");

            return (ActionResult) this.PartialView("_AjaxHelperUpdateCustomerEventForm", (object) customerEventDetails);
    }

        [Route("Test-Customers")]
        public JsonResult TestCustomers()
        {
            var customersDataList = (from customers in _dbContext.cust_users
                                     group customers by customers.cust_reg_sno into customerGroup
                                     orderby customerGroup.Key
                                     select new
                                     {
                                         CustomerUsers = customerGroup
                                         .Select(cs => new
                                         {
                                             cs.cust_reg_sno,
                                             cs.user_fullname,
                                             cs.mobile_no,
                                             cs.email_address,
                                             cs.customer_registration.physical_address,
                                             cs.user_type,
                                             cs.customer_registration.status
                                         }).FirstOrDefault()
                                     }).ToList();

            return Json(new
            {
                data = customersDataList.Select(cs => new
                {
                    cs.CustomerUsers.cust_reg_sno,
                    cs.CustomerUsers.user_fullname,
                    cs.CustomerUsers.mobile_no,
                    cs.CustomerUsers.email_address,
                    cs.CustomerUsers.physical_address,
                    cs.CustomerUsers.user_type,
                    cs.CustomerUsers.status
                })
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
    public JsonResult IndexDataTable()
    {
      var customerDataList = this._dbContext.cust_users.GroupBy(customers => customers.cust_reg_sno).OrderBy(customerGroup => customerGroup.Key).Select(customerGroup => new
      {
        CustomerUsers = customerGroup.Select(cs => new
        {
          cust_reg_sno = cs.cust_reg_sno,
          user_fullname = cs.user_fullname,
          mobile_no = cs.mobile_no,
          email_address = cs.email_address,
          physical_address = cs.customer_registration.physical_address,
          user_type = cs.user_type,
          status = cs.customer_registration.status
        }).FirstOrDefault()
      }).ToList();
      int index = 1;
      return this.Json((object) new
      {
        data = customerDataList.Select(cs => new
        {
          id = index++,
          cust_reg_sno = cs.CustomerUsers.cust_reg_sno,
          user_fullname = cs.CustomerUsers.user_fullname,
          mobile_no = cs.CustomerUsers.mobile_no,
          email_address = cs.CustomerUsers.email_address,
          physical_address = cs.CustomerUsers.physical_address,
          user_type = cs.CustomerUsers.user_type,
          status = cs.CustomerUsers.status
        })
      });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult AjaxHelperCreateForm([Bind(Include = "customerName,physical_address,mobile_no,email_address,status")] CustomerRegistrationViewModel customer)
    {
      customer_registration entity1 = new customer_registration()
      {
        cust_name = customer.customerName,
        physical_address = customer.physical_address,
        mobile_no = customer.mobile_no,
        email_address = customer.email_address,
        status = customer.status,
        posted_date = DateTime.Now,
        posted_by = this.GetPostedBy()
      };
      this._dbContext.customer_registration.Add(entity1);
      this._dbContext.SaveChanges();
      cust_users entity2 = new cust_users()
      {
        cust_reg_sno = new long?(entity1.cust_reg_sno),
        user_fullname = customer.customerName,
        password = this.GetGeneratedPassword(),
        user_type = customer.AdminUserType,
        created_date = new DateTime?(DateTime.Now),
        expiry_date = this.GetExpireDate(),
        mobile_no = customer.mobile_no,
        email_address = customer.email_address,
        posted_date = DateTime.Now,
        posted_by = this.GetPostedBy()
      };
      this._dbContext.cust_users.Add(entity2);
      this._dbContext.Entry<cust_users>(entity2).Property("username").IsModified = false;
      this._dbContext.SaveChanges();
      return (ActionResult) this.Json((object) new
      {
        createStatus = true,
        response = "Record successful created."
      });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult AjaxHelperUpdateForm([Bind(Include = "cust_reg_sno,customerName,cust_event_name,physical_address,mobile_no,email_address,status")] CustomerRegistrationViewModel customer)
    {
      customer_registration entity1 = this._dbContext.customer_registration.Find(new object[1]
      {
        (object) customer.cust_reg_sno
      });
      cust_users entity2 = this._dbContext.cust_users.SingleOrDefault<cust_users>((Expression<Func<cust_users, bool>>) (cu => cu.cust_reg_sno == (long?) customer.cust_reg_sno && cu.user_type == customer.AdminUserType));
      if (entity1 == null && entity2 == null)
        return (ActionResult) this.Json((object) new
        {
          updateStatus = false,
          response = "Failed! Item does not exist."
        });
      entity1.cust_reg_sno = customer.cust_reg_sno;
      entity1.cust_name = customer.customerName;
      entity1.physical_address = customer.physical_address;
      entity1.mobile_no = customer.mobile_no;
      entity1.email_address = customer.email_address;
      entity1.status = customer.status;
      entity1.posted_date = DateTime.Now;
      entity1.posted_by = this.GetPostedBy();
      this._dbContext.Entry<customer_registration>(entity1).State = EntityState.Modified;
      this._dbContext.SaveChanges();
      entity2.user_fullname = customer.customerName;
      entity2.mobile_no = customer.mobile_no;
      entity2.email_address = customer.email_address;
      entity2.posted_date = DateTime.Now;
      entity2.posted_by = this.GetPostedBy();
      this._dbContext.Entry<cust_users>(entity2).State = EntityState.Modified;
      this._dbContext.Entry<cust_users>(entity2).Property("username").IsModified = false;
      this._dbContext.Entry<cust_users>(entity2).Property("password").IsModified = false;
      this._dbContext.Entry<cust_users>(entity2).Property("user_type").IsModified = false;
      this._dbContext.Entry<cust_users>(entity2).Property("created_date").IsModified = false;
      this._dbContext.Entry<cust_users>(entity2).Property("expiry_date").IsModified = false;
      this._dbContext.SaveChanges();
      return (ActionResult) this.Json((object) new
      {
        updateStatus = true,
        response = "Record successful updated."
      });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult AjaxHelperDeleteForm(long? id)
    {
      if (!id.HasValue)
        return (ActionResult) this.Json((object) new
        {
          deleteStatus = false,
          response = "Failed! ID not supplied."
        });
      CustomerRegistrationViewModel customer = new CustomerRegistrationViewModel();
      try
      {
        customer_registration entity1 = this._dbContext.customer_registration.Find(new object[1]
        {
          (object) id
        });
        cust_users entity2 = this._dbContext.cust_users.SingleOrDefault<cust_users>((Expression<Func<cust_users, bool>>) (cu => cu.cust_reg_sno == id && cu.user_type == customer.AdminUserType));
        if (entity1 == null && entity2 == null)
          return (ActionResult) this.Json((object) new
          {
            deleteStatus = false,
            response = "Failed! Item does not exist."
          });
        this._dbContext.customer_registration.Remove(entity1);
        this._dbContext.SaveChanges();
        this._dbContext.cust_users.Remove(entity2);
        this._dbContext.SaveChanges();
      }
      catch (Exception ex)
      {
        return (ActionResult) this.Json((object) new
        {
          deleteStatus = false,
          response = "Record can not be deleted, it is being used by other records."+ex.Message
        });
      }
      return (ActionResult) this.Json((object) new
      {
        deleteStatus = true,
        response = "Record successful deleted."
      });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("Customers/AjaxHelperDeleteCustomerEvent/{event_id}")]
    public ActionResult AjaxHelperDeleteCustomerEventForm(long? event_id)
    {
      if (!event_id.HasValue)
        return (ActionResult) this.Json((object) new
        {
          deleteStatus = false,
          response = "Failed! ID not supplied."
        });
      try
      {
        event_details entity1 = this._dbContext.event_details.Find(new object[1]
        {
          (object) event_id
        });
        List<visitor_details> list = this._dbContext.visitor_details.Where<visitor_details>((Expression<Func<visitor_details, bool>>) (cu => cu.event_det_sno == event_id)).ToList<visitor_details>();
        if (entity1 == null && list == null)
          return (ActionResult) this.Json((object) new
          {
            deleteStatus = false,
            response = "Failed! Item does not exist."
          });
        this._dbContext.event_details.Remove(entity1);
        this._dbContext.SaveChanges();
        foreach (visitor_details entity2 in list)
        {
          this._dbContext.visitor_details.Remove(entity2);
          this._dbContext.SaveChanges();
        }
      }
      catch (Exception ex)
      {
        return (ActionResult) this.Json((object) new
        {
          deleteStatus = false,
          response = "Record can not be deleted, it is being used by other records."
        });
      }
      return (ActionResult) this.Json((object) new
      {
        deleteStatus = true,
        response = "Record successful deleted."
      });
    }

    private string DynamicEventStatus(DateTime customerWithEvent)
    {
      DateTime date = DateTime.Now.Date;
      double totalDays = Convert.ToDateTime(customerWithEvent).Date.Subtract(date).TotalDays;
      if (totalDays > 0.0)
        return totalDays.ToString() + " days to event";
      return totalDays == 0.0 ? "Active" : "Closed";
    }

    private string GetRegistrationStatus() => "Testing";

    private string GetPostedBy() => this.Session["UserID"].ToString();

    private DateTime? GetExpireDate() => new DateTime?(DateTime.Now.AddMonths(3));

    private string GetGeneratedPassword()
    {
      return CustomersController.GetEncryptedData(this.CreateRandomPassword(8));
    }

    private string GetEventStatus() => "Pending";

    private string CreateRandomPassword(int length)
    {
      string str = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
      Random random = new Random();
      char[] chArray = new char[length];
      for (int index = 0; index < length; ++index)
        chArray[index] = str[random.Next(0, str.Length)];
      return new string(chArray);
    }

    public static string GetEncryptedData(string value)
    {
      byte[] numArray = new byte[value.Length];
      return Convert.ToBase64String(Encoding.UTF8.GetBytes(value));
    }

    private string DecodeFrom64(string password)
    {
      string empty = string.Empty;
      Decoder decoder = new UTF8Encoding().GetDecoder();
      byte[] bytes = Convert.FromBase64String(password);
      char[] chars = new char[decoder.GetCharCount(bytes, 0, bytes.Length)];
      decoder.GetChars(bytes, 0, bytes.Length, chars, 0);
      return new string(chars);
    }

    public void SendWelcomeSmsToNewUser(cust_users customerUser)
    {
      if (customerUser == null)
        return;
      string password1 = customerUser.password;
      string mobileNo = customerUser.mobile_no;
      string password2 = this.DecodeFrom64(password1);
      string SmsBody = this.FormatMessageBody(customerUser.user_fullname, password2);
      this.SendSMSAction(mobileNo, SmsBody);
    }

    private string FormatMessageBody(string customerName, string password)
    {
      return string.Format("{0}, you have successfully registered an event on B-ENVIT system, the URL is https://bizlogicsolutions.co.in:99/ and Your password is {1}", (object) customerName, (object) password);
    }

    private void SendSMSAction(string visitorMobileNumber, string SmsBody)
    {
      S_SMTP sSmtp = new S_SMTP();
      EMAIL email = new EMAIL();
      Role role = new Role();
      Comments comments = new Comments();
      Arights arights = new Arights();
      AuditLogs auditLogs = new AuditLogs();
      SMS_SETTNG smsSettng = new SMS_SETTNG();
      SMS_TEXT smsText = new SMS_TEXT();
      try
      {
        SMS_SETTNG smtpConfig = smsSettng.getSMTPConfig();
        sSmtp.getSMTPText();
        string userName = smtpConfig.USER_Name;
        string str1 = this.DecodeFrom64(smtpConfig.Password);
        string fromAddress = smtpConfig.From_Address;
        string str2 = this.ReplaceFirstOccurrence(visitorMobileNumber.Trim(), "0", "255");
        string str3 = HttpUtility.UrlEncode(SmsBody);
        WebRequest webRequest = WebRequest.Create("http://api.infobip.com/api/v3/sendsms/plain?user=" + userName + "&password=" + str1 + "&sender=" + fromAddress + "&SMSText=" + str3 + "&GSM=" + str2 + "&type=longSMS");
        webRequest.Method = "POST";
        new StreamReader(webRequest.GetResponse().GetResponseStream(), Encoding.Default).ReadToEnd();
      }
      catch (Exception ex)
      {
        ex.Message.ToString();
      }
    }

    private string ReplaceFirstOccurrence(string Source, string Find, string Replace)
    {
      int startIndex = Source.IndexOf(Find);
      return Source.Remove(startIndex, Find.Length).Insert(startIndex, Replace);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
        this._dbContext.Dispose();
      base.Dispose(disposing);
    }
  }
}
