using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FUNDING.BL.BusinessEntities.Masters;


namespace FUNDING.Controllers
{
    public class BaseController : Controller
    {
        Arights access = new Arights();
        Arights act = new Arights();
        Language_Faci lang = new Language_Faci();
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                String[] list = new String[34] { "Sponsor", "Board","Budgeting", "Teams", "Disbursement to Board",
                        "Disbursement to Team",
                        "Expense Categories", "Expenses", "Sponsor Details", "Board Details",
                        "Team Expenses", "Board Balance Amount", "Team Wise Balance","Country","Region","District","Ward",
                        "Security Question","Users","Currency","Designation","Email","SMTP","Language","AccessRights","AuditTrails",
                 "Season" ,"Team Details","Disbursement to Board Details","Disbursement to Team Details",
                "SMS Settings","SMS Text","Disbursement Expenses","Consolidated Amount"};
                Array.Sort(list);

                List<String> Calc = new List<String>();
                for (int i = 0; i < list.Count(); i++)
                {
                    long result = access.checkact(list[i], long.Parse(Session["UserID"].ToString()));
                    Calc.Add(Convert.ToString(result));
                }
                ViewBag.menulist = Calc;
                String[] menuheader = new String[3] { "Setup", "Transaction", "Reports" };
                Array.Sort(menuheader);
                List<String> Calc1 = new List<String>();
                for (int i = 0; i < menuheader.Count(); i++)
                {
                    long result1 = access.Validatescreen(long.Parse(Session["UserID"].ToString()), menuheader[i]);
                    Calc1.Add(Convert.ToString(result1));
                }
                ViewBag.menuheader = Calc1;
                var drt = lang.validatelang(long.Parse(Session["UserID"].ToString())/*, long.Parse(Session["InstituteID"].ToString())*/);
                var snnum = lang.validatesno();

                //if (drt.Loc_Name == "Swahili" && snnum == true)
                //{
                //        var bb = lang.EditColumn1("Designation", "Designation");
                //        ViewData["desg"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;

                //    //bb = lang.EditColumn1("Budget Master", "Budgeting");
                //    //TempData["ospon"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;



                //    bb = lang.EditColumn1("Country", "Country");
                //        ViewData["coun"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //        bb = lang.EditColumn1("Region", "Region");
                //        ViewData["regi"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //        bb = lang.EditColumn1("District", "District");
                //        ViewData["distr"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //        bb = lang.EditColumn1("Ward", "Ward");
                //        ViewData["war"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //        bb = lang.EditColumn1("SecurityQuestion", "Security Question"); // first Param is Table Name  , second one column name 
                //        ViewData["seques"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;

                //        bb = lang.EditColumn1("Currency", "Currency");
                //        ViewData["curren"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //        bb = lang.EditColumn1("Currency", "Currency Code");
                //        ViewData["currencod"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;

                //    bb = lang.EditColumn1("Users", "Users");
                //    ViewData["users"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //    bb = lang.EditColumn1("Users", "User ID");
                //    ViewData["empid"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //    bb = lang.EditColumn1("Users", "First Name");
                //    ViewData["fname"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //    bb = lang.EditColumn1("Users", "Middle Name");
                //    ViewData["mname"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //    bb = lang.EditColumn1("Users", "Last Name");
                //    ViewData["lname"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //    bb = lang.EditColumn1("Users", "Username");
                //    ViewData["uname"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //    bb = lang.EditColumn1("Users", "Role");
                //    ViewData["role"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //    bb = lang.EditColumn1("Users", "Mobile Number");
                //    ViewData["mobnum"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //    bb = lang.EditColumn1("Users", "Email");
                //    ViewData["emaiid"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //    bb = lang.EditColumn1("Users", "Full Name");
                //    ViewData["funame"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //    bb = lang.EditColumn1("Users", "User Id");
                //    ViewData["uid"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;

                //    bb = lang.EditColumn1("Email", "Email");
                //        ViewData["emai"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //        bb = lang.EditColumn1("Email", "Flow Id");
                //        ViewData["floid"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //        bb = lang.EditColumn1("Email", "Subject English");
                //        ViewData["subeng"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //        bb = lang.EditColumn1("Email", "Subject Swahili");
                //        ViewData["subswa"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //        bb = lang.EditColumn1("Email", "Email Text");
                //        ViewData["emaitxt"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //        bb = lang.EditColumn1("Email", "Email Text Local");
                //        ViewData["emaitxtloc"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //        bb = lang.EditColumn1("Email", "Email Subject");
                //        ViewData["eamisub"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //        bb = lang.EditColumn1("Email", "Email Subject Local");
                //        ViewData["emaisubloc"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;

                //        bb = lang.EditColumn1("SMTP", "SMTP");
                //        ViewData["smtp"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //        bb = lang.EditColumn1("SMTP", "From Email Id");
                //        ViewData["frmemid"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //        bb = lang.EditColumn1("SMTP", "SMTP Address");
                //        ViewData["smtpaddr"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //        bb = lang.EditColumn1("SMTP", "Port Number");
                //        ViewData["prtnum"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //        bb = lang.EditColumn1("SMTP", "Password");
                //        ViewData["pwd"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //        bb = lang.EditColumn1("SMTP", "SSL Enable");
                //        ViewData["sslenb"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;

                //    bb = lang.EditColumn1("Sponsor", "Sponsor");
                //    ViewData["spon"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //    bb = lang.EditColumn1("Sponsor", "P.O Box");
                //    ViewData["pobox"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //    bb = lang.EditColumn1("Sponsor", "Address");
                //    ViewData["addr"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //    bb = lang.EditColumn1("Sponsor", "Contact Person");
                //    ViewData["cntper"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //    bb = lang.EditColumn1("Sponsor", "Approve Sponsor");
                //    ViewData["appspon"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //    bb = lang.EditColumn1("Sponsor", "Return Sponsor Details");
                //    ViewData["retspondet"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //    bb = lang.EditColumn1("Sponsor", "Sponsor Details");
                //    ViewData["spondet"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;

                //    bb = lang.EditColumn1("Grid", "Sno");
                //        ViewData["Bsno"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //        bb = lang.EditColumn1("Grid", "Actions");
                //        ViewData["Bact"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //        bb = lang.EditColumn1("Grid", "Status");
                //        ViewData["Bstat"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //        bb = lang.EditColumn1("Button", "Close");
                //        ViewData["Bclo"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //        bb = lang.EditColumn1("Button", "Save");
                //        ViewData["Bsav"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //        bb = lang.EditColumn1("Button", "Update");
                //        ViewData["BUpd"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //        bb = lang.EditColumn1("Button", "Create");
                //        ViewData["Bct"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //        bb = lang.EditColumn1("Button", "Delete");
                //        ViewData["Badel"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //        bb = lang.EditColumn1("Button", "Approve");
                //        ViewData["appr"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //        bb = lang.EditColumn1("Button", "Return");
                //        ViewData["retur"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //        bb = lang.EditColumn1("Button", "Home");
                //        ViewData["hom"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //        bb = lang.EditColumn1("Button", "PDF");
                //        ViewData["pdf"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //        bb = lang.EditColumn1("Button", "Excel");
                //        ViewData["excel"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //        bb = lang.EditColumn1("Button", "Submit");
                //        ViewData["subm"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //        bb = lang.EditColumn1("Button",  "Add New Row");
                //        ViewData["addnewr"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;

                //    bb = lang.EditColumn1("Board", "Board");
                //    ViewData["boar"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //    bb = lang.EditColumn1("Board", "Approve Board Details");
                //    ViewData["boardet"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //    bb = lang.EditColumn1("Board", "Return Board Details");
                //    ViewData["retboardet"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //    bb = lang.EditColumn1("Board", "Board Details");
                //    ViewData["boardet"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;

                //    bb = lang.EditColumn1("Team", "Team");
                //    ViewData["tea"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //    bb = lang.EditColumn1("Team", "Team Size");
                //    ViewData["teamem"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //    bb = lang.EditColumn1("Team", "Approve: Teams");
                //    ViewData["appteadet"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //    bb = lang.EditColumn1("Team", "Return: Teams");
                //    ViewData["retteamdet"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //    bb = lang.EditColumn1("Team", "No Of Team Members");
                //    ViewData["nofteamem"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //    bb = lang.EditColumn1("Team", "Team");
                //    ViewData["teamdet"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;

                //    bb = lang.EditColumn1("Disbursement to Board", "Disbursement to Board");
                //    ViewData["spontrantoboa"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //    bb = lang.EditColumn1("Disbursement to Board", "From Date");
                //    ViewData["frmdat"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //    bb = lang.EditColumn1("Disbursement to Board", "To Date");
                //    ViewData["todate"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //    bb = lang.EditColumn1("Disbursement to Board", "Amount");
                //    ViewData["amt"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //    bb = lang.EditColumn1("Disbursement to Board", "Remarks");
                //    ViewData["rem"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //    bb = lang.EditColumn1("Disbursement to Board", "Cheque No");
                //    ViewData["cheqno"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //    bb = lang.EditColumn1("Disbursement to Board", "Approve: Disbursement to Board");
                //    ViewData["appspontoboa"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //    bb = lang.EditColumn1("Disbursement to Board", "Return: Disbursement to Board");
                //    ViewData["retspontoboa"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //    bb = lang.EditColumn1("Disbursement to Board", "Transfer Date");
                //    ViewData["transdat"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //    bb = lang.EditColumn1("Disbursement to Board", "Upload Document");
                //    ViewData["updocu"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;

                //    bb = lang.EditColumn1("Disbursement to Team", "Disbursement to Team");
                //    ViewData["btranstotea"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //    bb = lang.EditColumn1("Disbursement to Team", "S To B Date");
                //    ViewData["stobdat"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //    bb = lang.EditColumn1("Disbursement to Team", "Balance Amount");
                //    ViewData["balamt"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //    bb = lang.EditColumn1("Disbursement to Team", "Amount Disbursed");
                //    ViewData["amtpai"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //    bb = lang.EditColumn1("Disbursement to Team", "Approve: Disbursement to Team");
                //    ViewData["appbtotdet"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //    bb = lang.EditColumn1("Disbursement to Team", "Return: Disbursement to Team");
                //    ViewData["retbtoteamdet"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //    bb = lang.EditColumn1("Disbursement to Team", "Search Board");
                //    ViewData["rseaboa"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //    bb = lang.EditColumn1("Disbursement to Team", "Received Amount");
                //    ViewData["reamt"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //    bb = lang.EditColumn1("Disbursement to Team", "Return Board Details");
                //    ViewData["retboadet"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;

                //    bb = lang.EditColumn1("Disbursement to Team", "Current Month");
                //    ViewData["trnascurrmonth"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //    bb = lang.EditColumn1("Disbursement to Team", "Previous Month");
                //    ViewData["trnascurryear"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;


                //    bb = lang.EditColumn1("Expenses", "Record Expenditures");
                //    ViewData["expmas"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //    bb = lang.EditColumn1("Expenses", "Board Transfer");
                //    ViewData["boatrans"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //    bb = lang.EditColumn1("Expenses", "B To T Date");
                //    ViewData["btotdate"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //    bb = lang.EditColumn1("Expenses", "Transfer Amount");
                //    ViewData["transamt"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //    bb = lang.EditColumn1("Expenses", "Spend Amount");
                //    ViewData["spenamt"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //    bb = lang.EditColumn1("Expenses", "Expenses Amount");
                //    ViewData["expamt"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //    bb = lang.EditColumn1("Expenses", "File Name");
                //    ViewData["filname"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //    bb = lang.EditColumn1("Expenses", "Expenses Details");
                //    ViewData["expdet"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //    bb = lang.EditColumn1("Expenses", "Search");
                //   ViewData["sear"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //    bb = lang.EditColumn1("Expenses", "Approve: Expenses");
                //    ViewData["appexpdet"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //    bb = lang.EditColumn1("Expenses", "Return: Expenses");
                //    ViewData["retexpmas"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //    bb = lang.EditColumn1("Expenses", "View");
                //    ViewData["viie"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;

                //    bb = lang.EditColumn1("Team Expenses", "Team Expenses");
                //    ViewData["teamexp"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //    bb = lang.EditColumn1("Team Expenses", "Expenses Date");
                //    ViewData["expdat"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;


                //    bb = lang.EditColumn1("Board Balance Amount", "Board Balance Amount");
                //    ViewData["boabalamt"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //    bb = lang.EditColumn1("Board Balance Amount", "Received Amount From Sponser");
                //    ViewData["reamtspon"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //        bb = lang.EditColumn1("Board Balance Amount", "Total Transfer Amount to Team");
                //    ViewData["tottraamt"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;

                //    bb = lang.EditColumn1("Access Rights", "Access Rights");
                //    ViewData["accri"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //    bb = lang.EditColumn1("Access Rights", "Code");
                //    ViewData["cod"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //    bb = lang.EditColumn1("Access Rights", "Description");
                //    ViewData["desc"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //    bb = lang.EditColumn1("Access Rights", "Module");
                //    ViewData["modu"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;

                //    bb = lang.EditColumn1("Menu", "Dashboard");
                //    ViewData["dashbd"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //    bb = lang.EditColumn1("Menu", "Setup");
                //    ViewData["setup"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //    bb = lang.EditColumn1("Menu", "Transaction");
                //    ViewData["transaction"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //    bb = lang.EditColumn1("Menu", "Language");
                //    ViewData["lang"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //    bb = lang.EditColumn1("Menu", "Audit Trail");
                //    ViewData["adtrail"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //    bb = lang.EditColumn1("Menu", "Reports");
                //    ViewData["report"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //    bb = lang.EditColumn1("Menu", "Select Page");
                //    ViewData["selpag"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;

                //    bb = lang.EditColumn1("Team Wise Balance", "Balance – Team wise");
                //    ViewData["teawisbal"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //    bb = lang.EditColumn1("Team Wise Balance", "Received Amount From Board");
                //    ViewData["reamtfrmboa"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //    bb = lang.EditColumn1("Team Wise Balance", "Start Date");
                //    ViewData["sttdate"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //    bb = lang.EditColumn1("Team Wise Balance", "End Date");
                //    ViewData["endddate"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //    bb = lang.EditColumn1("Team Wise Balance", "Column Name");
                //    ViewData["colname"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //    bb = lang.EditColumn1("Team Wise Balance", "Old Value");
                //    ViewData["oldval"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //    bb = lang.EditColumn1("Team Wise Balance", "New Value");
                //    ViewData["newval"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //    bb = lang.EditColumn1("Team Wise Balance", "Posted/Modified By");
                //    ViewData["postby"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //    bb = lang.EditColumn1("Team Wise Balance", "Audit Date");
                //    ViewData["audiby"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;

                //    bb = lang.EditColumn1("Comment", "Comment By");
                //    ViewData["cmtby"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //    bb = lang.EditColumn1("Comment", "Date");
                //    ViewData["cmtdate"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //    bb = lang.EditColumn1("Comment", "Comment");
                //    ViewData["cmt"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;

                //    bb = lang.EditColumn1("Layout", "Change Password");
                //    ViewData["layo"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;
                //    bb = lang.EditColumn1("Layout", "Logout");
                //    ViewData["logou"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;

                //    bb = lang.EditColumn1("Expenses", "Approve: Expenses");
                //    ViewData["eae"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;

                //    bb = lang.EditColumn1("Expenses", "Return: Expenses");
                //    ViewData["ere"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;

                //    bb = lang.EditColumn1("Budget Master", "Overall Sponsorship");
                //    ViewData["ospon"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;

                //    bb = lang.EditColumn1("Budget Master", "Season");
                //    ViewData["bseason"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;

                //    bb = lang.EditColumn1("Budget Master", "Season Start Month");
                //    ViewData["bsmonth"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;

                //    bb = lang.EditColumn1("Budget Master", "Months Per Season");
                //    ViewData["bspermonth"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;

                //    bb = lang.EditColumn1("Budget Master", "Budget Details");
                //    ViewData["bidgdet"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;

                //    bb = lang.EditColumn1("Budget Master", "Budget Amount");
                //    ViewData["budgamt"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;

                //    bb = lang.EditColumn1("Budget Master", "Budget");
                //    ViewData["budget"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;

                //    bb = lang.EditColumn1("Budget Master", "Approve Budget");
                //    ViewData["appbudget"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;

                //    bb = lang.EditColumn1("Budget Master", "Return Budget");
                //    ViewData["retbudget"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;


                //    bb = lang.EditColumn1("Season Master", "Season");
                //    ViewData["mseason"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;

                //    bb = lang.EditColumn1("Season Master", "Season Status");
                //    ViewData["mseasonstatus"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;

                //    bb = lang.EditColumn1("Expenses Category", "Category");
                //    ViewData["expscatg"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;

                //    bb = lang.EditColumn1("Expenses Category", "Category Description");
                //    ViewData["expsdesc"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;

                //    bb = lang.EditColumn1("Expenses Category", "Category Status");
                //    ViewData["expscatgstatus"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;

                //    bb = lang.EditColumn1("Consolidate", "Month");
                //    ViewData["consmonth"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;

                //    bb = lang.EditColumn1("Consolidate", "Year");
                //    ViewData["consyear"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;

                //    bb = lang.EditColumn1("Consolidate", "Consolidate Amount");
                //    ViewData["consamount"] = bb.Loc_Swa == null ? bb.Loc_Eng : bb.Loc_Swa;


                //}
                //else
                //{
                ViewData["eae"] = "Approve: Expenses";
                ViewData["ere"] = "Return: Expenses";

                ViewData["desg"] = "Designation";
                ViewData["Bsno"] = "Sno";
                ViewData["Bact"] = "Actions";
                ViewData["Bstat"] = "Status";
                ViewData["Bclo"] = "Close";
                ViewData["Bsav"] = "Save";
                ViewData["BUpd"] = "Update";
                ViewData["Bct"] = "Create";
                ViewData["Badel"] = "Delete";
                ViewData["addnewr"] = "AddNewRow";

                ViewData["cmtby"] = "Comment By";
                ViewData["cmt"] = "Comment";
                ViewData["cmtdate"] = "Date";
                ViewData["hom"] = "Home";
                ViewData["appr"] = "Approve";
                ViewData["retur"] = "Return";
                ViewData["pdf"] = "PDF";
                ViewData["excel"] = "Excel";
                ViewData["subm"] = "Submit";
                ViewData["coun"] = "Country";
                ViewData["regi"] = "Region";
                ViewData["distr"] = "District";
                ViewData["war"] = "Ward";
                ViewData["seques"] = "Security Question"; // maped directly loc_eng column 
                ViewData["curren"] = "Currency";
                ViewData["currencod"] = "Currency Code";

                ViewData["emai"] = "Email";
                ViewData["floid"] = "Flow Id";
                ViewData["subeng"] = "Subject English";
                ViewData["subswa"] = "Subject Swahili";
                ViewData["emaitxt"] = "Email Text";
                ViewData["emaitxtloc"] = "Email Text Local";
                ViewData["eamisub"] = "Email Subject";
                ViewData["emaisubloc"] = "Email Subject Local";

                ViewData["users"] = "Users";
                ViewData["empid"] = "User ID";
                ViewData["fname"] = "First Name";
                ViewData["lname"] = "Last Name";
                ViewData["mname"] = "Middle Name";
                ViewData["uname"] = "Username";
                ViewData["role"] = "Role";
                ViewData["mobnum"] = "Mobile Number";
                ViewData["emaiid"] = "Email";
                ViewData["funame"] = "Full Name";
                ViewData["uid"] = "User Id";

                ViewData["smtp"] = "SMTP";
                ViewData["frmemid"] = "From Email Id";
                ViewData["smtpaddr"] = "SMTP Address";
                ViewData["prtnum"] = "Port Number";
                ViewData["pwd"] = "Password";
                ViewData["sslenb"] = "SSL Enable";

                ViewData["spon"] = "Sponsor";
                ViewData["pobox"] = "P.O Box";
                ViewData["addr"] = "Address";
                ViewData["cntper"] = "Contact Person";
                ViewData["appspon"] = "Approve Sponsor";
                ViewData["retspondet"] = "Return Sponsor Details";
                ViewData["spondet"] = "Sponsor Details";

                ViewData["boar"] = "Board";
                ViewData["boardet"] = "Approve Board Details";
                ViewData["retboardet"] = "Return Board Details";
                ViewData["boardet"] = "Board Details";


                ViewData["tea"] = "Teams";
                ViewData["teamem"] = "Team Size";
                ViewData["appteadet"] = "Approve: Teams";
                ViewData["retteamdet"] = "Return: Teams";
                ViewData["nofteamem"] = "No Of Team Members";
                ViewData["teamdet"] = "Teams";

                ViewData["spontrantoboa"] = "Disbursement to Board";
                ViewData["frmdat"] = "From Date";
                ViewData["todate"] = "To Date";
                ViewData["amt"] = "Amount";
                ViewData["rem"] = "Remarks";
                ViewData["cheqno"] = "Cheque No";
                ViewData["appspontoboa"] = "Approve: Disbursement to Board";
                ViewData["retspontoboa"] = "Return: Disbursement to Board";
                ViewData["transdat"] = "Transfer Date";
                ViewData["updocu"] = "Upload Document";

                ViewData["btranstotea"] = "Disbursement to Team";
                ViewData["stobdat"] = "S To B Date";
                ViewData["balamt"] = "Balance Amount";
                ViewData["amtpai"] = "Amount Disbursed";
                ViewData["appbtotdet"] = "Approve: Disbursement to Team";
                ViewData["retbtoteamdet"] = "Return: Disbursement to Team";
                ViewData["rseaboa"] = "Search Board";
                ViewData["reamt"] = "Received Amount";
                ViewData["retboadet"] = "Return: Disbursement to Team";

                ViewData["expmas"] = "Record Expenditures";
                ViewData["boatrans"] = "Board Transfer";
                ViewData["btotdate"] = "B To T Date";
                ViewData["transamt"] = "Transfer Amount";
                ViewData["spenamt"] = "Spend Amount";
                ViewData["expamt"] = "Expenses Amount";
                ViewData["filname"] = "File Name";
                ViewData["expdet"] = "Expenses Details";
                ViewData["sear"] = "Search";
                ViewData["appexpdet"] = "Approve: Expenses";
                ViewData["retexpmas"] = "Return: Expenses";
                ViewData["viie"] = "View";

                ViewData["teamexp"] = "Team Expenses";
                ViewData["expdat"] = "Expenses Date";

                ViewData["boabalamt"] = "Board Balance Amount";
                ViewData["reamtspon"] = "Received Amount From Sponser";
                ViewData["tottraamt"] = "Total Transfer Amount to Team";

                ViewData["teawisbal"] = "Balance – Team wise";
                ViewData["reamtfrmboa"] = "Received Amount From Board";
                ViewData["colname"] = "Column Name";
                ViewData["oldval"] = "Old Value";
                ViewData["newval"] = "New Value";
                ViewData["postby"] = "Posted/Modified By";
                ViewData["audiby"] = "Audit Date";


                ViewData["sttdate"] = "Start Date";
                ViewData["endddate"] = "End Date";

                ViewData["layo"] = "Change Password";
                ViewData["logou"] = "Logout";

                ViewData["accri"] = "Access Rights";
                ViewData["cod"] = "Code";
                ViewData["desc"] = "Description";
                ViewData["modu"] = "Module";


                ViewData["dashbd"] = "Dashboard";
                ViewData["setup"] = "Setup";
                ViewData["transaction"] = "Transaction";
                ViewData["lang"] = "Language";
                ViewData["adtrail"] = "Audit Trail";
                ViewData["report"] = "Reports";
                ViewData["selpag"] = "Select Page";


                ViewData["ospon"] = "Overall Sponsorship";
                ViewData["bseason"] = "Season";
                ViewData["bsmonth"] = "Season Start Month";
                ViewData["bspermonth"] = "Months per Season";
                ViewData["bidgdet"] = "Budget Details";
                ViewData["budgamt"] = "Budget Amount";

                ViewData["budget"] = "Budgeting";
                ViewData["appbudget"] = "Approve: Budgeting";
                ViewData["retbudget"] = "Return Budgeting";

                ViewData["mseason"] = "Season";
                ViewData["mseasonstatus"] = "Status";

                ViewData["expscatg"] = "Expense Categories";
                ViewData["expsdesc"] = "Category Description";
                ViewData["expscatgstatus"] = "Category Status";


                ViewData["consmonth"] = "Month";
                ViewData["consyear"] = "Year";
                ViewData["consamount"] = "Consolidated Amount";


                ViewData["trnascurrmonth"] = "Current Month";
                ViewData["trnascurryear"] = "Previous Month";
                // }
            }
            catch (Exception ec)
            {
                ec.ToString();
            }
        }
    }
    public class Lang_FaciController : BaseController
    {
        Language_Faci lang = new Language_Faci();
        EMP_DET ed = new EMP_DET();
        Arights act = new Arights();

        [Route("Language")]
        public ActionResult Lang_Faci()
        {
            if (Session["admin1"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                //var count = act.checkact("expensescategory");
                var count = act.checkact("Language", long.Parse(Session["Desg"].ToString()), Session["role"].ToString(), Session["Rcode"].ToString());
                if (count == 0)
                {
                    return RedirectToAction("Setup", "Setup");
                }
            }
            return View();
        }
        public ActionResult Adddesg(string table, List<Language_Faci> details)
        {
            try
            {
                lang.Table_name = table;
                long ssno = 0;
                for (int i = 0; i < details.Count; i++)
                {
                    lang.Loc_Eng = details[i].Loc_Eng;
                    lang.Loc_Swa = details[i].Loc_Swa;
                    lang.Loc_Sno = details[i].Loc_Sno;
                    lang.Col_name = details[i].Loc_Eng;
                    lang.AuditBy = Session["UserID"].ToString();
                    lang.Updated_By = Session["UserID"].ToString();
                    lang.UpdateLan(lang);
                    ssno = details[i].Loc_Sno;
                }
                return Json(ssno, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }
            return null;
        }
        public ActionResult Editlangs(string tbname)
        {
            try
            {
                var result = lang.GetScreens(tbname);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
            }
            return null;
        }
        [HttpPost]
        public ActionResult Update(string name)
        {
            try
            {
                ed.Detail_Id = long.Parse(Session["UserID"].ToString());
                ed.Loc_Name = name;
                ed.Updatelang(ed);
                return Json(2, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
            }

            return null;
        }


    }
}
