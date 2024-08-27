using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FUNDING.BL.BusinessEntities.Masters
{
    public class Events
    {
        public Events()
        {
            VisitorVerification = new HashSet<Visitors_Verification>();
            Visitor = new HashSet<Visitors>();
        }

        public long event_det_sno { get; set; }

        public long? cust_reg_sno { get; set; }

        [Required]
        [Display(Name = "Host Name")]
        [MaxLength(200, ErrorMessage = "The field allows only 200 characters.")]
        public string inviter_name { get; set; }

        [Required]
        [Display(Name = "Event Name")]
        [MaxLength(200, ErrorMessage = "The field allows only 200 characters.")]
        public string event_name { get; set; }

        [Required(ErrorMessage = "Please enter event date.")]
        //[DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Event Date")]
        public DateTime? event_date { get; set; }

        [Required(ErrorMessage = "Please enter event start time.")]
        //[DataType(DataType.Time)]
        [Display(Name = "Start Time")]
        [DisplayFormat(DataFormatString = "{0:HH:mm}")]
        public DateTime? event_start_time { get; set; }

        [Display(Name = "Venue")]
        [DataType(DataType.MultilineText)]
        [MaxLength(200, ErrorMessage = "The field allows only 200 characters.")]
        public string venue { get; set; }

        [Display(Name = "Reservation")]
        [DataType(DataType.MultilineText)]
        [MaxLength(200, ErrorMessage = "The field allows only 200 characters.")]
        public string reservation { get; set; }
         
        [Display(Name = "Event Status")]
        [MaxLength(10, ErrorMessage = "The field allows only 10 characters.")]
        public string event_status { get; set; }

        [Display(Name = "Remarks")]
        [DataType(DataType.MultilineText)]
        [MaxLength(300, ErrorMessage = "The field allows only 300 characters.")]
        public string remarks { get; set; }

        [Display(Name = "Posted By")]
        public string posted_by { get; set; }

        [Display(Name = "Posted Date")]
        public DateTime posted_date { get; set; }

        public virtual Customers Customer { get; set; }
        public virtual ICollection<Visitors_Verification> VisitorVerification { get; set; }
        public virtual ICollection<Visitors> Visitor { get; set; }

        [NotMapped]
        [Display(Name = "Event Date")]
        public string Event_Date_String { get; set; }

        [NotMapped]
        [Display(Name = "Start Time")]
        public string Event_Start_Time_String { get; set; }

        private static string GetRandomKey(int len)
        {
            int maxSize = len;
            char[] chars = new char[30];
            string a;
            a = "1234567890";
            chars = a.ToCharArray();
            int size = maxSize;
            byte[] data = new byte[1];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            size = maxSize;
            data = new byte[size];
            crypto.GetNonZeroBytes(data);
            StringBuilder result = new StringBuilder(size);

            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }

            return result.ToString();
        }
        
        private static string GetRandomAlphanumericKey(int len)
        {
            int maxSize = len;
            char[] chars = new char[30];
            string a;
            a = "1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            chars = a.ToCharArray();
            int size = maxSize;
            byte[] data = new byte[1];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            size = maxSize;
            data = new byte[size];
            crypto.GetNonZeroBytes(data);
            StringBuilder result = new StringBuilder(size);

            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }

            return result.ToString();
        }

        public static string GenerateQRCodeIdentity(long visitor_id)
        {
            string randomNumber = GetRandomAlphanumericKey(4);
            var event_IdText = visitor_id.ToString().PadLeft(6, '0');
            return "BIZ" + event_IdText + randomNumber;
        }
        
        public static string GenerateQRCodeIdentity(long? event_id, byte numberOfRandomCharacters)
        {
            string randomNumber = GetRandomAlphanumericKey(numberOfRandomCharacters);
            var event_IdText = event_id.ToString().PadLeft(6, '0');
            return "BIZ" + event_IdText + randomNumber;
        }
    }

    public class EventViewModel
    {
        #region Class Property Section

        public long event_det_sno { get; set; }

        public long? cust_reg_sno { get; set; }

        [Required(ErrorMessage = "Host Name is required")]
        [Display(Name = "Host Name")]
        [MaxLength(200, ErrorMessage = "The field allows only 200 characters.")]
        public string inviter_name { get; set; }

        [Required]
        [Display(Name = "Event Name")]
        [MaxLength(200, ErrorMessage = "The field allows only 200 characters.")]
        public string event_name { get; set; }

        [Required(ErrorMessage = "Please enter event date.")]
        //[DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Event Date")]
        public DateTime? event_date { get; set; }

        [Required(ErrorMessage = "Please enter event start time.")]
        //[DataType(DataType.Time)]
        [Display(Name = "Start Time")]
        [DisplayFormat(DataFormatString = "{0:HH:mm}")]
        public DateTime? event_start_time { get; set; }

        [Display(Name = "Venue")]
        [DataType(DataType.MultilineText)]
        [MaxLength(200, ErrorMessage = "The field allows only 200 characters.")]
        public string venue { get; set; }

        [Display(Name = "Reservation")]
        [DataType(DataType.MultilineText)]
        [MaxLength(200, ErrorMessage = "The field allows only 200 characters.")]
        public string reservation { get; set; }

        [Display(Name = "Remarks")]
        [DataType(DataType.MultilineText)]
        [MaxLength(300, ErrorMessage = "The field allows only 300 characters.")]
        public string remarks { get; set; }


        [NotMapped]
        [Display(Name = "Event Date")]
        public string Event_Date_String { get; set; }

        [NotMapped]
        [Display(Name = "Start Time")]
        public string Event_Start_Time_String { get; set; }

        #endregion
    }
}
