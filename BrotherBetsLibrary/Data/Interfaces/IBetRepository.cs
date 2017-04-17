using System.Collections.Generic;
using BrotherBetsLibrary.Models;

namespace BrotherBetsLibrary.Data.Interfaces
{
    public interface IBetRepository
    {
        void Add(Bet bet, int bettorId, int brotherId);
        List<Bet> GetAll();
    }
}
