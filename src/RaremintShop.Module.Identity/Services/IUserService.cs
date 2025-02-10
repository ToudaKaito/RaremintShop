using Microsoft.AspNetCore.Identity;
using RaremintShop.Module.Identity.Models;

namespace RaremintShop.Module.Identity.Services
{
    public interface IUserService
    {
        Task<IdentityResult> RegisterUserAsync(UserRegisterViewModel model);

        Task<SignInResult> LoginAsync(UserLoginViewModel model);

        Task LogoutAsync();

        Task<List<UsersListViewModel>> GetAllUsersAsync();

        Task<IdentityUser> GetByEmailAsync(string email);

        Task<IList<string>> GetRolesAsync(IdentityUser user);
    }
}

