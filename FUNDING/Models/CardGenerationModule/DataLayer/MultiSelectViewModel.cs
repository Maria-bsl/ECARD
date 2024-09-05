// Decompiled with JetBrains decompiler
// Type: FUNDING.Models.CardGenerationModule.DataLayer.MultiSelectViewModel
// Assembly: FUNDING, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5DE1C1E8-8D80-43BD-915D-F2502219BD58
// Assembly location: D:\ECardtest - 22082024\bin\FUNDING.dll

using System.Collections.Generic;
using System.Web.Mvc;

 
namespace FUNDING.Models.CardGenerationModule.DataLayer
{
  public class MultiSelectViewModel
  {
    public List<SelectListItem> InviteeItem { get; set; }

    public List<string> SelectedInviteeItems { get; set; }
  }
}
