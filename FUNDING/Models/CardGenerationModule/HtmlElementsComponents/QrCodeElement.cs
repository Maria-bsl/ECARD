// Decompiled with JetBrains decompiler
// Type: FUNDING.Models.CardGenerationModule.HtmlElementsComponents.QrCodeElement
// Assembly: FUNDING, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5DE1C1E8-8D80-43BD-915D-F2502219BD58
// Assembly location: D:\ECardtest - 22082024\bin\FUNDING.dll

using System.IO;

 
namespace FUNDING.Models.CardGenerationModule.HtmlElementsComponents
{
  public class QrCodeElement
  {
    private readonly string _imageFullPath;
    private readonly float _imageSize;

    public string QrCodeFullPath => this._imageFullPath;

    public float QrCodeImageSize => this._imageSize;

    public QrCodeElement(string imageFullPath, float imageSize = 0.0f)
    {
      this._imageFullPath = File.Exists(imageFullPath) ? imageFullPath : (string) null;
      this._imageSize = (double) imageSize <= 0.0 ? 165f : imageSize;
    }
  }
}
