// Decompiled with JetBrains decompiler
// Type: FUNDING.Models.ForgotPasswordViewModel
// Assembly: FUNDING, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5DE1C1E8-8D80-43BD-915D-F2502219BD58
// Assembly location: D:\ECardtest - 22082024\bin\FUNDING.dll

using ECARD.DL.EDMX;
using FUNDING.BL.BusinessEntities.Masters;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Web;

 
namespace FUNDING.Models
{
  public class ForgotPasswordViewModel
  {
    public string MobileNumber { get; set; }

    public List<cust_users> VerifyUserWithMobileNumber(string mobileNumber)
    {
      using (ECARDAPPEntities ecardappEntities = new ECARDAPPEntities())
      {
        DateTime dateOfPasswordReset = DateTime.UtcNow.Date;
        List<cust_users> list = ecardappEntities.cust_users.Where<cust_users>((Expression<Func<cust_users, bool>>) (c => c.mobile_no == mobileNumber && c.expiry_date >= (DateTime?) dateOfPasswordReset)).ToList<cust_users>();
        return list.Count<cust_users>() != 0 ? list : (List<cust_users>) null;
      }
    }

    public void SendSmsWithCredential(List<cust_users> userList)
    {
      if (userList.Count<cust_users>() == 0)
        return;
      cust_users custUsers = userList.FirstOrDefault<cust_users>();
      string password = custUsers.password;
      this.SendSMSAction(custUsers.mobile_no, this.FormatMessageBody(this.DecodeFrom64(password)));
    }

    public void DeactivateUser(List<cust_users> userList)
    {
      foreach (cust_users user in userList)
      {
        using (ECARDAPPEntities ecardappEntities = new ECARDAPPEntities())
        {
          user.f_login = "false";
          ecardappEntities.Entry<cust_users>(user).State = EntityState.Modified;
          ecardappEntities.SaveChanges();
        }
      }
    }

    private string FormatMessageBody(string password)
    {
      return string.Format("Dear user, you password is {0} visit B-envit website to reset your password.", (object) password);
    }

    private void SendSMSAction(string visitorMobileNumber, string SmsBody)
    {
      S_SMTP sSmtp = new S_SMTP();
      EMAIL email = new EMAIL();
      Role role = new Role();
      Comments comments = new Comments();
      Arights arights = new Arights();
      AuditLogs auditLogs = new AuditLogs();
      SMS_SETTNG smsSettng = new SMS_SETTNG();
      SMS_TEXT smsText = new SMS_TEXT();
      try
      {
        SMS_SETTNG smtpConfig = smsSettng.getSMTPConfig();
        sSmtp.getSMTPText();
        string userName = smtpConfig.USER_Name;
        string str1 = this.DecodeFrom64(smtpConfig.Password);
        string fromAddress = smtpConfig.From_Address;
        string str2 = this.ReplaceFirstOccurrence(visitorMobileNumber.Trim(), "0", "255");
        string str3 = HttpUtility.UrlEncode(SmsBody);
        WebRequest webRequest = WebRequest.Create("http://api.infobip.com/api/v3/sendsms/plain?user=" + userName + "&password=" + str1 + "&sender=" + fromAddress + "&SMSText=" + str3 + "&GSM=" + str2 + "&type=longSMS");
        webRequest.Method = "POST";
        new StreamReader(webRequest.GetResponse().GetResponseStream(), Encoding.Default).ReadToEnd();
      }
      catch (Exception ex)
      {
        ex.Message.ToString();
      }
    }

    private string DecodeFrom64(string password)
    {
      string empty = string.Empty;
      Decoder decoder = new UTF8Encoding().GetDecoder();
      byte[] bytes = Convert.FromBase64String(password);
      char[] chars = new char[decoder.GetCharCount(bytes, 0, bytes.Length)];
      decoder.GetChars(bytes, 0, bytes.Length, chars, 0);
      return new string(chars);
    }

    private string ReplaceFirstOccurrence(string Source, string Find, string Replace)
    {
      int startIndex = Source.IndexOf(Find);
      return Source.Remove(startIndex, Find.Length).Insert(startIndex, Replace);
    }
  }
}
