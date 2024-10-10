namespace ClothesManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateProductTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Description", c => c.String(nullable: false));
            AddColumn("dbo.Products", "Material", c => c.String(nullable: false));
            AddColumn("dbo.Products", "Brand", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Brand");
            DropColumn("dbo.Products", "Material");
            DropColumn("dbo.Products", "Description");
        }
    }
}
