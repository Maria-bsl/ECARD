// Decompiled with JetBrains decompiler
// Type: FUNDING.Controllers.PackagesController
// Assembly: FUNDING, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5DE1C1E8-8D80-43BD-915D-F2502219BD58
// Assembly location: D:\ECardtest - 22082024\bin\FUNDING.dll

using ECARD.DL.EDMX;
using FUNDING.BL.BusinessEntities.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace FUNDING.Controllers
{
  public class PackagesController : Controller
  {
    private readonly ECARDAPPEntities _dbContext = new ECARDAPPEntities();
    private readonly CustomerUsers _users = new CustomerUsers();
    private PACKAGES cy = new PACKAGES();
    private Arights act = new Arights();

    public ActionResult Index() => (ActionResult) this.View();

    public ActionResult Package()
    {
      if (this.Session["admin1"] == null)
        return (ActionResult) this.RedirectToAction("Login", "Login");
      return this.act.checkact("Package Details", long.Parse(this.Session["Desg"].ToString()), this.Session["role"].ToString(), this.Session["Rcode"].ToString()) == 0L ? (ActionResult) this.RedirectToAction("Setup", "Setup") : (ActionResult) this.View();
    }

    [HttpPost]
    public ActionResult OnEventChangeAction(string Event_Id)
    {
      long event_id = Convert.ToInt64(Event_Id);
      List<visitor_details> list = this._dbContext.visitor_details.Where<visitor_details>((Expression<Func<visitor_details, bool>>) (v => v.event_det_sno == (long?) event_id && v.event_details.event_date >= (DateTime?) DateTime.Today)).ToList<visitor_details>();
      return list.Count<visitor_details>() > 0 ? (ActionResult) this.Json((object) new
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
        response = "Package details does not exist"
      });
    }

    [HttpPost]
    public ActionResult AddPackageDetail(
      string package_name,
      string package_description,
      long package_price,
      string package_status,
      long sno,
      DateTime? effective_date)
    {
      if (this.ModelState.IsValid)
      {
        package_details packageDetails = this._dbContext.package_details.Where(v => v.pack_det_sno == sno).FirstOrDefault();
        this.cy.pack_name = package_name;
        this.cy.pack_description = package_description;
        this.cy.pack_price = new long?(package_price);
        this.cy.pack_status = package_status;
        this.cy.effective_date = effective_date;
        this.cy.sno = sno;
        this.cy.posted_date = new DateTime?(DateTime.Now);
        this.cy.posted_by = this.Session["UserID"].ToString();
        if (sno == 0L)
        {
          if (packageDetails == null)
            return (ActionResult) this.Json((object) this.cy.AddPackageDetails(this.cy), JsonRequestBehavior.AllowGet);
        }
        else if (sno > 0L && packageDetails != null)
        {
          this.cy.UpdatePackageDetails(this.cy);
          return this.Json((object)sno, JsonRequestBehavior.AllowGet);
        }
      }
      return (ActionResult) null;
    }

    [HttpPost]
    public ActionResult GetPackageDetails()
    {
      try
      {
        var packageDetails = this.cy.GetPackageDetails();
        return packageDetails != null ? Json( packageDetails, JsonRequestBehavior.AllowGet) : Json( 0, JsonRequestBehavior.AllowGet);
      }
      catch (Exception ex)
      {
         return Json(ex.ToString(), JsonRequestBehavior.AllowGet) ;
      }
      
    }

    public ActionResult DeletePackageDetail(int Sno)
    {
      if (_dbContext.event_details.Where(ev => ev.pack_det_sno == (long?) Sno).FirstOrDefault() == null)
      {
        try
        {
          if (Sno > 0)
          {
            cy.DeletePackage(Sno);
            return Json(Sno, JsonRequestBehavior.AllowGet);
          }
        }
        catch (Exception ex)
        {
          ex.Message.ToString();
        }
      }
      return Json( 0, JsonRequestBehavior.AllowGet);
    }
  }
}
