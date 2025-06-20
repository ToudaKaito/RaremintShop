﻿namespace RaremintShop.WebHost.Models
{
    /// <summary>
    /// ユーザー管理のためのビューモデル
    /// </summary>
    public class UserManagementViewModel
    {
        public string Id { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}