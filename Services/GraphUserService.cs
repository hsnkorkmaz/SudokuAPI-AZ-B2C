using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using api.Interfaces;
using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph;

namespace api.Services
{
    public class GraphUserService : IGraphUserService
    {
        private readonly GraphServiceClient _graphClient;
        private readonly string[] _scopes = new[] { "https://graph.microsoft.com/.default" };

        public GraphUserService(IConfiguration configuration)
        {
            _graphClient = new GraphServiceClient(new ClientSecretCredential(configuration["AzureGraph:TenantId"],
                    configuration["AzureGraph:AppId"],
                    configuration["AzureGraph:ClientSecret"]),
                _scopes);
        }

        public async Task<Microsoft.Graph.User> GetUserByObjectId(string objectId)
        {
            if (objectId == null) throw new ArgumentNullException();

            try
            {
                var result = await _graphClient.Users[objectId]
                    .Request()
                    .Select(e => new
                    {
                        e.DisplayName,
                        e.Id,
                        e.Identities
                    })
                    .GetAsync();

                if (result != null)
                {
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException();
            }

            return new Microsoft.Graph.User();
        }

        public async Task<GraphServiceUsersCollectionPage> ListUsers()
        {
            var userList = new GraphServiceUsersCollectionPage();
            try
            {
                var users = await _graphClient.Users
                    .Request()
                    .Select(e => new
                    {
                        e.DisplayName,
                        e.Id,
                        e.Identities
                    })
                    .GetAsync();

                if (users != null)
                {
                    userList = (GraphServiceUsersCollectionPage) users;
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException();
            }

            return userList;


        }

        public async Task<string> GetObjectIdFromClaims(IEnumerable<Claim> claims)
        {
            //string[] scopeRequiredByApi = new string[] { "Data.Read", "Client.Read" };
            //HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);

            var objectId = claims.FirstOrDefault(x => x.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value;
            return objectId;
        }

    }
}
