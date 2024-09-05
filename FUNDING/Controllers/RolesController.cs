using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FUNDING.BL.BusinessEntities.Masters;

namespace FUNDING.Controllers
{
    public class RolesController : BaseController
    {
        Role ot = new Role();
        private readonly dynamic returnNull = null;
        AuditLogs ad = new AuditLogs();
        Arights act = new Arights();
        String[] list = new String[7] { "sno", "code", "descript", "admin1", "role_status", "posted_by", "posted_date" };
        public ActionResult Roles()
        {
            if (Session["admin1"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                //var count = act.checkact("Role");
                var count = act.checkact("Role", long.Parse(Session["Desg"].ToString()), Session["role"].ToString(), Session["Rcode"].ToString());
                if (count == 0)
                {
                    return RedirectToAction("Setup", "Setup");
                }
            }
            return View();
        }
        public ActionResult GetRolesAct()
        {
            try
            {
                var result = ot.GetRole();
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
        public ActionResult GetRolesAct2()
        {
            try
            {
                var result = ot.GetRole2();
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
        public ActionResult GetRolesAct1()
        {
            try
            {
                var result = ot.GetRole1();
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
        public ActionResult AddRoles(string act, string name, long sno, bool dummy)
        {
            try
            {
                //var cd = ot.GetLastInsertedId();
                //long ct; String aID;
                //if (cd > 0)
                //{
                //     ct = cd + 1;
                //     aID = "00" + ct;
                //}
                //else
                //{
                //     aID = "00" + 1;
                //}
                //ot.Inst_reg = long.Parse(Session["InstituteID"].ToString());
                //ot.Code = aID;
                ot.Description = name;
                // ot.Admin1 = "N";
                ot.Status = act;
                // ot.AuditBy = Session["UserID"].ToString();
                ot.Sno = sno;
                long ssno = 0;
                if (sno == 0)
                {
                    //var result = ot.ValidateRole(name);
                    //if (result == true)
                    //{
                    //    return Json(result, JsonRequestBehavior.AllowGet);
                    //}
                    //else
                    //{
                    ssno = ot.AddRole(ot);
                    //if(ssno >0)
                    //{
                    //    String[] list1 = new String[7] { ssno.ToString(), ot.Code, name, "N", act, Session["UserID"].ToString(), DateTime.Now.ToString() };
                    //    for (int i = 0; i < list.Count(); i++)
                    //    {
                    //        ad.Audit_Type = "Insert";
                    //        ad.Columnsname = list[i];
                    //        ad.Table_Name = "Roles";
                    //        ad.Newvalues = list1[i];
                    //        ad.AuditBy = Session["UserID"].ToString();
                    //        ad.Audit_Date = DateTime.Now;
                    //        ad.Audit_Time = DateTime.Now;
                    //        ad.Inst_Sno = long.Parse(Session["InstituteID"].ToString());
                    //        ad.AddAudit(ad);
                    //    }
                    //}
                    var result1 = sno;
                    return Json(result1, JsonRequestBehavior.AllowGet);
                    // }
                }
                else if (sno > 0)
                {
                    if (dummy == false)
                    {
                        return Json(dummy, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        ssno = sno;
                        //var dd = ot.editroleText(sno, long.Parse(Session["InstituteID"].ToString()));
                        //if (ssno > 0)
                        //{
                        //    String[] list2 = new String[7] { dd.Sno.ToString(), dd.Code, dd.Description, dd.Admin1, dd.Status, dd.AuditBy, dd.Audit_Date.ToString() };
                        //    String[] list1 = new String[7] { ssno.ToString(), ot.Code, name, "N", act, Session["UserID"].ToString(), DateTime.Now.ToString() };
                        //    for (int i = 0; i < list.Count(); i++)
                        //    {
                        //        ad.Audit_Type = "Update";
                        //        ad.Columnsname = list[i];
                        //        ad.Table_Name = "Roles";
                        //        ad.Oldvalues = list2[i];
                        //        ad.Newvalues = list1[i];
                        //        ad.AuditBy = Session["UserID"].ToString();
                        //        ad.Audit_Date = DateTime.Now;
                        //        ad.Audit_Time = DateTime.Now;
                        //        ad.Inst_Sno = long.Parse(Session["InstituteID"].ToString());
                        //        ad.AddAudit(ad);
                        //    }
                        //}
                        ot.UpdateRole(ot);
                        var result1 = sno;
                        return Json(result1, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
            }
            return returnNull;
        }
        [HttpPost]
        public ActionResult RoleEdit(long sno)
        {
            try
            {
                var check = ot.editroleText(sno);
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
        public ActionResult DeleteRole(long sno)
        {
            try
            {
                if (sno > 0)
                {
                    //var dd = ot.editroleText(sno, long.Parse(Session["InstituteID"].ToString()));
                    //var result = ot.ValidateDeletion(dd.Code, long.Parse(Session["InstituteID"].ToString()));
                    //if (result == true)
                    //{
                    //    return Json(result, JsonRequestBehavior.AllowGet);
                    //}
                    //else
                    //{
                    //    if (dd != null)
                    //    {
                    //        String[] list2 = new String[7] { dd.Sno.ToString(), dd.Code, dd.Description, dd.Admin1, dd.Status, dd.AuditBy, dd.Audit_Date.ToString() };
                    //        for (int i = 0; i < list.Count(); i++)
                    //        {
                    //            ad.Audit_Type = "Delete";
                    //            ad.Columnsname = list[i];
                    //            ad.Table_Name = "Roles";
                    //            ad.Oldvalues = list2[i];
                    //            ad.AuditBy = Session["UserID"].ToString();
                    //            ad.Audit_Date = DateTime.Now;
                    //            ad.Audit_Time = DateTime.Now;
                    //            ad.Inst_Sno = long.Parse(Session["InstituteID"].ToString());
                    //            ad.AddAudit(ad);
                    //        }
                    //    }
                    //    ot.Deleterole(dd.Code);
                    return Json(sno, JsonRequestBehavior.AllowGet);
                    //}
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