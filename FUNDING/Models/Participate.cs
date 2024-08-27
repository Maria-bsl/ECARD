// Decompiled with JetBrains decompiler
// Type: FUNDING.Models.Participate
// Assembly: FUNDING, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5DE1C1E8-8D80-43BD-915D-F2502219BD58
// Assembly location: D:\ECardtest - 22082024\bin\FUNDING.dll

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

 
namespace FUNDING.Models
{
  public class Participate
  {
    public long pat_sno { get; set; }

    [Required(ErrorMessage = "Please enter full name.")]
    [Display(Name = "Full Name :")]
    public string fullname { get; set; }

    [Required(ErrorMessage = "Pleas enter Mobile number.")]
    [Display(Name = "Mobile Number :")]
    public string phone_number { get; set; }

    [Display(Name = "Gender :")]
    public string gender { get; set; }

    [Required]
    [DataType(DataType.Date)]
    [Display(Name = "Date of Birth")]
    public string dob { get; set; }

    [Display(Name = "Region :")]
    public long region { get; set; }

    [Display(Name = "District :")]
    public long district { get; set; }

    [Display(Name = "Ward :")]
    public long ward { get; set; }

    public string posted_by { get; set; }

    public DateTime posted_date { get; set; }

    [Display(Name = "Entrepreneurship Status :")]
    public string entrepreneur_status { get; set; }

    [Display(Name = "I am looking For :")]
    public List<string> opportunity { get; set; }

    [Display(Name = "Business Sector :")]
    public List<string> business_sector { get; set; }

    public string ip_address { get; set; }

    public string Int_Mobile_number { get; set; }
  }
}
