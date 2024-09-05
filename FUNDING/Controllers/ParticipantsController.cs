
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
  public class ParticipantsController : Controller
  {
    private readonly ECARDAPPEntities _dbContext = new ECARDAPPEntities();
    private List<string> Opp = new List<string>();
    private List<string> Busi = new List<string>();

    public ActionResult Index()
    {
   

            ViewBag.regions =  new SelectList((IEnumerable) this._dbContext.region_master, "region_sno", "region_name");
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

            ViewBag.entrepreneur = selectListItemList2;


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
            List<SelectListItem> selectListItemList4 = new List<SelectListItem>
            {
                new SelectListItem()
                {
                    Text = "Agribusiness /Biashara Kilimo",
                    Value = "Agribusiness /Biashara Kilimo"
                },
                new SelectListItem()
                {
                    Text = "Manufacturing /Uzalishaji",
                    Value = "Manufacturing /Uzalishaji"
                },
                new SelectListItem()
                {
                    Text = "Service /Huduma",
                    Value = "Service /Huduma"
                },
                new SelectListItem()
                {
                    Text = "Logistics /Usafirishaji",
                    Value = "Logistics /Usafirishaji"
                },
                new SelectListItem()
                {
                    Text = "Mining /Madini",
                    Value = "Mining /Madini"
                },
                new SelectListItem()
                {
                    Text = "Export /Usafirishaji",
                    Value = "Export /Usafirishaji"
                },
                new SelectListItem()
                {
                    Text = "Others /Nyenginezo",
                    Value = "Others /Nyenginezo"
                }
            };

            
            ViewBag.business = selectListItemList4;

            return (ActionResult) this.View();
    }

    [HttpPost]
    [Route("participants/getDistricts/{event_id}")]
    public ActionResult getDistricts(long event_id)
    {
      List<SelectListItem> selectListItemList = new List<SelectListItem>();
      var list = this._dbContext.district_master.Where(ev => ev.region_id == (long?) event_id).Select(vd => new
      {
        vd.district_sno,
        vd.district_name
      }).ToList();
      if (list == null)
        return  this.Json(new
        {
          data = ""
        });
      foreach (var data in list)
      {
        selectListItemList.Add(new SelectListItem()
        {
          Value = data.district_sno.ToString(),
          Text = data.district_name
        });


                ViewBag.regions = selectListItemList;

      }

      return (ActionResult) this.Json((object) new
      {
        visitorSelectionList = selectListItemList
      });
    }

    [HttpPost]
    [Route("participants/getWards/{event_id}")]
    public ActionResult getWards(long event_id)
    {
      List<SelectListItem> selectListItemList = new List<SelectListItem>();
      var wards = _dbContext.ward_master.Where(ev => ev.district_sno == (long?) event_id).Select(vd => new
      {
        ward_sno = vd.ward_sno,
        ward_name = vd.ward_name
      }).ToList();
      if (wards == null)
        return Json(new
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
      return (ActionResult) this.Json((object) new
      {
        visitorSelectionList = selectListItemList
      });
    }

        [HttpPost]
        [Route("participants/indexdatatable")]
        public ActionResult IndexDataTable()
        {

            var participants = (from sc in _dbContext.participants
                                join f in _dbContext.district_master on sc.district equals f.district_sno
                                join u in _dbContext.region_master on sc.region equals u.region_sno

                                select (new
                                {
                                    pat_sno = sc.pat_sno,
                                    region = u.region_name,
                                    district = f.district_name,
                                    dob = sc.dob,
                                    fullname = sc.fullname,
                                    gender = sc.gender,
                                    phone_number = sc.phone_number,
                                    business_sector = sc.business_sector,
                                    entrepreneur_status = sc.entrepreneur_status,
                                    ip_address = sc.ip_address,
                                    opportunity = sc.opportunity,
                                    posted_by = sc.posted_by,
                                    posted_date = sc.posted_date
                                })).ToList();

            return (ActionResult)this.Json((object)new
            {
                data = participants
            });
          }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult IsMobileNumberAvailable(
      string __RequestVerificationToken,
      string Int_Mobile_number,
      string Mobile_number_clone)
    {
      string mobileNumber = Int_Mobile_number.Replace("+", "");
      if (Mobile_number_clone != null)
      {
        string str = Mobile_number_clone.Replace("+", "");
        if (mobileNumber == str)
          return (ActionResult) this.Json((object) true);
      }
      var data = this._dbContext.participants.Where(u => u.phone_number == mobileNumber).Select(u => new
      {
        mobileNumber = mobileNumber
      }).FirstOrDefault();
      return Json((data == null ? (true ? 1 : 0) : (false ? 1 : 0)));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult AjaxHelperCreateForm([Bind(Include = "fullname, gender, dob, phone_number, Int_Mobile_number, region, ward, district, opportunity, entrepreneur_status, business_sector")] Participate participate)
    {
      if (this._dbContext.participants.Select<participant, bool>((Expression<Func<participant, bool>>) (vd => vd.phone_number == participate.phone_number || vd.fullname == participate.fullname)).FirstOrDefault<bool>())
        return (ActionResult) this.Json((object) new
        {
          message = "Participant exist, create new."
        });
      for (int index = 0; index < participate.opportunity.Count<string>(); ++index)
        this.Opp.Add(participate.opportunity[index]);
      for (int index = 0; index < participate.business_sector.Count<string>(); ++index)
        this.Busi.Add(participate.business_sector[index]);
      participant entity = new participant()
      {
        region = new long?(participate.region),
        district = new long?(participate.district),
        dob = participate.dob,
        fullname = participate.fullname,
        gender = participate.gender,
        ip_address = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"],
        entrepreneur_status = participate.entrepreneur_status,
        opportunity = string.Join(",", (IEnumerable<string>) this.Opp),
        business_sector = string.Join(",", (IEnumerable<string>) this.Busi),
        phone_number = participate.Int_Mobile_number.Replace("+", ""),
        posted_by = "Anonymous",
        posted_date = DateTime.Now
      };
      try
      {
        this._dbContext.participants.Add(entity);
        this._dbContext.SaveChanges();
        return (ActionResult) this.Json((object) new
        {
          createStatus = true,
          response = "Record successful created."
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

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult AjaxHelperUpdateForm([Bind(Include = "pat_sno, fullname, gender, dob, phone_number, Int_Mobile_number, region, ward, district, opportunity, entrepreneur_status, business_sector")] Participate participate)
    {
      this.ModelState.Remove("mobile_no");
      for (int index = 0; index < participate.opportunity.Count<string>(); ++index)
        this.Opp.Add(participate.opportunity[index]);
      for (int index = 0; index < participate.business_sector.Count<string>(); ++index)
        this.Busi.Add(participate.business_sector[index]);
      participant entity = this._dbContext.participants.Find(new object[1]
      {
        participate.pat_sno
      });
      if (entity == null)
        return Json( new
        {
          updateStatus = false,
          response = "Failed! Record does not exist."
        });
      entity.fullname = participate.fullname;
      entity.dob = participate.dob;
      entity.region = new long?(participate.region);
      entity.gender = participate.gender;
      entity.district = new long?(participate.district);
      entity.phone_number = participate.Int_Mobile_number.Replace("+", "");
      entity.entrepreneur_status = participate.entrepreneur_status;
      entity.opportunity = string.Join(",", (IEnumerable<string>) this.Opp);
      entity.business_sector = string.Join(",", (IEnumerable<string>) this.Busi);
      entity.posted_date = DateTime.Now;
      entity.posted_by = "Support";
      this._dbContext.Entry<participant>(entity).State = EntityState.Modified;
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
      participant entity = this._dbContext.participants.Find(new object[1]
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
        this._dbContext.participants.Remove(entity);
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
  }
}
