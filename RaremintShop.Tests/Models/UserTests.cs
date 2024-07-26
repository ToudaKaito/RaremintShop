using System;
using RaremintShop.Models;
using Xunit;

namespace RaremintShop.Tests.Models
{
    /// <summary>
    /// Userモデルのユニットテストクラス
    /// </summary>
    public class UserTests
    {
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
            var user = new User
            {
                UserName = userName,
                Email = email,
                Password = password,
                CreatedAt = createdAt,
                UpdatedAt = updatedAt
            };

            // Assert
            Assert.Equal(userName, user.UserName);
            Assert.Equal(email, user.Email);
            Assert.Equal(password, user.Password);
            Assert.Equal(createdAt, user.CreatedAt);
            Assert.Equal(updatedAt, user.UpdatedAt);
        }

        [Fact]
        public void CanUpdateUserDetails()
        {
            // Arrange
            var user = new User
            {
                UserName = "OldUser",
                Email = "olduser@example.com",
                Password = "oldpassword",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

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
            Assert.Equal(newUserName, user.UserName);
            Assert.Equal(newEmail, user.Email);
            Assert.Equal(newPassword, user.Password);
            Assert.Equal(newUpdatedAt, user.UpdatedAt);
        }
    }
}
