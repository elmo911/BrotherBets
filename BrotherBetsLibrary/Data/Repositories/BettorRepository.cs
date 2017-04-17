using System.Collections.Generic;
using System.Linq;
using BrotherBetsLibrary.Data.Interfaces;
using BrotherBetsLibrary.Models;

namespace BrotherBetsLibrary.Data.Repositories
{
    public class BettorRepository : IBettorRepository
    {
        private readonly BrotherBetContext _context;

        public BettorRepository()
        {
            _context = new BrotherBetContext();
        }

        public Bettor Get(string name)
        {
            return _context.Bettors
                .FirstOrDefault(b => b.Name == name);
        }

        public void Add(string name)
        {
            _context.Bettors.Add(new Bettor() {Name = name});
            _context.SaveChanges();
        }

        public List<Bettor> GetAll()
        {
            return _context.Bettors.ToList();
        }

        public IEnumerable<string> NamesLike(string partialName)
        {
           return _context.Bettors
                .Select(b => b.Name)
                .Where(name => name.Contains(partialName));
        }
    }
}
