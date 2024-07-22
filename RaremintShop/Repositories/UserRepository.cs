using System.Linq;
using RaremintShop.Data;
using RaremintShop.Models;

namespace RaremintShop.Repositories
{
    /// <summary>
    /// ユーザー情報を管理するリポジトリの実装クラス
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// コンストラクタ。データベースコンテキストを受け取ります。
        /// </summary>
        /// <param name="context">データベースコンテキスト</param>
        public UserRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// ユーザーIDに基づいてユーザーを取得します。
        /// </summary>
        /// <param name="userId">取得するユーザーのID</param>
        /// <returns>ユーザーエンティティ</returns>
        public User GetUserById(int userId)
        {
            var user = _context.Users.Find(userId);
            if (user == null)
            {
                throw new NullReferenceException($"User with ID {userId} not found.");
            }
            return user;
        }

        /// <summary>
        /// メールアドレスに基づいてユーザーを取得します。
        /// </summary>
        /// <param name="email">取得するユーザーのメールアドレス</param>
        /// <returns>ユーザーエンティティ</returns>
        public User GetUserByEmail(string email)
        {
            var user = _context.Users.SingleOrDefault(u => u.Email == email);
            if (user == null)
            {
                throw new NullReferenceException($"User with email {email} not found.");
            }
            return user;
        }

        /// <summary>
        /// 新しいユーザーを追加します。
        /// </summary>
        /// <param name="user">追加するユーザーエンティティ</param>
        public void AddUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        /// <summary>
        /// 既存のユーザー情報を更新します。
        /// </summary>
        /// <param name="user">更新するユーザーエンティティ</param>
        public void UpdateUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            _context.Users.Update(user);
            _context.SaveChanges();
        }
    }
}
