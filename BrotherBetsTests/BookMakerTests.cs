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
        
        private Bet ValidNewBet => new Bet() {Expiration = DateTime.Today.AddDays(2)};
        private Bettor ValidBettor => new Bettor {Id = 1};
        private Brother ValidBrother => new Brother {Id = 1};
        private string[] ValidOutcome => new[] {"Outcome 1", "Outcome 2", "", ""};
        private BetOption ValidBetOption => new BetOption() { Id = 1 };
        private Bet ValidBet => new Bet() { Id = 1};

        [Fact]
        public void AddBet_BetNull_ArgumentNullEx()
        {
            var bookie = new BookMaker();
            Assert.Throws<ArgumentNullException>(() => bookie.AddBet(null, ValidBettor, ValidBrother, ValidOutcome));
        }
        [Fact]
        public void AddBet_BetIdSet_ArgumentEx()
        {
            var bookie = new BookMaker();
            Assert.Throws<ArgumentException>(() => bookie.AddBet(new Bet(){Id = 1}, ValidBettor, ValidBrother, ValidOutcome));
        }
        [Fact]
        public void AddBet_BetTargetNullOrDefault_ArgumentEx()
        {
            var bookie = new BookMaker();
            Assert.Throws<ArgumentNullException>(() => bookie.AddBet(ValidNewBet, ValidBettor, null, ValidOutcome));
            Assert.Throws<ArgumentException>(() => bookie.AddBet(ValidNewBet, ValidBettor, new Brother(), ValidOutcome));
        }

        [Fact]
        public void AddBet_BettorNullOrDefault_ArgumentEx()
        {
            var bookie = new BookMaker();
            Assert.Throws<ArgumentNullException>(() => bookie.AddBet(ValidNewBet, null, ValidBrother, ValidOutcome));
            Assert.Throws<ArgumentException>(() => bookie.AddBet(ValidNewBet, new Bettor(), ValidBrother, ValidOutcome));
        }

        [Fact]
        public void AddBet_OutcomesNull_ArgumentNullEx()
        {
            var bookie = new BookMaker();
            Assert.Throws<ArgumentNullException>(() => bookie.AddBet(ValidNewBet, ValidBettor, ValidBrother, null));
        }
        [Fact]
        public void AddBet_EmptyPredictions_Exception()
        {
            var betRepoMock = new Mock<IBetRepository>();
            var bookie = new BookMaker(betRepoMock.Object);
            Assert.Throws<Exception>(() => bookie.AddBet(ValidNewBet, ValidBettor, ValidBrother, new[] {"", "", ""}));
        }
        [Fact]
        public void AddBet_OnePrediction_ThrowsException()
        {
            var betRepoMock = new Mock<IBetRepository>();
            var bookie = new BookMaker(betRepoMock.Object);
            Assert.Throws<Exception>(() => bookie.AddBet(ValidNewBet, ValidBettor, ValidBrother, new[] {"Prediction", "", ""}));
        }

        [Fact]
        public void AddBet_ExpirationLessThanTwoDaysFromNow_ThrowsException()
        {
            var betRepoMock = new Mock<IBetRepository>();
            var bookie = new BookMaker(betRepoMock.Object);
            Assert.Throws<Exception>(() => bookie.AddBet(new Bet(){Expiration = DateTime.Today.AddDays(1)}, ValidBettor, ValidBrother, ValidOutcome));
        }

        [Fact]
        public void AddBet_ValidInput_AddsBet()
        {
            var betRepoMock = new Mock<IBetRepository>();
            var bookie = new BookMaker(betRepoMock.Object);
            bookie.AddBet(ValidNewBet, ValidBettor, ValidBrother, ValidOutcome);
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
        public void TakeBet_NullBettor_ThrowsArgumentNullEx()
        {
            var bookie = new BookMaker();
            Assert.Throws<ArgumentNullException>(() => bookie.TakeBet(null, ValidBetOption, ValidBrother));
        }

        [Fact]
        public void TakeBet_NoBettorId_ThrowsArgumentException()
        {
            var bookie = new BookMaker();
            Assert.Throws<ArgumentException>(() => bookie.TakeBet(new Bettor(), ValidBetOption, ValidBrother));
        }

        [Fact]
        public void TakeBet_NullOption_ThrowsArgumentNullEx()
        {
            var bookie = new BookMaker();
            Assert.Throws<ArgumentNullException>(() => bookie.TakeBet(ValidBettor, null, ValidBrother));
        }

        [Fact]
        public void TakeBet_NoOptionId_ThrowsArgumentException()
        {
            var bookie = new BookMaker();
            Assert.Throws<ArgumentException>(() => bookie.TakeBet(ValidBettor, new BetOption(), ValidBrother));
        }

        [Fact]
        public void TakeBet_ValidInput_AddsPrediction()
        {
            var betRepoMock = new Mock<IBetRepository>();
            var bookie = new BookMaker(betRepoMock.Object);
            bookie.TakeBet(ValidBettor, ValidBetOption, ValidBrother);
            betRepoMock.Verify(m => m.TakeBet(It.IsAny<Bettor>(), It.IsAny<BetOption>(), It.IsAny<Brother>()));
        }

        [Fact]
        public void HasTakenBet_NullBet_ThrowsNullException()
        {
            var bookie = new BookMaker();
            Assert.Throws<ArgumentNullException>(() => bookie.HasTakenBet(ValidBettor, null));
        }

        [Fact]
        public void HasTakenBet_DefaultBet_ThrowsArguementException()
        {
            var bookie = new BookMaker();
            Assert.Throws<ArgumentException>(() => bookie.HasTakenBet(ValidBettor, new Bet()));
        }

        [Fact]
        public void HasTakenBet_NullBettor_ThrowsNullException()
        {
            var bookie = new BookMaker();
            Assert.Throws<ArgumentNullException>(() => bookie.HasTakenBet(null, ValidBet));
        }

        [Fact]
        public void HasTakenBet_DefaultBettor_ThrowsArgumentException()
        {
            var bookie = new BookMaker();
            Assert.Throws<ArgumentException>(() => bookie.HasTakenBet(new Bettor(), ValidBet));
        }

        [Fact]
        public void HasTakenBet_Valid_ReturnsIfBetterBetOnBet()
        {
            var repo = new Mock<IBetRepository>();
            var bookie = new BookMaker(repo.Object);
            var betTaken = bookie.HasTakenBet(ValidBettor, ValidBet);
            repo.Verify(m => m.HasTakenBet(It.IsAny<Bettor>(), It.IsAny<Bet>()));
        }
    }
}
