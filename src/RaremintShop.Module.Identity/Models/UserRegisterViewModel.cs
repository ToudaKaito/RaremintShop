using System.ComponentModel.DataAnnotations;

namespace RaremintShop.Module.Identity.Models
{
    public class UserRegisterViewModel
    {
        [Required(ErrorMessage = "メールアドレスは必須です。")]
        [EmailAddress(ErrorMessage = "有効なメールアドレスを入力してください。")]
        public string Email { get; set; }

        [Required(ErrorMessage = "パスワードは必須です。")]
        [StringLength(100, ErrorMessage = "パスワードは少なくとも{2}文字以上である必要があります。", MinimumLength = 8)]
        public string Password { get; set; }

        [Required(ErrorMessage = "確認用パスワードは必須です。")]
        [Compare("Password", ErrorMessage = "パスワードと確認用パスワードが一致しません。")]
        public string ComfirmPassword { get; set; }
    }
}
