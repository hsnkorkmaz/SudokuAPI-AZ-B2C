using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface ISudokuGeneratorService
    {
        Task<Board> GetNewGame(int difficulty);
    }
}