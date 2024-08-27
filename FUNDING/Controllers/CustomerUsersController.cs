// Decompiled with JetBrains decompiler
// Type: FUNDING.Controllers.CustomerUsersController
// Assembly: FUNDING, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5DE1C1E8-8D80-43BD-915D-F2502219BD58
// Assembly location: D:\ECardtest - 22082024\bin\FUNDING.dll

using ECARD.DL.EDMX;
using FUNDING.BL.BusinessEntities.Masters;
using FUNDING.Models.AppHelpers;
using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

 
namespace FUNDING.Controllers
{
  public class CustomerUsersController : Controller
  {
    private readonly ECARDAPPEntities _dbContext = new ECARDAPPEntities();
    private readonly CustomerUsers _users = new CustomerUsers();

        [Route("mail")]
        public ActionResult SendMail()
        {
            var subject = "Hello";
            var receiver = "shilogilep@gmail.com";
            //var receiver_name = "Patrick Shilogile";
            var message = "I am just testing";

            var smtp_settings = _dbContext.smtp_settings.OrderByDescending(s => s.posted_date).FirstOrDefault();

            try
            {
                var senderEmail = new MailAddress("tests.trickie.ai@gmail.com", "B-ENVIT");
                //var senderEmail = new MailAddress(smtp_settings.from_address, "Sender");
                var receiverEmail = new MailAddress(receiver, "Patrick Shilogile");
                // var password = "Your Email Password here";
                var password = "dv5K5MM8nm6wcWh";
                //var password = EMPLOYDETController.DecodeFrom64(smtp_settings.smtp_password);
                var sub = subject;
                var body = message;
                var smtp = new SmtpClient
                {
                    //Host = "smtp.gmail.com",
                    Host = smtp_settings.smtp_address,
                    //Port = 587,
                    Port = Convert.ToInt32(smtp_settings.smtp_port),
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(senderEmail.Address, password)
                };
                using (var mess = new MailMessage(senderEmail, receiverEmail)
                {
                    Subject = subject,
                    Body = body,
                })
                {
                    var event_id = 6;

                    var visitorEventDetails = _dbContext.visitor_details
                       .Where(ev => ev.event_det_sno == event_id).FirstOrDefault();

                    string absolutePath = GetCardDirectoryAbsolutePath();

                    //var index = 1;
                    //foreach (var item in visitorEventDetails)
                    //{
                    //    if (index == 5)
                    //    {
                    for (int i = 0; i <= 9; i++)
                    {
                        mess.Attachments.Add(new Attachment(string.Format($"{absolutePath}\\{visitorEventDetails.visitor_det_sno}_{EscapeCharacterForFileName(visitorEventDetails.visitor_name)} - {i}.png")));
                    }
                    //break;
                    //    }

                    //    index++;
                    //}

                    smtp.Send(mess);
                }
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Some Error";
                return Json(new { error_message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            //return Json("Failed", JsonRequestBehavior.AllowGet);
        }

        private static string GetCardDirectoryAbsolutePath()
    {
      return HostingEnvironment.MapPath(DirectoryHelpers.Generated_Cards_DirVirtualDirectory);
    }

    private static string EscapeCharacterForFileName(string inviteeName)
    {
      return inviteeName.Trim().Replace("/", "").Replace("\\", "").Replace(":", "-").Replace("?", "").Replace("\"", "'").Replace("<", "-").Replace(">", "-");
    }

        private void SendActivationEmail(String email, String fullname, String pwd, String uname)
        {
            try
            {
                Guid activationCode = Guid.NewGuid();
                SmtpClient smtp = new SmtpClient();
                S_SMTP stp = new S_SMTP();
                EMAIL em = new EMAIL();

                using (MailMessage mm = new MailMessage())
                {
                    var m = stp.getSMTPText();
                    var data = em.getEMAILst("2");
                    mm.To.Add(email);
                    mm.From = new MailAddress(m.From_Address);
                    mm.Subject = data.Subject;
                    var drt = data.Subject;

                    var urlBuilder = new System.UriBuilder(Request.Url.AbsoluteUri)
                    {
                        Path = Url.Action("Login", "Login"),
                        Query = null
                    };

                    Uri uri = urlBuilder.Uri;
                    string url = urlBuilder.ToString();
                    string body = data.Email_Text.Replace("}+cName+{", fullname).Replace("}+uname+{", uname)
                        .Replace("}+pwd+{", pwd).Replace("}+actLink+{", url).Replace("{", "").Replace("}", "");

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


        // GET: CustomerUsers
        [Route("Event-Users")]
        public ActionResult Index()
        {
            if (Session["admin1"] as string == CustomerUsers.NormalUserType)
            {
                return View("Index_readonly");
            }
            else if (Session["admin1"] as string == CustomerUsers.EventAdminUserType)
            {
                List<SelectListItem> grant_roles_as_super_admin = new List<SelectListItem>() {
                    new SelectListItem {Text = "Event Admin", Value = "Event Admin"},
                    new SelectListItem {Text = "Normal User", Value = "Normal User"}
                };

                ViewBag.user_type = grant_roles_as_super_admin.Where(v => v.Value == "Normal User").ToList();

                return View();
            }
            else
            {
                return RedirectToAction("CustomerLogin", "Login");
            }

        }

    [HttpPost]
    public ActionResult IndexDataTable()
    {
      long eventAdminID = Convert.ToInt64(this.Session["EventAdminID"]);
      string eventID = this.Session["EventID"] as string;
      var cust_users = this._dbContext.cust_users.Where<cust_users>((Expression<Func<cust_users, bool>>) (c => c.cust_reg_sno == (long?) eventAdminID && c.event_det_sno.Contains(eventID))).Select(c => new
      {
        cust_users_sno = c.cust_users_sno,
        user_fullname = c.user_fullname,
        user_type = c.user_type,
        email_address = c.email_address,
        mobile_no = c.mobile_no
      }).ToList();
      int index = 1;
      return (ActionResult) this.Json((object) new
      {
        data = cust_users.Select(c => new
        {
          id = index++,
          cust_users_sno = c.cust_users_sno,
          user_fullname = c.user_fullname,
          user_type = c.user_type,
          email_address = c.email_address,
          mobile_no = c.mobile_no
        }),
        eventAdminID = eventAdminID
      });
    }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IsMobileNumberAvailable(string __RequestVerificationToken, string mobile_no, string mobile_no_clone)
        {
            if (mobile_no_clone != null)
            {
                if (mobile_no == mobile_no_clone)
                {
                    //Updating to the same name. Available for use.
                    return Json(true);
                }
            }

            var eventAdminID = GetCustomerAdminID();
            var event_id = Session["EventID"] as string;

            var isMobileAvailable = (from u in _dbContext.cust_users
                                     where u.cust_reg_sno == eventAdminID && u.mobile_no.ToUpper() == mobile_no.ToUpper()
                                     && u.event_det_sno.Contains(event_id)
                                     select new { mobile_no }).FirstOrDefault();

            bool availabilityStatus;
            if (isMobileAvailable != null)
            {
                //Username exists. Not available for use.
                availabilityStatus = false;
            }
            else
            {
                //Username does not exists. Available for use.
                availabilityStatus = true;
            }

            return Json(availabilityStatus);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AjaxHelperCreateForm([Bind(Include = "cust_users_sno,user_fullname,user_type,email_address,mobile_no")] CustomerUsers users)
        {
            if (ModelState.IsValid)
            {
                var customerUser = new cust_users
                {
                    cust_reg_sno = GetCustomerAdminID(),
                    user_fullname = users.user_fullname,
                    //username = users.username,
                    password = GetGeneratedPassword(),
                    user_type = users.user_type,
                    created_date = DateTime.Now,
                    expiry_date = GetExpireDate(),
                    mobile_no = users.mobile_no,
                    email_address = users.email_address,
                    event_det_sno = Session["EventID"] as string,
                    posted_date = DateTime.Now,
                    posted_by = GetPostedBy()
                };

                _dbContext.cust_users.Add(customerUser);
                _dbContext.SaveChanges();

                SendWelcomeSmsToNewUser(customerUser);

                return Json(new { createStatus = true, response = "Record successful created." });
            }

            List<SelectListItem> roles = new List<SelectListItem>() {
                new SelectListItem {
                    Text = "Event Admin", Value = "Event Admin"
                },
                new SelectListItem {
                    Text = "Normal User", Value = "Normal User"
                },
            };

            ViewBag.user_type = roles;

            //ViewBag.cust_reg_sno = new SelectList(_dbContext.customer_registration, "cust_reg_sno", "cust_name", users.cust_reg_sno);

            return PartialView("_AjaxHelperCreateForm", users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AjaxHelperUpdateForm([Bind(Include = "cust_users_sno,user_fullname,user_type,email_address,mobile_no")] CustomerUsers users)
        {

            if (ModelState.IsValid)
            {
                var usersDb = _dbContext.cust_users.Find(users.cust_users_sno);

                if (usersDb == null)
                {
                    return Json(new { updateStatus = false, response = "Failed! Item does not exist." });
                }

                usersDb.user_fullname = users.user_fullname;
                usersDb.user_type = users.user_type;
                usersDb.created_date = DateTime.Now;
                usersDb.expiry_date = GetExpireDate();
                usersDb.mobile_no = users.mobile_no;
                usersDb.email_address = users.email_address;
                usersDb.posted_date = DateTime.Now;
                usersDb.posted_by = GetPostedBy();

                _dbContext.Entry(usersDb).State = EntityState.Modified;
                _dbContext.Entry(usersDb).Property("cust_reg_sno").IsModified = false;
                //_dbContext.Entry(usersDb).Property("user_type").IsModified = false;
                //_dbContext.Entry(usersDb).Property("username").IsModified = false;
                _dbContext.Entry(usersDb).Property("password").IsModified = false;
                _dbContext.Entry(usersDb).Property("created_date").IsModified = false;
                _dbContext.Entry(usersDb).Property("expiry_date").IsModified = false;
                _dbContext.Entry(usersDb).Property("event_det_sno").IsModified = false;
                _dbContext.SaveChanges();

                return Json(new { updateStatus = true, response = "Record successful updated." });
            }

            List<SelectListItem> roles = new List<SelectListItem>() {
                new SelectListItem {
                    Text = "Event Admin", Value = "Event Admin"
                },
                new SelectListItem {
                    Text = "Normal User", Value = "Normal User"
                },
            };

            ViewBag.user_type = roles;

            return PartialView("_AjaxHelperUpdateForm", users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AjaxHelperDeleteForm(long? id)
        {
            if (id == null)
            {
                return Json(new { deleteStatus = false, response = "Failed! ID not supplied" });
            }
            cust_users users = _dbContext.cust_users.Find(id);

            if (users == null)
            {
                return Json(new { deleteStatus = false, response = "Failed! Item does not exist" });
            }

            try
            {
                _dbContext.cust_users.Remove(users);
                _dbContext.SaveChanges();
            }
            catch (Exception)
            {
                return Json(new { deleteStatus = false, response = "Record cannot be delete, it is being used by other records." });
            }

            return Json(new { deleteStatus = true, response = "Record successful deleted." });
        }


        private long GetCustomerAdminID()
        {
            // code for the action here
            return Convert.ToInt64(Session["EventAdminID"]);
        }

        private string GetPostedBy()
        {
            // code for the action here
            return Session["UserID"].ToString();
        }

        private DateTime? GetExpireDate()
        {
            // code for the action here
            return DateTime.Now.AddMonths(3);
        }

        private string GetGeneratedPassword()
        {
            return GetEncryptedData(CreateRandomPassword(8));
        }


        private string CreateRandomPassword(int length)
        {
            string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            //string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%&";
            Random random = new Random();
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }
            return new string(chars);
        }

        public static string GetEncryptedData(string value)
        {
            byte[] encData_byte = new byte[value.Length];
            encData_byte = System.Text.Encoding.UTF8.GetBytes(value);
            string encodedData = Convert.ToBase64String(encData_byte);
            return encodedData;
        }


        #region SMS methods

        private string DecodeFrom64(string password)
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

        public void SendWelcomeSmsToNewUser(cust_users customerUser)
        {
            if (customerUser != null)
            {
                var password = customerUser.password;
                var mobileNumber = customerUser.mobile_no;

                var decryptedPassword = DecodeFrom64(password);
                var formattedMessageBody = FormatMessageBody(decryptedPassword);

                SendSMSAction(mobileNumber, formattedMessageBody);

            }

        }

        private string FormatMessageBody(string password)
        {
            return string.Format("Dear user, you have been registered to E-NVIT system. Your password is {0}", password);
        }

        private void SendSMSAction(string visitorMobileNumber, string SmsBody)
        {
            #region Class Instances

            S_SMTP stp = new S_SMTP();
            EMAIL em = new EMAIL();
            Role rl = new Role();
            Comments cm = new Comments();
            Arights act = new Arights();
            AuditLogs ad = new AuditLogs();
            SMS_SETTNG smst = new SMS_SETTNG();
            SMS_TEXT sms = new SMS_TEXT();

            #endregion;

            try
            {
                var sm = smst.getSMTPConfig();
                //var data = sms.getSMSLst(1); // on User Registration
                var m = stp.getSMTPText();
                string username = sm.USER_Name;
                string password = DecodeFrom64(sm.Password);
                string senderb = sm.From_Address;
                //  string mess = data.SMS_Text;
                string gsm = ReplaceFirstOccurrence(visitorMobileNumber.Trim(), "0", "255");
                string body = HttpUtility.UrlEncode(SmsBody);

                string url = "http://api.infobip.com/api/v3/sendsms/plain?user=" + username + "&password=" + password + "&sender=" + senderb + "&SMSText=" + body + "&GSM=" + gsm;
                WebRequest myReq = WebRequest.Create(url);
                myReq.Method = "POST";
                WebResponse wr = myReq.GetResponse();
                StreamReader sr = new StreamReader(wr.GetResponseStream(), Encoding.UTF8);
                string Res = sr.ReadToEnd();
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
            }

        }

        private string ReplaceFirstOccurrence(string Source, string Find, string Replace)
        {
            int Place = Source.IndexOf(Find);
            string result = Source.Remove(Place, Find.Length).Insert(Place, Replace);
            return result;
        }

        #endregion;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}