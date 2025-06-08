using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RaremintShop.Core.DTOs;
using RaremintShop.Core.Interfaces.Services;
using RaremintShop.Shared.Exceptions;
using static RaremintShop.Shared.Constants;

namespace RaremintShop.Infrastructure.Services
{
    /// <summary>
    /// ユーザー管理、認証、およびロール管理のためのメソッドを提供します。
    /// </summary>
    public class UserService : IUserService
    {
        /// <summary>ユーザー管理のためのUserManager</summary>
        private readonly UserManager<IdentityUser> _userManager;

        /// <summary>認証のためのSignInManager</summary>
        private readonly SignInManager<IdentityUser> _signInManager;

        /// <summary>ロール管理のためのRoleManager</summary>
        private readonly RoleManager<IdentityRole> _roleManager;


        /// <summary>
        /// UserService クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="userManager">ユーザー管理のためのUserManager</param>
        /// <param name="signInManager">サインイン管理のためのSignInManager</param>
        /// <param name="roleManager">ロール管理のためのRoleManager</param>
        /// <param name="logger">ロギングのためのILogger</param>
        /// <exception cref="ArgumentNullException">引数がnullの場合にスローされます</exception>
        public UserService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
        }


        /// <summary>
        /// ユーザー登録処理
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public async Task RegisterUserAsync(UserDto userDto)
        {
            ArgumentNullException.ThrowIfNull(userDto, nameof(userDto)); // nullの場合は例外をスロー

            // メールアドレスの重複チェック
            var existingUserByEmail = await _userManager.FindByEmailAsync(userDto.Email);
            if (existingUserByEmail != null)
            {
                throw new BusinessException(ErrorMessages.DuplicateEmail);
            }

            // dto→IdentityUser変換
            var user = new IdentityUser
            {
                UserName = userDto.Email,
                Email = userDto.Email,
            };

            // ユーザーの作成
            var createResult = await _userManager.CreateAsync(user, userDto.Password);
            if (!createResult.Succeeded)
            {
                // debug用
                foreach (var error in createResult.Errors)
                {
                    Console.WriteLine($"Error: {error.Description}");
                }

                throw new BusinessException(ErrorMessages.UserRegisterError); // 登録失敗
            }

            // 管理者メールアドレスならAdminロール、それ以外はUserロールを付与
            // StringComparison.OrdinalIgnoreCase:大文字小文字区別しない
            string roleToAssign = userDto.Email.Equals(AdminUser.Email, StringComparison.OrdinalIgnoreCase)
                ? Roles.Admin
                : Roles.User;

            // ユーザーにロールを割り当て
            var roleResult = await _userManager.AddToRoleAsync(user, roleToAssign);
            if (!roleResult.Succeeded)
            {
                // debug用
                foreach (var error in roleResult.Errors)
                {
                    Console.WriteLine($"Error: {error.Description}");
                }

                // ロールの割り当てに失敗した場合、ユーザーを削除
                await _userManager.DeleteAsync(user);
                throw new BusinessException(ErrorMessages.UserRegisterError); // 登録失敗
            }
        }


        /// <summary>
        /// ユーザーのログインを処理します。
        /// </summary>
        /// <param name="model">ユーザーログインのためのモデル</param>
        /// <returns>ログインの結果を表すSignInResult</returns>
        /// <exception cref="ArgumentNullException">モデルがnullの場合にスローされます</exception>
        public async Task<string> LoginAsync(UserDto userDto)
        {
            ArgumentNullException.ThrowIfNull(userDto, nameof(userDto)); // nullの場合は例外をスロー

            // ユーザーの存在確認
            var user = await _userManager.FindByEmailAsync(userDto.Email) 
                ?? throw new BusinessException(ErrorMessages.UserNotFound);

            // パスワードサインイン
            var result = await _signInManager.PasswordSignInAsync(user, userDto.Password, isPersistent: false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                throw new BusinessException(ErrorMessages.InvalidLogin);
            }

            // ロールの取得
            var roles = await _userManager.GetRolesAsync(user);
            if (roles == null || roles.Count == 0)
            {
                throw new BusinessException(ErrorMessages.RoleFetchError);
            }

            return roles.FirstOrDefault() ?? string.Empty; // ユーザーのロールを取得
        }


        /// <summary>
        /// ユーザーのログアウトを処理します。
        /// </summary>
        public async Task LogoutAsync()
        {
            try
            {
                await _signInManager.SignOutAsync();
            }
            catch (Exception ex)
            {
                // ここでBusinessExceptionを投げる
                throw new BusinessException(ErrorMessages.UserLogoutError, ex);
            }
        }


        /// <summary>
        /// ユーザーを削除します。
        /// </summary>
        /// <param name="user">削除するユーザー</param>
        /// <returns>ユーザー削除の結果を表すIdentityResult</returns>
        public async Task DeleteUserAsync(string id)
        {
            ArgumentNullException.ThrowIfNull(id); // nullの場合は例外をスロー

            // ユーザーの取得
            var user = await _userManager.FindByIdAsync(id)
                ?? throw new BusinessException(ErrorMessages.UserNotFound);

            // ユーザーの削除
            var deleteResult = await _userManager.DeleteAsync(user);
            if (!deleteResult.Succeeded) // 削除に失敗した場合
            {
                throw new BusinessException(ErrorMessages.UserDeleteError);
            }
        }


        /// <summary>
        /// ユーザー情報を更新します。
        /// </summary>
        /// <param name="model">ユーザー編集のためのモデル</param>
        /// <returns>ユーザー情報更新の結果を表すIdentityResult</returns>
        /// <exception cref="ArgumentNullException">モデルがnullの場合にスローされます</exception>
        public async Task UpdateUserAsync(UserDto userDto)
        {
            ArgumentNullException.ThrowIfNull(userDto); // nullの場合は例外をスロー

            var user = await _userManager.FindByIdAsync(userDto.Id)
                ?? throw new BusinessException(ErrorMessages.UserNotFound);

            // DTO→IdentityUser変換
            user.UserName = userDto.UserName ?? string.Empty;
            user.Email = userDto.Email ?? string.Empty;
            user.LockoutEnd = userDto.IsActive && userDto.IsActive ? null : DateTimeOffset.MaxValue;

            // 既存ロールを削除
            var currentRoles = await _userManager.GetRolesAsync(user);
            var removeRolesResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
            if(!removeRolesResult.Succeeded) // 削除に失敗した場合
            {
                throw new BusinessException(ErrorMessages.RoleDeleteError);
            }

            // 新しいロールを追加
            var addRolesResult =await _userManager.AddToRoleAsync(user, userDto.Role);
            if (!addRolesResult.Succeeded) // 追加に失敗した場合
            {
                throw new BusinessException(ErrorMessages.RoleChangeError);
            }

            // ユーザー情報の更新
            var updateUserResult = await _userManager.UpdateAsync(user);
            if (!updateUserResult.Succeeded) // 更新に失敗した場合
            {
                throw new BusinessException(ErrorMessages.UserUpdateError);
            }
        }


        /// <summary>
        /// 全ユーザーを取得します。
        /// </summary>
        /// <returns>全ユーザーのリスト</returns>
        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            var usersList = new List<UserDto>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var role = roles.FirstOrDefault();
                usersList.Add(new UserDto
                {
                    Id = user.Id,
                    UserName = user.UserName ?? string.Empty, // Null 参照代入の可能性を回避
                    Email = user.Email ?? string.Empty,       // Null 参照代入の可能性を回避
                    Role = role ?? string.Empty               // Null 参照代入の可能性を回避
                });
            }

            return usersList;
        }


        /// <summary>
        /// IDからユーザーを取得します。
        /// </summary>
        /// <param name="id">ユーザーID</param>
        /// <returns>ユーザー,見つからなければnullか空のIdentityUser</returns>
        public async Task<UserDto> GetByIdAsync(string id)
        {
            ArgumentNullException.ThrowIfNull(id);

            var user = await _userManager.FindByIdAsync(id)
                ?? throw new BusinessException(ErrorMessages.UserNotFound);

            // IdentityUserをUserDtoに変換
            var userDto = new UserDto
            {
                Id = user.Id,
                UserName = user.UserName ?? string.Empty, // Null 参照代入の可能性を回避
                Email = user.Email ?? string.Empty        // Null 参照代入の可能性を回避
            };

            return userDto;
        }


        /// <summary>
        /// ユーザーのロールを取得します。
        /// </summary>
        /// <param name="user">ユーザー</param>
        /// <returns>ユーザーのロールのリスト</returns>
        public async Task<IList<string>> GetRolesAsync(string id)
        {
            ArgumentNullException.ThrowIfNull(id); // nullの場合は例外をスロー

            // ユーザーの存在確認
            var user = await _userManager.FindByIdAsync(id) 
                ?? throw new BusinessException(ErrorMessages.UserNotFound);

            return await _userManager.GetRolesAsync(user);
        }
    }
}