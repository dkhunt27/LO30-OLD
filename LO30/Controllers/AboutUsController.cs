using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LO30.Models;
using LO30.Services;

namespace LO30.Controllers
{
    public class AboutUsController : Controller
    {
        private IMailService _mailService;

        public AboutUsController(IMailService mailService)
        {
            _mailService = mailService;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Board()
        {
            return View();
        }
        public ActionResult ContactUs()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ContactUs(ContactModel contact)
        {
            var msg = string.Format("Comment From: {1}{0} Email: {2}{0} Subject: {3}{0} Message:{4}{0}",
                Environment.NewLine,
                contact.Name,
                contact.Email,
                contact.Subject,
                contact.Message);

            if (_mailService.SendMail("noreply@livoniaover30hockey.com", "information@livoniaover30hockey.com", "Website Contact Us", msg))
            {
                @ViewBag.MailSent = true;
            }
            return View();
        }
        public ActionResult Invitees()
        {
            return View();
        }
        public ActionResult Members()
        {
            return View();
        }
        public ActionResult Services()
        {
            return View();
        }
        public ActionResult Rules()
        {
            return View();
        }
        public ActionResult Sponsors()
        {
            return View();
        }
        public ActionResult WaitList()
        {
            return View();
        }
    }
}
