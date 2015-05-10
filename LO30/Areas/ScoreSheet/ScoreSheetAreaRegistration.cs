using System.Web.Mvc;

namespace LO30.Areas.ScoreSheet
{
  public class ScoreSheetAreaRegistration : AreaRegistration
  {
    public override string AreaName
    {
      get
      {
        return "ScoreSheet";
      }
    }

    public override void RegisterArea(AreaRegistrationContext context)
    {
      context.MapRoute(
          "ScoreSheet_default",
          "ScoreSheet/{controller}/{action}/{id}",
          new { action = "Index", id = UrlParameter.Optional }
      );
    }
  }
}
