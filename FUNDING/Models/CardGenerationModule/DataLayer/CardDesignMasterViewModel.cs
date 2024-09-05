// Decompiled with JetBrains decompiler
// Type: FUNDING.Models.CardGenerationModule.DataLayer.CardDesignMasterViewModel
// Assembly: FUNDING, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5DE1C1E8-8D80-43BD-915D-F2502219BD58
// Assembly location: D:\ECardtest - 22082024\bin\FUNDING.dll

using FUNDING.Models.CardGererationMaster;
using System.Collections.Generic;

 
namespace FUNDING.Models.CardGenerationModule.DataLayer
{
  public class CardDesignMasterViewModel
  {
    public IEnumerable<CardTemplates> CardTemplatesList { get; set; }

    public IEnumerable<FontFamily> AllFontFamilies { get; set; }

    public CardEventDetailsViewModel EventDetails { get; set; }

    public IEnumerable<InviteeItem> Invitee { get; set; }
  }
}
