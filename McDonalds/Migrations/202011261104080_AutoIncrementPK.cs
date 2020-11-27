namespace McDonalds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AutoIncrementPK : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ServerEvents", "UpTimes", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ServerEvents", "UpTimes");
        }
    }
}
