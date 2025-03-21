using System.Collections.Generic;

namespace RaremintShop.Module.Identity.Models
{
    /// <summary>
    /// ユーザー管理のためのビューモデル
    /// </summary>
    public class UserManagementViewModel
    {
        /// <summary>ユーザーID</summary>
        public string Id { get; set; } = string.Empty;

        /// <summary>ユーザー名</summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>ユーザーのメールアドレス</summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>ユーザーのロール</summary>
        public string Role { get; set; } = string.Empty;
    }
}