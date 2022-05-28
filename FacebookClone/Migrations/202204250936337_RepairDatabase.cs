namespace FacebookClone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RepairDatabase : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.NotAllowedUsers", "PostId", "dbo.Posts");
            DropForeignKey("dbo.NotAllowedUsers", "UserId", "dbo.Users");
            DropForeignKey("dbo.Comments", "Post_Id", "dbo.Posts");
            DropForeignKey("dbo.Comments", "User_Id", "dbo.Users");
            DropIndex("dbo.Comments", new[] { "Post_Id" });
            DropIndex("dbo.Comments", new[] { "User_Id" });
            DropIndex("dbo.NotAllowedUsers", new[] { "PostId" });
            DropIndex("dbo.NotAllowedUsers", new[] { "UserId" });
            DropIndex("dbo.Reacts", new[] { "UserId" });
            RenameColumn(table: "dbo.Comments", name: "Post_Id", newName: "PostId");
            RenameColumn(table: "dbo.Comments", name: "User_Id", newName: "UserId");
            DropPrimaryKey("dbo.Comments");
            CreateTable(
                "dbo.RestrictedUsers",
                c => new
                    {
                        PostID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PostID, t.UserID })
                .ForeignKey("dbo.Posts", t => t.PostID, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: false)
                .Index(t => t.PostID)
                .Index(t => t.UserID);

            DropColumn("dbo.Comments", "ID");
            DropColumn("dbo.Comments", "TextContent");
            DropColumn("dbo.Posts", "TextContent");
            DropColumn("dbo.Users", "MobileNumber");
            DropTable("dbo.NotAllowedUsers");


            AddColumn("dbo.Comments", "CommentID", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Comments", "CommentContent", c => c.String());
            AddColumn("dbo.Posts", "PostContent", c => c.String(nullable: false));
            AddColumn("dbo.Users", "PhoneNumber", c => c.String(nullable: false));
            AlterColumn("dbo.Comments", "PostId", c => c.Int(nullable: false));
            AlterColumn("dbo.Comments", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Users", "City", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Country", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Password", c => c.String(nullable: false));
            AddPrimaryKey("dbo.Comments", "CommentID");
            CreateIndex("dbo.Comments", "UserId");
            CreateIndex("dbo.Comments", "PostId");
            CreateIndex("dbo.Reacts", "UserID");
            AddForeignKey("dbo.Comments", "PostId", "dbo.Posts", "Id", cascadeDelete: false);
            AddForeignKey("dbo.Comments", "UserId", "dbo.Users", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.NotAllowedUsers",
                c => new
                    {
                        PostId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PostId, t.UserId });
            
            AddColumn("dbo.Users", "MobileNumber", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Posts", "TextContent", c => c.String(nullable: false, maxLength: 150));
            AddColumn("dbo.Comments", "TextContent", c => c.String(nullable: false, maxLength: 200));
            AddColumn("dbo.Comments", "ID", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.Comments", "UserId", "dbo.Users");
            DropForeignKey("dbo.Comments", "PostId", "dbo.Posts");
            DropForeignKey("dbo.RestrictedUsers", "UserID", "dbo.Users");
            DropForeignKey("dbo.RestrictedUsers", "PostID", "dbo.Posts");
            DropIndex("dbo.RestrictedUsers", new[] { "UserID" });
            DropIndex("dbo.RestrictedUsers", new[] { "PostID" });
            DropIndex("dbo.Reacts", new[] { "UserID" });
            DropIndex("dbo.Comments", new[] { "PostId" });
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropPrimaryKey("dbo.Comments");
            AlterColumn("dbo.Users", "Password", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Users", "Country", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Users", "City", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Comments", "UserId", c => c.Int());
            AlterColumn("dbo.Comments", "PostId", c => c.Int());
            DropColumn("dbo.Users", "PhoneNumber");
            DropColumn("dbo.Posts", "PostContent");
            DropColumn("dbo.Comments", "CommentContent");
            DropColumn("dbo.Comments", "CommentID");
            DropTable("dbo.RestrictedUsers");
            AddPrimaryKey("dbo.Comments", "ID");
            RenameColumn(table: "dbo.Comments", name: "UserId", newName: "User_Id");
            RenameColumn(table: "dbo.Comments", name: "PostId", newName: "Post_Id");
            CreateIndex("dbo.Reacts", "UserId");
            CreateIndex("dbo.NotAllowedUsers", "UserId");
            CreateIndex("dbo.NotAllowedUsers", "PostId");
            CreateIndex("dbo.Comments", "User_Id");
            CreateIndex("dbo.Comments", "Post_Id");
            AddForeignKey("dbo.Comments", "User_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.Comments", "Post_Id", "dbo.Posts", "Id");
            AddForeignKey("dbo.NotAllowedUsers", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.NotAllowedUsers", "PostId", "dbo.Posts", "Id", cascadeDelete: true);
        }
    }
}
