using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrotherBetsLibrary.Models;

namespace BrotherBetsLibrary.Data
{
    public interface IBetRepository
    {
        void Add(Bet bet, int brotherId);
        List<Bet> GetAll();
    }
}
