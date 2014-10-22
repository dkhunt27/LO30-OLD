using System.Web.Mvc;

namespace LO30.Areas.ScoreSheet
{
  public class ScoreSheetController : Controller
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
