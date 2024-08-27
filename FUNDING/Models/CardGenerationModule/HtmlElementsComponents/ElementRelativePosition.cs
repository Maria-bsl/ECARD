// Decompiled with JetBrains decompiler
// Type: FUNDING.Models.CardGenerationModule.HtmlElementsComponents.ElementRelativePosition
// Assembly: FUNDING, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5DE1C1E8-8D80-43BD-915D-F2502219BD58
// Assembly location: D:\ECardtest - 22082024\bin\FUNDING.dll

 
namespace FUNDING.Models.CardGenerationModule.HtmlElementsComponents
{
  public class ElementRelativePosition
  {
    private readonly float _topPosition;
    private readonly float _leftPosition;

    public float TopPosition => this._topPosition;

    public float LeftPosition => this._leftPosition;

    public ElementRelativePosition(float leftPositon, float topPosition)
    {
      this._leftPosition = leftPositon;
      this._topPosition = topPosition;
    }
  }
}
