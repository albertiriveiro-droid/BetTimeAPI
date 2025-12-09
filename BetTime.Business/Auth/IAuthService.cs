using System.Security.Claims;
using BetTime.Models;

namespace BetTime.Business;

    public interface IAuthService
    {
       
        public string GenerateJwtToken(User user);
        public bool HasAccessToResource(int requestedUserID, ClaimsPrincipal user);
    }