using RaremintShop.Module.Core.Models;

namespace RaremintShop.Module.Core.Services
{
    /// <summary>
    /// ユーザーに関するビジネスロジックを提供するサービスインターフェース
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// ユーザーIDに基づいてユーザーを非同期的に取得します。
        /// </summary>
        /// <param name="userId">ユーザーのID</param>
        /// <returns>ユーザーエンティティ</returns>
        Task<User> GetUserByIdAsync(int userId);

        /// <summary>
        /// メールアドレスに基づいてユーザーを非同期的に取得します。
        /// </summary>
        /// <param name="email">ユーザーのメールアドレス</param>
        /// <returns>ユーザーエンティティ</returns>
        Task<User> GetUserByEmailAsync(string email);

        /// <summary>
        /// 新しいユーザーを非同期的に追加します。
        /// </summary>
        /// <param name="user">追加するユーザーエンティティ</param>
        Task AddUserAsync(User user);

        /// <summary>
        /// ユーザー情報を非同期的に更新します。
        /// </summary>
        /// <param name="user">更新するユーザーエンティティ</param>
        Task UpdateUserAsync(User user);

        /// <summary>
        /// ユーザーを非同期的に削除します。
        /// </summary>
        /// <param name="userId">削除するユーザーのID</param>
        //void DeleteUser(int userId);
    }
}
