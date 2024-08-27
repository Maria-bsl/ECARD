// Decompiled with JetBrains decompiler
// Type: FUNDING.Models.CardGenerationModule.HtmlElements.DraggableQrCodeElement
// Assembly: FUNDING, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5DE1C1E8-8D80-43BD-915D-F2502219BD58
// Assembly location: D:\ECardtest - 22082024\bin\FUNDING.dll

using FUNDING.Models.CardGenerationModule.HtmlElementsComponents;
using iText.IO.Image;
using iText.Layout;
using System;

 
namespace FUNDING.Models.CardGenerationModule.HtmlElements
{
  public class DraggableQrCodeElement : DraggableHtmlElement
  {
    private readonly Document _document;
    private readonly string _imageFullPath;
    private readonly float _imageSize;
    private readonly ElementRelativePosition _position;
    private iText.Layout.Element.Image img;

    public DraggableQrCodeElement(
      Document document,
      QrCodeElement qrCode,
      ElementRelativePosition position)
      : base(document)
    {
      this._document = document;
      this._imageFullPath = qrCode.QrCodeFullPath;
      this._position = position;
      this._imageSize = this.Pixel2Point(qrCode.QrCodeImageSize);
      this.img = new iText.Layout.Element.Image(ImageDataFactory.Create(this._imageFullPath));
      this.ImplementElement();
    }

    private void SetQrCodeSize() => this.img.ScaleToFit(this._imageSize, this._imageSize);

    private void SetQrCodePosition()
    {
      this.img.SetRelativePosition(this.Pixel2Point(this._position.LeftPosition), this.Pixel2Point(this._position.TopPosition), 0.0f, 0.0f);
    }

    protected override void ImplementElement()
    {
      try
      {
        this.SetQrCodeSize();
        this.SetQrCodePosition();
        this._document.Add(this.img);
      }
      catch (Exception ex)
      {
        Console.WriteLine("Error! File does not exists");
      }
    }
  }
}
