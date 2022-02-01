using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using api.Models;
using api.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace api.Tests
{
    [TestClass]
    public class SudokuSolverTest
    {
        private readonly SudokuSolverService _sudokuSolverService;
        private readonly SudokuGeneratorService _sudokuGeneratorService;

        public SudokuSolverTest()
        {
            _sudokuSolverService = new SudokuSolverService();
            _sudokuGeneratorService = new SudokuGeneratorService(_sudokuSolverService);
        }

        [TestMethod]
        [DataRow(0,9, false)]
        [DataRow(0, 8, true)]
        [DataRow(80, 2, true)]
        [DataRow(15, 1, true)]
        [DataRow(3, 6, false)]
        public void IsNumberPossibleForGivenIndex(int index, int number, bool expected)
        {
            var array = new List<int>()
            {
                0, 1, 2, 7, 5, 3, 6, 4, 9,
                9, 4, 3, 6, 8, 2, 0, 7, 5,
                6, 7, 5, 4, 9, 1, 2, 8, 3,
                1, 5, 4, 2, 3, 7, 8, 9, 6,
                3, 6, 9, 8, 4, 5, 7, 2, 1,
                2, 8, 7, 1, 6, 9, 5, 3, 4,
                5, 2, 1, 9, 7, 4, 3, 6, 8,
                4, 3, 8, 5, 2, 6, 9, 1, 7,
                7, 9, 6, 3, 1, 8, 4, 5, 0
            };
            
            var result = _sudokuSolverService.IsPossible(array, index, number);
            Assert.AreEqual(expected, result, $"Normally it is not possible with i:{index} n:{number}");
        }

        [TestMethod]
        public void CompareSolvedBoard()
        {
            var board = new Board();
            board.Numbers = new List<int>()
            {
                0, 1, 2, 7, 5, 3, 6, 4, 9,
                9, 4, 3, 6, 8, 2, 0, 7, 5,
                6, 7, 5, 4, 9, 1, 2, 8, 3,
                1, 5, 4, 2, 3, 0, 8, 9, 6,
                3, 6, 9, 8, 4, 5, 7, 2, 1,
                2, 8, 7, 1, 6, 9, 5, 3, 4,
                5, 2, 1, 9, 7, 4, 3, 6, 8,
                4, 3, 8, 5, 2, 6, 9, 1, 7,
                7, 9, 6, 3, 1, 8, 4, 5, 0
            };

            var solvedNumbers = new List<int>()
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
            };


            var result = _sudokuSolverService.SolveGivenBoard(board).Numbers;

            CollectionAssert.AreEqual(solvedNumbers, result, "Solved puzzle is not correct");
        }

        [TestMethod]
        [DataRow(5)]
        [DataRow(10)]
        [DataRow(25)]
        [DataRow(6)]
        [DataRow(49)]
        [DataRow(78)]
        [DataRow(76)]
        [DataRow(33)]
        public void IsFilledAfterSolve(int difficulty)
        {
            var board = _sudokuGeneratorService.GetNewGame(difficulty).Result;

            var solvedBoard = _sudokuSolverService.SolveGivenBoard(board);

            var isFilled = solvedBoard.Numbers.Any(x=> x == 0);

            Assert.AreEqual(false, isFilled, "Could not solve the given board");
        }


        [TestMethod]
        public void IsValidBoard()
        {
            var solvedBoard = new Board();
            solvedBoard.Numbers = new List<int>()
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
            };

            var notSolvedBoard = new Board();
            notSolvedBoard.Numbers = new List<int>()
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
            };



            var result = _sudokuSolverService.ValidateBoard(solvedBoard).Result;
            Assert.AreEqual(true, result.IsValid, "Board is not valid");


        }


    }
}
