using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RaremintShop.Module.Core.Data;
using RaremintShop.Module.Core.Models;
using RaremintShop.Module.Core.Repositories;


namespace RaremintShop.Infrastructure.Repositories
{
    /// <summary>
    /// ユーザー情報を管理するリポジトリの実装クラス
    /// </summary>
    public class UserRepository : BaseRepository<CoreDbContext>, IUserRepository
    {
        /// <summary>
        /// コンストラクタ。データベースコンテキストとロガーを受け取ります。
        /// </summary>
        /// <param name="context">データベースコンテキスト</param>
        /// <param name="logger">ロガーインスタンス</param>
        public UserRepository(CoreDbContext context, ILogger<UserRepository> logger)
        : base(context, logger)
        {
        }

        /// <summary>
        /// ユーザーIDに基づいてユーザーを非同期的に取得します。
        /// </summary>
        /// <param name="userId">取得するユーザーのID</param>
        /// <returns>ユーザーエンティティ</returns>
        public async Task<User> GetUserByIdAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                throw new KeyNotFoundException($"ユーザーID {userId} のユーザーが見つかりませんでした。");
            }
            return user;
        }

        /// <summary>
        /// メールアドレスに基づいてユーザーを非同期的に取得します。
        /// </summary>
        /// <param name="email">取得するユーザーのメールアドレス</param>
        /// <returns>ユーザーエンティティ</returns>
        public async Task<User> GetUserByEmailAsync(string email)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                throw new KeyNotFoundException($"メールアドレス '{email}' のユーザーが見つかりませんでした。");
            }
            return user;
        }

        /// <summary>
        /// 新しいユーザーを非同期的に追加します。
        /// </summary>
        /// <param name="user">追加するユーザーエンティティ</param>
        public async Task AddUserAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            _context.Users.Add(user);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                HandleException(ex, "ユーザーの追加", user.UserID);
            }
        }

        /// <summary>
        /// 既存のユーザー情報を非同期的に更新します。
        /// </summary>
        /// <param name="user">更新するユーザーエンティティ</param>
        public async Task UpdateUserAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            _context.Users.Update(user);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                HandleException(ex, "ユーザーの更新", user.UserID);
            }
        }
    }
}
