// Decompiled with JetBrains decompiler
// Type: FUNDING.Models.CustomValidations.FileExtensiosValidator
// Assembly: FUNDING, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5DE1C1E8-8D80-43BD-915D-F2502219BD58
// Assembly location: D:\ECardtest - 22082024\bin\FUNDING.dll

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Web;
using System.Web.Mvc;

 
namespace FUNDING.Models.CustomValidations
{
  public class FileExtensiosValidator : ValidationAttribute, IClientValidatable
  {
    public string AllowedExtensions { get; set; } = "png,jpg,jpeg,gif";

    public override bool IsValid(object value)
    {
      return this.AllowedExtensions.Contains(Path.GetExtension((value as HttpPostedFileBase).FileName).TrimStart('.'));
    }

    public IEnumerable<ModelClientValidationRule> GetClientValidationRules(
      ModelMetadata metadata,
      ControllerContext context)
    {
      return (IEnumerable<ModelClientValidationRule>) new ModelClientValidationRule[1]
      {
        new ModelClientValidationRule()
        {
          ErrorMessage = this.ErrorMessage,
          ValidationType = "fileextensions"
        }
      };
    }
  }
}
