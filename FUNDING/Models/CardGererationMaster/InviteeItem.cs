// Decompiled with JetBrains decompiler
// Type: FUNDING.Models.CardGererationMaster.InviteeItem
// Assembly: FUNDING, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5DE1C1E8-8D80-43BD-915D-F2502219BD58
// Assembly location: D:\ECardtest - 22082024\bin\FUNDING.dll

using System;
using System.Collections.Generic;
using System.Web.Mvc;

 
namespace FUNDING.Models.CardGererationMaster
{
  public class InviteeItem
  {
    public InviteeItem(string v, string visitor_name)
    {
      this.V = v;
      this.Visitor_name = visitor_name;
    }

    public int Id { get; set; }

    public string Name { get; set; }

    public string V { get; }

    public string Visitor_name { get; }

    public static implicit operator InviteeItem(List<SelectListItem> v)
    {
      throw new NotImplementedException();
    }

    internal void Add(SelectListItem selectListItem) => throw new NotImplementedException();
  }
}
