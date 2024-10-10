namespace ClothesManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Validation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Products", "ProductPic", c => c.String(maxLength: 255));
            AlterColumn("dbo.Products", "Description", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("dbo.Products", "Material", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Products", "Brand", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Categories", "Type", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Categories", "Type", c => c.String());
            AlterColumn("dbo.Products", "Brand", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "Material", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "ProductPic", c => c.String());
            AlterColumn("dbo.Products", "Name", c => c.String(nullable: false));
        }
    }
}
