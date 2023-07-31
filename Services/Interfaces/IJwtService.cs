using Microsoft.AspNetCore.Identity;
using ProcessRUsTasks.Models;
using System.Security.Claims;

namespace ProcessRUsTasks.Services.Interfaces
{
    public interface IJwtService
    {
        DateTime ExpirationTime { get; }
        string GenerateJwtAccessToken(IEnumerable<Claim> claims);
        Task<Claim[]> GetClaimsAsync(ApplicationUser userInfo);
    }
}
