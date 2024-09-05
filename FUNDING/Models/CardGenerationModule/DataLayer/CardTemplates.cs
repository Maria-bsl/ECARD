

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
