// Decompiled with JetBrains decompiler
// Type: FUNDING.BL.BusinessEntities.Masters.AllowedFileSizeAttribute
// Assembly: FUNDING.BL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A421DCBA-0154-4E02-9814-774D89923779
// Assembly location: D:\ECardtest - 22082024\bin\FUNDING.BL.dll

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

#nullable disable
namespace FUNDING.BL.BusinessEntities.Masters
{
  [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
  public class AllowedFileSizeAttribute : ValidationAttribute, IClientValidatable
  {
    public int FileSize { get; set; } = 2097152;

    public override bool IsValid(object value)
    {
      HttpPostedFileBase httpPostedFileBase = value as HttpPostedFileBase;
      bool flag = true;
      int fileSize = this.FileSize;
      if (httpPostedFileBase != null)
        flag = httpPostedFileBase.ContentLength <= fileSize;
      return flag;
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
          ValidationType = "filesizeruleval"
        }
      };
    }
  }
}
