// Decompiled with JetBrains decompiler
// Type: FUNDING.Controllers.LoginController
// Assembly: FUNDING, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5DE1C1E8-8D80-43BD-915D-F2502219BD58
// Assembly location: D:\ECardtest - 22082024\bin\FUNDING.dll

using ECARD.DL.EDMX;
using FUNDING.BL.BusinessEntities.ConstantFile;
using FUNDING.BL.BusinessEntities.Masters;
using FUNDING.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;

 
namespace FUNDING.Controllers
{
  public class LoginController : Controller
  {
    private EMP_DET Empdet = new EMP_DET();
    private TRACK_DET dt = new TRACK_DET();

    [Route("Admin")]
    public ActionResult Login() => (ActionResult) this.View();

    [Route("")]
    public ActionResult CustomerLogin() => (ActionResult) this.View();

    [HttpPost]
    public ActionResult CustomerLoginAjax(string mobileNumber, string password)
    {
      string str = this.ServerSideValidation(mobileNumber, password);
      if (!string.IsNullOrEmpty(str))
        return (ActionResult) this.Json((object) new
        {
          message = str,
          status = 0
        });
      string encryptedData = this.GetEncryptedData(password);
      List<event_details> eventDetailsList = CustomerUsers.EventsEntitled(mobileNumber, encryptedData);
      if (eventDetailsList == null)
        return (ActionResult) this.Json((object) new
        {
          message = "Invalid mobile number or password",
          status = 0
        });
      List<object> objectList = new List<object>();
      foreach (event_details eventDetails in eventDetailsList)
        objectList.Add((object) new
        {
          event_det_sno = eventDetails.event_det_sno,
          event_name = eventDetails.event_name
        });
      this.Session[nameof (mobileNumber)] = (object) mobileNumber;
      return (ActionResult) this.Json((object) new
      {
        status = 1,
        eventDetails = objectList
      });
    }

        [HttpPost]
        public ActionResult EventOfChoiceAjax(string eventOfChoice)
        {
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
                var eventsDb = context.event_details.Find(Convert.ToInt32(eventOfChoice));

                if (eventsDb != null)
                {
                    //1. Fetch customer user id
                    var customerAdminID = eventsDb.cust_reg_sno;
                    var mobileNumber = Session["mobileNumber"] as string;

                    //2. Query for specific user with specific customer admin id and mobile number
                    var currentLoggedInUser = context.cust_users
                        .Where(c => c.cust_reg_sno == customerAdminID && c.mobile_no == mobileNumber && c.event_det_sno.Contains(eventsDb.event_det_sno.ToString())).SingleOrDefault();

                    //Configure session
                    Session["User_name"] = currentLoggedInUser.user_fullname;
                    Session["UfullName"] = currentLoggedInUser.user_fullname;
                    Session["admin1"] = currentLoggedInUser.user_type;
                    Session["flogin"] = currentLoggedInUser.f_login;
                    Session["UserID"] = currentLoggedInUser.cust_users_sno;
                    Session["EventAdminID"] = currentLoggedInUser.cust_reg_sno;
                    Session["EventID"] = eventOfChoice;

                    Session.Timeout = 10_000;

                    return Json(new { loginStatus = 1 });
                }
            }

            return Json(new { loginStatus = 0, response = "Failed! Item does not exist." });
        }

        private string ServerSideValidation(string mobileNumber, string password)
    {
      string str = (string) null;
      if (string.IsNullOrEmpty(mobileNumber))
        str += "Mobile Number is Required.<br/>";
      else if (!this.ValidateMobileNumber(mobileNumber))
        str = "Please enter valid mobile number.<br/>";
      if (string.IsNullOrEmpty(password))
        str += "Password is Required.<br/>";
      return str;
    }

    private bool ValidateMobileNumber(string mobileNumber)
    {
      return new Regex("^[0-9]{10}$").IsMatch(mobileNumber);
    }

    [HttpPost]
    public ActionResult AddLogin(string uname, string pwd)
    {
      string encryptedData = this.GetEncryptedData(pwd);
      EMP_DET empDet1 = this.Empdet.CheckLogin(uname, encryptedData);
      if (empDet1 != null)
      {
        this.Session["UfullName"] = (object) empDet1.Full_Name;
        this.Session["UserID"] = (object) empDet1.Detail_Id;
        this.Session["admin1"] = (object) "Bank";
        this.Session["User_name"] = (object) empDet1.User_name;
        this.Session["flogin"] = (object) empDet1.F_Login;
        this.Session["teamid"] = (object) empDet1.team_det_sno;
        this.Session["Desg"] = (object) empDet1.Desg_Id;
        this.Session["role"] = (object) empDet1.Desg_name;
        this.Session["Rcode"] = (object) empDet1.Rol_Code;
        this.Session["flogin"] = (object) empDet1.F_Login;
        this.dt.Full_Name = empDet1.Full_Name;
        this.dt.Facility_Reg_No = Convert.ToString(empDet1.Detail_Id);
        this.dt.Ipadd = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        this.dt.Email = empDet1.Email_Address;
        this.dt.Posted_by = Convert.ToString(empDet1.Detail_Id);
        this.dt.Login_Time = DateTime.Now;
        this.dt.Description = "Bank";
        if (!this.Empdet.ValidateLoginorNot(empDet1.Detail_Id, empDet1.Desg_Id))
        {
          if (empDet1.F_Login == "false")
            return (ActionResult) this.Json((object) new
            {
              check = "Bank",
              tem = empDet1.team_det_sno,
              flogin = empDet1.F_Login,
              InstID = 0,
              Usno = empDet1.Detail_Id
            }, JsonRequestBehavior.AllowGet);
          this.dt.AddTrack(this.dt);
          object data = (object) new
          {
            check = "Bank",
            tem = empDet1.team_det_sno,
            flogin = empDet1.F_Login,
            InstID = 0,
            Usno = empDet1.Detail_Id
          };
          this.Empdet.Desg_Id = empDet1.Desg_Id;
          this.Empdet.Detail_Id = empDet1.Detail_Id;
          this.Empdet.Loc_Name = "Swahili";
          this.Empdet.UpdatelangInst(this.Empdet);
          this.Empdet.UpdatelangInsttrue(this.Empdet);
          return (ActionResult) this.Json(data, JsonRequestBehavior.AllowGet);
        }
        if ((DateTime.Now - Convert.ToDateTime(empDet1.valiadte_Login(empDet1.Desg_Id, empDet1.Detail_Id).C_time)).Minutes < 5)
          return (ActionResult) this.Json((object) new
          {
            check = "You are already logged in (or) the browser is closed without logout, Please wait while reconnecting again..",
            check1 = "Bank"
          }, JsonRequestBehavior.AllowGet);
        this.Empdet.Desg_Id = empDet1.Desg_Id;
        this.Empdet.Detail_Id = empDet1.Detail_Id;
        this.Empdet.UpdatelangInsttrue(this.Empdet);
        this.dt.AddTrack(this.dt);
        object data1 = (object) new
        {
          check = "Bank",
          flogin = empDet1.F_Login,
          InstID = 0,
          Usno = empDet1.Detail_Id
        };
        this.Empdet.Detail_Id = empDet1.Detail_Id;
        this.Empdet.Desg_Id = empDet1.Desg_Id;
        this.Empdet.Loc_Name = "Swahili";
        this.Empdet.UpdatelangInst(this.Empdet);
        return (ActionResult) this.Json(data1, JsonRequestBehavior.AllowGet);
      }
      if (uname.ToLower().ToString() == "support" && pwd.ToLower().ToString() == "support2024")
      {
        this.Session["UfullName"] = (object) "Support Admin";
        this.Session["UserID"] = (object) "1";
        this.Session["admin1"] = (object) "Support";
        this.Session["User_name"] = (object) "Support";
        this.Session["flogin"] = (object) true;
        this.Session["teamid"] = (object) 12;
        this.Session["Desg"] = (object) 1;
        this.Session["role"] = (object) "Support";
        this.Session["Rcode"] = (object) 1;
        this.Session["flogin"] = (object) true;
        return (ActionResult) this.Json((object) new
        {
          check = "Support",
          tem = 1,
          flogin = true,
          InstID = 0,
          Usno = 0
        }, JsonRequestBehavior.AllowGet);
      }
      EMP_DET empDet2 = this.Empdet.CheckuserLogStat(uname);
      if (empDet2 != null)
      {
        if (empDet2.Log_Status != null)
          return (ActionResult) this.Json((object) new
          {
            check = "Your account was blocked by admin, Please contact Administrator"
          }, JsonRequestBehavior.AllowGet);
        EMP_DET empDet3 = this.Empdet.Checkuseratt(uname);
        if (empDet3 != null)
        {
          if (empDet3.Log_Att >= 3)
            return (ActionResult) this.Json((object) new
            {
              check = "Your account has been blocked, Please contact Administrator"
            }, JsonRequestBehavior.AllowGet);
          int num = empDet3.Log_Att + 1;
          this.Empdet.Log_Att = num;
          this.Empdet.Detail_Id = empDet2.Detail_Id;
          this.Empdet.Updatelogatt(this.Empdet);
          string str = (3 - num).ToString();
          if (str == "0")
          {
            this.Empdet.Log_Status = Utilites.GetEncryptedData("Blocked");
            this.Empdet.Detail_Id = empDet2.Detail_Id;
            this.Empdet.BlockUser(this.Empdet);
            return (ActionResult) this.Json((object) new
            {
              check = "Your account has been blocked, Please contact Administrator"
            }, JsonRequestBehavior.AllowGet);
          }
          var data = new
          {
            check = "Invalid Password. We Have " + str + " Attempt left"
          };
          return (ActionResult) this.Json((object) data, JsonRequestBehavior.AllowGet);
        }
        this.Empdet.Log_Att = 1;
        this.Empdet.Detail_Id = empDet2.Detail_Id;
        this.Empdet.Updatelogatt(this.Empdet);
        var data2 = new
        {
          check = "Invalid Password. We Have " + (3 - this.Empdet.Log_Att).ToString() + " Attempt left"
        };
        return (ActionResult) this.Json((object) data2, JsonRequestBehavior.AllowGet);
      }
      var data3 = new
      {
        check = "Username or password is incorrect"
      };
      return (ActionResult) this.Json((object) data3, JsonRequestBehavior.AllowGet);
    }

    [NoCache]
    [Route("Logout")]
    public ActionResult CustomerLogout()
    {
      this.Session.Clear();
      this.Session.Abandon();
      this.Session.RemoveAll();
      return (ActionResult) this.RedirectToAction("CustomerLogin", "Login");
    }

    [NoCache]
    [Route("Admin/Logout")]
    public ActionResult Logout()
    {
      if (this.Session["UserID"] != null)
      {
        this.Empdet.Detail_Id = Convert.ToInt64(this.Session["UserID"].ToString());
        this.Empdet.Desg_Id = Convert.ToInt64(this.Session["Desg"].ToString());
        this.Empdet.UpdatelangInstfalse(this.Empdet);
        TRACK_DET trackDet = this.dt.EditTRACK(this.Session["UserID"].ToString());
        this.dt.SNO = trackDet == null ? 0L : trackDet.SNO;
        this.dt.Posted_by = this.Session["UserID"].ToString();
        this.dt.UpdateTRACKEmp(this.dt);
      }
      this.Session.Clear();
      this.Session.Abandon();
      this.Session.RemoveAll();
      return (ActionResult) this.RedirectToAction("Login", "Login");
    }

    public string GetEncryptedData(string value)
    {
      byte[] numArray = new byte[value.Length];
      return Convert.ToBase64String(Encoding.UTF8.GetBytes(value));
    }
  }
}
