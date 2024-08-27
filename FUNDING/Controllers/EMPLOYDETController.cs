using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Configuration;
using System.Net.Mail;
using System.Net;
using System.Text;
using FUNDING.BL.BusinessEntities.Masters;




namespace FUNDING.Controllers
{
    public class EMPLOYDETController : BaseController
    {
        private readonly dynamic returnNull = null;
        EMP_DET dt = new EMP_DET();
        DESIGNATION dg = new DESIGNATION();
        S_SMTP stp = new S_SMTP();
        EMAIL em = new EMAIL();
        Role rl = new Role();
        Comments cm = new Comments();
        Arights act = new Arights();
        AuditLogs ad = new AuditLogs();


        SMS_SETTNG smst = new SMS_SETTNG();
        SMS_TEXT sms = new SMS_TEXT();

        String pwd = "", drt;
        String[] list = new String[15] { "emp_detail_id", "emp_id_no","fullname","first_name", "middle_name", "last_name", "desg_id","email_id", "mobile_no",
            "created_date","expiry_date", "app_status","posted_by", "posted_date","username"};

        [Route("Institution-Users")]
        public ActionResult EMPLOYDET()
        {
            if (Session["admin1"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                //var count = act.checkact("Users");
                var count = act.checkact("Users", long.Parse(Session["Desg"].ToString()), Session["role"].ToString(), Session["Rcode"].ToString());
                if (count == 0)
                {
                    return RedirectToAction("Setup", "Setup");
                }
            }
            return View();
        }
        [HttpPost]
        public ActionResult GetEMPDetailsuser()
        {
            try
            {
                long roldeid = (long)@Session["desg"];
                var result = dt.GetEMPuser(roldeid);
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
        public ActionResult GetEMPDetails()
        {
            try
            {
                string roldeid = Session["role"].ToString();
                // long   teamid = (long)Session["teamid"];
                var result = dt.GetEMPNew(roldeid);
                if (result != null)
                {
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var d = 0;
                    return Json(d, JsonRequestBehavior.AllowGet);
                }

                //long vl_teamid = 0; ;
                //if (Session["teamid"] == null)
                //{
                //    vl_teamid = 0;
                //}
                //else
                //{
                //    vl_teamid = (long)Session["teamid"];
                //}
                ////if (Session["teamid"] != null)
                //if (vl_teamid != 0 )
                //{

                //    long teamid = (long)Session["teamid"];
                //    var result = dt.GetEMPTeam(teamid );
                //    if (result != null)
                //    {
                //        return Json(result, JsonRequestBehavior.AllowGet);
                //    }
                //    else
                //    {
                //        var d = 0;
                //        return Json(d, JsonRequestBehavior.AllowGet);
                //    }
                //}
                //else
                //{
                //    var result = dt.GetEMP();
                //    if (result != null)
                //    {
                //        return Json(result, JsonRequestBehavior.AllowGet);
                //    }
                //    else
                //    {
                //        var d = 0;
                //        return Json(d, JsonRequestBehavior.AllowGet);
                //    }
                //}
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }
            return returnNull;
        }
        [HttpGet]
        public ActionResult GetdesgDetails()
        {
            try
            {
                var result = dg.GetDesignation();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }
            return returnNull;
        }


        [HttpGet]
        public ActionResult GetRoleDetails_unq()
        {
            try
            {
                long roldeid = (long)@Session["desg"];
                var result = rl.GetRoleunq(roldeid);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }
            return returnNull;
        }
        [HttpGet]
        public ActionResult GetRoleDetails()
        {
            try
            {
                var result = rl.GetRole();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }
            return returnNull;
        }
        [HttpPost]
        public ActionResult GetEmpindivi(long Sno)
        {
            try
            {
                var result = dt.EditEMP(Sno);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }
            return returnNull;
        }
        [HttpPost]
        public ActionResult AddEmp(string empid, string fullname, String fname, String mname, String lname, long desg, String email, String mobile, String user, String gender, long team, long sno, bool dummy)
        {
            try
            {
                dt.Emp_Id_No = empid;
                dt.First_Name = fname;
                dt.Middle_name = mname;
                dt.Last_name = lname;
                dt.Full_Name = fname + " " + mname + " " + lname;
                dt.Desg_Id = desg;
                dt.Email_Address = email;
                dt.Mobile_No = mobile;
                dt.User_name = user;
                dt.Emp_Status = gender;
                //dt.AuditBy = Session["UserID"].ToString(); 
                dt.Created_Date = System.DateTime.Now;
                dt.Expiry_Date = System.DateTime.Now.AddMonths(3);
                dt.Detail_Id = sno;
                // dt.team_det_sno =  team;

                long ssno = 0;
                if (sno == 0)
                {
                    var result = dt.Validateuser(empid.ToLower());

                    if (result == true)
                    {
                        return Json(result, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        pwd = CreateRandomPassword(8);
                        dt.Password = GetEncryptedData(pwd);
                        SendActivationEmail(email, dt.Full_Name, pwd, user);
                        // Sensms(mobile, dt.Full_Name, pwd, user);
                        ssno = dt.AddEMP(dt);
                        var des = dg.Editdesignation(desg);
                        if (ssno > 0)
                        {
                            String[] list1 = new String[15] { ssno.ToString(), dt.Emp_Id_No,dt.Full_Name,dt.First_Name, dt.Middle_name,  dt.Last_name,dt.Desg_Id.ToString(),dt.Email_Address,
                                dt.Mobile_No,dt.Created_Date.ToString(),dt.Expiry_Date.ToString(), dt.Emp_Status,Session["UserID"].ToString(), DateTime.Now.ToString(),dt.User_name };
                            for (int i = 0; i < list.Count(); i++)
                            {
                                ad.Audit_Type = "Insert";
                                ad.Columnsname = list[i];
                                ad.Table_Name = "emp_detail";
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
                        dt.Detail_Id = sno;
                        var des = dg.Editdesignation(desg);
                        var dd = dt.EditEMP(sno);
                        if (dd != null)
                        {
                            String[] list2 = new String[16] { dd.Detail_Id.ToString(), dd.Emp_Id_No,dd.Full_Name,dd.First_Name, dd.Middle_name,  dd.Last_name,dd.Desg_Id.ToString(),dd.Email_Address,
                                dd.Mobile_No,dd.Created_Date.ToString(),dd.Expiry_Date.ToString(), dd.Emp_Status,dd.AuditBy,dd.Audit_Date.ToString(),dd.User_name , dd.team_det_sno .ToString()  };
                            String[] list1 = new String[16] { ssno.ToString(), dt.Emp_Id_No,dt.Full_Name,dt.First_Name, dt.Middle_name,  dt.Last_name,dd.Desg_Id.ToString(),dt.Email_Address,
                                dt.Mobile_No,dt.Created_Date.ToString(),dt.Expiry_Date.ToString(), dt.Emp_Status,Session["UserID"].ToString(), DateTime.Now.ToString(),dt.User_name , dd.team_det_sno .ToString()};
                            for (int i = 0; i < list.Count(); i++)
                            {
                                ad.Audit_Type = "Update";
                                ad.Columnsname = list[i];
                                ad.Table_Name = "emp_detail";
                                ad.Oldvalues = list2[i];
                                ad.Newvalues = list1[i];
                                ad.AuditBy = Session["UserID"].ToString();
                                ad.Audit_Date = DateTime.Now;
                                ad.Audit_Time = DateTime.Now;
                                ad.AddAudit(ad);
                            }
                            dt.UpdateEMP(dt);
                        }
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
        private static string CreateRandomPassword(int length)
        {
            string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%&";
            Random random = new Random();
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }
            return new string(chars);
        }

        private void Sensms(String mobile, String fullname, String pwd, String uname)
        {
            try
            {
                Guid activationCode = Guid.NewGuid();
                SmtpClient smtp = new SmtpClient();
                using (MailMessage mm = new MailMessage())
                {
                    var sm = smst.getSMTPConfig();
                    var data = sms.getSMSLst(1); // on User Registration
                    var m = stp.getSMTPText();
                    using (MailMessage mm1 = new MailMessage())
                    {
                        string username = sm.From_Address;
                        string password = DecodeFrom64(sm.Password);
                        string senderb = sm.USER_Name;
                        //  string mess = data.SMS_Text;
                        string gsm = mobile;
                        String body = data.SMS_Text.Replace("}+cName+{", fullname).Replace("}+uname+{", uname).Replace("}+pwd+{", pwd).Replace("{", "").Replace("}", "");


                        string url = "http://api.infobip.com/api/v3/sendsms/plain?user=" + username + "&password=" + password + "&sender=" + senderb + "&SMSText=" + body + "&GSM=" + gsm;
                        WebRequest myReq = WebRequest.Create(url);
                        myReq.Method = "POST";
                        WebResponse wr = myReq.GetResponse();
                    }
                }
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
            }

        }
        private void SendActivationEmail(String email, String fullname, String pwd, String uname)
        {
            try
            {
                Guid activationCode = Guid.NewGuid();
                SmtpClient smtp = new SmtpClient();
                using (MailMessage mm = new MailMessage())
                {
                    var m = stp.getSMTPText();
                    var data = em.getEMAILst("2");
                    mm.To.Add(email);
                    mm.From = new MailAddress(m.From_Address);
                    mm.Subject = data.Subject;
                    drt = data.Subject;
                    var urlBuilder =
                   new System.UriBuilder(Request.Url.AbsoluteUri)
                   {
                       Path = Url.Action("Login", "Login"),
                       Query = null,
                   };
                    Uri uri = urlBuilder.Uri;
                    string url = urlBuilder.ToString();
                    String body = data.Email_Text.Replace("}+cName+{", fullname).Replace("}+uname+{", uname).Replace("}+pwd+{", pwd).Replace("}+actLink+{", url).Replace("{", "").Replace("}", "");
                    mm.Body = body;
                    mm.IsBodyHtml = true;
                    smtp.Host = m.SMTP_Address;
                    smtp.EnableSsl = Convert.ToBoolean(m.SSL_Enable);
                    NetworkCredential NetworkCred = new NetworkCredential(m.From_Address, DecodeFrom64(m.SMTP_Password));
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = Convert.ToInt16(m.SMTP_Port);
                    smtp.Send(mm);
                }
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
                ///*Utilites.logfile*/("Bank", drt, Ex.ToString());
            }
        }
        public static string GetEncryptedData(string value)
        {
            byte[] encData_byte = new byte[value.Length];
            encData_byte = System.Text.Encoding.UTF8.GetBytes(value);
            string encodedData = Convert.ToBase64String(encData_byte);
            return encodedData;
        }
        public static string DecodeFrom64(string password)
        {
            string decryptpwd = string.Empty;
            UTF8Encoding encodepwd = new UTF8Encoding();
            Decoder Decode = encodepwd.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(password);
            int charCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            decryptpwd = new String(decoded_char);
            return decryptpwd;
        }

        [HttpPost]
        public ActionResult DeleteEmp(string id, long sno)
        {
            try
            {
                if (sno > 0)
                {
                    var check = dt.Validateuser(id);
                    if (check == true)
                    {
                        return Json(check, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        var dd = dt.EditEMP(sno);
                        if (dd != null)
                        {
                            String[] list2 = new String[15] { dd.Detail_Id.ToString(), dd.Emp_Id_No,dd.Full_Name,dd.First_Name, dd.Middle_name,  dd.Last_name,dd.Desg_name.ToString(),dd.Email_Address,
                                dd.Mobile_No,dd.Created_Date.ToString(),dd.Expiry_Date.ToString(), dd.Emp_Status,dd.AuditBy,dd.Audit_Date.ToString(),dd.User_name };
                            for (int i = 0; i < list.Count(); i++)
                            {
                                ad.Audit_Type = "Delete";
                                ad.Columnsname = list[i];
                                ad.Table_Name = "emp_detail";
                                ad.Oldvalues = list2[i];
                                ad.AuditBy = Session["UserID"].ToString();
                                ad.Audit_Date = DateTime.Now;
                                ad.Audit_Time = DateTime.Now;
                                ad.AddAudit(ad);
                            }
                        }
                        dt.DeleteStatus(dd);
                        //dt.DeleteEMP(sno);
                    }
                    return Json(sno, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }
            return returnNull;
        }

        [HttpPost]
        public ActionResult Checkdupliacteempid(string empid)
        {
            try
            {
                var check = dt.Validateuser(empid);
                if (check == true)
                {
                    return Json(check, JsonRequestBehavior.AllowGet);
                }
                return Json(check, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }
            return returnNull;
        }
        [HttpPost]
        public ActionResult CheckdupliacteEmail(String email)
        {
            try
            {
                var check = dt.Validateemail(email);
                if (check == true)
                {
                    return Json(check, JsonRequestBehavior.AllowGet);
                }
                return Json(check, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }
            return returnNull;
        }


        [HttpPost]
        public ActionResult Checkdupliacte(String name)
        {
            try
            {
                var check = dt.Validateusername(name);
                if (check == true)
                {
                    return Json(check, JsonRequestBehavior.AllowGet);
                }
                return Json(check, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }
            return returnNull;
        }


        public ActionResult Checkdupliactemobile(string mob)
        {
            try
            {
                var check = dt.Validateemonbile(mob);
                if (check == true)
                {
                    return Json(check, JsonRequestBehavior.AllowGet);
                }
                return Json(check, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }
            return returnNull;
        }
        public ActionResult Getcommentusers(long sno, string name)
        {
            try
            {
                var result = cm.GetComment(name, sno);
                if (result != null)
                {
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
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