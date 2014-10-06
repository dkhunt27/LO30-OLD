using LO30.Data.Access;
using LO30.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Data.OleDb;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Transactions;

namespace LO30.Data
{
  public class Lo30MigrationsConfiguration
    : DbMigrationsConfiguration<Lo30Context>
  {


    public Lo30MigrationsConfiguration()
    {
      this.AutomaticMigrationDataLossAllowed = true;
      this.AutomaticMigrationsEnabled = true;
    }

    protected override void Seed(Lo30Context context)
    {
      base.Seed(context);
      Lo30ContextSeed seeder = new Lo30ContextSeed();
      seeder.Seed(context);
    }
  }
}
