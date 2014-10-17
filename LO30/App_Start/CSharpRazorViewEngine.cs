using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LO30
{
  public class CSharpRazorViewEngine : RazorViewEngine
  {
    public CSharpRazorViewEngine()
    {
      AreaViewLocationFormats = new[]
      {
        "~/Areas/{2}/{1}.{0}.cshtml",
        "~/Areas/{2}/{0}.cshtml",
        "~/Areas/{2}/Shared/{1}.{0}.cshtml",
        "~/Areas/{2}/Shared/{0}.cshtml"
      };

      AreaMasterLocationFormats = new[]
      {
        "~/Areas/{2}/{1}.{0}.cshtml",
        "~/Areas/{2}/{0}.cshtml",
        "~/Areas/{2}/Shared/{1}.{0}.cshtml",
        "~/Areas/{2}/Shared/{0}.cshtml"
      };

      AreaPartialViewLocationFormats = new[]
      {
        "~/Areas/{2}/{1}.{0}.cshtml",
        "~/Areas/{2}/{0}.cshtml",
        "~/Areas/{2}/Shared/{1}.{0}.cshtml",
        "~/Areas/{2}/Shared/{0}.cshtml"
      };

      ViewLocationFormats = new[]
      {
        "~/Views/{1}/{0}.cshtml",
        "~/Views/Shared/{0}.cshtml"
      };

      MasterLocationFormats = new[]
      {
        "~/Views/{1}/{0}.cshtml",
        "~/Views/Shared/{0}.cshtml"
      };

      PartialViewLocationFormats = new[]
      {
        "~/Views/{1}/{0}.cshtml",
        "~/Views/Shared/{0}.cshtml"
      };
    }
  }
}