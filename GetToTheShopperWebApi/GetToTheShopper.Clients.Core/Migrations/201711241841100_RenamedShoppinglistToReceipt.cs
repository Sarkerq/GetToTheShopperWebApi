namespace ShoppingList.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenamedShoppinglistToReceipt : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ShoppinglistProducts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ShoppinglistProducts", "ShoppinglistId", "dbo.Shoppinglists");
            DropIndex("dbo.ShoppinglistProducts", new[] { "ShoppinglistId" });
            DropIndex("dbo.ShoppinglistProducts", new[] { "ProductId" });
            CreateTable(
                "dbo.ReceiptProducts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReceiptId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
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
            
            DropTable("dbo.ShoppinglistProducts");
            DropTable("dbo.Shoppinglists");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Shoppinglists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        AuthorId = c.Int(nullable: false),
                        Done = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ShoppinglistProducts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ShoppinglistId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.ReceiptProducts", "ReceiptId", "dbo.Receipts");
            DropForeignKey("dbo.ReceiptProducts", "ProductId", "dbo.Products");
            DropIndex("dbo.ReceiptProducts", new[] { "ProductId" });
            DropIndex("dbo.ReceiptProducts", new[] { "ReceiptId" });
            DropTable("dbo.Receipts");
            DropTable("dbo.ReceiptProducts");
            CreateIndex("dbo.ShoppinglistProducts", "ProductId");
            CreateIndex("dbo.ShoppinglistProducts", "ShoppinglistId");
            AddForeignKey("dbo.ShoppinglistProducts", "ShoppinglistId", "dbo.Shoppinglists", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ShoppinglistProducts", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
        }
    }
}
