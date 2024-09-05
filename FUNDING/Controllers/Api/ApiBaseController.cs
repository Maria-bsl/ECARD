// Decompiled with JetBrains decompiler
// Type: FUNDING.Controllers.Api.ApiBaseController
// Assembly: FUNDING, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5DE1C1E8-8D80-43BD-915D-F2502219BD58
// Assembly location: D:\ECardtest - 22082024\bin\FUNDING.dll

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.ModelBinding;

 
namespace FUNDING.Controllers.Api
{
  [EnableCors("*", "*", "*")]
  public class ApiBaseController : ApiController
  {
    protected List<string> GetModelStateErrorList()
    {
      List<string> modelStateErrorList = new List<string>();
      foreach (KeyValuePair<string, System.Web.Http.ModelBinding.ModelState> keyValuePair in this.ModelState)
      {
        foreach (ModelError error in (Collection<ModelError>) keyValuePair.Value.Errors)
          modelStateErrorList.Add(error.ErrorMessage);
      }
      return modelStateErrorList;
    }
  }
}
