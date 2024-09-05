using ECARD.DL.EDMX;
using FUNDING.Models.BudgetingModule;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FUNDING.Controllers
{
    public class BudgetingController : Controller
    {
        private readonly ECARDAPPEntities _dbContext = new ECARDAPPEntities();


        #region Action results

        // GET: Budgeting
        [Route("Event-Budgeting")]
        public ActionResult Index()
        {
            if (Session["admin1"] == null) return RedirectToAction("CustomerLogin", "Login");

            var event_id = GetEventID();
            ViewBag.BudgetPieChart = GetBudgetDataList(event_id).Select(b => new { b.budget_item, b.total_price });

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBudgetRecord([Bind(Include = "Item,Quantity_Formatted,Unit_Price_Formatted,Remarks")] Budget budget)
        {
            if (Session["admin1"] == null) return Json(new { loginStatus = false });

            ModelState.Remove("Quantity");
            ModelState.Remove("Unit_Price");
            ModelState.Remove("Total_Price");
            ModelState.Remove("Total_Price_Formatted");

            if (ModelState.IsValid)
            {
                var budgetItemQuantity = StripCommaSeparatorsToInt(budget.Quantity_Formatted);
                var budgetItemUnitPrice = StripCommaSeparatorsToDecimal(budget.Unit_Price_Formatted);
                var budgetItemTotalPrice = GetTotalPrice(budget);

                var budget_record = new budget_master
                {
                    budget_item = budget.Item,
                    budget_qty = budgetItemQuantity,
                    unit_price = budgetItemUnitPrice,
                    total_price = budgetItemTotalPrice,
                    event_det_sno = GetEventID(),
                    remarks = budget.Remarks,
                    posted_by = GetUserID(),
                    posted_date = DateTime.Now
                };

                _dbContext.budget_master.Add(budget_record);
                _dbContext.SaveChanges();

                return Json(new { createStatus = true, response = "Record successful created" });
            }
            return PartialView("_CreateBudgetingRecord", budget);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateBudgetRecord([Bind(Include = "Record_Id,Item,Quantity_Formatted,Unit_Price_Formatted,Remarks")] Budget budget)
        {
            if (Session["admin1"] == null) return Json(new { loginStatus = false });


            ModelState.Remove("Quantity");
            ModelState.Remove("Unit_Price");
            ModelState.Remove("Total_Price");
            ModelState.Remove("Total_Price_Formatted");

            if (ModelState.IsValid)
            {
                var budget_details = _dbContext.budget_master.Find(budget.Record_Id);

                if (budget_details != null)
                {
                    var budgetItemQuantity = StripCommaSeparatorsToInt(budget.Quantity_Formatted);
                    var budgetItemUnitPrice = StripCommaSeparatorsToDecimal(budget.Unit_Price_Formatted);
                    var budgetItemTotalPrice = GetTotalPrice(budget);

                    budget_details.budget_item = budget.Item;
                    budget_details.budget_qty = budgetItemQuantity;
                    budget_details.unit_price = budgetItemUnitPrice;
                    budget_details.total_price = budgetItemTotalPrice;
                    budget_details.posted_by = GetUserID();
                    budget_details.posted_date = DateTime.Now;
                    budget_details.remarks = budget.Remarks;

                    _dbContext.Entry(budget_details).State = EntityState.Modified;
                    _dbContext.SaveChanges();

                    return Json(new { updateStatus = true, response = "Record successful Updated" });
                }

                return Json(new { updateStatus = false, response = "Failed! Record does not exist" });
            }
            return PartialView("_UpdateBudgetingRecord", budget);
        }

        [HttpPost]
        public JsonResult BudgetingDatatableIndex()
        {
            var event_id = GetEventID();
            var budgetDataList = GetBudgetDataList(event_id);

            var index = 1;
            decimal? budgetGrandTotal = budgetDataList.Select(b => b.total_price).DefaultIfEmpty(0).Sum();

            var budgetValues = budgetDataList
                .Select(b => new
                {
                    id = index++,
                    Record_Id = b.budget_mas_sno,
                    Item = b.budget_item,
                    Quantity = b.budget_qty,
                    Unit_Price = b.unit_price,
                    Total_Price = b.total_price,
                    Budget_Percentage = GetBudgetPercentage(b.total_price, budgetGrandTotal),
                    Remarks = b.remarks
                });

            return Json(new { data = budgetValues });
        }


        [Route("Event-Expenditures")]
        public ActionResult Expenditures()
        {
            if (Session["admin1"] == null) return RedirectToAction("CustomerLogin", "Login");


            var event_id = GetEventID();
            var expenditureDataList = GetExpenditureDataList(event_id);

            #region Budgeting details for various response on expenditure item change

            ViewBag.BudgetingDetails = GetBudgetDataList(event_id)
                .Select(b => new
                {
                    id = b.budget_mas_sno,
                    quantity = b.budget_qty,
                    b.total_price
                });

            #endregion;

            #region Expenditure Chart Parameters

            ViewBag.ExpenditureChart = GetBudgetDataList(event_id)
                .OrderBy(b => b.budget_mas_sno)
                .Select(bi => new
                {
                    item = bi.budget_item,
                    budget_total = bi.total_price,
                    expenditure_total = GetExpenditureItemTotal(bi, expenditureDataList)
                });

            #endregion;

            #region Expenditure budgeting list

            ViewBag.BudgetItem = _dbContext.budget_master.Where(b => b.event_det_sno == event_id)
                .Select(b => new SelectListItem
                {
                    Value = b.budget_mas_sno.ToString(),
                    Text = b.budget_item
                });

            #endregion;

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateExpenditureRecord([Bind(Include = "Budget_Record_Id, Quantity_Formatted, Unit_Price_Formatted, Remarks, Service_Provider, Int_Mobile_number")]
        Expenditures expenditure)
        {
            ModelState.Remove("Item");
            ModelState.Remove("Quantity");
            ModelState.Remove("Unit_Price");
            ModelState.Remove("Total_Price");
            ModelState.Remove("Total_Price_Formatted");
            ModelState.Remove("SPC_Mobile_number");
            ModelState.Remove("SPU_Mobile_number");

            if (ModelState.IsValid)
            {
                var expenditureItemQuantity = StripCommaSeparatorsToInt(expenditure.Quantity_Formatted);
                var expenditureItemUnitPrice = StripCommaSeparatorsToDecimal(expenditure.Unit_Price_Formatted);
                var expenditureItemTotalPrice = expenditureItemUnitPrice * expenditureItemQuantity;

                var event_id = GetEventID();
                var budget_id = expenditure.Budget_Record_Id;

                budget_master budgetRecord = GetBudgetRecordByBudgetId(budget_id, event_id);

                if (budgetRecord != null)
                {
                    var expenditure_record = new expenditure_details
                    {
                        event_det_sno = event_id,
                        budget_mas_sno = budget_id,
                        exp_item = budgetRecord.budget_item,
                        exp_qty = expenditureItemQuantity,
                        unit_price = expenditureItemUnitPrice,
                        total_price = expenditureItemTotalPrice,
                        name_of_service_provider = expenditure.Service_Provider,
                        sp_contact_details = expenditure.Int_Mobile_number,
                        remarks = expenditure.Remarks,
                        posted_by = GetUserID(),
                        posted_date = DateTime.Now
                    };

                    _dbContext.expenditure_details.Add(expenditure_record);
                    _dbContext.SaveChanges();

                    return Json(new { createStatus = true, response = "Record successful created" });
                }
                return Json(new { createStatus = false, response = "Failed, budget record does not exist." });
            }
            return PartialView("_CreateBudgetingRecord", expenditure);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateExpenditureRecord([Bind(Include = "Record_Id, Budget_Record_Id, Quantity_Formatted, Unit_Price_Formatted, Remarks, Service_Provider, Int_Mobile_number")] Expenditures expenditure)
        {
            ModelState.Remove("Item");
            ModelState.Remove("Quantity");
            ModelState.Remove("Unit_Price");
            ModelState.Remove("Total_Price");
            ModelState.Remove("Total_Price_Formatted");
            ModelState.Remove("SPC_Mobile_number");
            ModelState.Remove("SPU_Mobile_number");

            if (ModelState.IsValid)
            {
                var event_id = GetEventID();
                var budget_id = expenditure.Budget_Record_Id;
                var budgetRecord = GetBudgetRecordByBudgetId(budget_id, event_id);
                var expenditureRecord = GetExpenditureRecordByExpenditureId(expenditure, event_id);

                if (budgetRecord != null && expenditureRecord != null)
                {
                    var expenditureItemQuantity = StripCommaSeparatorsToInt(expenditure.Quantity_Formatted);
                    var expenditureItemUnitPrice = StripCommaSeparatorsToDecimal(expenditure.Unit_Price_Formatted);
                    var expenditureItemTotalPrice = expenditureItemUnitPrice * expenditureItemQuantity;

                    expenditureRecord.budget_mas_sno = budget_id;
                    expenditureRecord.exp_item = budgetRecord.budget_item;
                    expenditureRecord.exp_qty = expenditureItemQuantity;
                    expenditureRecord.unit_price = expenditureItemUnitPrice;
                    expenditureRecord.total_price = expenditureItemTotalPrice;
                    expenditureRecord.posted_by = GetUserID();
                    expenditureRecord.remarks = expenditure.Remarks;
                    expenditureRecord.name_of_service_provider = expenditure.Service_Provider;
                    expenditureRecord.sp_contact_details = expenditure.Int_Mobile_number;
                    expenditureRecord.posted_date = DateTime.Now;

                    _dbContext.Entry(expenditureRecord).State = EntityState.Modified;
                    _dbContext.Entry(expenditureRecord).Property(ex => ex.event_det_sno).IsModified = false;
                    _dbContext.SaveChanges();

                    return Json(new { updateStatus = true, response = "Record successful updated" });
                }
                return Json(new { updateStatus = false, response = "Failed, record does not exist" });

            }
            return PartialView("_UpdateBudgetingRecord", expenditure);
        }


        [HttpPost]
        [Route("Is-Budget-Item-Available")]
        public ActionResult IsBudgetItemAvailable(string Item, string Item_clone)
        {
            if (Item_clone != null)
            {
                //Updating to the same name. Available for use.
                if (Item.Trim() == Item_clone.Trim()) return Json(true);
            }

            var eventID = GetEventID();

            var isBudgetItemAvailable = (from u in _dbContext.budget_master
                                         where u.event_det_sno == eventID && u.budget_item.ToUpper() == Item.ToUpper()
                                         select new { Item }).FirstOrDefault();


            // 1. if Budget-Item exists, not available for use. budgetItemAvailabilityStatus == false;
            // 2. if Budget-Item does not exists, available for use. budgetItemAvailabilityStatus == true;

            bool budgetItemAvailabilityStatus = isBudgetItemAvailable == null;

            //return Json(budgetItemAvailabilityStatus);
            return Json(budgetItemAvailabilityStatus, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult ExpendituresDatatableIndex()
        {
            var event_id = GetEventID();
            var expenditureDataList = GetExpenditureDataList(event_id);

            var index = 1;
            var expendituresVsBudget = expenditureDataList
                .OrderBy(ex => ex.budget_mas_sno)
                .Select(ex => new
                {
                    id = index++,
                    Record_Id = ex.exp_det_sno,
                    Item = ex.exp_item,
                    Quantity = ex.exp_qty,
                    Unit_Price = ex.unit_price,
                    Total_Price = ex.total_price,
                    Variation_From_Budget = ExpenditureVsBudgetTotalPriceVariation(expenditureDataList, ex),
                    Remarks = ex.remarks,
                    Status = ExpenditureVariationStatus(expenditureDataList, ex),
                    Service_Provider = ex.name_of_service_provider,
                    SP_Mobile_Number = ex.sp_contact_details,
                    Budgeted_Quantity = ex.budget_master.budget_qty,
                    Budgeted_Total = ex.budget_master.total_price,
                }).ToList();

            return Json(new { data = expendituresVsBudget });
        }

        #endregion;


        #region Data Layer Region



        #region Unit conversion and their operations (Quantity, Unit Price & Total Price)

        private int? StripCommaSeparatorsToInt(string formated_amount)
        {
            var unformated_amount = formated_amount.Replace(",", "");

            return Convert.ToInt32(unformated_amount);
        }

        private decimal? StripCommaSeparatorsToDecimal(string formated_amount)
        {
            var unformated_amount = formated_amount.Replace(",", "");

            return Convert.ToDecimal(unformated_amount);
        }

        private decimal? GetTotalPrice(Budget budget)
        {
            var quantity = StripCommaSeparatorsToInt(budget.Quantity_Formatted);
            var unit_price = StripCommaSeparatorsToDecimal(budget.Unit_Price_Formatted);

            return quantity * unit_price;
        }

        #endregion;

        private static decimal? GetExpenditureItemTotal(budget_master budget_record, List<expenditure_details> expenditureDataList)
        {
            return expenditureDataList
                                .Where(ex => ex.budget_mas_sno == budget_record.budget_mas_sno)
                                .Select(ex => ex.total_price).DefaultIfEmpty(0).Sum();
        }

        private expenditure_details GetExpenditureRecordByExpenditureId(Expenditures expenditure, long? event_id)
        {
            return GetExpenditureDataList(event_id).Where(ex => ex.exp_det_sno == expenditure.Record_Id).FirstOrDefault();
        }

        private budget_master GetBudgetRecordByBudgetId(long? budget_id, long? event_id)
        {
            return GetBudgetDataList(event_id).Where(b => b.budget_mas_sno == budget_id).FirstOrDefault();
        }

        private List<budget_master> GetBudgetDataList(long? event_id)
        {
            return _dbContext.budget_master.Where(b => b.event_det_sno == event_id).ToList();
        }

        private List<expenditure_details> GetExpenditureDataList(long? event_id)
        {
            return _dbContext.expenditure_details.Where(b => b.event_det_sno == event_id).ToList();
        }

        private string GetBudgetPercentage(decimal? total_price, decimal? budgetGrandTotal)
        {
            var totalPriceRatio = (decimal)(total_price / budgetGrandTotal);
            return totalPriceRatio.ToString("P");
        }

        private decimal? ExpenditureVsBudgetTotalPriceVariation(List<expenditure_details> expenditureDataList, expenditure_details exp)
        {
            var budget_total = exp.budget_master.total_price;
            var expenditure_total = GetTotalExpenditurePerItem(expenditureDataList, exp.budget_mas_sno, exp.exp_det_sno);

            return budget_total - expenditure_total;
        }

        private decimal? GetTotalExpenditurePerItem(List<expenditure_details> expenditureDataList, long? budget_id, long? expenditure_id)
        {
            return expenditureDataList.Where(e => e.budget_mas_sno == budget_id && e.exp_det_sno <= expenditure_id)
                .Select(c => c.total_price).DefaultIfEmpty(0).Sum();
        }

        private string ExpenditureVariationStatus(List<expenditure_details> expenditureDataList, expenditure_details exp)
        {
            var variation = ExpenditureVsBudgetTotalPriceVariation(expenditureDataList, exp);

            if (variation == 0)
            {
                return "<i class='fas fa-2x fa-arrows-alt-h text-info'></i>";
            }
            else if (variation > 0)
            {
                return "<i class='fas fa-2x fa-arrow-alt-circle-down text-success'></i>";
            }
            else
            {
                return "<i class='fas fa-2x fa-arrow-alt-circle-up text-danger'></i>";
            }
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
    }

}