namespace FacebookClone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EnhanceDatabase : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "PostId", "dbo.Posts");
            DropForeignKey("dbo.Comments", "UserId", "dbo.Users");
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropIndex("dbo.Comments", new[] { "PostId" });
            RenameColumn(table: "dbo.Comments", name: "PostId", newName: "Post_Id");
            RenameColumn(table: "dbo.Comments", name: "UserId", newName: "User_Id");
            AlterColumn("dbo.Comments", "User_Id", c => c.Int());
            AlterColumn("dbo.Comments", "Post_Id", c => c.Int());
            AlterColumn("dbo.Users", "Country", c => c.String(nullable: false, maxLength: 60));
            AlterColumn("dbo.Users", "PhoneNumber", c => c.String(nullable: false, maxLength: 30));
            CreateIndex("dbo.Comments", "Post_Id");
            CreateIndex("dbo.Comments", "User_Id");
            AddForeignKey("dbo.Comments", "Post_Id", "dbo.Posts", "Id");
            AddForeignKey("dbo.Comments", "User_Id", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Comments", "Post_Id", "dbo.Posts");
            DropIndex("dbo.Comments", new[] { "User_Id" });
            DropIndex("dbo.Comments", new[] { "Post_Id" });
            AlterColumn("dbo.Users", "PhoneNumber", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Country", c => c.String(nullable: false));
            AlterColumn("dbo.Comments", "Post_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Comments", "User_Id", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Comments", name: "User_Id", newName: "UserId");
            RenameColumn(table: "dbo.Comments", name: "Post_Id", newName: "PostId");
            CreateIndex("dbo.Comments", "PostId");
            CreateIndex("dbo.Comments", "UserId");
            AddForeignKey("dbo.Comments", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Comments", "PostId", "dbo.Posts", "Id", cascadeDelete: true);
        }
    }
}
