// Decompiled with JetBrains decompiler
// Type: FUNDING.Models.NoCache
// Assembly: FUNDING, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5DE1C1E8-8D80-43BD-915D-F2502219BD58
// Assembly location: D:\ECardtest - 22082024\bin\FUNDING.dll

using System;
using System.Web;
using System.Web.Mvc;

 
namespace FUNDING.Models
{
  public class NoCache : ActionFilterAttribute
  {
    public override void OnResultExecuting(ResultExecutingContext filterContext)
    {
      filterContext.HttpContext.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1.0));
      filterContext.HttpContext.Response.Cache.SetValidUntilExpires(false);
      filterContext.HttpContext.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
      filterContext.HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
      filterContext.HttpContext.Response.Cache.SetNoStore();
      base.OnResultExecuting(filterContext);
    }
  }
}
