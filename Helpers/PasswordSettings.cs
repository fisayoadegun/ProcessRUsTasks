using Microsoft.AspNetCore.Identity;

namespace ProcessRUsTasks.Helpers
{
    public static class PasswordSettings
    {
        public static IdentityOptions GetPasswordSettings()
        {
            IdentityOptions options = new IdentityOptions();
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 0;
            return options;
        }
    }
}
