using LO30.Models;
using LO30.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LO30.Controllers
{
    public class AdminController : Controller
    {
        public AdminController()
        {
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
