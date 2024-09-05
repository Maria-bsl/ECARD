// Decompiled with JetBrains decompiler
// Type: FUNDING.Models.ApiModels.InviteeQRCode
// Assembly: FUNDING, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5DE1C1E8-8D80-43BD-915D-F2502219BD58
// Assembly location: D:\ECardtest - 22082024\bin\FUNDING.dll

using System.ComponentModel.DataAnnotations;

 
namespace FUNDING.Models.ApiModels
{
  public class InviteeQRCode
  {
    [Required(ErrorMessage = "Please provide event id")]
    [RegularExpression("^[1-9][0-9]*$", ErrorMessage = "Please enter valid event id")]
    public long? Event_Id { get; set; }

    [Required(ErrorMessage = "Please enter QR code")]
    public string QR_Code { get; set; }
  }
}
