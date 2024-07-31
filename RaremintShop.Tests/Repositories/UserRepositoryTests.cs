using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RaremintShop.Data;
using RaremintShop.Models;
using RaremintShop.Repositories;
using RaremintShop.Tests.Helpers;
using Xunit;

namespace RaremintShop.Tests.Repositories
{
    /// <summary>
    /// UserRepositoryのユニットテストクラス
    /// </summary>
    public class UserRepositoryTests
    {
        private ApplicationDbContext _context = null!;
        private UserRepository _userRepository = null!;

        /// <summary>
        /// テストデータベースを初期化します。
        /// </summary>
        private void InitializeTestDatabase()
        {
            _userRepository = UserTestHelper.CreateUserRepository(out _context);
        }

        /// <summary>
        /// ユーザーIDによるユーザーの取得をテストします。
        /// </summary>
        [Fact]
        public void GetUserById_UserExists_ReturnsUser()
        {
            InitializeTestDatabase();
            var user = UserTestHelper.CreateTestUser();
            _context.Users.Add(user);
            _context.SaveChanges();

            var result = _userRepository.GetUserById(user.UserID);

            Assert.NotNull(result);
            Assert.Equal(user.UserID, result.UserID);
            Assert.Equal(user.UserName, result.UserName);
            Assert.Equal(user.Email, result.Email);
            Assert.Equal(user.Password, result.Password);
            Assert.Equal(user.CreatedAt, result.CreatedAt);
            Assert.Equal(user.UpdatedAt, result.UpdatedAt);
        }

        /// <summary>
        /// メールアドレスによるユーザーの取得をテストします。
        /// </summary>
        [Fact]
        public void GetUserByEmail_UserExists_ReturnsUser()
        {
            InitializeTestDatabase();
            var user = UserTestHelper.CreateTestUser(); 
            _context.Users.Add(user);
            _context.SaveChanges();

            var result = _userRepository.GetUserByEmail(user.Email);

            Assert.NotNull(result);
            Assert.Equal(user.UserID, result.UserID);
            Assert.Equal(user.UserName, result.UserName);
            Assert.Equal(user.Email, result.Email);
            Assert.Equal(user.Password, result.Password);
            Assert.Equal(user.CreatedAt, result.CreatedAt);
            Assert.Equal(user.UpdatedAt, result.UpdatedAt);
        }

        /// <summary>
        /// 有効なユーザーの追加をテストします。
        /// </summary>
        [Fact]
        public void AddUser_ValidUser_AddsUser()
        {
            InitializeTestDatabase();
            var user = UserTestHelper.CreateTestUser();

            _userRepository.AddUser(user);
            var result = _context.Users.Find(user.UserID);

            Assert.NotNull(result);
            Assert.Equal(user.UserID, result.UserID);
            Assert.Equal(user.UserName, result.UserName);
            Assert.Equal(user.Email, result.Email);
            Assert.Equal(user.Password, result.Password);
            Assert.Equal(user.CreatedAt, result.CreatedAt);
            Assert.Equal(user.UpdatedAt, result.UpdatedAt);
        }

        /// <summary>
        /// 有効なユーザーの更新をテストします。
        /// </summary>
        [Fact]
        public void UpdateUser_ValidUser_UpdatesUser()
        {
            InitializeTestDatabase();
            var originalCreatedAt = DateTime.Now.AddDays(-1);
            var user = UserTestHelper.CreateTestUser(createdAt: originalCreatedAt);
            _context.Users.Add(user);
            _context.SaveChanges();

            // 元のエンティティをデタッチ
            // デタッチすることで、同じキー値（UserID）を持つ別のエンティティ（updatedUser）をトラッキングできるようにします。
            // これにより、同じUserIDを持つエンティティが2つトラッキングされることで発生する競合エラーを防ぎます。
            _context.Entry(user).State = EntityState.Detached;


            var updatedUser = UserTestHelper.CreateTestUser(user.UserID, "UpdatedUser", "updateduser@example.com", "updatedpassword123", createdAt: originalCreatedAt);

            _userRepository.UpdateUser(updatedUser);
            var result = _context.Users.Find(updatedUser.UserID);

            Assert.NotNull(result);
            Assert.Equal(updatedUser.UserID, result.UserID);
            Assert.Equal(updatedUser.UserName, result.UserName);
            Assert.Equal(updatedUser.Email, result.Email);
            Assert.Equal(updatedUser.Password, result.Password);
            Assert.Equal(updatedUser.CreatedAt, result.CreatedAt);
            Assert.Equal(updatedUser.UpdatedAt, result.UpdatedAt);
        }
    }
}
