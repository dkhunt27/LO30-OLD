using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LO30.Models;
using LO30.Services;

namespace LO30.Controllers
{
  public class HomeController : Controller
  {
    private IMailService _mailService;

    public HomeController(IMailService mailService)
    {
      _mailService = mailService;
    }

    public ActionResult Index()
    {
      ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

      return View();
    }

    public ActionResult IndexNg()
    {
      ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

      return View();
    }
  }
}
