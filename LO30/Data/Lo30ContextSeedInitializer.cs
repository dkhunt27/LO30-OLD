﻿using LO30.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Data.OleDb;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Transactions;

namespace LO30.Data
{
  public class LO30ContextSeedInitializer : DropCreateDatabaseAlways<Lo30Context>
  {

    protected override void Seed(Lo30Context context)
    {
      base.Seed(context);

      Lo30ContextService contextService = new Lo30ContextService(context);

      Lo30ContextSeed seeder = new Lo30ContextSeed(context, contextService);
      seeder.Seed();
    }
  }
}
