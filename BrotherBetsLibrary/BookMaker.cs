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
            if (betTarget == null) throw new ArgumentNullException(nameof(betTarget));
            if (predictedOutcomes == null) throw new ArgumentNullException(nameof(predictedOutcomes));

            var betOutcomes = predictedOutcomes
                .Where(o => !string.IsNullOrWhiteSpace(o))
                .Select(o => new BetOption() { Outcome = o, Bet = bet });
            bet.BetOptions = new List<BetOption>(betOutcomes);
            if(bet.BetOptions.Count < 2) throw new Exception("Bets must have at least two outcomes");
            _betRepository.Add(bet, bettor.Id, betTarget.Id);
        }

        public List<Bet> GetBets()
        {
            return _betRepository.GetAll();
        }
    }
}
