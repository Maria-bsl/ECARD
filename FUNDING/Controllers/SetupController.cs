// Decompiled with JetBrains decompiler
// Type: FUNDING.Controllers.SetupController
// Assembly: FUNDING, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5DE1C1E8-8D80-43BD-915D-F2502219BD58
// Assembly location: D:\ECardtest - 22082024\bin\FUNDING.dll

using ECARD.DL.EDMX;
using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web.Mvc;

 
namespace FUNDING.Controllers
{
  public class SetupController : BaseController
  {
    private readonly ECARDAPPEntities _dbContext = new ECARDAPPEntities();

    [Route("Admin-Dashboard")]
    public ActionResult Setup()
    {
      if (this.Session["admin1"] == null)
        return (ActionResult) this.RedirectToAction("Login", "Login");
      if (this.Session["admin1"].ToString() != "Bank")
        return (ActionResult) this.RedirectToAction("Login", "Login");

            ViewBag.CustomerCount = GetTotalNumberOfCustomers();
            ViewBag.EventCount = GetTotalNumberOfEvents();
            ViewBag.InternUsers = GetTotalNumberOfInternalUsers();

            return (ActionResult) this.View();
    }

    private int GetTotalNumberOfCustomers()
    {
      return this._dbContext.customer_registration.Count<customer_registration>();
    }

    private int GetTotalNumberOfEvents() => this._dbContext.event_details.Count<event_details>();

    private int GetTotalNumberOfInternalUsers() => this._dbContext.emp_detail.Count<emp_detail>();

    protected override void Dispose(bool disposing)
    {
      if (disposing)
        this._dbContext.Dispose();
      base.Dispose(disposing);
    }
  }
}
