// Decompiled with JetBrains decompiler
// Type: FUNDING.Controllers.ChangePasswordController
// Assembly: FUNDING, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5DE1C1E8-8D80-43BD-915D-F2502219BD58
// Assembly location: D:\ECardtest - 22082024\bin\FUNDING.dll

using FUNDING.Models;
using System;
using System.Text;
using System.Web.Mvc;

 
namespace FUNDING.Controllers
{
  public class ChangePasswordController : Controller
  {
    [Route("Change-Password")]
    public ActionResult Index()
    {
      return this.Session["admin1"] == null ? (ActionResult) this.RedirectToAction("CustomerLogin", "Login") : (ActionResult) this.View();
    }

    [HttpPost]
    public ActionResult CheckPassword(string password)
    {
      bool data = new ChangePasswordViewModel().ValidatePassword(this.GetEncryptedData(password));
      int num = data ? 1 : 0;
      return (ActionResult) this.Json((object) data);
    }

    [HttpPost]
    public ActionResult UpdatePassword(string password)
    {
      if (password == null)
        return (ActionResult) this.Json((object) "Failed");
      new ChangePasswordViewModel().UpdatePassword(Convert.ToInt64(this.Session["UserID"]), this.GetEncryptedData(password));
      return (ActionResult) this.Json((object) "Success");
    }

    private string GetEncryptedData(string value)
    {
      byte[] numArray = new byte[value.Length];
      return Convert.ToBase64String(Encoding.UTF8.GetBytes(value));
    }
  }
}
