using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using api.Dtos;
using api.Entities;
using api.Models;

namespace api.Interfaces
{
    public interface ISudokuGameService
    {
        Task<int> SaveGame(SaveGameRequest gameData, Guid objectId);
        Task<List<Game>> GetGameHistoryByUser(Guid objectId);
        Task<List<ScoreboardResponse>> GetScoreboard();
        Task<Game> GetGameById(int gameId);
    }
}
