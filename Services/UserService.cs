using Microsoft.AspNetCore.Identity;
using ProcessRUsTasks.Exceptions;
using ProcessRUsTasks.Models;
using ProcessRUsTasks.Services.Interfaces;
using System.Runtime.InteropServices;

namespace ProcessRUsTasks.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task AddUserRoleAsync(string userId, string role)
        {
            ApplicationUser user = await GetUserById(userId);
            await _userManager.AddToRoleAsync(user, role);
        }

        public async Task CreateUser(ApplicationUser user, string password)
        {
            if (await _userManager.FindByEmailAsync(user.Email) != null)
                throw new BadRequestException("User with this email is already registered");
            var result = await _userManager.CreateAsync(user, password);            
        }

        public  async Task<ApplicationUser> GetUserByEmail(string email)
        {
           return await _userManager.FindByEmailAsync(email);
        }

        public async Task<ApplicationUser> GetUserById(string userId)
        {
            ApplicationUser applicationUser = await _userManager.FindByIdAsync(userId);
            if (applicationUser == null)
                throw new BadRequestException("User Not Found");
            else
                return applicationUser;
        }

        public async Task<IList<string>> GetUserRoles(ApplicationUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }
    }
}
