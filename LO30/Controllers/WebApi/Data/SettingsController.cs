using LO30.Data;
using LO30.Data.Objects;
using System.Collections.Generic;
using System.Web.Http;

namespace LO30.Controllers.Data
{
  public class SettingsController : ApiController
  {
    private ILo30Repository _repo;

    public SettingsController(ILo30Repository repo, Lo30Context context)
    {
      _repo = repo;
    }

    public List<Setting> GetSettings()
    {
      return _repo.GetSettings();
    }

  }
}
