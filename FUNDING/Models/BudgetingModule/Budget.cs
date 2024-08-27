// Decompiled with JetBrains decompiler
// Type: FUNDING.Models.BudgetingModule.Budget
// Assembly: FUNDING, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5DE1C1E8-8D80-43BD-915D-F2502219BD58
// Assembly location: D:\ECardtest - 22082024\bin\FUNDING.dll

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

 
namespace FUNDING.Models.BudgetingModule
{
  public class Budget
  {
    public long Record_Id { get; set; }

    [Remote("IsBudgetItemAvailable", "Budgeting", HttpMethod = "POST", ErrorMessage = "This item already exists.", AdditionalFields = "Item_clone")]
    [Required(ErrorMessage = "Please enter item")]
    public string Item { get; set; }

    public int Quantity { get; set; }

    [Display(Name = "Unit Price (TZS)")]
    public Decimal Unit_Price { get; set; }

    [Display(Name = "Unit Price (TZS)")]
    public Decimal Total_Price { get; set; }

    public string Remarks { get; set; }

    [RegularExpression("^[1-9]{1}(\\d{1,2})*(,\\d{3})*$", ErrorMessage = "Enter valid quantity")]
    [Required(ErrorMessage = "Please enter quantity")]
    [Display(Name = "Quantity")]
    [NotMapped]
    public string Quantity_Formatted { get; set; }

    [RegularExpression("^[1-9]{1}(\\d{1,2})*(,\\d{3})*$", ErrorMessage = "Enter valid unit price.")]
    [Required(ErrorMessage = "Please enter unit price")]
    [Display(Name = "Unit Price (TZS)")]
    [NotMapped]
    public string Unit_Price_Formatted { get; set; }

    [RegularExpression("^[1-9]{1}(\\d{1,2})*(,\\d{3})*$", ErrorMessage = "Enter valid total price.")]
    [Required(ErrorMessage = "Please enter total price")]
    [Display(Name = "Total Price (TZS)")]
    [NotMapped]
    public Decimal Total_Price_Formatted { get; set; }
  }
}
