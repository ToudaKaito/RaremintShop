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
            User = new User(); // TODO:Userの初期化について検討する
            OrderDate = DateTime.Now;
            Status = "Pending";
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            OrderDetails = new List<OrderDetail>();
        }

        // 明示的なコンストラクタ
        public Order(
            int userId,
            User user,
            DateTime? orderDate = null,
            decimal totalAmount = 100.0m,
            string status = "Pending",
            DateTime? createdAt = null,
            DateTime? updatedAt = null)
        {
            UserID = userId;
            User = user ?? throw new ArgumentNullException(nameof(user));
            OrderDate = orderDate ?? DateTime.Now;
            TotalAmount = totalAmount;
            Status = status;
            CreatedAt = createdAt ?? DateTime.Now;
            UpdatedAt = updatedAt ?? DateTime.Now;
            OrderDetails = new List<OrderDetail>();
        }
    }
}
