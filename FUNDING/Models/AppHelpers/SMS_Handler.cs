// Decompiled with JetBrains decompiler
// Type: FUNDING.Models.AppHelpers.SMS_Handler
// Assembly: FUNDING, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5DE1C1E8-8D80-43BD-915D-F2502219BD58
// Assembly location: D:\ECardtest - 22082024\bin\FUNDING.dll

using FUNDING.BL.BusinessEntities.Masters;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

 
namespace FUNDING.Models.AppHelpers
{
  public static class SMS_Handler
  {
    private static readonly S_SMTP stp = new S_SMTP();
    private static readonly EMAIL em = new EMAIL();
    private static readonly Role rl = new Role();
    private static readonly Comments cm = new Comments();
    private static readonly Arights act = new Arights();
    private static readonly AuditLogs ad = new AuditLogs();
    private static readonly SMS_SETTNG smst = new SMS_SETTNG();
    private static readonly SMS_TEXT sms = new SMS_TEXT();

    public static void SendSMS(string SMS_Text, string mobileNumber)
    {
      try
      {
        SMS_SETTNG smtpConfig = SMS_Handler.smst.getSMTPConfig();
        SMS_Handler.stp.getSMTPText();
        string userName = smtpConfig.USER_Name;
        string str1 = SMS_Handler.DecodeFrom64(smtpConfig.Password);
        string fromAddress = smtpConfig.From_Address;
        string str2 = SMS_Handler.ReplaceFirstOccurrence(mobileNumber.Trim(), "0", "255");
        string str3 = HttpUtility.UrlEncode(SMS_Text);
        WebRequest webRequest = WebRequest.Create("http://api.infobip.com/api/v3/sendsms/plain?user=" + userName + "&password=" + str1 + "&sender=" + fromAddress + "&SMSText=" + str3 + "&GSM=" + str2);
        webRequest.Method = "POST";
        new StreamReader(webRequest.GetResponse().GetResponseStream(), Encoding.Default).ReadToEnd();
      }
      catch (Exception ex)
      {
        ex.Message.ToString();
      }
    }

    public static void SendLocalSMS(string SMS_Text, string mobileNumber)
    {
      if (!SMS_Handler.IsLocalMobileNumber(mobileNumber))
        return;
      try
      {
        SMS_SETTNG smtpConfig = SMS_Handler.smst.getSMTPConfig();
        SMS_Handler.stp.getSMTPText();
        string userName = smtpConfig.USER_Name;
        string str1 = SMS_Handler.DecodeFrom64(smtpConfig.Password);
        string fromAddress = smtpConfig.From_Address;
        string str2 = mobileNumber;
        string str3 = HttpUtility.UrlEncode(SMS_Text);
        WebRequest webRequest = WebRequest.Create("http://api.infobip.com/api/v3/sendsms/plain?user=" + userName + "&password=" + str1 + "&sender=" + fromAddress + "&SMSText=" + str3 + "&GSM=" + str2 + "&type=longSMS");
        webRequest.Method = "POST";
        new StreamReader(webRequest.GetResponse().GetResponseStream(), Encoding.Default).ReadToEnd();
      }
      catch (Exception ex)
      {
        ex.Message.ToString();
      }
    }

    private static bool IsLocalMobileNumber(string mobile_number)
    {
      return mobile_number.Replace("+", "").Substring(0, 3) == "255";
    }

    private static string DecodeFrom64(string password)
    {
      string empty = string.Empty;
      Decoder decoder = new UTF8Encoding().GetDecoder();
      byte[] bytes = Convert.FromBase64String(password);
      char[] chars = new char[decoder.GetCharCount(bytes, 0, bytes.Length)];
      decoder.GetChars(bytes, 0, bytes.Length, chars, 0);
      return new string(chars);
    }

    private static string ReplaceFirstOccurrence(string Source, string Find, string Replace)
    {
      int startIndex = Source.IndexOf(Find);
      return Source.Remove(startIndex, Find.Length).Insert(startIndex, Replace);
    }
  }
}
