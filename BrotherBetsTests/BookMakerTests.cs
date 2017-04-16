using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrotherBetsLibrary;
using BrotherBetsLibrary.Data;
using BrotherBetsLibrary.Models;
using Moq;
using Xunit;

namespace BrotherBetsTests
{
    public class BookMakerTests
    {
        [Fact]
        public void AddBet_BetNull_ArgumentNullEx()
        {
            var bookie = new BookMaker();
            Assert.Throws<ArgumentNullException>(() => bookie.AddBet(null, new Brother(), new[] {""}));
        }
        [Fact]
        public void AddBet_CreatorNull_ArgumentNullEx()
        {
            var bookie = new BookMaker();
            Assert.Throws<ArgumentNullException>(() => bookie.AddBet(new Bet(), null, new[] { "" }));
        }
        [Fact]
        public void AddBet_OutcomesNull_ArgumentNullEx()
        {
            var bookie = new BookMaker();
            Assert.Throws<ArgumentNullException>(() => bookie.AddBet(new Bet(), new Brother(), null));
        }
        [Fact]
        public void AddBet_EmptyPredictions_ThrowsException()
        {
            var betRepoMock = new Mock<IBetRepository>();
            var bookie = new BookMaker(betRepoMock.Object);
            Assert.Throws<Exception>(() => bookie.AddBet(new Bet(), new Brother(), new[] {"", "", ""}));
        }
        [Fact]
        public void AddBet_OnePrediction_ThrowsException()
        {
            var betRepoMock = new Mock<IBetRepository>();
            var bookie = new BookMaker(betRepoMock.Object);
            Assert.Throws<Exception>(() => bookie.AddBet(new Bet(), new Brother(), new[] { "Outcome1", "" }));
        }

        [Fact]
        public void AddBet_ValidInput_AddsBet()
        {
            var betRepoMock = new Mock<IBetRepository>();
            var bookie = new BookMaker(betRepoMock.Object);
            bookie.AddBet(new Bet(), new Brother(), new[] { "Outcome1", "Outcome2", "", "" });
            betRepoMock.Verify(m => m.Add(It.IsAny<Bet>(), It.IsAny<int>()));
        }

        [Fact]
        public void GetAll_GetsAllBets()
        {
            var betRepoMock = new Mock<IBetRepository>();
            var bookie = new BookMaker(betRepoMock.Object);
            var result = bookie.GetBets();
            betRepoMock.Verify(m => m.GetAll());
        }
    }
}
