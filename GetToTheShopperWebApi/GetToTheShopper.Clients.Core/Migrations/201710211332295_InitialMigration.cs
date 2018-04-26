namespace ShoppingList.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Double(nullable: false),
                        Unit_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Units", t => t.Unit_Id)
                .Index(t => t.Unit_Id);
            
            CreateTable(
                "dbo.ReceiptProducts",
                c => new
                    {
                        ReceiptId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Double(nullable: false),
                    })
                .PrimaryKey(t => new { t.ReceiptId, t.ProductId })
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Receipts", t => t.ReceiptId, cascadeDelete: true)
                .Index(t => t.ReceiptId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Receipts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        AuthorId = c.Int(nullable: false),
                        Done = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ShopProducts",
                c => new
                    {
                        ShopId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        AvailableUnits = c.Double(nullable: false),
                        Aisle = c.String(),
                    })
                .PrimaryKey(t => new { t.ShopId, t.ProductId })
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Shops", t => t.ShopId, cascadeDelete: true)
                .Index(t => t.ShopId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Shops",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Units",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Measure = c.String(),
                        Shortcut = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "Unit_Id", "dbo.Units");
            DropForeignKey("dbo.ShopProducts", "ShopId", "dbo.Shops");
            DropForeignKey("dbo.ShopProducts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ReceiptProducts", "ReceiptId", "dbo.Receipts");
            DropForeignKey("dbo.ReceiptProducts", "ProductId", "dbo.Products");
            DropIndex("dbo.ShopProducts", new[] { "ProductId" });
            DropIndex("dbo.ShopProducts", new[] { "ShopId" });
            DropIndex("dbo.ReceiptProducts", new[] { "ProductId" });
            DropIndex("dbo.ReceiptProducts", new[] { "ReceiptId" });
            DropIndex("dbo.Products", new[] { "Unit_Id" });
            DropTable("dbo.Units");
            DropTable("dbo.Shops");
            DropTable("dbo.ShopProducts");
            DropTable("dbo.Receipts");
            DropTable("dbo.ReceiptProducts");
            DropTable("dbo.Products");
        }
    }
}
