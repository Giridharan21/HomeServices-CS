namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BankAccountDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BankName = c.String(),
                        AccountNumber = c.String(nullable: false, maxLength: 20),
                        Balance = c.Decimal(nullable: false, precision: 10, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.AccountNumber, unique: true);
            
            CreateTable(
                "dbo.CustomerDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Contact = c.String(maxLength: 10),
                        Location = c.String(maxLength: 16),
                        BankFK = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BankAccountDetails", t => t.BankFK, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.Contact, unique: true)
                .Index(t => t.BankFK, unique: true);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 16),
                        Password = c.String(nullable: false, maxLength: 16),
                        Type = c.String(nullable: false, maxLength: 16),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Username, unique: true);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FromFK = c.Int(nullable: false),
                        ToFK = c.Int(nullable: false),
                        Status = c.String(nullable: false, maxLength: 10),
                        Date = c.DateTime(nullable: false),
                        ScheduleDate = c.DateTime(nullable: false),
                        FinalPrice = c.Decimal(nullable: false, precision: 7, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.FromFK, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.ToFK, cascadeDelete: false)
                .Index(t => t.FromFK)
                .Index(t => t.ToFK);
            
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderIdFK = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 10, scale: 2),
                        Status = c.String(nullable: false, maxLength: 10),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.OrderIdFK, cascadeDelete: true)
                .Index(t => t.OrderIdFK);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderIdFK = c.Int(nullable: false),
                        Stars = c.Decimal(precision: 2, scale: 1),
                        Comment = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.OrderIdFK, cascadeDelete: true)
                .Index(t => t.OrderIdFK);
            
            CreateTable(
                "dbo.ServiceProviderDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Contact = c.String(nullable: false, maxLength: 10),
                        BankFK = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BankAccountDetails", t => t.BankFK, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.Contact, unique: true)
                .Index(t => t.BankFK, unique: true);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Service = c.String(nullable: false, maxLength: 16),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Service, unique: true);
            
            CreateTable(
                "dbo.ServicesAssigneds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ServiceProviderFK = c.Int(nullable: false),
                        ServicesFK = c.Int(nullable: false),
                        Charge = c.Decimal(nullable: false, precision: 7, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Services", t => t.ServicesFK, cascadeDelete: true)
                .ForeignKey("dbo.ServiceProviderDetails", t => t.ServiceProviderFK, cascadeDelete: true)
                .Index(t => t.ServiceProviderFK)
                .Index(t => t.ServicesFK);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ServicesAssigneds", "ServiceProviderFK", "dbo.ServiceProviderDetails");
            DropForeignKey("dbo.ServicesAssigneds", "ServicesFK", "dbo.Services");
            DropForeignKey("dbo.ServiceProviderDetails", "UserId", "dbo.Users");
            DropForeignKey("dbo.ServiceProviderDetails", "BankFK", "dbo.BankAccountDetails");
            DropForeignKey("dbo.Reviews", "OrderIdFK", "dbo.Orders");
            DropForeignKey("dbo.Payments", "OrderIdFK", "dbo.Orders");
            DropForeignKey("dbo.Orders", "ToFK", "dbo.Users");
            DropForeignKey("dbo.Orders", "FromFK", "dbo.Users");
            DropForeignKey("dbo.CustomerDetails", "UserId", "dbo.Users");
            DropForeignKey("dbo.CustomerDetails", "BankFK", "dbo.BankAccountDetails");
            DropIndex("dbo.ServicesAssigneds", new[] { "ServicesFK" });
            DropIndex("dbo.ServicesAssigneds", new[] { "ServiceProviderFK" });
            DropIndex("dbo.Services", new[] { "Service" });
            DropIndex("dbo.ServiceProviderDetails", new[] { "BankFK" });
            DropIndex("dbo.ServiceProviderDetails", new[] { "Contact" });
            DropIndex("dbo.ServiceProviderDetails", new[] { "UserId" });
            DropIndex("dbo.Reviews", new[] { "OrderIdFK" });
            DropIndex("dbo.Payments", new[] { "OrderIdFK" });
            DropIndex("dbo.Orders", new[] { "ToFK" });
            DropIndex("dbo.Orders", new[] { "FromFK" });
            DropIndex("dbo.Users", new[] { "Username" });
            DropIndex("dbo.CustomerDetails", new[] { "BankFK" });
            DropIndex("dbo.CustomerDetails", new[] { "Contact" });
            DropIndex("dbo.CustomerDetails", new[] { "UserId" });
            DropIndex("dbo.BankAccountDetails", new[] { "AccountNumber" });
            DropTable("dbo.ServicesAssigneds");
            DropTable("dbo.Services");
            DropTable("dbo.ServiceProviderDetails");
            DropTable("dbo.Reviews");
            DropTable("dbo.Payments");
            DropTable("dbo.Orders");
            DropTable("dbo.Users");
            DropTable("dbo.CustomerDetails");
            DropTable("dbo.BankAccountDetails");
        }
    }
}
