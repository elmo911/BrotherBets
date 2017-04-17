using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrotherBetsLibrary.Data;
using BrotherBetsLibrary.Data.Interfaces;
using BrotherBetsLibrary.Data.Repositories;
using BrotherBetsLibrary.Models;

namespace BrotherBetsLibrary
{
    public class BookMaker
    {
        private IBetRepository _betRepository;

        public BookMaker()
        {
            _betRepository = new BetRepository();
        }

        public BookMaker(IBetRepository betRepository)
        {
            _betRepository = betRepository;
        }

        public void AddBet(Bet bet, Bettor bettor, Brother betTarget, string[] predictedOutcomes)
        {
            if (bet == null) throw new ArgumentNullException(nameof(bet));
            if (bet.Id != default(int)) throw new ArgumentException(nameof(bet));
            if (bettor == null) throw new ArgumentNullException(nameof(bettor));
            if (bettor.Id == default(int)) throw new ArgumentException(nameof(bettor));
            if (betTarget == null) throw new ArgumentNullException(nameof(betTarget));
            if (betTarget.Id == default(int)) throw new ArgumentException(nameof(betTarget));
            if (predictedOutcomes == null) throw new ArgumentNullException(nameof(predictedOutcomes));

            if (bet.Expiration < DateTime.Today.AddDays(2))
                throw new Exception(
                    $"Come on, you need to give more time than that! Set the date to at least {DateTime.Today.AddDays(2).ToShortDateString()}");

            var betOutcomes = predictedOutcomes
                .Where(o => !string.IsNullOrWhiteSpace(o))
                .Select(o => new BetOption() { Outcome = o, Bet = bet });
            bet.BetOptions = new List<BetOption>(betOutcomes);
            if(bet.BetOptions.Count < 2) throw new Exception("Bets must have at least two outcomes");
            _betRepository.Add(bet, bettor, betTarget);
        }

        public List<Bet> Bets()
        {
            return _betRepository.GetAll();
        }

        public Bet GetBet(int betId)
        {
            return _betRepository.Get(betId);
        }

        public void TakeBet(Bettor bettor, BetOption outcome, Brother brother)
        {
            if(bettor == null) throw new ArgumentNullException(nameof(bettor));
            if(bettor.Id == default(int)) throw new ArgumentException( nameof(bettor));
            if(outcome == null) throw new ArgumentNullException(nameof(outcome));
            if(outcome.Id == default(int)) throw new ArgumentException(nameof(outcome));
            if(brother == null) throw new ArgumentNullException(nameof(brother));
            if(brother.Id == default(int)) throw new ArgumentException(nameof(brother));
            _betRepository.TakeBet(bettor, outcome, brother);
        }

        public bool HasTakenBet(Bettor bettor, Bet bet)
        {
            if (bettor == null) throw new ArgumentNullException(nameof(bettor));
            if (bettor.Id == default(int)) throw new ArgumentException(nameof(bettor));
            if (bet == null) throw new ArgumentNullException(nameof(bet));
            if (bet.Id == default(int)) throw new ArgumentException(nameof(bet));
            return _betRepository.HasTakenBet(bettor, bet);
        }
    }
}
