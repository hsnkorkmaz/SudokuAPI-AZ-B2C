using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using api.Dtos;
using api.Entities;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace api.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class SudokuGameController : ControllerBase
    {
        private readonly ISudokuSolverService _sudokuSolverService;
        private readonly ISudokuGameService _sudokuGameService;
        private readonly IGraphUserService _graphUserService;

        public SudokuGameController(ISudokuSolverService sudokuSolverService, ISudokuGameService sudokuGameService, IGraphUserService graphUserService)
        {
            _sudokuSolverService = sudokuSolverService;
            _sudokuGameService = sudokuGameService;
            _graphUserService = graphUserService;
        }

        [Authorize]
        [RequiredScope("Client.Read")]
        [HttpPost("SaveGame")]
        public async Task<ActionResult> SaveGame(SaveGameRequest request)
        {
            var objectId = await _graphUserService.GetObjectIdFromClaims(HttpContext.User.Claims);
            var gameId = await _sudokuGameService.SaveGame(request, Guid.Parse(objectId));
            return Ok(gameId);
        }

        [Authorize]
        [RequiredScope("Client.Read")]
        [HttpGet("GetGameHistoryByUser")]
        public async Task<List<Game>> GetGameHistory()
        {
            var objectId = await _graphUserService.GetObjectIdFromClaims(HttpContext.User.Claims);
            var games = await _sudokuGameService.GetGameHistoryByUser(Guid.Parse(objectId));
            return games;
        }

        [HttpGet("GetScoreBoard")]
        public async Task<List<ScoreboardResponse>> GetScoreBoard()
        {
            var scoreBoard = await _sudokuGameService.GetScoreboard();
            return scoreBoard;

        }

        //[Authorize]
        [HttpGet("GetGameById")]
        public async Task<Game> GetGameById(int gameId)
        {
            var savedGame = await _sudokuGameService.GetGameById(gameId);
            return savedGame;
        }
    }
}
