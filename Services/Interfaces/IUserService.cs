using Microsoft.AspNetCore.Identity;
using ProcessRUsTasks.Models;

namespace ProcessRUsTasks.Services.Interfaces
{
    public interface IUserService
    {
        Task CreateUser(ApplicationUser user, string password);

        Task<ApplicationUser> GetUserByEmail(string email);
        Task<IList<string>> GetUserRoles(ApplicationUser user);
        Task AddUserRoleAsync(string userId, string role);

        Task<ApplicationUser> GetUserById(string userId);
    }
}
