// Decompiled with JetBrains decompiler
// Type: FUNDING.Controllers.ForgotController
// Assembly: FUNDING, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5DE1C1E8-8D80-43BD-915D-F2502219BD58
// Assembly location: D:\ECardtest - 22082024\bin\FUNDING.dll

using ECARD.DL.EDMX;
using FUNDING.BL.BusinessEntities.Masters;
using FUNDING.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.Mvc;

 
namespace FUNDING.Controllers
{
  public class ForgotController : Controller
  {
    private readonly dynamic returnNull = null;
    private EMP_DET emp = new EMP_DET();
    private S_SMTP stp = new S_SMTP();
    private EMAIL em = new EMAIL();
    private string drt;

    [Route("Admin/Forgot-Password")]
    public ActionResult Forgot() => (ActionResult) this.View();

    [Route("Forgot-Password")]
    public ActionResult CustomerForgot() => (ActionResult) this.View();

    [HttpPost]
    public ActionResult CustomerForgotPassword(string mobileNumber)
    {
      ForgotPasswordViewModel passwordViewModel = new ForgotPasswordViewModel();
      List<cust_users> custUsersList = passwordViewModel.VerifyUserWithMobileNumber(mobileNumber);
      if (custUsersList.Count<cust_users>() == 0)
        return (ActionResult) this.Json((object) new
        {
          data = 0
        });
      passwordViewModel.SendSmsWithCredential(custUsersList);
      passwordViewModel.DeactivateUser(custUsersList);
      return (ActionResult) this.Json((object) new
      {
        data = 1
      });
    }

    [HttpPost]
    public ActionResult Getemail(string Sno)
    {
      try
      {
        EMP_DET data = this.emp.GetmailsEmp(Sno);
        if (data == null)
          return (ActionResult) this.Json((object) 0, JsonRequestBehavior.AllowGet);
        this.SendActivationEmail(Sno, data.User_name, ForgotController.DecodeFrom64(data.Password), data.Q_Name);
        this.emp.Detail_Id = data.SNO;
        this.emp.Email_Address = Sno;
        this.emp.UpdateUsers(this.emp);
        return (ActionResult) this.Json((object) data, JsonRequestBehavior.AllowGet);
      }
      catch (Exception ex)
      {
        ex.ToString();
      }
      return (ActionResult) null;
    }

    private void ResetCustomerPassword(EMP_DET dep)
    {
      using (ECARDAPPEntities ecardappEntities = new ECARDAPPEntities())
      {
        emp_detail empDetail = ecardappEntities.emp_detail.Where<emp_detail>((Expression<Func<emp_detail, bool>>) (u => u.emp_detail_id == dep.Detail_Id && u.email_id == dep.Email_Address)).FirstOrDefault<emp_detail>();
        if (empDetail == null)
          return;
        empDetail.f_login = "false";
        empDetail.posted_by = dep.AuditBy;
        empDetail.posted_date = DateTime.Now;
        ecardappEntities.SaveChanges();
      }
    }

    private void SendActivationEmail(string email, string auname, string pwd, string uname)
    {
      try
      {
        Guid.NewGuid();
        SmtpClient smtpClient = new SmtpClient();
        using (MailMessage message = new MailMessage())
        {
          S_SMTP smtpText = this.stp.getSMTPText();
          EMAIL emaiLst = this.em.getEMAILst("2");
          message.To.Add(email);
          message.From = new MailAddress(smtpText.From_Address);
          message.Subject = emaiLst.Subject;
          this.drt = emaiLst.Subject;
          UriBuilder uriBuilder = new UriBuilder(this.Request.Url.AbsoluteUri)
          {
            Path = this.Url.Action("Login", "Login"),
            Query = (string) null
          };
          Uri uri = uriBuilder.Uri;
          string newValue = uriBuilder.ToString();
          string str = emaiLst.Email_Text.Replace("}+cName+{", uname).Replace("}+uname+{", auname).Replace("}+pwd+{", pwd).Replace("}+actLink+{", newValue).Replace("{", "").Replace("}", "");
          message.Body = str;
          message.IsBodyHtml = true;
          smtpClient.Host = smtpText.SMTP_Address;
          smtpClient.EnableSsl = Convert.ToBoolean(smtpText.SSL_Enable);
          NetworkCredential networkCredential = new NetworkCredential(smtpText.From_Address, ForgotController.DecodeFrom64(smtpText.SMTP_Password));
          smtpClient.UseDefaultCredentials = true;
          smtpClient.Credentials = (ICredentialsByHost) networkCredential;
          smtpClient.Port = (int) Convert.ToInt16(smtpText.SMTP_Port);
          smtpClient.Send(message);
        }
      }
      catch (Exception ex)
      {
      }
    }

    public static string DecodeFrom64(string password)
    {
      string empty = string.Empty;
      Decoder decoder = new UTF8Encoding().GetDecoder();
      byte[] bytes = Convert.FromBase64String(password);
      char[] chars = new char[decoder.GetCharCount(bytes, 0, bytes.Length)];
      decoder.GetChars(bytes, 0, bytes.Length, chars, 0);
      return new string(chars);
    }
  }
}
