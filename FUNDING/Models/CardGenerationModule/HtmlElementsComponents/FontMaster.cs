// Decompiled with JetBrains decompiler
// Type: FUNDING.Models.CardGenerationModule.HtmlElementsComponents.FontMaster
// Assembly: FUNDING, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5DE1C1E8-8D80-43BD-915D-F2502219BD58
// Assembly location: D:\ECardtest - 22082024\bin\FUNDING.dll

using FUNDING.Models.AppHelpers;
using iText.Kernel.Font;
using System.IO;
using System.Web.Hosting;

 
namespace FUNDING.Models.CardGenerationModule.HtmlElementsComponents
{
  public class FontMaster
  {
    public const string NormalFontStyle = "normal";
    private readonly string _fontName;
    private readonly string _fontFullPath;
    private readonly float _fontSize;
    private readonly string _fontStyle;

    public string FontName => this._fontName;

    public PdfFont FontFamily => this.GetFontFamily();

    public float FontSize => this._fontSize;

    public FontMaster(string fontName, string fontFullPath, float fontSize = 28f, string fontStyle = "normal")
    {
      this._fontName = fontName;
      this._fontFullPath = File.Exists(fontFullPath) ? fontFullPath : (string) null;
      this._fontSize = (double) fontSize < 0.0 ? 0.0f : fontSize;
      this._fontStyle = fontStyle;
    }

    public FontMaster(string fontName, float fontSize = 28f)
    {
      this._fontName = fontName;
      this._fontSize = (double) fontSize < 0.0 ? 0.0f : fontSize;
      this._fontFullPath = this.GetTtfFontFile();
    }

    public string GetTtfFontFile()
    {
      foreach (string file in Directory.GetFiles(HostingEnvironment.MapPath(Path.Combine(DirectoryHelpers.FontFamilyVirtualDirectory, this._fontName))))
      {
        if (Path.GetExtension(file) == ".ttf")
          return file;
      }
      return (string) null;
    }

    private PdfFont GetFontFamily()
    {
      return !string.IsNullOrEmpty(this._fontFullPath) ? PdfFontFactory.CreateFont(this._fontFullPath) : throw new FileNotFoundException("This font file does not exist.");
    }
  }
}
