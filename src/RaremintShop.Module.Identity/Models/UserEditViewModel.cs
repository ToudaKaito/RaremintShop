using System.ComponentModel.DataAnnotations;

namespace RaremintShop.Module.Identity.Models
{
    public class UserEditViewModel
    {
        [Required(ErrorMessage = "IDは必須です。")]
        public required string Id { get; set; }

        [Required(ErrorMessage = "ユーザー名は必須です。")]
        [StringLength(50, ErrorMessage = "ユーザー名は50文字以内で入力してください。")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "メールアドレスは必須です。")]
        [EmailAddress(ErrorMessage = "有効なメールアドレスを入力してください。")]
        public string Email { get; set; }

        [Required(ErrorMessage = "ロールは必須です。")]
        public string Role { get; set; }

        public List<string>? AvailableRoles { get; set; }

        public bool IsActive { get; set; }
    }
}
