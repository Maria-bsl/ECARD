// Decompiled with JetBrains decompiler
// Type: FUNDING.Models.Notifications.WhatsAppNotification
// Assembly: FUNDING, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5DE1C1E8-8D80-43BD-915D-F2502219BD58
// Assembly location: D:\ECardtest - 22082024\bin\FUNDING.dll

using ECARD.DL.EDMX;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

 
namespace FUNDING.Models.Notifications
{
  public class WhatsAppNotification
  {
    [Required(ErrorMessage = "Please enter WhatsApp mobile number.")]
    [Display(Name = "WhatsApp Number")]
    [RegularExpression("^\\+?\\d+$", ErrorMessage = "Please enter a valid WhatsApp mobile number.")]
    public string WhatsAppNumber { get; set; }

    [Required(ErrorMessage = "Please enter message.")]
    public string Message { get; set; }

    [ForeignKey("visitor_details")]
    [Required(ErrorMessage = "Please select visitor.")]
    [Display(Name = "Visitor")]
    public long Visitor_Id { get; set; }

    [ForeignKey("event_details")]
    [Required(ErrorMessage = "Please select event.")]
    [Display(Name = "Title of Event")]
    public long Event_Id { get; set; }

    public visitor_details Visitor_Details { get; set; }

    public event_details Event_Details { get; set; }
  }
}
