namespace McDonalds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Restaurants",
                c => new
                    {
                        RestaurantId = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        ShortName = c.String(),
                        ServerName = c.String(),
                        Email = c.String(),
                        City = c.String(),
                        Address_1 = c.String(),
                        Address_2 = c.String(),
                        Address_3 = c.String(),
                        Address_4 = c.String(),
                        ZipCode = c.Int(nullable: false),
                        PhoneNumber = c.String(),
                        FaxNumber = c.String(),
                        OpeningDate = c.DateTime(nullable: false),
                        PermanentClosureDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.RestaurantId);
            
            CreateTable(
                "dbo.ServerEvents",
                c => new
                    {
                        ServerEventId = c.Int(nullable: false, identity: true),
                        Event = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Restaurant_RestaurantId = c.Int(),
                    })
                .PrimaryKey(t => t.ServerEventId)
                .ForeignKey("dbo.Restaurants", t => t.Restaurant_RestaurantId)
                .Index(t => t.Restaurant_RestaurantId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ServerEvents", "Restaurant_RestaurantId", "dbo.Restaurants");
            DropIndex("dbo.ServerEvents", new[] { "Restaurant_RestaurantId" });
            DropTable("dbo.ServerEvents");
            DropTable("dbo.Restaurants");
        }
    }
}
