namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCharge : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ServiceProviderDetails", "Charge", c => c.Decimal(nullable: true, precision: 7, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ServiceProviderDetails", "Charge");
        }
    }
}
