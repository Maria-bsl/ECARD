// Decompiled with JetBrains decompiler
// Type: FUNDING.Models.AppHelpers.ExcelTemplateGenerationHandler
// Assembly: FUNDING, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5DE1C1E8-8D80-43BD-915D-F2502219BD58
// Assembly location: D:\ECardtest - 22082024\bin\FUNDING.dll

using DocumentFormat.OpenXml.Spreadsheet;
using SpreadsheetLight;
using System.IO;

 
namespace FUNDING.Models.AppHelpers
{
  public class ExcelTemplateGenerationHandler
  {
    public static string ExcelGenerationProcess(string excelFileNameWithoutExtension)
    {
      string str = ExcelTemplateGenerationHandler.ManageFileNames(excelFileNameWithoutExtension);
      if (File.Exists(str))
        return str;
      using (SLDocument spreadSheetDocument = new SLDocument())
      {
        ExcelTemplateGenerationHandler.GenerateTableHeaders(spreadSheetDocument);
        ExcelTemplateGenerationHandler.RenameCurrentWorkSheet(spreadSheetDocument, "B-ENVIT Invitees List");
        ExcelTemplateGenerationHandler.SetAllColumnWidth(spreadSheetDocument);
        ExcelTemplateGenerationHandler.SetPivotTableStyles(spreadSheetDocument);
        ExcelTemplateGenerationHandler.SetAllColumnsValidations(spreadSheetDocument);
        ExcelTemplateGenerationHandler.SetWholeWorksheetStyles(spreadSheetDocument);
        spreadSheetDocument.SaveAs(str);
        return str;
      }
    }

    private static void GenerateTableHeaders(SLDocument spreadSheetDocument)
    {
      spreadSheetDocument.SetCellValue("A1", "Full Name");
      spreadSheetDocument.SetCellValue("B1", "Card Size");
      spreadSheetDocument.SetCellValue("C1", "Mobile Number");
      spreadSheetDocument.SetCellValue("D1", "Email Address");
      spreadSheetDocument.SetCellValue("E1", "Table Number");
    }

    private static void SetPivotTableStyles(SLDocument spreadSheetDocument)
    {
      SLTable table = spreadSheetDocument.CreateTable("A1", "E2");
      table.SetTableStyle(SLTableStyleTypeValues.Light15);
      spreadSheetDocument.InsertTable(table);
      table.DisplayName = "BENVITInviteesTable";
      table.HasBandedColumns = true;
      table.HasBandedRows = true;
      table.HasFirstColumnStyled = true;
      table.HasLastColumnStyled = true;
      table.Sort(1, false);
    }

    private static void SetAllColumnWidth(SLDocument spreadSheetDocument)
    {
      spreadSheetDocument.SetColumnWidth(1, 36.0);
      spreadSheetDocument.SetColumnWidth(2, 16.0);
      spreadSheetDocument.SetColumnWidth(3, 24.0);
      spreadSheetDocument.SetColumnWidth(4, 48.0);
      spreadSheetDocument.SetColumnWidth(5, 24.0);
    }

    private static void RenameCurrentWorkSheet(SLDocument spreadSheetDocument, string wookSheetName)
    {
      spreadSheetDocument.RenameWorksheet(spreadSheetDocument.GetCurrentWorksheetName(), wookSheetName);
    }

    private static void SetAllColumnsValidations(SLDocument spreadSheetDocument)
    {
      ExcelTemplateGenerationHandler.FullNameColumnValidations(spreadSheetDocument, 1000);
      ExcelTemplateGenerationHandler.CardSizeColumnValidations(spreadSheetDocument, 1000);
      ExcelTemplateGenerationHandler.MobileNumberColumnValidations(spreadSheetDocument, 1000);
    }

    private static void FullNameColumnValidations(SLDocument spreadSheetDocument, int lastRowIndex)
    {
      SLDataValidation dataValidation = spreadSheetDocument.CreateDataValidation("A2", string.Format("A{0}", (object) lastRowIndex));
      dataValidation.AllowCustom(string.Format("AND(NOT(ISBLANK(A2:A{0})),ISTEXT(A2:A{1}))", (object) lastRowIndex, (object) lastRowIndex), true);
      dataValidation.SetErrorAlert(DataValidationErrorStyleValues.Warning, "Validation Alert", "Please enter a valid invitee name.");
      dataValidation.SetInputMessage("Validation Alert", "Only alphabets, space, full stop and appostrophe are allowed\n\nThe valid name must have atleast 3 characters.");
      spreadSheetDocument.AddDataValidation(dataValidation);
    }

    private static void CardSizeColumnValidations(SLDocument spreadSheetDocument, int lastRowIndex)
    {
      SLDataValidation dataValidation = spreadSheetDocument.CreateDataValidation("B2", string.Format("B{0}", (object) lastRowIndex));
      dataValidation.AllowWholeNumber(SLDataValidationSingleOperandValues.GreaterThan, 0, false);
      dataValidation.SetErrorAlert(DataValidationErrorStyleValues.Warning, "Card Size validation alert!", "Please enter a valid card size.");
      dataValidation.SetInputMessage("Validation Alert", "Card size should be a digit of value starting form 1 to above.");
      spreadSheetDocument.AddDataValidation(dataValidation);
    }

    private static void MobileNumberColumnValidations(
      SLDocument spreadSheetDocument,
      int lastRowIndex)
    {
      SLDataValidation dataValidation = spreadSheetDocument.CreateDataValidation("C2", string.Format("C{0}", (object) lastRowIndex));
      dataValidation.AllowCustom(string.Format("AND(LEN(C2:C{0})>9,LEN(C2:C{1})<18,ISNUMBER(C2:C{2}),NOT(EXACT(LEFT(C2:C{3},1),0)))", (object) lastRowIndex, (object) lastRowIndex, (object) lastRowIndex, (object) lastRowIndex), true);
      dataValidation.SetErrorAlert(DataValidationErrorStyleValues.Warning, "Mobile Number Validation Alert", "Please enter a valid mobile number.");
      dataValidation.SetInputMessage("Validation Alert", "Mobile number with their specific country codes are allowed here, example 2557*********");
      spreadSheetDocument.AddDataValidation(dataValidation);
    }

    private static void SetWholeWorksheetStyles(SLDocument spreadSheetDocument, int lastRowIndex = 1000)
    {
      SLStyle style = spreadSheetDocument.CreateStyle();
      style.Font.FontSize = new double?(12.0);
      for (int index = 1; index < lastRowIndex; ++index)
      {
        spreadSheetDocument.SetCellStyle(string.Format("A{0}", (object) index), style);
        spreadSheetDocument.SetCellStyle(string.Format("B{0}", (object) index), style);
        spreadSheetDocument.SetCellStyle(string.Format("D{0}", (object) index), style);
        spreadSheetDocument.SetCellStyle(string.Format("E{0}", (object) index), style);
        style.FormatCode = "###0";
        spreadSheetDocument.SetCellStyle(string.Format("C{0}", (object) index), style);
      }
    }

    private static string ManageFileNames(string fileNameWithoutExtension = "Default")
    {
      return Path.Combine(DirectoryHelpers.ExcelTemplateAbsoluteDirectory, string.Format("{0}.xlsx", (object) fileNameWithoutExtension));
    }
  }
}
