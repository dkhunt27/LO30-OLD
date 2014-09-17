using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LO30.Models;
using LO30.Services;

namespace LO30.Controllers
{
    public class ScheduleController : Controller
    {
        public ScheduleController()
        {
        }
        public ActionResult Today()
        {
            return View();
        }
        public ActionResult Week()
        {
            return View();
        }
        public ActionResult Month()
        {
            return View();
        }
        public ActionResult Season()
        {
            return View();
        }
    }
}
