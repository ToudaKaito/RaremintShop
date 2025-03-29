using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RaremintShop.Module.Catalog.Models
{
    /// <summary>
    /// 商品情報を管理するエンティティクラス。
    /// </summary>
    public class Product
    {
        /// <summary>
        /// 主キー。
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 商品名。
        /// </summary>
        [Required]
        [MaxLength(100)] // 100文字まで
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 価格。
        /// </summary>
        [Required]
        [Column(TypeName = "decimal(18,2)")] // 小数点2桁まで
        public decimal Price { get; set; }

        /// <summary>
        /// カテゴリの外部キー。
        /// </summary>
        [Required]
        public int CategoryId { get; set; }

        /// <summary>
        /// カテゴリのナビゲーションプロパティ。
        /// </summary>
        [ForeignKey("CategoryId")]
        public Category Category { get; set; } = null!;

        /// <summary>
        /// 商品説明。
        /// </summary>
        [Required]
        [Column(TypeName = "TEXT")] // 長い説明文も格納できるように
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// 在庫数（デフォルト0）。
        /// </summary>
        [Required]
        public int Stock { get; set; } = 0;

        /// <summary>
        /// 公開フラグ（デフォルト公開）。
        /// </summary>
        [Required]
        public bool IsPublished { get; set; } = true;

        /// <summary>
        /// 作成日時。
        /// </summary>
        [Required]
        [Column(TypeName = "timestamp")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// 更新日時（NULL許可）。
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// 画像リスト。
        /// </summary>
        public List<ProductImage> Images { get; set; } = new();
    }
}