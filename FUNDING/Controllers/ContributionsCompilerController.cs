/*using ECARD.DL.EDMX;
using FUNDING.Models.AppHelpers;
using FUNDING.Models.CollectionModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;


// Decompiled with JetBrains decompiler
// Type: FUNDING.Controllers.ContributionsController
// Assembly: FUNDING, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5DE1C1E8-8D80-43BD-915D-F2502219BD58
// Assembly location: D:\ECardtest - 22082024\bin\FUNDING.dll

using ECARD.DL.EDMX;
using FUNDING.Models.AppHelpers;
using FUNDING.Models.CollectionModule;
using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Web.Mvc;

#nullable disable
namespace FUNDING.Controllers
{
    public class ContributionsController : Controller
    {
        private readonly ECARDAPPEntities _dbContext = new ECARDAPPEntities();
        private readonly Contributions contribution = new Contributions();

        public ActionResult Index()
        {
            if (this.Session["admin1"] == null)
                return (ActionResult)this.RedirectToAction("CustomerLogin", "Login");
            if (this.IsEventNameNullOrEmpty())
            {
                // ISSUE: reference to a compiler-generated field
                if (ContributionsController.\u003C\u003Eo__2.\u003C\u003Ep__0 == null)
        {
                    // ISSUE: reference to a compiler-generated field
                    ContributionsController.\u003C\u003Eo__2.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, bool, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "EventNameIsNullMessage", typeof(ContributionsController), (IEnumerable<CSharpArgumentInfo>)new CSharpArgumentInfo[2]
                    {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
                    }));
                }
                // ISSUE: reference to a compiler-generated field
                // ISSUE: reference to a compiler-generated field
                object obj = ContributionsController.\u003C\u003Eo__2.\u003C\u003Ep__0.Target((CallSite)ContributionsController.\u003C\u003Eo__2.\u003C\u003Ep__0, this.ViewBag, true);
            }
            return (ActionResult)this.View();
        }

        [HttpPost]
        public JsonResult IndexDataTable()
        {
            long? event_id = this.GetEventID();
            IOrderedEnumerable <\u003C\u003Ef__AnonymousType58<long, string, string, string, string, long?, string, string> > orderedEnumerable = this._dbContext.contributor_details.Where<contributor_details>((Expression<Func<contributor_details, bool>>)(e => e.event_det_sno == event_id)).ToList<contributor_details>().Select(p => new
            {
                contribution_id = p.contri_det_sno,
                contributor_name = p.contributor_name,
                contribution_amount = Contributions.FormattedThousandsSeparator(p.contribution_amount),
                mobile_number = p.mobile_no,
                email_address = p.email_address,
                event_det_sno = p.event_det_sno,
                control_number = p.control_no,
                contribution_due_date = this.FormattedDate(p.contri_due_date)
            }).OrderByDescending(e => e.contribution_id);
            return this.Json((object)new
            {
                event_id = this.GetEventID(),
                data = orderedEnumerable
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IsMobileNumberAvailable(
          string __RequestVerificationToken,
          string Int_Mobile_number,
          string Mobile_number_clone)
        {
            string mobileNumber = Int_Mobile_number.Replace("+", "");
            if (Mobile_number_clone != null)
            {
                string str = Mobile_number_clone.Replace("+", "");
                if (mobileNumber == str)
                    return (ActionResult)this.Json((object)true);
            }
            this.GetEventAdminID();
            long? event_id = this.GetEventID();
            var data = this._dbContext.contributor_details.Where<contributor_details>((Expression<Func<contributor_details, bool>>)(u => u.event_det_sno == event_id && u.mobile_no == mobileNumber)).Select(u => new
            {
                mobileNumber = mobileNumber
            }).FirstOrDefault();
            return (ActionResult)this.Json((object)(bool)(data == null ? (true ? 1 : 0) : (false ? 1 : 0)));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRegisterPledges([Bind(Include = "Contributor_name,Contribution_amount,Int_Mobile_number,Mobile_number,Email_address")] Contributions contribution)
        {
            if (this.Session["admin1"] == null)
                return (ActionResult)this.Json((object)new
                {
                    loginStatus = false
                });
            if (this.IsEventNameNullOrEmpty())
                return (ActionResult)this.Json((object)new
                {
                    createStatus = false,
                    emptyEventNameStatus = true
                });
            this.ModelState.Remove("Number_of_people");
            if (!this.ModelState.IsValid)
                return (ActionResult)this.PartialView("_AjaxHelperCreateForm", (object)contribution);
            Contributions.SaveNewContributionRecordAndSendRespectiveSMS(contribution, this.GetEventID(), this.GetEventAdminID(), this.GetUserID());
            return (ActionResult)this.Json((object)new
            {
                createStatus = true,
                response = "Record successful created."
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Contributions/AjaxHelperSmsAllForm/{id}")]
        public ActionResult AjaxHelperSmsAllForm(long? id)
        {
            if (!id.HasValue)
                return (ActionResult)this.Json((object)new
                {
                    SmsStatus = false,
                    response = "Failed! ID not supplied"
                });
            Contributions.Send_SMS_All_Contributor_Pledges(id);
            return (ActionResult)this.Json((object)new
            {
                SmsStatus = true,
                response = "All SMS are successful Sent."
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateRegisterPledges([Bind(Include = "Contribution_id,Contributor_name,Int_Mobile_number,Mobile_number,Email_address")] Contributions contribution)
        {
            if (this.Session["admin1"] == null)
                return (ActionResult)this.Json((object)new
                {
                    loginStatus = false
                });
            if (this.IsEventNameNullOrEmpty())
                return (ActionResult)this.Json((object)new
                {
                    createStatus = false,
                    emptyEventNameStatus = true
                });
            this.ModelState.Remove("Number_of_people");
            this.ModelState.Remove("Contribution_amount");
            if (!this.ModelState.IsValid)
                return (ActionResult)this.PartialView("_AjaxHelperUpdateForm", (object)contribution);
            return Contributions.IsContributionRecordUpdated(contribution) ? (ActionResult)this.Json((object)new
            {
                updateStatus = true,
                response = "Record successful updated."
            }) : (ActionResult)this.Json((object)new
            {
                updateStatus = false,
                response = "Failed! Item does not exist."
            });
        }

        [Route("confirm-reservations")]
        public ActionResult ConfirmReservations()
        {
            return this.Session["admin1"] == null ? (ActionResult)this.RedirectToAction("CustomerLogin", "Login") : (ActionResult)this.View();
        }

        [HttpPost]
        public JsonResult ReservationsDataTable()
        {
            return this.Json((object)new
            {
                data = this.GetContributorsByContributionStatus("pending").Select(p => new
                {
                    contribution_id = p.contri_det_sno,
                    contributor_name = p.contributor_name,
                    contribution_amount = Contributions.FormattedThousandsSeparator(p.contribution_amount),
                    mobile_number = p.mobile_no,
                    control_number = p.control_no
                }).OrderByDescending(e => e.contribution_id)
            });
        }

        [HttpPost]
        public JsonResult ConfirmedReservationsDataTable()
        {
            return this.Session["admin1"] == null ? this.Json((object)new
            {
                loginStatus = false
            }) : this.Json((object)new
            {
                data = this.GetContributorsByContributionStatus("affirmed").Select(p => new
                {
                    contribution_id = p.contri_det_sno,
                    contributor_name = p.contributor_name,
                    contribution_amount = Contributions.FormattedThousandsSeparator(p.contribution_amount),
                    mobile_number = p.mobile_no,
                    control_number = p.control_no
                }).OrderByDescending(e => e.contribution_id)
            });
        }

        [HttpPost]
        public JsonResult UnsettledReservationsDataTable()
        {
            return this.Session["admin1"] == null ? this.Json((object)new
            {
                loginStatus = false
            }) : this.Json((object)new
            {
                data = this.GetContributorsByContributionStatus("unsettled").Select(p => new
                {
                    contribution_id = p.contri_det_sno,
                    contributor_name = p.contributor_name,
                    contribution_amount = Contributions.FormattedThousandsSeparator(p.contribution_amount),
                    mobile_number = p.mobile_no,
                    control_number = p.control_no
                }).OrderByDescending(e => e.contribution_id)
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageReservations([Bind(Include = "Contribution_id,Number_of_people,HasReservationRequested")] Contributions contribution)
        {
            contributor_details contributor = this._dbContext.contributor_details.Find(new object[1]
            {
        (object) contribution.Contribution_id
            });
            if (contributor == null)
                return (ActionResult)this.Json((object)new
                {
                    createStatus = false,
                    response = "Failed! Item does not exist."
                });
            this.ModelState.Remove("Contributor_name");
            this.ModelState.Remove("Contribution_amount");
            this.ModelState.Remove("Mobile_number");
            this.ModelState.Remove("Email_address");
            if (!this.ModelState.IsValid)
                return (ActionResult)this.PartialView("_AjaxHelperCreateForm", (object)contribution);
            if (!contribution.HasReservationRequested)
            {
                Contributions.RejectReservationHandler(this._dbContext, contributor);
                return (ActionResult)this.Json((object)new
                {
                    createStatus = true,
                    response = "Record successful updated, reservation has been cancelled for the contributor.",
                    reservationStatus = false
                });
            }
            Contributions.AcceptReservationHandler(this._dbContext, contributor, contribution);
            return (ActionResult)this.Json((object)new
            {
                createStatus = true,
                response = "Record successful updated and reservation has been set for the contributor.",
                reservationStatus = true
            });
        }

        [Route("Cash-Collections")]
        public ActionResult CashCollections()
        {
            return this.Session["admin1"] == null ? (ActionResult)this.RedirectToAction("CustomerLogin", "Login") : (ActionResult)this.View();
        }

        [HttpPost]
        public ActionResult FlexDataCashCollection()
        {
            long? event_id = this.GetEventID();
            return (ActionResult)this.Json((object)this._dbContext.contributor_details.Where<contributor_details>((Expression<Func<contributor_details, bool>>)(e => e.event_det_sno == event_id)).ToList<contributor_details>().Select(p => new
            {
                contribution_id = p.contri_det_sno,
                contributor_name = p.contributor_name,
                contribution_amount = Contributions.FormattedThousandsSeparator(p.contribution_amount),
                mobile_number = p.mobile_no,
                email_address = p.email_address,
                control_number = p.control_no,
                contribution_due_date = this.FormattedDate(p.contri_due_date)
            }).OrderByDescending(e => e.contribution_id), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("Cash-Collections-DataTable")]
        public JsonResult CashCollectionDataTable()
        {
            long? event_id = this.GetEventID();
            List<cash_collection> list = this._dbContext.cash_collection.Where<cash_collection>((Expression<Func<cash_collection, bool>>)(e => e.event_det_sno == event_id)).ToList<cash_collection>();
            long index = 1;
            return this.Json((object)new
            {
                data = list.Select(p => new
                {
                    id = index++,
                    contribution_id = p.contri_det_sno,
                    contributor_name = p.contributor_details.contributor_name,
                    pledged_amount = Contributions.FormattedThousandsSeparator(p.contributor_details.contribution_amount),
                    contribution_amount = Contributions.FormattedThousandsSeparator(p.amount),
                    mobile_number = p.contributor_details.mobile_no,
                    email_address = p.contributor_details.email_address,
                    remarks = p.remarks
                }).OrderByDescending(e => e.contribution_id)
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCashCollections([Bind(Include = "Contribution_id, Contribution_amount,Remarks")] CashCollection collection)
        {
            if (this.Session["admin1"] == null)
                return (ActionResult)this.Json((object)new
                {
                    loginStatus = false
                });
            if (!this.ModelState.IsValid)
                return (ActionResult)this.PartialView("_AjaxHelperCreateCashCollectionForm", (object)collection);
            contributor_details contributor = this._dbContext.contributor_details.Find(new object[1]
            {
        (object) collection.Contribution_id
            });
            if (contributor == null)
                return (ActionResult)this.Json((object)new
                {
                    createStatus = false,
                    response = "Failed, record does not exist."
                });
            this.FormulateAndSendSMS(this.SaveCashCollectionRecord(collection, contributor), new long?(collection.Contribution_id));
            return (ActionResult)this.Json((object)new
            {
                createStatus = true,
                response = "Record successful created."
            });
        }

        public void FormulateAndSendSMS(cash_collection cashCollectionDetails, long? contribution_id)
        {
            SMS_Handler.SendLocalSMS(this.GetFormulatedSmsTemplate(cashCollectionDetails, contribution_id), cashCollectionDetails.contributor_details.mobile_no);
        }

        private string GetFormulatedSmsTemplate(cash_collection cash_collection, long? contribution_id)
        {
            long cashColSno = cash_collection.cash_col_sno;
            cash_collection cashCollection = this._dbContext.cash_collection.Include("event_details").Where<cash_collection>((Expression<Func<cash_collection, bool>>)(c => c.cash_col_sno == cash_collection.cash_col_sno)).FirstOrDefault<cash_collection>();
            List<cash_collection> list = this._dbContext.cash_collection.ToList<cash_collection>();
            string contributorName = cashCollection.contributor_name;
            Decimal? amount = cashCollection.amount;
            string eventName = cashCollection.event_details.event_name;
            Decimal? contributionAmount = cashCollection.contributor_details.contribution_amount;
            Decimal? collectionAmount = this.GetTotalCashCollectionAmount(new long?(cashColSno), contribution_id, list);
            int num1 = 0;
            Decimal? nullable1 = contributionAmount;
            Decimal? nullable2 = collectionAmount;
            Decimal num2 = (Decimal)num1;
            Decimal? nullable3 = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + num2) : new Decimal?();
            Decimal? nullable4 = nullable1.HasValue & nullable3.HasValue ? new Decimal?(nullable1.GetValueOrDefault() - nullable3.GetValueOrDefault()) : new Decimal?();
            string formulatedSmsTemplate = contributorName + ", tunakushukuru kwa kuchangia Tsh " + string.Format("{0:N0}", (object)amount) + " kufanikisha " + eventName + ".";
            Decimal? nullable5 = nullable4;
            Decimal num3 = 0M;
            if (nullable5.GetValueOrDefault() > num3 & nullable5.HasValue)
                formulatedSmsTemplate = formulatedSmsTemplate + " kiasi cha Tsh " + string.Format("{0:N0}", (object)nullable4) + " kimebaki kukamilisha ahadi yako.";
            return formulatedSmsTemplate;
        }

        private Decimal? GetTotalCashCollectionAmount(
          long? cashCollection_id,
          long? contribution_id,
          List<cash_collection> cashCollectionDataList)
        {
            return cashCollectionDataList.Where<cash_collection>((Func<cash_collection, bool>)(c =>
            {
                long? contriDetSno = c.contri_det_sno;
                long? nullable1 = contribution_id;
                if (!(contriDetSno.GetValueOrDefault() == nullable1.GetValueOrDefault() & contriDetSno.HasValue == nullable1.HasValue))
                    return false;
                long cashColSno = c.cash_col_sno;
                long? nullable2 = cashCollection_id;
                long valueOrDefault = nullable2.GetValueOrDefault();
                return cashColSno <= valueOrDefault & nullable2.HasValue;
            })).Select<cash_collection, Decimal?>((Func<cash_collection, Decimal?>)(c => c.amount)).DefaultIfEmpty<Decimal?>(new Decimal?(0M)).Sum();
        }

        private Decimal? GetTotalElectronicPaymentAmount(long? payment_id, long? contribution_id)
        {
            long? nullable = Queryable.Sum(this._dbContext.payment_details.Where<payment_details>((Expression<Func<payment_details, bool>>)(c => c.contri_det_sno == contribution_id && (long?)c.payment_det_sno <= payment_id)).Select<payment_details, long?>((Expression<Func<payment_details, long?>>)(c => c.paid_amount)).DefaultIfEmpty<long?>(new long?(0L)));
            return !nullable.HasValue ? new Decimal?() : new Decimal?((Decimal)nullable.GetValueOrDefault());
        }

        private cash_collection SaveCashCollectionRecord(
          CashCollection cash_collection,
          contributor_details contributor)
        {
            cash_collection entity = new cash_collection()
            {
                contri_det_sno = new long?(contributor.contri_det_sno),
                contributor_name = contributor.contributor_name,
                event_det_sno = contributor.event_det_sno,
                amount = this.ConvertFormatedAmountToDecimalType(cash_collection.Contribution_amount),
                remarks = cash_collection.Remarks,
                posted_date = new DateTime?(DateTime.Now),
                posted_by = this.GetUserID()
            };
            this._dbContext.cash_collection.Add(entity);
            this._dbContext.SaveChanges();
            return entity;
        }

        private Decimal? ConvertFormatedAmountToDecimalType(string contribution_amount)
        {
            return new Decimal?(Convert.ToDecimal(this.StripCommaSeparators(contribution_amount)));
        }

        private string StripCommaSeparators(string contributionAmount)
        {
            return contributionAmount.Replace(",", "");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateCashCollections([Bind(Include = "Contribution_id,Contributor_name,Int_Mobile_number,Mobile_number,Email_address")] Contributions contribution)
        {
            if (this.Session["admin1"] == null)
                return (ActionResult)this.Json((object)new
                {
                    loginStatus = false
                });
            this.ModelState.Remove("Number_of_people");
            this.ModelState.Remove("Contribution_amount");
            return this.ModelState.IsValid ? (ActionResult)this.Json((object)new
            {
                updateStatus = false,
                response = "Failed! Item does not exist."
            }) : (ActionResult)this.PartialView("_AjaxHelperUpdateCashCollectionForm", (object)contribution);
        }

        [HttpGet]
        [Route("contribution-notifications")]
        public ActionResult ContributionNotificationMaster()
        {
            if (this.Session["admin1"] == null)
                return (ActionResult)this.RedirectToAction("CustomerLogin", "Login");
            con_notificaiton_master notificationDetails = this.GetNotificationDetails(this.GetEventID());
            if (notificationDetails != null)
            {
                ContributionNotification model = new ContributionNotification();
                DateTime dateTime = notificationDetails.intimate_date.Value;
                model.Formated_Intimate_Date = dateTime.ToString("dddd, dd MMMM yyyy");
                model.Intimate_Days = notificationDetails.intimate_days.Value;
                return (ActionResult)this.View("ContributionNotificationExists", (object)model);
            }
            // ISSUE: reference to a compiler-generated field
            if (ContributionsController.\u003C\u003Eo__25.\u003C\u003Ep__0 == null)
      {
                // ISSUE: reference to a compiler-generated field
                ContributionsController.\u003C\u003Eo__25.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "TodaysDate", typeof(ContributionsController), (IEnumerable<CSharpArgumentInfo>)new CSharpArgumentInfo[2]
                {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
                }));
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            object obj1 = ContributionsController.\u003C\u003Eo__25.\u003C\u003Ep__0.Target((CallSite)ContributionsController.\u003C\u003Eo__25.\u003C\u003Ep__0, this.ViewBag, this.GetTodaysDate());
            // ISSUE: reference to a compiler-generated field
            if (ContributionsController.\u003C\u003Eo__25.\u003C\u003Ep__1 == null)
      {
                // ISSUE: reference to a compiler-generated field
                ContributionsController.\u003C\u003Eo__25.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "EventDate", typeof(ContributionsController), (IEnumerable<CSharpArgumentInfo>)new CSharpArgumentInfo[2]
                {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
                }));
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            object obj2 = ContributionsController.\u003C\u003Eo__25.\u003C\u003Ep__1.Target((CallSite)ContributionsController.\u003C\u003Eo__25.\u003C\u003Ep__1, this.ViewBag, this.GetOneDayBeforeEventDate());
            return (ActionResult)this.View();
        }

        private string GetTodaysDate()
        {
            DateTime dateTime = DateTime.Now;
            dateTime = dateTime.AddMinutes(-150.0);
            return dateTime.ToString("dddd, dd MMMM yyyy");
        }

        private string GetOneDayBeforeEventDate()
        {
            return this._dbContext.event_details.Find(new object[1]
            {
        (object) this.GetEventID()
            }).event_date.Value.AddDays(-1.0).ToString("dddd, dd MMMM yyyy");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("contribution-notifications")]
        public ActionResult ContributionNotificationMaster([Bind(Include = "Intimate_Date,Intimate_Days")] ContributionNotification notification)
        {
            this.ModelState.Remove("Formated_Intimate_Date");
            if (!this.ModelState.IsValid)
                return (ActionResult)this.View((object)notification);
            this.SaveNotificationDetails(notification);
            return (ActionResult)this.RedirectToAction(nameof(ContributionNotificationMaster));
        }

        private void SaveNotificationDetails(ContributionNotification notification)
        {
            long? eventId = this.GetEventID();
            con_notificaiton_master entity = new con_notificaiton_master()
            {
                intimate_date = new DateTime?(notification.Intimate_Date),
                intimate_days = new int?(notification.Intimate_Days),
                event_det_sno = eventId,
                posted_by = this.GetUserID(),
                posted_date = new DateTime?(DateTime.Now)
            };
            if (!this.HasZeroNotification(eventId))
                return;
            this._dbContext.con_notificaiton_master.Add(entity);
            this._dbContext.SaveChanges();
        }

        private bool HasZeroNotification(long? event_id)
        {
            return this.GetNotificationDetails(event_id) == null;
        }

        private con_notificaiton_master GetNotificationDetails(long? event_id)
        {
            return this._dbContext.con_notificaiton_master.Where<con_notificaiton_master>((Expression<Func<con_notificaiton_master, bool>>)(e => e.event_det_sno == event_id)).FirstOrDefault<con_notificaiton_master>() ?? (con_notificaiton_master)null;
        }

        private List<contributor_details> GetContributorsByContributionStatus(string contributionStatus)
        {
            long? event_id = this.GetEventID();
            return this._dbContext.contributor_details.Where<contributor_details>((Expression<Func<contributor_details, bool>>)(e => e.event_det_sno == event_id && e.contri_status == contributionStatus)).ToList<contributor_details>();
        }

        private string GetUserID() => this.Session["UserID"].ToString();

        private long? GetEventAdminID() => new long?(Convert.ToInt64(this.Session["EventAdminID"]));

        private long? GetEventID() => new long?(Convert.ToInt64(this.Session["EventID"]));

        private string FormattedDate(DateTime? date)
        {
            return date.HasValue ? Convert.ToDateTime((object)date).ToString("dddd, dd MMMM yyyy") : (string)null;
        }

        private bool IsEventNameNullOrEmpty()
        {
            event_details eventDetails = this._dbContext.event_details.Find(new object[1]
            {
        (object) this.GetEventID()
            });
            return eventDetails != null && string.IsNullOrWhiteSpace(eventDetails.event_name);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                this._dbContext.Dispose();
            base.Dispose(disposing);
        }
    }
}
*/