using ECARD.DL.EDMX;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FUNDING.BL.BusinessEntities.Masters
{
    public class Visitors
    {
        public Visitors()
        {
            VisitorsVerification = new HashSet<Visitors_Verification>();
        }

        #region Class Properties

        public long visitor_det_sno { get; set; }
        public long? event_det_sno { get; set; }
        public long? cust_reg_sno { get; set; }

        [Required(ErrorMessage = "Please enter visitor name.")]
        [Display(Name = "Visitor Name")]
        [MaxLength(100, ErrorMessage = "The field allows only 100 characters.")]
        public string visitor_name { get; set; }

        [Display(Name = "QR Code")]
        public string qrcode { get; set; }

        [ForeignKey("card_state_master")]
        [Required(ErrorMessage = "Please select card state.")]
        public long? card_state_mas_sno { get; set; }

        [Required(ErrorMessage = "Please enter number of people.")]
        [Display(Name = "Number of People")]
        [DataType(DataType.Text)]
        [RegularExpression("\\d+", ErrorMessage = "This field allows only digits.")]
        public int? no_of_persons { get; set; }

        [Required(ErrorMessage = "Please enter mobile number.")]
        [Display(Name = "Mobile Number")]
        [RegularExpression("\\d+", ErrorMessage = "Please enter a valid mobile number.")]
        public string mobile_no { get; set; }

        [Display(Name = "Email Address")]
        [MaxLength(50, ErrorMessage = "The field allows only 50 characters.")]
        [DataType(DataType.EmailAddress)]
        public string email_address { get; set; }
        public string posted_by { get; set; }
        public DateTime posted_date { get; set; }

        [Display(Name = "Table Number")]
        public string Table_Number { get; set; }

        public virtual CardStates_Master CardStatesMaster { get; set; }
        public virtual Customers Customer { get; set; }
        public virtual Events Event { get; set; }

        public virtual ICollection<Visitors_Verification> VisitorsVerification { get; set; }

        [NotMapped]
        public string Int_Mobile_number { get; set; }


        #endregion;


        #region Class Methods
        public static string GetGeneratedQR_Code(ECARDAPPEntities context, long visitor_det_sno)
        {
            string qrCodeIdentity = null;
            bool flag;
            do
            {
                qrCodeIdentity = Events.GenerateQRCodeIdentity(visitor_det_sno);

                var visitorWithQrCode = context.visitor_details
                         .Where(v => v.event_det_sno == visitor_det_sno && v.qrcode == qrCodeIdentity).FirstOrDefault();

                if (visitorWithQrCode != null)
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }


            } while (flag);

            return qrCodeIdentity;
        }

        public static string GetGeneratedQRCodeWithEventId(ECARDAPPEntities _dbContext, long? event_id)
        {
            string qrCodeIdentity = null;
            bool flag;
            do
            {
                qrCodeIdentity = Events.GenerateQRCodeIdentity(event_id, 5);

                var visitorWithQrCode = _dbContext.visitor_details
                    .Where(v => v.event_det_sno == event_id && v.qrcode == qrCodeIdentity)
                    .FirstOrDefault();

                flag = visitorWithQrCode != null;

            } while (flag);

            return qrCodeIdentity;
        }


        #endregion;
    }



    /// <summary>  
    /// Allow file size attribute class  
    /// </summary>  
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class AllowedFileSizeAttribute : ValidationAttribute, IClientValidatable
    {
        #region Public / Protected Properties  

        /// <summary>  
        /// Gets or sets file size property. Default is 2MB (the value is in Bytes).  
        /// </summary>  
        public int FileSize { get; set; } = 2 * 1024 * 1024;


        #endregion

        #region Is valid method  

        /// <summary>  
        /// Is valid method.  
        /// </summary>  
        /// <param name="value">Value parameter</param>  
        /// <returns>Returns - true is specify extension matches.</returns>  
        public override bool IsValid(object value)
        {
            // Initialization  
            HttpPostedFileBase file = value as HttpPostedFileBase;
            bool isValid = true;

            // Settings.  
            int allowedFileSize = this.FileSize;

            // Verification.  
            if (file != null)
            {
                // Initialization.  
                var fileSize = file.ContentLength;

                // Settings.  
                isValid = fileSize <= allowedFileSize;
            }

            // Info  
            return isValid;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            ModelClientValidationRule fileSizeRule = new ModelClientValidationRule
            {
                ErrorMessage = base.ErrorMessage,
                ValidationType = "filesizeruleval"
            };
            return new ModelClientValidationRule[] { fileSizeRule };
        }

        #endregion
    }
}
