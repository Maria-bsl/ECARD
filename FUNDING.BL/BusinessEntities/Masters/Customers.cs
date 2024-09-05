using ECARD.DL.EDMX;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUNDING.BL.BusinessEntities.Masters
{
    public class Customers

    {

        #region Class Properties

        public long cust_reg_sno { get; set; }

        [Required(ErrorMessage = "Please enter a customer name.")]
        [Display(Name = "Customer Name")]
        [RegularExpression("^(([a-zA-Z]+)([',.-]?))*(([a-zA-Z]+)([',.-]?[ ]?))*[a-zA-Z]+[ ]{0,3}$", ErrorMessage = "Please enter a valid customer name.")]
        [MaxLength(200, ErrorMessage = "The field allows only 200 characters.")]
        public string cust_name { get; set; }

        [Display(Name = "Physical Address")]
        [RegularExpression("^(([a-zA-Z]+)([',.-]?))*(([a-zA-Z0-9]+)([',.-]?[ ]?))*[a-zA-Z0-9]+[ ]{0,3}$", ErrorMessage = "Please enter a valid physical address.")]
        [MaxLength(500, ErrorMessage = "The field allows only 500 characters.")]
        public string physical_address { get; set; }

        [Required]
        [Display(Name = "Mobile Number")]
        [MaxLength(200, ErrorMessage = "The field allows only 200 characters.")]
        [RegularExpression("^0\\d{9}$", ErrorMessage = "Please enter a valid mobile number.")]
        public string mobile_no { get; set; }

        [Display(Name = "Email Address")]
        [MaxLength(200, ErrorMessage = "The field allows only 200 characters.")]
        [DataType(DataType.EmailAddress)]
        public string email_address { get; set; }

        [MaxLength(10, ErrorMessage = "The field allows only 10 characters.")]
        public string status { get; set; }

        [Display(Name = "Posted By")]
        [MaxLength(20, ErrorMessage = "The field allows only 20 characters.")]
        public string posted_by { get; set; }

        [Display(Name = "Posted Date")]
        public System.DateTime posted_date { get; set; }

        public virtual ICollection<CustomerUsers> CustomerUser { get; set; }
        public virtual ICollection<Events> EventDetails { get; set; }
        public virtual ICollection<Visitors_Verification> VisitorsVerification { get; set; }
        public virtual ICollection<Visitors> Visitor { get; set; }

        #endregion;

        #region Model Methods

        #endregion;
    }
}
