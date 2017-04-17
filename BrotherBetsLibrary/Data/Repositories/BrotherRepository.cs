using System.Collections.Generic;
using System.Linq;
using BrotherBetsLibrary.Data.Interfaces;
using BrotherBetsLibrary.Models;

namespace BrotherBetsLibrary.Data.Repositories
{
    public class BrotherRepository : IBrotherRepository
    {
        private BrotherBetContext _context;

        public BrotherRepository()
        {
            _context = new BrotherBetContext();
        }

        public Brother Get(string name)
        {
            return _context.Brothers
                .FirstOrDefault(b => b.Name == name);
        }

        public List<Brother> GetAll()
        {
            return _context.Brothers.ToList();
        }
    }
}
