using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RaremintShop.Models
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

        public ICollection<OrderDetail> OrderDetails { get; set; }

        // パラメータレスコンストラクタ
        public Product()
        {
            ProductName = string.Empty;
            Description = string.Empty;
            Category = string.Empty;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            OrderDetails = new List<OrderDetail>();
        }

        // 明示的なコンストラクタ
        public Product(string productName, decimal price, int stock)
        {
            ProductName = productName;
            Price = price;
            Stock = stock;
            Description = string.Empty;
            Category = string.Empty;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            OrderDetails = new List<OrderDetail>();
        }
    }
}
