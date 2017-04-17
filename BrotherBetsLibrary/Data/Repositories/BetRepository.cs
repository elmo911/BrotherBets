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

        public void Add(Bet bet, int bettorId, int brotherId)
        {
            bet.Brother = _context.Brothers.Find(brotherId);
            bet.Bettor = _context.Bettors.Find(bettorId);
            _context.Bets.Add(bet);
            _context.SaveChanges();
        }

        public List<Bet> GetAll()
        {
            return _context.Bets.ToList();
        }
    }
}
