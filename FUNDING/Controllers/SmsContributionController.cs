// Decompiled with JetBrains decompiler
// Type: FUNDING.Controllers.SmsContributionController
// Assembly: FUNDING, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5DE1C1E8-8D80-43BD-915D-F2502219BD58
// Assembly location: D:\ECardtest - 22082024\bin\FUNDING.dll

using ECARD.DL.EDMX;
using FUNDING.BL.BusinessEntities.Masters;
using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Web.Mvc;

 
namespace FUNDING.Controllers
{
  public class SmsContributionController : BaseController
  {
    private readonly ECARDAPPEntities _dbContext = new ECARDAPPEntities();
    private readonly CustomerUsers _users = new CustomerUsers();
    private SMS_CONTRIBUTION cy = new SMS_CONTRIBUTION();
    private Arights act = new Arights();

    public ActionResult Index()
    {
      return this.Session["admin1"] == null ? RedirectToAction("CustomerLogin", "Login") : (ActionResult) this.View();
    }

    public ActionResult SMSContribution()
    {
      if (this.Session["admin1"] == null)
        return (ActionResult) this.RedirectToAction("Login", "Login");

     /* // ISSUE: reference to a compiler-generated field
              if (SmsContributionController.\u003C\u003Eo__5.\u003C\u003Ep__0 == null)
              {
                // ISSUE: reference to a compiler-generated field
                SmsContributionController.\u003C\u003Eo__5.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, IQueryable<SelectListItem>, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "EventDropDownList", typeof (SmsContributionController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
                {
                  CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                  CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
                }));
              }
              ParameterExpression parameterExpression;
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated field
              // ISSUE: method reference
              // ISSUE: method reference
              // ISSUE: method reference
              // ISSUE: method reference
              object obj = SmsContributionController.\u003C\u003Eo__5.\u003C\u003Ep__0.Target((CallSite) SmsContributionController.\u003C\u003Eo__5.\u003C\u003Ep__0, this.ViewBag, this._dbContext.event_details.Where<event_details>((Expression<Func<event_details, bool>>) (e => e.event_date >= (DateTime?) DateTime.Today)).Select<event_details, SelectListItem>(Expression.Lambda<Func<event_details, SelectListItem>>((Expression) Expression.MemberInit(Expression.New(typeof (SelectListItem)), (MemberBinding) Expression.Bind((MethodInfo) MethodBase.GetMethodFromHandle(__methodref (SelectListItem.set_Value)), (Expression) Expression.Call(b.event_det_sno, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (long.ToString)), Array.Empty<Expression>())), (MemberBinding) Expression.Bind((MethodInfo) MethodBase.GetMethodFromHandle(__methodref (SelectListItem.set_Text)), (Expression) Expression.Property((Expression) parameterExpression, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (event_details.get_event_name))))), parameterExpression)));
         */
     
            #region Event dropdownlist

            //TODO: there must be a way to view only active events

            ViewBag.EventDropDownList = _dbContext.event_details.Where(e => e.event_date >= DateTime.Today)
                .Select(b => new SelectListItem
                {
                    Value = b.event_det_sno.ToString(),
                    Text = b.event_name
                });

            #endregion;

            return this.act.checkact("Contribution SMS", long.Parse(this.Session["Desg"].ToString()), this.Session["role"].ToString(), this.Session["Rcode"].ToString()) == 0L ? (ActionResult) this.RedirectToAction("Setup", "Setup") : (ActionResult) this.View();
    }

    [HttpPost]
    public ActionResult OnEventChangeAction(string Event_Id)
    {
      long event_id = Convert.ToInt64(Event_Id);
      List<visitor_details> list = this._dbContext.visitor_details.Where<visitor_details>((Expression<Func<visitor_details, bool>>) (v => v.event_det_sno == (long?) event_id && v.event_details.event_date >= (DateTime?) DateTime.Today)).ToList<visitor_details>();
      return list.Count() > 0 ? (ActionResult) this.Json((object) new
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
    public ActionResult AddSMSContribution(long Event_Id, string Message, long sno)
    {
      sms_contribution smsContribution = this._dbContext.sms_contribution.Where<sms_contribution>((Expression<Func<sms_contribution, bool>>) (j => j.event_det_sno == (long?) Event_Id)).FirstOrDefault<sms_contribution>();
      this.cy.sms_text = Message;
      this.cy.event_sno = new long?(Event_Id);
      this.cy.sms_csno = sno;
      this.cy.posted_by = this.Session["UserID"].ToString();
      if (sno == 0L)
      {
        if (smsContribution == null)
          return (ActionResult) this.Json((object) this.cy.AddSMSContributions(this.cy), JsonRequestBehavior.AllowGet);
      }
      else if (sno > 0L && smsContribution != null)
      {
        this.cy.UpdateSMSContribution(this.cy);
        return (ActionResult) this.Json((object) sno, JsonRequestBehavior.AllowGet);
      }
      return (ActionResult) null;
    }

    [HttpPost]
    public ActionResult GetSMSContributionDetails()
    {
      try
      {
        List<SMS_CONTRIBUTION> smsContribution = this.cy.GetSMSContribution();
        return smsContribution != null ? (ActionResult) this.Json((object) smsContribution, JsonRequestBehavior.AllowGet) : (ActionResult) this.Json((object) 0, JsonRequestBehavior.AllowGet);
      }
      catch (Exception ex)
      {
        ex.ToString();
      }
      return (ActionResult) null;
    }

    public ActionResult DeleteSMSContribution(int Sno)
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
