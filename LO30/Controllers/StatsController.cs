using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LO30.Models;
using LO30.Services;

namespace LO30.Controllers
{
    public class StatsController : Controller
    {
        public StatsController()
        {
        }
        public ActionResult LeagueLeaders()
        {
            return View();
        }
        public ActionResult Individual()
        {
            return View();
        }
        public ActionResult Team()
        {
            return View();
        }
        public ActionResult GameByGame()
        {
            return View();
        }
        public ActionResult Career()
        {
            return View();
        }
        public ActionResult Milestones()
        {
            return View();
        }
        public ActionResult Awards()
        {
            return View();
        }
    }
}
