using System;
using System.ComponentModel.DataAnnotations;

namespace RaremintShop.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [Required, StringLength(255)]
        public string UserName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, StringLength(255)]
        public string Password { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // パラメータレスコンストラクタ
        public User()
        {
            // 開発・テスト中はこっちに変更するかも
            // UserName = "DefaultUser";
            // Email = "default@example.com";
            // Password = "default";

            UserName = string.Empty;
            Email = string.Empty;
            Password = string.Empty;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }

        // 明示的なコンストラクタ
        public User(string userName = "TestUser", string email = "test@example.com", string password = "password123", DateTime? createdAt = null, DateTime? updatedAt = null)
        {
            UserName = userName;
            Email = email;
            Password = password;
            CreatedAt = createdAt ?? DateTime.Now;
            UpdatedAt = updatedAt ?? DateTime.Now;
        }
    }
}
