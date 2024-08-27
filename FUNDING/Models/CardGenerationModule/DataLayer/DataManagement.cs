// Decompiled with JetBrains decompiler
// Type: FUNDING.Models.CardGenerationModule.DataLayer.DataManagement
// Assembly: FUNDING, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5DE1C1E8-8D80-43BD-915D-F2502219BD58
// Assembly location: D:\ECardtest - 22082024\bin\FUNDING.dll

using ECARD.DL.EDMX;
using FUNDING.Models.AppHelpers;
using FUNDING.Models.CardGenerationModule.HtmlElementsComponents;
using FUNDING.Models.CardGererationMaster;
using iText.Kernel.Colors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Hosting;

 
namespace FUNDING.Models.CardGenerationModule.DataLayer
{
  public class DataManagement
  {
    public static string GetCardTemplateFile(int cardTemplateIndex)
    {
      List<string> allCardTemplate = DataManagement.GetAllCardTemplate();
      int num = allCardTemplate.Count - 1;
      if (num < 0)
        throw new FileNotFoundException("No file exists in this directory.");
      return cardTemplateIndex >= 0 && cardTemplateIndex <= num ? allCardTemplate[cardTemplateIndex] : allCardTemplate[0];
    }

    private static List<string> GetAllCardTemplate()
    {
      string str = HostingEnvironment.MapPath(DirectoryHelpers.CardTemplatesVirtualDirectory);
      return new List<string>()
      {
        str + "\\image1.png",
        str + "\\image5.jpg",
        str + "\\Rebeka_Wedding_Card_Template.png",
        str + "\\Graphic.jpeg"
      };
    }

    public static List<Invitees> GetInvitees()
    {
      return new List<Invitees>()
      {
        new Invitees() { Id = 1L, InviteeName = "Mr. A" },
        new Invitees() { Id = 2L, InviteeName = "Mr and Mrs B" },
        new Invitees() { Id = 3L, InviteeName = "Miss C" },
        new Invitees() { Id = 4L, InviteeName = "Prof. D" }
      };
    }

        public static List<Invitees> GetBDInvitees(long event_id)
        {
            using (ECARDAPPEntities _dbContext = new ECARDAPPEntities())
            {
                var inviteesList = _dbContext.visitor_details
                    .Where(iv => iv.event_det_sno == event_id)
                    .Select(iv => new Invitees
                    {
                        Id = iv.visitor_det_sno,
                        InviteeName = iv.visitor_name,
                        CardSize = iv.no_of_persons

                    })
                    .ToList();

                return inviteesList;

            }
        }

        public static TextElement GetInviteeNameDraggableTextElement(string inviteeName)
    {
      string str = HostingEnvironment.MapPath(DirectoryHelpers.FontFamilyVirtualDirectory);
      DataManagement.GetAllSystemFonts();
      FontMaster font = new FontMaster("Acme", str + "\\Acme-Regular.ttf", 21f);
      return new TextElement(inviteeName, font, (Color) new DeviceRgb(0, 35, 102));
    }

    public static TextElement GetInviteeNameDraggableTextElement(
      string inviteeName,
      FontMasterClass fontMaster)
    {
      HostingEnvironment.MapPath(DirectoryHelpers.FontFamilyVirtualDirectory);
      FontMaster font = new FontMaster(fontMaster.FontName, fontMaster.FontSize);
      DeviceRgb colorValue = DataManagement.GetColorValue(fontMaster);
      return new TextElement(inviteeName, font, (Color) colorValue, fontMaster.TextAlign)
      {
        IsTextElementBold = fontMaster.Bold,
        IsTextElementItalic = fontMaster.Italic,
        IsTextElementUnderlined = fontMaster.Underline
      };
    }

    public static DeviceRgb GetColorValue(FontMasterClass fontMaster)
    {
      int[] colorValues = fontMaster.GetColorValues();
      return new DeviceRgb(colorValues[0], colorValues[1], colorValues[2]);
    }

    public static List<TextElement> GetAllDraggableTextElement(string inviteeName)
    {
      Dictionary<string, FontMaster> allSystemFonts = DataManagement.GetAllSystemFonts();
      return new List<TextElement>()
      {
        new TextElement("Mr & Mrs Host", allSystemFonts["Lora"], (Color) new DeviceRgb(0, 0, 0)),
        new TextElement("A word of welcome....", allSystemFonts["Roboto"], (Color) new DeviceRgb(0, 0, 0)),
        new TextElement("Event Name", allSystemFonts["Arizonia"], (Color) new DeviceRgb(0, 0, 0)),
        new TextElement(inviteeName, allSystemFonts["Rakkas"], (Color) new DeviceRgb(0, 0, 0)),
        new TextElement("14/02/2022", allSystemFonts["Roboto"], (Color) new DeviceRgb(0, 0, 0)),
        new TextElement("18:00", allSystemFonts["Roboto"], (Color) new DeviceRgb(0, 0, 0)),
        new TextElement("Golden Hall, Sinza Mori", allSystemFonts["Roboto"], (Color) new DeviceRgb(0, 0, 0)),
        new TextElement("Reservation", allSystemFonts["Roboto"], (Color) new DeviceRgb(0, 0, 0))
      };
    }

    public static List<ElementRelativePosition> GetAllElementsPosition()
    {
      return new List<ElementRelativePosition>()
      {
        new ElementRelativePosition(0.0f, 100f),
        new ElementRelativePosition(0.0f, 100f),
        new ElementRelativePosition(0.0f, 100f),
        new ElementRelativePosition(0.0f, 100f),
        new ElementRelativePosition(0.0f, 100f),
        new ElementRelativePosition(0.0f, 100f),
        new ElementRelativePosition(0.0f, 100f),
        new ElementRelativePosition(0.0f, 100f)
      };
    }

    public static Dictionary<string, FontMaster> GetAllSystemFonts()
    {
      string path1 = HostingEnvironment.MapPath(DirectoryHelpers.FontFamilyVirtualDirectory);
      return new Dictionary<string, FontMaster>()
      {
        {
          "Lora",
          new FontMaster("Lora", Path.Combine(path1, "Lora-Regular.ttf"))
        },
        {
          "FjallaOne",
          new FontMaster("FjallaOne", Path.Combine(path1 + "FjallaOne-Regular.ttf"))
        },
        {
          "Arizonia",
          new FontMaster("Arizonia", Path.Combine(path1 + "Arizonia-Regular.ttf"))
        },
        {
          "Roboto",
          new FontMaster("Roboto", Path.Combine(path1 + "Roboto-Regular.ttf"))
        },
        {
          "Rakkas",
          new FontMaster("Roboto", Path.Combine(path1 + "Rakkas-Regular.ttf"), 21f)
        },
        {
          "Acme",
          new FontMaster("Acme", Path.Combine(path1 + "Acme-Regular.ttf"), 21f)
        }
      };
    }
  }
}
