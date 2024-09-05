// Decompiled with JetBrains decompiler
// Type: FUNDING.Models.CustomValidations.CardTemplatesValidation
// Assembly: FUNDING, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5DE1C1E8-8D80-43BD-915D-F2502219BD58
// Assembly location: D:\ECardtest - 22082024\bin\FUNDING.dll

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

 
namespace FUNDING.Models.CustomValidations
{
  [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
  public class CardTemplatesValidation : ValidationAttribute, IClientValidatable
  {
    public string Extensions { get; set; } = "png,jpg,jpeg,gif";

    public IEnumerable<ModelClientValidationRule> GetClientValidationRules(
      ModelMetadata metadata,
      ControllerContext context)
    {
      return (IEnumerable<ModelClientValidationRule>) new ModelClientValidationRule[1]
      {
        new ModelClientValidationRule()
        {
          ErrorMessage = this.ErrorMessage,
          ValidationType = "fileattachmentsval"
        }
      };
    }

    public override bool IsValid(object value)
    {
      HttpPostedFileBase httpPostedFileBase = value as HttpPostedFileBase;
      bool flag = true;
      List<string> list = ((IEnumerable<string>) this.Extensions.Split(new char[1]
      {
        ','
      }, StringSplitOptions.RemoveEmptyEntries)).ToList<string>();
      if (httpPostedFileBase != null)
      {
        string fileName = httpPostedFileBase.FileName;
        flag = list.Any<string>((Func<string, bool>) (y => fileName.EndsWith(y)));
      }
      return flag;
    }
  }
}
