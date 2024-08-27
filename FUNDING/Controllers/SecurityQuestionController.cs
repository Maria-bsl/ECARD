using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FUNDING.BL.BusinessEntities.Masters;

namespace FUNDING.Controllers
{
    public class SecurityQuestionController : BaseController
    {
        Sec_Ques sec = new Sec_Ques();
        AuditLogs ad = new AuditLogs();
        Arights act = new Arights();
        String[] list = new String[5] { "sno", "q_name", "q_status", "posted_by", "posted_date" };
        private readonly dynamic returnNull = null;
        public ActionResult SecurityQuestion()
        {
            if (Session["admin1"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                //var count = act.checkact("Security Question");
                var count = act.checkact("Security Question", long.Parse(Session["Desg"].ToString()), Session["role"].ToString(), Session["Rcode"].ToString());
                if (count == 0)
                {
                    return RedirectToAction("Setup", "Setup");
                }
            }
            return View();
        }
        [HttpPost]
        public ActionResult AddSec(string name, string status, long sno, bool dummy)
        {
            try
            {
                sec.Question_Name = name;
                sec.Status = status;
                sec.Posted_By = Session["UserID"].ToString();
                sec.SNO = sno;
                long ssno = 0;
                if (sno == 0)
                {
                    var result = sec.ValidateSecQ(name.ToLower());
                    if (result == true)
                    {
                        return Json(result, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        ssno = sno;
                        ssno = sec.AddSecQues(sec);
                        if (ssno > 0)
                        {
                            String[] list1 = new String[5] { ssno.ToString(), sec.Question_Name, sec.Status, sec.Posted_By, sec.Posted_Date.ToString() };
                            for (int i = 0; i < list.Count(); i++)
                            {
                                ad.Audit_Type = "Insert";
                                ad.Columnsname = list[i];
                                ad.Table_Name = "reg_question";
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
                    var result1 = sec.Validateupdatecheckcont(sno);
                    if (result1 == false)
                    {
                        if (dummy == false)
                        {
                            return Json(dummy, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            var dd = sec.EditSecQ(sno);
                            if (dd != null)
                            {
                                String[] list2 = new String[5] { ssno.ToString(), dd.Question_Name, dd.Status, dd.Posted_By, dd.Posted_Date.ToString() };
                                String[] list1 = new String[5] { sec.SNO.ToString(), sec.Question_Name, sec.Status, sec.Posted_By, sec.Posted_Date.ToString() };
                                for (int i = 0; i < list.Count(); i++)
                                {
                                    ad.Audit_Type = "Update";
                                    ad.Columnsname = list[i];
                                    ad.Table_Name = "reg_question";
                                    ad.Oldvalues = list2[i];
                                    ad.Newvalues = list1[i];
                                    ad.AuditBy = Session["UserID"].ToString();
                                    ad.Audit_Date = DateTime.Now;
                                    ad.Audit_Time = DateTime.Now;
                                    ad.AddAudit(ad);
                                }
                            }
                            sec.UpdateSecQ(sec);
                            ssno = sno;
                            return Json(ssno, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json(result1, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }
            return returnNull;
        }
        [HttpPost]
        public ActionResult DeleteSec(long sno)
        {
            try
            {
                sec.SNO = sno;
                var name = sec.Validateupdatecheckcont(sno);
                if (name == true)
                {
                    return Json(name, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    if (sno > 0)
                    {
                        var dd = sec.EditSecQ(sno);
                        if (dd != null)
                        {
                            String[] list2 = new String[5] { dd.SNO.ToString(), dd.Question_Name, dd.Status, dd.Posted_By, dd.Posted_Date.ToString() };
                            for (int i = 0; i < list.Count(); i++)
                            {
                                ad.Audit_Type = "Delete";
                                ad.Columnsname = list[i];
                                ad.Table_Name = "reg_question";
                                ad.Oldvalues = list2[i];
                                ad.AuditBy = Session["UserID"].ToString();
                                ad.Audit_Date = DateTime.Now;
                                ad.Audit_Time = DateTime.Now;
                                ad.AddAudit(ad);
                            }
                        }
                        sec.DeleteSecQ(sno);
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
        [HttpPost]
        public ActionResult GetQues()
        {
            try
            {
                var result = sec.GETQuestions();
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
    }
}