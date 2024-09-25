using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RaremintShop.Module.Orders.Models
{
    public class OrderDetail
    {
        [Required]
        public int OrderID { get; set; }

        [Required]
        public int ProductID { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required, Column(TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; }

        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;

        // デフォルトコンストラクタ(ORMが使用)
        private OrderDetail() { }

        //// パラメータレスコンストラクタ
        //public OrderDetail()
        //{
        //    CreatedAt = DateTime.Now;
        //    UpdatedAt = DateTime.Now;
        //}

        //// 明示的なコンストラクタ
        //public OrderDetail(
        //    int orderId,
        //    Order order,
        //    int productId,
        //    int quantity = 1,
        //    decimal price = 10.0m,
        //    DateTime? createdAt = null,
        //    DateTime? updatedAt = null)
        //{
        //    OrderID = orderId;
        //    ProductID = productId;
        //    Quantity = quantity;
        //    Price = price;
        //    CreatedAt = createdAt ?? DateTime.Now;
        //    UpdatedAt = updatedAt ?? DateTime.Now;
        //}

    }
}
