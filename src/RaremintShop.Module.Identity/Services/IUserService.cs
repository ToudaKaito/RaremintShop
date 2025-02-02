using Microsoft.AspNetCore.Identity;
using RaremintShop.Module.Identity.Models;

namespace RaremintShop.Module.Identity.Services
{
    public interface IUserService
    {
        Task<IdentityResult> RegisterUserAsync(UserRegisterViewModel model);

        Task<List<UsersListViewModel>> GetAllUsersAsync();
    }
}

