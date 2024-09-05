// Decompiled with JetBrains decompiler
// Type: FUNDING.Controllers.CardDesignMasterController
// Assembly: FUNDING, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5DE1C1E8-8D80-43BD-915D-F2502219BD58
// Assembly location: D:\ECardtest - 22082024\bin\FUNDING.dll

using ECARD.DL.EDMX;
using FUNDING.BL.BusinessEntities.Masters;
using FUNDING.Models.AppHelpers;
using FUNDING.Models.CardGenerationModule;
using FUNDING.Models.CardGenerationModule.DataLayer;
using FUNDING.Models.CardGenerationModule.QR_Code;
using FUNDING.Models.CardGererationMaster;
using FUNDING.Models.CardGererationMaster.DraggableElements;
using FUNDING.Models.Notifications;
using Microsoft.CSharp.RuntimeBinder;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

 
namespace FUNDING.Controllers
{
  public class CardDesignMasterController : Controller
  {
    private readonly ECARDAPPEntities _dbContext = new ECARDAPPEntities();
    private readonly CustomerUsers _users = new CustomerUsers();
    private readonly List<CardTemplates> _ListOfCardTemplatesVirtualPath;
    private readonly List<FontFamily> _AllFonts;
    private readonly CardEventDetailsViewModel _EventDetails;

    public List<string>[] Lists { get; private set; }

    public CardDesignMasterController()
    {
      this._ListOfCardTemplatesVirtualPath = this.GetAllCardTemplatesVirtualPath();
      this._AllFonts = this.GetAllFontsDetails();
      this._EventDetails = new CardEventDetailsViewModel();
    }

    [Route("Card-Design")]
    public ActionResult Index()
    {
      if (this.Session["admin1"] == null)
        return (ActionResult) this.RedirectToAction("Login", "Login");
      var defaultCardTemplate = DirectoryHelpers.GetDefaultCardTemplate();
      ViewBag.DefaultCardTemplate = defaultCardTemplate;
      ViewBag.DefaultQRCode = DirectoryHelpers.GetDefaultQRCode();
      ViewBag.DefaultFont = DirectoryHelpers.GetDefaultFont();


            var qrCodeElement = new QrCodeDraggableElement
            {
                Position = new ElementPosition { LeftPosition = 0, TopPosition = 0 },
                QrCodeSize = 165F
            };

            var editableElement = new EditableDraggableElement
            {
                Position = new ElementPosition { LeftPosition = 0, TopPosition = 0 },
                FontDetails = new FontMasterClass
                {
                    Bold = false,
                    Italic = false,
                    Underline = false,
                    Color = "rgb(0, 0, 0)",
                    FontName = "Acme",
                    FontSize = 24,
                    TextAlign = "center"

                }
            };

            var tableNumberLabel = new EditableDraggableElement
            {
                Position = new ElementPosition { LeftPosition = 0, TopPosition = 0 },
                FontDetails = new FontMasterClass
                {
                    Bold = false,
                    Italic = false,
                    Underline = false,
                    Color = "rgb(0, 0, 0)",
                    FontName = "Acme",
                    FontSize = 24,
                    TextAlign = "center"

                }
            };

            var tableNumberDigit = new EditableDraggableElement
            {
                Position = new ElementPosition { LeftPosition = 0, TopPosition = 0 },
                FontDetails = new FontMasterClass
                {
                    Bold = false,
                    Italic = false,
                    Underline = false,
                    Color = "rgb(0, 0, 0)",
                    FontName = "Acme",
                    FontSize = 24,
                    TextAlign = "center"

                }
            };

       //     ViewBag.DraggableElements = new { event_id = 0, displayCardSize = false, cardTemplate = defaultCardTemplate, qrCodeElement, editableElement, tableNumberLabel, tableNumberDigit };

            ViewBag.DraggableElements = new { event_id = 0, displayCardSize = false, cardTemplate = defaultCardTemplate, qrCodeElement, editableElement, tableNumberLabel, tableNumberDigit };

      var modelData = new CardDesignMasterViewModel()
      {
        CardTemplatesList = _ListOfCardTemplatesVirtualPath,
        EventDetails = _EventDetails,
        AllFontFamilies = _AllFonts
      };

            ViewBag.FontStyle = _AllFonts;

            ViewBag.EventsList = GetAllActiveEvents();
            ViewBag.Invitee = AllInvitee(40042);


        return View(modelData);
    }

    private List<SelectListItem> GetAllActiveEvents()
    {
      List<SelectListItem> allActiveEvents = new List<SelectListItem>();
      foreach (event_details eventDetails in this._dbContext.event_details.ToList<event_details>())
      {
        if (Convert.ToDateTime((object) eventDetails.event_date).Date >= DateTime.Now.Date)
          allActiveEvents.Add(new SelectListItem()
          {
            Value = eventDetails.event_det_sno.ToString(),
            Text = eventDetails.event_name
          });
      }
      return allActiveEvents;
    }

    [HttpPost]
    [Route("carddesignmaster/AllInvitee/{event_id}")]
    public ActionResult AllInvitee(long event_id)
    {
      List<SelectListItem> selectListItemList = new List<SelectListItem>();
      var visitor_details = this._dbContext.visitor_details.Where(ev => ev.event_det_sno == event_id).Select(vd => new
      {
        visitor_id = vd.visitor_det_sno,
        event_det_sno = vd.event_det_sno,
        visitor_name = vd.visitor_name,
        event_name = vd.event_details.event_name,
        qrcode = vd.qrcode,
        card_size = vd.no_of_persons,
        venue = vd.event_details.venue,
        table_number = vd.table_number
      }).ToList();
      if (visitor_details == null)
        return (ActionResult) this.Json((object) new
        {
          data = ""
        });
      foreach (var data in visitor_details)
      {
        selectListItemList.Add(new SelectListItem()
        {
          Value = data.visitor_id.ToString(),
          Text = data.visitor_name
        });

        ViewBag.Invitee = selectListItemList;

      }
      return Json(new
      {
        visitorSelectionList = selectListItemList
      });
    }

    [HttpPost]
    [Route("carddesignmaster/Measurements/{event_id}")]
    public ActionResult Measurements(long event_id)
    {
      List<SelectListItem> selectListItemList = new List<SelectListItem>();
      QrCodeDraggableElement draggableElement1 = new QrCodeDraggableElement();
      EditableDraggableElement draggableElement2 = new EditableDraggableElement();
      EditableDraggableElement draggableElement3 = new EditableDraggableElement();
      EditableDraggableElement draggableElement4 = new EditableDraggableElement();
      EditableDraggableElement draggableElement5 = new EditableDraggableElement();
      card_measurement cardMeasurement = this._dbContext.card_measurement.Where(m => m.event_det_sno == event_id).FirstOrDefault();
      if (cardMeasurement == null)
      {
        var defaultCardTemplate = DirectoryHelpers.GetDefaultCardTemplate();
        ViewBag.DefaultCardTemplate = defaultCardTemplate;

        ViewBag.DefaultQRCode = DirectoryHelpers.GetDefaultQRCode();
        ViewBag.DefailtFont = DirectoryHelpers.GetDefaultFont();

        QrCodeDraggableElement draggableElement6 = new QrCodeDraggableElement();
        ElementPosition elementPosition = new ElementPosition();
        elementPosition.LeftPosition = 0.0;
        elementPosition.TopPosition = 0.0;
        draggableElement6.Position = elementPosition;
        draggableElement6.QrCodeSize = 165f;
        QrCodeDraggableElement draggableElement7 = draggableElement6;
        EditableDraggableElement draggableElement8 = new EditableDraggableElement();
        elementPosition = new ElementPosition();
        elementPosition.LeftPosition = 0.0;
        elementPosition.TopPosition = 0.0;
        draggableElement8.Position = elementPosition;
        FontMasterClass fontMasterClass = new FontMasterClass();
        fontMasterClass.Bold = false;
        fontMasterClass.Italic = false;
        fontMasterClass.Underline = false;
        fontMasterClass.Color = "rgb(0, 0, 0)";
        fontMasterClass.FontName = "Acme";
        fontMasterClass.FontSize = 24f;
        fontMasterClass.TextAlign = "center";
        draggableElement8.FontDetails = fontMasterClass;
        EditableDraggableElement draggableElement9 = draggableElement8;
        EditableDraggableElement draggableElement10 = new EditableDraggableElement();
        elementPosition = new ElementPosition();
        elementPosition.LeftPosition = 0.0;
        elementPosition.TopPosition = 0.0;
        draggableElement10.Position = elementPosition;
        fontMasterClass = new FontMasterClass();
        fontMasterClass.Bold = false;
        fontMasterClass.Italic = false;
        fontMasterClass.Underline = false;
        fontMasterClass.Color = "rgb(0, 0, 0)";
        fontMasterClass.FontName = "Acme";
        fontMasterClass.FontSize = 24f;
        fontMasterClass.TextAlign = "center";
        draggableElement10.FontDetails = fontMasterClass;
        EditableDraggableElement draggableElement11 = draggableElement10;
        EditableDraggableElement draggableElement12 = new EditableDraggableElement();
        elementPosition = new ElementPosition();
        elementPosition.LeftPosition = 0.0;
        elementPosition.TopPosition = 0.0;
        draggableElement12.Position = elementPosition;
        fontMasterClass = new FontMasterClass();
        fontMasterClass.Bold = false;
        fontMasterClass.Italic = false;
        fontMasterClass.Underline = false;
        fontMasterClass.Color = "rgb(0, 0, 0)";
        fontMasterClass.FontName = "Acme";
        fontMasterClass.FontSize = 24f;
        fontMasterClass.TextAlign = "center";
        draggableElement12.FontDetails = fontMasterClass;
        EditableDraggableElement draggableElement13 = draggableElement12;
        EditableDraggableElement draggableElement14 = new EditableDraggableElement();
        elementPosition = new ElementPosition();
        elementPosition.LeftPosition = 0.0;
        elementPosition.TopPosition = 0.0;
        draggableElement14.Position = elementPosition;
        fontMasterClass = new FontMasterClass();
        fontMasterClass.Bold = false;
        fontMasterClass.Italic = false;
        fontMasterClass.Underline = false;
        fontMasterClass.Color = "rgb(0, 0, 0)";
        fontMasterClass.FontName = "Acme";
        fontMasterClass.FontSize = 24f;
        fontMasterClass.TextAlign = "center";
        draggableElement14.FontDetails = fontMasterClass;
        EditableDraggableElement draggableElement15 = draggableElement14;
        return Json( new
        {
          data = 0,
          response = new
          {
            event_id = 0,
            displayCardSize = false,
            cardTemplate = defaultCardTemplate,
            qrCodeElement = draggableElement7,
            editableElement = draggableElement9,
            cardSizeLabel = draggableElement13,
            tableNumberLabel = draggableElement11,
            tableNumberDigit = draggableElement15,
            Visitors = selectListItemList
          }
        });
      }

      ViewBag.DefaultCardTemplate = cardMeasurement.template;

      QrCodeDraggableElement draggableElement16 = new QrCodeDraggableElement();
      ElementPosition elementPosition1 = new ElementPosition();
      elementPosition1.LeftPosition = Convert.ToDouble(cardMeasurement.qrleft);
      elementPosition1.TopPosition = Convert.ToDouble(cardMeasurement.qrtop);
      draggableElement16.Position = elementPosition1;
      draggableElement16.QrCodeSize = Convert.ToSingle((object) cardMeasurement.qrcodesize);
      QrCodeDraggableElement draggableElement17 = draggableElement16;
      EditableDraggableElement draggableElement18 = new EditableDraggableElement();
      elementPosition1 = new ElementPosition();
      elementPosition1.LeftPosition = Convert.ToDouble(cardMeasurement.element_left);
      elementPosition1.TopPosition = Convert.ToDouble(cardMeasurement.element_top);
      draggableElement18.Position = elementPosition1;
      FontMasterClass fontMasterClass1 = new FontMasterClass();
      fontMasterClass1.Bold = Convert.ToBoolean(cardMeasurement.element_bold);
      fontMasterClass1.Italic = Convert.ToBoolean(cardMeasurement.element_italic);
      fontMasterClass1.Underline = Convert.ToBoolean(cardMeasurement.element_underline);
      fontMasterClass1.Color = cardMeasurement.element_color;
      fontMasterClass1.FontName = cardMeasurement.element_font_name;
      fontMasterClass1.FontSize = Convert.ToSingle((object) cardMeasurement.element_font_size);
      fontMasterClass1.TextAlign = cardMeasurement.element_textalign;
      draggableElement18.FontDetails = fontMasterClass1;
      EditableDraggableElement draggableElement19 = draggableElement18;
      EditableDraggableElement draggableElement20 = new EditableDraggableElement();
      draggableElement20.Position = new ElementPosition()
      {
        LeftPosition = Convert.ToDouble(cardMeasurement.label_left),
        TopPosition = Convert.ToDouble(cardMeasurement.label_top)
      };
      fontMasterClass1 = new FontMasterClass();
      fontMasterClass1.Bold = Convert.ToBoolean(cardMeasurement.label_bold);
      fontMasterClass1.Italic = Convert.ToBoolean(cardMeasurement.label_italic);
      fontMasterClass1.Underline = Convert.ToBoolean(cardMeasurement.label_underline);
      fontMasterClass1.Color = cardMeasurement.label_color;
      fontMasterClass1.FontName = cardMeasurement.label_font_name;
      fontMasterClass1.FontSize = Convert.ToSingle((object) cardMeasurement.label_font_size);
      fontMasterClass1.TextAlign = cardMeasurement.label_textalign;
      draggableElement20.FontDetails = fontMasterClass1;
      EditableDraggableElement draggableElement21 = draggableElement20;
      EditableDraggableElement draggableElement22 = new EditableDraggableElement();
      draggableElement22.Position = new ElementPosition()
      {
        LeftPosition = Convert.ToDouble(cardMeasurement.cardsize_left),
        TopPosition = Convert.ToDouble(cardMeasurement.cardsize_top)
      };
      fontMasterClass1 = new FontMasterClass();
      fontMasterClass1.Bold = Convert.ToBoolean(cardMeasurement.cardsize_bold);
      fontMasterClass1.Italic = Convert.ToBoolean(cardMeasurement.cardsize_italic);
      fontMasterClass1.Underline = false;
      fontMasterClass1.Color = cardMeasurement.cardsize_color;
      fontMasterClass1.FontName = cardMeasurement.cardsize_font_name;
      fontMasterClass1.FontSize = Convert.ToSingle((object) cardMeasurement.cardsize_font_size);
      fontMasterClass1.TextAlign = cardMeasurement.cardsize_textalign;
      draggableElement22.FontDetails = fontMasterClass1;
      EditableDraggableElement draggableElement23 = draggableElement22;
      EditableDraggableElement draggableElement24 = new EditableDraggableElement();
      draggableElement24.Position = new ElementPosition()
      {
        LeftPosition = Convert.ToDouble(cardMeasurement.digit_left),
        TopPosition = Convert.ToDouble(cardMeasurement.digit_top)
      };
      fontMasterClass1 = new FontMasterClass();
      fontMasterClass1.Bold = Convert.ToBoolean(cardMeasurement.digit_bold);
      fontMasterClass1.Italic = Convert.ToBoolean(cardMeasurement.digit_italic);
      fontMasterClass1.Underline = false;
      fontMasterClass1.Color = cardMeasurement.digit_color;
      fontMasterClass1.FontName = cardMeasurement.digit_font_name;
      fontMasterClass1.FontSize = Convert.ToSingle((object) cardMeasurement.digit_font_size);
      fontMasterClass1.TextAlign = cardMeasurement.digit_textalign;
      draggableElement24.FontDetails = fontMasterClass1;
      EditableDraggableElement draggableElement25 = draggableElement24;

     ViewBag.DraggableElement = new
      {
        event_id = event_id,
        displayCardSize = cardMeasurement.card_size,
        cardTemplate = cardMeasurement.template,
        qrCodeElement = draggableElement17,
        editableElement = draggableElement19,
        cardSizeLabel = draggableElement23,
        tableNumberLabel = draggableElement21,
        tableNumberDigit = draggableElement25,
        Visitors = selectListItemList
      };


      return Json( new
      {
        data = 1,
        ViewBag.DraggableElement,
        response = new
        {
          event_id = event_id,
          displayCardSize = cardMeasurement.card_size,
          cardTemplate = cardMeasurement.template,
          qrCodeElement = draggableElement17,
          editableElement = draggableElement19,
          cardSizeLabel = draggableElement23,
          tableNumberLabel = draggableElement21,
          tableNumberDigit = draggableElement25,
          Visitors = selectListItemList
        }
      });
    }

    [HttpPost]
    [Route("carddesignmaster/MeasurementHandler")]
    public ActionResult MeasurementHandler(
      long event_id,
      bool displayCardSize,
      string cardTemplate,
      EditableDraggableElement editableElement,
      EditableDraggableElement cardSizeLabel,
      EditableDraggableElement tableNumberLabel,
      EditableDraggableElement tableNumberDigit,
      QrCodeDraggableElement qrCodeElement)
    {
      if (this.Session["admin1"] == null)
        return Json( new
        {
          loginStatus = false
        });
      card_measurement cardMeasurement1 = this._dbContext.card_measurement.Where(m => m.event_det_sno == (long?) event_id).FirstOrDefault();
      if (cardMeasurement1 != null)
      {
        cardMeasurement1.card_size = displayCardSize.ToString();
        cardMeasurement1.template = cardTemplate;
        cardMeasurement1.event_det_sno = new long?(event_id);
        cardMeasurement1.posted_by = this.Session["UserID"].ToString();
        cardMeasurement1.posted_date = new DateTime?(DateTime.Now);
        cardMeasurement1.qrcodesize = new int?(Convert.ToInt32(qrCodeElement.QrCodeSize));
        cardMeasurement1.qrleft = qrCodeElement.Position.LeftPosition.ToString();
        cardMeasurement1.qrtop = qrCodeElement.Position.TopPosition.ToString();
        cardMeasurement1.cardsize_bold = cardSizeLabel.FontDetails.Bold.ToString();
        card_measurement cardMeasurement2 = cardMeasurement1;
        FontMasterClass fontDetails1 = cardSizeLabel.FontDetails;
        string color1 = fontDetails1.Color;
        cardMeasurement2.cardsize_color = color1;
        card_measurement cardMeasurement3 = cardMeasurement1;
        fontDetails1 = cardSizeLabel.FontDetails;
        string fontName1 = fontDetails1.FontName;
        cardMeasurement3.cardsize_font_name = fontName1;
        cardMeasurement1.cardsize_font_size = new int?(Convert.ToInt32(cardSizeLabel.FontDetails.FontSize));
        card_measurement cardMeasurement4 = cardMeasurement1;
        bool flag = cardSizeLabel.FontDetails.Italic;
        string str1 = flag.ToString();
        cardMeasurement4.cardsize_italic = str1;
        card_measurement cardMeasurement5 = cardMeasurement1;
        double num = cardSizeLabel.Position.LeftPosition;
        string str2 = num.ToString();
        cardMeasurement5.cardsize_left = str2;
        cardMeasurement1.cardsize_textalign = cardSizeLabel.FontDetails.TextAlign;
        card_measurement cardMeasurement6 = cardMeasurement1;
        num = cardSizeLabel.Position.TopPosition;
        string str3 = num.ToString();
        cardMeasurement6.cardsize_top = str3;
        card_measurement cardMeasurement7 = cardMeasurement1;
        FontMasterClass fontDetails2 = cardSizeLabel.FontDetails;
        flag = fontDetails2.Underline;
        string str4 = flag.ToString();
        cardMeasurement7.cardsize_underline = str4;
        card_measurement cardMeasurement8 = cardMeasurement1;
        fontDetails2 = tableNumberDigit.FontDetails;
        flag = fontDetails2.Bold;
        string str5 = flag.ToString();
        cardMeasurement8.digit_bold = str5;
        card_measurement cardMeasurement9 = cardMeasurement1;
        fontDetails2 = tableNumberDigit.FontDetails;
        string color2 = fontDetails2.Color;
        cardMeasurement9.digit_color = color2;
        card_measurement cardMeasurement10 = cardMeasurement1;
        fontDetails2 = tableNumberDigit.FontDetails;
        string fontName2 = fontDetails2.FontName;
        cardMeasurement10.digit_font_name = fontName2;
        card_measurement cardMeasurement11 = cardMeasurement1;
        fontDetails2 = tableNumberDigit.FontDetails;
        int? nullable1 = new int?(Convert.ToInt32(fontDetails2.FontSize));
        cardMeasurement11.digit_font_size = nullable1;
        card_measurement cardMeasurement12 = cardMeasurement1;
        fontDetails2 = tableNumberDigit.FontDetails;
        flag = fontDetails2.Italic;
        string str6 = flag.ToString();
        cardMeasurement12.digit_italic = str6;
        card_measurement cardMeasurement13 = cardMeasurement1;
        num = tableNumberDigit.Position.LeftPosition;
        string str7 = num.ToString();
        cardMeasurement13.digit_left = str7;
        card_measurement cardMeasurement14 = cardMeasurement1;
        fontDetails2 = tableNumberDigit.FontDetails;
        string textAlign1 = fontDetails2.TextAlign;
        cardMeasurement14.digit_textalign = textAlign1;
        card_measurement cardMeasurement15 = cardMeasurement1;
        num = tableNumberDigit.Position.TopPosition;
        string str8 = num.ToString();
        cardMeasurement15.digit_top = str8;
        card_measurement cardMeasurement16 = cardMeasurement1;
        fontDetails2 = tableNumberDigit.FontDetails;
        flag = fontDetails2.Underline;
        string str9 = flag.ToString();
        cardMeasurement16.digit_underline = str9;
        card_measurement cardMeasurement17 = cardMeasurement1;
        fontDetails2 = tableNumberLabel.FontDetails;
        flag = fontDetails2.Bold;
        string str10 = flag.ToString();
        cardMeasurement17.label_bold = str10;
        card_measurement cardMeasurement18 = cardMeasurement1;
        fontDetails2 = tableNumberLabel.FontDetails;
        string color3 = fontDetails2.Color;
        cardMeasurement18.label_color = color3;
        card_measurement cardMeasurement19 = cardMeasurement1;
        fontDetails2 = tableNumberLabel.FontDetails;
        string fontName3 = fontDetails2.FontName;
        cardMeasurement19.label_font_name = fontName3;
        card_measurement cardMeasurement20 = cardMeasurement1;
        fontDetails2 = tableNumberLabel.FontDetails;
        int? nullable2 = new int?(Convert.ToInt32(fontDetails2.FontSize));
        cardMeasurement20.label_font_size = nullable2;
        card_measurement cardMeasurement21 = cardMeasurement1;
        fontDetails2 = tableNumberLabel.FontDetails;
        flag = fontDetails2.Italic;
        string str11 = flag.ToString();
        cardMeasurement21.label_italic = str11;
        card_measurement cardMeasurement22 = cardMeasurement1;
        num = tableNumberLabel.Position.LeftPosition;
        string str12 = num.ToString();
        cardMeasurement22.label_left = str12;
        card_measurement cardMeasurement23 = cardMeasurement1;
        fontDetails2 = tableNumberLabel.FontDetails;
        string textAlign2 = fontDetails2.TextAlign;
        cardMeasurement23.label_textalign = textAlign2;
        card_measurement cardMeasurement24 = cardMeasurement1;
        num = tableNumberLabel.Position.TopPosition;
        string str13 = num.ToString();
        cardMeasurement24.label_top = str13;
        card_measurement cardMeasurement25 = cardMeasurement1;
        fontDetails2 = tableNumberLabel.FontDetails;
        flag = fontDetails2.Underline;
        string str14 = flag.ToString();
        cardMeasurement25.label_underline = str14;
        card_measurement cardMeasurement26 = cardMeasurement1;
        fontDetails2 = editableElement.FontDetails;
        flag = fontDetails2.Bold;
        string str15 = flag.ToString();
        cardMeasurement26.element_bold = str15;
        card_measurement cardMeasurement27 = cardMeasurement1;
        fontDetails2 = editableElement.FontDetails;
        string color4 = fontDetails2.Color;
        cardMeasurement27.element_color = color4;
        card_measurement cardMeasurement28 = cardMeasurement1;
        fontDetails2 = editableElement.FontDetails;
        string fontName4 = fontDetails2.FontName;
        cardMeasurement28.element_font_name = fontName4;
        card_measurement cardMeasurement29 = cardMeasurement1;
        fontDetails2 = editableElement.FontDetails;
        int? nullable3 = new int?(Convert.ToInt32(fontDetails2.FontSize));
        cardMeasurement29.element_font_size = nullable3;
        card_measurement cardMeasurement30 = cardMeasurement1;
        fontDetails2 = editableElement.FontDetails;
        flag = fontDetails2.Italic;
        string str16 = flag.ToString();
        cardMeasurement30.element_italic = str16;
        card_measurement cardMeasurement31 = cardMeasurement1;
        num = editableElement.Position.LeftPosition;
        string str17 = num.ToString();
        cardMeasurement31.element_left = str17;
        card_measurement cardMeasurement32 = cardMeasurement1;
        fontDetails2 = editableElement.FontDetails;
        string textAlign3 = fontDetails2.TextAlign;
        cardMeasurement32.element_textalign = textAlign3;
        card_measurement cardMeasurement33 = cardMeasurement1;
        num = editableElement.Position.TopPosition;
        string str18 = num.ToString();
        cardMeasurement33.element_top = str18;
        card_measurement cardMeasurement34 = cardMeasurement1;
        fontDetails2 = editableElement.FontDetails;
        flag = fontDetails2.Underline;
        string str19 = flag.ToString();
        cardMeasurement34.element_underline = str19;
        this._dbContext.SaveChanges();
        return (ActionResult) this.Json((object) new
        {
          data = 0,
          loginStatus = true,
          message = "Successfully Changes Saved"
        });
      }
      card_measurement entity = new card_measurement();
      entity.card_size = displayCardSize.ToString();
      entity.template = cardTemplate;
      entity.event_det_sno = new long?(event_id);
      entity.posted_by = this.Session["UserID"].ToString();
      entity.posted_date = new DateTime?(DateTime.Now);
      entity.qrcodesize = new int?(Convert.ToInt32(qrCodeElement.QrCodeSize));
      entity.qrleft = qrCodeElement.Position.LeftPosition.ToString();
      entity.qrtop = qrCodeElement.Position.TopPosition.ToString();
      FontMasterClass fontDetails = cardSizeLabel.FontDetails;
      bool flag1 = fontDetails.Bold;
      entity.cardsize_bold = flag1.ToString();
      fontDetails = cardSizeLabel.FontDetails;
      entity.cardsize_color = fontDetails.Color;
      fontDetails = cardSizeLabel.FontDetails;
      entity.cardsize_font_name = fontDetails.FontName;
      fontDetails = cardSizeLabel.FontDetails;
      entity.cardsize_font_size = new int?(Convert.ToInt32(fontDetails.FontSize));
      fontDetails = cardSizeLabel.FontDetails;
      flag1 = fontDetails.Italic;
      entity.cardsize_italic = flag1.ToString();
      entity.cardsize_left = cardSizeLabel.Position.LeftPosition.ToString();
      fontDetails = cardSizeLabel.FontDetails;
      entity.cardsize_textalign = fontDetails.TextAlign;
      entity.cardsize_top = cardSizeLabel.Position.TopPosition.ToString();
      fontDetails = cardSizeLabel.FontDetails;
      flag1 = fontDetails.Underline;
      entity.cardsize_underline = flag1.ToString();
      fontDetails = tableNumberDigit.FontDetails;
      flag1 = fontDetails.Bold;
      entity.digit_bold = flag1.ToString();
      fontDetails = tableNumberDigit.FontDetails;
      entity.digit_color = fontDetails.Color;
      fontDetails = tableNumberDigit.FontDetails;
      entity.digit_font_name = fontDetails.FontName;
      fontDetails = tableNumberDigit.FontDetails;
      entity.digit_font_size = new int?(Convert.ToInt32(fontDetails.FontSize));
      fontDetails = tableNumberDigit.FontDetails;
      flag1 = fontDetails.Italic;
      entity.digit_italic = flag1.ToString();
      entity.digit_left = tableNumberDigit.Position.LeftPosition.ToString();
      fontDetails = tableNumberDigit.FontDetails;
      entity.digit_textalign = fontDetails.TextAlign;
      entity.digit_top = tableNumberDigit.Position.TopPosition.ToString();
      fontDetails = tableNumberDigit.FontDetails;
      flag1 = fontDetails.Underline;
      entity.digit_underline = flag1.ToString();
      fontDetails = tableNumberLabel.FontDetails;
      flag1 = fontDetails.Bold;
      entity.label_bold = flag1.ToString();
      fontDetails = tableNumberLabel.FontDetails;
      entity.label_color = fontDetails.Color;
      fontDetails = tableNumberLabel.FontDetails;
      entity.label_font_name = fontDetails.FontName;
      fontDetails = tableNumberLabel.FontDetails;
      entity.label_font_size = new int?(Convert.ToInt32(fontDetails.FontSize));
      fontDetails = tableNumberLabel.FontDetails;
      flag1 = fontDetails.Italic;
      entity.label_italic = flag1.ToString();
      entity.label_left = tableNumberLabel.Position.LeftPosition.ToString();
      fontDetails = tableNumberLabel.FontDetails;
      entity.label_textalign = fontDetails.TextAlign;
      entity.label_top = tableNumberLabel.Position.TopPosition.ToString();
      fontDetails = tableNumberLabel.FontDetails;
      flag1 = fontDetails.Underline;
      entity.label_underline = flag1.ToString();
      fontDetails = editableElement.FontDetails;
      flag1 = fontDetails.Bold;
      entity.element_bold = flag1.ToString();
      fontDetails = editableElement.FontDetails;
      entity.element_color = fontDetails.Color;
      fontDetails = editableElement.FontDetails;
      entity.element_font_name = fontDetails.FontName;
      fontDetails = editableElement.FontDetails;
      entity.element_font_size = new int?(Convert.ToInt32(fontDetails.FontSize));
      fontDetails = editableElement.FontDetails;
      flag1 = fontDetails.Italic;
      entity.element_italic = flag1.ToString();
      entity.element_left = editableElement.Position.LeftPosition.ToString();
      fontDetails = editableElement.FontDetails;
      entity.element_textalign = fontDetails.TextAlign;
      entity.element_top = editableElement.Position.TopPosition.ToString();
      fontDetails = editableElement.FontDetails;
      flag1 = fontDetails.Underline;
      entity.element_underline = flag1.ToString();
      this._dbContext.card_measurement.Add(entity);
      this._dbContext.SaveChanges();
      return Json(new
      {
        data = 1,
        loginStatus = true,
        message = "Successfully Saved"
      });
    }

    [HttpPost]
    [Route("Bulk-Card-Generate-Multiple")]
    public async Task<ActionResult> BulkCardGenerateAndPreviewMultiple(
      long event_id,
      bool displayCardSize,
      string cardTemplate,
      EditableDraggableElement editableElement,
      EditableDraggableElement cardSizeLabel,
      EditableDraggableElement tableNumberLabel,
      EditableDraggableElement tableNumberDigit,
      QrCodeDraggableElement qrCodeElement,
      string[] Visitors)
    {
      CardDesignMasterController masterController = this;
      if (masterController.Session["admin1"] == null)
        return masterController.Json(new
        {
          loginStatus = false
        });
      List<visitor_details> visitorEventDetails = masterController._dbContext.visitor_details.Where(ev => ev.event_det_sno == (long?) event_id).ToList();
      if (visitorEventDetails.Count() <= 0)
        return masterController.Json(new
        {
          message = "Failed to generate card",
          downloadStatus = false
        });
      List<Task> taskList1 = new List<Task>();
      string venue = visitorEventDetails.FirstOrDefault().event_details.venue;
      foreach (visitor_details visitorDetails in visitorEventDetails)
      {
        visitor_details visitor_details = visitorDetails;
        taskList1.Add(Task.Run(() => QRCodeGenerator.GenerateActionWithTableNumber(visitor_details.visitor_det_sno, visitor_details.qrcode, visitor_details.visitor_name, visitor_details.no_of_persons.ToString(), visitor_details.event_details.venue, visitor_details.table_number)));
      }
      await Task.WhenAll((IEnumerable<Task>) taskList1);
      List<Task> taskList2 = new List<Task>();
      foreach (visitor_details visitorDetails in visitorEventDetails)
      {
        visitor_details visitor_details = visitorDetails;
        taskList2.Add(Task.Run((Action) (() =>
        {
          Invitees invitee = new Invitees()
          {
            Id = visitor_details.visitor_det_sno,
            InviteeName = visitor_details.visitor_name,
            CardSize = visitor_details.no_of_persons ?? 1,
            TableNumber = visitor_details.table_number ?? ""
          };
          CardGenerationService.CardTemplateFile = HostingEnvironment.MapPath(cardTemplate);
          CardGenerationService.EditableElementValues = editableElement;
          CardGenerationService.TableNumberLabelElementValues = tableNumberLabel;
          CardGenerationService.TableNumberDigitElementValues = tableNumberDigit;
          CardGenerationService.CardSizeLabelElementValues = cardSizeLabel;
          CardGenerationService.QrCodeElementValues = qrCodeElement;
          CardGenerationService.IsCardSizeDisplayed = displayCardSize;
          CardGenerationService.PdfGenerator(invitee);
        })));
      }
      await Task.WhenAll((IEnumerable<Task>) taskList2);
      List<Task> taskList3 = new List<Task>();
      string pdfAbsolutePath = CardDesignMasterController.GetPDFFormatCardDirectoryAbsolutePath();
      foreach (visitor_details visitorDetails in visitorEventDetails)
      {
        visitor_details visitor_details = visitorDetails;
        taskList3.Add(Task.Run((Action) (() => CardGenerationService.ConvertPDF2png(Path.Combine(pdfAbsolutePath, string.Format("{0}_{1}.pdf", visitor_details.visitor_det_sno, EscapeCharacterForFileName(visitor_details.visitor_name)))))));
      }
      await Task.WhenAll((IEnumerable<Task>) taskList3);
      string directoryAbsolutePath = CardDesignMasterController.GetCardDirectoryAbsolutePath();
      visitor_details zipFileData = visitorEventDetails.FirstOrDefault<visitor_details>();
      CardDesignMasterController.GenerateAndPopulateZipFile(CardDesignMasterController.GetZipFullPath(directoryAbsolutePath, zipFileData), CardDesignMasterController.GetListOfGeneratedCardFullPaths(visitorEventDetails, directoryAbsolutePath));
      return masterController.Json(new
      {
        message = "Card successfully generated",
        downloadStatus = true
      });
    }

    [Route("Bulk-Card-Download-Multiple")]
    public ActionResult BulkDownloadGenerateAndPreviewMultiple(long event_id, string[] Visitors)
    {
      if (this.Session["admin1"] == null)
        return (ActionResult) this.RedirectToAction("Login", "Login");
      var data = this._dbContext.visitor_details.Where(ev => ev.event_det_sno == event_id).Select(vd => new
      {
        event_id = vd.event_details.event_det_sno,
        event_name = vd.event_details.event_name
      }).FirstOrDefault();
      string path = Path.Combine(HostingEnvironment.MapPath(DirectoryHelpers.Generated_Cards_DirVirtualDirectory), string.Format("{0}_{1}.zip", (object) data.event_id, (object) data.event_name));
      return System.IO.File.Exists(path) ? (ActionResult) this.File(System.IO.File.ReadAllBytes(path), "application/octet-stream", DirectoryHelpers.GetTimestampedFile(path)) : (ActionResult) this.Content("File does not exist");
    }

    [HttpPost]
    [Route("Generate-and-Preview1")]
    public ActionResult GenerateAndPreview1(
      long event_id,
      bool displayCardSize,
      string cardTemplate,
      EditableDraggableElement editableElement,
      EditableDraggableElement cardSizeLabel,
      EditableDraggableElement tableNumberLabel,
      EditableDraggableElement tableNumberDigit,
      QrCodeDraggableElement qrCodeElement)
    {
      if (this.Session["admin1"] == null)
        return (ActionResult) this.Json((object) new
        {
          loginStatus = false
        });
      var visitor_details = this._dbContext.visitor_details.Where(ev => ev.event_det_sno == event_id).Select(vd => new
      {
        visitor_id = vd.visitor_det_sno,
        vd.event_det_sno,
        vd.visitor_name,
        vd.event_details.event_name,
        vd.qrcode,
        card_size = vd.no_of_persons,
        vd.event_details.venue,
        vd.table_number
      }).ToList();
      if (visitor_details == null)
        return Json(new
        {
          message = "Failed to generate card",
          downloadStatus = false
        });
      foreach (var data in visitor_details)
      {
        QRCodeGenerator.GenerateActionWithTableNumber(data.visitor_id, data.qrcode, data.visitor_name, data.card_size.ToString(), data.venue, data.table_number);
        Invitees invitee = new Invitees()
        {
          Id = data.visitor_id,
          InviteeName = data.visitor_name,
          CardSize = data.card_size ?? 1,
          TableNumber = data.table_number ?? ""
        };
        CardGenerationService.CardTemplateFile = HostingEnvironment.MapPath(cardTemplate);
        CardGenerationService.EditableElementValues = editableElement;
        CardGenerationService.CardSizeLabelElementValues = cardSizeLabel;
        CardGenerationService.TableNumberLabelElementValues = tableNumberLabel;
        CardGenerationService.TableNumberDigitElementValues = tableNumberDigit;
        CardGenerationService.QrCodeElementValues = qrCodeElement;
        CardGenerationService.IsCardSizeDisplayed = displayCardSize;
        CardGenerationService.ConvertPDF2png(CardGenerationService.PdfGenerator(invitee));
      }
      return (ActionResult) this.Json((object) new
      {
        message = "Card successfully generated",
        downloadStatus = true
      });
    }

    public ActionResult DownloadGenerateAndPreview1(long event_id)
    {
      if (this.Session["admin1"] == null)
        return (ActionResult) this.RedirectToAction("Login", "Login");
      var visitor_details = this._dbContext.visitor_details.Where(ev => ev.event_det_sno == event_id);
      var visitors =  visitor_details.Select( vd => new
      {
        visitor_id = vd.visitor_det_sno,
        vd.visitor_name
      }).ToList();
      foreach (var data in visitors)
      {
        string path = Path.Combine(HostingEnvironment.MapPath(DirectoryHelpers.Generated_Cards_DirVirtualDirectory), string.Format("{0}_{1}.jpeg", (object) data.visitor_id, (object) data.visitor_name));
        if (System.IO.File.Exists(path))
          return (ActionResult) this.File(System.IO.File.ReadAllBytes(path), "application/octet-stream", DirectoryHelpers.GetTimestampedFile(path));
      }
      return (ActionResult) this.Content("File does not exist");
    }

    [HttpPost]
    [Route("Generate-and-Preview2")]
    public async Task<ActionResult> GenerateAndPreview2(
      long event_id,
      bool displayCardSize,
      string cardTemplate,
      EditableDraggableElement editableElement,
      EditableDraggableElement cardSizeLabel,
      EditableDraggableElement tableNumberLabel,
      EditableDraggableElement tableNumberDigit,
      QrCodeDraggableElement qrCodeElement,
      string[] Visitors)
    {
      CardDesignMasterController masterController = this;
      if (masterController.Session["admin1"] == null)
        return (ActionResult) masterController.Json((object) new
        {
          loginStatus = false
        });
      int i = 0;
      int d = Visitors.Length;
      List<visitor_details> visitorEventDetails = masterController._dbContext.visitor_details.Where<visitor_details>((Expression<Func<visitor_details, bool>>) (ev => ev.event_det_sno == (long?) event_id)).ToList<visitor_details>();
      if (visitorEventDetails.Count<visitor_details>() <= 0)
        return (ActionResult) masterController.Json((object) new
        {
          message = "Please Select at least one invitee",
          downloadStatus = false
        });
      List<Task> qrCodeGenerationTask = new List<Task>();
      string venue = visitorEventDetails.FirstOrDefault<visitor_details>().event_details.venue;
      foreach (visitor_details visitorDetails in visitorEventDetails)
      {
        visitor_details visitor_details = visitorDetails;
        for (i = 0; i < d; ++i)
        {
          if (visitor_details.visitor_det_sno == (long) int.Parse(Visitors[i]))
          {
            qrCodeGenerationTask.Add(Task.Run((Action) (() => QRCodeGenerator.GenerateActionWithTableNumber(visitor_details.visitor_det_sno, visitor_details.qrcode, visitor_details.visitor_name, visitor_details.no_of_persons.ToString(), visitor_details.event_details.venue, visitor_details.table_number))));
            await Task.WhenAll((IEnumerable<Task>) qrCodeGenerationTask);
            await Task.WhenAll((IEnumerable<Task>) new List<Task>()
            {
              Task.Run((Action) (() =>
              {
                Invitees invitee = new Invitees()
                {
                  Id = visitor_details.visitor_det_sno,
                  InviteeName = visitor_details.visitor_name,
                  CardSize = visitor_details.no_of_persons ?? 1,
                  TableNumber = visitor_details.table_number ?? ""
                };
                CardGenerationService.CardTemplateFile = HostingEnvironment.MapPath(cardTemplate);
                CardGenerationService.EditableElementValues = editableElement;
                CardGenerationService.TableNumberLabelElementValues = tableNumberLabel;
                CardGenerationService.TableNumberDigitElementValues = tableNumberDigit;
                CardGenerationService.CardSizeLabelElementValues = cardSizeLabel;
                CardGenerationService.QrCodeElementValues = qrCodeElement;
                CardGenerationService.IsCardSizeDisplayed = displayCardSize;
                CardGenerationService.PdfGenerator(invitee);
              }))
            });
            List<Task> taskList = new List<Task>();
            string pdfAbsolutePath = CardDesignMasterController.GetPDFFormatCardDirectoryAbsolutePath();
            taskList.Add(Task.Run((Action) (() => CardGenerationService.ConvertPDF2png(Path.Combine(pdfAbsolutePath, string.Format("{0}_{1}.pdf", (object) visitor_details.visitor_det_sno, (object) CardDesignMasterController.EscapeCharacterForFileName(visitor_details.visitor_name)))))));
            await Task.WhenAll((IEnumerable<Task>) taskList);
          }
        }
      }
      string directoryAbsolutePath = CardDesignMasterController.GetCardDirectoryAbsolutePath();
      visitor_details zipFileData = visitorEventDetails.FirstOrDefault<visitor_details>();
      CardDesignMasterController.GenerateAndPopulateZipFile(CardDesignMasterController.GetZipFullPath(directoryAbsolutePath, zipFileData), CardDesignMasterController.GetListOfGeneratedSomeCardFullPaths(Visitors, visitorEventDetails, directoryAbsolutePath));
      return (ActionResult) masterController.Json((object) new
      {
        message = "Card successfully generated",
        downloadStatus = true
      });
    }

    public ActionResult DownloadGenerateAndPreview2(long event_id, string[] Visitors)
    {
      if (this.Session["admin1"] == null)
        return RedirectToAction("Login", "Login");
      IQueryable<visitor_details> source = this._dbContext.visitor_details.Where(ev => ev.event_det_sno == event_id);
      var selector = source.Select(vd => new
      {
        visitor_id = vd.visitor_det_sno,
        vd.visitor_name
      }).ToList();
      foreach (var data in selector)
      {
        string path = Path.Combine(HostingEnvironment.MapPath(DirectoryHelpers.Generated_Cards_DirVirtualDirectory), string.Format("{0}_{1}.jpeg", data.visitor_id, data.visitor_name));
        if (System.IO.File.Exists(path))
          return this.File(System.IO.File.ReadAllBytes(path), "application/octet-stream", DirectoryHelpers.GetTimestampedFile(path));
      }
      return this.Content("File does not exist");
    }

    [HttpPost]
    [Route("Generate-and-Preview")]
    public ActionResult GenerateAndPreview(
      long event_id,
      bool displayCardSize,
      string cardTemplate,
      EditableDraggableElement editableElement,
      EditableDraggableElement tableNumberLabel,
      EditableDraggableElement cardSizeLabel,
      EditableDraggableElement tableNumberDigit,
      QrCodeDraggableElement qrCodeElement)
    {
      if (this.Session["admin1"] == null)
        return (ActionResult) this.Json((object) new
        {
          loginStatus = false
        });
      var data = this._dbContext.visitor_details.Where<visitor_details>((Expression<Func<visitor_details, bool>>) (ev => ev.event_det_sno == (long?) event_id)).Select(vd => new
      {
        visitor_id = vd.visitor_det_sno,
        event_det_sno = vd.event_det_sno,
        visitor_name = vd.visitor_name,
        event_name = vd.event_details.event_name,
        qrcode = vd.qrcode,
        card_size = vd.no_of_persons,
        venue = vd.event_details.venue,
        table_number = vd.table_number
      }).FirstOrDefault();
      if (data == null)
        return (ActionResult) this.Json((object) new
        {
          message = "Failed to generate card",
          downloadStatus = false
        });
      QRCodeGenerator.GenerateActionWithTableNumber(data.visitor_id, data.qrcode, data.visitor_name, data.card_size.ToString(), data.venue, data.table_number);
      Invitees invitee = new Invitees()
      {
        Id = data.visitor_id,
        InviteeName = data.visitor_name,
        CardSize = data.card_size ?? 1,
        TableNumber = data.table_number ?? ""
      };
      CardGenerationService.CardTemplateFile = HostingEnvironment.MapPath(cardTemplate);
      CardGenerationService.EditableElementValues = editableElement;
      CardGenerationService.CardSizeLabelElementValues = cardSizeLabel;
      CardGenerationService.TableNumberLabelElementValues = tableNumberLabel;
      CardGenerationService.TableNumberDigitElementValues = tableNumberDigit;
      CardGenerationService.QrCodeElementValues = qrCodeElement;
      CardGenerationService.IsCardSizeDisplayed = displayCardSize;
      CardGenerationService.ConvertPDF2png(CardGenerationService.PdfGenerator(invitee));
      return (ActionResult) this.Json((object) new
      {
        message = "Card successfully generated",
        downloadStatus = true
      });
    }

    public ActionResult DownloadGenerateAndPreview(long event_id)
    {
      if (this.Session["admin1"] == null)
        return (ActionResult) this.RedirectToAction("Login", "Login");
      var data = this._dbContext.visitor_details.Where(ev => ev.event_det_sno == (long?) event_id).Select(vd => new
      {
        visitor_id = vd.visitor_det_sno,
        vd.visitor_name
      }).FirstOrDefault();
      string path = Path.Combine(HostingEnvironment.MapPath(DirectoryHelpers.Generated_Cards_DirVirtualDirectory), string.Format("{0}_{1}.jpeg", (object) data.visitor_id, (object) data.visitor_name));
      return System.IO.File.Exists(path) ? (ActionResult) this.File(System.IO.File.ReadAllBytes(path), "application/octet-stream", DirectoryHelpers.GetTimestampedFile(path)) : (ActionResult) this.Content("File does not exist");
    }

    [Route("Card-Test")]
    public async Task<ActionResult> CardTest()
    {
        var event_id = 6;

        var visitorEventDetails = _dbContext.visitor_details
            .Where(ev => ev.event_det_sno == event_id).ToList();


        #region card data list

        var defaultCardTemplate = DirectoryHelpers.GetDefaultCardTemplate();

        ViewBag.DefaultCardTemplate = defaultCardTemplate;
        ViewBag.DefaultQRCode = DirectoryHelpers.GetDefaultQRCode();
        ViewBag.DefaultFont = DirectoryHelpers.GetDefaultFont();

        var qrCodeElement = new QrCodeDraggableElement
        {
            Position = new ElementPosition { LeftPosition = 0, TopPosition = 0 },
            QrCodeSize = 165F
        };

        var editableElement = new EditableDraggableElement
        {
            Position = new ElementPosition { LeftPosition = 0, TopPosition = 0 },
            FontDetails = new FontMasterClass
            {
                Bold = false,
                Italic = false,
                Underline = false,
                Color = "rgb(0, 0, 0)",
                FontName = "Acme",
                FontSize = 24,
                TextAlign = "center"

            }
        };

        #endregion;


        if (visitorEventDetails.Count() > 0)
        {

            #region QR code generation section

            List<Task> qrCodeGenerationTask = new List<Task>();

            var event_venue = visitorEventDetails.FirstOrDefault().event_details.venue;

            foreach (var visitor_details in visitorEventDetails)
            {
                qrCodeGenerationTask.Add(Task.Run(() =>
                {
                    //2. Generate QR Code
                    QRCodeGenerator.GenerateActionWithTableNumber(
                        visitor_details.visitor_det_sno,
                        visitor_details.qrcode,
                        visitor_details.visitor_name,
                        visitor_details.no_of_persons.ToString(),
                        event_venue,
                        visitor_details.table_number);
                }));

            }

            await Task.WhenAll(qrCodeGenerationTask);

            #endregion;


            #region PDF card generation section

            List<Task> pdgCardGenerationTask = new List<Task>();

            foreach (var visitor_details in visitorEventDetails)
            {
                pdgCardGenerationTask.Add(Task.Run(() =>
                {
                    //3. Generate PDF
                    var inviteeDetails = new Invitees
                    {
                        Id = visitor_details.visitor_det_sno,
                        InviteeName = visitor_details.visitor_name,
                        CardSize = visitor_details.no_of_persons
                    };

                    CardGenerationService.CardTemplateFile = HostingEnvironment.MapPath(defaultCardTemplate);
                    CardGenerationService.EditableElementValues = editableElement;
                    CardGenerationService.QrCodeElementValues = qrCodeElement;
                    CardGenerationService.IsCardSizeDisplayed = true;
                    var pdfGeneratedFile = CardGenerationService.PdfGenerator(inviteeDetails);

                    ////4. Generate image
                    //var path = CardGenerationService.ConvertPDF2png(pdfGeneratedFile);
                }));
            }

            await Task.WhenAll(pdgCardGenerationTask);

            #endregion;


            #region PNG card generataion section

            List<Task> pngCardGenerationTask = new List<Task>();

            var pdfAbsolutePath = GetPDFFormatCardDirectoryAbsolutePath();

            foreach (var visitor_details in visitorEventDetails)
            {
                pngCardGenerationTask.Add(Task.Run(() =>
                {
                    var pdfGeneratedFile = Path.Combine(pdfAbsolutePath, string.Format("{0}_{1}.pdf", visitor_details.visitor_det_sno, EscapeCharacterForFileName(visitor_details.visitor_name)));
                    //4. Generate image
                    var path = CardGenerationService.ConvertPDF2png(pdfGeneratedFile);
                }));
            }

            await Task.WhenAll(pngCardGenerationTask);

            #endregion;


            #region Zip Folder Generation Secton

            List<Task> addFilesToZipFolderTask = new List<Task>();

            string absolutePath = GetCardDirectoryAbsolutePath();

            var zipFileData = visitorEventDetails.FirstOrDefault();

            string zipPath = GetZipFullPath(absolutePath, zipFileData);

            foreach (var visitor_details in visitorEventDetails)
            {
                pngCardGenerationTask.Add(Task.Run(() =>
                {
                    var files = GetListOfGeneratedCardFullPaths(visitorEventDetails, absolutePath);

                    GenerateAndPopulateZipFile(zipPath, files);
                }));
            }

            await Task.WhenAll(addFilesToZipFolderTask);

            #endregion;
        }

        return Json("Done", JsonRequestBehavior.AllowGet);
    }

    [HttpPost]
    [Route("Bulk-Card-Generate")]
    public async Task<ActionResult> BulkGenerateAndPreview(
      long event_id,
      bool displayCardSize,
      string cardTemplate,
      EditableDraggableElement editableElement,
      EditableDraggableElement cardSizeLabel,
      EditableDraggableElement tableNumberLabel,
      EditableDraggableElement tableNumberDigit,
      QrCodeDraggableElement qrCodeElement)
    {
      CardDesignMasterController masterController = this;
      if (masterController.Session["admin1"] == null)
        return  masterController.Json( new
        {
          loginStatus = false
        });
      List<visitor_details> visitorEventDetails = masterController._dbContext.visitor_details.Where(ev => ev.event_det_sno == event_id).ToList();
      if (visitorEventDetails.Count() <= 0)
        return  masterController.Json( new
        {
          message = "Failed to generate card",
          downloadStatus = false
        });
      List<Task> taskList1 = new List<Task>();
      string venue = visitorEventDetails.FirstOrDefault().event_details.venue;
      foreach (visitor_details visitorDetails in visitorEventDetails)
      {
        visitor_details visitor_details = visitorDetails;
        taskList1.Add(Task.Run( (() => QRCodeGenerator.GenerateActionWithTableNumber(visitor_details.visitor_det_sno, visitor_details.qrcode, visitor_details.visitor_name, visitor_details.no_of_persons.ToString(), visitor_details.event_details.venue, visitor_details.table_number))));
      }
      await Task.WhenAll( taskList1);
      List<Task> taskList2 = new List<Task>();
      foreach (visitor_details visitorDetails in visitorEventDetails)
      {
        visitor_details visitor_details = visitorDetails;
        taskList2.Add(Task.Run( (() =>
        {
          Invitees invitee = new Invitees()
          {
            Id = visitor_details.visitor_det_sno,
            InviteeName = visitor_details.visitor_name,
            CardSize = visitor_details.no_of_persons ?? 1,
            TableNumber = visitor_details.table_number ?? ""
          };
          CardGenerationService.CardTemplateFile = HostingEnvironment.MapPath(cardTemplate);
          CardGenerationService.EditableElementValues = editableElement;
          CardGenerationService.TableNumberLabelElementValues = tableNumberLabel;
          CardGenerationService.TableNumberDigitElementValues = tableNumberDigit;
          CardGenerationService.CardSizeLabelElementValues = cardSizeLabel;
          CardGenerationService.QrCodeElementValues = qrCodeElement;
          CardGenerationService.IsCardSizeDisplayed = displayCardSize;
          CardGenerationService.PdfGenerator(invitee);
        })));
      }
      await Task.WhenAll( taskList2);
      List<Task> taskList3 = new List<Task>();
      string pdfAbsolutePath = GetPDFFormatCardDirectoryAbsolutePath();
      foreach (visitor_details visitorDetails in visitorEventDetails)
      {
        visitor_details visitor_details = visitorDetails;
        taskList3.Add(Task.Run((Action) (() => CardGenerationService.ConvertPDF2png(Path.Combine(pdfAbsolutePath, string.Format("{0}_{1}.pdf", (object) visitor_details.visitor_det_sno, (object) CardDesignMasterController.EscapeCharacterForFileName(visitor_details.visitor_name)))))));
      }
      await Task.WhenAll( taskList3);
      string directoryAbsolutePath = GetCardDirectoryAbsolutePath();
      visitor_details zipFileData = visitorEventDetails.FirstOrDefault();
      GenerateAndPopulateZipFile(GetZipFullPath(directoryAbsolutePath, zipFileData), GetListOfGeneratedCardFullPaths(visitorEventDetails, directoryAbsolutePath));
      return masterController.Json(new
      {
        message = "Card successfully generated",
        downloadStatus = true
      });
    }

    [Route("Bulk-Card-Download")]
    public ActionResult BulkDownloadGenerateAndPreview(long event_id)
    {
      if (this.Session["admin1"] == null)
        return (ActionResult) this.RedirectToAction("Login", "Login");
      var data = this._dbContext.visitor_details.Where<visitor_details>((Expression<Func<visitor_details, bool>>) (ev => ev.event_det_sno == (long?) event_id)).Select(vd => new
      {
        event_id = vd.event_details.event_det_sno,
        event_name = vd.event_details.event_name
      }).FirstOrDefault();
      string path = Path.Combine(HostingEnvironment.MapPath(DirectoryHelpers.Generated_Cards_DirVirtualDirectory), string.Format("{0}_{1}.zip", (object) data.event_id, (object) data.event_name));
      return System.IO.File.Exists(path) ? (ActionResult) this.File(System.IO.File.ReadAllBytes(path), "application/octet-stream", DirectoryHelpers.GetTimestampedFile(path)) : (ActionResult) this.Content("File does not exist");
    }

    [Route("whatsapp-notification")]
    public ActionResult WhatsAppNotification()
    {
      if (this.Session["admin1"] == null)
        return RedirectToAction("Login", "Login");


   /*   // ISSUE: reference to a compiler-generated field
      if (CardDesignMasterController.\u003C\u003Eo__26.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CardDesignMasterController.\u003C\u003Eo__26.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, IQueryable<SelectListItem>, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "EventDropDownList", typeof (CardDesignMasterController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      ParameterExpression parameterExpression;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method reference
      // ISSUE: method reference
      // ISSUE: method reference
      // ISSUE: method reference
      object obj = CardDesignMasterController.\u003C\u003Eo__26.\u003C\u003Ep__0.Target((CallSite) CardDesignMasterController.\u003C\u003Eo__26.\u003C\u003Ep__0, this.ViewBag, this._dbContext.event_details.Where<event_details>((Expression<Func<event_details, bool>>) (e => e.event_date >= (DateTime?) DateTime.Today)).Select<event_details, SelectListItem>(Expression.Lambda<Func<event_details, SelectListItem>>((Expression) Expression.MemberInit(Expression.New(typeof (SelectListItem)), (MemberBinding) Expression.Bind((MethodInfo) MethodBase.GetMethodFromHandle(__methodref (SelectListItem.set_Value)), (Expression) Expression.Call(b.event_det_sno, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (long.ToString)), Array.Empty<Expression>())), (MemberBinding) Expression.Bind((MethodInfo) MethodBase.GetMethodFromHandle(__methodref (SelectListItem.set_Text)), (Expression) Expression.Property((Expression) parameterExpression, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (event_details.get_event_name))))), parameterExpression)));
           */ 
   #region Event dropdownlist

            //TODO: there must be a way to view only active events

            ViewBag.EventDropDownList = _dbContext.event_details.Where(e => e.event_date >= DateTime.Today)
                .Select(b => new SelectListItem
                {
                    Value = b.event_det_sno.ToString(),
                    Text = b.event_name
                });

     #endregion;
            return View();
    }

    [Route("whatsapp-notification-cards")]
    public ActionResult WhatsAppNotificationCards()
    {
      long event_id = Convert.ToInt64(Session["EventID"]);

            #region Event dropdownlist

            //TODO: there must be a way to view only active events

            ViewBag.EventDropDownList = _dbContext.event_details.Where(e => e.event_det_sno == event_id)
                .Select(b => new SelectListItem
                {
                    Value = b.event_det_sno.ToString(),
                    Text = b.event_name
                });

            #endregion
            return View();
    }

    [Route("InvitationCards")]
    public ActionResult InvitationCards()
    {
      long event_id = Convert.ToInt64(this.Session["EventID"]);
            #region Event dropdownlist

            //TODO: there must be a way to view only active events

            ViewBag.EventDropDownList = _dbContext.event_details.Where(e => e.event_det_sno == event_id)
                .Select(b => new SelectListItem
                {
                    Value = b.event_det_sno.ToString(),
                    Text = b.event_name
                });

            #endregion
            return this.View();
    }

    [HttpPost]
    public ActionResult OnEventChangeAction(string Event_Id)
    {
      long event_id = Convert.ToInt64(Event_Id);
      List<visitor_details> list = this._dbContext.visitor_details.Where(v => v.event_det_sno == event_id && v.event_details.event_date >= (DateTime?) DateTime.Today).ToList();
      return list.Count() > 0 ? Json(new
      {
        status = true,
        data_value = list.Select(v => new
        {
          v.visitor_det_sno,
          v.visitor_name
        })
      }) : Json(new
      {
        status = false,
        response = "Event details does not exist"
      });
    }

    [HttpPost]
    [Route("whatsapp-notification1")]
    public async Task<ActionResult> WhatsAppNotification1([Bind(Include = "Event_Id, Visitor_Id, WhatsAppNumber, Message")] WhatsAppNotification whatsApp)
    {
      if (!this.ModelState.IsValid)
        return (ActionResult) this.Json((object) new
        {
          sendStatus = false,
          response = "Notification sending failed!"
        });
      visitor_details visitorDetails = _dbContext.visitor_details.Where(v => v.visitor_det_sno == whatsApp.Visitor_Id).FirstOrDefault();
      if (visitorDetails == null)
        return Json(new
        {
          sendStatus = false,
          response = "Notification sending failed!"
        });
      string mediaUri = GetMediaURI(visitorDetails);
      Url.Content(mediaUri);
      await SendWhatsAppMessageAsync(mediaUri, visitorDetails);
      return Json(new
      {
        sendStatus = true,
        response = "Notification has been successfully sent"
      });
    }

    [HttpPost]
    [Route("whatsapp-notification-all")]
    public async Task<ActionResult> WhatsAppNotificationAll([Bind(Include = "Event_Id")] WhatsAppNotification whatsApp)
    {
      List<visitor_details> list = _dbContext.visitor_details.Where(v => v.event_det_sno == whatsApp.Event_Id).ToList();
      if (list == null)
        return Json(new
        {
          sendStatus = false,
          response = "Notification sending failed!"
        });
      foreach (visitor_details visitorDetails in list)
        await SendWhatsAppMessageAsync(GetMediaURI(visitorDetails), visitorDetails);
      return Json(new
      {
        sendStatus = true,
        response = "WhatsApp Card has been successfully sent"
      });
    }

    [HttpPost]
    [Route("whatsapp-notification")]
    public async Task<ActionResult> WhatsAppNotification(string[] Visitors, long? Event_Id)
    {
      List<visitor_details> list = _dbContext.visitor_details.Where(v => v.event_det_sno == Event_Id).ToList();
      if (list == null)
        return Json( new
        {
          sendStatus = false,
          response = "Notification sending failed!"
        });
      int length = Visitors.Length;
      foreach (visitor_details visitorDetails in list)
      {
        for (int index = 0; index < length; ++index)
        {
          int visitor_id = int.Parse(Visitors[index]);
          if (visitorDetails.visitor_det_sno == visitor_id && visitorDetails.mobile_no != null)
          await SendWhatsAppMessageAsync(GetMediaURIMulti(visitorDetails, visitor_id), visitorDetails);
        }
      }
      return (ActionResult) this.Json((object) new
      {
        sendStatus = true,
        response = "WhatsApp Card has been successfully sent"
      });
    }

    private string GetMediaURI(visitor_details visitorDetails)
    {
      string stringToEscape = string.Format("{0}_{1}.jpeg", (object) visitorDetails.visitor_det_sno, (object) visitorDetails.visitor_name);
      string appSetting = ConfigurationManager.AppSettings["card_files_dir"];
      return Uri.EscapeUriString(stringToEscape);
    }

    private string GetMediaURIMulti(visitor_details visitorDetails, long? vid)
    {
      return Uri.EscapeUriString(string.Format("{0}_{1}.jpeg", (object) vid, (object) visitorDetails.visitor_name));
    }


        #region Normal EVENT WhatsApp Cards (old)


        private static async Task SendWhatsAppMessageAsyncOLD(
          string mediaUri,
          visitor_details visitorDetails)
        {
            TwilioClient.Init("AC14b36a565bc1c9863dea07a57161bdf2", "e9de6611667b639a480a22749e48328f"); // b73856e9cd8e6bd6297a51e795d52ac4
            // Auth Token : e9de6611667b639a480a22749e48328f
            //AccountSid : AC14b36a565bc1c9863dea07a57161bdf2

            await Task.WhenAll(new List<Task>()
      {
        Task.Run(() =>
        {
          string str = JsonConvert.SerializeObject( new Dictionary<string, object>()
          {
            {"1",visitorDetails.visitor_name},
            {"2",mediaUri},
            {"3", "3"},
            {"4", "4"},
            {"5", "5"}
          }, Formatting.Indented);
          PhoneNumber from = new PhoneNumber("whatsapp:+255745011604");
          string contentVariables = str;
            var messageResponse  =  MessageResource.CreateAsync(new PhoneNumber("whatsapp:+" + visitorDetails.mobile_no), from: from, messagingServiceSid: "MGf56bae8229d878500257da4dcbfbe068", contentSid: "HX560227120c59ce1bc3383b843c27ba06", contentVariables: contentVariables);


            // Extract and handle the response here
            System.Diagnostics.Debug.WriteLine($"Message SID: {messageResponse.Result.Sid}");
            System.Diagnostics.Debug.WriteLine($"Message Status: {messageResponse.Status}");
            System.Diagnostics.Debug.WriteLine($"Error Code (if any): {messageResponse.Result.ErrorCode}");
            System.Diagnostics.Debug.WriteLine($"Error Message (if any): {messageResponse.Result.ErrorMessage}");
            System.Diagnostics.Debug.WriteLine($"Message Body: {messageResponse.Result.Body}");
            System.Diagnostics.Debug.WriteLine($"Message Exception :{messageResponse.Exception}");
            System.Diagnostics.Debug.WriteLine($"Message AccountSid: {messageResponse.Result.AccountSid}");

        })
      });
        }



        #endregion


      private static async Task SendWhatsAppMessageAsyncDate(
      string mediaUri,
      visitor_details visitorDetails)
    {
      TwilioClient.Init("AC14b36a565bc1c9863dea07a57161bdf2", "e9de6611667b639a480a22749e48328f"); // b73856e9cd8e6bd6297a51e795d52ac4
            // Auth Token : e9de6611667b639a480a22749e48328f
            //AccountSid : AC14b36a565bc1c9863dea07a57161bdf2

            // Thank You: HX5157e3b69183e4c9138004eaa783d0d6

            // Save the date : HX560227120c59ce1bc3383b843c27ba06



            await Task.WhenAll(new List<Task>()
      {
        Task.Run(() =>
        {
          string str = JsonConvert.SerializeObject( new Dictionary<string, object>()
          {
            {"1",visitorDetails.visitor_name},
            {"2",mediaUri}
          }, Formatting.Indented);
          PhoneNumber from = new PhoneNumber("whatsapp:+255745011604");
          string contentVariables = str;
            var messageResponse  =  MessageResource.CreateAsync(new PhoneNumber("whatsapp:+" + visitorDetails.mobile_no), from: from, messagingServiceSid: "MGf56bae8229d878500257da4dcbfbe068", contentSid: "HX560227120c59ce1bc3383b843c27ba06", contentVariables: contentVariables);


            // Extract and handle the response here
            System.Diagnostics.Debug.WriteLine($"Message SID: {messageResponse.Result.Sid}");
            System.Diagnostics.Debug.WriteLine($"Message Status: {messageResponse.Status}");
            System.Diagnostics.Debug.WriteLine($"Error Code (if any): {messageResponse.Result.ErrorCode}");
            System.Diagnostics.Debug.WriteLine($"Error Message (if any): {messageResponse.Result.ErrorMessage}");
            System.Diagnostics.Debug.WriteLine($"Message Body: {messageResponse.Result.Body}");
            System.Diagnostics.Debug.WriteLine($"Message Exception :{messageResponse.Exception}");
            System.Diagnostics.Debug.WriteLine($"Message AccountSid: {messageResponse.Result.AccountSid}");

        })
      });
    }




        #region Send WhatsApp Cards
        // save_the_date_notification_card - HXab4baf7f14058adc5da41de4b430187c

        private static async Task SendWhatsAppMessageAsync(string mediaUri, visitor_details visitorDetails)
        {
            // Swahili - Cards Event Updated : HXec038b9bb126da9dc0e2a0fd0e86ff7d

            // English - Cards Event Updated : HX4144fe950291d54cc2bc511fdab1b5d6

            // Thank You: HX5157e3b69183e4c9138004eaa783d0d6

            // Save the date : HX560227120c59ce1bc3383b843c27ba06

            TwilioClient.Init("AC14b36a565bc1c9863dea07a57161bdf2", "e9de6611667b639a480a22749e48328f");
            // e9de6611667b639a480a22749e48328f - b73856e9cd8e6bd6297a51e795d52ac4 => old auth

            var contentVariables = JsonConvert.SerializeObject(new Dictionary<string, object>()
                {
                    { "1", visitorDetails.visitor_name },
                    { "2", mediaUri },
                    {"3", "3"},
                    {"4", "4"}
                }, Formatting.Indented);

            PhoneNumber from = new PhoneNumber("whatsapp:+255745011604");

            try
            {
                // Attempt to send the message
                var messageResponse = await MessageResource.CreateAsync(
                    to: new PhoneNumber("whatsapp:+" + visitorDetails.mobile_no),
                    from: from,
                    messagingServiceSid: "MGf56bae8229d878500257da4dcbfbe068",
                    contentSid: "HX4144fe950291d54cc2bc511fdab1b5d6",
                    contentVariables: contentVariables
                );

                ECARDAPPEntities context = new ECARDAPPEntities();

                var messageLog = new twilio_send_log
                {
                    event_det_sno = visitorDetails.event_det_sno.ToString(),
                    accountsid = messageResponse.AccountSid,
                    messaging_service_sid = messageResponse.MessagingServiceSid,
                    status = messageResponse.Status.ToString(),
                    //error_code = messageResponse.ErrorCode.ToString(),
                    //error_messages = messageResponse.ErrorMessage.ToString(),
                    //date_Sent = messageResponse.DateSent,
                    date_created = messageResponse.DateCreated,
                    posted_date = DateTime.Now,
                    posted_by = "Twilio",
                    uri = messageResponse.Uri,
                    subrecource_uris = messageResponse.SubresourceUris.ToString(),
                    sid = messageResponse.Sid,
                    num_segment = messageResponse.NumSegments,
                    num_media = messageResponse.NumMedia,
                    apiversion = messageResponse.ApiVersion,
                    body = messageResponse.Body,
                    direction = messageResponse.Direction.ToString(),
                    price = messageResponse.Price,
                    price_unit = messageResponse.PriceUnit,
                    whatsapp_from = messageResponse.From.ToString().Substring(10),
                    whatsapp_to = messageResponse.To.ToString().Substring(10),
                    date_updated = messageResponse.DateUpdated
                };


                context.twilio_send_log.Add(messageLog);
                await context.SaveChangesAsync();


            }
            catch (Exception ex)
            {
                // Capture error details and save them to the database
                //  await SaveMessageResponseToDatabase(visitorDetails, null, "Failed", ex.HResult.ToString(), ex.Message);
                System.Diagnostics.Debug.WriteLine($"Exception on DB: {ex}"); 
                System.Diagnostics.Debug.WriteLine($"Message : {ex.Message}");

                ECARDAPPEntities context = new ECARDAPPEntities();
                var errorLog = new ErrorLog
                {
                    Message = ex.Message,
                    Source = ex.Source,
                    StackTrace = ex.StackTrace,
                    TargetSite = ex.TargetSite.ToString(),
                    InnerException = ex.InnerException.ToString(),
                    AuditDate = DateTime.Now,
                    RequestURL = "twilio send log Db",
                    BrowserType = ""


                };
                context.ErrorLogs.Add(errorLog);
                await context.SaveChangesAsync();


            }
        }



        #endregion




        #region SendWhatsAppMessageAsyncdb and Save Error & Response to Db (Normal Envents Cards)
  /*      private static async Task SendWhatsAppMessageAsyncdb1(string mediaUri, visitor_details visitorDetails){

            TwilioClient.Init("AC14b36a565bc1c9863dea07a57161bdf2", "e9de6611667b639a480a22749e48328f");// e9de6611667b639a480a22749e48328f - b73856e9cd8e6bd6297a51e795d52ac4

            var contentVariables = JsonConvert.SerializeObject(new Dictionary<string, object>()
                {
                    { "1", visitorDetails.visitor_name },
                    { "2", mediaUri },
                    { "3", "3" },
                    { "4", "4" },
                    { "5", "5" }
                }, Formatting.Indented);

            PhoneNumber from = new PhoneNumber("whatsapp:+255745011604");

            try
            {
                // Attempt to send the message
                var messageResponse = await MessageResource.CreateAsync(
                    to: new PhoneNumber("whatsapp:+" + visitorDetails.mobile_no),
                    from: from,
                    messagingServiceSid: "MGf56bae8229d878500257da4dcbfbe068",
                    contentSid: "HX560227120c59ce1bc3383b843c27ba06",
                    contentVariables: contentVariables
                );

                ECARDAPPEntities context = new ECARDAPPEntities();
                var event_id = context.visitor_details.Where(v => v.mobile_no == visitorDetails.mobile_no).Select(v => v.event_det_sno).FirstOrDefault();

                var messageLog = new twilio_send_log
                {
                    event_det_sno = event_id.ToString(),
                    accountsid = messageResponse.AccountSid,
                    messaging_service_sid = messageResponse.MessagingServiceSid,
                    status = messageResponse.Status.ToString(),
                    error_code = messageResponse.ErrorCode.ToString() ?? "",
                    error_messages = messageResponse.ErrorMessage.ToString() ?? "",
                    date_Sent = messageResponse.DateSent,
                    date_created = messageResponse.DateCreated,
                    posted_date =  DateTime.Now,
                    posted_by = "Twilio",
                    uri = messageResponse.Uri,
                    subrecource_uris = messageResponse.SubresourceUris.ToString(),
                    sid = messageResponse.Sid,
                    num_segment = messageResponse.NumSegments,
                    num_media = messageResponse.NumMedia,
                    apiversion = messageResponse.ApiVersion,
                    body = messageResponse.Body,
                    direction = messageResponse.Direction.ToString(),
                    price = messageResponse.Price,
                    price_unit = messageResponse.PriceUnit,
                    whatsapp_from = messageResponse.From.ToString(),
                    whatsapp_to = messageResponse.To.ToString(),
                    date_updated = messageResponse.DateUpdated
                };

                context.twilio_send_log.Add(messageLog);
                await context.SaveChangesAsync();
               



                // Save success response to the database
               // await SaveMessageResponseToDatabase(visitorDetails, messageResponse.Sid, messageResponse.Status.ToString(), null, null);
            }
            catch (Exception ex)
            {
                // Capture error details and save them to the database
                // await SaveMessageResponseToDatabase(visitorDetails, null, "Failed", ex.HResult.ToString(), ex.Message);

               
            }
        }
*/
        private static async Task SaveMessageResponseToDatabase(visitor_details visitorDetails, string messageSid, string status, string errorCode, string errorMessage)
        {
            // Assuming you have a DbContext called `AppDbContext` and a model called `MessageLog`
            using (ECARDAPPEntities context = new ECARDAPPEntities())
            {
              /*  var messageLog = new MessageLog
                {
                    VisitorName = visitorDetails.visitor_name,
                    MobileNumber = visitorDetails.mobile_no,
                    MessageSid = messageSid,
                    Status = status,
                    ErrorCode = errorCode,
                    ErrorMessage = errorMessage,
                    DateSent = DateTime.Now
                };

                context.MessageLogs.Add(messageLog);
                await context.SaveChangesAsync();*/
            }
        }

        #endregion

        [ChildActionOnly]
    public ActionResult EventDetails()
    {
      return PartialView("_FillForm", _EventDetails);
    }

    [ChildActionOnly]
    public ActionResult FontsPartialView()
    {
      return PartialView("_FontsPartialView", _AllFonts);
    }

    private static void GenerateAndPopulateZipFile(string zipPath, List<string> files)
    {
      using (ZipArchive destination = ZipFile.Open(zipPath, ZipArchiveMode.Update))
      {
        foreach (string file in files)
        {
          FileInfo fileInfo = new FileInfo(file);
          destination.CreateEntryFromFile(fileInfo.FullName, fileInfo.Name);
        }
      }
    }

    private static string GetCardDirectoryAbsolutePath()
    {
      return HostingEnvironment.MapPath(DirectoryHelpers.Generated_Cards_DirVirtualDirectory);
    }

    private static string GetPDFFormatCardDirectoryAbsolutePath()
    {
      return HostingEnvironment.MapPath(DirectoryHelpers.GeneratedPDFVirtualDirectory);
    }

    private static List<string> GetListOfGeneratedCardFullPaths(
      List<visitor_details> inviteesDataList,
      string absolutePath)
    {
      List<string> generatedCardFullPaths = new List<string>();
      foreach (visitor_details inviteesData in inviteesDataList)
        generatedCardFullPaths.Add(Path.Combine(absolutePath, string.Format("{0}_{1}.jpeg", inviteesData.visitor_det_sno, EscapeCharacterForFileName(inviteesData.visitor_name))));
      return generatedCardFullPaths;
    }

    private static List<string> GetListOfGeneratedSomeCardFullPaths(
      string[] inviteesDataLists,
      List<visitor_details> inviteesDataList,
      string absolutePath)
    {
      int length = inviteesDataLists.Length;
      List<string> someCardFullPaths = new List<string>();
      foreach (visitor_details inviteesData in inviteesDataList)
      {
        for (int index = 0; index < length; ++index)
        {
          int num = int.Parse(inviteesDataLists[index]);
          if (inviteesData.visitor_det_sno == (long) num)
            someCardFullPaths.Add(Path.Combine(absolutePath, string.Format("{0}_{1}.jpeg", num,  EscapeCharacterForFileName(inviteesData.visitor_name))));
        }
      }
      return someCardFullPaths;
    }

    private static string GetZipFullPath(string absolutePath, visitor_details zipFileData)
    {
      string path = Path.Combine(absolutePath, string.Format("{0}_{1}.zip", zipFileData.event_det_sno,  zipFileData.event_details.event_name));
      if (System.IO.File.Exists(path))
        System.IO.File.Delete(path);
      return path;
    }

    [Route("Card-Templates")]
    public ActionResult CardTemplates()
    {
      return this.Session["admin1"] == null ? (ActionResult) this.RedirectToAction("Login", "Login") : View();
    }

    [HttpPost]
    public JsonResult CardTemplatesDataTable()
    {
      string virtualDirectory = DirectoryHelpers.CardTemplatesVirtualDirectory;
      List<string> list = ((IEnumerable<string>) Directory.GetFiles(HostingEnvironment.MapPath(virtualDirectory))).ToList<string>();
      List<CardTemplates> cardTemplatesList = new List<CardTemplates>();
      foreach (string path in list)
      {
        string fileName = Path.GetFileName(path);
        string path1 = DirectoryHelpers.RemoveTildeSymbolForJsVirtualPath(virtualDirectory);
        cardTemplatesList.Add(new CardTemplates()
        {
          CardTemplateFilePath = Path.Combine(path1, fileName)
        });
      }
      return this.Json((object) new
      {
        data = cardTemplatesList
      });
    }

    [HttpPost]
    public ActionResult UploadCardTemplate([Bind(Include = "CardTemplateFileAttach")] CardTemplates cardTemplate)
    {
      if (this.Session["admin1"] == null)
        return RedirectToAction("Login", "Login");
      if (!this.ModelState.IsValid)
        return PartialView("_AjaxHelperUploadCardTemplate", cardTemplate);
      HttpPostedFileBase templateFileAttach = cardTemplate.CardTemplateFileAttach;
      string fileName = Path.GetFileName(templateFileAttach.FileName);
      string filename = Path.Combine(DirectoryHelpers.MakeSureDirectoryExists(HostingEnvironment.MapPath(DirectoryHelpers.CardTemplatesVirtualDirectory)), fileName);
      templateFileAttach.SaveAs(filename);
      return Json( new
      {
        createStatus = true,
        response = "File Uploaded Successfully."
      });
    }

    [Route("Fonts-Management")]
    public ActionResult Fonts()
    {
      return this.Session["admin1"] == null ? RedirectToAction("Login", "Login") : (ActionResult) this.View();
    }

    [HttpPost]
    public ActionResult FontsDataTable()
    {
      if (this.Session["admin1"] == null)
        return RedirectToAction("Login", "Login");
      string virtualDirectory = DirectoryHelpers.FontFamilyVirtualDirectory;
      List<string> list = ((IEnumerable<string>) Directory.GetDirectories(HostingEnvironment.MapPath(virtualDirectory))).ToList<string>();
      List<FontFamily> fontFamilyList = new List<FontFamily>();
      foreach (string path in list)
      {
        string fileName = Path.GetFileName(path);
        string path1 = DirectoryHelpers.RemoveTildeSymbolForJsVirtualPath(virtualDirectory);
        fontFamilyList.Add(new FontFamily()
        {
          FontName = Path.GetFileNameWithoutExtension(fileName),
          FontStylesheetFileFullVirtuaPath = Path.Combine(path1, fileName)
        });
      }
      return Json(new
      {
        data = fontFamilyList
      });
    }

    [HttpPost]
    public ActionResult UploadFontFiles([Bind(Include = "FontName,FontFilesAttachment")] FontFilesUpload fontFiles)
    {
      if (!this.ModelState.IsValid)
        return (ActionResult) this.PartialView("_AjaxHelperUploadFontFIles", (object) fontFiles);
      HttpPostedFileBase[] fontFilesAttachment = fontFiles.FontFilesAttachment;
      string path1 = DirectoryHelpers.MakeSureDirectoryExists(Path.Combine(DirectoryHelpers.FontFamilyVirtualDirectory, fontFiles.FontName));
      foreach (HttpPostedFileBase httpPostedFileBase in fontFilesAttachment)
      {
        string filename = Path.Combine(path1, httpPostedFileBase.FileName);
        httpPostedFileBase.SaveAs(filename);
      }
      return (ActionResult) this.Json((object) new
      {
        createStatus = true,
        response = "Files Uploaded Successfully."
      });
    }

    private List<CardTemplates> GetAllCardTemplatesVirtualPath()
    {
      List<CardTemplates> templatesVirtualPath = new List<CardTemplates>();
      string virtualDirectory = DirectoryHelpers.CardTemplatesVirtualDirectory;
      foreach (string file in Directory.GetFiles(HostingEnvironment.MapPath(virtualDirectory)))
      {
        string fileName = Path.GetFileName(file);
        templatesVirtualPath.Add(new CardTemplates()
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
      foreach (string directory in Directory.GetDirectories(HostingEnvironment.MapPath(virtualDirectory)))
      {
        string fileName = Path.GetFileName(directory);
        string path1 = Path.Combine(virtualDirectory, fileName);
        allFontsDetails.Add(new FontFamily()
        {
          FontStylesheetFileFullVirtuaPath = Path.Combine(path1, "stylesheet.css"),
          FontName = fileName
        });
      }
      return allFontsDetails;
    }

    private CardEventDetailsViewModel GetEventDetails(long event_id)
    {
      var data = this._dbContext.event_details.Where(ev => ev.event_det_sno == event_id).Select(ev => new
      {
        ev.inviter_name,
        ev.event_name,
        ev.event_date,
        ev.event_start_time,
        ev.venue,
        ev.reservation
      }).FirstOrDefault();
      if (data == null)
        return null;
      string[] dateFormats = this.GetDateFormats(data.event_date);
      string[] timeFormats = this.GetTimeFormats(data.event_start_time);
      CardEventDetailsViewModel eventDetails = new CardEventDetailsViewModel()
      {
        EventHost = this.TrimAll(data.inviter_name),
        EventName = this.TrimAll(data.event_name),
        EventDate = dateFormats,
        EventStartTime = timeFormats,
        Venue = this.TrimAll(data.venue),
        Reservation = this.TrimAll(data.reservation)
      };

            ViewBag.DateFormats = new SelectList(dateFormats);
            ViewBag.TimeFormats = new SelectList(timeFormats);

       return eventDetails;
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

    private string TrimAll(string event_data)
    {
      return !string.IsNullOrEmpty(event_data) ? event_data.Trim() : (string) null;
    }

    private static string EscapeCharacterForFileName(string inviteeName)
    {
      return inviteeName.Trim().Replace("/", "").Replace("\\", "").Replace(":", "-").Replace("?", "").Replace("\"", "'").Replace("<", "-").Replace(">", "-");
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
        this._dbContext.Dispose();
      base.Dispose(disposing);
    }
  }
}
