using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using api.Dtos;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SudokuSolverController : ControllerBase
    {
        private readonly ISudokuSolverService _sudokuSolverService;

        public SudokuSolverController(ISudokuSolverService sudokuSolverService)
        {
            _sudokuSolverService = sudokuSolverService;
        }

        [HttpPost("SolveBoard")]
        public async Task<ActionResult> SolveBoard(Board board)
        {
            var solvedBoard = await Task.Run(() => _sudokuSolverService.SolveGivenBoard(board));
            return Ok(solvedBoard);
        }

        [HttpPost("ValidateBoard")]
        public async Task<ActionResult> ValidateBoard(Board board)
        {
            var validatedBoard = await _sudokuSolverService.ValidateBoard(board);
            return Ok(validatedBoard);
        }

        [HttpPost("IsPossible")]
        public async Task<ActionResult> CheckIsPossible(IsPossibleRequestDto request)
        {
            bool result = await Task.Run(() => _sudokuSolverService.IsPossible(request.Numbers, request.Index, request.Value));

            var response = new IsPossibleResponseDto()
            {
                Index = request.Index,
                Value = request.Value,
                IsPossible = result
            };

            return Ok(response);
        }
    }
}
