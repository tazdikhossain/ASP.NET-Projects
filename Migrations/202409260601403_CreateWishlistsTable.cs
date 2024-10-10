namespace ClothesManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateWishlistsTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Wishlists", "User_Email");
            DropColumn("dbo.Wishlists", "User_Password");
            DropColumn("dbo.Wishlists", "User_ConfirmPassword");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Wishlists", "User_ConfirmPassword", c => c.String());
            AddColumn("dbo.Wishlists", "User_Password", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.Wishlists", "User_Email", c => c.String(nullable: false));
        }
    }
}
