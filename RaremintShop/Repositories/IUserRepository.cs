using RaremintShop.Models;

namespace RaremintShop.Repositories
{
    /// <summary>
    /// ユーザー情報を管理するためのリポジトリインターフェース
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// ユーザーIDに基づいてユーザーを取得します。
        /// </summary>
        /// <param name="userId">取得するユーザーのID</param>
        /// <returns>ユーザーエンティティ</returns>
        User GetUserById(int userId);

        /// <summary>
        /// メールアドレスに基づいてユーザーを取得します。
        /// </summary>
        /// <param name="email">取得するユーザーのメールアドレス</param>
        /// <returns>ユーザーエンティティ</returns>
        User GetUserByEmail(string email);

        /// <summary>
        /// 新しいユーザーを追加します。
        /// </summary>
        /// <param name="user">追加するユーザーエンティティ</param>
        void AddUser(User user);

        /// <summary>
        /// 既存のユーザー情報を更新します。
        /// </summary>
        /// <param name="user">更新するユーザーエンティティ</param>
        void UpdateUser(User user);
    }
}
