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
    private Lo30Repository _repo;
    private AccessDatabaseService _accessDbService;
    private Lo30DataSerializationService _lo30DataService;
    private string _redirectToPage;

    public AdminController(Lo30Repository repo)
    {
      _accessDbService = new AccessDatabaseService();
      _lo30DataService = new Lo30DataSerializationService();
      _repo = repo;
      _redirectToPage = "/Admin/DataProcessing";
    }
    public ActionResult GameResult()
    {
      return View();
    }
    public ActionResult GameRoster()
    {
      return View();
    }
    public ActionResult GameSheet()
    {
      return View();
    }
    public ActionResult Registration()
    {
      return View();
    }
    public ActionResult Draft()
    {
      return View();
    }
    public ActionResult Roster()
    {
      return View();
    }
    public ActionResult Schedule()
    {
      return View();
    }
  }
}
