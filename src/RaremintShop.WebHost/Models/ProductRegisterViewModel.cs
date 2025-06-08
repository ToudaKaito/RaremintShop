using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace RaremintShop.WebHost.Models
{
    public class ProductRegisterViewModel
    {
        [Required]
        public int CategoryId { get; set; }

        public SelectList CategoryList { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Range(0, 999999.99)]
        public decimal Price { get; set; }

        [Required]
        public int Stock { get; set; }

        public bool IsPublished { get; set; } = true;

        // アップロードする画像（複数対応）
        public List<IFormFile> Images { get; set; } = new();
    }
}
