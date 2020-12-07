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
            HolydaysSeed.Holydays(context);

            RestaurantsSeed.Restaurants(context);

            context.SaveChanges();
        }
    }
}
