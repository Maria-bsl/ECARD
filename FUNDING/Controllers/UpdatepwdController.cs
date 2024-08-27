using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Configuration;
using FUNDING.BL.BusinessEntities.Masters;
using System.Web.UI.WebControls.WebParts;

namespace FUNDING.Controllers
{
    public class UpdatepwdController : Controller
    {
        // GET: Updatepwd
        Sec_Ques q = new Sec_Ques();
        EMP_DET Emp = new EMP_DET();
        private readonly dynamic returnNull = null;
        public ActionResult Updatepwd()
        {
            if (Session["admin1"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            return View();
        }
        [HttpGet]
        public ActionResult GetqDetails()
        {
            try
            {
                var result = q.GETQuestions();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }
            return returnNull;
        }
        [HttpPost]
        public ActionResult Addpwd(String pwd, /*long qustSno, String qust, String ansr,*/ String posted_by, String type, long? Instid, long? Usersno)
        {
            try
            {
                var check = Emp.Validatepwdbank(GetEncryptedData(pwd), long.Parse(Session["UserID"].ToString()));
                if (check == false)
                {
                    Emp.Password = GetEncryptedData(pwd);
                    //Emp.SNO =qustSno;
                    //Emp.Q_Name = qust;
                    //Emp.Q_Ans = ansr;
                    Emp.F_Login = "true";
                    Emp.AuditBy = posted_by;
                    Emp.Detail_Id = (long)Usersno;
                    Emp.UpdateQuestionEMP(Emp);
                    return Json(Usersno, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(check, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
            }
            return returnNull;
        }
        public static string GetEncryptedData(string value)
        {
            byte[] encData_byte = new byte[value.Length];
            encData_byte = System.Text.Encoding.UTF8.GetBytes(value);
            string encodedData = Convert.ToBase64String(encData_byte);
            return encodedData;
        }
    }
}