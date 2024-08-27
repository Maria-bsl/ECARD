// Decompiled with JetBrains decompiler
// Type: FUNDING.Models.CardGererationMaster.FontMasterClass
// Assembly: FUNDING, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5DE1C1E8-8D80-43BD-915D-F2502219BD58
// Assembly location: D:\ECardtest - 22082024\bin\FUNDING.dll

using System;

 
namespace FUNDING.Models.CardGererationMaster
{
  public struct FontMasterClass
  {
    public float FontSize { get; set; }

    public bool Italic { get; set; }

    public bool Bold { get; set; }

    public bool Underline { get; set; }

    public string TextAlign { get; set; }

    public string Color { get; set; }

    public string FontName { get; set; }

    public int[] GetColorValues()
    {
      return Array.ConvertAll<string, int>(this.Color.Replace("rgb(", "").Replace(")", "").Replace(" ", "").Split(','), new Converter<string, int>(int.Parse));
    }
  }
}
