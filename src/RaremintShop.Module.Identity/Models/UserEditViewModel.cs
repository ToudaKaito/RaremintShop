using System.ComponentModel.DataAnnotations;

namespace RaremintShop.Module.Identity.Models
{
    public class UserEditViewModel
    {
<<<<<<< HEAD
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
=======
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

>>>>>>> d4707bf1d5c41df3c00e04f20dbe6bdff7013462
        public bool IsActive { get; set; }
    }
}
