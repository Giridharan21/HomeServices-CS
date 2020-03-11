namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BankAccountDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BankName = c.String(),
                        Balance = c.Decimal(nullable: false, precision: 10, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Users", "BankFK", c => c.Int(nullable: false));
            CreateIndex("dbo.Users", "BankFK");
            AddForeignKey("dbo.Users", "BankFK", "dbo.BankAccountDetails", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "BankFK", "dbo.BankAccountDetails");
            DropIndex("dbo.Users", new[] { "BankFK" });
            DropColumn("dbo.Users", "BankFK");
            DropTable("dbo.BankAccountDetails");
        }
    }
}
