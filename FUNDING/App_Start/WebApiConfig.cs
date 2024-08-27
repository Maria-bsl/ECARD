// Decompiled with JetBrains decompiler
// Type: FUNDING.WebApiConfig
// Assembly: FUNDING, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5DE1C1E8-8D80-43BD-915D-F2502219BD58
// Assembly location: D:\ECardtest - 22082024\bin\FUNDING.dll

using System.Web.Http;

 
namespace FUNDING
{
  public static class WebApiConfig
  {
    public static void Register(HttpConfiguration config)
    {
      config.EnableCors();
      config.MapHttpAttributeRoutes();
      config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}",  new
      {
        id = RouteParameter.Optional
      });
      config.InitializeCustomWebHooks();
      //config.InitializeCustomWebHooksApis();
      //config.InitializeReceiveCustomWebHooks();
    }
  }
}
