using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using api.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace api.Tests
{
    [TestClass]
    public class GraphUserTest
    {
        private readonly GraphUserService _graphUserService;
        public GraphUserTest()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(@"appsettings.Development.json",false,false)
                .Build();

            _graphUserService = new GraphUserService(configuration);
        }

        [TestMethod]
        [DataRow("3a1e03e6-c46c-4f82-b175-9284a18546f7")]
        [ExpectedException(typeof(ArgumentNullException), "Expected exception is wrong!")]
        public async Task IsThrowsExceptionIfUserNotExist(string objectId)
        {
            var guid = Guid.Parse(objectId);
            var result = await _graphUserService.GetUserByObjectId(objectId);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Expected exception is wrong!")]
        public async Task IsThrowsExceptionIfUserIsNull()
        {
            var result = await _graphUserService.GetUserByObjectId(null);
        }

    }
}
