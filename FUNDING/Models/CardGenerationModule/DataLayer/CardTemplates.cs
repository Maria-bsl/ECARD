// Decompiled with JetBrains decompiler
// Type: FUNDING.Models.CardGenerationModule.DataLayer.CardTemplates
// Assembly: FUNDING, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5DE1C1E8-8D80-43BD-915D-F2502219BD58
// Assembly location: D:\ECardtest - 22082024\bin\FUNDING.dll

using FUNDING.Models.CustomValidations;
using System.ComponentModel.DataAnnotations;
using System.Web;

 
namespace FUNDING.Models.CardGenerationModule.DataLayer
{
  public class CardTemplates
  {
    public long Id { get; set; }

    public string CardTemplateFilePath { get; set; }

    [Required(ErrorMessage = "Card Template Attachment is required")]
    [Display(Name = "Card Template")]
    [CardTemplatesValidation(ErrorMessage = "Please select images with .jpg or .png extension", Extensions = "jpg,png")]
    public HttpPostedFileBase CardTemplateFileAttach { get; set; }
  }
}
