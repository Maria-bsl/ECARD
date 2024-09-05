// Decompiled with JetBrains decompiler
// Type: FUNDING.Models.Packages
// Assembly: FUNDING, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5DE1C1E8-8D80-43BD-915D-F2502219BD58
// Assembly location: D:\ECardtest - 22082024\bin\FUNDING.dll

using System;
using System.Collections.Generic;

 
namespace FUNDING.Models
{
  public class Packages
  {
    public long sno { get; set; }

    public string pack_name { get; set; }

    public string pack_description { get; set; }

    public long? pack_price { get; set; }

    public string pack_status { get; set; }

    public DateTime? effective_date { get; set; }

    public string posted_by { get; set; }

    public DateTime? posted_date { get; set; }

    public virtual ICollection<ECARD.DL.EDMX.event_details> event_details { get; set; }
  }
}
