namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BankAccountDetails", "AccountNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BankAccountDetails", "AccountNumber");
        }
    }
}
