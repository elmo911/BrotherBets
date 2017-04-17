using System.Collections.Generic;
using BrotherBetsLibrary.Models;

namespace BrotherBetsLibrary.Data.Interfaces
{
    public interface IBettorRepository
    {
        Bettor Get(string name);
        void Add(string name);
        List<Bettor> GetAll();
        IEnumerable<string> NamesLike(string partialName);
    }
}
