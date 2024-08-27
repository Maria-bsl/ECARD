using ECARD.DL.EDMX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FUNDING.Controllers
{
    public class CustomerActivityController : Controller
    {
        private readonly ECARDAPPEntities _dbContext = new ECARDAPPEntities();


        // GET: CustomerActivity
        [Route("Dashboard")]
        public ActionResult Dashboard()
        {
            if (Session["admin1"] == null) return RedirectToAction("CustomerLogin", "Login");

            var event_id = GetEventID();
            var expenditureDataList = GetExpendituresDataList(event_id);
            var inviteesDetailsList = GetEventVisitorsList(event_id);
            var cardSizeValues = GetCardSizeList(inviteesDetailsList);

            #region Counter ViewBags

            ViewBag.TotalVisitors = inviteesDetailsList.Select(v => v.no_of_persons).DefaultIfEmpty(0).Sum();

            ViewBag.TotalContributions = GetTotalContributions(event_id);

            ViewBag.TotalExpenditures = expenditureDataList.Select(e => e.total_price).DefaultIfEmpty(0).Sum();

            #endregion;

            #region Invitees Card Size labels

            var cards = GetVerifiedInviteesVisitorId(event_id);

            ViewBag.cardSizeInviteesVerificationData = inviteesDetailsList
                .OrderBy(iv => iv.no_of_persons)
                .GroupBy(group => group.no_of_persons)
                .Select(group => new
                {
                    card_size = GetFormatedCardSize(group.Key),
                    total_invitees = group.Select(c => c.no_of_persons).DefaultIfEmpty(0).Sum(),
                    total_verified_invitees = cards.Where(vi => vi.Invitee_Id == group.Select(v => v.visitor_det_sno).FirstOrDefault())
                    .Select(c => c.Total_Verified_Invitees).DefaultIfEmpty(0).Sum()
                }).ToList();

            #endregion;

            #region Expenditure Chart ViewBag

            ViewBag.BudgetDetailsForExpenditureChart = GetBudgetDataList(event_id)
                .OrderBy(b => b.budget_mas_sno)
                .Select(bi => new
                {
                    item = bi.budget_item,
                    budget_total = bi.total_price,
                    expenditure_total = GetExpenditureItemTotal(bi, expenditureDataList)
                });

            #endregion;

            return View();
        }

        private static decimal? GetExpenditureItemTotal(budget_master budget_record, List<expenditure_details> expenditureDataList)
        {
            return expenditureDataList
                                .Where(ex => ex.budget_mas_sno == budget_record.budget_mas_sno)
                                .Select(ex => ex.total_price).DefaultIfEmpty(0).Sum();
        }


        #region Helper Methods

        private string GetFormatedCardSize(int? card_size)
        {
            if (card_size == 1) return $"{card_size} person";

            return $"{card_size} people";
        }

        private List<budget_master> GetBudgetDataList(long? event_id)
        {
            return _dbContext.budget_master.Where(b => b.event_det_sno == event_id).ToList();
        }


        private int GetVerifiedInviteesSummationById(IGrouping<int?, visitor_details> group, List<VerifiedInvitees> cards)
        {
            return cards.Where(vi => vi.Invitee_Id == group.Select(v => v.visitor_det_sno).FirstOrDefault())
                                .Select(c => c.Total_Verified_Invitees).DefaultIfEmpty(0).Sum();
        }

        private List<VerifiedInvitees> GetVerifiedInviteesVisitorId(long? event_id)
        {
            var verifiedInvitees = _dbContext.qr_verify_details.Where(ev => ev.event_det_sno == event_id)
                .GroupBy(group => group.visitor_det_sno)
                .Select(group => new VerifiedInvitees
                {
                    Invitee_Id = (long)group.Key,
                    Total_Verified_Invitees = (int)group.Select(c => c.no_of_persons).DefaultIfEmpty(0).Sum()
                }).ToList();

            return verifiedInvitees;
        }


        private List<expenditure_details> GetExpendituresDataList(long? event_id)
        {
            return _dbContext.expenditure_details
                .Where(e => e.event_det_sno == event_id)
                .ToList();
        }

        private List<visitor_details> GetEventVisitorsList(long? event_id)
        {
            return _dbContext.visitor_details
                .Where(e => e.event_det_sno == event_id)
                .ToList();
        }

        private List<int?> GetCardSizeList(List<visitor_details> vistorDetailsList)
        {
            return vistorDetailsList
                .OrderBy(m => m.no_of_persons)
                .Select(m => m.no_of_persons)
                .Distinct()
                .ToList();
        }

        private decimal? GetTotalContributions(long? event_id)
        {
            return _dbContext.contributor_details.Where(v => v.event_det_sno == event_id).Select(v => v.contribution_amount).DefaultIfEmpty(0).Sum();
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

        #endregion;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
            base.Dispose(disposing);
        }


        class VerifiedInvitees
        {
            public long Invitee_Id { get; set; }
            public int Total_Verified_Invitees { get; set; }
        }
    }


}