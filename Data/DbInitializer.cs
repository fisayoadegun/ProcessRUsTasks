using Microsoft.AspNetCore.Identity;
using ProcessRUsTasks.Helpers;
using ProcessRUsTasks.Models;

namespace ProcessRUsTasks.Data
{
    public class DbInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        ApplicationUser admin;
        ApplicationUser frontOffice;
        ApplicationUser backOffice;

        public DbInitializer(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        
        // Initializes persistent storage with the 3 users: FrontOffice, BackOffice and Admin. 
        public async void Initialize()
        {
            await CreateUsers();
        }

        public async Task CreateUsers()
        {
            //Creating the roles
            if (!await _roleManager.RoleExistsAsync(RoleTypes.Admin))
                await _roleManager.CreateAsync(new IdentityRole(RoleTypes.Admin));
            if (!await _roleManager.RoleExistsAsync(RoleTypes.BackOffice))
                await _roleManager.CreateAsync(new IdentityRole(RoleTypes.BackOffice));

            //Creating the Identity Users
            var frontOfficeEmail = "frontoffice@gmail.com";
            var backOfficeEmail = "backoffice@gmail.com";
            var adminEmail = "admin@gmail.com";

            frontOffice = await _userManager.FindByEmailAsync(frontOfficeEmail);
            backOffice = await _userManager.FindByEmailAsync(backOfficeEmail);
            admin = await _userManager.FindByEmailAsync(adminEmail);
            
            if(frontOffice == null)
            {
                frontOffice = new ApplicationUser() { UserName = "FrontOffice", Email = frontOfficeEmail };
                await _userManager.CreateAsync(user: frontOffice, password: "Frontoffice@123");
            }
            if (backOffice == null)
            {
                backOffice = new ApplicationUser() { UserName = "BackOffice", Email = backOfficeEmail };
                await _userManager.CreateAsync(user: backOffice, password: "Backoffice@123");
            }
            if (admin == null)
            {
                admin = new ApplicationUser() { UserName = "Admin", Email = adminEmail };
                await _userManager.CreateAsync(user: admin, password: "Admin@123");
            }

            //Adding Admin and Backoffice Users to their respective roles
            if (!await _userManager.IsInRoleAsync(admin, RoleTypes.Admin))
                await _userManager.AddToRoleAsync(admin, RoleTypes.Admin);

            if (!await _userManager.IsInRoleAsync(backOffice, RoleTypes.BackOffice))
                await _userManager.AddToRoleAsync(backOffice, RoleTypes.BackOffice);
        }
    }
}
