namespace McDonalds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullableDateTime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ServerEvents", "Date", c => c.DateTime());
            AlterColumn("dbo.ServerEvents", "UpTimes", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ServerEvents", "UpTimes", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ServerEvents", "Date", c => c.DateTime(nullable: false));
        }
    }
}
