// Decompiled with JetBrains decompiler
// Type: FUNDING.Models.CardGenerationModule.HtmlElements.DraggableTextElement
// Assembly: FUNDING, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5DE1C1E8-8D80-43BD-915D-F2502219BD58
// Assembly location: D:\ECardtest - 22082024\bin\FUNDING.dll

using FUNDING.Models.CardGenerationModule.HtmlElementsComponents;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

 
namespace FUNDING.Models.CardGenerationModule.HtmlElements
{
  internal class DraggableTextElement : DraggableHtmlElement
  {
    private readonly Document _document;
    private readonly Paragraph paragraph;
    private readonly ElementRelativePosition _position;
    private readonly TextElement _textElement;

    public DraggableTextElement(
      Document document,
      TextElement textElement,
      ElementRelativePosition position)
      : base(document)
    {
      this._document = document;
      this._position = position;
      this._textElement = textElement;
      this.paragraph = new Paragraph(textElement.TextValue);
      this.ImplementElement();
    }

    private void SetElementTextBold()
    {
      if (!this._textElement.IsTextElementBold)
        return;
      this.paragraph.SetBold();
    }

    private void SetElementTextItalic()
    {
      if (!this._textElement.IsTextElementItalic)
        return;
      this.paragraph.SetItalic();
    }

    private void SetElementTextUnderlined()
    {
      if (!this._textElement.IsTextElementUnderlined)
        return;
      this.paragraph.SetUnderline();
    }

    private void SetElementFontSize()
    {
      this.paragraph.SetFontSize(this.Pixel2Point(this._textElement.Font.FontSize));
    }

    private void SetElementFontColor() => this.paragraph.SetFontColor(this._textElement.FontColor);

    private void SetElementFontFamily()
    {
      this.paragraph.SetFont(this._textElement.Font.FontFamily);
    }

    private void SetTextElementPosition()
    {
      this.paragraph.SetRelativePosition(this.Pixel2Point(this._position.LeftPosition), this.Pixel2Point(this._position.TopPosition), 0.0f, 0.0f);
    }

    private void SetTextElementHorizontalAlignment()
    {
      switch (this._textElement.TextAlign.ToLower())
      {
        case "left":
          this.paragraph.SetTextAlignment(new TextAlignment?(TextAlignment.LEFT));
          break;
        case "right":
          this.paragraph.SetTextAlignment(new TextAlignment?(TextAlignment.RIGHT));
          break;
        case "center":
          this.paragraph.SetTextAlignment(new TextAlignment?(TextAlignment.CENTER));
          break;
        case "justified":
          this.paragraph.SetTextAlignment(new TextAlignment?(TextAlignment.JUSTIFIED_ALL));
          break;
        default:
          this.paragraph.SetTextAlignment(new TextAlignment?(TextAlignment.CENTER));
          break;
      }
    }

    protected override void ImplementElement()
    {
      this.SetElementFontFamily();
      this.SetElementFontColor();
      this.SetElementFontSize();
      this.SetTextElementPosition();
      this.SetTextElementHorizontalAlignment();
      this.SetElementTextBold();
      this.SetElementTextItalic();
      this.SetElementTextUnderlined();
      this.paragraph.SetVerticalAlignment(new VerticalAlignment?(VerticalAlignment.MIDDLE)).SetMinHeight(this.Pixel2Point(33.5f)).SetMargin(0.0f).SetMultipliedLeading(0.75f).SetPadding(0.0f);
      this._document.Add((IBlockElement) this.paragraph);
    }
  }
}
