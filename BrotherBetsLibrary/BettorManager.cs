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
    public class BettorManager
    {
        private IBettorRepository _bettorRepository;

        public BettorManager()
        {
            _bettorRepository = new BettorRepository();
        }

        public BettorManager(IBettorRepository bettorRepository)
        {
            _bettorRepository = bettorRepository;
        }

        public List<Bettor> Bettors()
        {
            return _bettorRepository.GetAll();
        }

        public Bettor Get(string bettorName)
        {
            return _bettorRepository.Get(bettorName);
        }

        public void Add(string bettorName)
        {
            _bettorRepository.Add(bettorName);
        }

        public IEnumerable<string> BettorNamesLike(string partialName)
        {
            return _bettorRepository.NamesLike(partialName);
        }
    }
}
