namespace BrotherBetsLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBettorPoints : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bettors", "Points", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Bettors", "Points");
        }
    }
}
