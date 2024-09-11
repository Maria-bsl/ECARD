// Decompiled with JetBrains decompiler
// Type: FUNDING.Models.AppHelpers.ExcelUploadedFileHandler
// Assembly: FUNDING, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5DE1C1E8-8D80-43BD-915D-F2502219BD58
// Assembly location: D:\ECardtest - 22082024\bin\FUNDING.dll

using ECARD.DL.EDMX;
using ExcelDataReader;
using FUNDING.BL.BusinessEntities.Masters;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

 
namespace FUNDING.Models.AppHelpers
{
  public class ExcelUploadedFileHandler
  {
    public static string UploadedExcelSaveProcess(HttpPostedFileBase uploadedExcelFile)
    {
      string str = Path.Combine(DirectoryHelpers.ExcelTemplateAbsoluteDirectory, DirectoryHelpers.GetTimestampedFileName(uploadedExcelFile.FileName));
      if (File.Exists(str))
        File.Delete(str);
      uploadedExcelFile.SaveAs(str);
      return str;
    }

    public static void DeleteUploadedFile(string uploadedExcelFile)
    {
      if (!File.Exists(uploadedExcelFile))
        return;
      File.Delete(uploadedExcelFile);
    }

    public static void ReadExcelData(
      string uploadedExcelFile,
      List<visitor_details> listOfVisitors,
      long? CustomerAdminID,
      long? EventID,
      string PostedBy)
    {
      using (FileStream fileStream = File.Open(uploadedExcelFile, FileMode.Open, FileAccess.Read, FileShare.None))
      {
        using (IExcelDataReader reader = ExcelReaderFactory.CreateReader((Stream) fileStream))
        {
          int num = 0;
          while (reader.Read())
          {
            if (num > 0 && !IsReaderNull(reader))
              listOfVisitors.Add(new visitor_details()
              {
                visitor_name = GetStringExcelValue(reader.GetValue(0).ToString().Trim()),
                no_of_persons = GetIntegerExcelValue(reader.GetValue(1)),
                mobile_no = GetStringExcelValue(reader.GetValue(2)),
                email_address = GetStringExcelValue(reader.GetValue(3)),
                table_number = GetStringExcelValue(reader.GetValue(4)),
                card_state_mas_sno = CardStates_Master.DEFAULT_CARBD_STATE_ID,
                cust_reg_sno = CustomerAdminID,
                event_det_sno = EventID,
                posted_by = PostedBy,
                posted_date = DateTime.Now
              });
            ++num;
          }
        }
      }
    }

    public static void ReadExcelDataWithQRcode(
      string uploadedExcelFile,
      List<visitor_details> listOfVisitors,
      long? CustomerAdminID,
      long? EventID,
      string PostedBy)
    {
      using (FileStream fileStream = File.Open(uploadedExcelFile, FileMode.Open, FileAccess.Read, FileShare.None))
      {
        using (IExcelDataReader reader = ExcelReaderFactory.CreateReader((Stream) fileStream))
        {
          int num = 0;
          while (reader.Read())
          {
            if (num > 0 && !IsReaderNull(reader))
              listOfVisitors.Add(new visitor_details()
              {
                visitor_name = GetStringExcelValue(reader.GetValue(0).ToString().Trim()),
                no_of_persons = GetIntegerExcelValue(reader.GetValue(1)),
                qrcode = GetStringExcelValue((object) (reader.GetValue(2)?.ToString() + reader.GetValue(3)?.ToString())),
                mobile_no = GetStringExcelValue(reader.GetValue(4)),
                email_address = GetStringExcelValue(reader.GetValue(5)),
                table_number = GetStringExcelValue(reader.GetValue(6)),
                card_state_mas_sno = CardStates_Master.DEFAULT_CARBD_STATE_ID,
                cust_reg_sno = CustomerAdminID,
                event_det_sno = EventID,
                posted_by = PostedBy,
                posted_date = DateTime.Now
              });
            ++num;
          }
        }
      }
    }

    public static void SaveRecordToDatabaseWithQRcode(List<visitor_details> listOfVisitors)
    {
      foreach (visitor_details listOfVisitor in listOfVisitors)
      {
        using (ECARDAPPEntities dbContext = new ECARDAPPEntities())
        {
          if (!IsTheCurrentUserAlreadyExists(dbContext, listOfVisitor))
          {
            dbContext.visitor_details.Add(listOfVisitor);
            dbContext.SaveChanges();
          }
        }
      }
    }

    public static void SaveRecordToDatabase(List<visitor_details> listOfVisitors)
    {
      foreach (visitor_details listOfVisitor in listOfVisitors)
      {
        using (ECARDAPPEntities dbContext = new ECARDAPPEntities())
        {
          if (!IsTheCurrentUserAlreadyExists(dbContext, listOfVisitor))
          {
            dbContext.visitor_details.Add(listOfVisitor);
            dbContext.SaveChanges();
            listOfVisitor.qrcode = Visitors.GetGeneratedQRCodeWithEventId(dbContext, listOfVisitor.event_det_sno);
            dbContext.Entry<visitor_details>(listOfVisitor).State = EntityState.Modified;
            dbContext.SaveChanges();
          }
        }
      }
    }

    private static bool IsTheCurrentUserAlreadyExists(
      ECARDAPPEntities dbContext,
      visitor_details visitor)
    {
      return dbContext.visitor_details.Where(cv => cv.event_det_sno == visitor.event_det_sno && cv.visitor_name == visitor.visitor_name && cv.mobile_no == visitor.mobile_no).FirstOrDefault() != null;
    }

    private static bool IsReaderNull(IExcelDataReader reader)
    {
      string stringExcelValue1 = GetStringExcelValue(reader.GetValue(0));
      int? integerExcelValue = GetIntegerExcelValue(reader.GetValue(1));
      string stringExcelValue2 = GetStringExcelValue(reader.GetValue(2));
      return string.IsNullOrEmpty(stringExcelValue1) && !integerExcelValue.HasValue && string.IsNullOrEmpty(stringExcelValue2);
    }

    private static string GetStringExcelValue(object cellValue)
    {
      return cellValue != null ? Convert.ToString(cellValue) : (string) null;
    }

    private static int? GetIntegerExcelValue(object cellValue)
    {
      if (cellValue == null)
        return new int?();
      int result;
      return int.TryParse(Convert.ToString(cellValue), out result) ? new int?(result) : new int?();
    }
  }
}
