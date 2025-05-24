using RaremintShop.Core.DTOs;

namespace RaremintShop.Core.Interfaces.Services
{
    /// <summary>
    /// ユーザー管理、認証、およびロール管理のためのメソッドを提供します。
    /// </summary>
    public interface IUserService
    {
        // ユーザー管理
        Task RegisterUserAsync(UserDto userDto);
        Task<string> LoginAsync(UserDto userDto);
        Task LogoutAsync();
        Task DeleteUserAsync(string id);
        Task UpdateUserAsync(UserDto userDto);

        // ユーザー情報取得
        Task<List<UserDto>> GetAllUsersAsync();
        Task<UserDto> GetByIdAsync(string id);

        // ロール管理
        Task<IList<string>> GetRolesAsync(string id);
    }
}

