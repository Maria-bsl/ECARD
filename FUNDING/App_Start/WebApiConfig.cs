// Decompiled with JetBrains decompiler
// Type: FUNDING.WebApiConfig
// Assembly: FUNDING, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5DE1C1E8-8D80-43BD-915D-F2502219BD58
// Assembly location: D:\ECardtest - 22082024\bin\FUNDING.dll

using System.Net.Http.Headers;
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

            // Remove default XML formatter (if only JSON is needed)
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            // Add additional formatters (if needed)
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/octet-stream"));
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));



        }
    }
}
