using System;
using System.ComponentModel.DataAnnotations;

namespace FUNDING.BL.BusinessEntities.Masters
{
    public class CustomerEventViewModel
    {
        #region Class Properties

        public long EventID { get; set; }

        public string PackageName { get; set; }

        [Required(ErrorMessage = "Please select customer name.")]
        public long? CustomerID { get; set; }

        [Display(Name = "Customer Name")]
        [StringLength(200, ErrorMessage = "The field allows only 200 characters.")]
        public string CustomerName { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Please enter event name.")]
        [Display(Name = "Event Name")]
        [StringLength(200, ErrorMessage = "The field allows only 200 characters.")]
        public string EventName { get; set; }

        [Required(ErrorMessage = "Please enter event date.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Event Date")]
        public DateTime? EventDate { get; set; }

        [Required(ErrorMessage = "Please enter event start time.")]
        [DataType(DataType.Time)]
        [Display(Name = "Start Time")]
        [DisplayFormat(DataFormatString = "{0:HH:mm}")]
        public DateTime? Event_Start_Time { get; set; }

        [Display(Name = "Event Status")]
        [StringLength(10, ErrorMessage = "The field allows only 10 characters.")]
        public string EventStatus { get; set; }

        public virtual Customers Customer { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Event Host")]
        [StringLength(200, ErrorMessage = "The field allows only 200 characters.")]
        public string EventHost { get; set; }


        [DataType(DataType.MultilineText)]
        [StringLength(200, ErrorMessage = "The field allows only 200 characters.")]
        public string Venue { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(200, ErrorMessage = "The field allows only 200 characters.")]
        public string Reservation { get; set; }


        public string Visitor_Confirmation { get; set; }


        public bool Reservation_Confirmation { get; set; }



        #endregion

    }
}
