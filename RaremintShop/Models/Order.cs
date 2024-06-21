using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RaremintShop.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required, Column(TypeName = "decimal(10, 2)")]
        public decimal TotalAmount { get; set; }

        [Required, StringLength(255)]
        public string Status { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }

        // パラメータレスコンストラクタ
        public Order()
        {
            User = new User();
            OrderDate = DateTime.Now;
            Status = "Pending";
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            OrderDetails = new List<OrderDetail>();
        }

        // 明示的なコンストラクタ
        public Order(int userId, User user, decimal totalAmount)
        {
            UserID = userId;
            User = user ?? throw new ArgumentNullException(nameof(user));
            TotalAmount = totalAmount;
            OrderDate = DateTime.Now;
            Status = "Pending";
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            OrderDetails = new List<OrderDetail>();
        }
    }
}
