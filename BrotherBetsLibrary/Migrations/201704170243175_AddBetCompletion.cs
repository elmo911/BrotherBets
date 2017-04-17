namespace BrotherBetsLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBetCompletion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bets", "Complete", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Bets", "Complete");
        }
    }
}
