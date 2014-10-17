using System.Web.Mvc;

namespace LO30.Areas.AdminDataProcessing
{
  public class AdminDataProcessingAreaRegistration : AreaRegistration
  {
    public override string AreaName
    {
      get
      {
        return "AdminDataProcessing";
      }
    }

    public override void RegisterArea(AreaRegistrationContext context)
    {
      context.MapRoute(
          "AdminDataProcessing_default",
          "AdminDataProcessing/{controller}/{action}/{id}",
          new { action = "Index", id = UrlParameter.Optional }
      );
    }
  }
}
