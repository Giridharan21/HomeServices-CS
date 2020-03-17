namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FinalPrice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "finalPrice", c => c.Decimal(nullable: false, precision: 7, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "finalPrice");
        }
    }
}
