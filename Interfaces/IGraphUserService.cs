using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace api.Interfaces
{
    public interface IGraphUserService
    {
        Task<Microsoft.Graph.User> GetUserByObjectId(string objectId);
        Task<Microsoft.Graph.GraphServiceUsersCollectionPage> ListUsers();
        Task<string> GetObjectIdFromClaims(IEnumerable<Claim> claims);
    }
}
