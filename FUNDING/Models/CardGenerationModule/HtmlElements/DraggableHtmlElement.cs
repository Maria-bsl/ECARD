// Decompiled with JetBrains decompiler
// Type: FUNDING.Models.CardGenerationModule.HtmlElements.DraggableHtmlElement
// Assembly: FUNDING, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5DE1C1E8-8D80-43BD-915D-F2502219BD58
// Assembly location: D:\ECardtest - 22082024\bin\FUNDING.dll

using iText.Layout;

 
namespace FUNDING.Models.CardGenerationModule.HtmlElements
{
  public abstract class DraggableHtmlElement
  {
    private readonly Document _document;

    public DraggableHtmlElement(Document document) => this._document = document;

    protected float Pixel2Point(float pixelUnit) => pixelUnit * 0.75f;

    protected abstract void ImplementElement();
  }
}
