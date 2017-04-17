using System.Data.Entity;
using BrotherBetsLibrary.Models;

namespace BrotherBetsLibrary.Data
{
    internal class BrotherBetContext : DbContext
    {
        public BrotherBetContext()
            : base("name=BrotherBetContext")
        {
        }

        public virtual DbSet<Bet> Bets { get; set; }
        public virtual DbSet<BetOption> BetOptions { get; set; }
        public virtual DbSet<Brother> Brothers { get; set; }
        public virtual DbSet<Prediction> Predictions { get; set; }
        public virtual DbSet<Bettor> Bettors { get; set; }
    }
}