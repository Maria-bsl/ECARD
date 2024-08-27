// Decompiled with JetBrains decompiler
// Type: FUNDING.Models.FileManagement
// Assembly: FUNDING, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5DE1C1E8-8D80-43BD-915D-F2502219BD58
// Assembly location: D:\ECardtest - 22082024\bin\FUNDING.dll

using System.IO;

 
namespace FUNDING.Models
{
  public class FileManagement
  {
    public static void FileCheck(string fileName)
    {
      string path = "E:/QR-code-images/" + fileName + ".png";
      if (!File.Exists(path))
        return;
      File.Delete(path);
    }
  }
}
