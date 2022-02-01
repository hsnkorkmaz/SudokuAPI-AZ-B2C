using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using api.Interfaces;
using api.Models;

namespace api.Services
{
    public class SudokuGeneratorService : ISudokuGeneratorService
    {
        private readonly ISudokuSolverService _sudokuSolverService;

        private readonly Random _randomCreator = new Random();
        private readonly int _gridSize = 9;
        private readonly int _emptyCell = 0;
        private readonly int _randomNumberCount = 10;
        private Board _randomBoard = new Board() { Name = "Random Board" };
        private Board _solvedBoard;

        public SudokuGeneratorService(ISudokuSolverService sudokuSolverService)
        {
            _sudokuSolverService = sudokuSolverService;
        }

        public async Task<Board> GetNewGame(int difficulty)
        {
            var cancellation = new CancellationTokenSource();

            var sudokuTask = GenerateRandomBoard(difficulty, cancellation.Token);

            if (sudokuTask.Wait(750)) return sudokuTask.Result;
            cancellation.Cancel();
            
            var newBoard = new Board
            {
                Name = "Hardest Sudoku",
                //worlds hardest sudoku
                Numbers = new List<int>()
                {
                    8, 1, 2, 7, 5, 3, 6, 4, 9,
                    9, 4, 3, 6, 8, 2, 1, 7, 5,
                    6, 7, 5, 4, 9, 1, 2, 8, 3,
                    1, 5, 4, 2, 3, 7, 8, 9, 6,
                    3, 6, 9, 8, 4, 5, 7, 2, 1,
                    2, 8, 7, 1, 6, 9, 5, 3, 4,
                    5, 2, 1, 9, 7, 4, 3, 6, 8,
                    4, 3, 8, 5, 2, 6, 9, 1, 7,
                    7, 9, 6, 3, 1, 8, 4, 5, 2
                }
            };

            newBoard.Numbers = MakeRandomEmpty(newBoard.Numbers, difficulty);
            return newBoard;

        }



        public async Task<Board> GenerateRandomBoard(int difficulty, CancellationToken cancellationToken)
        {
            return await Task.Run(() => {

                _randomBoard.Numbers = FillEmpty();

                for (int i = 0; i < _randomNumberCount; i++)
                {
                    SetRandomNumber();
                }

                _solvedBoard.Numbers = MakeRandomEmpty(_solvedBoard.Numbers, difficulty);

                return _solvedBoard;

            }, cancellationToken);

        }

        public List<int> MakeRandomEmpty(List<int> numbers, int count)
        {
            var randomIndexes = Enumerable.Range(0, numbers.Count).OrderBy(x => _randomCreator.Next()).Take(count).ToList();
            for (int i = 0; i < randomIndexes.Count; i++)
            {
                numbers[randomIndexes[i]] = _emptyCell;
            }

            return numbers;
        }

        public List<int> FillEmpty()
        {
            var tempNumbers = new List<int>();
            for (int i = 0; i < _gridSize * _gridSize; i++)
            {
                tempNumbers.Add(_emptyCell);
            }

            return tempNumbers;
        }

        private bool SetRandomNumber()
        {
            var possibleValues = IsRandomPossible();
            _randomBoard.Numbers[possibleValues[0]] = possibleValues[1];

            var isSolvedBoard = _sudokuSolverService.SolveGivenBoard(_randomBoard);
            if (!isSolvedBoard.IsSolved)
            {
                _randomBoard.Numbers[possibleValues[0]] = _emptyCell;
                if (SetRandomNumber())
                {
                    return true;
                }

                return false;
            }

            _solvedBoard = isSolvedBoard;
            return true;
        }


        private List<int> IsRandomPossible()
        {
            var possibleValues = new List<int>();
            possibleValues.Add(GenerateRandomIndex());
            possibleValues.Add(GenerateRandomNumber());


            bool isPossible = _sudokuSolverService.IsPossible(_randomBoard.Numbers, possibleValues[0], possibleValues[1]);
            if (isPossible)
            {
                return possibleValues;
            }

            return IsRandomPossible();
        }

        private int GenerateRandomIndex()
        {
            var emptyIndexes = new List<int>();
            for (int i = 0; i < _randomBoard.Numbers.Count; i++)
            {
                if (_randomBoard.Numbers[i] == _emptyCell)
                {
                    emptyIndexes.Add(i);
                }
            }

            int emptyIndex = _randomCreator.Next(emptyIndexes.Count - 1);

            return emptyIndexes[emptyIndex];
        }

        private int GenerateRandomNumber()
        {
            return _randomCreator.Next(1, 9);
        }
    }
}
