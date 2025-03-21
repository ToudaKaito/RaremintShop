using System.ComponentModel.DataAnnotations;

namespace RaremintShop.Module.Identity.Models
{
    /// <summary>
    /// ユーザー登録のためのビューモデル
    /// </summary>
    public class UserRegisterViewModel
    {
        /// <summary>ユーザーのメールアドレス</summary>
        [Required(ErrorMessage = "メールアドレスは必須です。")]
        [EmailAddress(ErrorMessage = "有効なメールアドレスを入力してください。")]
        public string Email { get; set; } = string.Empty;


        /// <summary>ユーザーのパスワード</summary>
        [Required(ErrorMessage = "パスワードは必須です。")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "パスワードは少なくとも{2}文字以上である必要があります。", MinimumLength = 8)]
        public string Password { get; set; } = string.Empty;


        /// <summary>確認用パスワード</summary>
        [Required(ErrorMessage = "確認用パスワードは必須です。")]
        [Compare("Password", ErrorMessage = "パスワードと確認用パスワードが一致しません。")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}