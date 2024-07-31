using System;
using Microsoft.EntityFrameworkCore;
using RaremintShop.Data;
using RaremintShop.Models;
using RaremintShop.Repositories;

namespace RaremintShop.Tests.Helpers
{
    /// <summary>
    /// ユーザー関連のテストヘルパークラス。
    /// </summary>
    public static class UserTestHelper
    {
        /// <summary>
        /// 新しい DbContextOptions を作成し、一意の InMemory データベースを使用する。
        /// </summary>
        /// <returns>新しい DbContextOptions。</returns>
        public static DbContextOptions<ApplicationDbContext> CreateNewContextOptions()
        {
            return new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }


        /// <summary>
        /// テスト用のユーザーオブジェクトを作成する。
        /// </summary>
        /// <param name="id">ユーザーID。</param>
        /// <param name="userName">ユーザー名。</param>
        /// <param name="email">メールアドレス。</param>
        /// <param name="password">パスワード。</param>
        /// <param name="createdAt">作成日時（オプション）。</param>
        /// <param name="updatedAt">更新日時（オプション）。</param>
        /// <returns>新しいユーザーオブジェクト。</returns>
        public static User CreateTestUser(int id = 1, string userName = "TestUser", string email = "test@example.com", string password = "password123", DateTime? createdAt = null, DateTime? updatedAt = null)
        {
            return new User
            {
                UserID = id,
                UserName = userName,
                Email = email,
                Password = password,
                CreatedAt = createdAt ?? DateTime.Now,
                UpdatedAt = updatedAt ?? DateTime.Now
            };
        }

        /// <summary>
        /// 空のデータベースを持つ UserRepository を作成する。
        /// </summary>
        /// <param name="context">作成された ApplicationDbContext。</param>
        /// <returns>新しい UserRepository。</returns>
        public static UserRepository CreateUserRepository(out ApplicationDbContext context)
        {
            var options = CreateNewContextOptions();
            context = new ApplicationDbContext(options);
            return new UserRepository(context);
        }
    }
}
