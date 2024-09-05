// Decompiled with JetBrains decompiler
// Type: FUNDING.Models.CardGenerationModule.QR_Code.QRCodeGenerator
// Assembly: FUNDING, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5DE1C1E8-8D80-43BD-915D-F2502219BD58
// Assembly location: D:\ECardtest - 22082024\bin\FUNDING.dll

using FUNDING.Models.AppHelpers;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web.Hosting;
using ZXing;
using ZXing.Common;
using ZXing.QrCode.Internal;
using ZXing.Rendering;

 
namespace FUNDING.Models.CardGenerationModule.QR_Code
{
  public static class QRCodeGenerator
  {
    public static void GenerateAction(
      long VisitorId,
      string VisitorqrCodeID,
      string inviteeName,
      string cardSize,
      string venue)
    {
      string appearOnQrCode = QRCodeGenerator.FormatTextsToAppearOnQRCode(VisitorqrCodeID.Substring(9), inviteeName, cardSize, venue);
      string logicLogoFullPath = QRCodeGenerator.GetBizLogicLogoFullPath();
      bool flag = true;
      while (flag)
      {
        try
        {
          using (new MemoryStream())
          {
            string codeFullImagePath = QRCodeGenerator.GetSafeQR_CodeFullImagePath(VisitorId, inviteeName);
            BarcodeWriter barcodeWriter = new BarcodeWriter();
            EncodingOptions encodingOptions = new EncodingOptions()
            {
              Width = 800,
              Height = 800,
              Margin = 0,
              PureBarcode = false
            };
            encodingOptions.Hints.Add(EncodeHintType.ERROR_CORRECTION, (object) ErrorCorrectionLevel.H);
            barcodeWriter.Renderer = (IBarcodeRenderer<Bitmap>) new BitmapRenderer();
            barcodeWriter.Options = encodingOptions;
            barcodeWriter.Format = BarcodeFormat.QR_CODE;
            Bitmap bitmap1 = barcodeWriter.Write(appearOnQrCode);
            Bitmap bitmap2 = new Bitmap(logicLogoFullPath);
            Graphics.FromImage((Image) bitmap1).DrawImage((Image) bitmap2, new Point((bitmap1.Width - bitmap2.Width) / 2, (bitmap1.Height - bitmap2.Height) / 2));
            bitmap1.Save(codeFullImagePath, ImageFormat.Png);
          }
          flag = false;
        }
        catch (Exception ex)
        {
          flag = true;
        }
      }
    }

    public static void GenerateActionWithTableNumber(
      long VisitorId,
      string VisitorqrCodeID,
      string inviteeName,
      string cardSize,
      string venue,
      string table_number)
    {
      string appearOnQrCode = QRCodeGenerator.FormatTextsToAppearOnQRCode(VisitorqrCodeID.Substring(9), inviteeName, cardSize, venue, table_number);
      string logicLogoFullPath = QRCodeGenerator.GetBizLogicLogoFullPath();
      bool flag = true;
      while (flag)
      {
        try
        {
          using (new MemoryStream())
          {
            string codeFullImagePath = QRCodeGenerator.GetSafeQR_CodeFullImagePath(VisitorId, inviteeName);
            BarcodeWriter barcodeWriter = new BarcodeWriter();
            EncodingOptions encodingOptions = new EncodingOptions()
            {
              Width = 800,
              Height = 800,
              Margin = 0,
              PureBarcode = false
            };
            encodingOptions.Hints.Add(EncodeHintType.ERROR_CORRECTION, (object) ErrorCorrectionLevel.H);
            barcodeWriter.Renderer = (IBarcodeRenderer<Bitmap>) new BitmapRenderer();
            barcodeWriter.Options = encodingOptions;
            barcodeWriter.Format = BarcodeFormat.QR_CODE;
            Bitmap bitmap1 = barcodeWriter.Write(appearOnQrCode);
            Bitmap bitmap2 = new Bitmap(logicLogoFullPath);
            Graphics.FromImage((Image) bitmap1).DrawImage((Image) bitmap2, new Point((bitmap1.Width - bitmap2.Width) / 2, (bitmap1.Height - bitmap2.Height) / 2));
            bitmap1.Save(codeFullImagePath, ImageFormat.Png);
          }
          flag = false;
        }
        catch (Exception ex)
        {
          flag = true;
        }
      }
    }

    private static string FormatTextsToAppearOnQRCode(
      string VisitorqrCodeID,
      string inviteeName,
      string cardSize,
      string venue)
    {
      return string.Format("QR Code: {0};\nInvitee: {1};\nCard Size: {2};\nVenue: {3};", (object) VisitorqrCodeID, (object) inviteeName, (object) cardSize, (object) venue);
    }

    private static string FormatTextsToAppearOnQRCode(
      string VisitorqrCodeID,
      string inviteeName,
      string cardSize,
      string venue,
      string table_number)
    {
      return string.IsNullOrWhiteSpace(table_number) ? string.Format("QR Code: {0};\nInvitee: {1};\nCard Size: {2};\nVenue: {3};", (object) VisitorqrCodeID, (object) inviteeName, (object) cardSize, (object) venue) : string.Format("QR Code: {0};\nInvitee: {1};\nCard Size: {2};\nVenue: {3};\nTable No: {4}", (object) VisitorqrCodeID, (object) inviteeName, (object) cardSize, (object) venue, (object) table_number);
    }

    private static string GetSafeQR_CodeFullImagePath(long VisitorId, string inviteeName)
    {
      string withInviteeDetails = QRCodeGenerator.GenerateQRCodeFileNameWithInviteeDetails(VisitorId, inviteeName);
      string str = HostingEnvironment.MapPath(DirectoryHelpers.QrCodeVirtualDirectory);
      if (!Directory.Exists(str))
        Directory.CreateDirectory(str);
      string path = Path.Combine(str, withInviteeDetails);
      if (File.Exists(path))
        File.Delete(path);
      return path;
    }

    private static string GenerateQRCodeFileNameWithInviteeDetails(
      long VisitorId,
      string inviteeName)
    {
      return string.Format("{0}_{1}.jpeg", (object) VisitorId, (object) QRCodeGenerator.EscapeCharacterForFileName(inviteeName));
    }

    private static string GetBizLogicLogoFullPath()
    {
      string path2 = "Biz-Logic-Round-Logo.png";
      return Path.Combine(HostingEnvironment.MapPath(DirectoryHelpers.DefaultVirtualDirectory), path2);
    }

    private static string EscapeCharacterForFileName(string inviteeName)
    {
      return inviteeName.Trim().Replace("/", "").Replace("\\", "").Replace(":", "-").Replace("?", "").Replace("\"", "'").Replace("<", "-").Replace(">", "-");
    }
  }
}
