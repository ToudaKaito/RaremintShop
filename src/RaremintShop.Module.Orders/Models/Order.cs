using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RaremintShop.Module.Orders.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }

        [Required]
        public int UserID { get; set; } // ForeignKeyとしてUserIDのみを保持

        [Required]
        public DateTime OrderDate { get; set; }

        [Required, Column(TypeName = "decimal(10, 2)")]
        public decimal TotalAmount { get; set; }

        [Required, StringLength(255)]
        public string Status { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // デフォルトコンストラクタ(ORMが使用)
        private Order() { }

        //// パラメータレスコンストラクタ
        //public Order()
        //{
        //    OrderDate = DateTime.Now;
        //    Status = "Pending";
        //    CreatedAt = DateTime.Now;
        //    UpdatedAt = DateTime.Now;
        //}

        //// 明示的なコンストラクタ
        //public Order(
        //    int userId,
        //    User user,
        //    DateTime? orderDate = null,
        //    decimal totalAmount = 100.0m,
        //    string status = "Pending",
        //    DateTime? createdAt = null,
        //    DateTime? updatedAt = null)
        //{
        //    UserID = userId;
        //    OrderDate = orderDate ?? DateTime.Now;
        //    TotalAmount = totalAmount;
        //    Status = status;
        //    CreatedAt = createdAt ?? DateTime.Now;
        //    UpdatedAt = updatedAt ?? DateTime.Now;
        //}
    }
}
