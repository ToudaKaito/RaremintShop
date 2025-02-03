using System.ComponentModel.DataAnnotations;

namespace RaremintShop.Module.Identity.Models
{
    public class UserLoginViewModel
    {
        [Required(ErrorMessage = "メールアドレスは必須です。")]
        [EmailAddress(ErrorMessage = "有効なメールアドレスを入力してください。")]
        public string Email { get; set; }

        [Required(ErrorMessage = "パスワードは必須です。")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "パスワードは少なくとも{2}文字以上である必要があります。", MinimumLength = 8)]
        public string Password { get; set; }
    }
}
