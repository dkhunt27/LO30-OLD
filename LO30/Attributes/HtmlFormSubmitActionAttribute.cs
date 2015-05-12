using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace LO30.Attributes
{
  // http://jalukadev.blogspot.com/2014/10/multiple-submit-buttons-with-aspnet-mvc.html
  public class HtmlFormSubmitActionAttribute : ActionNameSelectorAttribute
  {
    public override bool IsValidName(ControllerContext controllerContext, string actionName, MethodInfo methodInfo)
    {
      if (actionName.Equals(methodInfo.Name, StringComparison.InvariantCultureIgnoreCase))
      {
        return true;
      }

      if (!actionName.Equals("DataProcessing", StringComparison.InvariantCultureIgnoreCase))
      {
        return false;
      }

      var request = controllerContext.RequestContext.HttpContext.Request;
      return request[methodInfo.Name] != null;
    }
  }
}