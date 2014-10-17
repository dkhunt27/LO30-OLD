using LO30.Attributes;
using LO30.Data;
using LO30.Models;
using LO30.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LO30.Controllers
{
  public partial class AdminController : Controller
  {
    [Authorize(Roles = "admin")]
    public ActionResult DataProcessing()
    {
      return View();
    }

  }
}
