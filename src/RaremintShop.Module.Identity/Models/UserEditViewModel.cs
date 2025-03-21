using System.ComponentModel.DataAnnotations;

namespace RaremintShop.Module.Identity.Models
{
    public class UserEditViewModel
    {
        /// <summary>ユーザーID</summary>
        public string Id { get; set; } = string.Empty;

        /// <summary>ユーザー名</summary>
        [Required(ErrorMessage = "ユーザー名は必須です。")]
        [StringLength(100, ErrorMessage = "ユーザー名は{1}文字以内で入力してください。")]
        public string UserName { get; set; } = string.Empty;

        /// <summary>ユーザーのメールアドレス</summary>
        [Required(ErrorMessage = "メールアドレスは必須です。")]
        [EmailAddress(ErrorMessage = "有効なメールアドレスを入力してください。")]
        public string Email { get; set; } = string.Empty;

        /// <summary>ユーザーのロール</summary>
        [Required(ErrorMessage = "ロールは必須です。")]
        public string Role { get; set; } = string.Empty;

        /// <summary>利用可能なロールのリスト</summary>
        public List<string> AvailableRoles { get; set; } = [];

        /// <summary>ユーザーがアクティブかどうかを示すフラグ</summary>
        public bool IsActive { get; set; }
    }
}
