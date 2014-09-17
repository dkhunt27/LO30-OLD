using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;

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

#if DEBUG
            if (context.Articles.Count() == 0)
            {
              var article = new Article()
                {
                    Title = "Payment Schedule",
                    Created = new DateTime(2014, 09, 10, 20, 5, 32),
                    Body = "Payments are due the Third Thursday of every month as follows:",
                };

                context.Articles.Add(article);

                article = new Article()
                {
                    Title = "Game Schedule",
                    Created = new DateTime(2014, 09, 8, 15, 12, 41),
                    Body = "The schedule has been updated and posted"
                };

                context.Articles.Add(article);

                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                }
            }
#endif
        }
    }
}
