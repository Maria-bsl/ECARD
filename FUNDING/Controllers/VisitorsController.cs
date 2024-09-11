
using ECARD.DL.EDMX;
using FUNDING.BL.BusinessEntities.Masters;
using FUNDING.Models.AppHelpers;
using FUNDING.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

 
namespace FUNDING.Controllers
{
  public class VisitorsController : Controller
  {
    private S_SMTP stp = new S_SMTP();
    private EMAIL em = new EMAIL();
    private Role rl = new Role();
    private Comments cm = new Comments();
    private Arights act = new Arights();
    private AuditLogs ad = new AuditLogs();
    private SMS_SETTNG smst = new SMS_SETTNG();
    private SMS_TEXT sms = new SMS_TEXT();
    private readonly Visitors _visitor = new Visitors();
    private readonly ECARDAPPEntities _dbContext = new ECARDAPPEntities();

    [Route("Invitees")]
    public ActionResult Index()
    {
      if (this.Session["admin1"] as string == CustomerUsers.EventAdminUserType)
      {

                ViewBag.cardState = new SelectList(_dbContext.card_state_master, "card_state_mas_sno", "card_state");

                return (ActionResult) this.View();
      }
      return this.Session["admin1"] as string == CustomerUsers.NormalUserType ? this.View("Index_readonly") : (ActionResult) this.RedirectToAction("CustomerLogin", "Login");
    }

    public ActionResult IndexDataTable()
    {
      this.GetCustomerAdminID();
      long? eventID = this.GetEventID();
      var visitor = this._dbContext.visitor_details.Include(c => c.card_state_master).Where(iv => iv.event_det_sno == eventID).OrderByDescending(iv => iv.posted_date).Select(vd => new
      {
        vd.visitor_det_sno,
        vd.visitor_name,
        vd.card_state_master.card_state,
        vd.no_of_persons,
        vd.mobile_no,
        vd.email_address,
        vd.qrcode,
        vd.table_number,
        vd.event_det_sno
      }).ToList();
      int index = 1;
      return (ActionResult) this.Json((object) new
      {
        data = visitor.Select((vd, id) => new
        {
          id = index++,
          visitor_det_sno = vd.visitor_det_sno,
          visitor_name = vd.visitor_name,
          card_state = vd.card_state,
          no_of_persons = vd.no_of_persons,
          mobile_no = vd.mobile_no,
          email_address = vd.email_address,
          qrcode = vd.qrcode,
          table_number = vd.table_number,
          event_det_sno = vd.event_det_sno
        }),
        TotalInvitees = visitor.Select(t => t.no_of_persons).DefaultIfEmpty<int?>(new int?(0)).Sum()
      });
    }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IsMobileNumberAvailable(string __RequestVerificationToken, string Int_Mobile_number, string Mobile_number_clone)
        {
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

            var eventAdminID = GetCustomerAdminID();
            var event_id = GetEventID();

            var isMobileAvailable = (from u in _dbContext.visitor_details
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
    public ActionResult AjaxHelperBulkUploadForm([Bind(Include = "VisitorsFileAttachment")] VisitorsBulkUpload uploadedFile)
    {
      if (!this.ModelState.IsValid)
        return (ActionResult) this.PartialView("_AjaxHelperBulkUploadForm", (object) uploadedFile);
      string uploadedExcelFile = ExcelUploadedFileHandler.UploadedExcelSaveProcess(uploadedFile.VisitorsFileAttachment);
      List<visitor_details> visitorDetailsList = new List<visitor_details>();
      try
      {
        long? customerAdminId = this.GetCustomerAdminID();
        long? eventId = this.GetEventID();
        string postedBy = this.GetPostedBy();
        ExcelUploadedFileHandler.ReadExcelData(uploadedExcelFile, visitorDetailsList, customerAdminId, eventId, postedBy);
        ExcelUploadedFileHandler.SaveRecordToDatabase(visitorDetailsList);
        return (ActionResult) this.Json((object) visitorDetailsList);
      }
      catch (Exception ex)
      {

        return (ActionResult) this.Json((object) new
        {
          uploadStatus = false,
          message = "File processing failed"
        });
      }
      finally
      {
        ExcelUploadedFileHandler.DeleteUploadedFile(uploadedExcelFile);
      }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult AjaxHelperBulkUploadInviteeForm([Bind(Include = "VisitorsFileAttachment")] VisitorsBulkUpload uploadedFile)
    {
      if (!this.ModelState.IsValid)
        return (ActionResult) this.PartialView("_AjaxHelperBulkUploadForm", (object) uploadedFile);
      string uploadedExcelFile = ExcelUploadedFileHandler.UploadedExcelSaveProcess(uploadedFile.VisitorsFileAttachment);
      List<visitor_details> visitorDetailsList = new List<visitor_details>();
      try
      {
        long? customerAdminId = this.GetCustomerAdminID();
        long? eventId = this.GetEventID();
        string postedBy = this.GetPostedBy();
        ExcelUploadedFileHandler.ReadExcelDataWithQRcode(uploadedExcelFile, visitorDetailsList, customerAdminId, eventId, postedBy);
        ExcelUploadedFileHandler.SaveRecordToDatabaseWithQRcode(visitorDetailsList);
        return (ActionResult) this.Json((object) visitorDetailsList);
      }
      catch (Exception ex)
      {
        return (ActionResult) this.Json((object) new
        {
          uploadStatus = false,
          message = "File processing failed"
        });
      }
      finally
      {
        ExcelUploadedFileHandler.DeleteUploadedFile(uploadedExcelFile);
      }
    }

    public FileResult DownloadExcelTemplateVisitorCode()
    {
      string path = Path.Combine(DirectoryHelpers.ExcelTemplateAbsoluteDirectory, string.Format("{0}.xlsx", "Invitees_Registation_Template_Visitors_Code"));
      return (FileResult) this.File(System.IO.File.ReadAllBytes(path), "application/octet-stream", DirectoryHelpers.GetTimestampedFile(path));
    }

    public FileResult DownloadExcelTemplate()
    {
      string path = ExcelTemplateGenerationHandler.ExcelGenerationProcess("Invitees_Registation_Template");
      return (FileResult) this.File(System.IO.File.ReadAllBytes(path), "application/octet-stream", DirectoryHelpers.GetTimestampedFile(path));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult AjaxHelperCreateForm([Bind(Include = "visitor_name, card_state_mas_sno, no_of_persons, Table_Number, Int_Mobile_number, email_address")] Visitors visitor)
    {
      if (this.Session["admin1"] == null)
        return (ActionResult) this.Json((object) new
        {
          loginStatus = false
        });
      this.ModelState.Remove("mobile_no");
      if (this.ModelState.IsValid)
      {
        visitor_details entity = new visitor_details()
        {
          event_det_sno = this.GetEventID(),
          cust_reg_sno = this.GetCustomerAdminID(),
          visitor_name = visitor.visitor_name.ToString().Trim(),
          card_state_mas_sno = visitor.card_state_mas_sno,
          no_of_persons = visitor.no_of_persons,
          table_number = visitor.Table_Number,
          mobile_no = visitor.Int_Mobile_number.Replace("+", ""),
          email_address = visitor.email_address,
          posted_date = DateTime.Now,
          posted_by = this.GetPostedBy()
        };
        try
        {
          this._dbContext.visitor_details.Add(entity);
          this._dbContext.SaveChanges();
          entity.qrcode = Visitors.GetGeneratedQRCodeWithEventId(_dbContext, entity.event_det_sno);
          this._dbContext.Entry<visitor_details>(entity).State = EntityState.Modified;
          this._dbContext.SaveChanges();
          return (ActionResult) this.Json((object) new
          {
            createStatus = true,
            response = "Record successful created."
          });
        }
        catch (Exception ex)
        {
          return (ActionResult) this.Json((object) new
          {
            createStatus = false,
            response = "Record creation failed."
          });
        }
      }
      else
      {
        ViewBag.cardState = new SelectList(_dbContext.card_state_master, "card_state_mas_sno", "card_state");
         return (ActionResult) this.PartialView("_AjaxHelperCreateForm", (object) visitor);
      }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult AjaxHelperUpdateForm([Bind(Include = "visitor_det_sno, visitor_name, card_state_mas_sno, no_of_persons, Table_Number, Int_Mobile_number, email_address")] Visitors visitor)
    {
      if (this.Session["admin1"] == null)
        return (ActionResult) this.Json((object) new
        {
          loginStatus = false
        });
      this.ModelState.Remove("mobile_no");
      if (!this.ModelState.IsValid)
        return (ActionResult) this.PartialView("_AjaxHelperUpdateForm", (object) visitor);
      visitor_details entity = this._dbContext.visitor_details.Find(new object[1]
      {
        (object) visitor.visitor_det_sno
      });
      if (entity == null)
        return (ActionResult) this.Json((object) new
        {
          updateStatus = false,
          response = "Failed! Record does not exist."
        });
      entity.visitor_name = visitor.visitor_name.ToString().Trim();
      entity.card_state_mas_sno = visitor.card_state_mas_sno;
      entity.no_of_persons = visitor.no_of_persons;
      entity.table_number = visitor.Table_Number;
      entity.mobile_no = visitor.Int_Mobile_number.Replace("+", "");
      entity.email_address = visitor.email_address;
      entity.posted_date = DateTime.Now;
      entity.posted_by = this.GetPostedBy();
      this._dbContext.Entry<visitor_details>(entity).State = EntityState.Modified;
      this._dbContext.Entry<visitor_details>(entity).Property("event_det_sno").IsModified = false;
      this._dbContext.Entry<visitor_details>(entity).Property("cust_reg_sno").IsModified = false;
      this._dbContext.Entry<visitor_details>(entity).Property("qrcode").IsModified = false;
      this._dbContext.SaveChanges();
      return (ActionResult) this.Json((object) new
      {
        updateStatus = true,
        response = "Record successful updated."
      });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult AjaxHelperDeleteForm(long? id)
    {
      if (!id.HasValue)
        return (ActionResult) this.Json((object) new
        {
          deleteStatus = false,
          response = "Failed! ID not supplied"
        });
      visitor_details entity = this._dbContext.visitor_details.Find(new object[1]
      {
        (object) id
      });
      if (entity == null)
        return (ActionResult) this.Json((object) new
        {
          deleteStatus = false,
          response = "Failed! Item does not exist"
        });
      try
      {
        this._dbContext.visitor_details.Remove(entity);
        this._dbContext.SaveChanges();
      }
      catch (Exception ex)
      {
        return (ActionResult) this.Json((object) new
        {
          deleteStatus = false,
          response = "Record cannot be delete, it is being used by other records."
        });
      }
      return (ActionResult) this.Json((object) new
      {
        deleteStatus = true,
        response = "Record successful deleted."
      });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("Visitors/AjaxHelperSmsAllForm/{id}")]
    public ActionResult AjaxHelperSmsAllForm(long? id)
    {
      if (!id.HasValue)
        return Json(new
        {
          SmsStatus = false,
          response = "Failed! ID not supplied"
        });
      long? event_id = id;
      var list = this._dbContext.visitor_details.Include(c => c.card_state_master).Where(iv => iv.event_det_sno == event_id).ToList();
          list.FirstOrDefault();
      sms_invitation smsInvitation = this._dbContext.sms_invitation.Where(iv => iv.event_det_sno == event_id).First();
      List<string> stringList = new List<string>();
      if (smsInvitation == null)
        return Json( new
        {
          SmsStatus = false,
          response = "SMS Template was not found, Please define before sending SMS."
        });
      if (list != null)
      {
        foreach (visitor_details visitorDetails in list)
        {
          stringList.Add(this.GetInviteeWelcomeText(visitorDetails.visitor_name, visitorDetails.qrcode, visitorDetails.no_of_persons, event_id));
          string inviteeWelcomeText = this.GetInviteeWelcomeText(visitorDetails.visitor_name, visitorDetails.qrcode, visitorDetails.no_of_persons, event_id);
          this.SendLocalInviteeWelcomeSMS(visitorDetails.mobile_no, inviteeWelcomeText);
        }
      }
      return (ActionResult) this.Json((object) new
      {
        SmsStatus = true,
        response = "All SMS were successful Sent."
      });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("Visitors/AjaxHelperSmsForm/{id}/{event_sno}")]
    public ActionResult AjaxHelperSmsForm(long? visitor_det_sno, long? event_det_sno)
    {
      if (!visitor_det_sno.HasValue)
        return (ActionResult) this.Json((object) new
        {
          SmsStatus = false,
          response = "Failed! ID not supplied"
        });
      if (!event_det_sno.HasValue)
        return (ActionResult) this.Json((object) new
        {
          SmsStatus = false,
          response = "Failed! Item does not exist"
        });
      long? event_id = event_det_sno;
      var visitor_Details = this._dbContext.visitor_details.Include(c => c.card_state_master).Where(iv => iv.event_det_sno == event_id  && iv.visitor_det_sno == visitor_det_sno).FirstOrDefault();
            
          //long visitorDetSno = visitorDetails.visitor_det_sno;
          long? nullable = visitor_det_sno;
          long valueOrDefault = nullable.GetValueOrDefault();
      var smsInvitation = this._dbContext.sms_invitation.Where(iv => iv.event_det_sno == event_id).First();
      List<string> stringList = new List<string>();
      if (smsInvitation == null)
        return (ActionResult) this.Json((object) new
        {
          SmsStatus = false,
          response = "SMS Template was not found, Please define before sending SMS."
        });
      if (visitor_Details != null)
      {
        /*foreach (var visitorDetails in list)
        {*/
          if (visitor_Details.visitor_det_sno  == visitor_det_sno)
          {
            stringList.Add(this.GetInviteeWelcomeText(visitor_Details.visitor_name, visitor_Details.qrcode, visitor_Details.no_of_persons, event_id));
            string inviteeWelcomeText = this.GetInviteeWelcomeText(visitor_Details.visitor_name, visitor_Details.qrcode, visitor_Details.no_of_persons, event_id);
            this.SendLocalInviteeWelcomeSMS(visitor_Details.mobile_no, inviteeWelcomeText);
          }
        //}
      }
      return (ActionResult) this.Json((object) new
      {
        SmsStatus = true,
        response = "SMS was successful Sent."
      });
    }

    [HttpPost]
    [Route("send-sms")]
    public JsonResult SendSMS(long? id, long? event_sno)
    {
      List<visitor_details> list = this._dbContext.visitor_details.Include(c => c.card_state_master).Where(iv => iv.event_det_sno == event_sno).ToList();
            list.FirstOrDefault<visitor_details>();
      List<string> stringList = new List<string>();
      if (list != null)
      {
        foreach (visitor_details visitorDetails in list)
        {
          long visitorDetSno = visitorDetails.visitor_det_sno;
          long? nullable = id;
          long valueOrDefault = nullable.GetValueOrDefault();
          if (visitorDetSno == valueOrDefault & nullable.HasValue)
          {
            stringList.Add(this.GetInviteeWelcomeText(visitorDetails.visitor_name, visitorDetails.qrcode, visitorDetails.no_of_persons, event_sno));
            string inviteeWelcomeText = this.GetInviteeWelcomeText(visitorDetails.visitor_name, visitorDetails.qrcode, visitorDetails.no_of_persons, event_sno);
            this.SendLocalInviteeWelcomeSMS(visitorDetails.mobile_no, inviteeWelcomeText);
          }
        }
      }
      return this.Json((object) new
      {
        status = "done",
        sms_list = stringList
      }, JsonRequestBehavior.AllowGet);
    }

    public ActionResult SendWelcomeSMS(
      string __RequestVerificationToken,
      long event_id = 4,
      long visitor_id = 660)
    {
      List<visitor_details> list = this._dbContext.visitor_details.Include<visitor_details, card_state_master>((Expression<Func<visitor_details, card_state_master>>) (c => c.card_state_master)).Where<visitor_details>((Expression<Func<visitor_details, bool>>) (iv => iv.event_det_sno == (long?) event_id && iv.visitor_det_sno == visitor_id)).ToList<visitor_details>();
      if (list.Count == 0)
        return (ActionResult) this.Json((object) new
        {
          sendStatus = 0,
          message = "Failed! SMS failed to be sent."
        }, JsonRequestBehavior.AllowGet);
      var visitorList = list.Select((v, Sno) => new
      {
        mobileNumber = v.mobile_no,
        visitorName = v.visitor_name,
        eventName = v.event_details.event_name,
        eventDateTime = Convert.ToDateTime((object) v.event_details.event_start_time).ToString("dd/MM/yyyy HH:mm"),
        venue = v.event_details.venue,
        qrCodeIdentity = this.GetQrCodeRandomAlphanumeric(v.qrcode)
      }).ToArray();
      foreach (var data in visitorList)
        this.SendWelcomeSMSAction("0672515085", data.visitorName, data.qrCodeIdentity);
      return (ActionResult) this.Json((object) new
      {
        visitorName = visitorList[0].visitorName,
        sendStatus = 1,
        message = "Successfully, sent SMS to an invitees."
      }, JsonRequestBehavior.AllowGet);
    }

    public ActionResult SendBulkWelcomeSMS(string __RequestVerificationToken = "", long event_id = 4)
    {
      List<visitor_details> list = this._dbContext.visitor_details.Include(c => c.card_state_master).Where<visitor_details>(iv => iv.event_det_sno == event_id).ToList();
      if (list.Count == 0)
        return (ActionResult) this.Json((object) new
        {
          sendStatus = 0,
          message = "Failed! SMS failed to be sent."
        }, JsonRequestBehavior.AllowGet);
      var visitorList = list.Select((v, Sno) => new
      {
        mobileNumber = v.mobile_no,
        visitorName = v.visitor_name,
        eventName = v.event_details.event_name,
        eventDateTime = Convert.ToDateTime((object) v.event_details.event_start_time).ToString("dd/MM/yyyy HH:mm"),
        venue = v.event_details.venue,
        qrCodeIdentity = this.GetQrCodeRandomAlphanumeric(v.qrcode)
      });
      foreach (var data in visitorList)
        this.SendWelcomeSMSAction(data.mobileNumber, data.visitorName, data.qrCodeIdentity);
      return (ActionResult) this.Json((object) new
      {
        formatedVisitorData = visitorList,
        sendStatus = 1,
        message = "Successfully, sent SMS to all invitees."
      }, JsonRequestBehavior.AllowGet);
    }

    private long? GetCustomerAdminID() => new long?(Convert.ToInt64(this.Session["EventAdminID"]));

    private long? GetEventID() => new long?(Convert.ToInt64(this.Session["EventID"]));

    private string GetPostedBy() => this.Session["UserID"].ToString();

    private string GetInviteeWelcomeText(
      string inviteeName,
      string qrCodeIdentity,
      int? card_size,
      long? event_id)
    {
      var textInfo = new CultureInfo("en-US", false).TextInfo;
     
    
      string smsText = _dbContext.sms_invitation.Where(c =>c.event_details.event_det_sno == event_id)
                .Select(e => e.sms_text.ToString()).First(); // Unable to render the statement
      string titleCase = textInfo.ToTitleCase(inviteeName.ToLower());
      string newValue = qrCodeIdentity.Substring(9);
      int? nullable = card_size;
      int num = 1;
      return (nullable.GetValueOrDefault() == num & nullable.HasValue) ? string.Format(smsText.Replace("{qr_code}", newValue).Replace("{invitee_name}", titleCase).Replace("{card_size}", "(Single - "+ card_size.ToString()) +" )" ) : string.Format(smsText.Replace("{qr_code}", newValue).Replace("{invitee_name}", titleCase).Replace("{card_size}", "(Double - "  + card_size.ToString())+ ") ");
    }

    private void SendLocalInviteeWelcomeSMS(string visitorMobileNumber, string sms_text)
    {
      try
      {
        SMS_SETTNG smtpConfig = this.smst.getSMTPConfig();
        this.stp.getSMTPText();
        string userName = smtpConfig.USER_Name;
        string str1 = this.DecodeFrom64(smtpConfig.Password);
        string fromAddress = smtpConfig.From_Address;
        string str2 = visitorMobileNumber;
        string str3 = HttpUtility.UrlEncode(sms_text);
        WebRequest webRequest = WebRequest.Create("http://api.infobip.com/api/v3/sendsms/plain?user=" + userName + "&password=" + str1 + "&sender=" + fromAddress + "&SMSText=" + str3 + "&GSM=" + str2 + "&type=longSMS");
        webRequest.Method = "POST";
        new StreamReader(webRequest.GetResponse().GetResponseStream(), Encoding.Default).ReadToEnd();
      }
      catch (Exception ex)
      {
        ex.Message.ToString();
      }
    }

    private static bool IsLocalMobileNumber(string mobile_number)
    {
      return mobile_number.Replace("+", "").Substring(0, 3) == "255";
    }

    private void SendWelcomeSMSAction(
      string visitorMobileNumber,
      string inviteeName,
      string qrCodeIdentity)
    {
      string str1 = "kwa huduma ya Card za mwaliko za kidijitali wasiliana nasi kwa namba 0684831846. Biz-Logic Solutions Ltd.";
      if (!(visitorMobileNumber.Substring(0, 4) != "0000"))
        return;
      try
      {
        SMS_SETTNG smtpConfig = this.smst.getSMTPConfig();
        this.stp.getSMTPText();
        string userName = smtpConfig.USER_Name;
        string str2 = this.DecodeFrom64(smtpConfig.Password);
        string fromAddress = smtpConfig.From_Address;
        string str3 = this.ReplaceFirstOccurrence(visitorMobileNumber.Trim(), "0", "255");
        string str4 = HttpUtility.UrlEncode(str1.Replace("}Invitee_name{", inviteeName).Replace("}qr_code_id{", qrCodeIdentity).Replace("{", "").Replace("}", ""));
        WebRequest webRequest = WebRequest.Create("http://api.infobip.com/api/v3/sendsms/plain?user=" + userName + "&password=" + str2 + "&sender=" + fromAddress + "&SMSText=" + str4 + "&GSM=" + str3 + "&type=longSMS");
        webRequest.Method = "POST";
        new StreamReader(webRequest.GetResponse().GetResponseStream(), Encoding.Default).ReadToEnd();
      }
      catch (Exception ex)
      {
        ex.Message.ToString();
      }
    }

    private string SmsEscapeString(string stringWithCharactersToEscape)
    {
      string str = stringWithCharactersToEscape;
      if (!string.IsNullOrEmpty(str))
        str = str.Replace("'", "&apos;").Replace("\"", "&quot;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("&", "&amp;");
      return str;
    }

    private string DecodeFrom64(string password)
    {
      string empty = string.Empty;
      Decoder decoder = new UTF8Encoding().GetDecoder();
      byte[] bytes = Convert.FromBase64String(password);
      char[] chars = new char[decoder.GetCharCount(bytes, 0, bytes.Length)];
      decoder.GetChars(bytes, 0, bytes.Length, chars, 0);
      return new string(chars);
    }

    private string ReplaceFirstOccurrence(string Source, string Find, string Replace)
    {
      int startIndex = Source.IndexOf(Find);
      return Source.Remove(startIndex, Find.Length).Insert(startIndex, Replace);
    }

    private string ReplaceAllOccurrence(string Source, string Find, string Replace)
    {
      int startIndex = Source.IndexOf(Find);
      return startIndex != -1 ? Source.Remove(startIndex, Find.Length).Insert(startIndex, Replace) : Source;
    }

    private string GetQrCodeRandomAlphanumeric(string qrCodeIdentity)
    {
      return qrCodeIdentity.Substring(9);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
        this._dbContext.Dispose();
      base.Dispose(disposing);
    }
  }
}
