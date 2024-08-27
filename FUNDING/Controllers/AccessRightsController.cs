// Decompiled with JetBrains decompiler
// Type: FUNDING.Controllers.AccessRightsController
// Assembly: FUNDING, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5DE1C1E8-8D80-43BD-915D-F2502219BD58
// Assembly location: D:\ECardtest - 22082024\bin\FUNDING.dll

using FUNDING.BL.BusinessEntities.Masters;
using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Web.Mvc;

 
namespace FUNDING.Controllers
{
  public class AccessRightsController : BaseController
  {
    private Modules ot = new Modules();
    private Comments cm = new Comments();
    private Arights act = new Arights();
    private AuditLogs ad = new AuditLogs();
    private string[] list = new string[19]
    {
      "sno",
      "rcode",
      "rdesc",
      "radmin",
      "role_status",
      "emp_detail_id",
      "usernaem",
      "mcode",
      "mname",
      "module_status",
      "acode",
      "adescript",
      "aadd",
      "asearch",
      "aupdate",
      "adelete",
      "aview",
      "posted_by",
      "posted_date"
    };
    private readonly dynamic returnNull = null;

    [Route("Access-Rights")]
    public ActionResult AccessRights()
    {
      if (this.Session["admin1"] == null)
        return (ActionResult) this.RedirectToAction("Login", "Login");
      return this.act.checkact(nameof (AccessRights), long.Parse(this.Session["Desg"].ToString()), this.Session["role"].ToString(), this.Session["Rcode"].ToString()) == 0L ? (ActionResult) this.RedirectToAction("Setup", "Setup") : (ActionResult) this.View();
    }

    public ActionResult Getart(string mcode, string rcode)
    {
      try
      {
        List<Arights> aright1 = this.act.GetAright1(mcode, rcode);
        return aright1 != null ? (ActionResult) this.Json((object) aright1, JsonRequestBehavior.AllowGet) : (ActionResult) this.Json((object) 0, JsonRequestBehavior.AllowGet);
      }
      catch (Exception ex)
      {
        ex.ToString();
      }
            return returnNull;
    }

    public ActionResult Getscreen(string name)
    {
      try
      {
        return (ActionResult) this.Json((object) this.act.Validatescreen(name), JsonRequestBehavior.AllowGet);
      }
      catch (Exception ex)
      {
        ex.ToString();
      }

      return returnNull;
    }

    public ActionResult Checkbox(string name)
    {
      try
      {
        List<Modules> module1 = this.ot.GetModule1();
        List<string> data = new List<string>();
        foreach (Modules modules in module1)
        {
          int num = this.act.check(name, modules.Description) ? 1 : 0;
          data.Add(Convert.ToString(num));
        }
        return (ActionResult) this.Json((object) data, JsonRequestBehavior.AllowGet);
      }
      catch (Exception ex)
      {
        ex.ToString();
      }
      
      return returnNull;
    }

    public ActionResult Editright(string name)
    {
      try
      {
        this.Session["User_name"].ToString();
        Arights aright2 = this.act.GetAright2(name, long.Parse(this.Session["UserID"].ToString()));
        return aright2 != null ? (ActionResult) this.Json((object) aright2, JsonRequestBehavior.AllowGet) : (ActionResult) this.Json((object) 0, JsonRequestBehavior.AllowGet);
      }
      catch (Exception ex)
      {
        ex.ToString();
      }
      return this.returnNull;
    }

    public ActionResult addacess(List<Arights> details)
    {
      try
      {
        long data = 0;
        for (int index = 0; index < details.Count; ++index)
        {
          string rcode = details[index].RCode;
          if (details[index].Sno > 0L)
          {
            this.act.RCode = details[index].RCode;
            this.act.RDesc = details[index].RDesc;
            this.act.Radmin = details[index].Radmin;
            this.act.Role_Status = details[index].Role_Status;
            this.act.MCode = details[index].MCode;
            this.act.MName = details[index].MName;
            this.act.Module_Status = details[index].Module_Status;
            this.act.Acode = details[index].Acode;
            this.act.Adescrip = details[index].Adescrip;
            this.act.ADelete = details[index].ADelete;
            this.act.AUpdate = details[index].AUpdate;
            this.act.AAdd = details[index].AAdd;
            this.act.Aview = details[index].Aview;
            this.act.ASearch = "Y";
            this.act.Sno = details[index].Sno;
            this.act.AuditBy = this.Session["UserID"].ToString();
            this.act.UpdateAct(this.act);
          }
          else
          {
            this.act.RCode = details[index].RCode;
            this.act.RDesc = details[index].RDesc;
            this.act.Radmin = details[index].Radmin;
            this.act.Role_Status = details[index].Role_Status;
            this.act.MCode = details[index].MCode;
            this.act.MName = details[index].MName;
            this.act.Module_Status = details[index].Module_Status;
            this.act.Acode = details[index].Acode;
            this.act.Adescrip = details[index].Adescrip;
            this.act.ADelete = details[index].ADelete;
            this.act.AUpdate = details[index].AUpdate;
            this.act.AAdd = details[index].AAdd;
            this.act.Aview = details[index].Aview;
            this.act.ASearch = "Y";
            this.act.Sno = details[index].Sno;
            this.act.AuditBy = this.Session["UserID"].ToString();
            this.act.AddAright(this.act);
            data = details[index].Sno;
          }
        }
        return (ActionResult) this.Json((object) data, JsonRequestBehavior.AllowGet);
      }
      catch (Exception ex)
      {
        ex.ToString();
      }
      
      return this.returnNull;
    }
  }
}
