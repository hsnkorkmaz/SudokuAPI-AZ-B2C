using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos;
using api.Entities;
using api.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph;

namespace api.Services
{
    public class SudokuGameService : ISudokuGameService
    {
        private readonly SqlContext _sqlContext;
        private readonly IGraphUserService _graphUserService;

        public SudokuGameService(SqlContext sqlContext, IGraphUserService graphUserService)
        {
            _sqlContext = sqlContext;
            _graphUserService = graphUserService;
        }
        public async Task<Game> GetGameById(int gameId)
        {
            var game = await _sqlContext.Games.FirstOrDefaultAsync(x => x.Id == gameId);
            return game;
        }
        public async Task<List<Game>> GetGameHistoryByUser(Guid objectId)
        {
            var gamesHistory = await _sqlContext.Games.Where(x => x.ObjectId == objectId).OrderByDescending(x=> x.Id).Take(10).ToListAsync();
            return gamesHistory;
        }
        
        public async Task<List<ScoreboardResponse>> GetScoreboard()
        {
            var topGames = await _sqlContext.Games.Where(x => x.Finished == true).OrderByDescending(x => x.Points).Take(5).ToListAsync();
            var topScores = new List<ScoreboardResponse>();

            foreach (var game in topGames)
            {
                var userDetails = await _graphUserService.GetUserByObjectId(game.ObjectId.ToString());

                var tempPlayer = new ScoreboardResponse()
                {
                    DisplayName = userDetails.DisplayName,
                    Score = game.Points
                };

                topScores.Add(tempPlayer);
            }

            return topScores;
        }

        public async Task<int> SaveGame(SaveGameRequest gameData, Guid objectId)
        {
            var newGame = new Game()
            {
                SolvedNumbers = string.Join("", gameData.SolvedNumbers),
                StaticNumbers = string.Join("", gameData.StaticNumbers),
                TimeSpent = gameData.TimeSpent,
                Points = gameData.Points,
                Mistakes = gameData.Mistakes,
                Finished = !gameData.SolvedNumbers.Contains(0),
                ObjectId = objectId
            };

            var sameBoard = await _sqlContext.Games.FirstOrDefaultAsync(x => x.StaticNumbers == string.Join("", gameData.StaticNumbers));

            if (sameBoard != null)
            {
                sameBoard.SolvedNumbers = string.Join("", gameData.SolvedNumbers);
                sameBoard.TimeSpent = gameData.TimeSpent;
                sameBoard.Points = gameData.Points;
                gameData.Mistakes = gameData.Mistakes;
                gameData.Finished = !gameData.SolvedNumbers.Contains(0);
                _sqlContext.Games.Update(sameBoard);
            }
            else
            {
                await _sqlContext.Games.AddAsync(newGame);
            }
            
            int gameId = await _sqlContext.SaveChangesAsync();
            return gameId;
        }



    }
}
