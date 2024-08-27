// Decompiled with JetBrains decompiler
// Type: FUNDING.Models.ViewModels.VisitorsBulkUpload
// Assembly: FUNDING, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5DE1C1E8-8D80-43BD-915D-F2502219BD58
// Assembly location: D:\ECardtest - 22082024\bin\FUNDING.dll

using FUNDING.Models.CustomValidations;
using System.ComponentModel.DataAnnotations;
using System.Web;

 
namespace FUNDING.Models.ViewModels
{
  public class VisitorsBulkUpload
  {
    [Required(ErrorMessage = "Please upload an attachment")]
    [Display(Name = "Upload File")]
    [FileExtensiosValidator(AllowedExtensions = "xlsx,xls", ErrorMessage = "Only excel (.xlsx or .xls) attachments are accepted.")]
    public HttpPostedFileBase VisitorsFileAttachment { get; set; }
  }
}
