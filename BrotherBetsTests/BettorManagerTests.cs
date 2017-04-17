using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrotherBetsLibrary;
using BrotherBetsLibrary.Data.Interfaces;
using Moq;
using Xunit;

namespace BrotherBetsTests
{
    public class BettorManagerTests
    {
        [Fact]
        public void Add_ByName_AddsBettor()
        {
            var repo = new Mock<IBettorRepository>();
            var manager = new BettorManager(repo.Object);
            manager.Add("Jacob");
            repo.Verify(m => m.Add("Jacob"));
        }

        [Fact]
        public void Get_ByName_GetsBettor()
        {
            var repo = new Mock<IBettorRepository>();
            var manager = new BettorManager(repo.Object);
            var bettor = manager.Get("Jacob");
            repo.Verify(m => m.Get("Jacob"));
        }

        [Fact]
        public void Bettors_GetsAllBettors()
        {
            var repo = new Mock<IBettorRepository>();
            var manager = new BettorManager(repo.Object);
            var bettors = manager.Bettors();
            repo.Verify(m => m.GetAll());
        }

        [Fact]
        public void BettorNamesLike_GetsBettorNamesLikeTerm()
        {
            var repo = new Mock<IBettorRepository>();
            var manager = new BettorManager(repo.Object);
            var bettors = manager.BettorNamesLike("Ja");
            repo.Verify(m => m.NamesLike("Ja"));
        }
    }
}
