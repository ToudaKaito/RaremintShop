using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RaremintShop.Core.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }  // 主キー

        [Required(ErrorMessage = "カテゴリ名は必須です。")]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;  // カテゴリ名

        [Required]
        [Column(TypeName = "timestamp")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;  // 作成日時

        public DateTime? UpdatedAt { get; set; }  // 更新日時（NULL許可）

        public List<Product> Products { get; set; } = new();  // 商品リスト
    }
}
