// Decompiled with JetBrains decompiler
// Type: FUNDING.Controllers.CardStatesController
// Assembly: FUNDING, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5DE1C1E8-8D80-43BD-915D-F2502219BD58
// Assembly location: D:\ECardtest - 22082024\bin\FUNDING.dll

using ECARD.DL.EDMX;
using FUNDING.BL.BusinessEntities.Masters;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;

 
namespace FUNDING.Controllers
{
  public class CardStatesController : Controller
  {
    private readonly CardStates_Master _cardState = new CardStates_Master();
    private readonly ECARDAPPEntities _dbContext = new ECARDAPPEntities();

    [Route("Card-States")]
    public ActionResult Index()
    {
      return this.Session["admin1"] == null ? (ActionResult) this.RedirectToAction("Login", "Login") : (ActionResult) this.View();
    }

    public ActionResult IndexDataTable()
    {
      var cardStatelist = this._dbContext.card_state_master.Select(cd => new
      {
        card_state_mas_sno = cd.card_state_mas_sno,
        card_state = cd.card_state
      }).ToList();
      int index = 1;
      return (ActionResult) this.Json((object) new
      {
        data = cardStatelist.Select(c => new
        {
          id = index++,
          card_state_mas_sno = c.card_state_mas_sno,
          card_state = c.card_state
        })
      });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult IsCardStateAvailable(
      string __RequestVerificationToken,
      string card_state,
      string card_state_clone)
    {
            if (card_state_clone != null)
            {
                if (card_state == card_state_clone)
                {
                    //Updating to the same name. Available for use.
                    return Json(true);
                }
            }

            var isCardStateAvailable = (from u in _dbContext.card_state_master
                                        where u.card_state.ToUpper() == card_state.ToUpper()
                                        select new { card_state }).FirstOrDefault();

            bool availabilityStatus;
            if (isCardStateAvailable != null)
            {
                //Username exists. Not available for use.
                availabilityStatus = false;
            }
            else
            {
                //Username does not exists. Available for use.
                availabilityStatus = true;
            }

            return Json(availabilityStatus);
        }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult AjaxHelperCreateForm([Bind(Include = "card_state_mas_sno,card_state")] CardStates_Master cardState)
    {
      if (this.Session["admin1"] == null)
        return (ActionResult) this.Json((object) new
        {
          loginStatus = false
        });
      if (!this.ModelState.IsValid)
        return (ActionResult) this.PartialView("_AjaxHelperCreateForm", (object) cardState);
      this._dbContext.card_state_master.Add(new card_state_master()
      {
        card_state = cardState.card_state,
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

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult AjaxHelperUpdateForm([Bind(Include = "card_state_mas_sno,card_state")] CardStates_Master cardState)
    {
      if (!this.ModelState.IsValid)
        return (ActionResult) this.PartialView("_AjaxHelperUpdateForm", (object) cardState);
      card_state_master entity = this._dbContext.card_state_master.Find(new object[1]
      {
        (object) cardState.card_state_mas_sno
      });
      if (entity == null)
        return (ActionResult) this.Json((object) new
        {
          updateStatus = false,
          response = "Failed! Item does not exist."
        });
      entity.card_state = cardState.card_state;
      entity.posted_date = DateTime.Now;
      entity.posted_by = this.GetPostedBy();
      this._dbContext.Entry<card_state_master>(entity).State = EntityState.Modified;
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
          response = "Failed! ID not supplied"
        });
      card_state_master entity = this._dbContext.card_state_master.Find(new object[1]
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
        this._dbContext.card_state_master.Remove(entity);
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
        updateStatus = true,
        response = "Record successful deleted."
      });
    }

    private string GetPostedBy() => this.Session["UserID"] as string;

    protected override void Dispose(bool disposing)
    {
      if (disposing)
        this._dbContext.Dispose();
      base.Dispose(disposing);
    }
  }
}
