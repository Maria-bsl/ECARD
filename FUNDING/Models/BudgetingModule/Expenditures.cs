// Decompiled with JetBrains decompiler
// Type: FUNDING.Models.BudgetingModule.Expenditures
// Assembly: FUNDING, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5DE1C1E8-8D80-43BD-915D-F2502219BD58
// Assembly location: D:\ECardtest - 22082024\bin\FUNDING.dll

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

 
namespace FUNDING.Models.BudgetingModule
{
  public class Expenditures
  {
    public long Record_Id { get; set; }

    public string Item { get; set; }

    [ForeignKey("Budget")]
    [Required(ErrorMessage = "Please select item")]
    [Display(Name = "Item")]
    public long Budget_Record_Id { get; set; }

    public int Quantity { get; set; }

    public Decimal Unit_Price { get; set; }

    public Decimal Total_Price { get; set; }

    public string Remarks { get; set; }

    public Budget BudgetInstance { get; set; }

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

    [Display(Name = "Full Name")]
    public string Service_Provider { get; set; }

    [Display(Name = "Mobile Number")]
    [NotMapped]
    public string SPC_Mobile_Number { get; set; }

    [Display(Name = "Mobile Number")]
    [NotMapped]
    public string SPU_Mobile_Number { get; set; }

    public string Int_Mobile_number { get; set; }
  }
}
