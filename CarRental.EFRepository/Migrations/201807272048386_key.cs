namespace CarRental.EFRepository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class key : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Cars", "Category_CategoryId", "dbo.Categories");
            DropIndex("dbo.Cars", new[] { "Category_CategoryId" });
            RenameColumn(table: "dbo.Cars", name: "Category_CategoryId", newName: "CategoryId");
            AlterColumn("dbo.Cars", "CategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Cars", "CategoryId");
            AddForeignKey("dbo.Cars", "CategoryId", "dbo.Categories", "CategoryId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cars", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Cars", new[] { "CategoryId" });
            AlterColumn("dbo.Cars", "CategoryId", c => c.Int());
            RenameColumn(table: "dbo.Cars", name: "CategoryId", newName: "Category_CategoryId");
            CreateIndex("dbo.Cars", "Category_CategoryId");
            AddForeignKey("dbo.Cars", "Category_CategoryId", "dbo.Categories", "CategoryId");
        }
    }
}
