namespace RaremintShop.Core.DTOs
{
    public class ProductImageDto
    {
        public int Id { get; set; }
        public string ImagePath { get; set; } = string.Empty; // Webでアクセスできるパス
        public int SortOrder { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
