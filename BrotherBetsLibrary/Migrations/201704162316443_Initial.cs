namespace BrotherBetsLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Prediction = c.String(nullable: false, maxLength: 50),
                        Expiration = c.DateTime(nullable: false),
                        Bettor_Id = c.Int(nullable: false),
                        Brother_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bettors", t => t.Bettor_Id, cascadeDelete: true)
                .ForeignKey("dbo.Brothers", t => t.Brother_Id)
                .Index(t => t.Bettor_Id)
                .Index(t => t.Brother_Id);
            
            CreateTable(
                "dbo.BetOptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Outcome = c.String(maxLength: 75),
                        Bet_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bets", t => t.Bet_Id)
                .Index(t => t.Bet_Id);
            
            CreateTable(
                "dbo.Bettors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 75),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.Brothers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Predictions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TimeOfPrediction = c.DateTime(nullable: false),
                        Bettor_Id = c.Int(),
                        OutcomePredicted_Id = c.Int(),
                        Brother_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bettors", t => t.Bettor_Id)
                .ForeignKey("dbo.BetOptions", t => t.OutcomePredicted_Id)
                .ForeignKey("dbo.Brothers", t => t.Brother_Id)
                .Index(t => t.Bettor_Id)
                .Index(t => t.OutcomePredicted_Id)
                .Index(t => t.Brother_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bets", "Brother_Id", "dbo.Brothers");
            DropForeignKey("dbo.Predictions", "Brother_Id", "dbo.Brothers");
            DropForeignKey("dbo.Predictions", "OutcomePredicted_Id", "dbo.BetOptions");
            DropForeignKey("dbo.Predictions", "Bettor_Id", "dbo.Bettors");
            DropForeignKey("dbo.Bets", "Bettor_Id", "dbo.Bettors");
            DropForeignKey("dbo.BetOptions", "Bet_Id", "dbo.Bets");
            DropIndex("dbo.Predictions", new[] { "Brother_Id" });
            DropIndex("dbo.Predictions", new[] { "OutcomePredicted_Id" });
            DropIndex("dbo.Predictions", new[] { "Bettor_Id" });
            DropIndex("dbo.Bettors", new[] { "Name" });
            DropIndex("dbo.BetOptions", new[] { "Bet_Id" });
            DropIndex("dbo.Bets", new[] { "Brother_Id" });
            DropIndex("dbo.Bets", new[] { "Bettor_Id" });
            DropTable("dbo.Predictions");
            DropTable("dbo.Brothers");
            DropTable("dbo.Bettors");
            DropTable("dbo.BetOptions");
            DropTable("dbo.Bets");
        }
    }
}
