// Decompiled with JetBrains decompiler
// Type: FUNDING.Controllers.SMSController
// Assembly: FUNDING, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5DE1C1E8-8D80-43BD-915D-F2502219BD58
// Assembly location: D:\ECardtest - 22082024\bin\FUNDING.dll

using FUNDING.BL.BusinessEntities.ConstantFile;
using FUNDING.BL.BusinessEntities.Masters;
using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Web.Mvc;

 
namespace FUNDING.Controllers
{
  public class SMSController : BaseController
  {
    private SMS_TEXT cy = new SMS_TEXT();
    private EMP_DET ed = new EMP_DET();
    private AuditLogs ad = new AuditLogs();
    private Arights act = new Arights();
    private string[] list = new string[9]
    {
      "sno",
      "flow_id",
      "email_text",
      "effective_date",
      "posted_by",
      "posted_date",
      "email_sub",
      "email_sub_local",
      "email_text_local"
    };
    private readonly dynamic returnNull = null;

    public ActionResult SMS()
    {
      if (this.Session["admin1"] == null)
        return (ActionResult) this.RedirectToAction("Login", "Login");
      return this.act.checkact("SMS Text", long.Parse(this.Session["Desg"].ToString()), this.Session["role"].ToString(), this.Session["Rcode"].ToString()) == 0L ? (ActionResult) this.RedirectToAction("Setup", "Setup") : (ActionResult) this.View();
    }

    [HttpPost]
    public ActionResult GetEmailDetails()
    {
      try
      {
        List<SMS_TEXT> sms = this.cy.GetSMS();
        return sms != null ? (ActionResult) this.Json((object) sms, JsonRequestBehavior.AllowGet) : (ActionResult) this.Json((object) 0, JsonRequestBehavior.AllowGet);
      }
      catch (Exception ex)
      {
        ex.ToString();
      }
      return this.returnNull;
    }

    [HttpPost]
    public ActionResult AddEmail(
      string flow,
      string text,
      string loctext,
      string sub,
      string subloc,
      long sno)
    {
      try
      {
        this.cy.Flow_Id = flow;
        this.cy.SMS_Text = Utilites.RemoveSpecialCharacters(text);
        this.cy.SMS_Local = Utilites.RemoveSpecialCharacters(loctext);
        this.cy.SMS_Subject = Utilites.RemoveSpecialCharacters(sub);
        this.cy.SMS_Other = Utilites.RemoveSpecialCharacters(subloc);
        this.cy.SNO = sno;
        this.cy.AuditBy = this.Session["UserID"].ToString();
        if (sno == 0L)
          return (ActionResult) this.Json((object) this.cy.AddSMS(this.cy), JsonRequestBehavior.AllowGet);
        if (sno > 0L)
        {
          this.cy.UpdateSMS(this.cy);
          return (ActionResult) this.Json((object) sno, JsonRequestBehavior.AllowGet);
        }
      }
      catch (Exception ex)
      {
        ex.Message.ToString();
      }
      
      return this.returnNull;
    }

    public ActionResult DeleteEmail(int Sno, string name)
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
      return this.returnNull;
    }
  }
}
