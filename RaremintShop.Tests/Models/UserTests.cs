using System;
using RaremintShop.Models;
using RaremintShop.Tests.Helpers;
using Xunit;

namespace RaremintShop.Tests.Models
{
    /// <summary>
    /// Userモデルのユニットテストクラス
    /// </summary>
    public class UserTests
    {
        /// <summary>
        /// 有効なデータでユーザーを作成できることをテストします。
        /// </summary>
        [Fact]
        public void CanCreateUser_WithValidData()
        {
            // Arrange
            var userName = "TestUser";
            var email = "testuser@example.com";
            var password = "securepassword";
            var createdAt = DateTime.Now;
            var updatedAt = DateTime.Now;

            // Act
            var user = UserTestHelper.CreateTestUser(userName: userName, email: email, password: password, createdAt: createdAt, updatedAt: updatedAt);

            // Assert
            Assert.NotNull(user);
            Assert.Equal(userName, user.UserName);
            Assert.Equal(email, user.Email);
            Assert.Equal(password, user.Password);
            Assert.Equal(createdAt, user.CreatedAt);
            Assert.Equal(updatedAt, user.UpdatedAt);
        }

        /// <summary>
        /// ユーザーの詳細を更新できることをテストします。
        /// </summary>
        [Fact]
        public void CanUpdateUserDetails()
        {
            // Arrange
            var originalCreatedAt = DateTime.Now.AddDays(-1);
            var user = UserTestHelper.CreateTestUser(userName: "OldUser", email: "olduser@example.com", password: "oldpassword", createdAt: originalCreatedAt, updatedAt: DateTime.Now);

            var newUserName = "NewUser";
            var newEmail = "newuser@example.com";
            var newPassword = "newpassword";
            var newUpdatedAt = DateTime.Now;

            // Act
            user.UserName = newUserName;
            user.Email = newEmail;
            user.Password = newPassword;
            user.UpdatedAt = newUpdatedAt;

            // Assert
            Assert.NotNull(user);
            Assert.Equal(newUserName, user.UserName);
            Assert.Equal(newEmail, user.Email);
            Assert.Equal(newPassword, user.Password);
            Assert.Equal(originalCreatedAt, user.CreatedAt); 
            Assert.Equal(newUpdatedAt, user.UpdatedAt);
        }
    }
}
