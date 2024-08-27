// Decompiled with JetBrains decompiler
// Type: FUNDING.Models.ApiModels.EventOfChoice
// Assembly: FUNDING, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5DE1C1E8-8D80-43BD-915D-F2502219BD58
// Assembly location: D:\ECardtest - 22082024\bin\FUNDING.dll

using System.ComponentModel.DataAnnotations;

 
namespace FUNDING.Models.ApiModels
{
  public class EventOfChoice
  {
    [Required(ErrorMessage = "Please provide event id")]
    [RegularExpression("^[1-9][0-9]*$", ErrorMessage = "Please enter valid event id")]
    public long? Event_Id { get; set; }

    [Required(ErrorMessage = "Please provide mobile number")]
    [RegularExpression("\\d+", ErrorMessage = "Please enter valid mobile number")]
    public string Mobile_Number { get; set; }
  }
}
