
using FUNDING.BL.BusinessEntities.Masters;
using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Runtime.CompilerServices;
using System.Web.Mvc;

 
namespace FUNDING.Controllers
{
  public class ChangepwdController : BaseController
  {
    private EMP_DET ed = new EMP_DET();
    private LoginController lg = new LoginController();
    private readonly dynamic returnNull = null;

    [Route("Admin/Change-Password")]
    public ActionResult Changepwd()
    {
      return this.Session["admin1"] == null ? (ActionResult) this.RedirectToAction("Login", "Login") : (ActionResult) this.View();
    }

    [HttpPost]
    public ActionResult Getcurpwd(string pwd)
    {
      try
      {
        this.lg.GetEncryptedData(pwd);
        bool data = this.ed.Validateuserpwd(this.lg.GetEncryptedData(pwd));
        int num = data ? 1 : 0;
        return (ActionResult) this.Json((object) data, JsonRequestBehavior.AllowGet);
      }
      catch (Exception ex)
      {
        ex.ToString();
      }
      return  this.returnNull;
    }

    [HttpPost]
    public ActionResult updatepwd(string pwd)
    {
      try
      {
        this.ed.Detail_Id = Convert.ToInt64(this.Session["UserID"].ToString());
        this.ed.Password = this.lg.GetEncryptedData(pwd);
        this.ed.UpdateOnlyflsg(this.ed);
        this.ed.UpdateOnlypwd(this.ed);
        return (ActionResult) this.Json((object) 23, JsonRequestBehavior.AllowGet);
      }
      catch (Exception ex)
      {
        ex.ToString();
      }
      return this.returnNull;
    }
  }
}
