// Decompiled with JetBrains decompiler
// Type: FUNDING.Controllers.VisitorVerificationController
// Assembly: FUNDING, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5DE1C1E8-8D80-43BD-915D-F2502219BD58
// Assembly location: D:\ECardtest - 22082024\bin\FUNDING.dll

using ECARD.DL.EDMX;
using FUNDING.BL.BusinessEntities.Masters;
using FUNDING.Models.AppHelpers;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Hosting;
using System.Web.Mvc;

 
namespace FUNDING.Controllers
{
  public class VisitorVerificationController : Controller
  {
    private readonly Visitors _visitor = new Visitors();
    private readonly ECARDAPPEntities _dbContext = new ECARDAPPEntities();

    [Route("Invitee-Verification")]
    public ActionResult Index() { var eventID = this.GetEventID();  ViewBag.eventId = eventID;  return View(); }

    public ActionResult IndexDataTable()
    {
      long eventAdminID = Convert.ToInt64(this.Session["EventAdminID"]);
      var eventID = this.GetEventID();
      var qr = this._dbContext.qr_verify_details.Where(v => v.event_det_sno == eventID && v.cust_reg_sno == eventAdminID).Select(vd => new
      {
        qr_ver_det_sno = vd.qr_ver_det_sno,
        visitor_name = vd.visitor_details.visitor_name,
        no_of_persons = vd.no_of_persons,
        event_start_time = vd.event_start_time,
        posted_date = vd.posted_date,
        eventID = vd.event_det_sno,
        scan_status = vd.scan_status
      }).ToList();
      int index = 1;
      return (ActionResult) this.Json((object) new
      {
        data = qr.Select(vd =>
        {
          int num = index++;
          long qrVerDetSno = vd.qr_ver_det_sno;
          string visitorName = vd.visitor_name;
          int? noOfPersons = vd.no_of_persons;
          DateTime dateTime = Convert.ToDateTime((object) vd.event_start_time);
          string str1 = dateTime.ToString("hh:mm tt");
          dateTime = Convert.ToDateTime(vd.posted_date);
          dateTime = dateTime.AddMinutes(-150.0);
          string str2 = dateTime.ToString("hh:mm:ss tt");
          string scanStatus = this.GetScanStatus(vd.qr_ver_det_sno);
          return new
          {
            id = num,
            qr_ver_det_sno = qrVerDetSno,
            visitor_name = visitorName,
            no_of_persons = noOfPersons,
            event_start_time = str1,
            check_in_time = str2,
            scan_status = scanStatus
          };
        }),
        TotalInvitees = qr.Select(t => t.no_of_persons).DefaultIfEmpty<int?>(new int?(0)).Sum()
      });
    }


        #region Simplified IndexDataTable
        /*
         var qrList = this._dbContext.qr_verify_details
                .Where(v => v.event_det_sno == eventID && v.cust_reg_sno == eventAdminID)
                .Select(vd => new
                {
                    vd.qr_ver_det_sno,
                    vd.visitor_details.visitor_name,
                    vd.no_of_persons,
                    vd.event_start_time,
                    vd.posted_date,
                    vd.event_det_sno,
                    vd.scan_status
                })
                .ToList();

            int index = 1;
            var json = Json(new
            {
                data = qrList.Select(vd => new
                {
                    id = index++,
                    qr_ver_det_sno = vd.qr_ver_det_sno,
                    visitor_name = vd.visitor_name,
                    no_of_persons = vd.no_of_persons,
                    event_start_time = vd.event_start_time.ToString("hh:mm tt"),
                    check_in_time = vd.posted_date.AddMinutes(-150).ToString("hh:mm:ss tt"),
                    scan_status = this.GetScanStatus(vd.qr_ver_det_sno)
                }),
                TotalInvitees = qrList.Sum(t => t.no_of_persons ?? 0)
            });

         */
        #endregion



        [Route("CheckinReportinvitees")]
        public ActionResult CheckinReportinvitees(long event_id)
        {

            if (event_id.ToString() == null)
            {
                return null;
            }

            // Get Invoice and Invoice Items from Invoiceno here
            var invitee = _dbContext.qr_verify_details.Where(v => v.event_det_sno == event_id).FirstOrDefault();
            var invitees = _dbContext.visitor_details.Where(iv => iv.event_det_sno == event_id).ToList();

            long eventAdminID = Convert.ToInt64(Session["EventAdminID"]);
            var eventID = this.GetEventID();
            var qr = this._dbContext.qr_verify_details
                .Where(v => v.event_det_sno == eventID && v.cust_reg_sno == eventAdminID)
                .Select(vd => new
                {
                    vd.qr_ver_det_sno,
                    vd.visitor_details.visitor_name,
                    vd.no_of_persons,
                    vd.event_start_time,
                    vd.posted_date,
                    vd.event_det_sno,
                    vd.scan_status
                }).ToList();
            int index = 1;
            var data = qr.Select(vd =>
            {
                int num = index++;
                long qrVerDetSno = vd.qr_ver_det_sno;
                string visitorName = vd.visitor_name;
                int? noOfPersons = vd.no_of_persons;
                DateTime dateTime = Convert.ToDateTime((object)vd.event_start_time);
                string str1 = dateTime.ToString("hh:mm tt");
                dateTime = Convert.ToDateTime(vd.posted_date);
                dateTime = dateTime.AddMinutes(-150.0);
                string str2 = dateTime.ToString("hh:mm:ss tt");
                string scanStatus = this.GetScanStatus(vd.qr_ver_det_sno);
                return new
                {
                    id = num,
                    qr_ver_det_sno = qrVerDetSno,
                    visitor_name = visitorName,
                    event_id = eventID,
                    no_of_persons = noOfPersons,
                    event_start_time = str1,
                    check_in_time = str2,
                    scan_status = scanStatus
                };
            });
            var TotalInvitees = qr.Select(t => t.no_of_persons).DefaultIfEmpty(new int?(0)).Sum();
            var TotalVisitors = invitees.Select(v => v.no_of_persons).DefaultIfEmpty(new int?(0)).Sum();

            /*var json = Json(new
            {
                data,
                TotalInvitees

            });*/



            //string path = "/Invoices/";

            // Set the file path for the PDF
        string filePath = Path.Combine(HostingEnvironment.ApplicationHost.GetPhysicalPath() + ConfigurationManager.AppSettings["reports"], $"Invitees-Check-In_{invitee.event_details.event_name.ToString().Trim()}_Report.pdf");
        string filePath1 = Path.Combine(HostingEnvironment.ApplicationHost.GetPhysicalPath(), $"Invitees-Check-In_{invitee.event_details}.pdf");

        string filePath2 = ConfigurationManager.AppSettings["reports"] + $"Invitees-Check-In_{invitee.event_details.event_name.ToString().Trim()}.pdf";

        // Create a new PDF document
        Document document = new Document(PageSize.A4, 25, 25, 10, 10);
        PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
        document.Open();

        // Step 1: Add Company Logo
        string logoPath = Path.Combine(HostingEnvironment.ApplicationHost.GetPhysicalPath() + ConfigurationManager.AppSettings["reports"], "logo.png"); // Path to your logo image

        string logoPath1 = HostingEnvironment.ApplicationHost.GetPhysicalPath() + ConfigurationManager.AppSettings["reports"] + "logo.png"; // Path to your logo image
        if (System.IO.File.Exists(logoPath))
        {
            Image logo = Image.GetInstance(logoPath);
            logo.ScalePercent(18f); // Resize the logo if needed
            logo.Alignment = Element.ALIGN_CENTER;
            document.Add(logo);
        }

        BaseColor LabelColor = new BaseColor(37, 150, 190); // RGB Base custom color
        BaseColor HeaderColor = new BaseColor(12, 98, 133, 255); // for table header and total background
        BaseColor tableColor = new BaseColor(11, 99, 133);
        BaseColor ShadesColor = new BaseColor(12, 103, 148);
        BaseColor textColor = new BaseColor(20, 36, 44);

        // Step 1.0: Create a Paragraph
        Paragraph paragraph = new Paragraph();
        Paragraph paragraph1 = new Paragraph();
        Paragraph paragraph2 = new Paragraph();
        Paragraph paragraph3 = new Paragraph();

        // Step 1.1: Add a Tab to Push Content to the Right
        Chunk tab = new Chunk(new VerticalPositionMark()); // Acts as a spacer to the right  BaseColor.GRAY

        // Step 2: Add Invoice Header
        Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14, HeaderColor);
        Font headerFontbelow = FontFactory.GetFont(FontFactory.HELVETICA, 11, textColor);
        Paragraph header = new Paragraph("Invitees-Check-In Report", headerFont)
        {
            Alignment = Element.ALIGN_CENTER
        };
        Paragraph header2 = new Paragraph("Event: " + invitee.event_details.event_name.ToString(), headerFont)
        {
            Alignment = Element.ALIGN_CENTER
        };
        Paragraph header3 = new Paragraph("Total Invitees Verified: " + TotalInvitees + " Out of :"+ TotalVisitors, headerFont)
        {
            Alignment = Element.ALIGN_CENTER
        };
        /*Paragraph header4 = new Paragraph("Total Invitees Verified: " + TotalInvitees, headerFont)
        {
            Alignment = Element.ALIGN_CENTER
        };*/


        document.Add(header);
        document.Add(header2);
        document.Add(header3);
        //document.Add(header4);


            // Add a blank line after the header
            document.Add(new Paragraph("\n"));

        // Step 3: Create Table for Invoice Details
        PdfPTable table = new PdfPTable(5)
        {
            WidthPercentage = 100, // Table width as percentage of page width
            SpacingBefore = 20f,
            SpacingAfter = 20f
        }; // 7 columns

        // Set column widths
        float[] columnWidths = { 1f, 3f, 2f, 2f, 2f };
        table.SetWidths(columnWidths);

        // Add table headers
        AddTableHeader(table, "Sno");
        AddTableHeader(table, "Invitee Name");
        AddTableHeader(table, "Checked-in Invitee");
        AddTableHeader(table, "Check-in time");
        AddTableHeader(table, "Status");
       /* AddTableHeader(table, "Mobile No");
        AddTableHeader(table, "Email");*/
        //int o = 1;
            // Step 4: Add Invoice Items
            
            foreach (var item in data)
        {
            AddTableCell(table, item.id.ToString() ?? "");
            AddTableCell(table, item.visitor_name ?? "");
            AddTableCell(table, text: item.no_of_persons.ToString() ?? "");
            AddTableCell(table, item.check_in_time ?? "");
            AddTableCell(table, item.scan_status ?? "");
          /*  AddTableCell(table, item.mobile_no.ToString() ?? "");
            AddTableCell(table, item.email_address ?? "");*/
           // o++;
        }
  
        document.Add(table);

        document.Add(new Paragraph("\n"));

        // Step 7: Add Footer with Thank You Message
        Paragraph footer = new Paragraph("B-envit : System Generated Report." + " Date : " + DateTime.Now, FontFactory.GetFont(FontFactory.HELVETICA, 10, textColor))
        {
            Alignment = Element.ALIGN_CENTER,
            SpacingBefore = 12f
        };
        document.Add(footer);

        // Close the document
        document.Close();


        var directorypath = HostingEnvironment.MapPath(DirectoryHelpers.Pdf_Report_DirVirtualDirectory);
        var cardPath = filePath;

        var path = Path.Combine(directorypath, cardPath);

        if (System.IO.File.Exists(path))
        {
            //Read the File data into Byte Array.
            byte[] bytes = System.IO.File.ReadAllBytes(path);

            string downloadedFileName = DirectoryHelpers.GetTimestampedFile(path);

            //Send the File to Download.
            return File(bytes, System.Net.Mime.MediaTypeNames.Application.Pdf, downloadedFileName);
        }
          

        return Json(new
        {
            sendStatus = false,
            mesage = "File does not exist"
        });
    }



    private static void AddTableHeader(PdfPTable table, string headerText)
    {

        BaseColor LabelColor = new BaseColor(12, 98, 133, 255);

        PdfPCell headerCell = new PdfPCell(new Phrase(headerText, new Font(Font.FontFamily.HELVETICA, 11, Font.BOLD, BaseColor.WHITE)))
        {
            BackgroundColor = LabelColor,
            HorizontalAlignment = Element.ALIGN_LEFT,
            Padding = 2f
        };
        table.AddCell(headerCell);
    }

    private static void AddTableCell(PdfPTable table, string text)
    {
        PdfPCell cell = new PdfPCell(new Phrase(text, new Font(Font.FontFamily.HELVETICA, 10)))
        {
            HorizontalAlignment = Element.ALIGN_LEFT,
            Padding = 2f
        };
        table.AddCell(cell);
    }

    private string GetScanStatus(long qr_ver_det_sno)
    {
        IQueryable<qr_verify_details> source = this._dbContext.qr_verify_details.Include("visitor_details").Where(c => c.qr_ver_det_sno == qr_ver_det_sno);
        if (source == null)
        return (string) null;
        qr_verify_details qrVerifyDetails = source.ToList()[0];
        int int32_1 = Convert.ToInt32((object) qrVerifyDetails.no_of_persons);
        string qrCode = qrVerifyDetails.qrcode;
        int int32_2 = Convert.ToInt32((object) qrVerifyDetails.visitor_details.no_of_persons);
        int num = Queryable.Sum(this._dbContext.qr_verify_details.Where(iv => iv.qrcode == qrCode && iv.qr_ver_det_sno < qr_ver_det_sno).Select((iv => iv.no_of_persons)).DefaultIfEmpty<int?>(new int?(0))).Value;
        return int32_1 < int32_2 - num ? string.Format("{0}/{1} Scanned", (object) (num + int32_1), (object) int32_2) : string.Format("{0}/{1} Scanned", (object) int32_2, (object) int32_2);
    }

    private long? GetCustomerAdminID() => new long?(Convert.ToInt64(this.Session["EventAdminID"]));

    private long? GetEventID() => new long?(Convert.ToInt64(this.Session["EventID"]));
  }
}
