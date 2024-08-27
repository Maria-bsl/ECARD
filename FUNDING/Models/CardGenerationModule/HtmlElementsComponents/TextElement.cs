// Decompiled with JetBrains decompiler
// Type: FUNDING.Models.CardGenerationModule.HtmlElementsComponents.TextElement
// Assembly: FUNDING, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5DE1C1E8-8D80-43BD-915D-F2502219BD58
// Assembly location: D:\ECardtest - 22082024\bin\FUNDING.dll

using iText.Kernel.Colors;

 
namespace FUNDING.Models.CardGenerationModule.HtmlElementsComponents
{
  public class TextElement
  {
    private readonly string _texValue;
    private readonly FontMaster _font;
    private readonly Color _fontColor;
    private readonly string _textAlign;

    public TextElement(string textValue, FontMaster font, Color fontColor, string textAlign = "center")
    {
      this._texValue = textValue;
      this._font = font;
      this._fontColor = fontColor;
      this._textAlign = textAlign;
    }

    public bool IsTextElementBold { get; set; }

    public bool IsTextElementItalic { get; set; }

    public bool IsTextElementUnderlined { get; set; }

    public string TextValue => this._texValue;

    public FontMaster Font => this._font;

    public Color FontColor => this._fontColor;

    public string TextAlign => this._textAlign;
  }
}
