namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SURIYA : DbMigration
    {
        public override void Up()
        {
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
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.FromFK, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.ToFK, cascadeDelete: true)
                .Index(t => t.FromFK)
                .Index(t => t.ToFK);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 16),
                        Password = c.String(nullable: false, maxLength: 16),
                        Type = c.String(nullable: false, maxLength: 16),
                        Contact = c.String(nullable: false, maxLength: 10),
                        Location = c.String(maxLength: 16),
                        Service = c.String(maxLength: 16),
                        BankFK = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BankAccountDetails", t => t.BankFK, cascadeDelete: true)
                .Index(t => t.Username, unique: true)
                .Index(t => t.Contact, unique: true)
                .Index(t => t.BankFK);
            
            CreateTable(
                "dbo.BankAccountDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BankName = c.String(),
                        Balance = c.Decimal(nullable: false, precision: 10, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
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
                        Comment = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.OrderIdFK, cascadeDelete: true)
                .Index(t => t.OrderIdFK);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reviews", "OrderIdFK", "dbo.Orders");
            DropForeignKey("dbo.Payments", "OrderIdFK", "dbo.Orders");
            DropForeignKey("dbo.Orders", "ToFK", "dbo.Users");
            DropForeignKey("dbo.Orders", "FromFK", "dbo.Users");
            DropForeignKey("dbo.Users", "BankFK", "dbo.BankAccountDetails");
            DropIndex("dbo.Reviews", new[] { "OrderIdFK" });
            DropIndex("dbo.Payments", new[] { "OrderIdFK" });
            DropIndex("dbo.Users", new[] { "BankFK" });
            DropIndex("dbo.Users", new[] { "Contact" });
            DropIndex("dbo.Users", new[] { "Username" });
            DropIndex("dbo.Orders", new[] { "ToFK" });
            DropIndex("dbo.Orders", new[] { "FromFK" });
            DropTable("dbo.Reviews");
            DropTable("dbo.Payments");
            DropTable("dbo.BankAccountDetails");
            DropTable("dbo.Users");
            DropTable("dbo.Orders");
        }
    }
}
