using RaremintShop.Core.DTOs;

namespace RaremintShop.Module.Identity.Services
{
    /// <summary>
    /// ユーザー管理、認証、およびロール管理のためのメソッドを提供します。
    /// </summary>
    public interface IUserService
    {
        // ユーザー管理
        Task RegisterUserAsync(UserRegisterDto dto);
        Task<string> LoginAsync(UserLoginDto dto);
        Task LogoutAsync();
        Task<IdentityResult> DeleteUserAsync(IdentityUser user);
        Task<IdentityResult> UpdateUserAsync(UserEditViewModel model);

        // ユーザー情報取得
        Task<List<UserManagementViewModel>> GetAllUsersAsync();
        Task<> GetByEmailAsync(string email);
        Task<IdentityUser?> GetByIdAsync(string id);
        Task<UserEditViewModel> GetByIdForEditAsync(string id);

        // ロール管理
        Task<IList<string>> GetRolesAsync(string id);
        Task<List<IdentityRole>> GetAllRolesAsync();
    }
}

