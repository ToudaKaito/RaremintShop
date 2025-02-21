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
        Task<IdentityUser> GetByIdAsync(string id);
        Task<UserEditViewModel> GetByIdForEditAsync(string id);
        Task<IList<string>> GetRolesAsync(IdentityUser user);
        Task<IdentityResult> DeleteUserAsync(IdentityUser user);
        Task<List<IdentityRole>> GetAllRolesAsync();
        Task<IdentityResult> UpdateUserAsync(UserEditViewModel model);
    }
}

