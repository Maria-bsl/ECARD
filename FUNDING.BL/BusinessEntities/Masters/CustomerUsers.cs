using ECARD.DL.EDMX;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FUNDING.BL.BusinessEntities.Masters
{
    public class CustomerUsers
    {
        #region Class Properties

        public long cust_users_sno { get; set; }
        public long? cust_reg_sno { get; set; }

        [Required(ErrorMessage = "Please enter user's full name.")]
        [Display(Name = "Full Name")]
        [RegularExpression("^(([a-zA-Z]+)([',.-]?))*(([a-zA-Z]+)([',.-]?[ ]?))*[a-zA-Z]+[ ]{0,3}$", ErrorMessage = "Please enter a valid full name.")]
        [StringLength(100, ErrorMessage = "The field allows only 100 characters.")]
        public string user_fullname { get; set; }

        public string eventName { get; set; }

        //[Display(Name = "Username")]
        //[Required(ErrorMessage = "Please enter username.")]
        //[RegularExpression("^[@A-Za-z][A-Za-z0-9_]{3,29}$", ErrorMessage = "Please enter a valid username.")]
        //[StringLength(30, ErrorMessage = "The field allows only 30 characters.")]
        //[Remote("IsUsernameAvailable", "CustomerUsers", HttpMethod = "POST", ErrorMessage = "This username already exists.", AdditionalFields = "__RequestVerificationToken")]
        //public string username { get; set; }

        [Display(Name = "Password")]
        public string password { get; set; }

        [Display(Name = "User Type")]
        [Required(ErrorMessage = "Please select user type.")]
        [StringLength(100, ErrorMessage = "The field allows only 100 characters.")]
        public string user_type { get; set; }
        public DateTime? created_date { get; set; }
        public DateTime? expiry_date { get; set; }

        [Display(Name = "Email Address")]
        [StringLength(50, ErrorMessage = "The field allows only 50 characters.")]
        [DataType(DataType.EmailAddress)]
        public string email_address { get; set; }

        [Display(Name = "Mobile Number")]
        [Required(ErrorMessage = "Please enter a mobile number.")]
        [Remote("IsMobileNumberAvailable", "CustomerUsers", AdditionalFields = "__RequestVerificationToken, mobile_no_clone",
            ErrorMessage = "This mobile number already exists",
            HttpMethod = "POST")]
        [RegularExpression("^[0-9]{10}$", ErrorMessage = "Please enter a valid mobile number.")]
        public string mobile_no { get; set; }

        public string f_login { get; set; }
        public int? log_att { get; set; }

        [Display(Name = "Login Time")]
        public DateTime? log_time { get; set; }

        [Display(Name = "Login Status")]
        public string log_status { get; set; }

        [Display(Name = "Posted By")]
        public string posted_by { get; set; }

        [Display(Name = "Posted Date")]
        public DateTime posted_date { get; set; }
        public string Events_IDs { get; set; }

        public virtual Customers Customer { get; set; }

        #endregion;


        #region User Roles Section

        public static string EventSuperAdminUserType { get { return "Event Super Admin"; } }
        public static string EventAdminUserType { get { return "Event Admin"; } }
        public static string NormalUserType { get { return "Normal User"; } }
        public static string SupportUserType { get { return "Support User"; } }

        #endregion

        #region Model Methods

        ///<summary>
        ///This method validates if the credentials provided which are <b>[mobile number]</b> and <b>[password]</b> do exist in the <b>[cust_users]</b> table table.
        ///If the credential have a match in the table it then return all record matching the mobile number.
        ///</summary> 
        public static List<CustomerUsers> LoginAction(string mobileNumber, string password)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var loggedInCustomer = context.cust_users
                    .Include("customer_registration")
                    .Where(c => c.mobile_no == mobileNumber && c.password == password)
                    .FirstOrDefault();

                if (loggedInCustomer != null)
                {
                    var customer = (
                        from c in context.cust_users.Include("customer_registration")
                        .Where(c => c.mobile_no == mobileNumber && !c.event_det_sno.Equals(string.Empty))
                        select new CustomerUsers
                        {
                            cust_reg_sno = c.cust_reg_sno,
                            //eventName = c.customer_registration.cust_name,
                            cust_users_sno = c.cust_users_sno,
                            user_fullname = c.user_fullname,
                            user_type = c.user_type,
                            Events_IDs = c.event_det_sno,
                        }).ToList();

                    return customer;

                }

                return null;
            }
        }

        ///<summary>
        ///This method fetchs and retrieves the records matching to <b>[event_det_sno]</b> existing to the <b>[cust_users]</b> database table.
        ///</summary>
        public static List<event_details> EventsEntitled(string mobileNumber, string encryptedPassword
            )
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var list = LoginAction(mobileNumber, encryptedPassword);

                if (list != null)
                {
                    List<long> eventIDsList = new List<long>();

                    foreach (var item in list)
                    {
                        if (GetEventIDsArray(item.Events_IDs) != null)
                        {
                            foreach (var eventID in GetEventIDsArray(item.Events_IDs))
                            {
                                if (eventID.ToString().ToLower() != "all")
                                {
                                    eventIDsList.Add(Convert.ToInt64(eventID));
                                }
                            }
                        }
                    }

                    if (eventIDsList.Count != 0)
                    {
                        var eligibleEvents = context.event_details.Where(e => eventIDsList.Contains(e.event_det_sno)).ToList();

                        if (eligibleEvents != null)
                        {
                            return eligibleEvents;
                        }
                    }
                }

                return null;
            }
        }

        ///<summary>
        ///This method returns the values from <b>[event_det_sno]</b> column within <b>[cust_users]</b> database table as an array.
        ///</summary> 
        private static string[] GetEventIDsArray(string eventIDs)
        {
            if (string.IsNullOrEmpty(eventIDs))
            {
                return null;
            }
            return eventIDs.Split(',');
        }

        public static CustomerUsers LoginAction2(string uname, string pwd)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                CustomerUsers customer = (
                    from c in context.cust_users.Include("customer_registration").Where(c => c.mobile_no == uname && c.password == pwd)
                    select new CustomerUsers
                    {
                        cust_reg_sno = c.cust_reg_sno,
                        eventName = c.customer_registration.cust_name,
                        cust_users_sno = c.cust_users_sno,
                        user_fullname = c.user_fullname,
                        user_type = c.user_type,

                    }).FirstOrDefault();

                if (customer != null)
                {
                    return customer;
                }
                return null;
            }
        }

        #endregion;

    }

    public class SupportUsers
    {
        public long SupportUser_Id { get; set; }

        [Required(ErrorMessage = "Please enter Full Name.")]
        [Display(Name = "Full Name")]
        [RegularExpression("^(([a-zA-Z]+)([',.-]?))*(([a-zA-Z]+)([',.-]?[ ]?))*[a-zA-Z]+[ ]{0,3}$", ErrorMessage = "Please enter a valid full name.")]
        [StringLength(100, ErrorMessage = "The field allows only 100 characters.")]
        public string FullName { get; set; }

        [Display(Name = "Email Address")]
        [StringLength(50, ErrorMessage = "The field allows only 50 characters.")]
        //[DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Please enter valid email address")]
        public string Email_Address { get; set; }

        [Display(Name = "Mobile Number")]
        //[Remote(
        //    "IsSupportMobileNumberAvailable", "CustomerUsers",
        //    AdditionalFields = "mobile_no_clone",
        //    ErrorMessage = "Mobile Number is already used"
        //)]
        [Required(ErrorMessage = "Please enter a Mobile Number.")]
        [RegularExpression("^[0-9]{10}$", ErrorMessage = "Please enter a valid Mobile Number.")]
        public string Mobile_Number { get; set; }

        public string Mobile_Number_Clone { get; set; }
    }

    public class CustomerEventsModel
    {
        public CustomerUsers CustomerUsers { get; set; }

        public List<Events> EventLists { get; set; }
    }
}
