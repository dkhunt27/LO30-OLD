using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Restier.WebApi;
using LO30.Data.Models;
using LO30.Data.Contexts;

namespace LO30.Web.Api.Controllers
{
  public class LO30Controller : ODataDomainController<LO30Domain>
  {
    private LO30Context DbContext
    {
      get { return Domain.Context; }
    }
  }
}