// Decompiled with JetBrains decompiler
// Type: FUNDING.Models.ApiModels.LoginCredentials
// Assembly: FUNDING, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5DE1C1E8-8D80-43BD-915D-F2502219BD58
// Assembly location: D:\ECardtest - 22082024\bin\FUNDING.dll

using System.ComponentModel.DataAnnotations;

 
namespace FUNDING.Models.ApiModels
{
  public class LoginCredentials
  {
    [Required(ErrorMessage = "Please enter mobile number")]
    [RegularExpression("\\d+", ErrorMessage = "Please enter valid mobile number")]
    public string Mobile_Number { get; set; }

    [Required(ErrorMessage = "Please enter password")]
    public string Password { get; set; }
  }
}
