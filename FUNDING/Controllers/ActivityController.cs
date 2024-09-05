using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FUNDING.BL.BusinessEntities.Masters;

namespace FUNDING.Controllers
{
    public class ActivityController : Controller
    {
        Activities ot = new Activities();
        Modules md = new Modules();
        Comments cm = new Comments();
        private readonly dynamic returnNull = null;
        public ActionResult Activity()
        {
            if (Session["admin1"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            long count = 0;
            try
            {
                long aID = 0;
                count = ot.GetLastInsertedId();

                var value = ot.editactText1((int)count);

                if (value != null)
                {
                    aID = long.Parse(value.Code);
                    aID++;

                    ViewData["Num"] = aID.ToString();
                }
            }
            catch
            {
                count = 1;
                ViewData["Num"] = count;
            }
            return View();

        }
        public ActionResult GetactAct()
        {
            try
            {
                var result = ot.GetAct();
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
        public ActionResult GetactActlist(long sno)
        {
            try
            {
                var result = ot.GetActlist(sno);
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
        public ActionResult AddAct(string code, string act, string name, long sno, long msno, string mname)
        {
            try
            {
                ot.Code = code;
                ot.Description = name;
                ot.Mod_Sno = msno;
                ot.Name = mname;
                ot.Status = act;
                ot.AuditBy = Session["UserID"].ToString();
                ot.Sno = sno;
                long ssno = 0;
                if (sno == 0)
                {
                    var result = ot.ValidateAct(name, msno);

                    if (result == true)
                    {
                        return Json(result, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {

                        ssno = ot.AddAct(ot);
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
                    ot.UpdateAct(ot);

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
        public ActionResult ActEdit(long sno)
        {
            try
            {
                var check = ot.editactText(sno);
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
        public ActionResult DeleteAct(long sno)
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
                        ot.DeleteAct(sno);
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