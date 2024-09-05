// Decompiled with JetBrains decompiler
// Type: FUNDING.Models.CardGenerationModule.DataLayer.CardEventDetailsViewModel
// Assembly: FUNDING, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5DE1C1E8-8D80-43BD-915D-F2502219BD58
// Assembly location: D:\ECardtest - 22082024\bin\FUNDING.dll

using System.Collections.Generic;
using System.Web.Mvc;

 
namespace FUNDING.Models.CardGenerationModule.DataLayer
{
  public class CardEventDetailsViewModel
  {
    public long EventId { get; set; }

    public string EventHost { get; set; }

    public EditableCardItemStyles EventHostStyles { get; set; }

    public string EventName { get; set; }

    public string[] EventDate { get; set; }

    public string[] EventStartTime { get; set; }

    public string Venue { get; set; }

    public string Reservation { get; set; }

    public bool ShowCardSize { get; set; }

    public MultiSelectViewModel InviteeList { get; set; }

    public List<SelectListItem> InviteeItems { get; set; }

    public List<string> SelectedInviteeItems { get; set; }
  }
}
