using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LO30.Models;
using LO30.Services;

namespace LO30.Controllers
{
    public class StandingsController : Controller
    {
        public StandingsController()
        {
        }
        public ActionResult RegularSeason()
        {
            return View();
        }
        public ActionResult Playoffs()
        {
            return View();
        }
    }
}
