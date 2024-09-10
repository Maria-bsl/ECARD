/*using ECARD.DL.EDMX;
using FUNDING.Models.AppHelpers;
using FUNDING.Models.CollectionModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace FUNDING.Controllers
{
    public class ContributionsController : Controller
    {
        private readonly ECARDAPPEntities _dbContext = new ECARDAPPEntities();
        private readonly Contributions contribution = new Contributions();

        // GET: Contributions
        #region Actions Results

        public ActionResult Index()
        {
            if (Session["admin1"] == null) return RedirectToAction("CustomerLogin", "Login");

            if (IsEventNameNullOrEmpty())
            {
                ViewBag.EventNameIsNullMessage = true;
            }
            return View();
        }

        [HttpPost]
        public JsonResult IndexDataTable()
        {
            var event_id = GetEventID();
            var pledgingDetails = _dbContext.contributor_details.Where(e => e.event_det_sno == event_id).ToList();

            var formatedPledingDetails = pledgingDetails
                .Select(p => new
                {
                    contribution_id = p.contri_det_sno,
                    p.contributor_name,
                    contribution_amount = Contributions.FormattedThousandsSeparator(p.contribution_amount),
                    mobile_number = p.mobile_no,
                    p.email_address,
                    control_number = p.control_no,
                    contribution_due_date = FormattedDate(p.contri_due_date)
                }).OrderByDescending(e => e.contribution_id);

            return Json(new { event_id = GetEventID(), data = formatedPledingDetails });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IsMobileNumberAvailable(string __RequestVerificationToken, string Int_Mobile_number, string Mobile_number_clone)
        {
            // if (Session["admin1"] == null) return Json(new { loginStatus = false });

            string mobileNumber = Int_Mobile_number.Replace("+", "");

            if (Mobile_number_clone != null)
            {
                var mobileNumber_Clone = Mobile_number_clone.Replace("+", "");
                if (mobileNumber == mobileNumber_Clone)
                {
                    //Updating to the same name. Available for use.
                    return Json(true);
                }
            }

            var eventAdminID = GetEventAdminID();
            var event_id = GetEventID();

            var isMobileAvailable = (from u in _dbContext.contributor_details
                                     where u.event_det_sno == event_id && u.mobile_no == mobileNumber
                                     select new { mobileNumber }).FirstOrDefault();

            bool availabilityStatus;
            if (isMobileAvailable != null)
            {
                //Username exists. Not available for use.
                availabilityStatus = false;
            }
            else
            {
                //Username does not exists. Available for use.
                availabilityStatus = true;
            }

            return Json(availabilityStatus);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRegisterPledges([Bind(Include = "Contributor_name,Contribution_amount,Int_Mobile_number,Mobile_number,Email_address")] Contributions contribution)
        {
            if (Session["admin1"] == null) return Json(new { loginStatus = false });

            if (IsEventNameNullOrEmpty())
            {
                return Json(new { createStatus = false, emptyEventNameStatus = true });
            }

            ModelState.Remove("Number_of_people");

            if (ModelState.IsValid)
            {
                Contributions.SaveNewContributionRecordAndSendRespectiveSMS(contribution, GetEventID(), GetEventAdminID(), GetUserID());

                return Json(new { createStatus = true, response = "Record successful created." });

            }

            return PartialView("_AjaxHelperCreateForm", contribution);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateRegisterPledges([Bind(Include = "Contribution_id,Contributor_name,Int_Mobile_number,Mobile_number,Email_address")] Contributions contribution)
        {
            if (Session["admin1"] == null) return Json(new { loginStatus = false });

            if (IsEventNameNullOrEmpty())
            {
                return Json(new { createStatus = false, emptyEventNameStatus = true });
            }

            ModelState.Remove("Number_of_people");
            ModelState.Remove("Contribution_amount");

            if (ModelState.IsValid)
            {
                if (Contributions.IsContributionRecordUpdated(contribution))
                {
                    return Json(new { updateStatus = true, response = "Record successful updated." });
                }

                return Json(new { updateStatus = false, response = "Failed! Item does not exist." });

            }
            return PartialView("_AjaxHelperUpdateForm", contribution);

        }

        [Route("confirm-reservations")]
        public ActionResult ConfirmReservations()
        {
            if (Session["admin1"] == null) return RedirectToAction("CustomerLogin", "Login");

            return View();
        }

        [HttpPost]
        public JsonResult ReservationsDataTable()
        {
            var pledgingDetails = GetContributorsByContributionStatus(Contributions.PENDING_STATUS);

            var formatedPledingDetails = pledgingDetails
                .Select(p => new
                {
                    contribution_id = p.contri_det_sno,
                    p.contributor_name,
                    contribution_amount = Contributions.FormattedThousandsSeparator(p.contribution_amount),
                    mobile_number = p.mobile_no,
                    control_number = p.control_no,
                }).OrderByDescending(e => e.contribution_id);

            return Json(new { data = formatedPledingDetails });
        }

        [HttpPost]
        public JsonResult ConfirmedReservationsDataTable()
        {
            if (Session["admin1"] == null) return Json(new { loginStatus = false });

            var confirmedReservationDetails = GetContributorsByContributionStatus(Contributions.AFFIRMATIVE_STATUS);

            var formatedPledingDetails = confirmedReservationDetails
                .Select(p => new
                {
                    contribution_id = p.contri_det_sno,
                    p.contributor_name,
                    contribution_amount = Contributions.FormattedThousandsSeparator(p.contribution_amount),
                    mobile_number = p.mobile_no,
                    control_number = p.control_no,
                }).OrderByDescending(e => e.contribution_id);

            return Json(new { data = formatedPledingDetails });
        }

        [HttpPost]
        public JsonResult UnsettledReservationsDataTable()
        {
            if (Session["admin1"] == null) return Json(new { loginStatus = false });

            var unsettledReservationDetails = GetContributorsByContributionStatus(Contributions.UNSETTLED_STATUS);

            var formatedPledingDetails = unsettledReservationDetails
                .Select(p => new
                {
                    contribution_id = p.contri_det_sno,
                    p.contributor_name,
                    contribution_amount = Contributions.FormattedThousandsSeparator(p.contribution_amount),
                    mobile_number = p.mobile_no,
                    control_number = p.control_no,
                }).OrderByDescending(e => e.contribution_id);

            return Json(new { data = formatedPledingDetails });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageReservations([Bind(Include = "Contribution_id,Number_of_people,HasReservationRequested")] Contributions contribution)
        {
            //if (Session["admin1"] == null) return Json(new { loginStatus = false });

            var contributor = _dbContext.contributor_details.Find(contribution.Contribution_id);

            if (contributor != null)
            {
                ModelState.Remove("Contributor_name");
                ModelState.Remove("Contribution_amount");
                ModelState.Remove("Mobile_number");
                ModelState.Remove("Email_address");

                if (ModelState.IsValid)
                {
                    if (contribution.HasReservationRequested == false)
                    {
                        Contributions.RejectReservationHandler(_dbContext, contributor);

                        return Json(new { createStatus = true, response = "Record successful updated, reservation has been cancelled for the contributor.", reservationStatus = false });
                    }

                    Contributions.AcceptReservationHandler(_dbContext, contributor, contribution);

                    return Json(new { createStatus = true, response = "Record successful updated and reservation has been set for the contributor.", reservationStatus = true });
                }

                return PartialView("_AjaxHelperCreateForm", contribution);

            }
            return Json(new { createStatus = false, response = "Failed! Item does not exist." });
        }

        [Route("Cash-Collections")]
        public ActionResult CashCollections()
        {
            if (Session["admin1"] == null) return RedirectToAction("CustomerLogin", "Login");

            return View();
        }

        [HttpPost]
        public ActionResult FlexDataCashCollection()
        {
            var event_id = GetEventID();
            var pledgingDetails = _dbContext.contributor_details.Where(e => e.event_det_sno == event_id).ToList();

            var formatedPledingDetails = pledgingDetails
                .Select(p => new
                {
                    contribution_id = p.contri_det_sno,
                    p.contributor_name,
                    contribution_amount = Contributions.FormattedThousandsSeparator(p.contribution_amount),
                    mobile_number = p.mobile_no,
                    p.email_address,
                    control_number = p.control_no,
                    contribution_due_date = FormattedDate(p.contri_due_date)
                }).OrderByDescending(e => e.contribution_id);

            return Json(formatedPledingDetails, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("Cash-Collections-DataTable")]
        public JsonResult CashCollectionDataTable()
        {
            var event_id = GetEventID();
            var pledgingDetails = _dbContext.cash_collection.Where(e => e.event_det_sno == event_id).ToList();

            long index = 1;
            var formatedPledingDetails = pledgingDetails
                .Select(p => new
                {
                    id = index++,
                    contribution_id = p.contri_det_sno,
                    p.contributor_details.contributor_name,
                    pledged_amount = Contributions.FormattedThousandsSeparator(p.contributor_details.contribution_amount),
                    contribution_amount = Contributions.FormattedThousandsSeparator(p.amount),
                    mobile_number = p.contributor_details.mobile_no,
                    p.contributor_details.email_address,
                    p.remarks
                }).OrderByDescending(e => e.contribution_id);

            return Json(new { data = formatedPledingDetails });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCashCollections([Bind(Include = "Contribution_id, Contribution_amount,Remarks")] CashCollection collection)
        {
            if (Session["admin1"] == null) return Json(new { loginStatus = false });

            if (ModelState.IsValid)
            {
                var contributor = _dbContext.contributor_details.Find(collection.Contribution_id);

                if (contributor != null)
                {
                    var cashCollectionDetails = SaveCashCollectionRecord(collection, contributor);

                    FormulateAndSendSMS(cashCollectionDetails, collection.Contribution_id);

                    return Json(new { createStatus = true, response = "Record successful created." });
                }
                return Json(new { createStatus = false, response = "Failed, record does not exist." });
            }

            return PartialView("_AjaxHelperCreateCashCollectionForm", collection);
        }

        #region Cash Collection Management

        public void FormulateAndSendSMS(cash_collection cashCollectionDetails, long? contribution_id)
        {
            string sms_template = GetFormulatedSmsTemplate(cashCollectionDetails, contribution_id);

            var mobile_number = cashCollectionDetails.contributor_details.mobile_no;

            SMS_Handler.SendLocalSMS(sms_template, mobile_number);
        }


        private string GetFormulatedSmsTemplate(cash_collection cash_collection, long? contribution_id)
        {
            #region Data processing section

            var cashCollection_id = cash_collection.cash_col_sno;

            var cashCollectionDetails = _dbContext.cash_collection.Include("event_details")
                .Where(c => c.cash_col_sno == cash_collection.cash_col_sno).FirstOrDefault();

            var cashCollectionDataList = _dbContext.cash_collection.ToList();

            var contributor_name = cashCollectionDetails.contributor_name;
            var amount = cashCollectionDetails.amount;
            var event_name = cashCollectionDetails.event_details.event_name;
            var pledged_amount = cashCollectionDetails.contributor_details.contribution_amount;

            decimal? total_cashCollection = GetTotalCashCollectionAmount(cashCollection_id, contribution_id, cashCollectionDataList);

            var total_OtherChannelsCollection = 0;

            var balance_amount = pledged_amount - (total_cashCollection + total_OtherChannelsCollection);

            #endregion;

            #region SMS template formulation section

            string sms_template = $"{contributor_name}, tunakushukuru kwa kuchangia Tsh {string.Format("{0:N0}", amount)} kufanikisha {event_name}.";

            if (balance_amount > 0)
            {
                sms_template += $" kiasi cha Tsh {string.Format("{0:N0}", balance_amount)} kimebaki kukamilisha ahadi yako.";
            }

            #endregion;

            return sms_template;
        }

        private decimal? GetTotalCashCollectionAmount(long? cashCollection_id, long? contribution_id, List<cash_collection> cashCollectionDataList)
        {
            return cashCollectionDataList
                .Where(c => c.contri_det_sno == contribution_id && c.cash_col_sno <= cashCollection_id)
                .Select(c => c.amount).DefaultIfEmpty(0).Sum();
        }

        private decimal? GetTotalElectronicPaymentAmount(long? payment_id, long? contribution_id)
        {
            return _dbContext.payment_details
                .Where(c => c.contri_det_sno == contribution_id && c.payment_det_sno <= payment_id)
                .Select(c => c.paid_amount).DefaultIfEmpty(0).Sum();
        }


        private cash_collection SaveCashCollectionRecord(CashCollection cash_collection, contributor_details contributor)
        {
            var cashCollectionDetails = new cash_collection
            {
                contri_det_sno = contributor.contri_det_sno,
                contributor_name = contributor.contributor_name,
                event_det_sno = contributor.event_det_sno,
                amount = ConvertFormatedAmountToDecimalType(cash_collection.Contribution_amount),
                remarks = cash_collection.Remarks,
                posted_date = DateTime.Now,
                posted_by = GetUserID()
            };

            _dbContext.cash_collection.Add(cashCollectionDetails);
            _dbContext.SaveChanges();

            return cashCollectionDetails;
        }

        private decimal? ConvertFormatedAmountToDecimalType(string contribution_amount)
        {
            var contributionAmount = StripCommaSeparators(contribution_amount);

            return Convert.ToDecimal(contributionAmount);
        }

        private string StripCommaSeparators(string contributionAmount)
        {
            return contributionAmount.Replace(",", "");
        }

        #endregion;

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateCashCollections([Bind(Include = "Contribution_id,Contributor_name,Int_Mobile_number,Mobile_number,Email_address")] Contributions contribution)
        {
            if (Session["admin1"] == null) return Json(new { loginStatus = false });

            ModelState.Remove("Number_of_people");
            ModelState.Remove("Contribution_amount");

            if (ModelState.IsValid)
            {
                //TODO: Updating cash collection details

                //if (Contributions.IsContributionRecordUpdated(contribution))
                //{
                //    return Json(new { updateStatus = true, response = "Record successful updated." });
                //}

                return Json(new { updateStatus = false, response = "Failed! Item does not exist." });

            }
            return PartialView("_AjaxHelperUpdateCashCollectionForm", contribution);

        }

        [HttpGet]
        [Route("contribution-notifications")]
        public ActionResult ContributionNotificationMaster()
        {
            if (Session["admin1"] == null) return RedirectToAction("CustomerLogin", "Login");

            var event_id = GetEventID();

            var notification_contributions = GetNotificationDetails(event_id);

            if (notification_contributions != null)
            {
                var notificationModel = new ContributionNotification();

                var intimateDate = (DateTime)(notification_contributions.intimate_date);
                notificationModel.Formated_Intimate_Date = intimateDate.ToString("dddd, dd MMMM yyyy");
                notificationModel.Intimate_Days = (int)notification_contributions.intimate_days;


                return View("ContributionNotificationExists", notificationModel);
            }


            ViewBag.TodaysDate = GetTodaysDate();

            ViewBag.EventDate = GetOneDayBeforeEventDate();

            return View();
        }

        private string GetTodaysDate()
        {
            return DateTime.Now.AddMinutes(-150).ToString("dddd, dd MMMM yyyy");
        }

        private string GetOneDayBeforeEventDate()
        {
            var event_id = GetEventID();

            var event_date = (DateTime)_dbContext.event_details.Find(event_id).event_date;

            return event_date.AddDays(-1).ToString("dddd, dd MMMM yyyy");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("contribution-notifications")]
        public ActionResult ContributionNotificationMaster([Bind(Include = "Intimate_Date,Intimate_Days")] ContributionNotification notification)
        {
            ModelState.Remove("Formated_Intimate_Date");

            if (ModelState.IsValid)
            {
                SaveNotificationDetails(notification);

                return RedirectToAction("ContributionNotificationMaster");
            }

            return View(notification);
        }

        private void SaveNotificationDetails(ContributionNotification notification)
        {
            var event_id = GetEventID();

            var contribution_notification = new con_notificaiton_master
            {
                intimate_date = notification.Intimate_Date,
                intimate_days = notification.Intimate_Days,
                event_det_sno = event_id,
                posted_by = GetUserID(),
                posted_date = DateTime.Now
            };

            if (HasZeroNotification(event_id))
            {
                _dbContext.con_notificaiton_master.Add(contribution_notification);
                _dbContext.SaveChanges();
            }
        }

        private bool HasZeroNotification(long? event_id)
        {
            var existingContributionNotification = GetNotificationDetails(event_id);

            return existingContributionNotification == null;
        }

        private con_notificaiton_master GetNotificationDetails(long? event_id)
        {
            var contribution_notification = _dbContext.con_notificaiton_master
                 .Where(e => e.event_det_sno == event_id).FirstOrDefault();

            return (contribution_notification != null) ? contribution_notification : null;
        }


        #endregion;

        private List<contributor_details> GetContributorsByContributionStatus(string contributionStatus)
        {
            var event_id = GetEventID();

            return _dbContext.contributor_details
                .Where(e => e.event_det_sno == event_id && e.contri_status == contributionStatus).ToList();
        }

        private string GetUserID()
        {
            return Session["UserID"].ToString();
        }

        private long? GetEventAdminID()
        {
            return Convert.ToInt64(Session["EventAdminID"]);
        }

        private long? GetEventID()
        {
            return Convert.ToInt64(Session["EventID"]);
        }

        private string FormattedDate(DateTime? date)
        {
            return (date == null) ? null : Convert.ToDateTime(date).ToString("dddd, dd MMMM yyyy");
        }

        private bool IsEventNameNullOrEmpty()
        {
            var eventDetails = _dbContext.event_details.Find(GetEventID());

            return (eventDetails != null) ? string.IsNullOrWhiteSpace(eventDetails.event_name) : false;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}*/