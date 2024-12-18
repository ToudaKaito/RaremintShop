using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace RaremintShop.Module.Identity.Services
{
    public interface IUserService
    {
        // ユーザーIDでユーザーを取得
        Task<IdentityUser?> GetUserByIdAsync(string userId);

        // ユーザー名でユーザーを取得
        Task<IdentityUser?> GetUserByNameAsync(string userName);

        // 新しいユーザーを作成
        Task<bool> CreateUserAsync(IdentityUser user, string password);

        // ユーザーの削除
        Task<bool> DeleteUserAsync(string userId);
    }
}
