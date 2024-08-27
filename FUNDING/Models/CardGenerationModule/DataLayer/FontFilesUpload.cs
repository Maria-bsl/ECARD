// Decompiled with JetBrains decompiler
// Type: FUNDING.Models.CardGenerationModule.DataLayer.FontFilesUpload
// Assembly: FUNDING, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5DE1C1E8-8D80-43BD-915D-F2502219BD58
// Assembly location: D:\ECardtest - 22082024\bin\FUNDING.dll

using FUNDING.Models.CustomValidations;
using System.ComponentModel.DataAnnotations;
using System.Web;

 
namespace FUNDING.Models.CardGenerationModule.DataLayer
{
  public class FontFilesUpload
  {
    [Required(ErrorMessage = "Font Name is required")]
    [Display(Name = "Font Name")]
    public string FontName { get; set; }

    [Required(ErrorMessage = "Font File Attachment is required")]
    [Display(Name = "Font Files")]
    [CardTemplatesValidation(ErrorMessage = "Please select valid font files", Extensions = "eot,svg,ttf,woff,woff2,css,html")]
    public HttpPostedFileBase[] FontFilesAttachment { get; set; }
  }
}
