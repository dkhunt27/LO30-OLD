using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LO30.Data.Access
{
  public class AccessO30Context : DbContext
  {
      public AccessO30Context()
      : base("LO30AccessDB")
    {
      this.Configuration.LazyLoadingEnabled = false;
      this.Configuration.ProxyCreationEnabled = false;

      Database.SetInitializer(
        new MigrateDatabaseToLatestVersion<AccessO30Context, AccessO30MigrationsConfiguration>()
        );
    }

      public DbSet<AccessO30Season> Season { get; set; }
      public DbSet<AccessO30Standing> Standings { get; set; }

  }
}