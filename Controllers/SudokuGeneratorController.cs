using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SudokuGeneratorController : ControllerBase
    {
        private readonly ISudokuGeneratorService _sudokuGeneratorService;

        public SudokuGeneratorController(ISudokuGeneratorService sudokuGeneratorService)
        {
            _sudokuGeneratorService = sudokuGeneratorService;
        }
        
        [HttpGet("GetNewGame")]
        public async Task<ActionResult> GetNewGame(int difficulty)
        {
            var newGame = await _sudokuGeneratorService.GetNewGame(difficulty);
            return Ok(newGame);
        }
    }
}
