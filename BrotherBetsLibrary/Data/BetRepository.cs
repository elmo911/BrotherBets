using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrotherBetsLibrary.Models;

namespace BrotherBetsLibrary.Data
{
    public class BetRepository : IBetRepository
    {
        private BrotherBetContext _context;

        public BetRepository()
        {
            _context = new BrotherBetContext();
        }

        public void Add(Bet bet, int brotherId)
        {
            bet.TargetOfBet = _context.Brothers.Find(brotherId);
            _context.Bets.Add(bet);
            _context.SaveChanges();
        }

        public List<Bet> GetAll()
        {
            return _context.Bets.ToList();
        }
    }
}
