using Microsoft.AspNetCore.Identity;

namespace RaremintShop.Module.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;

        // コンストラクタで UserManager を注入
        public UserService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        // ユーザーIDでユーザーを取得
        public async Task<IdentityUser?> GetUserByIdAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentException("User ID cannot be null or empty.", nameof(userId));

            return await _userManager.FindByIdAsync(userId);
        }

        // ユーザー名でユーザーを取得
        public async Task<IdentityUser?> GetUserByNameAsync(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentException("User name cannot be null or empty.", nameof(userName));

            return await _userManager.FindByNameAsync(userName);
        }

        // 新しいユーザーを作成
        public async Task<bool> CreateUserAsync(IdentityUser user, string password)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be null or empty.", nameof(password));

            var result = await _userManager.CreateAsync(user, password);
            return result.Succeeded;
        }

        // ユーザーを削除
        public async Task<bool> DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;

            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }
    }
}
