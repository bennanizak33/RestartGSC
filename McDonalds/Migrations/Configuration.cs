namespace McDonalds.Migrations
{
    using System;
	using System.Collections.Generic;
	using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
	using McDonalds.DAL;
    using McDonalds.Migrations.Seeds;

    internal sealed class Configuration : DbMigrationsConfiguration<McDonalds.DAL.McDonaldsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(McDonalds.DAL.McDonaldsContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            HolydaysSeed.Holydays(context);

            context.SaveChanges();
        }
    }
}
