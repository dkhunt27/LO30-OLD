using System.Web.Mvc;

namespace LO30.Areas.AdminDataProcessing
{
    public class AdminDataProcessingController : Controller
    {
#if DEBUG
#else
      [Authorize(Roles = "admin")]
#endif
      public ActionResult Index()
      {
        return View();
      }
    }
}
