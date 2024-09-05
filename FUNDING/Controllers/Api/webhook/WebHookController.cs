// Decompiled with JetBrains decompiler
// Type: FUNDING.Controllers.Api.webhook.WebHookController
// Assembly: FUNDING, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5DE1C1E8-8D80-43BD-915D-F2502219BD58
// Assembly location: D:\ECardtest - 22082024\bin\FUNDING.dll

using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Web.Http;

 
namespace FUNDING.Controllers.Api.webhook
{
  public class WebHookController : ApiBaseController
  {
    [HttpGet]
    public IHttpActionResult HandleWebhook([FromBody] object payload)
    {
        
            return null;
    }

    private void LogWebhookPayload(object payload)
    {

    }
  }
}
