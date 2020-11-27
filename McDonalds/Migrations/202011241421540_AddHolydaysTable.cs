namespace McDonalds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddHolydaysTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Holydays",
                c => new
                    {
                        HolydayId = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Label = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.HolydayId);
            
            AddColumn("dbo.Restaurants", "ServerIpAddress", c => c.String());
            AddColumn("dbo.ServerEvents", "Detail", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ServerEvents", "Detail");
            DropColumn("dbo.Restaurants", "ServerIpAddress");
            DropTable("dbo.Holydays");
        }
    }
}
