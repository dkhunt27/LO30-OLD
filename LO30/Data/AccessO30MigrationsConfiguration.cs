using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;

namespace LO30.Data
{
    public class AccessO30MigrationsConfiguration
      : DbMigrationsConfiguration<AccessO30Context>
    {
        public AccessO30MigrationsConfiguration()
        {
            this.AutomaticMigrationDataLossAllowed = true;
            this.AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(AccessO30Context context)
        {
            base.Seed(context);
        }
    }
}
