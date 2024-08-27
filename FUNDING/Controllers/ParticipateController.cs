// Decompiled with JetBrains decompiler
// Type: FUNDING.Controllers.ParticipateController
// Assembly: FUNDING, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5DE1C1E8-8D80-43BD-915D-F2502219BD58
// Assembly location: D:\ECardtest - 22082024\bin\FUNDING.dll

using ECARD.DL.EDMX;
using FUNDING.Models;
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
  public class ParticipateController : Controller
  {
    private readonly ECARDAPPEntities _dbContext = new ECARDAPPEntities();
    private List<string> Opp = new List<string>();
    private List<string> Busi = new List<string>();

    public ActionResult Index()
    {

            ViewBag.regions = new SelectList((IEnumerable)this._dbContext.region_master, "region_sno", "region_name");
            ViewBag.districts = new SelectList((IEnumerable)this._dbContext.district_master, "district_sno", "district_name");


            ViewBag.wards = new SelectList((IEnumerable)this._dbContext.ward_master, "ward_sno", "ward_name");


            List<SelectListItem> selectListItemList1 = new List<SelectListItem>();
      selectListItemList1.Add(new SelectListItem()
      {
        Text = "Male",
        Value = "Male"
      });
      selectListItemList1.Add(new SelectListItem()
      {
        Text = "Female",
        Value = "Female"
      });

            ViewBag.statusList = selectListItemList1;

      List<SelectListItem> selectListItemList2 = new List<SelectListItem>();
      selectListItemList2.Add(new SelectListItem()
      {
        Text = "Starting/ Naanza",
        Value = "Starting/ Naanza"
      });
      selectListItemList2.Add(new SelectListItem()
      {
        Text = "I have a business/ Nina Biashara",
        Value = "I have a business/ Nina Biashara"
      });

            ViewBag.entreprenuer = selectListItemList2;
      List<SelectListItem> selectListItemList3 = new List<SelectListItem>();
      selectListItemList3.Add(new SelectListItem()
      {
        Text = "Market/Masoko",
        Value = "Market /Masoko"
      });
      selectListItemList3.Add(new SelectListItem()
      {
        Text = "Raw Materials /Malighafi",
        Value = "Raw Materials /Malighafi"
      });
      selectListItemList3.Add(new SelectListItem()
      {
        Text = "Knowledge /Utaalamu",
        Value = "Knowledge /Utaalamu"
      });
      selectListItemList3.Add(new SelectListItem()
      {
        Text = "Networking /Kukutana na watu muhimu",
        Value = "Networking /Kukutana na watu muhimu"
      });
      selectListItemList3.Add(new SelectListItem()
      {
        Text = "Others /Mengineyo",
        Value = "Others /Mengineyo"
      });

      ViewBag.opportunity = selectListItemList3;

      List<SelectListItem> selectListItemList4 = new List<SelectListItem>();
      selectListItemList4.Add(new SelectListItem()
      {
        Text = "Agribusiness /Biashara Kilimo",
        Value = "Agribusiness /Biashara Kilimo"
      });
      selectListItemList4.Add(new SelectListItem()
      {
        Text = "Manufacturing /Uzalishaji",
        Value = "Manufacturing /Uzalishaji"
      });
      selectListItemList4.Add(new SelectListItem()
      {
        Text = "Service /Huduma",
        Value = "Service /Huduma"
      });
      selectListItemList4.Add(new SelectListItem()
      {
        Text = "Logistics /Usafirishaji",
        Value = "Logistics /Usafirishaji"
      });
      selectListItemList4.Add(new SelectListItem()
      {
        Text = "Mining /Madini",
        Value = "Mining /Madini"
      });
      selectListItemList4.Add(new SelectListItem()
      {
        Text = "Export /Usafirishaji",
        Value = "Export /Usafirishaji"
      });
      selectListItemList4.Add(new SelectListItem()
      {
        Text = "Others /Nyenginezo",
        Value = "Others /Nyenginezo"
      });
            ViewBag.business = selectListItemList4;
            
      return (ActionResult) this.View();
    }

    [HttpPost]
    [Route("participate/getDistricts/{event_id}")]
    public ActionResult getDistricts(long event_id)
    {
      List<SelectListItem> selectListItemList = new List<SelectListItem>();
      var district = this._dbContext.district_master.Where(ev => ev.region_id == (long?) event_id).Select(vd => new
      {
        district_sno = vd.district_sno,
        district_name = vd.district_name
      }).ToList();
      if (district == null)
        return (ActionResult) this.Json((object) new
        {
          data = ""
        });
      foreach (var data in district)
      {
        selectListItemList.Add(new SelectListItem()
        {
          Value = data.district_sno.ToString(),
          Text = data.district_name
        });
                ViewBag.regions = selectListItemList;
      }
      return Json(new
      {
        visitorSelectionList = selectListItemList
      });
    }

    [HttpPost]
    [Route("participate/getWards/{event_id}")]
    public ActionResult getWards(long event_id)
    {
      List<SelectListItem> selectListItemList = new List<SelectListItem>();
      var wards  = this._dbContext.ward_master.Where(ev => ev.district_sno == (long?) event_id).Select(vd => new
      {
        ward_sno = vd.ward_sno,
        ward_name = vd.ward_name
      }).ToList();
      if (wards == null)
        return (ActionResult) this.Json((object) new
        {
          data = ""
        });
      foreach (var data in wards)
      {
        selectListItemList.Add(new SelectListItem()
        {
          Value = data.ward_sno.ToString(),
          Text = data.ward_name
        });

                ViewBag.wards = selectListItemList;
       }
      return Json(new
      {
        visitorSelectionList = selectListItemList
      });
    }

    [HttpPost]
    [Route("participate/register")]
    public ActionResult Register(Participate participate)
    {
      try
      {
        if (_dbContext.participants.Select (vd => vd.phone_number == participate.phone_number || vd.fullname.ToLower().Equals(participate.fullname.ToLower())).FirstOrDefault())
          return Json(new
          {
            message = "Participant exist, create new."
          });
        for (int index = 0; index < participate.opportunity.Count<string>(); ++index)
          this.Opp.Add(participate.opportunity[index]);
        for (int index = 0; index < participate.business_sector.Count<string>(); ++index)
          this.Busi.Add(participate.business_sector[index]);
        this._dbContext.participants.Add(new participant()
        {
          region = new long?(participate.region),
          district = new long?(participate.district),
          dob = participate.dob,
          fullname = participate.fullname,
          gender = participate.gender,
          phone_number = participate.phone_number,
          posted_by = "Anonymous",
          posted_date = DateTime.Now,
          ip_address = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"],
          entrepreneur_status = participate.entrepreneur_status,
          opportunity = string.Join(",", (IEnumerable<string>) this.Opp),
          business_sector = string.Join(",", (IEnumerable<string>) this.Busi)
        });
        this._dbContext.SaveChanges();
        return (ActionResult) this.Json((object) new
        {
          message = "Successfully Added."
        });
      }
      catch (Exception ex)
      {
        return (ActionResult) this.Json((object) new
        {
          createStatus = false,
          response = "Record creation failed.",
          message = ex.Message.ToString()
        });
      }
    }

    public ActionResult IndexDataTable()
    {
     var participants = this._dbContext.participants.Select(
       vd => new
      {
        pat_sno = vd.pat_sno,
        region = vd.region,
        district = vd.district,
        dob = vd.dob,
        fullname = vd.fullname,
        gender = vd.gender,
        business_sector = vd.business_sector,
        entrepreneur_status = vd.entrepreneur_status,
        opportunity = vd.opportunity,
        phone_number = vd.phone_number,
        posted_by = vd.posted_by,
        posted_date = vd.posted_date
      }).ToList();
      return (ActionResult) this.Json((object) new
      {
        data = participants
      });
    }
  }
}
