using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using api;
using api.Entities;
using api.Interfaces;
using api.Models;
using api.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace api.Tests
{
    [TestClass]
    public class SudokuGeneratorTest
    {
        private readonly SudokuGeneratorService _sudokuGeneratorService;
        public SudokuGeneratorTest()
        {
            _sudokuGeneratorService = new SudokuGeneratorService(new SudokuSolverService());
        }
        
        [TestMethod]
        public void IsZeroCountCorrect()
        {
            for (int i = 1; i < 80; i++)
            {
                var result = _sudokuGeneratorService.GetNewGame(i).Result;
                var zeroCount = result.Numbers.Count(x => x == 0);
                Assert.AreEqual(i, zeroCount);
            }
        }

        [TestMethod]
        public void IsFilledNumbersCorrect()
        {
            for (int i = 1; i < 80; i++)
            {
                var result = _sudokuGeneratorService.GetNewGame(i).Result;
                var filledCount = result.Numbers.Count(x => x != 0);
                Assert.AreEqual(81 - i, filledCount);
            }
        }

        [TestMethod]
        public void IsModelCorrect()
        {
            var result = _sudokuGeneratorService.GetNewGame(10).Result;
            Assert.IsInstanceOfType(result, typeof(Board));
        }

        [TestMethod]
        [DataRow(new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, })]
        public void IsRandomEmptyCountCorrect(int[] array)
        {
            for (int i = 0; i < 81; i++)
            {
                var result = _sudokuGeneratorService.MakeRandomEmpty(array.ToList(), i);
                var zeroCount = result.Count(x => x == 0);
                Assert.AreEqual(i, zeroCount);
            }
        }
    }
}
