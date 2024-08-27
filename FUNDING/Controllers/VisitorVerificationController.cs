// Decompiled with JetBrains decompiler
// Type: FUNDING.Controllers.VisitorVerificationController
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
  public class VisitorVerificationController : Controller
  {
    private readonly Visitors _visitor = new Visitors();
    private readonly ECARDAPPEntities _dbContext = new ECARDAPPEntities();

    [Route("Invitee-Verification")]
    public ActionResult Index() => (ActionResult) this.View();

    public ActionResult IndexDataTable()
    {
      long eventAdminID = Convert.ToInt64(this.Session["EventAdminID"]);
      var eventID = this.GetEventID();
      var qr = this._dbContext.qr_verify_details.Where<qr_verify_details>((Expression<Func<qr_verify_details, bool>>) (v => v.event_det_sno == eventID && v.cust_reg_sno == (long?) eventAdminID)).Select(vd => new
      {
        qr_ver_det_sno = vd.qr_ver_det_sno,
        visitor_name = vd.visitor_details.visitor_name,
        no_of_persons = vd.no_of_persons,
        event_start_time = vd.event_start_time,
        posted_date = vd.posted_date,
        scan_status = vd.scan_status
      }).ToList();
      int index = 1;
      return (ActionResult) this.Json((object) new
      {
        data = qr.Select(vd =>
        {
          int num = index++;
          long qrVerDetSno = vd.qr_ver_det_sno;
          string visitorName = vd.visitor_name;
          int? noOfPersons = vd.no_of_persons;
          DateTime dateTime = Convert.ToDateTime((object) vd.event_start_time);
          string str1 = dateTime.ToString("hh:mm tt");
          dateTime = Convert.ToDateTime(vd.posted_date);
          dateTime = dateTime.AddMinutes(-150.0);
          string str2 = dateTime.ToString("hh:mm:ss tt");
          string scanStatus = this.GetScanStatus(vd.qr_ver_det_sno);
          return new
          {
            id = num,
            qr_ver_det_sno = qrVerDetSno,
            visitor_name = visitorName,
            no_of_persons = noOfPersons,
            event_start_time = str1,
            check_in_time = str2,
            scan_status = scanStatus
          };
        }),
        TotalInvitees = qr.Select(t => t.no_of_persons).DefaultIfEmpty<int?>(new int?(0)).Sum()
      });
    }

    private string GetScanStatus(long qr_ver_det_sno)
    {
      IQueryable<qr_verify_details> source = this._dbContext.qr_verify_details.Include("visitor_details").Where<qr_verify_details>((Expression<Func<qr_verify_details, bool>>) (c => c.qr_ver_det_sno == qr_ver_det_sno));
      if (source == null)
        return (string) null;
      qr_verify_details qrVerifyDetails = source.ToList<qr_verify_details>()[0];
      int int32_1 = Convert.ToInt32((object) qrVerifyDetails.no_of_persons);
      string qrCode = qrVerifyDetails.qrcode;
      int int32_2 = Convert.ToInt32((object) qrVerifyDetails.visitor_details.no_of_persons);
      int num = Queryable.Sum(this._dbContext.qr_verify_details.Where<qr_verify_details>((Expression<Func<qr_verify_details, bool>>) (iv => iv.qrcode == qrCode && iv.qr_ver_det_sno < qr_ver_det_sno)).Select<qr_verify_details, int?>((Expression<Func<qr_verify_details, int?>>) (iv => iv.no_of_persons)).DefaultIfEmpty<int?>(new int?(0))).Value;
      return int32_1 < int32_2 - num ? string.Format("{0}/{1} Scanned", (object) (num + int32_1), (object) int32_2) : string.Format("{0}/{1} Scanned", (object) int32_2, (object) int32_2);
    }

    private long? GetCustomerAdminID() => new long?(Convert.ToInt64(this.Session["EventAdminID"]));

    private long? GetEventID() => new long?(Convert.ToInt64(this.Session["EventID"]));
  }
}
