using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RaremintShop.Models
{
    public class OrderDetail
    {
        [Required]
        public int OrderID {  get; set; }

        [Required]
        public int ProductID { get; set; }

        [Required]
        public int Quantity {  get; set; }

        [Required, Column(TypeName = "decimal(10, 2)")]
        public decimal Price {  get; set; }

        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;

        [ForeignKey("OrderID")]
        public Order Order { get; set; }

        [ForeignKey("ProductID")]
        public Product Product { get; set; }

        // パラメータレスコンストラクタ
        public OrderDetail()
        {
            Order = new Order();     // TODO:Orderの初期化について検討する
            Product = new Product(); // TODO:Productの初期化について検討する
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }

        // 明示的なコンストラクタ
        public OrderDetail(
            int orderId,
            Order order,
            int productId,
            Product product,
            int quantity =1,
            decimal price =10.0m,
            DateTime? createdAt = null, 
            DateTime? updatedAt = null)
        {
            OrderID = orderId;
            Order = order ?? throw new ArgumentNullException(nameof(order));
            ProductID = productId;
            Product = product ?? throw new ArgumentNullException(nameof(product));
            Quantity = quantity;
            Price = price;
            CreatedAt = createdAt ?? DateTime.Now;
            UpdatedAt = updatedAt ?? DateTime.Now;
        }

    }
}
