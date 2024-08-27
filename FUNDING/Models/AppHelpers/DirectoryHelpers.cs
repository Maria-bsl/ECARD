// Decompiled with JetBrains decompiler
// Type: FUNDING.Models.AppHelpers.DirectoryHelpers
// Assembly: FUNDING, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5DE1C1E8-8D80-43BD-915D-F2502219BD58
// Assembly location: D:\ECardtest - 22082024\bin\FUNDING.dll

using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.Hosting;

 
namespace FUNDING.Models.AppHelpers
{
  public static class DirectoryHelpers
  {
    private static readonly string CardDesignFolder = "~/CardDesign_Dir/";

    public static string DefaultVirtualDirectory
    {
      get
      {
        string directoryPath = DirectoryHelpers.CardDesignFolder + "Default_Dir";
        DirectoryHelpers.MakeSureDirectoryExists(directoryPath);
        return directoryPath;
      }
    }

    public static string ExcelTemplateAbsoluteDirectory
    {
      get
      {
        string path = HostingEnvironment.MapPath(DirectoryHelpers.CardDesignFolder + "ExcelTemplate_Dir");
        if (!Directory.Exists(path))
          Directory.CreateDirectory(path);
        return path;
      }
    }

    public static string CardTemplatesVirtualDirectory
    {
      get
      {
        string directoryPath = DirectoryHelpers.CardDesignFolder + "CardTemplates_Dir";
        DirectoryHelpers.MakeSureDirectoryExists(directoryPath);
        return directoryPath;
      }
    }

    public static string GeneratedPDFVirtualDirectory
    {
      get
      {
        string directoryPath = DirectoryHelpers.CardDesignFolder + "PDFs_Dir";
        DirectoryHelpers.MakeSureDirectoryExists(directoryPath);
        return directoryPath;
      }
    }

    public static string Generated_Cards_DirVirtualDirectory
    {
      get
      {
        string directoryPath = DirectoryHelpers.CardDesignFolder + "Generated_Cards_Dir";
        DirectoryHelpers.MakeSureDirectoryExists(directoryPath);
        return directoryPath;
      }
    }

    public static string FontFamilyVirtualDirectory
    {
      get
      {
        string directoryPath = DirectoryHelpers.CardDesignFolder + "Fonts_Dir";
        DirectoryHelpers.MakeSureDirectoryExists(directoryPath);
        return directoryPath;
      }
    }

    public static string QrCodeVirtualDirectory
    {
      get => DirectoryHelpers.CardDesignFolder + "Qr_Code_Dir";
    }

    public static string RemoveTildeSymbolForJsVirtualPath(string virtualPath)
    {
      return virtualPath.Substring(1);
    }

    public static string MakeSureDirectoryExists(string directoryPath)
    {
      if (Regex.IsMatch(directoryPath, "~.*"))
        directoryPath = HostingEnvironment.MapPath(directoryPath);
      if (!Directory.Exists(directoryPath))
        Directory.CreateDirectory(directoryPath);
      return directoryPath;
    }

    public static string DeleteFileIfExists(string fullFilePath)
    {
      if (!File.Exists(fullFilePath))
        File.Delete(fullFilePath);
      return fullFilePath;
    }

    public static string GetDefaultCardTemplate()
    {
      return DirectoryHelpers.DefaultVirtualDirectory + "/Default_Template.png";
    }

    public static string GetDefaultQRCode()
    {
      return DirectoryHelpers.DefaultVirtualDirectory + "/Default_QR_Code.png";
    }

    public static string GetDefaultFont()
    {
      return DirectoryHelpers.DefaultVirtualDirectory + "/Default_Font_Lato-Regular.ttf";
    }

    public static string GetTimestampedFileName(string fileFullName)
    {
      string str = DateTime.Now.ToString("yyMMddHHmmss");
      string withoutExtension = Path.GetFileNameWithoutExtension(fileFullName);
      string extension = Path.GetExtension(fileFullName);
      return string.Format("{0}_{1}{2}", (object) withoutExtension, (object) str, (object) extension);
    }

    public static string GetTimestampedFile(string path)
    {
      string withoutExtension = Path.GetFileNameWithoutExtension(path);
      string extension = Path.GetExtension(path);
      string str = DateTime.Now.ToString("yyMMddHHmmss");
      return string.Format("{0}_{1}{2}", (object) withoutExtension, (object) str, (object) extension);
    }
  }
}
