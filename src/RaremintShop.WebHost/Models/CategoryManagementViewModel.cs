﻿namespace RaremintShop.WebHost.Models
{
    public class CategoryManagementViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
