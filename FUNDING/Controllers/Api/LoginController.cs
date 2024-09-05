// Decompiled with JetBrains decompiler
// Type: FUNDING.Controllers.Api.LoginController
// Assembly: FUNDING, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5DE1C1E8-8D80-43BD-915D-F2502219BD58
// Assembly location: D:\ECardtest - 22082024\bin\FUNDING.dll

using ECARD.DL.EDMX;
using FUNDING.BL.BusinessEntities.Masters;
using FUNDING.Models;
using FUNDING.Models.ApiModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;

 
namespace FUNDING.Controllers.Api
{
  [EnableCors("*", "*", "*")]
  public class LoginController : ApiBaseController
  {
    private readonly ECARDAPPEntities dbContext = new ECARDAPPEntities();

    [HttpPost]
    [Route("api/login")]
    public HttpResponseMessage Post([FromBody] LoginCredentials credentials)
    {
      if (credentials == null)
        return this.Request.CreateResponse(HttpStatusCode.BadRequest, new
        {
          status = 0,
          message = "Mobile number and password should not be empty"
        });
      if (!this.ModelState.IsValid)
        return this.Request.CreateResponse(HttpStatusCode.BadRequest, new
        {
          status = 0,
          errorList = this.GetModelStateErrorList()
        });
      string encryptedData = this.GetEncryptedData(credentials.Password);
      List<event_details> source = CustomerUsers.EventsEntitled(credentials.Mobile_Number, encryptedData);
      return source == null ? this.Request.CreateResponse(HttpStatusCode.BadRequest, new
      {
        status = 0,
        message = "Invalid mobile number or password"
      }) : this.Request.CreateResponse(HttpStatusCode.OK, new
      {
        status = 1,
        event_details = source.Select(ev => new
        {
          event_id = ev.event_det_sno,
          event_name = ev.event_name
        }),
        Mobile_Number = credentials.Mobile_Number
      });
    }

    [HttpPost]
    [Route("api/event-of-choice")]
    public HttpResponseMessage EventOfChoice([FromBody] FUNDING.Models.ApiModels.EventOfChoice eventOfChoice)
    {
      if (eventOfChoice == null)
        return this.Request.CreateResponse(HttpStatusCode.BadRequest, new
        {
          status = 0,
          message = "Failed! Event does not exist."
        });
      if (!this.ModelState.IsValid)
        return this.Request.CreateResponse(HttpStatusCode.BadRequest, new
        {
          status = 0,
          errorList = this.GetModelStateErrorList()
        });
      cust_users detailsEntitledToEvent = this.GetCustomerDetailsEntitledToEvent(eventOfChoice);
      return detailsEntitledToEvent == null ? this.Request.CreateResponse(HttpStatusCode.NotFound, new
      {
        status = 0,
        message = "Failed! Event does not exist."
      }) : this.Request.CreateResponse(HttpStatusCode.OK, new
      {
        status = 1,
        customer_admin_id = detailsEntitledToEvent.cust_reg_sno,
        full_name = detailsEntitledToEvent.user_fullname,
        user_id = detailsEntitledToEvent.cust_users_sno
      });
    }

    [HttpPost]
    [Route("api/forgot-password")]
    public HttpResponseMessage CustomerForgot([FromBody] ForgotPassword forgotPassword)
    {
      if (forgotPassword == null)
        return this.Request.CreateResponse(HttpStatusCode.BadRequest, new
        {
          message = "Please provide mobile number."
        });
      if (!this.ModelState.IsValid)
        return this.Request.CreateResponse(HttpStatusCode.BadRequest, new
        {
          status = 0,
          errorList = this.GetModelStateErrorList()
        });
      ForgotPasswordViewModel passwordViewModel = new ForgotPasswordViewModel();
      List<cust_users> userList = passwordViewModel.VerifyUserWithMobileNumber(forgotPassword.Mobile_Number);
      if (userList != null)
      {
        passwordViewModel.SendSmsWithCredential(userList);
        passwordViewModel.DeactivateUser(userList);
      }
      return this.Request.CreateResponse(HttpStatusCode.OK, new
      {
        status = 1
      });
    }

    [HttpPost]
    [Route("api/verify-existing-password")]
    public HttpResponseMessage CheckPassword(
      [FromBody] CheckCurrentPasswordExistence checkPasswordExistence)
    {
      if (checkPasswordExistence == null)
        return this.Request.CreateResponse(HttpStatusCode.BadRequest, new
        {
          message = "Please provide mobile number and current password."
        });
      return this.ModelState.IsValid ? this.Request.CreateResponse(HttpStatusCode.OK, new
      {
        password_exists = this.CheckPasswordExistence(checkPasswordExistence.Current_Password, checkPasswordExistence.Mobile_Number) != null
      }) : this.Request.CreateResponse(HttpStatusCode.BadRequest, new
      {
        status = 0,
        errorList = this.GetModelStateErrorList()
      });
    }

    [HttpPost]
    [Route("api/change-password")]
    public HttpResponseMessage ChangePassword([FromBody] FUNDING.Models.ApiModels.ChangePassword changePassword)
    {
      if (changePassword == null)
        return this.Request.CreateResponse(HttpStatusCode.BadRequest, new
        {
          message = "Please provide mobile number, current password and new password."
        });
      if (!this.ModelState.IsValid)
        return this.Request.CreateResponse(HttpStatusCode.BadRequest, new
        {
          password_Change_Status = 0,
          errorList = this.GetModelStateErrorList()
        });
      cust_users entity = this.CheckPasswordExistence(changePassword.Current_Password, changePassword.Mobile_Number);
      if (entity == null)
        return this.Request.CreateResponse(HttpStatusCode.NotFound, new
        {
          password_Change_Status = 0
        });
      entity.password = this.GetEncryptedData(changePassword.New_Password);
      this.dbContext.Entry<cust_users>(entity).State = EntityState.Modified;
      this.dbContext.SaveChanges();
      return this.Request.CreateResponse(HttpStatusCode.OK, new
      {
        password_Change_Status = 1
      });
    }

    private cust_users GetCustomerDetailsEntitledToEvent(FUNDING.Models.ApiModels.EventOfChoice eventOfChoice)
    {
      string event_id = eventOfChoice.Event_Id.ToString();
      string mobileNumber = eventOfChoice.Mobile_Number;
      return this.dbContext.cust_users.Where<cust_users>((Expression<Func<cust_users, bool>>) (e => e.mobile_no == mobileNumber && e.event_det_sno.Contains(event_id))).SingleOrDefault<cust_users>();
    }

    public string GetEncryptedData(string value)
    {
      return Convert.ToBase64String(Encoding.UTF8.GetBytes(value));
    }

    private cust_users CheckPasswordExistence(string currentPassword, string mobileNumber)
    {
      string encryptedPassword = this.GetEncryptedData(currentPassword);
      return this.dbContext.cust_users.Where<cust_users>((Expression<Func<cust_users, bool>>) (v => v.password == encryptedPassword && v.mobile_no == mobileNumber)).SingleOrDefault<cust_users>();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
        this.dbContext.Dispose();
      base.Dispose(disposing);
    }
  }
}
