using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrotherBetsLibrary;
using BrotherBetsLibrary.Data;
using BrotherBetsLibrary.Data.Interfaces;
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
            Assert.Throws<ArgumentNullException>(() => bookie.AddBet(null, new Bettor(){Id = 1}, new Brother(){Id = 1}, new[] {""}));
        }
        [Fact]
        public void AddBet_BetIdSet_ArgumentEx()
        {
            var bookie = new BookMaker();
            Assert.Throws<ArgumentException>(() => bookie.AddBet(new Bet(){Id = 1}, new Bettor() { Id = 1 }, new Brother() { Id = 1 }, new[] { "" }));
        }
        [Fact]
        public void AddBet_BetTargetNullOrDefault_ArgumentEx()
        {
            var bookie = new BookMaker();
            Assert.Throws<ArgumentNullException>(() => bookie.AddBet(new Bet(), new Bettor() { Id = 1 }, null, new[] { "" }));
            Assert.Throws<ArgumentException>(() => bookie.AddBet(new Bet(), new Bettor() { Id = 1 }, new Brother(), new[] { "" }));
        }

        [Fact]
        public void AddBet_CreatorNullOrDefault_ArgumentEx()
        {
            var bookie = new BookMaker();
            Assert.Throws<ArgumentNullException>(() => bookie.AddBet(new Bet(), null, null, new[] { "" }));
            Assert.Throws<ArgumentException>(() => bookie.AddBet(new Bet(), new Bettor(), new Brother(), new[] { "" }));
        }

        [Fact]
        public void AddBet_OutcomesNull_ArgumentNullEx()
        {
            var bookie = new BookMaker();
            Assert.Throws<ArgumentNullException>(() => bookie.AddBet(new Bet(), new Bettor() { Id = 1 }, new Brother() { Id = 1 }, null));
        }
        [Fact]
        public void AddBet_EmptyPredictions_Exception()
        {
            var betRepoMock = new Mock<IBetRepository>();
            var bookie = new BookMaker(betRepoMock.Object);
            Assert.Throws<Exception>(() => bookie.AddBet(new Bet(), new Bettor() { Id = 1 }, new Brother() { Id = 1 }, new[] {"", "", ""}));
        }
        [Fact]
        public void AddBet_OnePrediction_ThrowsException()
        {
            var betRepoMock = new Mock<IBetRepository>();
            var bookie = new BookMaker(betRepoMock.Object);
            Assert.Throws<Exception>(() => bookie.AddBet(new Bet(), new Bettor() { Id = 1 }, new Brother() { Id = 1 }, new[] { "Outcome1", "" }));
        }

        [Fact]
        public void AddBet_ValidInput_AddsBet()
        {
            var betRepoMock = new Mock<IBetRepository>();
            var bookie = new BookMaker(betRepoMock.Object);
            bookie.AddBet(new Bet(), new Bettor() { Id = 1 }, new Brother() { Id = 1 }, new[] { "Outcome1", "Outcome2", "", "" });
            betRepoMock.Verify(m => m.Add(It.IsAny<Bet>(), It.IsAny<Bettor>(), It.IsAny<Brother>()));
        }

        [Fact]
        public void Bets_GetsAllBets()
        {
            var betRepoMock = new Mock<IBetRepository>();
            var bookie = new BookMaker(betRepoMock.Object);
            var result = bookie.Bets();
            betRepoMock.Verify(m => m.GetAll());
        }

        [Fact]
        public void GetBet_GetsBet()
        {
            var betRepoMock = new Mock<IBetRepository>();
            var bookie = new BookMaker(betRepoMock.Object);
            var result = bookie.GetBet(1);
            betRepoMock.Verify(m => m.Get(1));
        }

        [Fact]
        public void TakeBet_NullBettor_ThrowsException()
        {
            var bookie = new BookMaker();
            Assert.Throws<ArgumentException>(() => bookie.TakeBet(null, new BetOption(){Id = 1}, new Brother(){Id = 1}));
        }

        [Fact]
        public void TakeBet_NoBettorId_ThrowsException()
        {
            var bookie = new BookMaker();
            Assert.Throws<ArgumentException>(() => bookie.TakeBet(new Bettor(), new BetOption() { Id = 1 }, new Brother() { Id = 1 }));
        }

        [Fact]
        public void TakeBet_NullOption_ThrowsException()
        {
            var bookie = new BookMaker();
            Assert.Throws<ArgumentException>(() => bookie.TakeBet(new Bettor(){Id = 1}, null, new Brother() { Id = 1 }));
        }

        [Fact]
        public void TakeBet_NoOptionId_ThrowsException()
        {
            var bookie = new BookMaker();
            Assert.Throws<ArgumentException>(() => bookie.TakeBet(new Bettor() { Id = 1 }, new BetOption(), new Brother() { Id = 1 }));
        }

        [Fact]
        public void TakeBet_ValidInput_AddsPrediction()
        {
            var betRepoMock = new Mock<IBetRepository>();
            var bookie = new BookMaker(betRepoMock.Object);
            bookie.TakeBet(new Bettor(){Id = 1}, new BetOption(){Id = 1}, new Brother() { Id = 1 });
            betRepoMock.Verify(m => m.TakeBet(It.IsAny<Bettor>(), It.IsAny<BetOption>(), It.IsAny<Brother>()));
        }
    }
}
