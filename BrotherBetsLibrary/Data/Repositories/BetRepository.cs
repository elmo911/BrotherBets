using System;
using System.Collections.Generic;
using System.Linq;
using BrotherBetsLibrary.Data.Interfaces;
using BrotherBetsLibrary.Models;

namespace BrotherBetsLibrary.Data.Repositories
{
    public class BetRepository : IBetRepository
    {
        private readonly BrotherBetContext _context;

        public BetRepository()
        {
            _context = new BrotherBetContext();
        }

        public void Add(Bet bet, Bettor bettor, Brother brother)
        {
            bet.Brother = _context.Brothers.Find(brother.Id);
            bet.Creator = _context.Bettors.Find(bettor.Id);
            _context.Bets.Add(bet);
            _context.SaveChanges();
        }

        public List<Bet> GetAll()
        {
            return _context.Bets.ToList();
        }

        public Bet Get(int betId)
        {
            return _context.Bets.Find(betId);
        }

        public void TakeBet(Bettor bettor, BetOption outcome, Brother brother)
        {
            brother = _context.Brothers.Find(brother.Id);
            var prediction = new Prediction()
            {
                Bettor = _context.Bettors.Find(bettor.Id),
                OutcomePredicted = _context.BetOptions.Find(outcome.Id),
                TimeOfPrediction = DateTime.Now
            };
            if (brother.Predictions == null)
                brother.Predictions = new List<Prediction>();
            brother.Predictions.Add(prediction);
            _context.SaveChanges();
        }

        public bool HasTakenBet(Bettor bettor, Bet bet)
        {
            return _context.Predictions
                .Any(p => p.Bettor.Id == bettor.Id && p.OutcomePredicted.Bet.Id == bet.Id);
        }

        public void MarkComplete(Bet bet, Bettor bettor)
        {
            bet = _context.Bets.Find(bet.Id);
            bet.Complete = true;
            bet.MarkedCompleteBy = _context.Bettors.Find(bettor.Id);
            _context.SaveChanges();
        }

        public void MarkCorrect(BetOption betOption)
        {
            var outcome = _context.BetOptions.Find(betOption.Id);
            outcome.Correct = true;
            _context.SaveChanges();
        }

        public void AddPointsToSuccessfulGuess(BetOption betOption, long points)
        {
            var successfulGuessers = _context.Predictions
                .Where(p => p.OutcomePredicted.Id == betOption.Id)
                .Select(p => p.Bettor);
            foreach (var bettor in successfulGuessers)
            {
                bettor.Points += points;
            }
            _context.SaveChanges();
        }
    }
}
