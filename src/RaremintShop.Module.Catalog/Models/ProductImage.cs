using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RaremintShop.Module.Catalog.Models
{
    public class ProductImage
    {
        [Key]
        public int Id { get; set; }  // 主キー

        [Required]
        public int ProductId { get; set; }  // 外部キー（商品）

        [ForeignKey("ProductId")]
        public Product Product { get; set; } = null!;  // ナビゲーションプロパティ

        [Required]
        public string ImageUrl { get; set; } = string.Empty;  // 画像URL

        [Required]
        [Column(TypeName = "timestamp")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;  // 作成日時

        public DateTime? UpdatedAt { get; set; }  // 更新日時（NULL許可）
    }
}
