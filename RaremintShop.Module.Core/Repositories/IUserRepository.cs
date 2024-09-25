using RaremintShop.Module.Core.Models;

namespace RaremintShop.Module.Core.Repositories
{
    /// <summary>
    /// ユーザー情報を管理するためのリポジトリインターフェース
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// ユーザーIDに基づいてユーザーを非同期的に取得します。
        /// </summary>
        /// <param name="userId">取得するユーザーのID</param>
        /// <returns>ユーザーエンティティ</returns>
        Task<User> GetUserByIdAsync(int userId);

        /// <summary>
        /// メールアドレスに基づいてユーザーを非同期的に取得します。
        /// </summary>
        /// <param name="email">取得するユーザーのメールアドレス</param>
        /// <returns>ユーザーエンティティ</returns>
        Task<User> GetUserByEmailAsync(string email);

        /// <summary>
        /// 新しいユーザーを非同期的に追加します。
        /// </summary>
        /// <param name="user">追加するユーザーエンティティ</param>
        Task AddUserAsync(User user);

        /// <summary>
        /// 既存のユーザー情報を非同期的に更新します。
        /// </summary>
        /// <param name="user">更新するユーザーエンティティ</param>
        Task UpdateUserAsync(User user);
    }
}
