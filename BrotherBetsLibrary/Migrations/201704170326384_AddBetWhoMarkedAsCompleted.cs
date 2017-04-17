namespace BrotherBetsLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBetWhoMarkedAsCompleted : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Bets", name: "Bettor_Id", newName: "Creator_Id");
            RenameIndex(table: "dbo.Bets", name: "IX_Bettor_Id", newName: "IX_Creator_Id");
            AddColumn("dbo.Bets", "MarkedCompleteBy_Id", c => c.Int());
            CreateIndex("dbo.Bets", "MarkedCompleteBy_Id");
            AddForeignKey("dbo.Bets", "MarkedCompleteBy_Id", "dbo.Bettors", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bets", "MarkedCompleteBy_Id", "dbo.Bettors");
            DropIndex("dbo.Bets", new[] { "MarkedCompleteBy_Id" });
            DropColumn("dbo.Bets", "MarkedCompleteBy_Id");
            RenameIndex(table: "dbo.Bets", name: "IX_Creator_Id", newName: "IX_Bettor_Id");
            RenameColumn(table: "dbo.Bets", name: "Creator_Id", newName: "Bettor_Id");
        }
    }
}
