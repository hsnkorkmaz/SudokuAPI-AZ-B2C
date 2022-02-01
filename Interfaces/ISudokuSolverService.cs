using System.Collections.Generic;
using System.Threading.Tasks;
using api.Dtos;
using api.Models;

namespace api.Interfaces
{
    public interface ISudokuSolverService
    {
        Board SolveGivenBoard(Board board);
        bool IsPossible(List<int> numbers, int index, int possibleNumber);
        Task<ValidateBoardResponseDto> ValidateBoard(Board board);
    }
}
