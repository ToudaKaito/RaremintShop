using Microsoft.AspNetCore.Identity;
using RaremintShop.Module.Identity.Models;

namespace RaremintShop.Module.Identity.Services
{
    /// <summary>
    /// ユーザー管理、認証、およびロール管理のためのメソッドを提供します。
    /// </summary>
    public interface IUserService
    {
        // ユーザー管理
        Task<IdentityResult> RegisterUserAsync(UserRegisterViewModel model);
        Task<SignInResult> LoginAsync(UserLoginViewModel model);
        Task LogoutAsync();
        Task<IdentityResult> DeleteUserAsync(IdentityUser user);
        Task<IdentityResult> UpdateUserAsync(UserEditViewModel model);

        // ユーザー情報取得
        Task<List<UserManagementViewModel>> GetAllUsersAsync();
        Task<IdentityUser?> GetByEmailAsync(string email);
        Task<IdentityUser?> GetByIdAsync(string id);
        Task<UserEditViewModel> GetByIdForEditAsync(string id);

        // ロール管理
        Task<IList<string>> GetRolesAsync(IdentityUser user);
        Task<List<IdentityRole>> GetAllRolesAsync();
    }
}

