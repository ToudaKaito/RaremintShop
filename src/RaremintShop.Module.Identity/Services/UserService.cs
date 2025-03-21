using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RaremintShop.Module.Identity.Models;
using Microsoft.Extensions.Logging;

namespace RaremintShop.Module.Identity.Services
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

        /// <summary>ロガー</summary>
        private readonly ILogger<UserService> _logger;


        /// <summary>
        /// UserService クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="userManager">ユーザー管理のためのUserManager</param>
        /// <param name="signInManager">サインイン管理のためのSignInManager</param>
        /// <param name="roleManager">ロール管理のためのRoleManager</param>
        /// <param name="logger">ロギングのためのILogger</param>
        /// <exception cref="ArgumentNullException">引数がnullの場合にスローされます</exception>
        public UserService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, ILogger<UserService> logger)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        /// <summary>
        /// 新しいユーザーを登録します。
        /// </summary>
        /// <param name="model">ユーザー登録のためのモデル</param>
        /// <returns>ユーザー登録の結果を表すIdentityResult</returns>
        /// <exception cref="ArgumentNullException">モデルがnullの場合にスローされます</exception>
        public async Task<IdentityResult> RegisterUserAsync(UserRegisterViewModel model)
        {
            ArgumentNullException.ThrowIfNull(model);

            try
            {
                var user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ユーザー登録中にエラーが発生しました。");
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
            ArgumentNullException.ThrowIfNull(model);

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
            catch (Exception ex)
            {
                _logger.LogError(ex, "ユーザーログイン中にエラーが発生しました。");
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
<<<<<<< HEAD
            catch (Exception ex)
            {
                _logger.LogError(ex, "ユーザーログアウト中にエラーが発生しました。");
                throw;
            }
=======

            return usersList;
        }

        // メールアドレスからユーザーを取得
        public async Task<IdentityUser> GetByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        // ID からユーザーを取得
        public async Task<IdentityUser> GetByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        // IDからユーザーを取得(Edit用)
        public async Task<UserEditViewModel> GetByIdForEditAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var roles = await GetAllRolesAsync(); // await を追加してタスクを完了させる
            var role = roles.FirstOrDefault()?.Name; // 役割名を取得
            var model = new UserEditViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Role = role,
                AvailableRoles = roles.Select(r => r.Name).ToList(), // 役割名のリストに変換
                IsActive = user.LockoutEnd == null
            };
            return model;
>>>>>>> d4707bf1d5c41df3c00e04f20dbe6bdff7013462
        }


        /// <summary>
        /// ユーザーを削除します。
        /// </summary>
        /// <param name="user">削除するユーザー</param>
        /// <returns>ユーザー削除の結果を表すIdentityResult</returns>
        public async Task<IdentityResult> DeleteUserAsync(IdentityUser user)
        {
            ArgumentNullException.ThrowIfNull(user);

            try
            {
                return await _userManager.DeleteAsync(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ユーザー削除中にエラーが発生しました。");
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
            ArgumentNullException.ThrowIfNull(model);

            try
            {
                var user = await _userManager.FindByIdAsync(model.Id);
                user.Email = model.Email;
                user.UserName = model.Email;

                // アカウントの有効・無効を設定
                user.LockoutEnd = model.IsActive ? null : DateTimeOffset.MaxValue;

                // 既存のロールを削除し、新しいロールを設定
                var currentRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, currentRoles);
                await _userManager.AddToRolesAsync(user, new List<string> { model.Role });

                // ユーザー情報を更新
                return await _userManager.UpdateAsync(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ユーザー情報更新中にエラーが発生しました。");
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
                        UserName = user.UserName,
                        Email = user.Email,
                        Role = role
                    });
                }

                return usersList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "全ユーザー取得中にエラーが発生しました。");
                throw;
            }
        }


        /// <summary>
        /// メールアドレスからユーザーを取得します。
        /// </summary>
        /// <param name="email">メールアドレス</param>
        /// <returns>ユーザー</returns>
        public async Task<IdentityUser> GetByEmailAsync(string email)
        {
            ArgumentNullException.ThrowIfNull(email);

            try
            {
                return await _userManager.FindByEmailAsync(email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "メールアドレスからユーザー取得中にエラーが発生しました。");
                throw;
            }
        }


        /// <summary>
        /// IDからユーザーを取得します。
        /// </summary>
        /// <param name="id">ユーザーID</param>
        /// <returns>ユーザー</returns>
        public async Task<IdentityUser> GetByIdAsync(string id)
        {
            ArgumentNullException.ThrowIfNull(id);

            try
            {
                return await _userManager.FindByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "IDからユーザー取得中にエラーが発生しました。");
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
            ArgumentNullException.ThrowIfNull(id);

            try
            {
                var user = await _userManager.FindByIdAsync(id);
                var roles = await _userManager.GetRolesAsync(user);
                var role = roles.FirstOrDefault();
                var model = new UserEditViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    Role = role,
                    AvailableRoles = roles.ToList(),
                    IsActive = user.LockoutEnd == null
                };
                return model;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "IDからユーザー取得中にエラーが発生しました（編集用）。");
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
            ArgumentNullException.ThrowIfNull(user);

            try
            {
                return await _userManager.GetRolesAsync(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ユーザーのロール取得中にエラーが発生しました。");
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "全てのロール取得中にエラーが発生しました。");
                throw;
            }
        }
    }
}