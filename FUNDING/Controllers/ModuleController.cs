using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FUNDING.BL.BusinessEntities.Masters;

namespace FUNDING.Controllers
{
    public class ModuleController : Controller
    {
        Modules ot = new Modules();
        Arights act = new Arights();
        private readonly dynamic returnNull = null;
        public ActionResult Module()
        {
            if (Session["admin1"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            return View();
        }
        public ActionResult GetModuleAct()
        {
            try
            {
                var result = ot.GetModule1();
                if (result != null)
                {
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var d = 0;
                    return Json(d, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }
            return returnNull;
        }
        [HttpPost]
        public ActionResult AddModules(string code, string act, string name, long sno)
        {
            try
            {
                ot.Code = code;
                ot.Description = name;
                ot.Status = act;
                ot.AuditBy = Session["UserID"].ToString();
                ot.Sno = sno;
                long ssno = 0;
                if (sno == 0)
                {
                    var result = ot.ValidateModule(name);
                    if (result == true)
                    {
                        return Json(result, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        ssno = ot.AddModule(ot);
                        var result1 = sno;
                        return Json(result1, JsonRequestBehavior.AllowGet);
                    }
                }
                else if (sno > 0)
                {
                    //var result = ot.Validateage(from, to, long.Parse(Session["InstituteID"].ToString()));
                    //if (result == true)
                    //{
                    //    return Json(result, JsonRequestBehavior.AllowGet);
                    //}
                    //else
                    //{
                    ssno = sno;
                    ot.UpdateModule(ot);
                    //}
                    var result1 = sno;
                    return Json(result1, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
            }
            return returnNull;
        }
        [HttpPost]
        public ActionResult ModuleEdit(long sno)
        {
            try
            {
                var check = ot.editmoduleText(sno, long.Parse(Session["InstituteID"].ToString()));
                if (check != null)
                {
                    return Json(check, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(check, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }
            return returnNull;
        }
        [HttpPost]
        public ActionResult DeleteModules(long sno)
        {
            try
            {
                if (sno > 0)
                {
                    var result = ot.ValidateDeletion(sno);
                    if (result == true)
                    {
                        return Json(result, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        ot.Deletemodule(sno);
                        return Json(sno, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }
            return returnNull;
        }
    }
}