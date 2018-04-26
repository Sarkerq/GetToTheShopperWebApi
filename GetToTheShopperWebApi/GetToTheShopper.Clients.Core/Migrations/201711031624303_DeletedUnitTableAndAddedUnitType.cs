namespace ShoppingList.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeletedUnitTableAndAddedUnitType : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "Unit_Id", "dbo.Units");
            DropIndex("dbo.Products", new[] { "Unit_Id" });
            AddColumn("dbo.Products", "Unit_UnitType", c => c.Int(nullable: false));
            DropColumn("dbo.Products", "Unit_Id");
            DropTable("dbo.Units");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Units",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Measure = c.String(),
                        Shortcut = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Products", "Unit_Id", c => c.Int());
            DropColumn("dbo.Products", "Unit_UnitType");
            CreateIndex("dbo.Products", "Unit_Id");
            AddForeignKey("dbo.Products", "Unit_Id", "dbo.Units", "Id");
        }
    }
}
