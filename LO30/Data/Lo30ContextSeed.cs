using LO30.Data.Objects;
using LO30.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace LO30.Data
{
  public class Lo30ContextSeed
  {
    private Lo30ContextService _lo30ContextService;
    private AccessDatabaseService _accessDatabaseService;
    private Lo30DataService _lo30DataService;
    private Lo30ContextService contextService;
    private Lo30Context context;

    public Lo30ContextSeed(Lo30Context context2, Lo30ContextService contextService2)
    {
      context = context2;
      contextService = contextService2;
    }

    public void Seed()
    {
    }
  }
}
