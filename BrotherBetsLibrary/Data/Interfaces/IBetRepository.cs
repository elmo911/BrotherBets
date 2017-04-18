using System.Collections.Generic;
using BrotherBetsLibrary.Models;

namespace BrotherBetsLibrary.Data.Interfaces
{
    public interface IBetRepository
    {
        void Add(Bet bet, Bettor bettor, Brother brother);
        List<Bet> GetAll();
        Bet Get(int betId);
        void TakeBet(Bettor bettor, BetOption outcome, Brother brother);
        bool HasTakenBet(Bettor bettor, Bet bet);
        void MarkComplete(Bet bet, Bettor bettor);
        void MarkCorrect(BetOption betOption);
        void AddPointsToSuccessfulGuess(BetOption betOption, long points);
    }
}
