// Decompiled with JetBrains decompiler
// Type: FUNDING.Controllers.CardDesignController
// Assembly: FUNDING, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5DE1C1E8-8D80-43BD-915D-F2502219BD58
// Assembly location: D:\ECardtest - 22082024\bin\FUNDING.dll

using ECARD.DL.EDMX;
using FUNDING.BL.BusinessEntities.Masters;
using FUNDING.Models.AppHelpers;
using FUNDING.Models.CardGenerationModule;
using FUNDING.Models.CardGenerationModule.DataLayer;
using FUNDING.Models.CardGenerationModule.QR_Code;
using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Web.Hosting;
using System.Web.Mvc;

 
namespace FUNDING.Controllers
{
  public class CardDesignController : Controller
  {
    private readonly ECARDAPPEntities _dbContext = new ECARDAPPEntities();
    private readonly CustomerUsers _users = new CustomerUsers();
    private List<FUNDING.Models.CardGenerationModule.DataLayer.CardTemplates> _ListOfCardTemplatesVirtualPath;
    private CardEventDetailsViewModel _EventDetails;
    private List<FontFamily> _AllFonts;

    public ActionResult Index()
    {
            var event_id = 2;
            _ListOfCardTemplatesVirtualPath = GetAllCardTemplatesVirtualPath();
            _EventDetails = GetEventDetails(event_id);
            _AllFonts = GetAllFontsDetails();

            ViewBag.DefaultCardTemplate = DirectoryHelpers.GetDefaultCardTemplate();
            ViewBag.DefaultQRCode = DirectoryHelpers.GetDefaultQRCode();
            ViewBag.DefaultFont = DirectoryHelpers.GetDefaultFont();

         //   object obj3 = CardDesignController.\u003C\u003Eo__5.\u003C\u003Ep__2.Target((CallSite) CardDesignController.\u003C\u003Eo__5.\u003C\u003Ep__2, this.ViewBag, DirectoryHelpers.GetDefaultFont());
      return (ActionResult) this.View((object) new CardDesignMasterViewModel()
      {
        CardTemplatesList = (IEnumerable<FUNDING.Models.CardGenerationModule.DataLayer.CardTemplates>) this._ListOfCardTemplatesVirtualPath,
        EventDetails = this._EventDetails,
        AllFontFamilies = (IEnumerable<FontFamily>) this._AllFonts
      });
    }

    [ChildActionOnly]
    public ActionResult CardTemplates()
    {
      return (ActionResult) this.PartialView("_CardTemplates", (object) this._ListOfCardTemplatesVirtualPath);
    }

    [ChildActionOnly]
    public ActionResult EventDetails()
    {
      return (ActionResult) this.PartialView("_FillForm", (object) this._EventDetails);
    }

    [ChildActionOnly]
    public ActionResult FontsPartialView()
    {
      return (ActionResult) this.PartialView("_FontsPartialView", (object) this._AllFonts);
    }

    public ActionResult GenerateCards()
    {
      foreach (var invitee in DataManagement.GetBDInvitees(2L))
        CardGenerationService.DedicatedPdfGenerator(invitee);
      return (ActionResult) this.Json((object) "Cards generated.", JsonRequestBehavior.AllowGet);
    }

    public ActionResult GenerateQR()
    {
            var visitorsList = _dbContext.visitor_details.Include(v => v.event_details)
                      .Where(iv => iv.event_det_sno == 2)
                      .Select(vd => new
                      {
                          id = vd.visitor_det_sno,
                          qrCodeID = vd.qrcode,
                          inviteeName = vd.visitor_name,
                          cardSize = vd.no_of_persons.ToString(),
                          vd.event_details.venue

                      }).ToList();

            foreach (var v in visitorsList)
            {
                QRCodeGenerator.GenerateAction(v.id, v.qrCodeID, v.inviteeName, v.cardSize, v.venue);
            }

            return Json("QR code generated.", JsonRequestBehavior.AllowGet);
        }

    private string[] GetDateFormats(DateTime? event_date)
    {
      return new string[2]
      {
        Convert.ToDateTime((object) event_date).ToString("dddd, dd MMMM yyyy"),
        Convert.ToDateTime((object) event_date).ToString("dd/MM/yyyy")
      };
    }

    private string[] GetTimeFormats(DateTime? event_start_time)
    {
      return new string[2]
      {
        Convert.ToDateTime((object) event_start_time).ToString("HH:mm"),
        Convert.ToDateTime((object) event_start_time).ToString("h:mm tt")
      };
    }

    private List<FUNDING.Models.CardGenerationModule.DataLayer.CardTemplates> GetAllCardTemplatesVirtualPath()
    {
      List<FUNDING.Models.CardGenerationModule.DataLayer.CardTemplates> templatesVirtualPath = new List<FUNDING.Models.CardGenerationModule.DataLayer.CardTemplates>();
      string virtualDirectory = DirectoryHelpers.CardTemplatesVirtualDirectory;
      foreach (string file in Directory.GetFiles(HostingEnvironment.MapPath(virtualDirectory)))
      {
        string fileName = Path.GetFileName(file);
        templatesVirtualPath.Add(new FUNDING.Models.CardGenerationModule.DataLayer.CardTemplates()
        {
          CardTemplateFilePath = Path.Combine(virtualDirectory, fileName)
        });
      }
      return templatesVirtualPath;
    }

    private List<FontFamily> GetAllFontsDetails()
    {
      List<FontFamily> allFontsDetails = new List<FontFamily>();
      string virtualDirectory = DirectoryHelpers.FontFamilyVirtualDirectory;
      foreach (string file in Directory.GetFiles(HostingEnvironment.MapPath(virtualDirectory)))
      {
        string fileName = Path.GetFileName(file);
        if (Path.GetExtension(fileName) == ".ttf")
          allFontsDetails.Add(new FontFamily()
          {
            FontStylesheetFileFullVirtuaPath = Path.Combine(virtualDirectory, fileName),
            FontName = Path.GetFileNameWithoutExtension(fileName)
          });
      }
      return allFontsDetails;
    }

    private CardEventDetailsViewModel GetEventDetails(long event_id)
    {
            var eventInfo = _dbContext.event_details
                      .Where(ev => ev.event_det_sno == event_id)
                      .Select(ev => new
                      {
                          ev.inviter_name,
                          ev.event_name,
                          ev.event_date,
                          ev.event_start_time,
                          ev.venue,
                          ev.reservation
                      }).FirstOrDefault();

            if (eventInfo == null) return null;

            var dateFormats = GetDateFormats(eventInfo.event_date);
            var timeFormats = GetTimeFormats(eventInfo.event_start_time);

            var formattedEventDetailsData = new CardEventDetailsViewModel()
            {
                EventHost = TrimAll(eventInfo.inviter_name),
                EventName = TrimAll(eventInfo.event_name),
                EventDate = dateFormats,
                EventStartTime = timeFormats,
                Venue = TrimAll(eventInfo.venue),
                Reservation = TrimAll(eventInfo.reservation)
            };

            ViewBag.DateFormats = new SelectList(dateFormats);
            ViewBag.TimeFormats = new SelectList(timeFormats);

            return formattedEventDetailsData;
        }

    public ActionResult MutliField() => (ActionResult) this.View();

    [HttpPost]
    public ActionResult MutliField(ContactDetailsViewModel contact)
    {
      return (ActionResult) this.Json((object) contact, JsonRequestBehavior.AllowGet);
    }

    private string TrimAll(string event_data)
    {
      return !string.IsNullOrEmpty(event_data) ? event_data.Trim() : (string) null;
    }
  }
}
