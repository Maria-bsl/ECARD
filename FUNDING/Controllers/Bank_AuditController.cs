// Decompiled with JetBrains decompiler
// Type: FUNDING.Controllers.Bank_AuditController
// Assembly: FUNDING, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5DE1C1E8-8D80-43BD-915D-F2502219BD58
// Assembly location: D:\ECardtest - 22082024\bin\FUNDING.dll

using FUNDING.BL.BusinessEntities.Masters;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

 
namespace FUNDING.Controllers
{
  public class Bank_AuditController : BaseController
  {
    private AuditLogs ad = new AuditLogs();
    private Tab_Details tb = new Tab_Details();
    private EMP_DET ed = new EMP_DET();
    private Arights act = new Arights();

    [Route("Audit-Trail")]
    public ActionResult Bank_Audit()
    {
      if (this.Session["admin1"] == null)
        return (ActionResult) this.RedirectToAction("Login", "Login");
      if (this.act.checkact("AuditTrails", long.Parse(this.Session["Desg"].ToString()), this.Session["role"].ToString(), this.Session["Rcode"].ToString()) == 0L)
        return (ActionResult) this.RedirectToAction("Setup", "Setup");
      this.ed.Detail_Id = Convert.ToInt64(this.Session["UserID"].ToString());
      this.ed.UpdateOnlyflsgtrue(this.ed);
      return (ActionResult) this.View();
    }

    [HttpPost]
    public ActionResult getdet(string tbname, string Startdate, string Enddate, string act)
    {
      try
      {
        List<object> data = new List<object>();
        string s1 = DateTime.ParseExact(Startdate, "dd/MM/yyyy", (IFormatProvider) null).ToString("MM/dd/yyyy");
        string s2 = DateTime.ParseExact(Enddate, "dd/MM/yyyy", (IFormatProvider) null).ToString("MM/dd/yyyy");
        DateTime frm = DateTime.Parse(s1);
        DateTime to = DateTime.Parse(s2);
        Tab_Details tabDetails = this.tb.Getlog(tbname);
        List<AuditLogs> bloglist = this.ad.GetBloglist(frm, to, tbname, act, tabDetails.Relation);
        if (bloglist == null)
          return (ActionResult) this.Json((object) 0, JsonRequestBehavior.AllowGet);
        foreach (AuditLogs auditLogs in bloglist)
          data.Add((object) new
          {
            ovalue = auditLogs.Oldvalues,
            nvalue = auditLogs.Newvalues,
            atype = auditLogs.Audit_Type,
            colname = auditLogs.Columnsname,
            aby = auditLogs.AuditBy,
            adate = auditLogs.Audit_Date
          });
        return (ActionResult) this.Json((object) data, JsonRequestBehavior.AllowGet);
      }
      catch (Exception ex)
      {
        ex.ToString();
      }
      return (ActionResult) null;
    }
  }
}
