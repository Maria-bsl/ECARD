// Decompiled with JetBrains decompiler
// Type: FUNDING.Models.SMS
// Assembly: FUNDING, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5DE1C1E8-8D80-43BD-915D-F2502219BD58
// Assembly location: D:\ECardtest - 22082024\bin\FUNDING.dll

using System.Net;

 
namespace FUNDING.Models
{
  public class SMS
  {
    public static void SendCredentials(
      string username,
      string password,
      string senderb,
      string mess,
      string gsm)
    {
      WebRequest webRequest = WebRequest.Create("http://api.infobip.com/api/v3/sendsms/plain?user=" + username + "&password=" + password + "&sender=" + senderb + "&SMSText=" + mess + "&GSM=" + gsm);
      webRequest.Method = "POST";
      webRequest.GetResponse();
    }

    public static void SendQRCodeIdentity(
      string username,
      string password,
      string senderb,
      string mess,
      string gsm)
    {
      WebRequest webRequest = WebRequest.Create("http://api.infobip.com/api/v3/sendsms/plain?user=" + username + "&password=" + password + "&sender=" + senderb + "&SMSText=" + mess + "&GSM=" + gsm);
      webRequest.Method = "POST";
      webRequest.GetResponse();
    }
  }
}
