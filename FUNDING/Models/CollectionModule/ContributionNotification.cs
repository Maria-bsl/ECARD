// Decompiled with JetBrains decompiler
// Type: FUNDING.Models.CollectionModule.ContributionNotification
// Assembly: FUNDING, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5DE1C1E8-8D80-43BD-915D-F2502219BD58
// Assembly location: D:\ECardtest - 22082024\bin\FUNDING.dll

using System;
using System.ComponentModel.DataAnnotations;

 
namespace FUNDING.Models.CollectionModule
{
  public class ContributionNotification
  {
    public long Notification_id { get; set; }

    [Required(ErrorMessage = "Please enter intimate date.")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    [Display(Name = "Intimate Date")]
    public DateTime Intimate_Date { get; set; }

    [Display(Name = "Intimate Date")]
    public string Formated_Intimate_Date { get; set; }

    [Required(ErrorMessage = "Please enter intimace days.")]
    [Display(Name = "Intimate Days")]
    [RegularExpression("^[1-9][0-9]*$", ErrorMessage = "Enter valid intimace days.")]
    public int Intimate_Days { get; set; }
  }
}
