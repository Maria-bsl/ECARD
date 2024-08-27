using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FUNDING.BL.BusinessEntities.Masters;

namespace FUNDING.Controllers
{
    public class DesignationController : BaseController
    {
        DESIGNATION des = new DESIGNATION();
        private readonly dynamic returnNull = null;
        EMP_DET ed = new EMP_DET();
        AuditLogs ad = new AuditLogs();
        Arights act = new Arights();
        String[] list = new String[4] { "desg_id", "desg_name", "posted_by", "posted_date" };
        public ActionResult Designation()
        {
            if (Session["admin1"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                //var count = act.checkact("Designation");
                var count = act.checkact("Designation", long.Parse(Session["Desg"].ToString()), Session["role"].ToString(), Session["Rcode"].ToString());
                if (count == 0)
                {
                    return RedirectToAction("Setup", "Setup");
                }
            }
            return View();
        }
        public ActionResult GetdesDetails()
        {
            try
            {
                var result = des.GetDesignation();
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
                Ex.Message.ToString();
            }
            return returnNull;
        }
        [HttpPost]
        public ActionResult Adddesg(string desg, long sno, bool dummy)
        {
            try
            {
                des.Desg_Name = desg;
                des.Desg_Id = sno;
                //des.Posted_By = Session["UserID"].ToString();
                long ssno = 0;
                if (sno == 0)
                {
                    var result = des.ValidateDesignation(desg.ToLower());
                    if (result == true)
                    {
                        return Json(result, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        ssno = des.AddUser(des);
                        if (ssno > 0)
                        {
                            String[] list1 = new String[4] { ssno.ToString(), desg, Session["UserID"].ToString(), DateTime.Now.ToString() };
                            for (int i = 0; i < list.Count(); i++)
                            {
                                ad.Audit_Type = "Insert";
                                ad.Columnsname = list[i];
                                ad.Table_Name = "Designation";
                                ad.Newvalues = list1[i];
                                ad.AuditBy = Session["UserID"].ToString();
                                ad.Audit_Date = DateTime.Now;
                                ad.Audit_Time = DateTime.Now;
                                ad.AddAudit(ad);
                            }
                        }
                        return Json(ssno, JsonRequestBehavior.AllowGet);
                    }
                }
                else if (sno > 0)
                {
                    if (dummy == false)
                    {
                        return Json(dummy, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        var dd = des.Editdesignation(sno);
                        if (dd != null)
                        {
                            String[] list2 = new String[4] { dd.Desg_Id.ToString(), dd.Desg_Name, dd.Posted_By, dd.Posted_Date.ToString() };
                            String[] list1 = new String[4] { dd.Desg_Id.ToString(), desg, Session["UserID"].ToString(), DateTime.Now.ToString() };
                            for (int i = 0; i < list.Count(); i++)
                            {
                                ad.Audit_Type = "Update";
                                ad.Columnsname = list[i];
                                ad.Table_Name = "Designation";
                                ad.Oldvalues = list2[i];
                                ad.Newvalues = list1[i];
                                ad.AuditBy = Session["UserID"].ToString();
                                ad.Audit_Date = DateTime.Now;
                                ad.Audit_Time = DateTime.Now;
                                ad.AddAudit(ad);
                            }
                        }
                        des.UpdateDesignation(des);
                        ssno = sno;
                        return Json(ssno, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }
            return returnNull;
        }
        public ActionResult Deletedesg(long sno)
        {
            try
            {
                var check = des.ValidateDeletion(sno);
                if (check == true)
                {
                    return Json(check, JsonRequestBehavior.AllowGet);
                }
                else if (sno > 0)
                {
                    var dd = des.Editdesignation(sno);
                    if (dd != null)
                    {
                        String[] list2 = new String[4] { dd.Desg_Id.ToString(), dd.Desg_Name, dd.Posted_By, dd.Posted_Date.ToString() };
                        for (int i = 0; i < list.Count(); i++)
                        {
                            ad.Audit_Type = "Delete";
                            ad.Columnsname = list[i];
                            ad.Table_Name = "Designation";
                            ad.Oldvalues = list2[i];
                            ad.AuditBy = Session["UserID"].ToString();
                            ad.Audit_Date = DateTime.Now;
                            ad.Audit_Time = DateTime.Now;
                            ad.AddAudit(ad);
                        }
                    }
                    des.DeleteDesignation(sno);
                }
                var result = sno;
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
            }
            return returnNull;
        }
    }
}