using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LO30.Data.Access
{
  public class AccessO30Repository : IAccessO30Repository
  {
      AccessO30Context _ctx;
    public AccessO30Repository(AccessO30Context ctx)
    {
      _ctx = ctx;
    }

    public IQueryable<AccessO30Standing> GetStandings()
    {
        return _ctx.Standings;
    }
  }
}