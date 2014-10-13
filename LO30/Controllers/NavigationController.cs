using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LO30.Models;
using LO30.Services;
using System.Web.Security;

namespace LO30.Controllers
{
  public class NavigationController : Controller
  {
    public NavigationController()
    {
    }

    [ChildActionOnly]
    public ActionResult Menu()
    {

      try
      {
        if (Roles.IsUserInRole("admin"))
        {
          return PartialView("NavAdmin");
        }
        else if (Roles.IsUserInRole("board"))
        {
          return PartialView("NavBoard");
        }
      } 
      catch (Exception ex)
      {
        System.Diagnostics.Debug.Print("Could not determine the user role, defaulting to public.");
        ErrorHandlingService.PrintFullErrorMessage(ex);
      }

      return PartialView("NavPublic");
    }

  }
}
