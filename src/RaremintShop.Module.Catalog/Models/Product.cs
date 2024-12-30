using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RaremintShop.Module.Catalog.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        [Required, StringLength(255)]
        public string ProductName { get; set; }

        [Required, Column(TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; }

        [Required]
        public int Stock { get; set; }

        public string? Description { get; set; }

        [StringLength(255)]
        public string? Category { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // デフォルトコンストラクタ(ORMが使用)
        private Product() { }

        //// パラメータレスコンストラクタ
        //public Product()
        //{
        //    ProductName = string.Empty;
        //    Description = string.Empty;
        //    Category = string.Empty;
        //    CreatedAt = DateTime.Now;
        //    UpdatedAt = DateTime.Now;
        //}

        //// 明示的なコンストラクタ
        //public Product(
        //    string productName = "TestProduct",
        //    decimal price = 10.0m,
        //    int stock = 100,
        //    string description = "Description",
        //    string category = "General",
        //    DateTime? createdAt = null,
        //    DateTime? updatedAt = null)
        //{
        //    ProductName = productName;
        //    Price = price;
        //    Stock = stock;
        //    Description = description;
        //    Category = category;
        //    CreatedAt = createdAt ?? DateTime.Now;
        //    UpdatedAt = updatedAt ?? DateTime.Now;
        //}
    }
}
