using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using FUNDING.BL.BusinessEntities.Masters;
using FUNDING.BL.BusinessEntities.ConstantFile;

namespace FUNDING.Controllers
{
    public class SMSSETNGSController : BaseController
    {
        // GET: SMSSETNGS
        SMS_SETTNG smtp = new SMS_SETTNG();
        EMP_DET ed = new EMP_DET();
        private readonly dynamic returnNull = null;
        Arights act = new Arights();
        public ActionResult SMSSETNGS()
        {
            if (Session["admin1"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                var count = act.checkact("SMS Settings", long.Parse(Session["Desg"].ToString()), Session["role"].ToString(), Session["Rcode"].ToString());
                if (count == 0)
                {
                    return RedirectToAction("Setup", "Setup");
                }
            }
            return View();
        }


        [HttpPost]
        public ActionResult AddSMTP(string from_address, string smtp_uname, string smtp_pwd, string smtp_mob, string posted_by, long sno)
        {
            try
            {
                smtp.From_Address = from_address;
                smtp.USER_Name = smtp_uname;
                smtp.Password = Utilites.GetEncryptedData(smtp_pwd);
                smtp.Mobile_Service = smtp_mob;
                smtp.AuditBy = Session["UserID"].ToString();
                smtp.SNO = sno;
                long ssno = 0;
                if (sno == 0)
                {
                    ssno = smtp.AddSMS(smtp);
                    return Json(ssno, JsonRequestBehavior.AllowGet);

                }
                else if (sno > 0)
                {
                    smtp.UpdateSMS(smtp);
                    ssno = sno;
                    return Json(ssno, JsonRequestBehavior.AllowGet);
                }


            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            return returnNull;
        }
        [HttpPost]
        public ActionResult DeleteSMTP(long sno)
        {
            try
            {

                smtp.SNO = sno;
                if (sno > 0)
                {
                    smtp.DeleteSMS(sno);
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
        [HttpPost]
        public ActionResult GetSMTPDetails()
        {
            try
            {
                var result = smtp.GetSMS();
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



    }
}