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
    public class BrotherManager
    {
        private readonly IBrotherRepository _brotherRepository;

        public BrotherManager()
        {
            _brotherRepository = new BrotherRepository();
        }

        public BrotherManager(IBrotherRepository brotherRepository)
        {
            _brotherRepository = brotherRepository;
        }

        public Brother Get(string name)
        {
            return _brotherRepository.Get(name);
        }

        public List<Brother> GetAll()
        {
            return _brotherRepository.GetAll();
        }
    }
}
