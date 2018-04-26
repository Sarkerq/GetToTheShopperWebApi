namespace ShoppingList.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPKForAllTables : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.ReceiptProducts");
            DropPrimaryKey("dbo.ShopProducts");
            AddColumn("dbo.ReceiptProducts", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.ShopProducts", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.ReceiptProducts", "Id");
            AddPrimaryKey("dbo.ShopProducts", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.ShopProducts");
            DropPrimaryKey("dbo.ReceiptProducts");
            DropColumn("dbo.ShopProducts", "Id");
            DropColumn("dbo.ReceiptProducts", "Id");
            AddPrimaryKey("dbo.ShopProducts", new[] { "ShopId", "ProductId" });
            AddPrimaryKey("dbo.ReceiptProducts", new[] { "ReceiptId", "ProductId" });
        }
    }
}
