// Decompiled with JetBrains decompiler
// Type: FUNDING.Models.CardGenerationModule.CardGenerationService
// Assembly: FUNDING, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5DE1C1E8-8D80-43BD-915D-F2502219BD58
// Assembly location: D:\ECardtest - 22082024\bin\FUNDING.dll

using ECARD.DL.EDMX;
using FUNDING.Models.AppHelpers;
using FUNDING.Models.CardGenerationModule.DataLayer;
using FUNDING.Models.CardGenerationModule.HtmlElements;
using FUNDING.Models.CardGenerationModule.HtmlElementsComponents;
using FUNDING.Models.CardGererationMaster;
using FUNDING.Models.CardGererationMaster.DraggableElements;
using iText.IO.Image;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
using System;
using System.Drawing.Imaging;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.Hosting;

 
namespace FUNDING.Models.CardGenerationModule
{
  public class CardGenerationService
  {
    public static EditableDraggableElement VisitorsElementValues { get; set; }

    public static EditableDraggableElement EditableElementValues { get; set; }

    public static EditableDraggableElement TableNumberLabelElementValues { get; set; }

    public static EditableDraggableElement CardSizeLabelElementValues { get; set; }

    public static EditableDraggableElement TableNumberDigitElementValues { get; set; }

    public static string CardTemplateFile { get; set; }

    public static QrCodeDraggableElement QrCodeElementValues { get; set; }

    public static bool IsCardSizeDisplayed { get; set; }

    private static string FullCardFileName(Invitees invitee)
    {
      string inviteeName = string.Format("{0}_{1}", invitee.Id, invitee.InviteeName.Trim());
      string path = string.Format(@"{0}\{1}.pdf", HostingEnvironment.MapPath(DirectoryHelpers.GeneratedPDFVirtualDirectory),EscapeCharacterForFileName(inviteeName));
      if (File.Exists(path))
        File.Delete(path);
      return path;
    }

    public static void DedicatedPdfGenerator(Invitees invitee)
    {
      iText.Kernel.Pdf.PdfDocument pdfDoc = new iText.Kernel.Pdf.PdfDocument(new PdfWriter(FullCardFileName(invitee)));
      PageSize pageSize = (PageSize) PageSize.DEFAULT.SetWidth(1024).SetHeight(481);
      string cardTemplateFile = DataManagement.GetCardTemplateFile(4);
      using (Document document = new Document(pdfDoc, pageSize))
      {
        new PdfCanvas(pdfDoc.AddNewPage()).AddImageFittedIntoRectangle(ImageDataFactory.Create(cardTemplateFile), pageSize, false);
        document.SetMargins(0, 0, 0, 0);
        string inviteeName = invitee.InviteeName.Trim();
        int? cardSize = invitee.CardSize;
        int? nullable = cardSize;
        int num1 = 1;
        if (nullable.GetValueOrDefault() == num1 & nullable.HasValue)
        {
          inviteeName += "\n(Single)";
        }
        else
        {
          nullable = cardSize;
          int num2 = 2;
          if (nullable.GetValueOrDefault() == num2 & nullable.HasValue)
            inviteeName += "\n(Double)";
        }
        TextElement draggableTextElement1 = DataManagement.GetInviteeNameDraggableTextElement(inviteeName);
        ElementRelativePosition position = new ElementRelativePosition(0, 0);
        DraggableTextElement draggableTextElement2 = new DraggableTextElement(document, draggableTextElement1, position);
        AddImageDraggableElement(document, invitee);
      }
    }

    public static string PdfGenerator(Invitees invitee)
    {
      string filename = (string) null;
      bool flag = true; //isFileUsedByAnotherProcess
     while (flag)
      {
        try
        {
          filename = FullCardFileName(invitee);
          //string filePath = filename.Replace("\\", @"\");

        using (PdfDocument pdfDoc = new PdfDocument(new PdfWriter(filename)))
          {
            PageSize a5 = PageSize.A5;
            string cardTemplateFile = CardTemplateFile;
            using (Document document = new Document(pdfDoc, a5))
            {
              new PdfCanvas(pdfDoc.AddNewPage()).AddImageFittedIntoRectangle(ImageDataFactory.Create(cardTemplateFile), (Rectangle) a5, false);
              document.SetMargins(0, 0, 0, 0);
              string inviteeName1 = invitee.InviteeName.Trim();
              string inviteeName2 = invitee.TableNumber.Trim();
              int? cardSize = invitee.CardSize;
              if (IsCardSizeDisplayed)
              {
                int? nullable = cardSize;
                int num1 = 1;
                if (nullable.GetValueOrDefault() == num1 & nullable.HasValue)
                {
                  inviteeName1 += "\n(Single)";
                }
                else
                {
                  nullable = cardSize;
                  int num2 = 2;
                  if (nullable.GetValueOrDefault() == num2 & nullable.HasValue)
                  {
                    inviteeName1 += "\n(Double)";
                  }
                  else
                  {
                    nullable = cardSize;
                    int num3 = 2;
                    if (nullable.GetValueOrDefault() > num3 & nullable.HasValue)
                      inviteeName1 += "\n(Group)";
                  }
                }
              }
              TextElement textElement1 = (TextElement) null;
              int? nullable1 = cardSize;
              int num4 = 1;
              if (nullable1.GetValueOrDefault() == num4 & nullable1.HasValue)
              {
                textElement1 = DataManagement.GetInviteeNameDraggableTextElement("SINGLE", CardSizeLabelElementValues.FontDetails);
              }
              else
              {
                int? nullable2 = cardSize;
                int num5 = 2;
                if (nullable2.GetValueOrDefault() == num5 & nullable2.HasValue)
                {
                  textElement1 = DataManagement.GetInviteeNameDraggableTextElement("DOUBLE", CardSizeLabelElementValues.FontDetails);
                }
                else
                {
                  nullable2 = cardSize;
                  int num6 = 2;
                  if (nullable2.GetValueOrDefault() > num6 & nullable2.HasValue)
                    textElement1 = DataManagement.GetInviteeNameDraggableTextElement("GROUP", CardSizeLabelElementValues.FontDetails);
                }
              }
              ElementPosition position1 = CardSizeLabelElementValues.Position;
              float leftPosition1 = (float) position1.LeftPosition;
              position1 = CardSizeLabelElementValues.Position;
              float topPosition1 = (float) position1.TopPosition;
              ElementRelativePosition position2 = new ElementRelativePosition(leftPosition1, topPosition1);
              DraggableTextElement draggableTextElement1 = new DraggableTextElement(document, textElement1, position2);
              TextElement draggableTextElement2 = DataManagement.GetInviteeNameDraggableTextElement(inviteeName1, EditableElementValues.FontDetails);
              position1 = EditableElementValues.Position;
              float leftPosition2 = (float) position1.LeftPosition;
              position1 = EditableElementValues.Position;
              float topPosition2 = (float) position1.TopPosition;
              ElementRelativePosition position3 = new ElementRelativePosition(leftPosition2, topPosition2);
              DraggableTextElement draggableTextElement3 = new DraggableTextElement(document, draggableTextElement2, position3);
              TextElement textElement2 = string.IsNullOrWhiteSpace(inviteeName2) ? DataManagement.GetInviteeNameDraggableTextElement("", TableNumberLabelElementValues.FontDetails) : DataManagement.GetInviteeNameDraggableTextElement("Table No", TableNumberLabelElementValues.FontDetails);
              position1 = TableNumberLabelElementValues.Position;
              float leftPosition3 = (float) position1.LeftPosition;
              position1 = TableNumberLabelElementValues.Position;
              float topPosition3 = (float) position1.TopPosition;
              ElementRelativePosition position4 = new ElementRelativePosition(leftPosition3, topPosition3);
              DraggableTextElement draggableTextElement4 = new DraggableTextElement(document, textElement2, position4);
              TextElement textElement3 = string.IsNullOrWhiteSpace(inviteeName2) ? DataManagement.GetInviteeNameDraggableTextElement(inviteeName2,
                  TableNumberDigitElementValues.FontDetails) : DataManagement.GetInviteeNameDraggableTextElement(inviteeName2, TableNumberDigitElementValues.FontDetails);
              position1 = TableNumberDigitElementValues.Position;
              float leftPosition4 = (float) position1.LeftPosition;
              position1 = TableNumberDigitElementValues.Position;
              float topPosition4 = (float) position1.TopPosition;
              ElementRelativePosition position5 = new ElementRelativePosition(leftPosition4, topPosition4);
              DraggableTextElement draggableTextElement5 = new DraggableTextElement(document, textElement3, position5);
              AddImageDraggableElement(document, invitee);
            }
          }
          flag = false;
        }
        catch (Exception ex)
        {
            ECARDAPPEntities context = new ECARDAPPEntities();
            var errorLog = new service_error_logs
            {
                error = ex.ToString(),
                posted_date = DateTime.Now
            };
            context.service_error_logs.Add(errorLog);
            context.SaveChangesAsync();
            flag = true;
        }
      }
      return filename;
    }

    private static string EscapeCharacterForFileName(string inviteeName)
    {
      return inviteeName.Trim().Replace("/", "").Replace("\\", "").Replace(":", "-").Replace("?", "").Replace("\"", "'").Replace("<", "-").Replace(">", "-");
    }

    private static void AddImageDraggableElement(Document document, Invitees invitee)
    {
      string imageFullPath = string.Format(@"{0}\{1}_{2}.jpeg",  HostingEnvironment.MapPath(DirectoryHelpers.QrCodeVirtualDirectory),  invitee.Id,  EscapeCharacterForFileName(invitee.InviteeName.Trim()));
      ElementRelativePosition position = new ElementRelativePosition((float) QrCodeElementValues.Position.LeftPosition, (float) QrCodeElementValues.Position.TopPosition);
      QrCodeElement qrCode = new QrCodeElement(imageFullPath, QrCodeElementValues.QrCodeSize);
      DraggableQrCodeElement draggableQrCodeElement = new DraggableQrCodeElement(document, qrCode, position);
    }

    public static string ConvertPDF2png(string pdfGeneratedFile)
    {
      return GenCards(pdfGeneratedFile);
    }



    private static string GenCards(string pdfFile)
    {
            string pngGeneratedFile;

            if (Regex.IsMatch(pdfFile, "\\.pdf"))
            {
                var isFileUsedByAnotherProcess = true;

                while (isFileUsedByAnotherProcess)
                {
                    try
                    {
                        using (var file = new FileStream(pdfFile, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                        {
                            // If using Professional version, put your serial key below.
                            GemBox.Pdf.ComponentInfo.SetLicense("FREE-LIMITED-KEY");

                            using (GemBox.Pdf.PdfDocument document = GemBox.Pdf.PdfDocument.Load(file))
                            {
                                // In order to achieve the conversion of a loaded PDF file to image,
                                // we just need to save a PdfDocument object to desired image format.

                                var fileName = System.IO.Path.GetFileNameWithoutExtension(pdfFile);

                                var virtualPath1 = DirectoryHelpers.Generated_Cards_DirVirtualDirectory;
                                var absolutePath1 = HostingEnvironment.MapPath(virtualPath1);

                                if (!Directory.Exists(absolutePath1)) Directory.CreateDirectory(absolutePath1);

                               var pngFile = System.IO.Path.Combine(absolutePath1, string.Format($"{fileName}.jpeg"));


                                if (File.Exists(pngFile)) File.Delete(pngFile);

                                document.Save(pngFile);

                                pngGeneratedFile = pngFile;

                                document.Close();
                            }

                            file.Close();
                        }

                        isFileUsedByAnotherProcess = false;
                        //File.Delete(pdfFile);
                        return pngGeneratedFile;
                    }
                    catch (Exception)
                    {
                        isFileUsedByAnotherProcess = true;
                    }
                }

            }
            return null;
        }

        private static string GenCards1(string pdfFile)
    {
      if (Regex.IsMatch(pdfFile, "\\.pdf"))
      {
        bool flag = true;
        while (flag)
        {
          try
          {
            string str1;
            using (FileStream fileStream = new FileStream(pdfFile, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
            {
                 GemBox.Pdf.ComponentInfo.SetLicense("FREE-LIMITED-KEY");
              using (GemBox.Pdf.PdfDocument pdfDocument = GemBox.Pdf.PdfDocument.Load((Stream) fileStream))
              {
                string withoutExtension = System.IO.Path.GetFileNameWithoutExtension(pdfFile);
                HostingEnvironment.MapPath(DirectoryHelpers.Generated_Cards_DirVirtualDirectory);
                string str2 = HostingEnvironment.MapPath(DirectoryHelpers.Generated_Cards_DirVirtualDirectory);
                if (!Directory.Exists(str2))
                  Directory.CreateDirectory(str2);
                string path = System.IO.Path.Combine(str2, string.Format($"{withoutExtension}.jpeg"));
                if (File.Exists(path))
                  File.Delete(path);
                pdfDocument.Save(path);
                str1 = path;
              }
              fileStream.Close();
            }
            return str1;
          }
          catch (Exception ex)
          {
            ECARDAPPEntities context = new ECARDAPPEntities();
            var errorLog = new service_error_logs
            {
                error = ex.ToString(),
                posted_date = DateTime.Now
            };
            context.service_error_logs.Add(errorLog);
            context.SaveChangesAsync();
            flag = true;
          }
        }
      }
      return (string) null;
    }

    public static void SaveJpeg(string path, iText.Layout.Element.Image img, long quality)
    {
      EncoderParameter encoderParameter = quality >= 0 && quality <= 100 ? new EncoderParameter(Encoder.Quality, quality) : throw new ArgumentOutOfRangeException("quality must be between 0 and 100.");
      GetEncoderInfo("image/jpeg");
      new EncoderParameters(1).Param[0] = encoderParameter;
    }

    private static ImageCodecInfo GetEncoderInfo(string mimeType)
    {
      ImageCodecInfo[] imageEncoders = ImageCodecInfo.GetImageEncoders();
      for (int index = 0; index < imageEncoders.Length; ++index)
      {
        if (imageEncoders[index].MimeType == mimeType)
          return imageEncoders[index];
      }
      return (ImageCodecInfo) null;
    }
  }
}
