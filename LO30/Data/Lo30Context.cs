using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LO30.Data
{
  public class Lo30Context : DbContext
  {
      public Lo30Context()
      : base("DefaultConnection")
    {
      this.Configuration.LazyLoadingEnabled = false;
      this.Configuration.ProxyCreationEnabled = false;

      Database.SetInitializer(
        new MigrateDatabaseToLatestVersion<Lo30Context, Lo30MigrationsConfiguration>()
        );
    }

      public DbSet<Article> Articles { get; set; }

  }
}