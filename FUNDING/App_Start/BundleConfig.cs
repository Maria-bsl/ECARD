// Decompiled with JetBrains decompiler
// Type: FUNDING.BundleConfig
// Assembly: FUNDING, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5DE1C1E8-8D80-43BD-915D-F2502219BD58
// Assembly location: D:\ECardtest - 22082024\bin\FUNDING.dll

using System.Web.Optimization;

 
namespace FUNDING
{
  public class BundleConfig
  {
    public static void RegisterBundles(BundleCollection bundles)
    {
      bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Script/jquery-{version}.js"));
      bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include("~/Script/jquery.validate*"));
      bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Script/modernizr-*"));
      bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Script/bootstrap.js"));
      bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/bootstrap.css", "~/Content/site.css"));
    }
  }
}
