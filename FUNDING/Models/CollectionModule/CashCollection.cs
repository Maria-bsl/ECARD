// Decompiled with JetBrains decompiler
// Type: FUNDING.Models.CollectionModule.CashCollection
// Assembly: FUNDING, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5DE1C1E8-8D80-43BD-915D-F2502219BD58
// Assembly location: D:\ECardtest - 22082024\bin\FUNDING.dll

using System.ComponentModel.DataAnnotations;

 
namespace FUNDING.Models.CollectionModule
{
  public class CashCollection
  {
    [Required(ErrorMessage = "Please enter contributor name.")]
    public long Contribution_id { get; set; }

    [Required(ErrorMessage = "Please enter pledged amount.")]
    [Display(Name = "Pledged Amount (TZS)")]
    [RegularExpression("^[1-9]{1}(\\d{1,2})*(,\\d{3})*$", ErrorMessage = "Enter valid pledged amount.")]
    public string Contribution_amount { get; set; }

    public string Remarks { get; set; }
  }
}
