using System.ComponentModel.DataAnnotations;

namespace RaremintShop.WebHost.Models
{
    public class CategoryEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "カテゴリ名は必須です。")]
        [MaxLength(50, ErrorMessage = "カテゴリ名は50文字以内で入力してください。")]
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
