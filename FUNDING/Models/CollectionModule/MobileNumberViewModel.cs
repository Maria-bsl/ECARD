// Decompiled with JetBrains decompiler
// Type: FUNDING.Models.CollectionModule.MobileNumberViewModel
// Assembly: FUNDING, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5DE1C1E8-8D80-43BD-915D-F2502219BD58
// Assembly location: D:\ECardtest - 22082024\bin\FUNDING.dll

using System.ComponentModel.DataAnnotations;

 
namespace FUNDING.Models.CollectionModule
{
  public class MobileNumberViewModel
  {
    public string InternationalMobileNumber_clone { get; set; }

    [Required(ErrorMessage = "Please enter a mobile number")]
    [RegularExpression("^\\d+$", ErrorMessage = "Please enter a valid mobile number.")]
    public string InternationalMobileNumber { get; set; }
  }
}
