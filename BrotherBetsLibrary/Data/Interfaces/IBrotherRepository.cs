using System.Collections.Generic;
using BrotherBetsLibrary.Models;

namespace BrotherBetsLibrary.Data.Interfaces
{
    public interface IBrotherRepository
    {
        Brother Get(string name);
        List<Brother> GetAll();
    }
}
