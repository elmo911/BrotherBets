namespace BrotherBetsLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBetOptionCorrect : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BetOptions", "Correct", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BetOptions", "Correct");
        }
    }
}
