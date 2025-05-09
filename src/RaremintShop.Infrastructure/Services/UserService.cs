using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RaremintShop.Module.Identity.Models;
using Microsoft.Extensions.Logging;
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
        /// 新しいユーザーを登録します。
        /// </summary>
        /// <param name="model">ユーザー登録のためのモデル</param>
        /// <returns>ユーザー登録の結果を表すIdentityResult</returns>
        /// <exception cref="ArgumentNullException">モデルがnullの場合にスローされます</exception>
        public async Task<IdentityResult> RegisterUserAsync(UserRegisterViewModel model)
        {
            ArgumentNullException.ThrowIfNull(model); // nullの場合は例外をスロー

            try
            {
                var user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if(!result.Succeeded)
                {
                    return result;
                }

                // ユーザーにロールを割り当て
                var roleResult = await _userManager.AddToRoleAsync(user, Roles.User);
                if (!roleResult.Succeeded)
                {
                    // ロールの割り当てに失敗した場合、ユーザーを削除
                    await _userManager.DeleteAsync(user);
                    return roleResult;
                }

                return result;
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// ユーザーのログインを処理します。
        /// </summary>
        /// <param name="model">ユーザーログインのためのモデル</param>
        /// <returns>ログインの結果を表すSignInResult</returns>
        /// <exception cref="ArgumentNullException">モデルがnullの場合にスローされます</exception>
        public async Task<SignInResult> LoginAsync(UserLoginViewModel model)
        {
            ArgumentNullException.ThrowIfNull(model); // nullの場合は例外をスロー

            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    return SignInResult.Failed; // ユーザーが存在しない
                }

                var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                return result;
            }
            catch
            {
                throw;
            }
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
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// ユーザーを削除します。
        /// </summary>
        /// <param name="user">削除するユーザー</param>
        /// <returns>ユーザー削除の結果を表すIdentityResult</returns>
        public async Task<IdentityResult> DeleteUserAsync(IdentityUser user)
        {
            ArgumentNullException.ThrowIfNull(user); // nullの場合は例外をスロー

            try
            {
                return await _userManager.DeleteAsync(user);
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// ユーザー情報を更新します。
        /// </summary>
        /// <param name="model">ユーザー編集のためのモデル</param>
        /// <returns>ユーザー情報更新の結果を表すIdentityResult</returns>
        /// <exception cref="ArgumentNullException">モデルがnullの場合にスローされます</exception>
        public async Task<IdentityResult> UpdateUserAsync(UserEditViewModel model)
        {
            ArgumentNullException.ThrowIfNull(model); // nullの場合は例外をスロー

            try
            {
                var user = await _userManager.FindByIdAsync(model.Id);
                if (user == null)
                {
                    throw new InvalidOperationException(ErrorMessages.UserNotFound);
                }

                user.Email = model.Email ?? throw new ArgumentNullException(nameof(model));
                user.UserName = model.Email ?? throw new ArgumentNullException(nameof(model));

                // アカウントの有効・無効を設定
                user.LockoutEnd = model.IsActive ? null : DateTimeOffset.MaxValue;

                // 既存のロールを削除し、新しいロールを設定
                var currentRoles = await _userManager.GetRolesAsync(user);

                var removeRolesResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
                if (!removeRolesResult.Succeeded) // 削除に失敗した場合
                {
                    return removeRolesResult;
                }

                var addRolesResult = await _userManager.AddToRoleAsync(user, model.Role);
                if (!addRolesResult.Succeeded) // 追加に失敗した場合
                {
                    return addRolesResult;
                }

                // ユーザー情報を更新
                return await _userManager.UpdateAsync(user);
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// 全ユーザーを取得します。
        /// </summary>
        /// <returns>全ユーザーのリスト</returns>
        public async Task<List<UserManagementViewModel>> GetAllUsersAsync()
        {
            try
            {
                var users = await _userManager.Users.ToListAsync();
                var usersList = new List<UserManagementViewModel>();

                foreach (var user in users)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    var role = roles.FirstOrDefault();
                    usersList.Add(new UserManagementViewModel
                    {
                        Id = user.Id,
                        UserName = user.UserName ?? string.Empty, // Null 参照代入の可能性を回避
                        Email = user.Email ?? string.Empty,       // Null 参照代入の可能性を回避
                        Role = role ?? string.Empty               // Null 参照代入の可能性を回避
                    });
                }

                return usersList;
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// メールアドレスからユーザーを取得します。
        /// </summary>
        /// <param name="email">メールアドレス</param>
        /// <returns>ユーザー,見つからなければnullか空のIdentityUser</returns>
        public async Task<IdentityUser?> GetByEmailAsync(string email)
        {
            ArgumentNullException.ThrowIfNull(email);

            try
            {
                return await _userManager.FindByEmailAsync(email);
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// IDからユーザーを取得します。
        /// </summary>
        /// <param name="id">ユーザーID</param>
        /// <returns>ユーザー,見つからなければnullか空のIdentityUser</returns>
        public async Task<IdentityUser?> GetByIdAsync(string id)
        {
            ArgumentNullException.ThrowIfNull(id);

            try
            {
                return await _userManager.FindByIdAsync(id);
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// IDからユーザーを取得します（編集用）。
        /// </summary>
        /// <param name="id">ユーザーID</param>
        /// <returns>ユーザー編集のためのモデル</returns>
        public async Task<UserEditViewModel> GetByIdForEditAsync(string id)
        {
            ArgumentNullException.ThrowIfNull(id); // nullの場合は例外をスロー

            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                {
                    throw new InvalidOperationException(ErrorMessages.UserNotFound);
                }
                var roles = await _userManager.GetRolesAsync(user);
                var role = roles.FirstOrDefault();
                var model = new UserEditViewModel
                {
                    Id = user.Id,
                    Email = user.Email ?? string.Empty, // Null 参照代入の可能性を回避
                    Role = role ?? string.Empty,        // Null 参照代入の可能性を回避
                    AvailableRoles = roles.ToList(),
                    IsActive = user.LockoutEnd == null
                };
                return model;
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// ユーザーのロールを取得します。
        /// </summary>
        /// <param name="user">ユーザー</param>
        /// <returns>ユーザーのロールのリスト</returns>
        public async Task<IList<string>> GetRolesAsync(IdentityUser user)
        {
            ArgumentNullException.ThrowIfNull(user); // nullの場合は例外をスロー

            try
            {
                return await _userManager.GetRolesAsync(user);
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// 全てのロールを取得します。
        /// </summary>
        /// <returns>全てのロールのリスト</returns>
        public async Task<List<IdentityRole>> GetAllRolesAsync()
        {
            try
            {
                return await _roleManager.Roles.ToListAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}